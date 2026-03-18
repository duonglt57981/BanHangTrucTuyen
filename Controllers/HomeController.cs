// Controllers/HomeController.cs
using Assigmemt_Thanh_C4.Models;
using AssigmentC4_TrinhHuuThanh.Data;
using AssigmentC4_TrinhHuuThanh.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assigmemt_Thanh_C4.Controllers
{
    public class HomeController : Controller
    {
        private readonly AssigmentThanhC4Context _context;

        public HomeController(AssigmentThanhC4Context context)
        {
            _context = context;
        }

        // Trang chủ
        public async Task<IActionResult> Index(int? danhMucId, string searchString, string sortOrder)
        {
            ViewBag.DanhMucs = await _context.DanhMucs.ToListAsync();
            ViewBag.CurrentDanhMuc = danhMucId;
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;

            var sanPhams = _context.SanPhams.Include(s => s.DanhMuc).AsQueryable();

            // Lọc theo danh mục
            if (danhMucId.HasValue)
            {
                sanPhams = sanPhams.Where(s => s.DanhMucID == danhMucId);
            }

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s => s.TenSanPham.Contains(searchString)
                    || s.MoTa.Contains(searchString));
            }

            // Sắp xếp
            sanPhams = sortOrder switch
            {
                "price_asc" => sanPhams.OrderBy(s => s.Gia),
                "price_desc" => sanPhams.OrderByDescending(s => s.Gia),
                "name_asc" => sanPhams.OrderBy(s => s.TenSanPham),
                "name_desc" => sanPhams.OrderByDescending(s => s.TenSanPham),
                _ => sanPhams.OrderByDescending(s => s.NgayThem)
            };

            return View(await sanPhams.ToListAsync());
        }

        // Chi tiết sản phẩm
        public async Task<IActionResult> Details(int id)
        {
            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.ThuongHieu)
                .Include(s => s.ChiTietSanPham)
                    .ThenInclude(ct => ct.MauSacSanPhams) 
                .FirstOrDefaultAsync(s => s.SanPhamID == id);

            if (sanPham == null) return NotFound();

            // Danh sách màu lấy từ ChiTietSanPham (không query thêm DB)
            ViewBag.DanhSachMau = sanPham.ChiTietSanPham?.MauSacSanPhams?.ToList()
                                  ?? new List<MauSacSanPham>();

            // Sản phẩm liên quan (giữ nguyên như cũ)
            ViewBag.SanPhamLienQuan = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Where(s => s.DanhMucID == sanPham.DanhMucID && s.SanPhamID != id)
                .Take(4)
                .ToListAsync();

            return View(sanPham);
        }
        // Lich su don hang
        public async Task<IActionResult> LichSuDonHang()
        {
            // Kiểm tra đăng nhập
            var userId = HttpContext.Session.GetInt32("NguoiDungID");
            if (userId == null)
                return RedirectToAction("DangNhap", "Account");

            var donHangs = await _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.SanPham)
                .Where(d => d.NguoiDungID == userId)
                .OrderByDescending(d => d.NgayDat)
                .ToListAsync();

            return View(donHangs);
        }

        // Giới thiệu
        public IActionResult About()
        {
            return View();
        }

        // Liên hệ
        public IActionResult Contact()
        {
            return View();
        }
    }
}