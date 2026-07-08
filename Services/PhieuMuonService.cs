using QuanLyThuVien.Models;
using QuanLyThuVien.Models.ViewModels;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class PhieuMuonService : IPhieuMuonService
    {
        private readonly IPhieuMuonRepository _phieuMuonRepo;
        private readonly ISachRepository _sachRepo;
        private readonly IGenericRepository<ChiTietPhieuMuon> _chiTietRepo;

        public PhieuMuonService(
            IPhieuMuonRepository phieuMuonRepo,
            ISachRepository sachRepo,
            IGenericRepository<ChiTietPhieuMuon> chiTietRepo)
        {
            _phieuMuonRepo = phieuMuonRepo;
            _sachRepo = sachRepo;
            _chiTietRepo = chiTietRepo;
        }

        public async Task<IEnumerable<PhieuMuon>> GetAllAsync()
        {
            return await _phieuMuonRepo.GetAllWithDetailAsync();
        }

        public async Task<IEnumerable<PhieuMuon>> GetDangMuonAsync()
        {
            return await _phieuMuonRepo.GetDangMuonAsync();
        }

        public async Task<PhieuMuon> GetByIdAsync(int id)
        {
            return await _phieuMuonRepo.GetByIdWithDetailAsync(id);
        }

        public async Task<(bool ThanhCong, string ThongBao)> LapPhieuMuonAsync(LapPhieuMuonViewModel model, int nguoiDungId)
        {
            // B1: Kiểm tra tất cả sách được chọn còn đủ số lượng để cho mượn
            var danhSachSach = new List<Sach>();
            foreach (var sachId in model.SachIds.Distinct())
            {
                var sach = await _sachRepo.GetByIdAsync(sachId);
                if (sach == null)
                    return (false, $"Không tìm thấy sách có Id = {sachId}");

                if (sach.SoLuongConLai <= 0)
                    return (false, $"Sách \"{sach.TenSach}\" đã hết, không thể cho mượn");

                danhSachSach.Add(sach);
            }

            // B2: Tạo phiếu mượn chính
            var phieuMuon = new PhieuMuon
            {
                DocGiaId = model.DocGiaId,
                NguoiDungId = nguoiDungId,
                NgayMuon = DateTime.Now,
                NgayHenTra = model.NgayHenTra,
                TrangThai = TrangThaiPhieuMuon.DangMuon
            };
            await _phieuMuonRepo.AddAsync(phieuMuon);

            // Lưu trước để lấy được Id của PhieuMuon (cần cho ChiTiet)
            await _phieuMuonRepo.SaveChangesAsync();

            // B3: Tạo chi tiết phiếu mượn cho từng cuốn sách + trừ số lượng còn lại
            foreach (var sach in danhSachSach)
            {
                var chiTiet = new ChiTietPhieuMuon
                {
                    PhieuMuonId = phieuMuon.Id,
                    SachId = sach.Id,
                    NgayTraThucTe = null // chưa trả
                };
                await _chiTietRepo.AddAsync(chiTiet);

                sach.SoLuongConLai -= 1;
                _sachRepo.Update(sach);
            }

            await _chiTietRepo.SaveChangesAsync();

            return (true, $"Lập phiếu mượn thành công cho {danhSachSach.Count} cuốn sách");
        }

        public async Task<(bool ThanhCong, string ThongBao)> TraSachAsync(int chiTietPhieuMuonId)
        {
            var chiTiet = await _chiTietRepo.GetByIdAsync(chiTietPhieuMuonId);
            if (chiTiet == null)
                return (false, "Không tìm thấy chi tiết phiếu mượn này");

            if (chiTiet.NgayTraThucTe != null)
                return (false, "Cuốn sách này đã được trả trước đó");

            // B1: Đánh dấu đã trả
            chiTiet.NgayTraThucTe = DateTime.Now;
            _chiTietRepo.Update(chiTiet);

            // B2: Cộng lại số lượng còn lại của sách
            var sach = await _sachRepo.GetByIdAsync(chiTiet.SachId);
            if (sach != null)
            {
                sach.SoLuongConLai += 1;
                _sachRepo.Update(sach);
            }

            await _chiTietRepo.SaveChangesAsync();

            // B3: Kiểm tra nếu TẤT CẢ sách trong phiếu đã trả hết thì cập nhật trạng thái phiếu
            var phieuMuon = await _phieuMuonRepo.GetByIdWithDetailAsync(chiTiet.PhieuMuonId);
            if (phieuMuon != null && phieuMuon.ChiTietPhieuMuons.All(ct => ct.NgayTraThucTe != null))
            {
                phieuMuon.TrangThai = TrangThaiPhieuMuon.DaTraHet;
                _phieuMuonRepo.Update(phieuMuon);
                await _phieuMuonRepo.SaveChangesAsync();
            }

            return (true, "Trả sách thành công");
        }
    }
}
