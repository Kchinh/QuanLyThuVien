using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class SachRepository : GenericRepository<Sach>, ISachRepository
    {
        public SachRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sach>> GetAllWithTheLoaiAsync()
        {
            return await _dbSet.Include(s => s.TheLoai).ToListAsync();
        }

        public async Task<Sach> GetByIdWithTheLoaiAsync(int id)
        {
            return await _dbSet.Include(s => s.TheLoai)
                                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sach>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllWithTheLoaiAsync();

            return await _dbSet.Include(s => s.TheLoai)
                .Where(s => s.TenSach.Contains(keyword) || s.TacGia.Contains(keyword))
                .ToListAsync();
        }
    }
}
