using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class PhieuMuonRepository : GenericRepository<PhieuMuon>, IPhieuMuonRepository
    {
        public PhieuMuonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PhieuMuon>> GetAllWithDetailAsync()
        {
            return await _dbSet
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .OrderByDescending(p => p.NgayMuon)
                .ToListAsync();
        }

        public async Task<PhieuMuon> GetByIdWithDetailAsync(int id)
        {
            return await _dbSet
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PhieuMuon>> GetDangMuonAsync()
        {
            return await _dbSet
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.Sach)
                .Where(p => p.TrangThai == TrangThaiPhieuMuon.DangMuon)
                .ToListAsync();
        }
    }
}
