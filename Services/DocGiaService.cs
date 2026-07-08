using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class DocGiaService : IDocGiaService
    {
        private readonly IGenericRepository<DocGia> _docGiaRepo;
        private readonly IPhieuMuonRepository _phieuMuonRepo;

        public DocGiaService(IGenericRepository<DocGia> docGiaRepo, IPhieuMuonRepository phieuMuonRepo)
        {
            _docGiaRepo = docGiaRepo;
            _phieuMuonRepo = phieuMuonRepo;
        }

        public async Task<IEnumerable<DocGia>> GetAllAsync()
        {
            return await _docGiaRepo.GetAllAsync();
        }

        public async Task<DocGia> GetByIdAsync(int id)
        {
            return await _docGiaRepo.GetByIdAsync(id);
        }

        public async Task AddAsync(DocGia docGia)
        {
            await _docGiaRepo.AddAsync(docGia);
            await _docGiaRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocGia docGia)
        {
            _docGiaRepo.Update(docGia);
            await _docGiaRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var docGia = await _docGiaRepo.GetByIdAsync(id);
            if (docGia == null) return false;

            // Không cho xóa độc giả nếu đang có phiếu mượn (kể cả đã trả, để giữ lịch sử)
            var phieuMuons = await _phieuMuonRepo.FindAsync(p => p.DocGiaId == id);
            if (phieuMuons.Any())
                return false;

            _docGiaRepo.Delete(docGia);
            await _docGiaRepo.SaveChangesAsync();
            return true;
        }
    }
}
