using bangna_hospital.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bangna_hospital.services
{
    /// <summary>
    /// Claude API Client with built-in rate limiting
    /// </summary>
    public class RateLimitedClaudeClient : IDisposable
    {
        private readonly ClaudeApiClient _client;
        private readonly RateLimiter _rateLimiter;

        /// <summary>
        /// Initialize rate-limited Claude client
        /// </summary>
        /// <param name="config">Claude configuration</param>
        /// <param name="requestsPerMinute">Max requests per minute (default: 5 for free tier)</param>
        public RateLimitedClaudeClient(ClaudeConfig config, int requestsPerMinute = 5)
        {
            _client = new ClaudeApiClient(config);
            _rateLimiter = new RateLimiter(requestsPerMinute);
        }

        public async Task<ClaudeResponse> SendMessageAsync(
            string message,
            string model = null,
            int? maxTokens = null,
            string systemPrompt = null)
        {
            await _rateLimiter.WaitAsync();
            return await _client.SendMessageAsync(message, model, maxTokens, systemPrompt);
        }

        public async Task<ClaudeResponse> SendConversationAsync(
            List<ClaudeMessage> messages,
            string model = null,
            int? maxTokens = null,
            string systemPrompt = null)
        {
            await _rateLimiter.WaitAsync();
            return await _client.SendConversationAsync(messages, model, maxTokens, systemPrompt);
        }

        public async Task<ClaudeResponse> SendRequestAsync(ClaudeRequest request)
        {
            await _rateLimiter.WaitAsync();
            return await _client.SendRequestAsync(request);
        }

        public void ClearCache()
        {
            _client.ClearCache();
        }

        public void Dispose()
        {
            _client?.Dispose();
            _rateLimiter?.Dispose();
        }
    }

    /// <summary>
    /// Rate limiter using sliding window algorithm
    /// </summary>
    public class RateLimiter : IDisposable
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly Queue<DateTime> _requestTimes;
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;
        private readonly object _lock = new object();

        public RateLimiter(int requestsPerMinute)
        {
            _maxRequests = requestsPerMinute;
            _timeWindow = TimeSpan.FromMinutes(1);
            _semaphore = new SemaphoreSlim(1, 1);
            _requestTimes = new Queue<DateTime>();
        }

        public async Task WaitAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                lock (_lock)
                {
                    var now = DateTime.UtcNow;
                    var cutoff = now - _timeWindow;

                    // Remove old requests outside the window
                    while (_requestTimes.Count > 0 && _requestTimes.Peek() < cutoff)
                    {
                        _requestTimes.Dequeue();
                    }

                    // Check if we've hit the limit
                    if (_requestTimes.Count >= _maxRequests)
                    {
                        var oldestRequest = _requestTimes.Peek();
                        var waitTime = (oldestRequest + _timeWindow) - now;

                        if (waitTime > TimeSpan.Zero)
                        {
                            Console.WriteLine($"⏳ Rate limit: waiting {waitTime.TotalSeconds:F1}s...");
                            Thread.Sleep(waitTime);
                        }

                        // Remove the oldest after waiting
                        _requestTimes.Dequeue();
                    }

                    // Record this request
                    _requestTimes.Enqueue(DateTime.UtcNow);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Dispose()
        {
            _semaphore?.Dispose();
        }
    }

    /// <summary>
    /// Extended configuration with rate limit settings
    /// </summary>
    public class RateLimitedClaudeConfig : ClaudeConfig
    {
        /// <summary>
        /// Maximum requests per minute
        /// Free tier: 5, Tier 1: 50, Tier 2: 1000, Tier 3: 4000
        /// </summary>
        public int RequestsPerMinute { get; set; } = 5;

        /// <summary>
        /// Maximum tokens per minute
        /// Free tier: 20000, Tier 1: 40000, Tier 2: 80000
        /// </summary>
        public int TokensPerMinute { get; set; } = 20000;
    }
}

// ========================================
// USAGE EXAMPLES
// ========================================

namespace HIS.AI.Examples
{
    public class RateLimitExamples
    {
        public static async Task Example1_BasicUsage()
        {
            // Create rate-limited client (5 requests/min for free tier)
            var config = new ClaudeConfig
            {
                ApiKey = Environment.GetEnvironmentVariable("CLAUDE_API_KEY"),
                EnableLogging = true
            };

            using (var client = new RateLimitedClaudeClient(config, requestsPerMinute: 5))
            {
                // Send multiple requests - will auto-throttle
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine($"\n=== Request {i} ===");
                    var response = await client.SendMessageAsync($"คำถามที่ {i}");
                    Console.WriteLine($"Response: {response.Text.Substring(0, 50)}...");
                }
            }
        }

        public static async Task Example2_ChatWithRateLimit()
        {
            var config = new ClaudeConfig
            {
                ApiKey = Environment.GetEnvironmentVariable("CLAUDE_API_KEY")
            };

            // Free tier: 5 requests/min
            using (var client = new RateLimitedClaudeClient(config, requestsPerMinute: 5))
            {
                var messages = new List<ClaudeMessage>();

                // Simulate chat conversation
                string[] questions =
                {
                    "BMI คืออะไร?",
                    "คำนวณ BMI อย่างไร?",
                    "BMI 25 ถือว่าอ้วนไหม?",
                    "ควรลดน้ำหนักอย่างไร?",
                    "ออกกำลังกายแบบไหนดี?",
                    "กินอาหารอะไรดี?"
                };

                foreach (var question in questions)
                {
                    // Add user message
                    messages.Add(new ClaudeMessage { Role = "user", Content = question });

                    // Send to Claude (will auto-throttle if needed)
                    var response = await client.SendConversationAsync(messages);

                    // Add assistant response
                    messages.Add(new ClaudeMessage { Role = "assistant", Content = response.Text });

                    Console.WriteLine($"Q: {question}");
                    Console.WriteLine($"A: {response.Text.Substring(0, 100)}...\n");
                }
            }
        }

        public static async Task Example3_HighVolumeWithPaidTier()
        {
            var config = new ClaudeConfig
            {
                ApiKey = Environment.GetEnvironmentVariable("CLAUDE_API_KEY")
            };

            // Tier 2: 50 requests/min
            using (var client = new RateLimitedClaudeClient(config, requestsPerMinute: 50))
            {
                var tasks = new List<Task<ClaudeResponse>>();

                // Send 100 requests in parallel - will throttle to 50/min
                for (int i = 1; i <= 100; i++)
                {
                    int index = i; // Capture for lambda
                    tasks.Add(Task.Run(async () =>
                        await client.SendMessageAsync($"Question {index}")
                    ));
                }

                var responses = await Task.WhenAll(tasks);
                Console.WriteLine($"Completed {responses.Length} requests");
            }
        }
    }
}
