using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG;
namespace GMS.API
{

    [ApiController]
    [Route("Api/[controller]")]
    public class MANAGE_COMPLAINTDETAILS_CONFIGController : ControllerBase
    {

        public IConfiguration Configuration;
        private readonly IMANAGE_COMPLAINTDETAILS_CONFIGRepository _MANAGE_COMPLAINTDETAILS_CONFIGRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public MANAGE_COMPLAINTDETAILS_CONFIGController(IConfiguration configuration, IMANAGE_COMPLAINTDETAILS_CONFIGRepository MANAGE_COMPLAINTDETAILS_CONFIGRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _MANAGE_COMPLAINTDETAILS_CONFIGRepository = MANAGE_COMPLAINTDETAILS_CONFIGRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("CreateMANAGE_COMPLAINTDETAILS_CONFIG")]
        public IActionResult MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL)
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
                    if (TBL.ComplaintId == 0 || TBL.ComplaintId == null)
                    {
                        var data = _MANAGE_COMPLAINTDETAILS_CONFIGRepository.INSERT_MANAGE_COMPLAINTDETAILS_CONFIG(TBL);
                        return Ok(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });

                    }
                    else
                    {
                        var data = _MANAGE_COMPLAINTDETAILS_CONFIGRepository.UPDATE_MANAGE_COMPLAINTDETAILS_CONFIG(TBL);
                        return Ok(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });

                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetMANAGE_COMPLAINTDETAILS_CONFIG")]
        public async Task<IActionResult> Get_MANAGE_COMPLAINTDETAILS_CONFIG()
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
                List<VIEWMANAGE_COMPLAINTDETAILS_CONFIG> lst = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.VIEW_MANAGE_COMPLAINTDETAILS_CONFIG(new MANAGE_COMPLAINTDETAILS_CONFIG_Model());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Ok(jsonres);

            }

        }

        [HttpDelete("DeleteMANAGE_COMPLAINTDETAILS_CONFIG")]

        public async Task<IActionResult> Delete_MANAGE_COMPLAINTDETAILS_CONFIG(int Id)
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
                MANAGE_COMPLAINTDETAILS_CONFIG_Model ob = new MANAGE_COMPLAINTDETAILS_CONFIG_Model();
                ob.ComplaintId = Id;

                var data = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.DELETE_MANAGE_COMPLAINTDETAILS_CONFIG(ob);
                return Ok(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }
        [HttpGet("GetByIDMANAGE_COMPLAINTDETAILS_CONFIG")]

        public async Task<IActionResult> EDIT_MANAGE_COMPLAINTDETAILS_CONFIG(int Id)
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

                MANAGE_COMPLAINTDETAILS_CONFIG_Model ob = new MANAGE_COMPLAINTDETAILS_CONFIG_Model();
                ob.ComplaintId = Id;
                List<EDITMANAGE_COMPLAINTDETAILS_CONFIG> lst = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.EDIT_MANAGE_COMPLAINTDETAILS_CONFIG(ob);
                var jsonres = JsonConvert.SerializeObject(lst?.FirstOrDefault());
                return Ok(jsonres);
            }

        }

    }
}
