using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssigmentC4_TrinhHuuThanh.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    DanhMucID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.DanhMucID);
                });

            migrationBuilder.CreateTable(
                name: "QuyenNguoiDungs",
                columns: table => new
                {
                    QuyenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyenNguoiDungs", x => x.QuyenID);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    ThuongHieuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    QuocGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.ThuongHieuID);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    NguoiDungID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuyenID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.NguoiDungID);
                    table.ForeignKey(
                        name: "FK_NguoiDungs_QuyenNguoiDungs_QuyenID",
                        column: x => x.QuyenID,
                        principalTable: "QuyenNguoiDungs",
                        principalColumn: "QuyenID");
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    SanPhamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TonKho = table.Column<int>(type: "int", nullable: false),
                    NgayThem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DanhMucID = table.Column<int>(type: "int", nullable: true),
                    ThuongHieuID = table.Column<int>(type: "int", nullable: true),
                    HinhAnh = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.SanPhamID);
                    table.ForeignKey(
                        name: "FK_SanPhams_DanhMucs_DanhMucID",
                        column: x => x.DanhMucID,
                        principalTable: "DanhMucs",
                        principalColumn: "DanhMucID");
                    table.ForeignKey(
                        name: "FK_SanPhams_ThuongHieu_ThuongHieuID",
                        column: x => x.ThuongHieuID,
                        principalTable: "ThuongHieu",
                        principalColumn: "ThuongHieuID");
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    DonHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.DonHangID);
                    table.ForeignKey(
                        name: "FK_DonHangs_NguoiDungs_NguoiDungID",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDungs",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    GioHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.GioHangID);
                    table.ForeignKey(
                        name: "FK_GioHangs_NguoiDungs_NguoiDungID",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDungs",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietSanPham",
                columns: table => new
                {
                    ChiTietSanPhamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    DoPhanGiai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CamBien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ISO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OngKinh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KhauDo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChongRung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Video = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ManHinh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KetNoi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TrongLuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KichThuoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietSanPham", x => x.ChiTietSanPhamID);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHangs",
                columns: table => new
                {
                    ChiTietDonHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangID = table.Column<int>(type: "int", nullable: false),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHangs", x => x.ChiTietDonHangID);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_DonHangs_DonHangID",
                        column: x => x.DonHangID,
                        principalTable: "DonHangs",
                        principalColumn: "DonHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TheoDoiDonHang",
                columns: table => new
                {
                    TheoDoiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangID = table.Column<int>(type: "int", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheoDoiDonHang", x => x.TheoDoiID);
                    table.ForeignKey(
                        name: "FK_TheoDoiDonHang_DonHangs_DonHangID",
                        column: x => x.DonHangID,
                        principalTable: "DonHangs",
                        principalColumn: "DonHangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHangs",
                columns: table => new
                {
                    ChiTietGioHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GioHangID = table.Column<int>(type: "int", nullable: false),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHangs", x => x.ChiTietGioHangID);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHangs_GioHangs_GioHangID",
                        column: x => x.GioHangID,
                        principalTable: "GioHangs",
                        principalColumn: "GioHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHangs_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DanhMucs",
                columns: new[] { "DanhMucID", "MoTa", "TenDanhMuc" },
                values: new object[,]
                {
                    { 1, "Máy ảnh DSLR chuyên nghiệp", "Máy ảnh DSLR" },
                    { 2, "Máy ảnh mirrorless không gương lật", "Máy ảnh Mirrorless" },
                    { 3, "Máy ảnh compact cho du lịch", "Máy ảnh du lịch" },
                    { 4, "Ống kính cho máy ảnh", "Ống kính" },
                    { 5, "Phụ kiện máy ảnh", "Phụ kiện" }
                });

            migrationBuilder.InsertData(
                table: "QuyenNguoiDungs",
                columns: new[] { "QuyenID", "TenQuyen" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Khách hàng" }
                });

            migrationBuilder.InsertData(
                table: "NguoiDungs",
                columns: new[] { "NguoiDungID", "DiaChi", "Email", "HoTen", "MatKhau", "NgayTao", "QuyenID", "SoDienThoai", "TenDangNhap" },
                values: new object[,]
                {
                    { 1, "123 Đường ABC, Quận 1, TP.HCM", "admin@camerashop.vn", "Quản trị viên", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0123456789", "admin" },
                    { 2, "456 Đường XYZ, Quận 2, TP.HCM", "customer@example.com", "Nguyễn Văn A", "5o/ZKF3XYRonqXJbFBSdxIu27p2K8b8Y8M+6yUNlJIA=", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "0987654321", "customer" }
                });

            migrationBuilder.InsertData(
                table: "SanPhams",
                columns: new[] { "SanPhamID", "DanhMucID", "Gia", "HinhAnh", "MoTa", "NgayThem", "TenSanPham", "ThuongHieuID", "TonKho" },
                values: new object[,]
                {
                    { 1, 1, 32990000m, null, "Máy ảnh DSLR 32.5MP, video 4K", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon EOS 90D", null, 15 },
                    { 2, 1, 75990000m, null, "Máy ảnh DSLR 45.7MP Full-frame", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikon D850", null, 8 },
                    { 3, 2, 63990000m, null, "Máy ảnh Mirrorless 33MP, chống rung 5 trục", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony A7 IV", null, 12 },
                    { 4, 2, 54990000m, null, "Máy ảnh Mirrorless 40MP, màu Fuji Film Simulation", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fujifilm X-T5", null, 10 },
                    { 5, 3, 18990000m, null, "Máy ảnh compact 20MP, video 4K", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon PowerShot G7 X Mark III", null, 20 },
                    { 6, 3, 31990000m, null, "Máy ảnh compact cao cấp 20MP", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony RX100 VII", null, 15 },
                    { 7, 4, 3290000m, null, "Ống kính fix khẩu độ lớn", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon EF 50mm f/1.8 STM", null, 30 },
                    { 8, 4, 53990000m, null, "Ống kính zoom chuyên nghiệp", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony FE 24-70mm f/2.8 GM", null, 5 },
                    { 9, 5, 4590000m, null, "Chân máy nhôm chuyên nghiệp", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manfrotto Tripod 290 Xtra", null, 25 },
                    { 10, 5, 1290000m, null, "Thẻ nhớ SDXC UHS-I tốc độ cao", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SanDisk Extreme Pro 128GB", null, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_DonHangID",
                table: "ChiTietDonHangs",
                column: "DonHangID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_SanPhamID",
                table: "ChiTietDonHangs",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_GioHangID",
                table: "ChiTietGioHangs",
                column: "GioHangID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_SanPhamID",
                table: "ChiTietGioHangs",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_SanPhamID",
                table: "ChiTietSanPham",
                column: "SanPhamID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_NguoiDungID",
                table: "DonHangs",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_NguoiDungID",
                table: "GioHangs",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_QuyenID",
                table: "NguoiDungs",
                column: "QuyenID");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_DanhMucID",
                table: "SanPhams",
                column: "DanhMucID");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_ThuongHieuID",
                table: "SanPhams",
                column: "ThuongHieuID");

            migrationBuilder.CreateIndex(
                name: "IX_TheoDoiDonHang_DonHangID",
                table: "TheoDoiDonHang",
                column: "DonHangID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "ChiTietGioHangs");

            migrationBuilder.DropTable(
                name: "ChiTietSanPham");

            migrationBuilder.DropTable(
                name: "TheoDoiDonHang");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropTable(
                name: "QuyenNguoiDungs");
        }
    }
}
