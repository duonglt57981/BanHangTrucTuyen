using AssigmentC4_TrinhHuuThanh.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace BanMayAnh.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly AssigmentThanhC4Context _context;

        public ChatController(IHttpClientFactory httpClientFactory,
                              AssigmentThanhC4Context context,
                              IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
            _config = config;
        }
        [HttpPost]
        public async Task<JsonResult> GetResponse([FromBody] ChatRequest request)
        {
            var apiKey = _config["Gemini:ApiKey"];

            var products = _context.SanPhams
            .Take(6)
            .Select(p => new
            {
                p.TenSanPham,
                p.Gia,
                p.MoTa
            })
            .ToList();

            string productList = "";

            int i = 1;

            foreach (var p in products)
            {
                productList += $"{i}. {p.TenSanPham} - {p.Gia} VND ({p.MoTa})\n";
                i++;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(request.Message))
                    return Json(new { success = false, reply = "Bạn chưa nhập tin nhắn!" });

                // Tạo context cho AI về cửa hàng máy ảnh
                var systemPrompt = $@"
                Bạn là trợ lý ảo của CameraShop - cửa hàng máy ảnh tại Việt Nam.

                Danh sách sản phẩm hiện có:
                {productList}

                Chính sách cửa hàng:
                - Bảo hành 12-24 tháng
                - Miễn phí vận chuyển đơn từ 5 triệu
                - Đổi trả trong 7 ngày
                - Hỗ trợ trả góp 0%

                Nhiệm vụ:
                - Tư vấn máy ảnh phù hợp cho khách
                - Giới thiệu sản phẩm từ danh sách trên
                - Trả lời ngắn gọn, thân thiện.
                "; 

                var fullMessage = systemPrompt + "\n\nKhách hàng hỏi: " + request.Message;

                string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = fullMessage }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        maxOutputTokens = 500
                    }
                };

                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(apiUrl, requestBody);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Status: " + response.StatusCode);
                Console.WriteLine("Gemini response: " + jsonResponse); 

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API Error: {jsonResponse}");
                    return Json(new { success = false, reply = "Xin lỗi, tôi đang gặp sự cố. Vui lòng thử lại sau!" });
                }

                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                string botReply = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return Json(new { success = true, reply = botReply });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, reply = "Xin lỗi, có lỗi xảy ra. Vui lòng thử lại!" });
            }
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
    }
}