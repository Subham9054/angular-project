using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Model.Entities.GMS;
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

        [HttpPost("DetailcomplaintRegistration")]
        public async Task<IActionResult> complaintRegistration([FromBody] Complaint complaint)
        {
            if (complaint == null)
            {
                return BadRequest(new { message = "Provide all the data" });
            }
            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.ComplaintRegistrationdetail(complaint);

                if (result)
                {
                    return Ok(new { message = "Complaint registered successfully." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to register complaint." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }
        [HttpGet("GetGmsComplaintdetails")]
        public async Task<IActionResult> getgmscomplaintdetails()
        {
            try
            {
                var complaints = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.getGmscomplaintdetail();
                return Ok(complaints);
            }
            catch
            {
                return BadRequest("Error in fetching Districts");
            }
        }

        [HttpGet("Getgmstakeaction")]
        public async Task<IActionResult> Getgmstakeaction(string token)
        {
            if (token == null)
            {
                return NotFound("Please provide token number");
                
            }
            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.Getupdatetakeaction(token);

                if (result == null || !result.Any())
                {
                    return NotFound("No records found for the specified token.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving escalations.");
            }
        }



        #region Mobile Team Api
        [HttpGet("MobileOtpVerify")]
        public async Task<IActionResult> mobileotpverify(string otp)
        {
            try
            {
                var isValid = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.mobileotpverify(otp);
                if (isValid)
                {
                    return Ok("OTP Verified Successfully");
                }
                else
                {
                    return BadRequest("Invalid OTP");
                }
            }
            catch
            {
                return BadRequest("OTP Validation Error");
            }
        }

        [HttpGet("GetCitizenAddressDetails")]
        public async Task<IActionResult> GetCitizenAddressDetails(string token)
        {
            if (token == null)
            {
                return NotFound("Please provide token number");

            }
            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetCitizendetails(token);

                if (result == null || !result.Any())
                {
                    return NotFound("No records found for the specified token.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving escalations.");
            }
        }

        [HttpPost("UpdateCitizenAddressDetails")]
        public async Task<IActionResult> UpdateCitizenAddressDetails(string token, UpdateCitizen updateCitizen)
        {
            if (token == null)
            {
                return NotFound("Please provide token number");

            }
            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.UpdateCitizendetails(token, updateCitizen);

                if (result == null)
                {
                    return NotFound("No records found for the specified token.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving escalations.");
            }
        }
        [HttpGet("GetAllCitizenDetails")]
        public async Task<IActionResult> GetAllCitizenDetails(string token, string mobno)
        {
            if (token == null && mobno == null)
            {
                return NotFound("Please provide token number and mobile number");

            }
            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetallCitizendetails(token, mobno);

                if (result == null)
                {
                    return NotFound("No records found for the specified token.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving Records.");
            }
        }
        #endregion

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
