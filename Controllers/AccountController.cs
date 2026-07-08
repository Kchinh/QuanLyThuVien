using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models.ViewModels;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(DangNhapViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var nguoiDung = await _authService.DangNhapAsync(model.TenDangNhap, model.MatKhau);

            if (nguoiDung == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View(model);
            }

            // Lưu thông tin người dùng vào Session sau khi đăng nhập thành công
            HttpContext.Session.SetInt32("NguoiDungId", nguoiDung.Id);
            HttpContext.Session.SetString("HoTen", nguoiDung.HoTen);
            HttpContext.Session.SetString("VaiTro", nguoiDung.VaiTro.ToString());

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
