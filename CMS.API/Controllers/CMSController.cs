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

        #region Manage Pages
        [HttpPost("CreateOrUpdatePageLink")]
        public async Task<IActionResult> CreateOrUpdatePageLink([FromForm] PageLinkModel pageLink, [FromForm] IFormFile? iconFile)
        {
            if (pageLink == null)
            {
                return BadRequest(new { Success = false, Message = "Page Link data cannot be null." });
            }

            try
            {
                // Handle file upload if a new iconFile is provided
                if (iconFile != null && iconFile.Length > 0)
                {
                    // Set directory path and ensure it exists
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "icons");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Set unique file name to avoid overwriting
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(iconFile.FileName)}";
                    var filePath = Path.Combine(folderPath, fileName);

                    // Save the file to wwwroot/assets/icons
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await iconFile.CopyToAsync(stream);
                    }

                    // Set the file path to be saved in the database
                    pageLink.Icon = $"/assets/icons/{fileName}";
                }
                else if (string.IsNullOrEmpty(pageLink.Icon))
                {
                    var existingPageLink = await _contentManagementRepository.GetPageLinkByIdAsync(pageLink.PageId!.Value);
                    if (existingPageLink != null && existingPageLink.Count > 0)
                    {
                        // Assuming that the first element of existingPageLink has the Icon property you want to retain
                        pageLink.Icon = existingPageLink.FirstOrDefault()?.Icon; // Retain the existing icon path
                    }
                }

                // Call repository to create or update the page link
                var result = await _contentManagementRepository.CreateOrUpdatePageLinkAsync(pageLink);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Page Link Menu saved successfully.", StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Success = false, Message = "Failed to save Page Link Menu.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }        

        [HttpGet("GetPageLinks")]
        public async Task<IActionResult> GetPageLinks()
        {
            try
            {
                var pageLinks = await _contentManagementRepository.GetPageLinkAsync();
                if (pageLinks != null && pageLinks.Any())
                {
                    return Ok(new { Success = true, Data = pageLinks, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "No page links are found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetPageLinkById")]
        public async Task<IActionResult> GetPageLinkById(int pageId)
        {
            if (pageId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Page ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var pageLink = await _contentManagementRepository.GetPageLinkByIdAsync(pageId);
                if (pageLink != null)
                {
                    return Ok(new { Success = true, Data = pageLink, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Page link is not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpDelete("DeletePageLink")]
        public async Task<IActionResult> DeletePageLink(int pageId)
        {
            if (pageId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Page Link Menu ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Call the repository to delete the header menu
                var result = await _contentManagementRepository.DeletePageLinkAsync(pageId);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Page Link Menu deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete Page Link Menu.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetParentMenus")]
        public async Task<IActionResult> GetParentMenus()
        {
            try
            {
                var parentMenus = await _contentManagementRepository.GetParentMenusAsync();
                if (parentMenus != null && parentMenus.Any())
                {
                    return Ok(new { Success = true, Data = parentMenus, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "No page links are found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetMenuSubmenu")]
        public async Task<IActionResult> GetMenuSubmenu()
        {
            try
            {
                var headerMenus = await _contentManagementRepository.GetMenuSubmenuAsync();

                // Check if headerMenus is not null and if the Data property contains any items
                if (headerMenus != null && headerMenus.Data != null && headerMenus.Data.Any())
                {
                    return Ok(new { Success = true, Data = headerMenus, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "No Header Menus found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region Banner Master Page
        [HttpPost("CreateOrUpdateBanner")]
        public async Task<IActionResult> CreateOrUpdateBanner([FromForm] BannerModel banner, [FromForm] IFormFile? bannerImage)
        {
            if (banner == null)
            {
                return BadRequest(new { Success = false, Message = "Bannner data cannot be null." });
            }

            try
            {
                // Handle file upload if a new iconFile is provided
                if (bannerImage != null && bannerImage.Length > 0)
                {
                    // Set directory path and ensure it exists
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "banners");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Set unique file name to avoid overwriting
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(bannerImage.FileName)}";
                    var filePath = Path.Combine(folderPath, fileName);

                    // Save the file to wwwroot/assets/icons
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await bannerImage.CopyToAsync(stream);
                    }

                    // Set the file path to be saved in the database
                    banner.BannerImage = $"/assets/banners/{fileName}";
                }
                else if (string.IsNullOrEmpty(banner.BannerImage))
                {
                    var existingbanner = await _contentManagementRepository.GetBannerByIdAsync(banner.BannerId!.Value);
                    if (existingbanner != null && existingbanner.Count > 0)
                    {
                        // Assuming that the first element of existingPageLink has the Icon property you want to retain
                        banner.BannerImage = existingbanner.FirstOrDefault()?.BannerImage; // Retain the existing icon path
                    }
                }

                // Call repository to create or update the banner details
                var result = await _contentManagementRepository.CreateOrUpdateBannerDetailsAsync(banner);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Banner details saved successfully.", StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Success = false, Message = "Failed to save banner details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetBanners")]
        public async Task<IActionResult> GetBanners()
        {
            try
            {
                var banners = await _contentManagementRepository.GetBannersAsync();
                if (banners != null && banners.Any())
                {
                    return Ok(new { Success = true, Data = banners, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Banner details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetBannerById")]
        public async Task<IActionResult> GetBannerById(int bannerId)
        {
            if (bannerId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Banner ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var banner = await _contentManagementRepository.GetBannerByIdAsync(bannerId);
                if (banner != null)
                {
                    return Ok(new { Success = true, Data = banner, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Banner Details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetBannerByName")]
        public async Task<IActionResult> GetBannerByName(string bannerName)
        {
            try
            {
                var banner = await _contentManagementRepository.GetBannerByNameAsync(bannerName);
                if (banner != null)
                {
                    return Ok(new { Success = true, Data = banner, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Banner Details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpDelete("DeleteBanner")]
        public async Task<IActionResult> DeleteBanner(int bannerId)
        {
            if (bannerId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Banner ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Call the repository to soft delete the banner details
                var result = await _contentManagementRepository.DeleteBannerDetailsAsync(bannerId);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Banner deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete banner details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region Page Content Master Page

        #endregion

        #region What is New Master Page
        [HttpPost("CreateOrUpdateWhatIsNew")]
        public async Task<IActionResult> CreateOrUpdateWhatIsNew([FromForm] WhatIsNewModel whatIsNewModel, [FromForm] IFormFile? documentFile)
        {
            if (whatIsNewModel == null)
            {
                return BadRequest(new { Success = false, Message = "What's new data cannot be null." });
            }

            try
            {
                // Handle document upload
                if (documentFile != null && documentFile.Length > 0)
                {
                    var documentFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "whatnews");

                    // Create directory if it does not exist
                    if (!Directory.Exists(documentFolderPath))
                    {
                        Directory.CreateDirectory(documentFolderPath);
                    }

                    // Generate a unique file name using GUID and original file extension to avoid overwriting
                    var documentFileName = $"{Guid.NewGuid()}_{Path.GetFileName(documentFile.FileName)}";
                    var documentFilePath = Path.Combine(documentFolderPath, documentFileName);

                    // Save the file to wwwroot/assets/whatnews
                    using (var stream = new FileStream(documentFilePath, FileMode.Create))
                    {
                        await documentFile.CopyToAsync(stream);
                    }

                    // Save the document path to the database
                    whatIsNewModel.DocumentFile = $"/assets/whatnews/{documentFileName}";
                }
                else if (string.IsNullOrEmpty(whatIsNewModel.DocumentFile))
                {
                    // If no new document is uploaded, retain the existing document path
                    var existingWhatIsNew = await _contentManagementRepository.GetWhatIsNewByIdAsync(whatIsNewModel.WhatIsNewId!.Value);
                    if (existingWhatIsNew != null && existingWhatIsNew.Count > 0)
                    {
                        whatIsNewModel.DocumentFile = existingWhatIsNew.FirstOrDefault()?.DocumentFile; // Retain the existing document path
                    }
                }

                // Call repository to create or update what is new details
                var result = await _contentManagementRepository.CreateOrUpdateWhatIsNewAsync(whatIsNewModel);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "What's new details saved successfully.", StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Success = false, Message = "Failed to save what's new details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetWhatIsNews")]
        public async Task<IActionResult> GetWhatIsNews()
        {
            try
            {
                var whatIsNews = await _contentManagementRepository.GetWhatIsNewsAsync();
                if (whatIsNews != null && whatIsNews.Any())
                {
                    return Ok(new { Success = true, Data = whatIsNews, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "What's News details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetWhatIsNewById")]
        public async Task<IActionResult> GetWhatIsNewById(int whatIsNewId)
        {
            if (whatIsNewId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid What's New ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var whatIsNew = await _contentManagementRepository.GetWhatIsNewByIdAsync(whatIsNewId);
                if (whatIsNew != null)
                {
                    return Ok(new { Success = true, Data = whatIsNew, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "What's new details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetWhatIsNewName")]
        public async Task<IActionResult> GetWhatIsNewName(string whatIsNewName)
        {
            try
            {
                var whatIsNew = await _contentManagementRepository.GetWhatIsNewByNameAsync(whatIsNewName);
                if (whatIsNew != null)
                {
                    return Ok(new { Success = true, Data = whatIsNew, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "What's new details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpDelete("DeleteWhatIsNew")]
        public async Task<IActionResult> DeleteWhatIsNew(int whatIsNewId)
        {
            if (whatIsNewId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid What's New ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Call the repository to soft delete the event details
                var result = await _contentManagementRepository.DeleteWhatIsNewAsync(whatIsNewId);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "What's new details deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete what's new profile details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("DownloadDocument")]
        public async Task<IActionResult> DownloadDocument(int whatIsNewId)
        {
            if (whatIsNewId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid What's New ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Fetch the document details by ID
                var documentDetailsList = await _contentManagementRepository.GetWhatIsNewByIdAsync(whatIsNewId);
                var documentDetails = documentDetailsList.FirstOrDefault();

                if (documentDetails == null || string.IsNullOrEmpty(documentDetails.DocumentFile))
                {
                    return NotFound(new { Success = false, Message = "Document not found.", StatusCode = StatusCodes.Status404NotFound });
                }

                // Convert the relative URL path to a full file path
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", documentDetails.DocumentFile.TrimStart('/'));
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { Success = false, Message = "Document file not found.", StatusCode = StatusCodes.Status404NotFound });
                }

                // Get MIME type
                var contentType = "application/octet-stream"; // Default MIME type
                var extension = Path.GetExtension(filePath).ToLowerInvariant();
                var provider = new FileExtensionContentTypeProvider();
                if (provider.TryGetContentType(filePath, out string? resolvedContentType))
                {
                    contentType = resolvedContentType;
                }

                // Return the file for download
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, contentType, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region News & Events Master Page
        [HttpPost("CreateOrUpdateEvent")]
        public async Task<IActionResult> CreateOrUpdateEvent([FromForm] NewsEventsModel newsEvents, [FromForm] IFormFile? thumbnail, [FromForm] IFormFile? featureImage)
        {
            if (newsEvents == null)
            {
                return BadRequest(new { Success = false, Message = "News and Events data cannot be null." });
            }

            try
            {
                // Handle thumbnail upload
                if (thumbnail != null && thumbnail.Length > 0)
                {
                    var thumbnailFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "events", "thumbnails");
                    if (!Directory.Exists(thumbnailFolderPath))
                    {
                        Directory.CreateDirectory(thumbnailFolderPath);
                    }

                    // Generate a unique file name using GUID and original file extension
                    var thumbnailFileName = $"{Guid.NewGuid()}_{Path.GetFileName(thumbnail.FileName)}";
                    var thumbnailFilePath = Path.Combine(thumbnailFolderPath, thumbnailFileName);

                    using (var stream = new FileStream(thumbnailFilePath, FileMode.Create))
                    {
                        await thumbnail.CopyToAsync(stream);
                    }

                    // Save the thumbnail path to the database
                    newsEvents.Thumbnail = $"/assets/events/thumbnails/{thumbnailFileName}";
                }
                else if (string.IsNullOrEmpty(newsEvents.Thumbnail))
                {
                    // If no new thumbnail is uploaded, retain the existing thumbnail path
                    var existingEvent = await _contentManagementRepository.GetEventByIdAsync(newsEvents.EventId!.Value);
                    if (existingEvent != null && existingEvent.Count > 0)
                    {
                        newsEvents.Thumbnail = existingEvent.FirstOrDefault()?.Thumbnail; // Retain the existing thumbnail path
                    }
                }

                // Handle feature image upload
                if (featureImage != null && featureImage.Length > 0)
                {
                    var featureImageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "events", "images");
                    if (!Directory.Exists(featureImageFolderPath))
                    {
                        Directory.CreateDirectory(featureImageFolderPath);
                    }

                    var featureImageFileName = $"{Guid.NewGuid()}_{Path.GetFileName(featureImage.FileName)}";
                    var featureImageFilePath = Path.Combine(featureImageFolderPath, featureImageFileName);

                    using (var stream = new FileStream(featureImageFilePath, FileMode.Create))
                    {
                        await featureImage.CopyToAsync(stream);
                    }

                    // Save the feature image path to the database
                    newsEvents.FeatureImage = $"/assets/events/images/{featureImageFileName}";
                }
                else if (string.IsNullOrEmpty(newsEvents.FeatureImage))
                {
                    var existingEvent = await _contentManagementRepository.GetEventByIdAsync(newsEvents.EventId!.Value);
                    if (existingEvent != null && existingEvent.Count > 0)
                    {
                        newsEvents.FeatureImage = existingEvent.FirstOrDefault()?.FeatureImage; // Retain the existing feature image path
                    }
                }

                // Call repository to create or update event details
                var result = await _contentManagementRepository.CreateOrUpdateEventAsync(newsEvents);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Event details saved successfully.", StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Success = false, Message = "Failed to save event details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _contentManagementRepository.GetEventsAsync();
                if (events != null && events.Any())
                {
                    return Ok(new { Success = true, Data = events, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Events details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetEventById")]
        public async Task<IActionResult> GetEventById(int eventId)
        {
            if (eventId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Event ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var eventDetails = await _contentManagementRepository.GetEventByIdAsync(eventId);
                if (eventDetails != null)
                {
                    return Ok(new { Success = true, Data = eventDetails, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Event details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetEventByName")]
        public async Task<IActionResult> GetEventByName(string eventName)
        {
            try
            {
                var eventdetails = await _contentManagementRepository.GetEventByNameAsync(eventName);
                if (eventdetails != null)
                {
                    return Ok(new { Success = true, Data = eventdetails, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Event details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            if (eventId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Event ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Call the repository to soft delete the event details
                var result = await _contentManagementRepository.DeleteEventAsync(eventId);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Event details deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete event details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region Gallery Master Page
        [HttpPost("CreateOrUpdateGallery")]
        public async Task<IActionResult> CreateOrUpdateGallery([FromForm] GalleryModel gallery, [FromForm] IFormFile? thumbnail, [FromForm] IFormFile? image)
        {
            if (gallery == null)
            {
                return BadRequest(new { Success = false, Message = "Gallery data cannot be null." });
            }

            try
            {
                if (string.IsNullOrEmpty(gallery.GalleryType))
                {
                    return BadRequest(new { Success = false, Message = "GalleryType is required." });
                }

                if (gallery.GalleryType.Equals("Photo", StringComparison.OrdinalIgnoreCase))
                {
                    // Handle thumbnail upload
                    if (thumbnail != null && thumbnail.Length > 0)
                    {
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "gallery", "thumbnails");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(thumbnail.FileName)}";
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await thumbnail.CopyToAsync(stream);
                        }

                        gallery.Thumbnail = $"/assets/gallery/thumbnails/{fileName}";
                    }

                    // Handle gallery image upload
                    if (image != null && image.Length > 0)
                    {
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "gallery", "photos");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        gallery.Image = $"/assets/gallery/photos/{fileName}";
                    }
                }
                else if (gallery.GalleryType.Equals("Video", StringComparison.OrdinalIgnoreCase))
                {
                    // Validate that a video URL is provided
                    if (string.IsNullOrEmpty(gallery.VideoUrl))
                    {
                        return BadRequest(new { Success = false, Message = "Video URL is required for GalleryType 'Video'." });
                    }

                    // Handle thumbnail upload
                    if (thumbnail != null && thumbnail.Length > 0)
                    {
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "gallery", "thumbnails");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(thumbnail.FileName)}";
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await thumbnail.CopyToAsync(stream);
                        }

                        gallery.Thumbnail = $"/assets/gallery/thumbnails/{fileName}";
                    }
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Invalid GalleryType. Accepted values are 'Photo' or 'Video'." });
                }

                // Call repository to create or update the gallery details
                var result = await _contentManagementRepository.CreateOrUpdateGalleryDetailsAsync(gallery);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Gallery details saved successfully.", StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Success = false, Message = "Failed to save gallery details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }        

        [HttpGet("GetGallery")]
        public async Task<IActionResult> GetGallery()
        {
            try
            {
                var galleries = await _contentManagementRepository.GetGalleryAsync();
                if (galleries != null && galleries.Any())
                {
                    return Ok(new { Success = true, Data = galleries, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Gallery details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetGalleryById")]
        public async Task<IActionResult> GetGalleryById(int galleryId)
        {
            if (galleryId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Gallery ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var banner = await _contentManagementRepository.GetGalleryByIdAsync(galleryId);
                if (banner != null)
                {
                    return Ok(new { Success = true, Data = banner, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Gallery Details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetGalleryByName")]
        public async Task<IActionResult> GetGalleryByName(string galleryName)
        {
            try
            {
                var gallery = await _contentManagementRepository.GetGalleryByNameAsync(galleryName);
                if (gallery != null)
                {
                    return Ok(new { Success = true, Data = gallery, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Gallery Details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
            
        [HttpDelete("DeleteGallery")]
        public async Task<IActionResult> DeleteGallery([FromQuery] int galleryId)
        {
            if (galleryId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Gallery ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Call the repository to delete the header menu
                var result = await _contentManagementRepository.DeleteGalleryDetailsAsync(galleryId);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Gallery deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete gallery details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region FAQ Master Page
        [HttpPost("CreateOrUpdateFaq")]
        public async Task<IActionResult> CreateOrUpdateFaq([FromBody] FaqModel faqModel)
        {

            if (faqModel == null)
            {
                return BadRequest(new { Success = false, Message = "FAQ data cannot be null." });
            }

            try
            {
                var result = await _contentManagementRepository.CreateOrUpdateFaqAsync(faqModel);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "FAQ saved successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to save FAQ.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }        

        [HttpGet("GetFaqs")]
        public async Task<IActionResult> GetFaqs()
        {
            try
            {
                var faqs = await _contentManagementRepository.GetFaqsAsync();
                if (faqs != null && faqs.Any())
                {
                    return Ok(new { Success = true, Data = faqs, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "No FAQs found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetFaqById")]
        public async Task<IActionResult> GetFaqById(int faqId)
        {
            if (faqId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid FAQ ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var faqs = await _contentManagementRepository.GetFaqByIdAsync(faqId);
                if (faqs != null && faqs.Any())
                {
                    return Ok(new { Success = true, Data = faqs, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "FAQ not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpDelete("DeleteFaq")]
        public async Task<IActionResult> DeleteFaq([FromBody] FaqModel delFaq)
        {
            if (delFaq?.FaqId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid FAQ ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var result = await _contentManagementRepository.DeleteFaqAsync(delFaq!);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "FAQ deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete FAQ.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion

        #region Contact Details Master Page
        [HttpPost("CreateOrUpdateContact")]
        public async Task<IActionResult> CreateOrUpdateContact([FromForm] ContactDetailsModel contactDetails)
        {
            if (contactDetails == null)
            {
                return BadRequest(new { Success = false, Message = "Contact data cannot be null." });
            }

            try
            {
                // Call repository to create or update event details
                var result = await _contentManagementRepository.CreateOrUpdateContactAsync(contactDetails);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Contact details saved successfully.", StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Success = false, Message = "Failed to save contact details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetContacts")]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var events = await _contentManagementRepository.GetContactsAsync();
                if (events != null && events.Any())
                {
                    return Ok(new { Success = true, Data = events, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Contact details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpGet("GetContactById")]
        public async Task<IActionResult> GetContactById(int contactId)
        {
            if (contactId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Contact ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                var contactDetails = await _contentManagementRepository.GetContactByIdAsync(contactId);
                if (contactDetails != null)
                {
                    return Ok(new { Success = true, Data = contactDetails, StatusCode = StatusCodes.Status200OK });
                }
                return NotFound(new { Success = false, Message = "Contact details are not found.", StatusCode = StatusCodes.Status404NotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        [HttpDelete("DeleteContact")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            if (contactId <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid Contact ID.", StatusCode = StatusCodes.Status400BadRequest });
            }

            try
            {
                // Call the repository to soft delete the event details
                var result = await _contentManagementRepository.DeleteContactAsync(contactId);
                if (result > 0)
                {
                    return Ok(new { Success = true, Message = "Contact details deleted successfully.", StatusCode = StatusCodes.Status200OK });
                }
                return BadRequest(new { Success = false, Message = "Failed to delete contact details.", StatusCode = StatusCodes.Status400BadRequest });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError });
            }
        }
        #endregion        

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
