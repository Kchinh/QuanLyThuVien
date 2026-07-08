using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IDocGiaService
    {
        Task<IEnumerable<DocGia>> GetAllAsync();
        Task<DocGia> GetByIdAsync(int id);
        Task AddAsync(DocGia docGia);
        Task UpdateAsync(DocGia docGia);
        Task<bool> DeleteAsync(int id);
    }
}
