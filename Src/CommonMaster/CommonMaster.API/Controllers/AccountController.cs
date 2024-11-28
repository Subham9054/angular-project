using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommonMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration Configuration;
    }
}
