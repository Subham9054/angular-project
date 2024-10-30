using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using PHED_CGRC.MANAGE_CATEGORYMASTER ;
using CommonMaster.Repository.Interfaces.MANAGE_CATEGORYMASTER;
namespace CommonMaster.API
{

 [ApiController]
 [Route("Api/[controller]")]
 public class MANAGE_CATEGORYMASTERController : ControllerBase
 {
 	 
		public IConfiguration Configuration;
		private readonly IMANAGE_CATEGORYMASTERRepository _MANAGE_CATEGORYMASTERRepository;
        private IWebHostEnvironment _hostingEnvironment;
		public MANAGE_CATEGORYMASTERController(IConfiguration configuration,IMANAGE_CATEGORYMASTERRepository MANAGE_CATEGORYMASTERRepository,IWebHostEnvironment hostingEnvironment)
		{
		Configuration = configuration;
	_MANAGE_CATEGORYMASTERRepository = MANAGE_CATEGORYMASTERRepository;
		
            _hostingEnvironment = hostingEnvironment;}
  [HttpPost("CreateMANAGE_CATEGORYMASTER")]
  public IActionResult MANAGE_CATEGORYMASTER(MANAGE_CATEGORYMASTER_Model TBL)
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
                if (TBL.CategoryId == 0 || TBL.CategoryId == null)
                {
                    var data = _MANAGE_CATEGORYMASTERRepository.INSERT_MANAGE_CATEGORYMASTER(TBL);
                    return Ok(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });

                }
                else
                {
                    var data = _MANAGE_CATEGORYMASTERRepository.UPDAE_MANAGE_CATEGORYMASTER(TBL);
                    return Ok(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });

                }
            }
   
            }
            catch (Exception)
            {
                throw;
            }
          }
		[HttpGet("GetMANAGE_CATEGORYMASTER")]
		public async Task<IActionResult> Get_MANAGE_CATEGORYMASTER()
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
			List<VIEWMANAGE_CATEGORYMASTER>	lst =await 	_MANAGE_CATEGORYMASTERRepository.VIEW_MANAGE_CATEGORYMASTER(new MANAGE_CATEGORYMASTER_Model());
		var jsonres = JsonConvert.SerializeObject(lst);
		
		return Ok(jsonres);
		
}
		
}   

   [HttpDelete("DeleteMANAGE_CATEGORYMASTER")]
       
        public async Task<IActionResult>Delete_MANAGE_CATEGORYMASTER(int Id)
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
                 MANAGE_CATEGORYMASTER_Model ob = new MANAGE_CATEGORYMASTER_Model();
                ob.CategoryId = Id;

                var data =await _MANAGE_CATEGORYMASTERRepository.DELETE_MANAGE_CATEGORYMASTER(ob);
                return Ok(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }        [HttpGet("GetByIDMANAGE_CATEGORYMASTER")]

        public async Task<IActionResult>EDIT_MANAGE_CATEGORYMASTER(int Id)
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

             MANAGE_CATEGORYMASTER_Model ob = new MANAGE_CATEGORYMASTER_Model();
                ob.CategoryId = Id;
                List<EDITMANAGE_CATEGORYMASTER> lst = await _MANAGE_CATEGORYMASTERRepository.EDIT_MANAGE_CATEGORYMASTER(ob);
                var jsonres = JsonConvert.SerializeObject(lst?.FirstOrDefault());
                return Ok(jsonres);
            }

        }
 
}
}
