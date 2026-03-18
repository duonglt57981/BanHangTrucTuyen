// Controllers/CartController.cs
using Assigmemt_Thanh_C4.Models.DataModels;
using AssigmentC4_TrinhHuuThanh.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BanMayAnh.Controllers
{
    public class CartController : Controller
    {
        private readonly AssigmentThanhC4Context _context;
        private const string CART_KEY = "SHOPPING_CART";

        public CartController(AssigmentThanhC4Context context)
        {
            _context = context;
        }

        // Xem giỏ hàng
        public async Task<IActionResult> Index()
        {
            var cart = GetCart();
            var cartItems = new List<CartItemViewModel>();

            foreach (var item in cart)
            {
                var sanPham = await _context.SanPhams
                    .Include(s => s.DanhMuc)
                    .Include(s => s.ChiTietSanPham)   // ← thêm include
                    .FirstOrDefaultAsync(s => s.SanPhamID == item.SanPhamID);

                if (sanPham != null)
                {
                    cartItems.Add(new CartItemViewModel
                    {
                        SanPhamID = sanPham.SanPhamID,
                        TenSanPham = sanPham.TenSanPham,
                        Gia = sanPham.Gia,
                        SoLuong = item.SoLuong,
                        TonKho = sanPham.ChiTietSanPham?.SoLuong ?? sanPham.TonKho,
                        DanhMuc = sanPham.DanhMuc?.TenDanhMuc,
                        ThanhTien = sanPham.Gia * item.SoLuong,
                        HinhAnh = sanPham.HinhAnh,                          // ← thêm
                        BienThe = item.BienThe,                             // ← thêm
                        DoPhanGiai = sanPham.ChiTietSanPham?.DoPhanGiai,    // ← thêm
                        CamBien = sanPham.ChiTietSanPham?.CamBien,          // ← thêm
                        Video = sanPham.ChiTietSanPham?.Video               // ← thêm
                    });
                }
            }

            return View(cartItems);
        }

        // Thêm vào giỏ hàng
        [HttpPost]
        public async Task<IActionResult> AddToCart(int sanPhamId, int soLuong = 1, string bienThe = "")  // ← thêm bienThe
        {
            try
            {
                var sanPham = await _context.SanPhams
                    .Include(s => s.ChiTietSanPham)   // ← thêm include
                    .FirstOrDefaultAsync(s => s.SanPhamID == sanPhamId);

                if (sanPham == null)
                    return Json(new { success = false, message = "Sản phẩm không tồn tại!" });

                // Dùng SoLuong từ ChiTietSanPham nếu có, không thì dùng TonKho
                int maxStock = sanPham.ChiTietSanPham?.SoLuong ?? sanPham.TonKho;

                if (maxStock < soLuong)
                    return Json(new { success = false, message = "Không đủ hàng trong kho!" });

                var cart = GetCart();
                var existItem = cart.FirstOrDefault(x => x.SanPhamID == sanPhamId);

                if (existItem != null)
                {
                    if (maxStock < existItem.SoLuong + soLuong)
                        return Json(new { success = false, message = "Không đủ hàng trong kho!" });

                    existItem.SoLuong += soLuong;
                    existItem.BienThe = bienThe;   // ← cập nhật biến thể nếu đổi
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        SanPhamID = sanPhamId,
                        SoLuong = soLuong,
                        BienThe = bienThe   // ← thêm
                    });
                }

                SaveCart(cart);
                return Json(new
                {
                    success = true,
                    message = "Đã thêm vào giỏ hàng!",
                    cartCount = cart.Sum(x => x.SoLuong)
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Cập nhật số lượng
        [HttpPost]
        public IActionResult UpdateQuantity(int sanPhamId, int soLuong)
        {
            if (soLuong <= 0)
                return Json(new { success = false, message = "Số lượng không hợp lệ!" });

            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.SanPhamID == sanPhamId);

            if (item != null)
            {
                var sanPham = _context.SanPhams
                    .Include(s => s.ChiTietSanPham)   // ← thêm include
                    .FirstOrDefault(s => s.SanPhamID == sanPhamId);

                int maxStock = sanPham?.ChiTietSanPham?.SoLuong ?? sanPham?.TonKho ?? 0;

                if (sanPham != null && maxStock >= soLuong)
                {
                    item.SoLuong = soLuong;
                    SaveCart(cart);
                    return Json(new { success = true, cartCount = cart.Sum(x => x.SoLuong) });
                }
                else
                {
                    return Json(new { success = false, message = "Không đủ hàng trong kho!" });
                }
            }

            return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });
        }

        // Xóa khỏi giỏ hàng
        [HttpPost]
        public IActionResult RemoveFromCart(int sanPhamId)
        {
            var cart = GetCart();
            cart.RemoveAll(x => x.SanPhamID == sanPhamId);
            SaveCart(cart);

            return Json(new
            {
                success = true,
                message = "Đã xóa khỏi giỏ hàng!",
                cartCount = cart.Sum(x => x.SoLuong)
            });
        }

        // Xóa toàn bộ giỏ hàng
        [HttpPost]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CART_KEY);
            return Json(new { success = true, message = "Đã xóa toàn bộ giỏ hàng!" });
        }

        // Lấy số lượng sản phẩm trong giỏ
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            return Json(new { count = cart.Sum(x => x.SoLuong) });
        }

        // Helper methods
        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CART_KEY);
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString(CART_KEY, JsonSerializer.Serialize(cart));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string diaChiGiaoHang, string soDienThoai, string ghiChu)
        {
            var userId = HttpContext.Session.GetInt32("NguoiDungID");
            if (userId == null)
                return Json(new { success = false, message = "Vui lòng đăng nhập để thanh toán!" });

            var cart = GetCart();
            if (cart == null || !cart.Any())
                return Json(new { success = false, message = "Giỏ hàng của bạn đang trống!" });

            try
            {
                decimal tongTien = 0;
                var danhSachChiTiet = new List<ChiTietDonHang>();

                foreach (var item in cart)
                {
                    var sp = await _context.SanPhams
                        .Include(s => s.ChiTietSanPham)
                        .FirstOrDefaultAsync(s => s.SanPhamID == item.SanPhamID);

                    int maxStock = sp?.ChiTietSanPham?.SoLuong ?? sp?.TonKho ?? 0;

                    if (sp == null || maxStock < item.SoLuong)
                        return Json(new { success = false, message = $"Sản phẩm {sp?.TenSanPham} không đủ hàng!" });

                    tongTien += sp.Gia * item.SoLuong;

                    danhSachChiTiet.Add(new ChiTietDonHang
                    {
                        SanPhamID = item.SanPhamID,
                        SoLuong = item.SoLuong,
                        Gia = sp.Gia
                    });

                    // Trừ tồn kho
                    sp.TonKho -= item.SoLuong;
                    if (sp.ChiTietSanPham != null)
                        sp.ChiTietSanPham.SoLuong -= item.SoLuong;  // ← trừ cả ChiTietSanPham.SoLuong
                }

                var donHang = new DonHang
                {
                    NguoiDungID = userId.Value,
                    NgayDat = DateTime.Now,
                    TongTien = tongTien,
                    DiaChiGiaoHang = diaChiGiaoHang,
                    SoDienThoai = soDienThoai,
                    GhiChu = ghiChu,
                    TrangThai = "Chờ xác nhận",
                    DaThanhToan = false
                };

                _context.DonHangs.Add(donHang);
                await _context.SaveChangesAsync();

                foreach (var ct in danhSachChiTiet)
                {
                    ct.DonHangID = donHang.DonHangID;
                    _context.ChiTietDonHangs.Add(ct);
                }

                await _context.SaveChangesAsync();
                HttpContext.Session.Remove(CART_KEY);

                return Json(new { success = true, message = "Đặt hàng thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi xử lý đơn hàng: " + ex.Message });
            }
        }
    }

    // Models cho giỏ hàng
    public class CartItem
    {
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public string? BienThe { get; set; }   // ← thêm: màu/phiên bản đã chọn
    }

    public class CartItemViewModel
    {
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public int TonKho { get; set; }
        public string DanhMuc { get; set; }
        public decimal ThanhTien { get; set; }
        public byte[]? HinhAnh { get; set; }       // ← thêm
        public string? BienThe { get; set; }        // ← thêm
        public string? DoPhanGiai { get; set; }     // ← thêm
        public string? CamBien { get; set; }        // ← thêm
        public string? Video { get; set; }          // ← thêm
    }
}