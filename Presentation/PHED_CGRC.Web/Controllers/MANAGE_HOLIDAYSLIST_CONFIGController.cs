using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using  System.Text;
using PHED_CGRC.MANAGE_HOLIDAYSLIST_CONFIG;
namespace PHED_CGRC.Web
{
 public class MANAGE_HOLIDAYSLIST_CONFIGController : Controller
 {
 	 
		        private IWebHostEnvironment _hostingEnvironment;
		Uri url = new Uri("https://localhost:7197/gateway");
		 HttpClient client;
		public MANAGE_HOLIDAYSLIST_CONFIGController(IWebHostEnvironment hostingEnvironment)
		{
		 _hostingEnvironment = hostingEnvironment;
		client= new HttpClient();
		 client.BaseAddress = url;
		}
		[HttpGet]
		public IActionResult MANAGE_HOLIDAYSLIST_CONFIG()
		{
		return View();
		}

        [HttpPost]
        public async Task<IActionResult> MANAGE_HOLIDAYSLIST_CONFIG(MANAGE_HOLIDAYSLIST_CONFIG_Model TBL)
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
                HttpResponseMessage resNew = await client.PostAsync(url + "/MANAGE_HOLIDAYSLIST_CONFIG/CreateMANAGE_HOLIDAYSLIST_CONFIG", new StringContent(JsonConvert.SerializeObject(TBL),
Encoding.UTF8, "application/json"));

                if (TBL.HolidayId == 0)
                {
                    return Json(new { sucess = false, responseMessage = "Record Saved Successfully", responseText = "Success", data = "" });
                }
                else if (TBL.HolidayId > 0)
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
		public IActionResult ViewMANAGE_HOLIDAYSLIST_CONFIG()
		{
		return View();
		}
        [HttpGet]
        public async Task<JsonResult> Get_MANAGE_HOLIDAYSLIST_CONFIG()
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
                var data = JsonConvert.DeserializeObject<List<VIEWMANAGE_HOLIDAYSLIST_CONFIG>>(await client.GetStringAsync(url + "/MANAGE_HOLIDAYSLIST_CONFIG/GetMANAGE_HOLIDAYSLIST_CONFIG"));
                return Json(JsonConvert.SerializeObject(data));
            }
       
            }
            catch (Exception)
            {
                throw;
            }
             }
  
        [HttpDelete]

        public async Task<JsonResult> Delete_MANAGE_HOLIDAYSLIST_CONFIG(int Id)
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
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/MANAGE_HOLIDAYSLIST_CONFIG/DeleteMANAGE_HOLIDAYSLIST_CONFIG?Id=" + Id).Result;
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

        public async Task<JsonResult> E_MANAGE_HOLIDAYSLIST_CONFIG(int Id)
        {
            VIEWMANAGE_HOLIDAYSLIST_CONFIG lst = new VIEWMANAGE_HOLIDAYSLIST_CONFIG();
      
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
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/MANAGE_HOLIDAYSLIST_CONFIG/GetByIDMANAGE_HOLIDAYSLIST_CONFIG?Id=" + Id).Result;
                string errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error Response: " + errorResponse);
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    lst = JsonConvert.DeserializeObject<VIEWMANAGE_HOLIDAYSLIST_CONFIG>(data);
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
