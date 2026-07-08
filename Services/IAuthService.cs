using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IAuthService
    {
        // Trả về NguoiDung nếu đăng nhập đúng, null nếu sai
        Task<NguoiDung> DangNhapAsync(string tenDangNhap, string matKhau);
    }
}
