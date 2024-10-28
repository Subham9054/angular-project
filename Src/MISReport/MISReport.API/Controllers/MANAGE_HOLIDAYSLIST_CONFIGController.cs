using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_HOLIDAYSLIST_CONFIG;
using MISReport.Repository.Interfaces.MANAGE_HOLIDAYSLIST_CONFIG;
namespace MISReport.API
{

    [ApiController]
    [Route("Api/[controller]")]
    public class MANAGE_HOLIDAYSLIST_CONFIGController : ControllerBase
    {

        public IConfiguration Configuration;
        private readonly IMANAGE_HOLIDAYSLIST_CONFIGRepository _MANAGE_HOLIDAYSLIST_CONFIGRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public MANAGE_HOLIDAYSLIST_CONFIGController(IConfiguration configuration, IMANAGE_HOLIDAYSLIST_CONFIGRepository MANAGE_HOLIDAYSLIST_CONFIGRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _MANAGE_HOLIDAYSLIST_CONFIGRepository = MANAGE_HOLIDAYSLIST_CONFIGRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("CreateMANAGE_HOLIDAYSLIST_CONFIG")]
        public IActionResult MANAGE_HOLIDAYSLIST_CONFIG(MANAGE_HOLIDAYSLIST_CONFIG_Model TBL)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                                  .SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage));
                    return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
                }
                else
                {
                    if (TBL.HolidayId == 0 || TBL.HolidayId == null)
                    {
                        var data = _MANAGE_HOLIDAYSLIST_CONFIGRepository.INSERT_MANAGE_HOLIDAYSLIST_CONFIG(TBL);
                        return Ok(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });

                    }
                    else
                    {
                        var data = _MANAGE_HOLIDAYSLIST_CONFIGRepository.UPDATE_MANAGE_HOLIDAYSLIST_CONFIG(TBL);
                        return Ok(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });

                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetMANAGE_HOLIDAYSLIST_CONFIG")]
        public async Task<IActionResult> Get_MANAGE_HOLIDAYSLIST_CONFIG()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
     .SelectMany(v => v.Errors)
    .Select(e => e.ErrorMessage));
                return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                List<VIEWMANAGE_HOLIDAYSLIST_CONFIG> lst = await _MANAGE_HOLIDAYSLIST_CONFIGRepository.VIEW_MANAGE_HOLIDAYSLIST_CONFIG(new MANAGE_HOLIDAYSLIST_CONFIG_Model());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Ok(jsonres);

            }

        }

        [HttpDelete("DeleteMANAGE_HOLIDAYSLIST_CONFIG")]

        public async Task<IActionResult> Delete_MANAGE_HOLIDAYSLIST_CONFIG(int Id)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                MANAGE_HOLIDAYSLIST_CONFIG_Model ob = new MANAGE_HOLIDAYSLIST_CONFIG_Model();
                ob.HolidayId = Id;

                var data = await _MANAGE_HOLIDAYSLIST_CONFIGRepository.DELETE_MANAGE_HOLIDAYSLIST_CONFIG(ob);
                return Ok(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }
        [HttpGet("GetByIDMANAGE_HOLIDAYSLIST_CONFIG")]

        public async Task<IActionResult> EDIT_MANAGE_HOLIDAYSLIST_CONFIG(int Id)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {

                MANAGE_HOLIDAYSLIST_CONFIG_Model ob = new MANAGE_HOLIDAYSLIST_CONFIG_Model();
                ob.HolidayId = Id;
                List<EDITMANAGE_HOLIDAYSLIST_CONFIG> lst = await _MANAGE_HOLIDAYSLIST_CONFIGRepository.EDIT_MANAGE_HOLIDAYSLIST_CONFIG(ob);
                var jsonres = JsonConvert.SerializeObject(lst?.FirstOrDefault());
                return Ok(jsonres);
            }

        }

    }
}
