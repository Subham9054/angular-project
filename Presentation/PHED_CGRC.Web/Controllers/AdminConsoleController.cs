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
            return View();


        }
    }
}
