using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface ISachService
    {
        Task<IEnumerable<Sach>> GetAllAsync();
        Task<Sach> GetByIdAsync(int id);
        Task<IEnumerable<Sach>> SearchAsync(string keyword);
        Task AddAsync(Sach sach);
        Task UpdateAsync(Sach sach);
        Task<bool> DeleteAsync(int id);
    }
}
