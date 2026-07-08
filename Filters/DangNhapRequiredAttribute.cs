using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuanLyThuVien.Filters
{
    // Attribute tự viết để kiểm tra Session, gắn lên các Controller cần đăng nhập mới truy cập được
    public class DangNhapRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var nguoiDungId = context.HttpContext.Session.GetInt32("NguoiDungId");

            if (nguoiDungId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
