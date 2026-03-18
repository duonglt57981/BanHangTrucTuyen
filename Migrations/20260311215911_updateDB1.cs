using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssigmentC4_TrinhHuuThanh.Migrations
{
    /// <inheritdoc />
    public partial class updateDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_SanPhams_SanPhamID",
                table: "ChiTietSanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_ThuongHieu_ThuongHieuID",
                table: "SanPhams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThuongHieu",
                table: "ThuongHieu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietSanPham",
                table: "ChiTietSanPham");

            migrationBuilder.RenameTable(
                name: "ThuongHieu",
                newName: "thuongHieus");

            migrationBuilder.RenameTable(
                name: "ChiTietSanPham",
                newName: "chiTietSanPhams");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPham_SanPhamID",
                table: "chiTietSanPhams",
                newName: "IX_chiTietSanPhams_SanPhamID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_thuongHieus",
                table: "thuongHieus",
                column: "ThuongHieuID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chiTietSanPhams",
                table: "chiTietSanPhams",
                column: "ChiTietSanPhamID");

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "NguoiDungID",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "NguoiDungID",
                keyValue: 2,
                column: "NgayTao",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 1,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 2,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 3,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 4,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 5,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 6,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 7,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 8,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 9,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 10,
                column: "NgayThem",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_chiTietSanPhams_SanPhams_SanPhamID",
                table: "chiTietSanPhams",
                column: "SanPhamID",
                principalTable: "SanPhams",
                principalColumn: "SanPhamID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_thuongHieus_ThuongHieuID",
                table: "SanPhams",
                column: "ThuongHieuID",
                principalTable: "thuongHieus",
                principalColumn: "ThuongHieuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chiTietSanPhams_SanPhams_SanPhamID",
                table: "chiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_thuongHieus_ThuongHieuID",
                table: "SanPhams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_thuongHieus",
                table: "thuongHieus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chiTietSanPhams",
                table: "chiTietSanPhams");

            migrationBuilder.RenameTable(
                name: "thuongHieus",
                newName: "ThuongHieu");

            migrationBuilder.RenameTable(
                name: "chiTietSanPhams",
                newName: "ChiTietSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_chiTietSanPhams_SanPhamID",
                table: "ChiTietSanPham",
                newName: "IX_ChiTietSanPham_SanPhamID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThuongHieu",
                table: "ThuongHieu",
                column: "ThuongHieuID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietSanPham",
                table: "ChiTietSanPham",
                column: "ChiTietSanPhamID");

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "NguoiDungID",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "NguoiDungID",
                keyValue: 2,
                column: "NgayTao",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 1,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 2,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 3,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 4,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 5,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 6,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 7,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 8,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 9,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SanPhams",
                keyColumn: "SanPhamID",
                keyValue: 10,
                column: "NgayThem",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_SanPhams_SanPhamID",
                table: "ChiTietSanPham",
                column: "SanPhamID",
                principalTable: "SanPhams",
                principalColumn: "SanPhamID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_ThuongHieu_ThuongHieuID",
                table: "SanPhams",
                column: "ThuongHieuID",
                principalTable: "ThuongHieu",
                principalColumn: "ThuongHieuID");
        }
    }
}
