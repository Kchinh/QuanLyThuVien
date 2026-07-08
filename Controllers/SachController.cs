using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Filters;
using QuanLyThuVien.Models;
using QuanLyThuVien.Models.ViewModels;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [DangNhapRequired]
    public class SachController : Controller
    {
        private readonly ISachService _sachService;
        private readonly ITheLoaiService _theLoaiService;

        public SachController(ISachService sachService, ITheLoaiService theLoaiService)
        {
            _sachService = sachService;
            _theLoaiService = theLoaiService;
        }

        // GET: /Sach?keyword=...
        public async Task<IActionResult> Index(string keyword)
        {
            var danhSach = string.IsNullOrWhiteSpace(keyword)
                ? await _sachService.GetAllAsync()
                : await _sachService.SearchAsync(keyword);

            ViewBag.Keyword = keyword; // để giữ lại từ khóa trên ô tìm kiếm sau khi search
            return View(danhSach);
        }

        // GET: /Sach/Create
        public async Task<IActionResult> Create()
        {
            var model = new SachViewModel
            {
                DanhSachTheLoai = (await _theLoaiService.GetAllAsync()).ToList()
            };
            return View(model);
        }

        // POST: /Sach/Create
        [HttpPost]
        public async Task<IActionResult> Create(SachViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DanhSachTheLoai = (await _theLoaiService.GetAllAsync()).ToList();
                return View(model);
            }

            var sach = new Sach
            {
                TenSach = model.TenSach,
                TacGia = model.TacGia,
                SoLuong = model.SoLuong,
                TheLoaiId = model.TheLoaiId
            };

            await _sachService.AddAsync(sach);
            TempData["ThongBao"] = $"Thêm sách \"{sach.TenSach}\" thành công";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Sach/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sach = await _sachService.GetByIdAsync(id);
            if (sach == null) return NotFound();

            var model = new SachViewModel
            {
                Id = sach.Id,
                TenSach = sach.TenSach,
                TacGia = sach.TacGia,
                SoLuong = sach.SoLuong,
                TheLoaiId = sach.TheLoaiId,
                DanhSachTheLoai = (await _theLoaiService.GetAllAsync()).ToList()
            };

            return View(model);
        }

        // POST: /Sach/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SachViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                model.DanhSachTheLoai = (await _theLoaiService.GetAllAsync()).ToList();
                return View(model);
            }

            var sach = new Sach
            {
                Id = model.Id,
                TenSach = model.TenSach,
                TacGia = model.TacGia,
                SoLuong = model.SoLuong,
                TheLoaiId = model.TheLoaiId
            };

            await _sachService.UpdateAsync(sach);
            TempData["ThongBao"] = "Cập nhật sách thành công";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Sach/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sach = await _sachService.GetByIdAsync(id);
            if (sach == null) return NotFound();
            return View(sach);
        }

        // POST: /Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thanhCong = await _sachService.DeleteAsync(id);

            if (!thanhCong)
                TempData["Loi"] = "Không thể xóa vì sách đang được mượn";
            else
                TempData["ThongBao"] = "Xóa sách thành công";

            return RedirectToAction(nameof(Index));
        }
    }
}
