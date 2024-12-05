using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Model.Entities.GMS;
using MySql.Data.MySqlClient;
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


        [HttpGet("GetCitizenAddressDetails")]
        public async Task<IActionResult> GetCitizenAddressDetails(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Please provide a valid token number.",StatusCode=400 }); // 400 Bad Request
            }

            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetCitizendetails(token);

                if (result == null || !result.Any())
                {
                    return NotFound(new { Message = "No records found for the specified token.",StatusCode=404 }); // 404 Not Found
                }

                return Ok(new { Message = "Citizen address details retrieved successfully.", Data = result,StatusCode=200 }); // 200 OK
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "An error occurred while retrieving citizen address details.", Error = ex.Message,StatusCode=500 }); // 500 Internal Server Error
            }
        }


        [HttpPost("UpdateCitizenAddressDetails")]
        public async Task<IActionResult> UpdateCitizenAddressDetails(string token, UpdateCitizen updateCitizen)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Please provide a valid token number." }); 
            }

            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.UpdateCitizendetails(token, updateCitizen);

                if (result == null)
                {
                    return NotFound(new { Message = "No records found for the specified token.",StatusCode=400  }); 
                }
                return Ok(new { Message = "Citizen address details updated successfully.", Data = result,StatusCode=200 }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating citizen address details.", Error = ex.Message,StatusCode=500 }); // 500 Internal Server Error
            }
        }


        [HttpGet("GetAllCitizenDetails")]
        public async Task<IActionResult> GetAllCitizenDetails(string token, string mobno)
        {
            if (string.IsNullOrEmpty(token) && string.IsNullOrEmpty(mobno))
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Please provide both token number and mobile number."
                });
            }

            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetallCitizendetails(token, mobno);

                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No records found for the specified token and mobile number."
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Records retrieved successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving records.",
                    Error = ex.Message // Include the exception message for debugging
                });
            }
        }

        [HttpGet("GetAllComplaints")]
        public async Task<IActionResult> GetAllComplaints( string mobno)
        {
            if ( string.IsNullOrEmpty(mobno))
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Please provide mobile number."
                });
            }

            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetallComplaints( mobno);

                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No records found for mobile number."
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Records retrieved successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving records.",
                    Error = ex.Message // Include the exception message for debugging
                });
            }
        }

        [HttpGet("Otpgenerate")]
        public async Task<IActionResult> Otpgenerate(string mobno)
        {
            if (string.IsNullOrWhiteSpace(mobno) || mobno.Length != 10)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Please provide a valid phone number."
                });
            }


            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GenerateOtp(mobno);

                if (result == null || !result)
                {
                    return StatusCode(500, new
                    {
                        StatusCode = 500,
                        Message = "OTP generation failed. Please try again."
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "OTP generated successfully.",
                    Data = result // Include any additional data, if needed
                });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error: {ex.Message}");

                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An internal error occurred while generating OTP.",
                    Error = ex.Message
                });
            }
        }


        [HttpPost("ValidateOtp")]
        public async Task<IActionResult> ValidateOtp(string phoneNumber, string otp)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(otp))
            {
                return BadRequest(new { Message = "Phone number and OTP are required." });
            }

            try
            {
                // Validate OTP
                var otpDetails = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.ValidateOtp(phoneNumber, otp);

                if (otpDetails == null)
                {
                    return BadRequest(new { Message = "Invalid OTP or OTP already used." });
                }

                // Check if OTP has expired
                if (DateTime.Now > otpDetails.ExpiresOn)
                {
                    return BadRequest(new { Message = "OTP has expired." });
                }

                // Mark OTP as used
                var otpMarkedAsUsed = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.MarkOtpAsUsedAsync(otpDetails.Id);

                if (otpMarkedAsUsed==false)
                {
                    return StatusCode(500, new { Message = "Error marking OTP as used." });
                }

                return Ok(new { Message = "OTP validated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
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
