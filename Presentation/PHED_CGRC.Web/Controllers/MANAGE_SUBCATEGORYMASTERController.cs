using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using  System.Text;
using PHED_CGRC. MANAGE_SUBCATEGORYMASTER;
namespace PHED_CGRC.Web
{
 public class MANAGE_SUBCATEGORYMASTERController : Controller
 {
 	 
		        private IWebHostEnvironment _hostingEnvironment;
		Uri url = new Uri("https://localhost:7197/gateway");
		 HttpClient client;
		public MANAGE_SUBCATEGORYMASTERController(IWebHostEnvironment hostingEnvironment)
		{
		 _hostingEnvironment = hostingEnvironment;
		client= new HttpClient();
		 client.BaseAddress = url;
		}
		[HttpGet]
		public IActionResult MANAGE_SUBCATEGORYMASTER()
		{
		return View();
		}

        [HttpPost]
        public async Task<IActionResult> MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
        {
    
            try
            {
                    if (!ModelState.IsValid)
  {
   var message = string.Join(" | ", ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
     else
                 {
                HttpResponseMessage resNew = await client.PostAsync(url + "/MANAGE_SUBCATEGORYMASTER/CreateMANAGE_SUBCATEGORYMASTER", new StringContent(JsonConvert.SerializeObject(TBL),
Encoding.UTF8, "application/json"));

                if (TBL.SubCategoryId == 0)
                {
                    return Json(new { sucess = false, responseMessage = "Record Saved Successfully", responseText = "Success", data = "" });
                }
                else if (TBL.SubCategoryId > 0)
                {
                    return Json(new { sucess = false, responseMessage = "Updated Saved Successfully", responseText = "Success", data = "" });
                }
                else
                {
                    return Json(new { sucess = false, responseMessage = "Record Already Exist", responseText = "Fail", data = "" });
                }
            }
      
            }
            catch (Exception)
            {
                throw;
            }
             }

		[HttpGet]
		public IActionResult ViewMANAGE_SUBCATEGORYMASTER()
		{
		return View();
		}
        [HttpGet]
        public async Task<JsonResult> Get_MANAGE_SUBCATEGORYMASTER()
		        {
    
            try
            {
                       if (!ModelState.IsValid)
		{
			var message = string.Join(" | ", ModelState.Values
 .SelectMany(v => v.Errors)
.Select(e => e.ErrorMessage));
			return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });}
 else 
{
                var data = JsonConvert.DeserializeObject<List<VIEWMANAGE_SUBCATEGORYMASTER>>(await client.GetStringAsync(url + "/MANAGE_SUBCATEGORYMASTER/GetMANAGE_SUBCATEGORYMASTER"));
                return Json(JsonConvert.SerializeObject(data));
            }
       
            }
            catch (Exception)
            {
                throw;
            }
             }
  
        [HttpDelete]

        public async Task<JsonResult> Delete_MANAGE_SUBCATEGORYMASTER(int Id)
        {
     
            try
            {
                       int x=0;
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/MANAGE_SUBCATEGORYMASTER/DeleteMANAGE_SUBCATEGORYMASTER?Id=" + Id).Result;
                string errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error Response: " + errorResponse);
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    x = 1;
                }
                return Json(x);
            }
            
    
            }
            catch (Exception)
            {
                throw;
            }
            }

       
        [HttpGet]

        public async Task<JsonResult> E_MANAGE_SUBCATEGORYMASTER(int Id)
        {
            VIEWMANAGE_SUBCATEGORYMASTER lst = new VIEWMANAGE_SUBCATEGORYMASTER();
      
            try
            {
                      if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/MANAGE_SUBCATEGORYMASTER/GetByIDMANAGE_SUBCATEGORYMASTER?Id=" + Id).Result;
                string errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error Response: " + errorResponse);
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    lst = JsonConvert.DeserializeObject<VIEWMANAGE_SUBCATEGORYMASTER>(data);
                }
                
                var jsonres = JsonConvert.SerializeObject(lst);
                return Json(jsonres);
            }
     
            }
            catch (Exception)
            {
                throw;
            }
           }

}
}
