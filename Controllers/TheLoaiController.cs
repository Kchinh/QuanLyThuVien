using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Filters;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [DangNhapRequired] // Bắt buộc đăng nhập mới truy cập được Controller này
    public class TheLoaiController : Controller
    {
        private readonly ITheLoaiService _theLoaiService;

        public TheLoaiController(ITheLoaiService theLoaiService)
        {
            _theLoaiService = theLoaiService;
        }

        // GET: /TheLoai
        public async Task<IActionResult> Index()
        {
            var danhSach = await _theLoaiService.GetAllAsync();
            return View(danhSach);
        }

        // GET: /TheLoai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /TheLoai/Create
        [HttpPost]
        public async Task<IActionResult> Create(TheLoai theLoai)
        {
            if (!ModelState.IsValid)
                return View(theLoai);

            await _theLoaiService.AddAsync(theLoai);
            TempData["ThongBao"] = $"Thêm thể loại \"{theLoai.TenTheLoai}\" thành công";
            return RedirectToAction(nameof(Index));
        }

        // GET: /TheLoai/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var theLoai = await _theLoaiService.GetByIdAsync(id);
            if (theLoai == null)
                return NotFound();

            return View(theLoai);
        }

        // POST: /TheLoai/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TheLoai theLoai)
        {
            if (id != theLoai.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(theLoai);

            await _theLoaiService.UpdateAsync(theLoai);
            TempData["ThongBao"] = "Cập nhật thể loại thành công";
            return RedirectToAction(nameof(Index));
        }

        // GET: /TheLoai/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var theLoai = await _theLoaiService.GetByIdAsync(id);
            if (theLoai == null)
                return NotFound();

            return View(theLoai);
        }

        // POST: /TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thanhCong = await _theLoaiService.DeleteAsync(id);

            if (!thanhCong)
                TempData["Loi"] = "Không thể xóa vì thể loại này đang có sách";
            else
                TempData["ThongBao"] = "Xóa thể loại thành công";

            return RedirectToAction(nameof(Index));
        }
    }
}
