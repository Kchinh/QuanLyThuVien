using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<NguoiDung> _nguoiDungRepo;

        public AuthService(IGenericRepository<NguoiDung> nguoiDungRepo)
        {
            _nguoiDungRepo = nguoiDungRepo;
        }

        public async Task<NguoiDung> DangNhapAsync(string tenDangNhap, string matKhau)
        {
            var danhSach = await _nguoiDungRepo.FindAsync(u => u.TenDangNhap == tenDangNhap);
            var nguoiDung = danhSach.FirstOrDefault();

            if (nguoiDung == null)
                return null;

            // So sánh mật khẩu người dùng nhập với hash đã lưu trong DB
            bool dungMatKhau = BCrypt.Net.BCrypt.Verify(matKhau, nguoiDung.MatKhauHash);

            return dungMatKhau ? nguoiDung : null;
        }
    }
}
