using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using bangna_hospital.services;

namespace bangna_hospital.services
{
    // ========================================
    // SQL SCRIPT สำหรับสร้างตาราง
    // ========================================
    /*
    
    -- สร้างตาราง claude_chat_logs
    CREATE TABLE claude_chat_logs (
        log_id BIGINT IDENTITY(1,1) PRIMARY KEY,
        session_id NVARCHAR(100) NOT NULL,
        user_id NVARCHAR(50) NULL,
        user_name NVARCHAR(200) NULL,
        
        -- ข้อความ
        user_message NVARCHAR(MAX) NOT NULL,
        assistant_message NVARCHAR(MAX) NULL,
        
        -- ข้อมูล API
        model NVARCHAR(50) NULL,
        system_prompt NVARCHAR(MAX) NULL,
        
        -- Token และค่าใช้จ่าย
        input_tokens INT NULL,
        output_tokens INT NULL,
        total_tokens INT NULL,
        cost_usd DECIMAL(10, 6) NULL,
        
        -- เวลา
        created_at DATETIME NOT NULL DEFAULT GETDATE(),
        response_time_ms INT NULL,
        
        -- Status
        status NVARCHAR(20) NULL, -- 'success', 'error', 'timeout'
        error_message NVARCHAR(MAX) NULL,
        
        -- เพิ่มเติม
        ip_address NVARCHAR(50) NULL,
        department NVARCHAR(100) NULL,
        
        INDEX IX_session_id (session_id),
        INDEX IX_user_id (user_id),
        INDEX IX_created_at (created_at)
    );

    -- สร้างตาราง claude_sessions
    CREATE TABLE claude_sessions (
        session_id NVARCHAR(100) PRIMARY KEY,
        user_id NVARCHAR(50) NULL,
        user_name NVARCHAR(200) NULL,
        started_at DATETIME NOT NULL DEFAULT GETDATE(),
        ended_at DATETIME NULL,
        total_messages INT DEFAULT 0,
        total_cost_usd DECIMAL(10, 6) DEFAULT 0,
        department NVARCHAR(100) NULL,
        
        INDEX IX_user_id (user_id),
        INDEX IX_started_at (started_at)
    );

    */

    // ========================================
    // MODELS
    // ========================================

    public class ChatLog
    {
        public long LogId { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string UserMessage { get; set; }
        public string AssistantMessage { get; set; }

        public string Model { get; set; }
        public string SystemPrompt { get; set; }

        public int? InputTokens { get; set; }
        public int? OutputTokens { get; set; }
        public int? TotalTokens { get; set; }
        public decimal? CostUsd { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? ResponseTimeMs { get; set; }

        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public string IpAddress { get; set; }
        public string Department { get; set; }
    }

    public class ChatSession
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int TotalMessages { get; set; }
        public decimal TotalCostUsd { get; set; }
        public string Department { get; set; }
    }

    // ========================================
    // CHAT LOGGER
    // ========================================

    public class ClaudeChatLogger
    {
        private readonly string _connectionString;
        private readonly string _sessionId;
        private readonly string _userId;
        private readonly string _userName;
        private readonly string _department;

        public ClaudeChatLogger(
            string connectionString,
            string userId = null,
            string userName = null,
            string department = null)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _sessionId = Guid.NewGuid().ToString();
            _userId = userId;
            _userName = userName;
            _department = department;

            // สร้าง session ใหม่
            CreateSession().Wait();
        }

        // ========================================
        // สร้าง Session
        // ========================================

        public async Task CreateSession()
        {
            const string sql = @"
                INSERT INTO claude_sessions (session_id, user_id, user_name, started_at, department)
                VALUES (@SessionId, @UserId, @UserName, GETDATE(), @Department)";

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SessionId", _sessionId);
                    cmd.Parameters.AddWithValue("@UserId", (object)_userId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserName", (object)_userName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Department", (object)_department ?? DBNull.Value);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // ========================================
        // บันทึก Chat Log
        // ========================================

        public async Task<long> LogChatAsync(
            string userMessage,
            ClaudeResponse response = null,
            string model = null,
            string systemPrompt = null,
            int responseTimeMs = 0,
            string status = "success",
            string errorMessage = null)
        {
            const string sql = @"
                INSERT INTO claude_chat_logs (
                    session_id, user_id, user_name, department,
                    user_message, assistant_message,
                    model, system_prompt,
                    input_tokens, output_tokens, total_tokens, cost_usd,
                    created_at, response_time_ms,
                    status, error_message
                )
                VALUES (
                    @SessionId, @UserId, @UserName, @Department,
                    @UserMessage, @AssistantMessage,
                    @Model, @SystemPrompt,
                    @InputTokens, @OutputTokens, @TotalTokens, @CostUsd,
                    GETDATE(), @ResponseTimeMs,
                    @Status, @ErrorMessage
                );
                SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SessionId", _sessionId);
                    cmd.Parameters.AddWithValue("@UserId", (object)_userId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserName", (object)_userName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Department", (object)_department ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@UserMessage", userMessage);
                    cmd.Parameters.AddWithValue("@AssistantMessage", (object)response?.Text ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@Model", (object)model ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SystemPrompt", (object)systemPrompt ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@InputTokens", (object)response?.Usage?.InputTokens ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OutputTokens", (object)response?.Usage?.OutputTokens ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TotalTokens",
                        (object)(response?.Usage != null ? response.Usage.InputTokens + response.Usage.OutputTokens : (int?)null)
                        ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CostUsd", (object)response?.Cost ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@ResponseTimeMs", responseTimeMs);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ErrorMessage", (object)errorMessage ?? DBNull.Value);

                    var logId = await cmd.ExecuteScalarAsync();

                    // อัพเดท session
                    await UpdateSessionAsync(response?.Cost ?? 0);

                    return Convert.ToInt64(logId);
                }
            }
        }

        // ========================================
        // อัพเดท Session
        // ========================================

        private async Task UpdateSessionAsync(decimal cost)
        {
            const string sql = @"
                UPDATE claude_sessions
                SET total_messages = total_messages + 1,
                    total_cost_usd = total_cost_usd + @Cost
                WHERE session_id = @SessionId";

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SessionId", _sessionId);
                    cmd.Parameters.AddWithValue("@Cost", cost);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // ========================================
        // ปิด Session
        // ========================================

        public async Task EndSessionAsync()
        {
            const string sql = @"
                UPDATE claude_sessions
                SET ended_at = GETDATE()
                WHERE session_id = @SessionId";

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SessionId", _sessionId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // ========================================
        // ดึงประวัติการ Chat
        // ========================================

        public async Task<DataTable> GetChatHistoryAsync(int limit = 50)
        {
            const string sql = @"
                SELECT TOP (@Limit)
                    log_id, session_id, user_id, user_name,
                    user_message, assistant_message,
                    model, input_tokens, output_tokens, cost_usd,
                    created_at, response_time_ms, status
                FROM claude_chat_logs
                WHERE session_id = @SessionId
                ORDER BY created_at DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SessionId", _sessionId);
                    cmd.Parameters.AddWithValue("@Limit", limit);

                    var dt = new DataTable();
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }

        // Properties
        public string SessionId => _sessionId;
        public string UserId => _userId;
        public string UserName => _userName;
    }

    // ========================================
    // CLAUDE CLIENT WITH LOGGING
    // ========================================

    public class ClaudeApiClientWithLogging : ClaudeApiClient
    {
        private readonly ClaudeChatLogger _logger;
        public ClaudeApiClientWithLogging(
            ClaudeConfig config,
            ClaudeChatLogger logger) : base(config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ClaudeResponse> SendMessageWithLoggingAsync(String message, string model = null, int? maxTokens = null, String systemPrompt = null)
        {
            var startTime = DateTime.Now; ClaudeResponse response = null; string status = "success"; string errorMessage = null;
            try
            {
                // ส่งข้อความไป Claude
                response = await base.SendMessageAsync(message, model, maxTokens, systemPrompt);
            }
            catch (ClaudeApiException ex)
            {
                status = "error";
                errorMessage = $"[{ex.StatusCode}] {ex.Message}";
                throw;
            }
            catch (Exception ex)
            {
                status = "error";
                errorMessage = ex.Message;
                throw;
            }
            finally
            {
                // บันทึก log
                var responseTime = (int)(DateTime.Now - startTime).TotalMilliseconds;
                await _logger.LogChatAsync(
                    userMessage: message,
                    response: response,
                    model: model,
                    systemPrompt: systemPrompt,
                    responseTimeMs: responseTime,
                    status: status,
                    errorMessage: errorMessage
                );
            }

            return response;
        }
    }

    // ========================================
    // HELPER METHODS
    // ========================================

    public static class ChatLogReports
    {
        // รายงานสรุปการใช้งานรายวัน
        public static async Task<DataTable> GetDailyUsageReportAsync(
            string connectionString,
            DateTime date)
        {
            const string sql = @"
                SELECT 
                    user_id,
                    user_name,
                    department,
                    COUNT(*) as total_messages,
                    SUM(input_tokens) as total_input_tokens,
                    SUM(output_tokens) as total_output_tokens,
                    SUM(cost_usd) as total_cost,
                    AVG(response_time_ms) as avg_response_time,
                    COUNT(CASE WHEN status = 'error' THEN 1 END) as error_count
                FROM claude_chat_logs
                WHERE CAST(created_at AS DATE) = @Date
                GROUP BY user_id, user_name, department
                ORDER BY total_cost DESC";

            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);

                    var dt = new DataTable();
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }

        // ค้นหาการสนทนา
        public static async Task<DataTable> SearchChatsAsync(
            string connectionString,
            string searchText,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string userId = null)
        {
            var sql = @"
                SELECT 
                    log_id,
                    session_id,
                    user_id,
                    user_name,
                    user_message,
                    assistant_message,
                    created_at,
                    cost_usd
                FROM claude_chat_logs
                WHERE 1=1";

            if (!string.IsNullOrEmpty(searchText))
                sql += " AND (user_message LIKE @SearchText OR assistant_message LIKE @SearchText)";

            if (fromDate.HasValue)
                sql += " AND created_at >= @FromDate";

            if (toDate.HasValue)
                sql += " AND created_at <= @ToDate";

            if (!string.IsNullOrEmpty(userId))
                sql += " AND user_id = @UserId";

            sql += " ORDER BY created_at DESC";

            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(searchText))
                        cmd.Parameters.AddWithValue("@SearchText", $"%{searchText}%");

                    if (fromDate.HasValue)
                        cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);

                    if (toDate.HasValue)
                        cmd.Parameters.AddWithValue("@ToDate", toDate.Value);

                    if (!string.IsNullOrEmpty(userId))
                        cmd.Parameters.AddWithValue("@UserId", userId);

                    var dt = new DataTable();
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }
    }
}

// ========================================
// ตัวอย่างการใช้งาน
// ========================================
/*

using System;
using System.Configuration;
using System.Threading.Tasks;
using bangna_hospital.services;

public class Example
{
    static async Task Main()
    {
        // Connection String
        var connString = ConfigurationManager.ConnectionStrings["Hospital"].ConnectionString;

        // 1. สร้าง Logger
        var logger = new ClaudeChatLogger(
            connectionString: connString,
            userId: "DOC001",
            userName: "นพ.สมชาย ใจดี",
            department: "อายุรกรรม"
        );

        // 2. สร้าง Claude Client
        var config = new ClaudeConfig
        {
            ApiKey = "sk-ant-api03-YOUR_API_KEY",
            EnableLogging = true
        };

        var client = new ClaudeApiClientWithLogging(config, logger);

        // 3. ส่งข้อความและบันทึก Log อัตโนมัติ
        try
        {
            var response = await client.SendMessageWithLoggingAsync(
                message: "ผู้ป่วยมีอาการไอมา 3 วัน ควรให้ยาอะไร",
                systemPrompt: "คุณเป็นผู้ช่วยแพทย์"
            );

            Console.WriteLine($"Claude: {response.Text}");
            Console.WriteLine($"Cost: ${response.Cost:F6}");
            Console.WriteLine($"Session ID: {logger.SessionId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // 4. ดูประวัติการสนทนา
        var history = await logger.GetChatHistoryAsync(limit: 10);
        foreach (DataRow row in history.Rows)
        {
            Console.WriteLine($"[{row["created_at"]}] User: {row["user_message"]}");
            Console.WriteLine($"                        AI: {row["assistant_message"]}\n");
        }

        // 5. ปิด Session
        await logger.EndSessionAsync();

        // 6. ดูรายงานรายวัน
        var report = await ChatLogReports.GetDailyUsageReportAsync(
            connString, 
            DateTime.Today
        );
        
        Console.WriteLine("\n=== รายงานการใช้งานวันนี้ ===");
        foreach (DataRow row in report.Rows)
        {
            Console.WriteLine($"{row["user_name"]}: {row["total_messages"]} messages, ${row["total_cost"]:F4}");
        }
    }
}

*/