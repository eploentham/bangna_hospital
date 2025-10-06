using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace bangna_hospital.services
{
    internal class WebView2MessageFix
    {
        // ========================================
     // ❌ ปัญหา: EscapeJS ไม่ถูกต้อง
     // ========================================

        // ฟังก์ชัน EscapeJS แบบเก่า (อาจมีปัญหา)
        private string EscapeJS_OLD(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

            return text
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "");
        }

        // ========================================
        // ✅ ฟังก์ชัน EscapeJS ที่ถูกต้อง
        // ========================================

        private string EscapeJS(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

            return text
                .Replace("\\", "\\\\")   // \ -> \\
                .Replace("'", "\\'")     // ' -> \'
                .Replace("\"", "\\\"")   // " -> \"
                .Replace("\n", "\\n")    // newline -> \n
                .Replace("\r", "")       // ลบ \r
                .Replace("\t", "\\t")    // tab -> \t
                .Replace("`", "\\`")     // ` -> \`
                .Replace("<", "\\x3C")   // < -> \x3C (ป้องกัน XSS)
                .Replace(">", "\\x3E");  // > -> \x3E (ป้องกัน XSS)
        }

        // ========================================
        // ✅ วิธีที่ 1: ใช้ JSON.stringify (แนะนำ!)
        // ========================================

        private async Task SendMessageToWebView_Method1(WebView2 wvClaude, string message, string time)
        {
            try
            {
                // ใช้ JSON เพื่อ escape อัตโนมัติ
                var jsonMessage = JsonSerializer.Serialize(message);
                var jsonTime = JsonSerializer.Serialize(time);

                var script = $"addAssistantMessage({jsonMessage}, {jsonTime});";

                Console.WriteLine($"[DEBUG] Executing script: {script.Substring(0, Math.Min(100, script.Length))}...");

                await wvClaude.ExecuteScriptAsync(script);

                Console.WriteLine("[DEBUG] Script executed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ExecuteScriptAsync failed: {ex.Message}");
            }
        }

        // ========================================
        // ✅ วิธีที่ 2: ใช้ Template Literals (`)
        // ========================================

        private async Task SendMessageToWebView_Method2(WebView2 wvClaude, string message, string time)
        {
            try
            {
                // Escape สำหรับ template literal
                var escapedMessage = message
                    .Replace("\\", "\\\\")
                    .Replace("`", "\\`")
                    .Replace("$", "\\$");

                var script = $"addAssistantMessage(`{escapedMessage}`, `{time}`);";

                await wvClaude.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
            }
        }

        // ========================================
        // ✅ วิธีที่ 3: ใช้ PostWebMessageAsJson
        // ========================================

        private async Task SendMessageToWebView_Method3(WebView2 wvClaude, string message, string time)
        {
            try
            {
                var data = new
                {
                    type = "addMessage",
                    role = "assistant",
                    content = message,
                    time = time
                };

                var json = JsonSerializer.Serialize(data);
                wvClaude.CoreWebView2.PostWebMessageAsJson(json);
                //await wvClaude.CoreWebView2.PostWebMessageAsJsonAsync(json);

                // ใน HTML ต้องมี event listener:
                // window.chrome.webview.addEventListener('message', function(event) {
                //     const data = event.data;
                //     if (data.type === 'addMessage') {
                //         addAssistantMessage(data.content, data.time);
                //     }
                // });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
            }
        }

        // ========================================
        // ✅ วิธีที่ 4: ตรวจสอบก่อนส่ง
        // ========================================

        private async Task SendMessageToWebView_Method4(WebView2 wvClaude, string message, string time)
        {
            try
            {
                // 1. ตรวจสอบว่า WebView2 พร้อมหรือยัง
                if (wvClaude.CoreWebView2 == null)
                {
                    Console.WriteLine("[ERROR] CoreWebView2 is null - not initialized");
                    return;
                }
                // 2. ตรวจสอบว่า function มีอยู่หรือไม่
                var checkScript = "typeof addAssistantMessage === 'function'";
                var result = await wvClaude.ExecuteScriptAsync(checkScript);
                Console.WriteLine($"[DEBUG] Function exists: {result}");
                if (result != "true")
                {
                    Console.WriteLine("[ERROR] addAssistantMessage function not found!");
                    return;
                }
                // 3. ส่งข้อความ
                var jsonMessage = JsonSerializer.Serialize(message);
                var jsonTime = JsonSerializer.Serialize(time);
                var script = $"addAssistantMessage({jsonMessage}, {jsonTime});";
                Console.WriteLine($"[DEBUG] Sending: {script.Substring(0, Math.Min(200, script.Length))}...");
                var executeResult = await wvClaude.ExecuteScriptAsync(script);
                Console.WriteLine($"[DEBUG] Result: {executeResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                Console.WriteLine($"[ERROR] Stack: {ex.StackTrace}");
            }
        }
        // ========================================
        // ✅ แก้ไขในโค้ดจริง - แบบสมบูรณ์
        // ========================================

        public async Task SendClaudeResponseToWebView(WebView2 wvClaude, string responseText)
        {
            try
            {
                // รอให้ WebView2 พร้อม
                if (wvClaude.CoreWebView2 == null)
                {
                    Console.WriteLine("[WARN] WebView2 not ready, waiting...");
                    await Task.Delay(100);

                    if (wvClaude.CoreWebView2 == null)
                    {
                        Console.WriteLine("[ERROR] WebView2 still not ready!");
                        return;
                    }
                }

                // เตรียมข้อมูล
                var time = DateTime.Now.ToString("HH:mm");

                // Debug: แสดงข้อความก่อนส่ง
                Console.WriteLine($"[DEBUG] Sending message to WebView2:");
                Console.WriteLine($"[DEBUG] Text length: {responseText.Length}");
                Console.WriteLine($"[DEBUG] Preview: {responseText.Substring(0, Math.Min(100, responseText.Length))}...");
                Console.WriteLine($"[DEBUG] Time: {time}");

                // วิธีที่ 1: ใช้ JSON (แนะนำ)
                var jsonMessage = JsonSerializer.Serialize(responseText);
                var jsonTime = JsonSerializer.Serialize(time);
                var script = $"addAssistantMessage({jsonMessage}, {jsonTime});";

                Console.WriteLine($"[DEBUG] Script: {script.Substring(0, Math.Min(150, script.Length))}...");

                // ส่งไปยัง WebView2
                var result = await wvClaude.ExecuteScriptAsync(script);

                Console.WriteLine($"[DEBUG] ExecuteScript result: {result}");

                // ตรวจสอบผลลัพธ์
                if (string.IsNullOrEmpty(result))
                {
                    Console.WriteLine("[WARN] ExecuteScript returned empty result");
                }
                //else if (result.Contains("error", StringComparison.OrdinalIgnoreCase))
                //{
                //    Console.WriteLine($"[ERROR] JavaScript error: {result}");
                //}
                else
                {
                    Console.WriteLine("[SUCCESS] Message sent to WebView2");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to send message to WebView2:");
                Console.WriteLine($"[ERROR] Type: {ex.GetType().Name}");
                Console.WriteLine($"[ERROR] Message: {ex.Message}");
                Console.WriteLine($"[ERROR] Stack: {ex.StackTrace}");
            }
        }

        // ========================================
        // ✅ ตรวจสอบ JavaScript Function
        // ========================================

        public async Task<bool> CheckJavaScriptFunction(WebView2 wvClaude, string functionName)
        {
            try
            {
                if (wvClaude.CoreWebView2 == null)
                {
                    Console.WriteLine("[ERROR] CoreWebView2 is null");
                    return false;
                }

                var script = $"typeof {functionName}";
                var result = await wvClaude.ExecuteScriptAsync(script);

                Console.WriteLine($"[DEBUG] typeof {functionName} = {result}");

                return result.Contains("function");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Check function failed: {ex.Message}");
                return false;
            }
        }

        // ========================================
        // ✅ Debug: ดูว่า HTML มีอะไรบ้าง
        // ========================================

        public async Task DebugWebView2Content(WebView2 wvClaude)
        {
            try
            {
                Console.WriteLine("\n=== WebView2 Debug Info ===");
                // 1. ตรวจสอบ URL
                var url = wvClaude.Source?.ToString();
                Console.WriteLine($"Current URL: {url}");
                // 2. ตรวจสอบ Title
                var title = await wvClaude.ExecuteScriptAsync("document.title");
                Console.WriteLine($"Page Title: {title}");
                // 3. ตรวจสอบ Functions
                var functions = await wvClaude.ExecuteScriptAsync(@"Object.keys(window).filter(key => typeof window[key] === 'function').join(',')");
                Console.WriteLine($"Available Functions: {functions}");
                // 4. ตรวจสอบว่ามี addAssistantMessage หรือไม่
                var hasFunction = await wvClaude.ExecuteScriptAsync("typeof addAssistantMessage");
                Console.WriteLine($"addAssistantMessage type: {hasFunction}");
                // 5. ทดสอบเรียกใช้
                if (hasFunction.Contains("function"))
                {
                    Console.WriteLine("Attempting to call function...");
                    var testResult = await wvClaude.ExecuteScriptAsync("addAssistantMessage('Test message', '12:00'); 'called'");
                    Console.WriteLine($"Test call result: {testResult}");
                }
                Console.WriteLine("===========================\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Debug failed: {ex.Message}");
            }
        }

        // ========================================
        // ✅ HTML ที่ถูกต้องสำหรับ WebView2
        // ========================================

        public string GetSampleHTML()
        {
            return @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Claude Chat</title>
    <style>
        body { 
            font-family: Arial, sans-serif; 
            margin: 20px;
            background: #f5f5f5;
        }
        #messages { 
            max-width: 800px;
            margin: 0 auto;
        }
        .message { 
            margin: 10px 0; 
            padding: 15px;
            border-radius: 8px;
            background: white;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .assistant { 
            background: #e3f2fd; 
        }
        .time {
            font-size: 0.8em;
            color: #666;
            margin-top: 5px;
        }
    </style>
</head>
<body>
    <div id='messages'></div>

    <script>
        // ฟังก์ชันสำหรับแสดงข้อความ
        function addAssistantMessage(content, time) {
            console.log('addAssistantMessage called:', content, time);
            
            try {
                const messagesDiv = document.getElementById('messages');
                
                const messageDiv = document.createElement('div');
                messageDiv.className = 'message assistant';
                
                const contentDiv = document.createElement('div');
                contentDiv.textContent = content;
                
                const timeDiv = document.createElement('div');
                timeDiv.className = 'time';
                timeDiv.textContent = time;
                
                messageDiv.appendChild(contentDiv);
                messageDiv.appendChild(timeDiv);
                messagesDiv.appendChild(messageDiv);
                
                // Scroll ลงล่างสุด
                messageDiv.scrollIntoView({ behavior: 'smooth' });
                
                console.log('Message added successfully');
                return true;
            } catch (error) {
                console.error('Error in addAssistantMessage:', error);
                return false;
            }
        }

        // รับข้อความจาก C# ผ่าน PostWebMessage
        if (window.chrome && window.chrome.webview) {
            window.chrome.webview.addEventListener('message', function(event) {
                console.log('Received message from C#:', event.data);
                
                const data = event.data;
                if (data.type === 'addMessage') {
                    addAssistantMessage(data.content, data.time);
                }
            });
        }

        console.log('Script loaded. addAssistantMessage is available:', typeof addAssistantMessage);
    </script>
</body>
</html>
";
        }

        // ========================================
        // ✅ ตัวอย่างการใช้งานแบบสมบูรณ์
        // ========================================

        public async Task CompleteExample(WebView2 wvClaude)
        {
            // 1. รอให้ WebView2 พร้อม
            while (wvClaude.CoreWebView2 == null)
            {
                await Task.Delay(100);
            }

            Console.WriteLine("[INFO] WebView2 is ready");

            // 2. Debug ดูข้อมูล
            await DebugWebView2Content(wvClaude);

            // 3. ตรวจสอบ function
            var hasFunction = await CheckJavaScriptFunction(wvClaude, "addAssistantMessage");

            if (!hasFunction)
            {
                Console.WriteLine("[ERROR] addAssistantMessage function not found!");
                Console.WriteLine("[ERROR] Please check your HTML file");
                return;
            }

            Console.WriteLine("[INFO] Function found, ready to send messages");

            // 4. ส่งข้อความ
            await SendClaudeResponseToWebView(
                wvClaude,
                "สวัสดีครับ! ยินดีต้อนรับสู่ระบบ Claude AI"
            );
        }
    }

    // ========================================
    // 📋 Checklist สำหรับการแก้ปัญหา
    // ========================================
    /*

    ✅ ตรวจสอบสิ่งเหล่านี้:

    1. CoreWebView2 ถูก initialize แล้วหรือยัง?
       if (wvClaude.CoreWebView2 == null) { ... }

    2. HTML มีฟังก์ชัน addAssistantMessage หรือไม่?
       ดูใน HTML <script> tag

    3. ใช้ JSON.Serialize เพื่อ escape string
       var json = JsonSerializer.Serialize(text);

    4. เช็ค Console.WriteLine ว่ามี error อะไรไหม

    5. ลอง debug ด้วย:
       await DebugWebView2Content(wvClaude);

    6. ตรวจสอบว่า WebView2 โหลด HTML สำเร็จหรือยัง
       wvClaude.NavigationCompleted += ...

    7. ดู JavaScript Console ใน WebView2:
       F12 Developer Tools (ถ้าเปิดได้)

    ========================================
    */


}
