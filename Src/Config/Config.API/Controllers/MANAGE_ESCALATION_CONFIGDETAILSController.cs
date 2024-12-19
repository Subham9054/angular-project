using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_ESCALATION_CONFIGDETAILS;
using Config.Repository.Interfaces.MANAGE_ESCALATION_CONFIGDETAILS;
using Config.Model.Entities.Config;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
        [HttpPost("insertescalation")]
        public async Task<IActionResult> InsertEscalation([FromBody] EscalationModel request)
        {
            if (request == null)
            {
                return BadRequest(new { Error = "Request cannot be null." });
            }

            // Validate the model
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { Error = "Invalid input data.", Details = validationErrors });
            }

            try
            {
                if (request.EscalationDetails != null && request.EscalationDetails.Any())
                {
                    foreach (var detail in request.EscalationDetails)
                    {
                        // Validate individual detail items if necessary
                        if (detail == null || detail.INT_DESIG_ID == 0)
                        {
                            return BadRequest(new { Error = "Detail cannot be null or must have a valid INT_DESIG_ID." });
                        }

                        var escalationinsert = new Escalationinsert
                        {
                            INT_DESIG_ID = detail.INT_DESIG_ID,
                            INT_DESIG_LEVELID = detail.INT_DESIG_LEVELID,
                            VCH_STANDARD_DAYS = detail.VCH_STANDARD_DAYS,
                            INT_CATEGORY_ID = request.INT_CATEGORY_ID,
                            INT_SUB_CATEGORY_ID = request.INT_SUB_CATEGORY_ID,
                            INT_ESCALATION_LEVELID = request.INT_ESCALATION_LEVELID,
                            INT_CREATED_BY = request.INT_CREATED_BY, // Ensure this is passed if required
                            DTM_CREATED_ON = DateTime.Now // Consider setting this if not passed
                        };

                        // Call the repository method
                        int escalationId = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.InsertEscalation(escalationinsert);

                    }
                }

                return Ok(new { responseMessage = "Inserted Successfully.", responseText = "Success" });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, new { Error = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckEscalationExists(int categoryId, int subcategoryId)
        {
            try
            {
                int result = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.CheckEscalationExist(categoryId, subcategoryId);
                if (result == 0)
                {
                    return NotFound(new { Message = "No record found." });
                }
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Details = ex.Message });
            }
        }
        [HttpGet("viewescalation")]
        public async Task<IActionResult> ViewEscalation(int categoryId, int subcategoryId)
        {
            try
            {

                var result = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.GetEscalations(categoryId, subcategoryId);

                if (result == null || !result.Any())
                {
                    return NotFound("No escalation records found for the specified category and subcategory.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving escalations.");
            }
        }
        [HttpGet("viewescalationeye")]
        public async Task<IActionResult> ViewEscalationeye(int categoryId, int subcategoryId)
        {
            try
            {
                var result = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.GetEscalationseye(categoryId, subcategoryId);

                if (result == null || !result.Any())
                {
                    return NotFound("No escalation records found for the specified category and subcategory.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving escalations.");
            }
        }
        [HttpGet("viewupdatepen")]
        public async Task<IActionResult> ViewUpdatepen(int categoryId, int subcategoryId,int esclid)
        {
            try
            {
                var result = await _MANAGE_ESCALATION_CONFIGDETAILSRepository.GetUpdatepen(categoryId, subcategoryId, esclid);

                if (result == null || !result.Any())
                {
                    return NotFound("No escalation records found for the specified category and subcategory.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving escalations.");
            }
        }

        [HttpGet("GetMANAGE_ESCALATION_CONFIGDETAILS")]
        public async Task<IActionResult> Get_MANAGE_ESCALATION_CONFIGDETAILS()
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
