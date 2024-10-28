using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_SUBCATEGORYMASTER;
using CommonMaster.Repository.Interfaces.MANAGE_SUBCATEGORYMASTER;
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
        [HttpPost("CreateMANAGE_SUBCATEGORYMASTER")]
        public IActionResult MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
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
                    if (TBL.SubCategoryId == 0 || TBL.SubCategoryId == null)
                    {
                        var data = _MANAGE_SUBCATEGORYMASTERRepository.INSERT_MANAGE_SUBCATEGORYMASTER(TBL);
                        return Ok(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });

                    }
                    else
                    {
                        var data = _MANAGE_SUBCATEGORYMASTERRepository.UPDATE_MANAGE_SUBCATEGORYMASTER(TBL);
                        return Ok(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });

                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetMANAGE_SUBCATEGORYMASTER")]
        public async Task<IActionResult> Get_MANAGE_SUBCATEGORYMASTER()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                List<VIEWMANAGE_SUBCATEGORYMASTER> lst = await _MANAGE_SUBCATEGORYMASTERRepository.VIEW_MANAGE_SUBCATEGORYMASTER(new MANAGE_SUBCATEGORYMASTER_Model());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Ok(jsonres);

            }

        }

        [HttpDelete("DeleteMANAGE_SUBCATEGORYMASTER")]

        public async Task<IActionResult> Delete_MANAGE_SUBCATEGORYMASTER(int Id)
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
                MANAGE_SUBCATEGORYMASTER_Model ob = new MANAGE_SUBCATEGORYMASTER_Model();
                ob.SubCategoryId = Id;

                var data = await _MANAGE_SUBCATEGORYMASTERRepository.DELETE_MANAGE_SUBCATEGORYMASTER(ob);
                return Ok(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }
        [HttpGet("GetByIDMANAGE_SUBCATEGORYMASTER")]

        public async Task<IActionResult> EDIT_MANAGE_SUBCATEGORYMASTER(int Id)
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

                MANAGE_SUBCATEGORYMASTER_Model ob = new MANAGE_SUBCATEGORYMASTER_Model();
                ob.SubCategoryId = Id;
                List<EDITMANAGE_SUBCATEGORYMASTER> lst = await _MANAGE_SUBCATEGORYMASTERRepository.EDIT_MANAGE_SUBCATEGORYMASTER(ob);
                var jsonres = JsonConvert.SerializeObject(lst?.FirstOrDefault());
                return Ok(jsonres);
            }

        }

    }
}
