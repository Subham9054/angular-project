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
        //[HttpPost("DetailcomplaintRegistration")]
        //public async Task<IActionResult> complaintRegistration([FromBody] Complaint complaint)
        //{
        //    if (complaint == null)
        //    {
        //        return BadRequest(new { message = "Provide all the data" });
        //    }
        //    try
        //    {
        //        var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.ComplaintRegistrationdetail(complaint);

        //        if (result)
        //        {
        //            return Ok(new { message = "Complaint registered successfully." });
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to register complaint." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
        //    }
        //}
        [HttpPost("DetailcomplaintRegistration")]
        public async Task<IActionResult> ComplaintRegistration([FromForm] Complaint complaint, [FromForm] List<IFormFile> files)
        {
            if (complaint == null)
            {
                return BadRequest(new { message = "Provide all the data" });
            }

            // Validation check
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                return BadRequest(new { errors = errorMessages });
            }

            try
            {
                // Initialize the list to store uploaded file names
                List<string> uploadedFiles = new List<string>();

                // Handle file upload if files are provided
                if (files != null && files.Count > 0)
                {
                    // Define the file storage path within wwwroot
                    var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var subFolderPath = Path.Combine(webRootPath, "assets", "ComplaintDocuments");

                    // Ensure directory exists
                    Directory.CreateDirectory(subFolderPath);

                    foreach (var file in files)
                    {
                        if (file == null || file.Length == 0)
                        {
                            continue;
                        }

                        // Validate file extension (only allow certain types)
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx" };
                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            return BadRequest(new { message = "Invalid file type. Only jpg, jpeg, png, pdf, and docx are allowed." });
                        }

                        // Validate file size (example: max 5MB)
                        if (file.Length > 5 * 1024 * 1024)
                        {
                            return BadRequest(new { message = "File size exceeds the 5MB limit." });
                        }

                        // Create a unique file name
                        var uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileNameWithoutExtension(file.FileName)}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(subFolderPath, uniqueFileName);

                        // Save the file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Add the file name to the list
                        uploadedFiles.Add(uniqueFileName);
                    }
                }

                // Save the uploaded file names in the complaint object (if any)
                complaint.VCH_COMPLAINT_FILE = uploadedFiles.Any() ? string.Join(",", uploadedFiles) : null;

                // Process the complaint registration
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.ComplaintRegistrationdetail(complaint);

                if (result != null)
                {
                    return Ok(new
                    {
                        message = "Complaint registered successfully.",
                        uploadedFiles = uploadedFiles,
                        Data = result
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to register complaint." });
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                // _logger.LogError(ex, "An error occurred during complaint registration");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while processing your request.",
                    error = ex.Message
                });
            }
        }

        [HttpPost("GetGmsComplaintdetails")]
        public async Task<IActionResult> getgmscomplaintdetails([FromBody]Userdata userdata)
        {
            try
            {
                var complaints = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.getGmscomplaintdetail(userdata.userid);
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

        [HttpGet("Getgmsactionhistory")]
        public async Task<IActionResult> Getgmsactionhistory(string token)
        {
            if (token == null)
            {
                return NotFound("Please provide token number");
            }
            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.Getgmsactionhistory(token);

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
        [HttpGet("GetAllDetailsagainsttoken")]
        public async Task<IActionResult> GetAllDetailsagainsttoken(string token, int catid, int subcatid)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(token) || catid <= 0 || subcatid <= 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Please provide a valid complaint token, category ID, and subcategory ID."
                });
            }

            try
            {
                // Fetch data from repository
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.Getalldetailagaintstoken(token, catid, subcatid);

                // Check if any records are found
                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No records found for the specified token, category, and subcategory."
                    });
                }

                // Return success response
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Records retrieved successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving records. Please try again later.",
                    Error = ex.Message // Include detailed error for debugging (optional in production)
                });
            }
        }
        #region Escalation
        [HttpPost("EscalationUpdateREP")]
        public async Task<IActionResult> EscalationUpdateREP([FromForm] ComplaintLog  complaintLog, [FromForm] List<IFormFile> files)
        {
            if (complaintLog == null)
            {
                return BadRequest(new { message = "Provide all the data" });
            }

            // Validation check
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                return BadRequest(new { errors = errorMessages });
            }

            try
            {
                // Initialize the list to store uploaded file names
                List<string> uploadedFiles = new List<string>();

                // Handle file upload if files are provided
                if (files != null && files.Count > 0)
                {
                    // Define the file storage path within wwwroot
                    var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var subFolderPath = Path.Combine(webRootPath, "assets", "ComplaintDocuments");

                    // Ensure directory exists
                    Directory.CreateDirectory(subFolderPath);

                    foreach (var file in files)
                    {
                        if (file == null || file.Length == 0)
                        {
                            continue;
                        }

                        // Validate file extension (only allow certain types)
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx" };
                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            return BadRequest(new { message = "Invalid file type. Only jpg, jpeg, png, pdf, and docx are allowed." });
                        }

                        // Validate file size (example: max 5MB)
                        if (file.Length > 5 * 1024 * 1024)
                        {
                            return BadRequest(new { message = "File size exceeds the 5MB limit." });
                        }

                        // Create a unique file name
                        var uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileNameWithoutExtension(file.FileName)}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(subFolderPath, uniqueFileName);

                        // Save the file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Add the file name to the list
                        uploadedFiles.Add(uniqueFileName);
                    }
                }

                // Save the uploaded file names in the complaint object (if any)
                complaintLog.VCH_FILE = uploadedFiles.Any() ? string.Join(",", uploadedFiles) : null;

                // Ensure designation is a string or perform type conversion if necessary
                // Declare 'result' outside of the if/else block to ensure it's accessible later
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.UpdatecomplainRep(complaintLog);

                //if (complaintLog.designation == "6")
                //{
                //    result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.UpdatecomplainCont(complaintLog);
                   
                //}
              

                // Process the complaint registration
                if (result != null)
                {
                    return Ok(new
                    {
                        message = "Complaint Escalated successfully.",
                        uploadedFiles = uploadedFiles, // assuming this is declared and holds file data
                        Data = result
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to register complaint." });
                }

            }
            catch (Exception ex)
            {
                // Optionally log the exception
                // _logger.LogError(ex, "An error occurred during complaint registration");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while processing your request.",
                    error = ex.Message
                });
            }
        }

        #endregion


        #region Mobile Team Api


        [HttpGet("GetCitizenAddressDetails")]
        public async Task<IActionResult> GetCitizenAddressDetails(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Please provide a valid token number.", StatusCode = 400 }); // 400 Bad Request
            }

            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetCitizendetails(token);

                if (result == null || !result.Any())
                {
                    return NotFound(new { Message = "No records found for the specified token.", StatusCode = 404 }); // 404 Not Found
                }

                return Ok(new { Message = "Citizen address details retrieved successfully.", Data = result, StatusCode = 200 }); // 200 OK
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "An error occurred while retrieving citizen address details.", Error = ex.Message, StatusCode = 500 }); // 500 Internal Server Error
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
                    return NotFound(new { Message = "No records found for the specified token.", StatusCode = 400 });
                }
                return Ok(new { Message = "Citizen address details updated successfully.", Data = result, StatusCode = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating citizen address details.", Error = ex.Message, StatusCode = 500 }); // 500 Internal Server Error
            }
        }


        [HttpPost("GetAllCitizenDetails")]
        public async Task<IActionResult> GetAllCitizenDetails([FromBody] GetAllCitizenDetails getAllCitizenDetails)
        {
            if (string.IsNullOrEmpty(getAllCitizenDetails.token) && string.IsNullOrEmpty(getAllCitizenDetails.mobno))
            {
                return StatusCode(200, new
                {
                    StatusCode = 400,
                    Message = "Please provide both token number and mobile number."
                });
            }

            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetallCitizendetails(getAllCitizenDetails.token, getAllCitizenDetails.mobno);

                if (result == null || !result.Any())
                {
                    return StatusCode(200, new
                    {
                        StatusCode = 201,
                        Message = "No records found for the specified token and mobile number."
                    });
                }

                return StatusCode(400, new
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

        [HttpPost("GetAllComplaints")]
        public async Task<IActionResult> GetAllComplaints([FromBody] OtpGenerate getallcomplaint)
        {
            if (string.IsNullOrEmpty(getallcomplaint.mobno))
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Please provide mobile number."
                });
            }

            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GetallComplaints(getallcomplaint.mobno);

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

        [HttpPost("Otpgenerate")]
        public async Task<IActionResult> Otpgenerate([FromBody] OtpGenerate otpGenerate)
        {
            if (string.IsNullOrWhiteSpace(otpGenerate.mobno) || otpGenerate.mobno.Length != 10)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Please provide a valid phone number."
                });
            }


            try
            {
                var result = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.GenerateOtp(otpGenerate.mobno);

                if (result == null)
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
                    Message = "Your OTP is " + result,
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
        public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PhoneNumber) || string.IsNullOrWhiteSpace(request.Otp))
            {
                return StatusCode(200, new { StatusCode = 400, Message = "Phone number and OTP are required." });
            }
            if (request.PhoneNumber.Length != 10)
            {
                return StatusCode(200, new { StatusCode = 400, Message = "Please Enter Valid Phone number" });
            }
            if (request.Otp.Length != 6)
            {
                return StatusCode(200, new { StatusCode = 400, Message = "Please Enter Valid Otp" });
            }

            try
            {
                // Validate OTP
                var otpDetails = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.ValidateOtp(request.PhoneNumber, request.Otp);

                if (otpDetails == null)
                {
                    return StatusCode(201, new { StatusCode = 201, Message = "Invalid OTP or OTP already used." });
                }

                // Check if OTP has expired
                if (DateTime.Now > otpDetails.ExpiresOn)
                {
                    return StatusCode(201, new { StatusCode = 201, Message = "OTP has expired." });
                }

                // Mark OTP as used
                var otpMarkedAsUsed = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.MarkOtpAsUsedAsync(otpDetails.Id);

                if (!otpMarkedAsUsed)
                {
                    return StatusCode(500, new { StatusCode = 500, Message = "Error marking OTP as used." });
                }

                return StatusCode(200, new { StatusCode = 200, Message = "OTP validated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
            }
        }

        //[HttpPost("ValidateOtp")]
        //public async Task<IActionResult> ValidateOtp(string phoneNumber, string otp)
        //{
        //    if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(otp))
        //    {
        //        return BadRequest(new { Message = "Phone number and OTP are required." });
        //    }

        //    try
        //    {
        //        // Validate OTP
        //        var otpDetails = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.ValidateOtp(phoneNumber, otp);

        //        if (otpDetails == null)
        //        {
        //            return StatusCode(201,new {Statuscode=201, Message = "Invalid OTP or OTP already used." });
        //        }

        //        // Check if OTP has expired
        //        if (DateTime.Now > otpDetails.ExpiresOn)
        //        {
        //            return StatusCode(201,new { Statuscode = 201, Message = "OTP has expired." });
        //        }

        //        // Mark OTP as used
        //        var otpMarkedAsUsed = await _MANAGE_COMPLAINTDETAILS_CONFIGRepository.MarkOtpAsUsedAsync(otpDetails.Id);

        //        if (otpMarkedAsUsed==false)
        //        {
        //            return StatusCode(500, new {StatusCode=500, Message = "Error marking OTP as used." });
        //        }

        //        return Ok(new { Message = "OTP validated successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
        //    }
        //}


        #endregion

    }
}
