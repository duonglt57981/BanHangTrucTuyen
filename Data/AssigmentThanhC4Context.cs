// Data/ApplicationDbContext.cs
using Assigmemt_Thanh_C4.Models.DataModels;
using AssigmentC4_TrinhHuuThanh.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AssigmentC4_TrinhHuuThanh.Data
{
    public class AssigmentThanhC4Context : DbContext
    {
        public AssigmentThanhC4Context(DbContextOptions<AssigmentThanhC4Context> options)
            : base(options)
        {
        }

        public DbSet<QuyenNguoiDung> QuyenNguoiDungs { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<ChiTietSanPham> chiTietSanPhams { get; set; }
        public DbSet<ThuongHieu> thuongHieus { get; set; }
        public DbSet<MauSacSanPham> mauSacSanPhams{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed dữ liệu mẫu
            modelBuilder.Entity<QuyenNguoiDung>().HasData(
                new QuyenNguoiDung { QuyenID = 1, TenQuyen = "Admin" },
                new QuyenNguoiDung { QuyenID = 2, TenQuyen = "Khách hàng" }
            );

            // Seed tài khoản demo (password đã hash SHA256)
            // admin123 hash = jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=
            // customer123 hash = 8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92 (nhưng dùng SHA256 Base64)
            modelBuilder.Entity<NguoiDung>().HasData(
                new NguoiDung
                {
                    NguoiDungID = 1,
                    TenDangNhap = "admin",
                    MatKhau = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", // admin123
                    HoTen = "Quản trị viên",
                    Email = "admin@camerashop.vn",
                    SoDienThoai = "0123456789",
                    DiaChi = "123 Đường ABC, Quận 1, TP.HCM",
                    NgayTao = new DateTime(2026, 1, 1),
                    QuyenID = 1
                },
                new NguoiDung
                {
                    NguoiDungID = 2,
                    TenDangNhap = "customer",
                    MatKhau = "5o/ZKF3XYRonqXJbFBSdxIu27p2K8b8Y8M+6yUNlJIA=", // customer123
                    HoTen = "Nguyễn Văn A",
                    Email = "customer@example.com",
                    SoDienThoai = "0987654321",
                    DiaChi = "456 Đường XYZ, Quận 2, TP.HCM",
                    NgayTao = new DateTime(2026, 1, 1),
                    QuyenID = 2
                }
            );

            modelBuilder.Entity<DanhMuc>().HasData(
                new DanhMuc { DanhMucID = 1, TenDanhMuc = "Máy ảnh DSLR", MoTa = "Máy ảnh DSLR chuyên nghiệp" },
                new DanhMuc { DanhMucID = 2, TenDanhMuc = "Máy ảnh Mirrorless", MoTa = "Máy ảnh mirrorless không gương lật" },
                new DanhMuc { DanhMucID = 3, TenDanhMuc = "Máy ảnh du lịch", MoTa = "Máy ảnh compact cho du lịch" },
                new DanhMuc { DanhMucID = 4, TenDanhMuc = "Ống kính", MoTa = "Ống kính cho máy ảnh" },
                new DanhMuc { DanhMucID = 5, TenDanhMuc = "Phụ kiện", MoTa = "Phụ kiện máy ảnh" }
            );

            modelBuilder.Entity<SanPham>().HasData(
                new SanPham { SanPhamID = 1, TenSanPham = "Canon EOS 90D", MoTa = "Máy ảnh DSLR 32.5MP, video 4K", Gia = 32990000, TonKho = 15, DanhMucID = 1 },
                new SanPham { SanPhamID = 2, TenSanPham = "Nikon D850", MoTa = "Máy ảnh DSLR 45.7MP Full-frame", Gia = 75990000, TonKho = 8, DanhMucID = 1 },
                new SanPham { SanPhamID = 3, TenSanPham = "Sony A7 IV", MoTa = "Máy ảnh Mirrorless 33MP, chống rung 5 trục", Gia = 63990000, TonKho = 12, DanhMucID = 2 },
                new SanPham { SanPhamID = 4, TenSanPham = "Fujifilm X-T5", MoTa = "Máy ảnh Mirrorless 40MP, màu Fuji Film Simulation", Gia = 54990000, TonKho = 10, DanhMucID = 2 },
                new SanPham { SanPhamID = 5, TenSanPham = "Canon PowerShot G7 X Mark III", MoTa = "Máy ảnh compact 20MP, video 4K", Gia = 18990000, TonKho = 20, DanhMucID = 3 },
                new SanPham { SanPhamID = 6, TenSanPham = "Sony RX100 VII", MoTa = "Máy ảnh compact cao cấp 20MP", Gia = 31990000, TonKho = 15, DanhMucID = 3 },
                new SanPham { SanPhamID = 7, TenSanPham = "Canon EF 50mm f/1.8 STM", MoTa = "Ống kính fix khẩu độ lớn", Gia = 3290000, TonKho = 30, DanhMucID = 4 },
                new SanPham { SanPhamID = 8, TenSanPham = "Sony FE 24-70mm f/2.8 GM", MoTa = "Ống kính zoom chuyên nghiệp", Gia = 53990000, TonKho = 5, DanhMucID = 4 },
                new SanPham { SanPhamID = 9, TenSanPham = "Manfrotto Tripod 290 Xtra", MoTa = "Chân máy nhôm chuyên nghiệp", Gia = 4590000, TonKho = 25, DanhMucID = 5 },
                new SanPham { SanPhamID = 10, TenSanPham = "SanDisk Extreme Pro 128GB", MoTa = "Thẻ nhớ SDXC UHS-I tốc độ cao", Gia = 1290000, TonKho = 50, DanhMucID = 5 }
            );
        }
    }
}