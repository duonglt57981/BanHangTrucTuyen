// Controllers/AccountController.cs
using AssigmentC4_TrinhHuuThanh.Data;
using Assigmemt_Thanh_C4.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BanMayAnh.Controllers
{   
    public class AccountController : Controller
    {
        private readonly AssigmentThanhC4Context _context;

        public AccountController(AssigmentThanhC4Context context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string tenDangNhap, string matKhau, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                TempData["Error"] = "Vui lòng nhập đầy đủ thông tin đăng nhập!";
                return View();
            }


            var nguoiDung = await _context.NguoiDungs
                .Include(n => n.Quyen)
                .FirstOrDefaultAsync(n => n.TenDangNhap == tenDangNhap && n.MatKhau == matKhau);

            if (nguoiDung == null)
            {
                TempData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }

            // Lưu thông tin vào Session
            HttpContext.Session.SetInt32("NguoiDungID", nguoiDung.NguoiDungID);
            HttpContext.Session.SetString("TenDangNhap", nguoiDung.TenDangNhap);
            HttpContext.Session.SetString("HoTen", nguoiDung.HoTen ?? nguoiDung.TenDangNhap);
            HttpContext.Session.SetString("Quyen", nguoiDung.Quyen?.TenQuyen ?? "Khách hàng");

            TempData["Success"] = $"Xin chào {nguoiDung.HoTen ?? nguoiDung.TenDangNhap}!";

            // Redirect theo quyền
            if (nguoiDung.Quyen?.TenQuyen == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            // Redirect về trang trước đó hoặc trang chủ
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(NguoiDung nguoiDung, string xacNhanMatKhau)
        {
            if (string.IsNullOrEmpty(nguoiDung.TenDangNhap) || string.IsNullOrEmpty(nguoiDung.MatKhau))
            {
                TempData["Error"] = "Vui lòng nhập đầy đủ thông tin!";
                return View(nguoiDung);
            }

            if (nguoiDung.MatKhau != xacNhanMatKhau)
            {
                TempData["Error"] = "Mật khẩu xác nhận không khớp!";
                return View(nguoiDung);
            }

            // Kiểm tra tên đăng nhập đã tồn tại
            var existed = await _context.NguoiDungs.AnyAsync(n => n.TenDangNhap == nguoiDung.TenDangNhap);
            if (existed)
            {
                TempData["Error"] = "Tên đăng nhập đã tồn tại!";
                return View(nguoiDung);
            }

            // Mã hóa mật khẩu
            nguoiDung.MatKhau = nguoiDung.MatKhau;
            nguoiDung.NgayTao = new DateTime(2026,1,1);
            nguoiDung.QuyenID = 2; // Khách hàng

            _context.Add(nguoiDung);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction(nameof(Login));
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }

        // Access Denied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}