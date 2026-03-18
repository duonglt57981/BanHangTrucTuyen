using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssigmentC4_TrinhHuuThanh.Migrations
{
    /// <inheritdoc />
    public partial class updateDB4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CRI",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChatLieu",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChieuCaoToiDa",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CongSuat",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KhoangCachLayNet",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NgamLens",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhietMau",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoLaKhau",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaiTrongToiDa",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TieuCu",
                table: "chiTietSanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRI",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "ChatLieu",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "ChieuCaoToiDa",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "CongSuat",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "KhoangCachLayNet",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "NgamLens",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "NhietMau",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "SoLaKhau",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "TaiTrongToiDa",
                table: "chiTietSanPhams");

            migrationBuilder.DropColumn(
                name: "TieuCu",
                table: "chiTietSanPhams");
        }
    }
}
