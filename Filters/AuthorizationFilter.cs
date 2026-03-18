// Filters/AuthorizationFilter.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AssigmentC4_TrinhHuuThanh.Filters
{
    // Attribute kiểm tra đăng nhập
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var nguoiDungID = context.HttpContext.Session.GetInt32("NguoiDungID");

            if (!nguoiDungID.HasValue)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new
                {
                    returnUrl = context.HttpContext.Request.Path
                });
            }

            base.OnActionExecuting(context);
        }
    }

    // Attribute kiểm tra quyền Admin
    public class AdminAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var nguoiDungID = context.HttpContext.Session.GetInt32("NguoiDungID");
            var quyen = context.HttpContext.Session.GetString("Quyen");

            // Nếu chưa đăng nhập
            if (!nguoiDungID.HasValue)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new
                {
                    returnUrl = context.HttpContext.Request.Path
                });
                return;
            }

            // Nếu không phải Admin
            if (quyen != "Admin")
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}