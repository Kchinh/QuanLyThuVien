using QuanLyThuVien.Models;
using QuanLyThuVien.Models.ViewModels;

namespace QuanLyThuVien.Services
{
    public interface IPhieuMuonService
    {
        Task<IEnumerable<PhieuMuon>> GetAllAsync();
        Task<IEnumerable<PhieuMuon>> GetDangMuonAsync();
        Task<PhieuMuon> GetByIdAsync(int id);

        // Trả về (thành công hay không, thông báo) để Controller hiển thị cho người dùng
        Task<(bool ThanhCong, string ThongBao)> LapPhieuMuonAsync(LapPhieuMuonViewModel model, int nguoiDungId);
        Task<(bool ThanhCong, string ThongBao)> TraSachAsync(int chiTietPhieuMuonId);
    }
}
