using AdminConsole.Repository.Repositories.Interfaces;
using AdminConsole.Repository.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace AdminConsole.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminConsoleController : ControllerBase
    {
        public IConfiguration _configuration;
        public IAdminconsRepository _adminconsRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public AdminConsoleController(IConfiguration configuration, IAdminconsRepository adminconsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _adminconsRepository = adminconsRepository;
            _hostingEnvironment = webHostEnvironment;
        }
        [HttpGet("GetDistricts")]
        public async Task<IActionResult> getdistricts()
        {
            try
            {
                var dists = await _adminconsRepository.GetDistricts();
                return Ok(dists);
            }
            catch
            {
                return BadRequest("Error in fetching Districts");
            }
        }
        [HttpGet("GetDesignation")]
        public async Task<IActionResult> getdesingtions()
        {
            try
            {
                var desingnation = await _adminconsRepository.getDesignation();
                return Ok(desingnation);
            }
            catch
            {
                return BadRequest("Error in fetching Desingnation");
            }
        }
       
    }
}
