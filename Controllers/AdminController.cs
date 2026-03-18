// Controllers/AdminController.cs
using Assigmemt_Thanh_C4.Models.DataModels;
using AssigmentC4_TrinhHuuThanh.Data;
using AssigmentC4_TrinhHuuThanh.Filters;
using AssigmentC4_TrinhHuuThanh.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BanMayAnh.Controllers
{
    [AdminAuthorization]
    public class AdminController : Controller
    {
        private readonly AssigmentThanhC4Context _context;

        public AdminController(AssigmentThanhC4Context context)
        {
            _context = context;
        }

        // Dashboard
        public async Task<IActionResult> Index()
        {
            ViewBag.TongSanPham = await _context.SanPhams.CountAsync();
            ViewBag.TongDonHang = await _context.DonHangs.CountAsync();
            ViewBag.TongKhachHang = await _context.NguoiDungs.CountAsync();
            ViewBag.DoanhThu = await _context.ChiTietDonHangs.SumAsync(c => c.Gia * c.SoLuong);

            var donHangMoi = await _context.DonHangs
                .Include(d => d.NguoiDung)
                .OrderByDescending(d => d.NgayDat)
                .Take(5)
                .ToListAsync();

            var sanPhamSapHet = await _context.SanPhams
                .Where(s => s.TonKho < 10)
                .OrderBy(s => s.TonKho)
                .Take(5)
                .ToListAsync();

            ViewBag.DonHangMoi = donHangMoi;
            ViewBag.SanPhamSapHet = sanPhamSapHet;

            return View();
        }

        #region Quản lý Sản phẩm

        // Danh sách sản phẩm
        public async Task<IActionResult> SanPham(string searchString, int? danhMucId)
        {
            ViewBag.DanhMucs = await _context.DanhMucs.ToListAsync();
            ViewBag.SearchString = searchString;
            ViewBag.DanhMucId = danhMucId;

            var sanPhams = _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.ThuongHieu)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                sanPhams = sanPhams.Where(s => s.TenSanPham.Contains(searchString));

            if (danhMucId.HasValue)
                sanPhams = sanPhams.Where(s => s.DanhMucID == danhMucId);

            return View(await sanPhams.OrderByDescending(s => s.NgayThem).ToListAsync());
        }

        // Thêm sản phẩm - GET
        public async Task<IActionResult> ThemSanPham()
        {
            ViewBag.DanhMucs = new SelectList(await _context.DanhMucs.ToListAsync(), "DanhMucID", "TenDanhMuc");
            ViewBag.ThuongHieus = new SelectList(await _context.thuongHieus.ToListAsync(), "ThuongHieuID", "TenThuongHieu");
            return View();
        }

        // Thêm sản phẩm - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemSanPham(SanPham sanPham, IFormFile hinhAnh)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload hình ảnh
                if (hinhAnh != null && hinhAnh.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await hinhAnh.CopyToAsync(memoryStream);
                        sanPham.HinhAnh = memoryStream.ToArray();
                    }
                }

                sanPham.NgayThem = DateTime.Now;
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction(nameof(SanPham));
            }
            ViewBag.DanhMucs = new SelectList(await _context.DanhMucs.ToListAsync(), "DanhMucID", "TenDanhMuc", sanPham.DanhMucID);
            ViewBag.ThuongHieus = new SelectList(await _context.thuongHieus.ToListAsync(), "ThuongHieuID", "TenThuongHieu", sanPham.ThuongHieuID);
            return View(sanPham);
        }

        // Sửa sản phẩm - GET
        public async Task<IActionResult> SuaSanPham(int? id)
        {
            if (id == null) return NotFound();

            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.ThuongHieu)
                .FirstOrDefaultAsync(s => s.SanPhamID == id);

            if (sanPham == null) return NotFound();

            ViewBag.DanhMucs = new SelectList(await _context.DanhMucs.ToListAsync(), "DanhMucID", "TenDanhMuc", sanPham.DanhMucID);
            ViewBag.ThuongHieus = new SelectList(await _context.thuongHieus.ToListAsync(), "ThuongHieuID", "TenThuongHieu", sanPham.ThuongHieuID);
            return View(sanPham);
        }

        // Sửa sản phẩm - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaSanPham(int id, SanPham sanPham, IFormFile hinhAnh)
        {
            if (id != sanPham.SanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy sản phẩm cũ để giữ hình ảnh nếu không upload mới
                    var sanPhamCu = await _context.SanPhams.AsNoTracking().FirstOrDefaultAsync(s => s.SanPhamID == id);

                    // Xử lý upload hình ảnh mới
                    if (hinhAnh != null && hinhAnh.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await hinhAnh.CopyToAsync(memoryStream);
                            sanPham.HinhAnh = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        // Giữ hình ảnh cũ nếu không upload mới
                        sanPham.HinhAnh = sanPhamCu?.HinhAnh;
                    }

                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật sản phẩm thành công!";
                    return RedirectToAction(nameof(SanPham));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SanPhams.Any(e => e.SanPhamID == sanPham.SanPhamID))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            ViewBag.DanhMucs = new SelectList(await _context.DanhMucs.ToListAsync(), "DanhMucID", "TenDanhMuc", sanPham.DanhMucID);
            return View(sanPham);
        }

        // Xóa sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaSanPham(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa sản phẩm thành công!";
            }
            return RedirectToAction(nameof(SanPham));
        }

        #endregion

        #region Quản lý Danh mục

        // Danh sách danh mục
        public async Task<IActionResult> DanhMuc()
        {
            var danhMucs = await _context.DanhMucs
                .Select(d => new
                {
                    DanhMuc = d,
                    SoLuongSanPham = d.SanPhams.Count()
                })
                .ToListAsync();

            return View(danhMucs.Select(d => d.DanhMuc).ToList());
        }

        // Thêm danh mục - GET
        public IActionResult ThemDanhMuc()
        {
            return View();
        }

        // Thêm danh mục - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemDanhMuc(DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMuc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm danh mục thành công!";
                return RedirectToAction(nameof(DanhMuc));
            }
            return View(danhMuc);
        }

        // Sửa danh mục - GET
        public async Task<IActionResult> SuaDanhMuc(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // Sửa danh mục - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaDanhMuc(int id, DanhMuc danhMuc)
        {
            if (id != danhMuc.DanhMucID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMuc);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật danh mục thành công!";
                    return RedirectToAction(nameof(DanhMuc));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DanhMucs.Any(e => e.DanhMucID == danhMuc.DanhMucID))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(danhMuc);
        }

        // Xóa danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaDanhMuc(int id)
        {
            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc != null)
            {
                // Kiểm tra xem có sản phẩm nào đang sử dụng danh mục này không
                var hasSanPham = await _context.SanPhams.AnyAsync(s => s.DanhMucID == id);
                if (hasSanPham)
                {
                    TempData["Error"] = "Không thể xóa danh mục đang có sản phẩm!";
                }
                else
                {
                    _context.DanhMucs.Remove(danhMuc);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa danh mục thành công!";
                }
            }
            return RedirectToAction(nameof(DanhMuc));
        }

        #endregion

        #region Quản lý Chi tiết Sản phẩm

        // Thêm/Sửa chi tiết sản phẩm - GET
        [HttpGet]
        public async Task<IActionResult> ChiTietSanPham(int sanPhamId)
        {
            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.ThuongHieu)
                .Include(s => s.ChiTietSanPham)
                    .ThenInclude(ct => ct.MauSacSanPhams) 
                .FirstOrDefaultAsync(s => s.SanPhamID == sanPhamId);

            if (sanPham == null) return NotFound();

            ViewBag.SanPham = sanPham;

            // Danh sách màu lấy từ ChiTietSanPham
            var dsMau = sanPham.ChiTietSanPham?.MauSacSanPhams?.ToList()
                        ?? new List<MauSacSanPham>();
            ViewBag.DanhSachMau = dsMau;

            var chiTiet = sanPham.ChiTietSanPham ?? new ChiTietSanPham { SanPhamID = sanPhamId };
            return View(chiTiet);
        }

        // POST - lưu thông số kỹ thuật
        [HttpPost]
        public async Task<IActionResult> ChiTietSanPham(ChiTietSanPham model)
        {
            if (ModelState.IsValid)
            {
                // SoLuong = tổng các màu trong ChiTietSanPham này
                if (model.ChiTietSanPhamID != 0)
                {
                    model.SoLuong = await _context.mauSacSanPhams
                        .Where(m => m.ChiTietSanPhamID == model.ChiTietSanPhamID)
                        .SumAsync(m => m.SoLuong);
                }

                if (model.ChiTietSanPhamID == 0)
                    _context.chiTietSanPhams.Add(model);
                else
                    _context.chiTietSanPhams.Update(model);

                await _context.SaveChangesAsync();
                TempData["Success"] = "Lưu thông số kỹ thuật thành công!";
                return RedirectToAction("ChiTietSanPham", new { sanPhamId = model.SanPhamID });
            }

            // Reload nếu lỗi
            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc).Include(s => s.ThuongHieu)
                .FirstOrDefaultAsync(s => s.SanPhamID == model.SanPhamID);
            ViewBag.SanPham = sanPham;
            ViewBag.DanhSachMau = await _context.mauSacSanPhams
                .Where(m => m.ChiTietSanPhamID == model.ChiTietSanPhamID).ToListAsync();
            return View(model);
        }


        // ---- THÊM MÀU ----
        [HttpPost]
        public async Task<IActionResult> ThemMau(int chiTietSanPhamId, int sanPhamId,
    string tenMau, int soLuong, IFormFile? hinhAnhMau)
        {
            if (!string.IsNullOrWhiteSpace(tenMau) && soLuong >= 0)
            {
                var mauCu = await _context.mauSacSanPhams
                    .FirstOrDefaultAsync(m => m.ChiTietSanPhamID == chiTietSanPhamId &&
                                              m.TenMau.ToLower() == tenMau.Trim().ToLower());
                if (mauCu != null)
                {
                    TempData["Error"] = $"Màu '{tenMau}' đã tồn tại!";
                }
                else
                {
                    byte[]? anhBytes = null;
                    if (hinhAnhMau != null && hinhAnhMau.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await hinhAnhMau.CopyToAsync(ms);
                        anhBytes = ms.ToArray();
                    }

                    _context.mauSacSanPhams.Add(new MauSacSanPham
                    {
                        ChiTietSanPhamID = chiTietSanPhamId,
                        TenMau = tenMau.Trim(),
                        SoLuong = soLuong,
                        HinhAnh = anhBytes
                    });
                    await _context.SaveChangesAsync();
                    await CapNhatTongSoLuong(chiTietSanPhamId);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Đã thêm màu '{tenMau}'!";
                }
            }
            return RedirectToAction("ChiTietSanPham", new { sanPhamId });
        }


        [HttpPost]
        public async Task<IActionResult> CapNhatSoLuongMau(int mauSacId, int soLuong,
    int sanPhamId, IFormFile? hinhAnhMau)
        {
            var mau = await _context.mauSacSanPhams.FindAsync(mauSacId);
            if (mau != null)
            {
                mau.SoLuong = soLuong;

                // Chỉ cập nhật ảnh nếu có file mới upload
                if (hinhAnhMau != null && hinhAnhMau.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await hinhAnhMau.CopyToAsync(ms);
                    mau.HinhAnh = ms.ToArray();
                }

                await CapNhatTongSoLuong(mau.ChiTietSanPhamID);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật!";
            }
            return RedirectToAction("ChiTietSanPham", new { sanPhamId });
        }

        // ---- XÓA MÀU ----
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaMau(int mauSacId, int sanPhamId)
        {
            var mau = await _context.mauSacSanPhams.FindAsync(mauSacId);
            if (mau != null)
            {
                int ctspId = mau.ChiTietSanPhamID;
                _context.mauSacSanPhams.Remove(mau);
                await _context.SaveChangesAsync();

                // Cập nhật lại tổng SoLuong trong ChiTietSanPham
                await CapNhatTongSoLuong(ctspId);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Đã xóa màu '{mau.TenMau}'!";
            }
            return RedirectToAction("ChiTietSanPham", new { sanPhamId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaAnhMau(int mauSacId, int sanPhamId)
        {
            var mau = await _context.mauSacSanPhams.FindAsync(mauSacId);
            if (mau != null)
            {
                mau.HinhAnh = null;
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Đã xóa ảnh của màu '{mau.TenMau}'!";
            }
            return RedirectToAction("ChiTietSanPham", new { sanPhamId });
        }

        // ---- HELPER: cập nhật SoLuong trong ChiTietSanPham = tổng các màu ----
        private async Task CapNhatTongSoLuong(int chiTietSanPhamId)
        {
            var chiTiet = await _context.chiTietSanPhams.FindAsync(chiTietSanPhamId);
            if (chiTiet != null)
            {
                chiTiet.SoLuong = await _context.mauSacSanPhams
                    .Where(m => m.ChiTietSanPhamID == chiTietSanPhamId)
                    .SumAsync(m => m.SoLuong);
            }
        }

        #endregion

        #region Quản lý Thương hiệu

        // Danh sách thương hiệu
        public async Task<IActionResult> ThuongHieu()
        {
            var thuongHieus = await _context.thuongHieus
                .Include(t => t.SanPhams)
                .ToListAsync();
            return View(thuongHieus);
        }

        // Thêm thương hiệu - GET
        public IActionResult ThemThuongHieu()
        {
            return View();
        }

        // Thêm thương hiệu - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemThuongHieu(ThuongHieu thuongHieu, IFormFile logo)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload logo
                if (logo != null && logo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await logo.CopyToAsync(memoryStream);
                        thuongHieu.Logo = memoryStream.ToArray();
                    }
                }

                _context.Add(thuongHieu);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm thương hiệu thành công!";
                return RedirectToAction(nameof(ThuongHieu));
            }
            return View(thuongHieu);
        }

        // Sửa thương hiệu - GET
        public async Task<IActionResult> SuaThuongHieu(int? id)
        {
            if (id == null) return NotFound();

            var thuongHieu = await _context.thuongHieus.FindAsync(id);
            if (thuongHieu == null) return NotFound();

            return View(thuongHieu);
        }

        // Sửa thương hiệu - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaThuongHieu(int id, ThuongHieu thuongHieu, IFormFile logo)
        {
            if (id != thuongHieu.ThuongHieuID) return NotFound();

            if (ModelState.IsValid)
            {
                var thuongHieuCu = await _context.thuongHieus.AsNoTracking()
                    .FirstOrDefaultAsync(t => t.ThuongHieuID == id);

                // Xử lý upload logo mới
                if (logo != null && logo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await logo.CopyToAsync(memoryStream);
                        thuongHieu.Logo = memoryStream.ToArray();
                    }
                }
                else
                {
                    // Giữ logo cũ
                    thuongHieu.Logo = thuongHieuCu?.Logo;
                }

                _context.Update(thuongHieu);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cập nhật thương hiệu thành công!";
                return RedirectToAction(nameof(ThuongHieu));
            }
            return View(thuongHieu);
        }

        // Xóa thương hiệu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaThuongHieu(int id)
        {
            var thuongHieu = await _context.thuongHieus.FindAsync(id);
            if (thuongHieu != null)
            {
                // Kiểm tra có sản phẩm nào đang dùng thương hiệu này không
                var hasSanPham = await _context.SanPhams.AnyAsync(s => s.ThuongHieuID == id);
                if (hasSanPham)
                {
                    TempData["Error"] = "Không thể xóa thương hiệu đang có sản phẩm!";
                }
                else
                {
                    _context.thuongHieus.Remove(thuongHieu);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa thương hiệu thành công!";
                }
            }
            return RedirectToAction(nameof(ThuongHieu));
        }

        #endregion

        #region Quản lý Đơn hàng

        // Danh sách đơn hàng
        public async Task<IActionResult> DonHang(string trangThai, bool? daThanhToan)
        {
            ViewBag.TrangThai = trangThai;
            ViewBag.DaThanhToan = daThanhToan;

            var donHangs = _context.DonHangs
                .Include(d => d.NguoiDung)
                .Include(d => d.ChiTietDonHangs)
                .AsQueryable();

            if (!string.IsNullOrEmpty(trangThai))
                donHangs = donHangs.Where(d => d.TrangThai == trangThai);

            if (daThanhToan.HasValue)
                donHangs = donHangs.Where(d => d.DaThanhToan == daThanhToan.Value);

            return View(await donHangs.OrderByDescending(d => d.NgayDat).ToListAsync());
        }


        // Chi tiết đơn hàng
        public async Task<IActionResult> ChiTietDonHang(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs
                .Include(d => d.NguoiDung)
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(c => c.SanPham)
                .FirstOrDefaultAsync(d => d.DonHangID == id);

            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // Cập nhật trạng thái đơn hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThai)
        {
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang != null)
            {
                donHang.TrangThai = trangThai;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cập nhật trạng thái đơn hàng thành công!";
            }
            return RedirectToAction(nameof(ChiTietDonHang), new { id });
        }

        #endregion

        #region Quản lý Người dùng

        // Danh sách người dùng
        public async Task<IActionResult> NguoiDung()
        {
            var nguoiDungs = await _context.NguoiDungs
                .Include(n => n.Quyen)
                .OrderByDescending(n => n.NgayTao)
                .ToListAsync();

            return View(nguoiDungs);
        }


        #endregion

        #region Cap nhap thanh toan

        // Cap Nhat Thanh Toan 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatThanhToan(int id, string returnUrl = "list")
        {
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang != null)
            {
                donHang.DaThanhToan = !donHang.DaThanhToan;
                await _context.SaveChangesAsync();

                TempData["Success"] = donHang.DaThanhToan
                    ? "Đã đánh dấu thanh toán thành công!"
                    : "Đã đánh dấu chưa thanh toán!";
            }

            if (returnUrl == "detail")
                return RedirectToAction(nameof(ChiTietDonHang), new { id });

            return RedirectToAction(nameof(DonHang));
        }
        #endregion
    }
}