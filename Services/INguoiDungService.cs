using QuanLyThuVien.Models;
using QuanLyThuVien.Models.ViewModels;

namespace QuanLyThuVien.Services
{
    public interface INguoiDungService
    {
        Task<IEnumerable<NguoiDung>> GetAllAsync();
        Task<NguoiDung> GetByIdAsync(int id);
        Task<(bool ThanhCong, string ThongBao)> AddAsync(NguoiDungViewModel model);
        Task<(bool ThanhCong, string ThongBao)> UpdateAsync(NguoiDungViewModel model);
        Task<(bool ThanhCong, string ThongBao)> DeleteAsync(int id, int nguoiDangDangNhapId);
    }
}
