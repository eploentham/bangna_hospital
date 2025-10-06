using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.services
{
    // ========================================
    // แก้ปัญหา 401 Unauthorized
    // ========================================

    public class Fix401ApiKey
    {
        // ========================================
        // สาเหตุที่เป็นไปได้
        // ========================================
        /*
         * ❌ สาเหตุของ 401:
         * 
         * 1. API Key ผิด
         * 2. API Key มีวงเล็บ [] หรือเครื่องหมายพิเศษ
         * 3. API Key หมดอายุ
         * 4. API Key ถูกลบแล้ว
         * 5. API Key ไม่ได้ activate
         * 6. มีช่องว่าง (space) ใน API Key
         * 7. คัดลอก API Key ไม่ครบ
         */

        // ========================================
        // ✅ วิธีตรวจสอบ API Key
        // ========================================
        public static void CheckApiKey(string apiKey)
        {
            Console.WriteLine("=== API KEY VALIDATION ===\n");

            // 1. ความยาว
            Console.WriteLine($"1. Length: {apiKey.Length} chars");
            Console.WriteLine($"   Expected: ~110-120 chars");
            Console.WriteLine($"   Status: {(apiKey.Length >= 100 && apiKey.Length <= 150 ? "✅ OK" : "❌ TOO SHORT/LONG")}\n");

            // 2. รูปแบบ
            Console.WriteLine($"2. Format check:");
            Console.WriteLine($"   Starts with 'sk-ant-': {(apiKey.StartsWith("sk-ant-") ? "✅ OK" : "❌ WRONG")}");
            Console.WriteLine($"   Contains 'api03-': {(apiKey.Contains("api03-") ? "✅ OK" : "❌ MISSING")}\n");

            // 3. อักขระพิเศษ
            Console.WriteLine($"3. Special characters:");
            Console.WriteLine($"   Has [ or ]: {(apiKey.Contains("[") || apiKey.Contains("]") ? "❌ REMOVE BRACKETS" : "✅ OK")}");
            Console.WriteLine($"   Has spaces: {(apiKey.Contains(" ") ? "❌ REMOVE SPACES" : "✅ OK")}");
            Console.WriteLine($"   Has quotes: {(apiKey.Contains("\"") || apiKey.Contains("'") ? "❌ REMOVE QUOTES" : "✅ OK")}\n");

            // 4. แสดง preview
            if (apiKey.Length >= 20)
            {
                Console.WriteLine($"4. Preview:");
                Console.WriteLine($"   First 30 chars: {apiKey.Substring(0, Math.Min(30, apiKey.Length))}");
                Console.WriteLine($"   Last 10 chars: ...{apiKey.Substring(Math.Max(0, apiKey.Length - 10))}\n");
            }

            // 5. คำแนะนำ
            Console.WriteLine("5. Recommendations:");
            if (apiKey.Contains("[") || apiKey.Contains("]"))
            {
                Console.WriteLine("   ⚠️  Remove [ and ] brackets");
                Console.WriteLine($"   Corrected: {apiKey.Trim('[', ']')}");
            }
            if (apiKey.Contains(" "))
            {
                Console.WriteLine("   ⚠️  Remove spaces");
            }
            if (apiKey.Length < 100)
            {
                Console.WriteLine("   ⚠️  API Key seems too short - copy the full key");
            }

            Console.WriteLine("\n==========================\n");
        }

        // ========================================
        // ✅ ทดสอบ API Key
        // ========================================
        public static async Task TestApiKey(string apiKey)
        {
            Console.WriteLine("=== TESTING API KEY ===\n");

            // Clean API Key
            apiKey = apiKey.Trim().Trim('[', ']').Trim('"', '\'').Trim();

            Console.WriteLine($"Cleaned API Key: {apiKey.Substring(0, 30)}...{apiKey.Substring(apiKey.Length - 10)}\n");

            using (var client = new System.Net.Http.HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

                var requestBody = @"{
                ""model"": ""claude-sonnet-4-5-20250929"",
                ""max_tokens"": 50,
                ""messages"": [{
                    ""role"": ""user"",
                    ""content"": ""Hi""
                }]
            }";

                var content = new System.Net.Http.StringContent(
                    requestBody,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                try
                {
                    Console.WriteLine("📤 Sending test request...");
                    var response = await client.PostAsync("https://api.anthropic.com/v1/messages", content);
                    var responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"📊 Status: {(int)response.StatusCode} {response.StatusCode}\n");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("✅ SUCCESS! API Key is valid!");
                        Console.WriteLine($"Response preview: {responseBody.Substring(0, Math.Min(200, responseBody.Length))}...\n");
                    }
                    else
                    {
                        Console.WriteLine($"❌ FAILED! Status: {response.StatusCode}");
                        Console.WriteLine($"Response: {responseBody}\n");

                        if ((int)response.StatusCode == 401)
                        {
                            Console.WriteLine("🔍 Error 401 means:");
                            Console.WriteLine("   1. API Key is incorrect");
                            Console.WriteLine("   2. API Key was deleted");
                            Console.WriteLine("   3. API Key is not activated");
                            Console.WriteLine("\n💡 Solution:");
                            Console.WriteLine("   1. Go to https://console.anthropic.com");
                            Console.WriteLine("   2. Click 'API Keys' in sidebar");
                            Console.WriteLine("   3. Create a NEW API Key");
                            Console.WriteLine("   4. Copy the FULL key (all characters)");
                            Console.WriteLine("   5. Use it WITHOUT brackets [ ]\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Exception: {ex.Message}\n");
                }
            }

            Console.WriteLine("=======================\n");
        }

        // ========================================
        // ✅ สร้าง API Key ใหม่ - คำแนะนำ
        // ========================================
        public static void ShowHowToCreateApiKey()
        {
            Console.WriteLine(@"
========================================
🔑 วิธีสร้าง API Key ใหม่
========================================

1. เปิดเบราว์เซอร์ไปที่:
   https://console.anthropic.com

2. Login เข้าบัญชีของคุณ

3. คลิก 'API Keys' ในเมนูด้านซ้าย

4. คลิกปุ่ม 'Create Key' หรือ '+ New Key'

5. ตั้งชื่อให้กับ Key (เช่น 'Hospital Project')

6. คลิก 'Create Key'

7. คัดลอก API Key ทั้งหมด
   ⚠️ จะแสดงเพียงครั้งเดียว!
   ⚠️ ต้องคัดลอกทั้งหมด ตั้งแต่ sk-ant- ถึงตัวสุดท้าย

8. เก็บไว้ในที่ปลอดภัย

========================================
✅ รูปแบบที่ถูกต้อง
========================================

sk-ant-api03-XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX-XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

ความยาว: ~110-120 ตัวอักษร
ไม่มี: วงเล็บ [] เครื่องหมายคำพูด "" ช่องว่าง

========================================
❌ รูปแบบที่ผิด
========================================

[sk-ant-api03-XXX]           ❌ มีวงเล็บ
""sk-ant-api03-XXX""           ❌ มีเครื่องหมายคำพูด
sk-ant-api03-XXX...          ❌ ไม่ครบ
sk-ant- api03-XXX            ❌ มีช่องว่าง

========================================
");
        }

        // ========================================
        // ✅ ตัวอย่างการใช้ใน ClaudeApiClient
        // ========================================
        public static void ShowCorrectUsage()
        {
            Console.WriteLine(@"
========================================
📝 วิธีใช้ API Key ที่ถูกต้อง
========================================

// ❌ ผิด - มีวงเล็บ
var config = new ClaudeConfig
{
    ApiKey = ""[sk-ant-api03-XXXX]""
};

// ❌ ผิด - มีเครื่องหมายคำพูดซ้อน
var apiKey = ""'sk-ant-api03-XXXX'"";

// ✅ ถูกต้อง
var config = new ClaudeConfig
{
    ApiKey = ""sk-ant-api03-XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX-XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX""
};

// ✅ ถูกต้อง - ใช้ Environment Variable (แนะนำ)
Environment.SetEnvironmentVariable(""CLAUDE_API_KEY"", ""sk-ant-api03-XXX..."");
var config = new ClaudeConfig
{
    ApiKey = Environment.GetEnvironmentVariable(""CLAUDE_API_KEY"")
};

// ✅ ถูกต้อง - อ่านจากไฟล์ config
var apiKey = System.IO.File.ReadAllText(""apikey.txt"").Trim();
var config = new ClaudeConfig
{
    ApiKey = apiKey
};

========================================
");
        }

        // ========================================
        // Main - ทดสอบ
        // ========================================
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("🔑 API Key Validator & Tester\n");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();

            // ⚠️ ใส่ API Key ของคุณที่นี่เพื่อทดสอบ
            var apiKey = "sk-ant-api03-YOUR_API_KEY_HERE";

            if (apiKey == "sk-ant-api03-YOUR_API_KEY_HERE")
            {
                Console.WriteLine("⚠️  กรุณาใส่ API Key ของคุณในตัวแปร 'apiKey'\n");

                ShowHowToCreateApiKey();
                ShowCorrectUsage();

                Console.WriteLine("\nกด Enter เพื่อออก...");
                Console.ReadLine();
                return;
            }

            // Clean API Key
            Console.WriteLine("🧹 Cleaning API Key...");
            var originalKey = apiKey;
            apiKey = apiKey.Trim().Trim('[', ']').Trim('"', '\'').Trim();

            if (originalKey != apiKey)
            {
                Console.WriteLine("⚠️  API Key was cleaned:");
                Console.WriteLine($"   Before: {originalKey.Substring(0, Math.Min(50, originalKey.Length))}...");
                Console.WriteLine($"   After:  {apiKey.Substring(0, Math.Min(50, apiKey.Length))}...\n");
            }
            else
            {
                Console.WriteLine("✅ API Key is already clean\n");
            }

            // ตรวจสอบ
            CheckApiKey(apiKey);

            // ทดสอบ
            await TestApiKey(apiKey);

            // คำแนะนำ
            Console.WriteLine("💡 If still getting 401:");
            Console.WriteLine("   1. Create a NEW API Key at console.anthropic.com");
            Console.WriteLine("   2. Delete the old key");
            Console.WriteLine("   3. Copy the new key completely");
            Console.WriteLine("   4. Use it without brackets or quotes");

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("\nกด Enter เพื่อออก...");
            Console.ReadLine();
        }
    }

    // ========================================
    // 📋 Quick Fix Checklist
    // ========================================
    /*

    ✅ ทำตามนี้เพื่อแก้ 401:

    1. ไปที่ https://console.anthropic.com

    2. คลิก API Keys

    3. ลบ Key เก่า (ถ้ามี)

    4. สร้าง Key ใหม่ โดยคลิก "Create Key"

    5. ตั้งชื่อ เช่น "BangnaHospital"

    6. คัดลอก Key ทั้งหมด (110-120 ตัวอักษร)

    7. เอาวงเล็บ [] ออกถ้ามี

    8. ใส่ใน code:

       var config = new ClaudeConfig
       {
           ApiKey = "sk-ant-api03-XXX..."  // วาง Key ตรงนี้
       };

    9. Rebuild project

    10. Run ใหม่

    ควรได้ 200 OK แทน 401!

    */
}
