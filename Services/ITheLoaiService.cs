using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface ITheLoaiService
    {
        Task<IEnumerable<TheLoai>> GetAllAsync();
        Task<TheLoai> GetByIdAsync(int id);
        Task AddAsync(TheLoai theLoai);
        Task UpdateAsync(TheLoai theLoai);
        Task<bool> DeleteAsync(int id);
    }
}
