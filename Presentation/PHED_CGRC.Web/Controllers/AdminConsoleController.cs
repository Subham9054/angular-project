using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace PHED_CGRC.Web.Controllers
{
    public class AdminConsoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddGlobalLink()
        {
            var s = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
              
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }
    }
}
