using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_SUBCATEGORYMASTER;
using CommonMaster.Repository.Interfaces.MANAGE_SUBCATEGORYMASTER;
using CommonMaster.Model.Entities.CommonMaster;
namespace CommonMaster.API
{

    [ApiController]
    [Route("Api/[controller]")]
    public class MANAGE_SUBCATEGORYMASTERController : ControllerBase
    {

        public IConfiguration Configuration;
        private readonly IMANAGE_SUBCATEGORYMASTERRepository _MANAGE_SUBCATEGORYMASTERRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public MANAGE_SUBCATEGORYMASTERController(IConfiguration configuration, IMANAGE_SUBCATEGORYMASTERRepository MANAGE_SUBCATEGORYMASTERRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _MANAGE_SUBCATEGORYMASTERRepository = MANAGE_SUBCATEGORYMASTERRepository;

            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("ComplaintSubCategory")]
        public async Task<IActionResult> Complaintcatagory([FromBody] ComplaintSubCategoryModel complaintCategory)
        {

            if (complaintCategory == null)
            {
                return BadRequest("Provide All The Data");
            }

            try
            {
                var subcatagory = await _MANAGE_SUBCATEGORYMASTERRepository.InsertComplaintSubCategory(complaintCategory);
                return Ok(subcatagory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ViewComplaintSubCategory")]
        public async Task<IActionResult> viewComplaintcatagory(int catid, int subcatid)
        {

            if (catid == null && subcatid == null)
            {
                return BadRequest("Provide All The Data");
            }

            try
            {
                var subcatagory = await _MANAGE_SUBCATEGORYMASTERRepository.viewtComplaintSubCategory(catid, subcatid);
                return Ok(subcatagory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("UpdateComplaintSubCategory")]
        public async Task<IActionResult> UpdateComplaintSubCategory([FromQuery] int subcatid, [FromBody] UpdateComplaintSubCategoryModel complaintCategory)
        {
            if (complaintCategory == null)
            {
                return BadRequest("Provide All The Data");
            }
            try
            {
                var subcategory = await _MANAGE_SUBCATEGORYMASTERRepository.UpdateComplaintSubCategory(subcatid, complaintCategory);
                if (subcategory != null)
                {
                    return Ok(subcategory);  // You can return the updated subcategory data here
                }
                else
                {
                    return NotFound("Subcategory not found or could not be updated");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while updating the subcategory.");
            }
        }
        [HttpDelete("DeleteSubcat")]
        public async Task<IActionResult> GetComplaintdeletebyid(int catid, int subcatid)
        {
            try
            {
                var complaints = await _MANAGE_SUBCATEGORYMASTERRepository.getdeleteCatagorybyid(catid, subcatid);
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

        //    [HttpPost("CreateMANAGE_SUBCATEGORYMASTER")]
        //    public IActionResult MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
        //    {

        //        try
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                var message = string.Join(" | ", ModelState.Values
        //                              .SelectMany(v => v.Errors)
        //                              .Select(e => e.ErrorMessage));
        //                return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
        //            }
        //            else
        //            {
        //                if (TBL.SubCategoryId == 0 || TBL.SubCategoryId == null)
        //                {
        //                    var data = _MANAGE_SUBCATEGORYMASTERRepository.INSERT_MANAGE_SUBCATEGORYMASTER(TBL);
        //                    return Ok(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });

        //                }
        //                else
        //                {
        //                    var data = _MANAGE_SUBCATEGORYMASTERRepository.UPDATE_MANAGE_SUBCATEGORYMASTER(TBL);
        //                    return Ok(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });

        //                }
        //            }

        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    [HttpGet("GetMANAGE_SUBCATEGORYMASTER")]
        //    public async Task<IActionResult> Get_MANAGE_SUBCATEGORYMASTER()
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var message = string.Join(" | ", ModelState.Values
        // .SelectMany(v => v.Errors)
        //.Select(e => e.ErrorMessage));
        //            return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
        //        }
        //        else
        //        {
        //            List<VIEWMANAGE_SUBCATEGORYMASTER> lst = await _MANAGE_SUBCATEGORYMASTERRepository.VIEW_MANAGE_SUBCATEGORYMASTER(new MANAGE_SUBCATEGORYMASTER_Model());
        //            var jsonres = JsonConvert.SerializeObject(lst);

        //            return Ok(jsonres);

        //        }

        //    }

        //    [HttpDelete("DeleteMANAGE_SUBCATEGORYMASTER")]

        //    public async Task<IActionResult> Delete_MANAGE_SUBCATEGORYMASTER(int Id)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var message = string.Join(" | ", ModelState.Values
        //                .SelectMany(v => v.Errors)
        //                .Select(e => e.ErrorMessage));
        //            return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
        //        }
        //        else
        //        {
        //            MANAGE_SUBCATEGORYMASTER_Model ob = new MANAGE_SUBCATEGORYMASTER_Model();
        //            ob.SubCategoryId = Id;

        //            var data = await _MANAGE_SUBCATEGORYMASTERRepository.DELETE_MANAGE_SUBCATEGORYMASTER(ob);
        //            return Ok(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
        //        }
        //    }
        //    [HttpGet("GetByIDMANAGE_SUBCATEGORYMASTER")]

        //    public async Task<IActionResult> EDIT_MANAGE_SUBCATEGORYMASTER(int Id)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var message = string.Join(" | ", ModelState.Values
        //                .SelectMany(v => v.Errors)
        //                .Select(e => e.ErrorMessage));
        //            return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
        //        }
        //        else
        //        {

        //            MANAGE_SUBCATEGORYMASTER_Model ob = new MANAGE_SUBCATEGORYMASTER_Model();
        //            ob.SubCategoryId = Id;
        //            List<EDITMANAGE_SUBCATEGORYMASTER> lst = await _MANAGE_SUBCATEGORYMASTERRepository.EDIT_MANAGE_SUBCATEGORYMASTER(ob);
        //            var jsonres = JsonConvert.SerializeObject(lst?.FirstOrDefault());
        //            return Ok(jsonres);
        //        }

        //    }

    }
}
