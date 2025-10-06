using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace bangna_hospital.services
{
    // ========================================
    // MODELS
    // ========================================

    #region Request/Response Models

    public class ClaudeMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class ClaudeRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }

        [JsonPropertyName("messages")]
        public List<ClaudeMessage> Messages { get; set; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        [JsonPropertyName("system")]
        public string System { get; set; }

        [JsonPropertyName("stream")]
        public bool Stream { get; set; } = false;
    }

    public class ClaudeContentBlock
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class ClaudeUsage
    {
        [JsonPropertyName("input_tokens")]
        public int InputTokens { get; set; }

        [JsonPropertyName("output_tokens")]
        public int OutputTokens { get; set; }

        public decimal CalculateCost(string model)
        {
            decimal inputCost = model.Contains("sonnet") ? 3.00m :
                               model.Contains("opus") ? 15.00m : 0.25m;
            decimal outputCost = model.Contains("sonnet") ? 15.00m :
                                model.Contains("opus") ? 75.00m : 1.25m;

            return (InputTokens * inputCost / 1_000_000m) +
                   (OutputTokens * outputCost / 1_000_000m);
        }
    }

    public class ClaudeResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public List<ClaudeContentBlock> Content { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("stop_reason")]
        public string StopReason { get; set; }

        [JsonPropertyName("usage")]
        public ClaudeUsage Usage { get; set; }

        public string Text => Content?.FirstOrDefault()?.Text ?? string.Empty;
        public decimal Cost => Usage?.CalculateCost(Model) ?? 0;
    }

    public class ClaudeErrorResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("error")]
        public ClaudeErrorDetail Error { get; set; }
    }

    public class ClaudeErrorDetail
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    #endregion

    #region Configuration

    public class ClaudeConfig
    {
        public string ApiKey { get; set; }
        public string DefaultModel { get; set; } = "claude-sonnet-4-5-20250929";
        public int DefaultMaxTokens { get; set; } = 1024;
        public double? DefaultTemperature { get; set; } = null;
        public int TimeoutSeconds { get; set; } = 60;
        public int MaxRetries { get; set; } = 3;
        public int MaxConcurrentRequests { get; set; } = 10;
        public bool EnableLogging { get; set; } = true;
        public bool EnableCaching { get; set; } = false;
        public string LogFilePath { get; set; } = null;
    }

    #endregion

    // ========================================
    // CLAUDE API CLIENT - เวอร์ชันใหม่
    // ========================================

    public class ClaudeApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ClaudeConfig _config;
        private readonly SemaphoreSlim _rateLimiter;
        private readonly Dictionary<string, ClaudeResponse> _cache;
        private readonly object _cacheLock = new object();

        private const string API_URL = "https://api.anthropic.com/v1/messages";
        private const string API_VERSION = "2023-06-01";

        public ClaudeApiClient(ClaudeConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            ValidateConfig();

            _httpClient = CreateHttpClient();
            _rateLimiter = new SemaphoreSlim(_config.MaxConcurrentRequests, _config.MaxConcurrentRequests);
            _cache = new Dictionary<string, ClaudeResponse>();

            Log("✅ ClaudeApiClient initialized");
            Log($"📍 API URL: {API_URL}");
        }

        public async Task<ClaudeResponse> SendMessageAsync(
            string message,
            string model = null,
            int? maxTokens = null,
            string systemPrompt = null)
        {
            var messages = new List<ClaudeMessage>
            {
                new ClaudeMessage { Role = "user", Content = message }
            };

            return await SendAsync(messages, model, maxTokens, systemPrompt);
        }

        public async Task<ClaudeResponse> SendConversationAsync(
            List<ClaudeMessage> messages,
            string model = null,
            int? maxTokens = null,
            string systemPrompt = null)
        {
            return await SendAsync(messages, model, maxTokens, systemPrompt);
        }

        public async Task<ClaudeResponse> SendRequestAsync(ClaudeRequest request)
        {
            ValidateRequest(request);

            if (_config.EnableCaching)
            {
                var cacheKey = GenerateCacheKey(request);
                lock (_cacheLock)
                {
                    if (_cache.ContainsKey(cacheKey))
                    {
                        Log("📦 Cache hit");
                        return _cache[cacheKey];
                    }
                }
            }

            await _rateLimiter.WaitAsync();

            try
            {
                var response = await SendWithRetryAsync(request);

                if (_config.EnableCaching && response != null)
                {
                    var cacheKey = GenerateCacheKey(request);
                    lock (_cacheLock)
                    {
                        _cache[cacheKey] = response;
                    }
                }

                return response;
            }
            finally
            {
                _rateLimiter.Release();
            }
        }

        public void ClearCache()
        {
            lock (_cacheLock)
            {
                _cache.Clear();
                Log("🗑️ Cache cleared");
            }
        }

        private async Task<ClaudeResponse> SendAsync(
            List<ClaudeMessage> messages,
            string model = null,
            int? maxTokens = null,
            string systemPrompt = null)
        {
            var request = new ClaudeRequest
            {
                Model = model ?? _config.DefaultModel,
                MaxTokens = maxTokens ?? _config.DefaultMaxTokens,
                Temperature = _config.DefaultTemperature,
                System = systemPrompt,
                Messages = messages
            };

            return await SendRequestAsync(request);
        }

        private async Task<ClaudeResponse> SendWithRetryAsync(ClaudeRequest request)
        {
            Exception lastException = null;

            for (int attempt = 1; attempt <= _config.MaxRetries; attempt++)
            {
                try
                {
                    Log($"🔄 Attempt {attempt}/{_config.MaxRetries}");
                    return await SendHttpRequestAsync(request);
                }
                catch (HttpRequestException ex) when (attempt < _config.MaxRetries)
                {
                    lastException = ex;
                    var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));
                    Log($"⚠️ Request failed: {ex.Message}. Retrying in {delay.TotalSeconds}s...");
                    await Task.Delay(delay);
                }
                catch (ClaudeApiException ex) when (((int)ex.StatusCode == 429) && attempt < _config.MaxRetries)
                {
                    lastException = ex;
                    var retryAfter = GetRetryAfterSeconds(ex);
                    var delay = retryAfter > 0
                        ? TimeSpan.FromSeconds(retryAfter)
                        : TimeSpan.FromSeconds(Math.Pow(2, attempt + 3));

                    Log($"⏸️ Rate limited (429). Retrying in {delay.TotalSeconds}s...");
                    await Task.Delay(delay);
                }
            }

            throw lastException ?? new Exception("Request failed after all retries");
        }

        private int GetRetryAfterSeconds(ClaudeApiException ex)
        {
            try
            {
                if (ex.ErrorDetails?.Error?.Message?.Contains("retry after") == true)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(
                        ex.ErrorDetails.Error.Message,
                        @"(\d+)\s*second"
                    );

                    if (match.Success && int.TryParse(match.Groups[1].Value, out int seconds))
                    {
                        return seconds;
                    }
                }
            }
            catch { }

            return 0;
        }

        private async Task<ClaudeResponse> SendHttpRequestAsync(ClaudeRequest request)
        {
            // Serialize request
            var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = false
            });

            Log($"📤 Request: {TruncateForLog(json, 200)}");

            // Create content
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var startTime = DateTime.Now;

            // ใช้ URL เต็มโดยตรง - แก้ปัญหา 404
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, API_URL))
            {
                requestMessage.Content = content;

                // ส่ง request
                var httpResponse = await _httpClient.SendAsync(requestMessage);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var duration = DateTime.Now - startTime;

                Log($"📊 Status: {(int)httpResponse.StatusCode} {httpResponse.StatusCode} ({duration.TotalMilliseconds}ms)");

                // Check for errors
                if (!httpResponse.IsSuccessStatusCode)
                {
                    var error = TryParseError(responseString);
                    var errorMessage = error?.Error?.Message ?? responseString;

                    Log($"❌ Error: {errorMessage}");
                    Log($"❌ Response: {responseString}");

                    throw new ClaudeApiException(
                        $"API Error [{httpResponse.StatusCode}]: {errorMessage}",
                        httpResponse.StatusCode,
                        error
                    );
                }

                // Parse response
                var response = JsonSerializer.Deserialize<ClaudeResponse>(responseString);

                Log($"📥 Response: {TruncateForLog(response.Text, 200)}");
                Log($"💰 Tokens: {response.Usage?.InputTokens} in + {response.Usage?.OutputTokens} out = ${response.Cost:F4}");

                return response;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(_config.TimeoutSeconds)
            };

            // Set headers
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("x-api-key", _config.ApiKey);
            client.DefaultRequestHeaders.Add("anthropic-version", API_VERSION);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("ClaudeClient", "2.0")
            );

            Log("🔧 HttpClient created with headers:");
            Log($"   x-api-key: {_config.ApiKey.Substring(0, 20)}...");
            Log($"   anthropic-version: {API_VERSION}");

            return client;
        }

        private void ValidateConfig()
        {
            if (string.IsNullOrWhiteSpace(_config.ApiKey))
                throw new ArgumentException("API Key is required", nameof(_config.ApiKey));

            if (_config.TimeoutSeconds <= 0)
                throw new ArgumentException("Timeout must be positive", nameof(_config.TimeoutSeconds));

            if (_config.MaxRetries < 0)
                throw new ArgumentException("MaxRetries cannot be negative", nameof(_config.MaxRetries));
        }

        private void ValidateRequest(ClaudeRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Model))
                throw new ArgumentException("Model is required", nameof(request.Model));

            if (request.MaxTokens <= 0)
                throw new ArgumentException("MaxTokens must be positive", nameof(request.MaxTokens));

            if (request.Messages == null || !request.Messages.Any())
                throw new ArgumentException("Messages cannot be empty", nameof(request.Messages));
        }

        private ClaudeErrorResponse TryParseError(string responseString)
        {
            try
            {
                return JsonSerializer.Deserialize<ClaudeErrorResponse>(responseString);
            }
            catch
            {
                return null;
            }
        }

        private string GenerateCacheKey(ClaudeRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(json));
                return Convert.ToBase64String(hash);
            }
        }

        private string TruncateForLog(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength) + "...";
        }

        private void Log(string message)
        {
            if (!_config.EnableLogging) return;

            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}";
            Console.WriteLine(logMessage);

            if (!string.IsNullOrEmpty(_config.LogFilePath))
            {
                try
                {
                    System.IO.File.AppendAllText(
                        _config.LogFilePath,
                        logMessage + Environment.NewLine
                    );
                }
                catch
                {
                }
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _rateLimiter?.Dispose();
        }
    }

    // ========================================
    // CUSTOM EXCEPTION
    // ========================================

    public class ClaudeApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public ClaudeErrorResponse ErrorDetails { get; }

        public ClaudeApiException(
            string message,
            HttpStatusCode statusCode,
            ClaudeErrorResponse errorDetails = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorDetails = errorDetails;
        }
    }
}

// ========================================
// ตัวอย่างการใช้งาน
// ========================================
/*

using System;
using System.Threading.Tasks;
using bangna_hospital.services;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // สร้าง config
        var config = new ClaudeConfig
        {
            ApiKey = "sk-ant-api03-OJI_CWuS7aTSJlFj5hjfATNr1ihYD5H2TD0vucUhEDHm_Zt4C5OMhkhe8dLmPX_Owzlaf8cOkIvJRadrECVOw-zihRQAAA",  // ⚠️ ใส่ API Key ของคุณ
            DefaultModel = "claude-sonnet-4-5-20250929",
            DefaultMaxTokens = 1024,
            EnableLogging = true
        };

        // สร้าง client
        using (var client = new ClaudeApiClient(config))
        {
            try
            {
                Console.WriteLine("📤 Sending message...\n");
                
                var response = await client.SendMessageAsync("สวัสดีครับ");
                
                Console.WriteLine("\n✅ SUCCESS!");
                Console.WriteLine($"📥 Claude: {response.Text}");
                Console.WriteLine($"💰 Cost: ${response.Cost:F6}");
            }
            catch (ClaudeApiException ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
                Console.WriteLine($"Status Code: {ex.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
        }

        Console.WriteLine("\nกด Enter เพื่อออก...");
        Console.ReadLine();
    }
}

*/