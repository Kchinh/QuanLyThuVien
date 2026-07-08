using QuanLyThuVien.Models;
using QuanLyThuVien.Models.ViewModels;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly IGenericRepository<NguoiDung> _nguoiDungRepo;

        public NguoiDungService(IGenericRepository<NguoiDung> nguoiDungRepo)
        {
            _nguoiDungRepo = nguoiDungRepo;
        }

        public async Task<IEnumerable<NguoiDung>> GetAllAsync()
        {
            return await _nguoiDungRepo.GetAllAsync();
        }

        public async Task<NguoiDung> GetByIdAsync(int id)
        {
            return await _nguoiDungRepo.GetByIdAsync(id);
        }

        public async Task<(bool ThanhCong, string ThongBao)> AddAsync(NguoiDungViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.MatKhau))
                return (false, "Vui lòng nhập mật khẩu cho tài khoản mới");

            // Kiểm tra trùng tên đăng nhập
            var trung = await _nguoiDungRepo.FindAsync(u => u.TenDangNhap == model.TenDangNhap);
            if (trung.Any())
                return (false, $"Tên đăng nhập \"{model.TenDangNhap}\" đã tồn tại");

            var nguoiDung = new NguoiDung
            {
                HoTen = model.HoTen,
                TenDangNhap = model.TenDangNhap,
                MatKhauHash = BCrypt.Net.BCrypt.HashPassword(model.MatKhau),
                VaiTro = model.VaiTro
            };

            await _nguoiDungRepo.AddAsync(nguoiDung);
            await _nguoiDungRepo.SaveChangesAsync();
            return (true, $"Thêm tài khoản \"{nguoiDung.TenDangNhap}\" thành công");
        }

        public async Task<(bool ThanhCong, string ThongBao)> UpdateAsync(NguoiDungViewModel model)
        {
            var nguoiDung = await _nguoiDungRepo.GetByIdAsync(model.Id);
            if (nguoiDung == null)
                return (false, "Không tìm thấy tài khoản");

            // Kiểm tra trùng tên đăng nhập với tài khoản khác (loại trừ chính nó)
            var trung = await _nguoiDungRepo.FindAsync(u => u.TenDangNhap == model.TenDangNhap && u.Id != model.Id);
            if (trung.Any())
                return (false, $"Tên đăng nhập \"{model.TenDangNhap}\" đã được dùng bởi tài khoản khác");

            nguoiDung.HoTen = model.HoTen;
            nguoiDung.TenDangNhap = model.TenDangNhap;
            nguoiDung.VaiTro = model.VaiTro;

            // Chỉ đổi mật khẩu nếu người dùng có nhập (để trống = giữ nguyên)
            if (!string.IsNullOrWhiteSpace(model.MatKhau))
                nguoiDung.MatKhauHash = BCrypt.Net.BCrypt.HashPassword(model.MatKhau);

            _nguoiDungRepo.Update(nguoiDung);
            await _nguoiDungRepo.SaveChangesAsync();
            return (true, "Cập nhật tài khoản thành công");
        }

        public async Task<(bool ThanhCong, string ThongBao)> DeleteAsync(int id, int nguoiDangDangNhapId)
        {
            if (id == nguoiDangDangNhapId)
                return (false, "Không thể tự xóa tài khoản đang đăng nhập");

            var nguoiDung = await _nguoiDungRepo.GetByIdAsync(id);
            if (nguoiDung == null)
                return (false, "Không tìm thấy tài khoản");

            // Không cho xóa Admin cuối cùng của hệ thống
            if (nguoiDung.VaiTro == VaiTro.Admin)
            {
                var tatCa = await _nguoiDungRepo.GetAllAsync();
                var soAdmin = tatCa.Count(u => u.VaiTro == VaiTro.Admin);
                if (soAdmin <= 1)
                    return (false, "Không thể xóa Admin duy nhất còn lại của hệ thống");
            }

            _nguoiDungRepo.Delete(nguoiDung);
            await _nguoiDungRepo.SaveChangesAsync();
            return (true, "Xóa tài khoản thành công");
        }
    }
}
