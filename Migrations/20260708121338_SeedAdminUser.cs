using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVien.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NguoiDungs",
                columns: new[] { "Id", "HoTen", "MatKhauHash", "TenDangNhap", "VaiTro" },
                values: new object[] { 1, "Quản trị viên", "$2b$12$/BIkP98iFS/1qD0.MSqz3uEqYKkc8hv2YX4VwSzIIYKiiSGT0oQMq", "admin", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
