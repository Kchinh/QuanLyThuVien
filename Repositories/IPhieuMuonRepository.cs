using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public interface IPhieuMuonRepository : IGenericRepository<PhieuMuon>
    {
        // Lấy phiếu mượn kèm Độc giả + chi tiết sách (join sẵn)
        Task<IEnumerable<PhieuMuon>> GetAllWithDetailAsync();
        Task<PhieuMuon> GetByIdWithDetailAsync(int id);
        Task<IEnumerable<PhieuMuon>> GetDangMuonAsync();
    }
}
