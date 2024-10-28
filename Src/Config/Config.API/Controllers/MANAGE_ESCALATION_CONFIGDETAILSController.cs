using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_ESCALATION_CONFIGDETAILS;
using Config.Repository.Interfaces.MANAGE_ESCALATION_CONFIGDETAILS;
namespace Config.API
{

    [ApiController]
    [Route("Api/[controller]")]
    public class MANAGE_ESCALATION_CONFIGDETAILSController : ControllerBase
    {

        public IConfiguration Configuration;
        private readonly IMANAGE_ESCALATION_CONFIGDETAILSRepository _MANAGE_ESCALATION_CONFIGDETAILSRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public MANAGE_ESCALATION_CONFIGDETAILSController(IConfiguration configuration, IMANAGE_ESCALATION_CONFIGDETAILSRepository MANAGE_ESCALATION_CONFIGDETAILSRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _MANAGE_ESCALATION_CONFIGDETAILSRepository = MANAGE_ESCALATION_CONFIGDETAILSRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("CreateMANAGE_ESCALATION_CONFIGDETAILS")]
        public IActionResult MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL)
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
                    if (TBL.ConfigId == 0 || TBL.ConfigId == null)
                    {
                        var data = _MANAGE_ESCALATION_CONFIGDETAILSRepository.INSERT_MANAGE_ESCALATION_CONFIGDETAILS(TBL);
                        return Ok(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });

                    }
                    else
                    {
                        var data = _MANAGE_ESCALATION_CONFIGDETAILSRepository.UPDATE_MANAGE_ESCALATION_CONFIGDETAILS(TBL);
                        return Ok(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });

                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetMANAGE_ESCALATION_CONFIGDETAILS")]
        public async Task<IActionResult> Get_MANAGE_ESCALATION_CONFIGDETAILS()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ",ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                List<VIEWMANAGE_ESCALATION_CONFIGDETAILS> lst = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.VIEW_MANAGE_ESCALATION_CONFIGDETAILS(new MANAGE_ESCALATION_CONFIGDETAILS_Model());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Ok(jsonres);

            }

        }

        [HttpDelete("DeleteMANAGE_ESCALATION_CONFIGDETAILS")]

        public async Task<IActionResult> Delete_MANAGE_ESCALATION_CONFIGDETAILS(int Id)
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
                MANAGE_ESCALATION_CONFIGDETAILS_Model ob = new MANAGE_ESCALATION_CONFIGDETAILS_Model();
                ob.ConfigId = Id;

                var data = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.DELETE_MANAGE_ESCALATION_CONFIGDETAILS(ob);
                return Ok(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }
        [HttpGet("GetByIDMANAGE_ESCALATION_CONFIGDETAILS")]

        public async Task<IActionResult> EDIT_MANAGE_ESCALATION_CONFIGDETAILS(int Id)
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

                MANAGE_ESCALATION_CONFIGDETAILS_Model ob = new MANAGE_ESCALATION_CONFIGDETAILS_Model();
                ob.ConfigId = Id;
                List<EDITMANAGE_ESCALATION_CONFIGDETAILS> lst = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.EDIT_MANAGE_ESCALATION_CONFIGDETAILS(ob);
                var jsonres = JsonConvert.SerializeObject(lst?.FirstOrDefault());
                return Ok(jsonres);
            }

        }

    }
}
