using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssigmentC4_TrinhHuuThanh.Migrations
{
    /// <inheritdoc />
    public partial class updateDB7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "HinhAnh",
                table: "mauSacSanPhams",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HinhAnh",
                table: "mauSacSanPhams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
