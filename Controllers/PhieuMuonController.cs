using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Filters;
using QuanLyThuVien.Models.ViewModels;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [DangNhapRequired]
    public class PhieuMuonController : Controller
    {
        private readonly IPhieuMuonService _phieuMuonService;
        private readonly ISachService _sachService;
        private readonly IDocGiaService _docGiaService;

        public PhieuMuonController(
            IPhieuMuonService phieuMuonService,
            ISachService sachService,
            IDocGiaService docGiaService)
        {
            _phieuMuonService = phieuMuonService;
            _sachService = sachService;
            _docGiaService = docGiaService;
        }

        // GET: /PhieuMuon - Danh sách toàn bộ phiếu mượn
        public async Task<IActionResult> Index()
        {
            var danhSach = await _phieuMuonService.GetAllAsync();
            return View(danhSach);
        }

        // GET: /PhieuMuon/Create - Form lập phiếu mượn mới
        public async Task<IActionResult> Create()
        {
            var tatCaSach = await _sachService.GetAllAsync();

            var model = new LapPhieuMuonViewModel
            {
                NgayHenTra = DateTime.Now.AddDays(7), // mặc định hẹn trả sau 7 ngày
                // Chỉ hiển thị sách còn hàng để chọn mượn
                DanhSachSachCoSan = tatCaSach.Where(s => s.SoLuongConLai > 0).ToList(),
                DanhSachDocGia = (await _docGiaService.GetAllAsync()).ToList()
            };

            return View(model);
        }

        // POST: /PhieuMuon/Create
        [HttpPost]
        public async Task<IActionResult> Create(LapPhieuMuonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DanhSachSachCoSan = (await _sachService.GetAllAsync()).Where(s => s.SoLuongConLai > 0).ToList();
                model.DanhSachDocGia = (await _docGiaService.GetAllAsync()).ToList();
                return View(model);
            }

            // Lấy Id người dùng đang đăng nhập từ Session để ghi vào phiếu mượn
            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId") ?? 0;

            var (thanhCong, thongBao) = await _phieuMuonService.LapPhieuMuonAsync(model, nguoiDungId);

            if (!thanhCong)
            {
                ModelState.AddModelError("", thongBao);
                model.DanhSachSachCoSan = (await _sachService.GetAllAsync()).Where(s => s.SoLuongConLai > 0).ToList();
                model.DanhSachDocGia = (await _docGiaService.GetAllAsync()).ToList();
                return View(model);
            }

            TempData["ThongBao"] = thongBao;
            return RedirectToAction(nameof(Index));
        }

        // GET: /PhieuMuon/Details/5 - Xem chi tiết phiếu + nút trả từng cuốn
        public async Task<IActionResult> Details(int id)
        {
            var phieuMuon = await _phieuMuonService.GetByIdAsync(id);
            if (phieuMuon == null) return NotFound();

            return View(phieuMuon);
        }

        // POST: /PhieuMuon/TraSach/3 - Trả 1 cuốn sách (chiTietPhieuMuonId)
        [HttpPost]
        public async Task<IActionResult> TraSach(int chiTietPhieuMuonId, int phieuMuonId)
        {
            var (thanhCong, thongBao) = await _phieuMuonService.TraSachAsync(chiTietPhieuMuonId);

            if (thanhCong)
                TempData["ThongBao"] = thongBao;
            else
                TempData["Loi"] = thongBao;

            return RedirectToAction(nameof(Details), new { id = phieuMuonId });
        }
    }
}
