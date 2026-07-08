using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Filters;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [DangNhapRequired]
    public class DocGiaController : Controller
    {
        private readonly IDocGiaService _docGiaService;

        public DocGiaController(IDocGiaService docGiaService)
        {
            _docGiaService = docGiaService;
        }

        // GET: /DocGia
        public async Task<IActionResult> Index()
        {
            var danhSach = await _docGiaService.GetAllAsync();
            return View(danhSach);
        }

        // GET: /DocGia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /DocGia/Create
        [HttpPost]
        public async Task<IActionResult> Create(DocGia docGia)
        {
            if (!ModelState.IsValid)
                return View(docGia);

            await _docGiaService.AddAsync(docGia);
            TempData["ThongBao"] = $"Thêm độc giả \"{docGia.HoTen}\" thành công";
            return RedirectToAction(nameof(Index));
        }

        // GET: /DocGia/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var docGia = await _docGiaService.GetByIdAsync(id);
            if (docGia == null) return NotFound();
            return View(docGia);
        }

        // POST: /DocGia/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, DocGia docGia)
        {
            if (id != docGia.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(docGia);

            await _docGiaService.UpdateAsync(docGia);
            TempData["ThongBao"] = "Cập nhật độc giả thành công";
            return RedirectToAction(nameof(Index));
        }

        // GET: /DocGia/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var docGia = await _docGiaService.GetByIdAsync(id);
            if (docGia == null) return NotFound();
            return View(docGia);
        }

        // POST: /DocGia/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thanhCong = await _docGiaService.DeleteAsync(id);

            if (!thanhCong)
                TempData["Loi"] = "Không thể xóa vì độc giả này đã có lịch sử mượn sách";
            else
                TempData["ThongBao"] = "Xóa độc giả thành công";

            return RedirectToAction(nameof(Index));
        }
    }
}
