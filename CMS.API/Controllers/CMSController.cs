using CMS.Model;
using CMS.Repositories.Repositories.CmsRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CMS.API.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CMSController : ControllerBase
    {
        public IConfiguration Configuration;        
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IContentManagementRepository _contentManagementRepository;

        public CMSController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IContentManagementRepository contentManagementRepository)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _contentManagementRepository = contentManagementRepository;
        }


        #region Citizen Mobile App API
        [HttpPost("GetComplaintCounts")]
        public async Task<IActionResult> GetComplaintCounts([FromBody] ComplaintCountsDto comCounts)
        {
            // Validate input parameters
            if (comCounts == null || string.IsNullOrWhiteSpace(comCounts.Mobile))
            {
                return BadRequest(new { Success = false, Message = "Mobile must be provided.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var complaintCounts = await _contentManagementRepository.GetComplaintCountsAsync(comCounts.Mobile);
                if (complaintCounts != null)
                {
                    return Ok(new { Success = true, Data = complaintCounts, StatusCode = StatusCodes.Status200OK });
                }

                return NotFound(new { Success = false, Message = "Complaint count details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }


        //[HttpGet("GetComplaintCounts")]
        //public async Task<IActionResult> GetComplaintCounts([FromQuery(Name = "Mobile")] string mobile)
        //{
        //    // Validate input parameters
        //    if (string.IsNullOrWhiteSpace(mobile))
        //    {
        //        return BadRequest(new
        //        {
        //            Success = false,
        //            Message = "Mobile must be provided.",
        //            StatusCode = StatusCodes.Status400BadRequest
        //        });
        //    }
        //    try
        //    {
        //        var complaintCounts = await _contentManagementRepository.GetComplaintCountsAsync(mobile);
        //        if (complaintCounts != null)
        //        {
        //            return Ok(new { Success = true, Data = complaintCounts, StatusCode = StatusCodes.Status200OK });
        //        }
        //        return NotFound(new { Success = false, Message = "Complaint count details are not found.", StatusCode = StatusCodes.Status404NotFound });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
        //    }
        //}

        [HttpGet("GetComplaintDetailsDrillDown")]
        public async Task<IActionResult> GetComplaintDetailsDrillDown([FromQuery(Name = "MobileNo")] string mobile, [FromQuery(Name = "ComplaintPriority")] int complaintStatusId)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Mobile must be provided and cannot be empty.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (complaintStatusId <= 0)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "ComplaintPriority must be a valid positive integer.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var complaintDetails = await _contentManagementRepository.GetComplaintDetailsDrillDownAsync(mobile, complaintStatusId);
                if (complaintDetails != null)
                {
                    return Ok(new { Success = true, Data = complaintDetails, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Complaint count details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region Demography Mapping API
        [HttpGet("GetCircles")]
        public async Task<IActionResult> GetCircles()
        {
            try
            {
                // Fetch circle details using the repository
                var circles = await _contentManagementRepository.GetCirclesAsync();

                if (circles != null && circles.Any())
                {
                    return StatusCode(200,new { Message= "Circle details are fetched successfully.", Data = circles, StatusCode=200 });
                }

                return NotFound(new { Success = false, Message = "Circle details not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpPost("GetDivisions")]
        public async Task<IActionResult> GetDivisions([FromBody] DivisionDto divisionDto)
        {
            // Validate input parameters
            if (divisionDto == null || divisionDto.CircleId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Valid CircleId must be provided.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Fetch division details using the repository method
                var divisions = await _contentManagementRepository.GetDivisionsAsync(divisionDto.CircleId);

                if (divisions != null)
                {
                    return StatusCode(200, new { Message = "Division details are fetched successfully.", Data = divisions, StatusCode = 200 });
                    //return Ok(new { Success = true, Data = division, StatusCode = StatusCodes.Status200OK });
                }

                return NotFound(new { Success = false, Message = "Division details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpPost("GetSubDivisions")]
        public async Task<IActionResult> GetSubDivisions([FromBody] SubDivisionDto subDivisionDto)
        {
            // Validate input parameters
            if (subDivisionDto == null || subDivisionDto.DivisionId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Valid DivisionId must be provided.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Fetch division details using the repository method
                var subDivisions = await _contentManagementRepository.GetSubDivisionsAsync(subDivisionDto.DivisionId);

                if (subDivisions != null)
                {
                    return StatusCode(200, new { Message = "Sub-Division details are fetched successfully.", Data = subDivisions, StatusCode = 200 });
                }

                return NotFound(new { Success = false, Message = "Sub-Division details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpPost("GetSections")]
        public async Task<IActionResult> GetSections([FromBody] SectionDto sectionDto)
        {
            // Validate input parameters
            if (sectionDto == null || sectionDto.SubDivisionId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Valid SubDivisionId must be provided.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Fetch section details using the repository method
                var sections = await _contentManagementRepository.GetSectionsAsync(sectionDto.SubDivisionId);

                if (sections != null)
                {
                    return StatusCode(200, new { Message = "Section details are fetched successfully.", Data = sections, StatusCode = 200 });
                }

                return NotFound(new { Success = false, Message = "Section details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region Manage Designation Master Page
        [HttpPost("CreateOrUpdateDesignation")]
        public async Task<IActionResult> CreateOrUpdateDesignation([FromForm] DesignationModel designationDetails)
        {
            if (designationDetails == null)
            {
                return BadRequest(new { Success = false, Message = "Designation data cannot be null." });
            }

            try
            {
                // Call repository to create or update designation details
                var result = await _contentManagementRepository.CreateOrUpdateDesignationAsync(designationDetails);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Designation details saved successfully.", StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Success = false, Message = "Failed to save designation details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        [HttpGet("GetDesignations")]
        public async Task<IActionResult> GetDesignations()
        {
            try
            {
                var designations = await _contentManagementRepository.GetDesignationsAsync();
                if (designations != null && designations.Any())
                {
                    return Ok(new { Success = true, Data = designations, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Designation details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        [HttpGet("GetDesignationById")]
        public async Task<IActionResult> GetDesignationById(int designationId)
        {
            if (designationId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Designation ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var designationDetails = await _contentManagementRepository.GetDesignationByIdAsync(designationId);
                if (designationDetails != null)
                {
                    return Ok(new { Success = true, Data = designationDetails, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Designation details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        [HttpDelete("DeleteDesignation")]
        public async Task<IActionResult> DeleteDesignation(int designationId)
        {
            if (designationId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Designation ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Call the repository to soft delete the designation details
                var result = await _contentManagementRepository.DeleteDesignationAsync(designationId);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Designation details deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete designation details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromQuery] UserDetailsModel userDetails)
        {
            if (userDetails == null)
            {
                return BadRequest(new { Success = false, Message = "User details cannot be null." });
            }

            try
            {
                // Call repository to change the password
                var result = await _contentManagementRepository.ChangePasswordAsync(userDetails);

                if (result == 1)
                {
                    return Ok(new { Success = true, Message = "Password changed successfully.", StatusCode = StatusCodes.Status200OK });
                }

                // Failure case (e.g., old password does not match)
                return BadRequest(new { Success = false, Message = "Old password does not match or user not found.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                // Return internal server error with exception message
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion
    }
}
