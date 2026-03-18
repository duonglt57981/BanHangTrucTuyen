using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssigmentC4_TrinhHuuThanh.Migrations
{
    /// <inheritdoc />
    public partial class updateDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mauSacSanPhams",
                columns: table => new
                {
                    MauSacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiTietSanPhamID = table.Column<int>(type: "int", nullable: false),
                    TenMau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mauSacSanPhams", x => x.MauSacID);
                    table.ForeignKey(
                        name: "FK_mauSacSanPhams_chiTietSanPhams_ChiTietSanPhamID",
                        column: x => x.ChiTietSanPhamID,
                        principalTable: "chiTietSanPhams",
                        principalColumn: "ChiTietSanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mauSacSanPhams_ChiTietSanPhamID",
                table: "mauSacSanPhams",
                column: "ChiTietSanPhamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mauSacSanPhams");
        }
    }
}
