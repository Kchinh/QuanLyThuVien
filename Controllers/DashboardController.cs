using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Filters;
using QuanLyThuVien.Models;
using QuanLyThuVien.Models.ViewModels;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [DangNhapRequired]
    public class DashboardController : Controller
    {
        private readonly ISachService _sachService;
        private readonly IDocGiaService _docGiaService;
        private readonly IPhieuMuonService _phieuMuonService;

        public DashboardController(
            ISachService sachService,
            IDocGiaService docGiaService,
            IPhieuMuonService phieuMuonService)
        {
            _sachService = sachService;
            _docGiaService = docGiaService;
            _phieuMuonService = phieuMuonService;
        }

        // GET: /Dashboard - trang chủ sau khi đăng nhập
        public async Task<IActionResult> Index()
        {
            var danhSachSach = (await _sachService.GetAllAsync()).ToList();
            var danhSachDocGia = (await _docGiaService.GetAllAsync()).ToList();
            var danhSachPhieuMuon = (await _phieuMuonService.GetAllAsync()).ToList();

            var model = new DashboardViewModel
            {
                // Tổng số bản sách vật lý (SoLuong gốc, không phải SoLuongConLai)
                TongSoSach = danhSachSach.Sum(s => s.SoLuong),
                TongSoDauSach = danhSachSach.Count,
                TongSoDocGia = danhSachDocGia.Count,

                SoPhieuDangMuon = danhSachPhieuMuon
                    .Count(p => p.TrangThai == TrangThaiPhieuMuon.DangMuon),

                SoPhieuQuaHan = danhSachPhieuMuon
                    .Count(p => p.TrangThai == TrangThaiPhieuMuon.DangMuon
                                && p.NgayHenTra.Date < DateTime.Now.Date),

                // Gom tất cả ChiTietPhieuMuon của mọi phiếu, đếm theo Sách, lấy Top 5
                TopSachMuonNhieu = danhSachPhieuMuon
                    .SelectMany(p => p.ChiTietPhieuMuons)
                    .GroupBy(ct => ct.Sach.TenSach)
                    .Select(g => (TenSach: g.Key, SoLuotMuon: g.Count()))
                    .OrderByDescending(x => x.SoLuotMuon)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }
    }
}
