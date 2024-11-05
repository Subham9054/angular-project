using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_CATEGORYMASTER;
using CommonMaster.Repository.Interfaces.MANAGE_CATEGORYMASTER;
using CommonMaster.Model.Entities.CommonMaster;
namespace CommonMaster.API
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MANAGE_CATEGORYMASTERController : ControllerBase
    {
        public IConfiguration Configuration;
        private readonly IMANAGE_CATEGORYMASTERRepository _MANAGE_CATEGORYMASTERRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public MANAGE_CATEGORYMASTERController(IConfiguration configuration, IMANAGE_CATEGORYMASTERRepository MANAGE_CATEGORYMASTERRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _MANAGE_CATEGORYMASTERRepository = MANAGE_CATEGORYMASTERRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("ComplaintCategory")]
        public async Task<IActionResult> Complaintcatagory([FromBody] ComplaintCategory complaintCategory)
        {

            if (complaintCategory == null)
            {
                return BadRequest("Provide All The Data");
            }

            try
            {
                var catagory = await _MANAGE_CATEGORYMASTERRepository.ComplaintCatagory(complaintCategory);
                return Ok(catagory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("GetallComplaint")]
        public async Task<IActionResult> GetAllComplaints()
        {
            try
            {
                var complaints = await _MANAGE_CATEGORYMASTERRepository.getCatagory();
                if (complaints == null || !complaints.Any())
                {
                    return Ok(new List<ComplaintCategory>()); // Return an empty list instead of NoContent
                }
                return Ok(complaints);
            }
            catch (Exception ex)
            {
                // Log exception details if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error"); // Return a meaningful error message
            }
        }

        [HttpPost("UpdateComplaint/{id}")]
        public async Task<IActionResult> UpdateComplaint([FromRoute] int id, [FromBody] ComplaintCategory complaintCategory)
        {
            if (complaintCategory == null)
            {
                return BadRequest(new { message = "Complaint category data is missing." });
            }
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid ID." });
            }
            try
            {
                var isUpdated = await _MANAGE_CATEGORYMASTERRepository.UpdateComplaintCatagory(id, complaintCategory);

                if (isUpdated)
                {
                    return Ok(new { message = "Complaint category updated successfully." });
                }
                else
                {
                    return NotFound(new { message = "Update failed. Category not found or no changes made." });
                }
            }
            catch (Exception ex)
            {
                // Log exception details if needed
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error.", details = ex.Message });
            }
        }

        [HttpDelete("deleteComplaintbyid/{id}")]
        public async Task<IActionResult> GetComplaintdeletebyid(int id)
        {
            try
            {
                var complaints = await _MANAGE_CATEGORYMASTERRepository.getdeleteCatagorybyid(id);
                if (complaints <= 0)
                {
                    return NoContent(); // Use NoContent for an empty result
                }
                return Ok(complaints);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error"); // Return a meaningful error message
            }
        }

    }
}
