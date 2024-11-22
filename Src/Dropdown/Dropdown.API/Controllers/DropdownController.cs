using Dropdown.Repository.Repositories.Interfaces;
using Dropdown.Repository.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dropdown.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        public IConfiguration _configuration;
        public IDropdownRepository _dropdownRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public DropdownController(IConfiguration configuration, IDropdownRepository dropdownRepository, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _dropdownRepository = dropdownRepository;
            _hostingEnvironment = webHostEnvironment;
        }
        [HttpGet("GetDistricts")]
        public async Task<IActionResult> getdistricts()
        {
            try
            {
                var dists = await _dropdownRepository.GetDistricts();
                return Ok(dists);
            }
            catch
            {
                return BadRequest("Error in fetching Districts");
            }
        }
        [HttpGet("GetBlocks")]
        public async Task<IActionResult> getBlocks(int distid)
        {
            try
            {
                var blocks = await _dropdownRepository.GetBlocks(distid);
                return Ok(blocks);
            }
            catch
            {
                return BadRequest("Error in fetching Blocks");
            }
        }
        [HttpGet("GetGp")]
        public async Task<IActionResult> getGps(int blockid)
        {
            try
            {
                var gps = await _dropdownRepository.GetGp(blockid);
                return Ok(gps);
            }
            catch
            {
                return BadRequest("Error in fetching Gps");
            }
        }
        [HttpGet("GetVillages")]
        public async Task<IActionResult> getVillages(int gpid)
        {
            try
            {
                var villages = await _dropdownRepository.Getvillage(gpid);
                return Ok(villages);
            }
            catch
            {
                return BadRequest("Error in fetching Villages");
            }
        }

        [HttpGet("GetWards")]
        public async Task<IActionResult> getWards(int villageid)
        {
            try
            {
                var wards = await _dropdownRepository.Getward(villageid);
                return Ok(wards);
            }
            catch
            {
                return BadRequest("Error in fetching Wards");
            }
        }
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Define the base path where you want to store files
            var baseFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "assets");

            // Define sub-folder (ComplaintDocuments in this case)
            var subFolderPath = Path.Combine(baseFolderPath, "ComplaintDocuments");

            // Ensure the folders exist, if not, create them
            Directory.CreateDirectory(baseFolderPath);
            Directory.CreateDirectory(subFolderPath);

            // Generate the unique timestamp-based number (yyyymmddhhmmss)
            var uniqueNumber = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Get the file extension
            var fileExtension = Path.GetExtension(file.FileName);

            // Get the original file name without the extension
            var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);

            // Combine unique number and original file name with the extension
            var uniqueFileName = $"{uniqueNumber}_{originalFileName}{fileExtension}";

            // Define the file path to save the file with the unique file name
            var filePath = Path.Combine(subFolderPath, uniqueFileName);

            // Check if a file with the same name already exists
            if (System.IO.File.Exists(filePath))
            {
                return Conflict("A file with the same name already exists.");
            }

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Respond with success and return the unique file name
            return Ok(new { message = "File uploaded successfully!", fileName = uniqueFileName });
        }
        [HttpGet("GetComplaints")]
        public async Task<IActionResult> getComplaints()
        {
            try
            {
                var complaints = await _dropdownRepository.GetComplaints();
                return Ok(complaints);
            }
            catch
            {
                return BadRequest("Error in fetching Districts");
            }
        }
        [HttpGet("GetComplaintstype")]
        public async Task<IActionResult> getComplaintstype()
        {
            try
            {
                var complaintslog = await _dropdownRepository.GetComplaintslogtype();
                return Ok(complaintslog);
            }
            catch
            {
                return BadRequest("Error in fetching Complaints Type");
            }
        }
        [HttpGet("GetCategory")]
        public async Task<IActionResult> getCategoriess()
        {
            try
            {
                var categories = await _dropdownRepository.GetCategories();
                return Ok(categories);
            }
            catch
            {
                return BadRequest("Error in fetching Categories");
            }
        }
        [HttpGet("GetSubCategories")]
        public async Task<IActionResult> getSubCategories(int catid)
        {
            try
            {
                var subCategories = await _dropdownRepository.GetSubCategories(catid);
                return Ok(subCategories);
            }
            catch
            {
                return BadRequest("Error in fetching Subcategories");
            }
        }
        [HttpGet("GetDesignation")]
        public async Task<IActionResult> getDesignation()
        {
            try
            {
                var designations = await _dropdownRepository.getDesignation();
                return Ok(designations);
            }
            catch
            {
                return BadRequest("Error in fetching designations");
            }
        }
        [HttpGet("GetLocationLevel")]
        public async Task<IActionResult> getLocationLevel()
        {
            try
            {
                var locations = await _dropdownRepository.getLocation();
                return Ok(locations);
            }
            catch
            {
                return BadRequest("Error in fetching designations");
            }
        }
        [HttpGet("GetComplaintPriority")]
        public async Task<IActionResult> getcomplaintpriority()
        {
            try
            {
                var compority = await _dropdownRepository.GetComplaintPriority();
                return Ok(compority);
            }
            catch
            {
                return BadRequest("Error in fetching Districts");
            }
        }
    }
}
