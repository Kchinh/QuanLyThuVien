using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Filters;
using QuanLyThuVien.Models.ViewModels;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [DangNhapRequired]
    [AdminRequired] // Chỉ Admin mới vào được toàn bộ Controller này
    public class NguoiDungController : Controller
    {
        private readonly INguoiDungService _nguoiDungService;

        public NguoiDungController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

        // GET: /NguoiDung
        public async Task<IActionResult> Index()
        {
            var danhSach = await _nguoiDungService.GetAllAsync();
            return View(danhSach);
        }

        // GET: /NguoiDung/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /NguoiDung/Create
        [HttpPost]
        public async Task<IActionResult> Create(NguoiDungViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (thanhCong, thongBao) = await _nguoiDungService.AddAsync(model);

            if (!thanhCong)
            {
                ModelState.AddModelError("", thongBao);
                return View(model);
            }

            TempData["ThongBao"] = thongBao;
            return RedirectToAction(nameof(Index));
        }

        // GET: /NguoiDung/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var nguoiDung = await _nguoiDungService.GetByIdAsync(id);
            if (nguoiDung == null) return NotFound();

            var model = new NguoiDungViewModel
            {
                Id = nguoiDung.Id,
                HoTen = nguoiDung.HoTen,
                TenDangNhap = nguoiDung.TenDangNhap,
                VaiTro = nguoiDung.VaiTro
                // Không đổ MatKhau ra Form vì đã hash, không đọc ngược lại được
            };

            return View(model);
        }

        // POST: /NguoiDung/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NguoiDungViewModel model)
        {
            if (id != model.Id) return NotFound();

            // Bỏ qua validate bắt buộc cho MatKhau khi Sửa (được phép để trống)
            ModelState.Remove(nameof(model.MatKhau));

            if (!ModelState.IsValid)
                return View(model);

            var (thanhCong, thongBao) = await _nguoiDungService.UpdateAsync(model);

            if (!thanhCong)
            {
                ModelState.AddModelError("", thongBao);
                return View(model);
            }

            TempData["ThongBao"] = thongBao;
            return RedirectToAction(nameof(Index));
        }

        // GET: /NguoiDung/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var nguoiDung = await _nguoiDungService.GetByIdAsync(id);
            if (nguoiDung == null) return NotFound();
            return View(nguoiDung);
        }

        // POST: /NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiDangDangNhapId = HttpContext.Session.GetInt32("NguoiDungId") ?? 0;
            var (thanhCong, thongBao) = await _nguoiDungService.DeleteAsync(id, nguoiDangDangNhapId);

            if (thanhCong)
                TempData["ThongBao"] = thongBao;
            else
                TempData["Loi"] = thongBao;

            return RedirectToAction(nameof(Index));
        }
    }
}
