using CMS.Model;
using CMS.Repositories.Factory;
using CMS.Repositories.Repositories.BaseRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repositories.Repositories.CmsRepository
{
    public class ContentManagementRepository : PhedRepositoryBase, IContentManagementRepository
    {
        public ContentManagementRepository(IPhedConnectionFactory phedConnectionFactory) : base(phedConnectionFactory)
        {
        }

        #region Manage Pages or Menu Repository
        public async Task<int> CreateOrUpdatePageLinkAsync(PageLinkModel creOrUpdPageLink)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdPageLink.PageId == 0 || creOrUpdPageLink.PageId == null)
                {
                    // Creating a new page link
                    parameters.Add("P_PageId", null, DbType.Int32);
                    parameters.Add("P_PageNameEng", creOrUpdPageLink.PageNameEng);
                    parameters.Add("P_PageNameHin", creOrUpdPageLink.PageNameHin);
                    parameters.Add("P_IsSubMenu", creOrUpdPageLink.IsSubMenu);
                    parameters.Add("P_ParentId", creOrUpdPageLink.ParentId);
                    parameters.Add("P_Position", creOrUpdPageLink.Position);
                    parameters.Add("P_IsExternal", creOrUpdPageLink.IsExternal);
                    parameters.Add("P_URL", creOrUpdPageLink.URL);
                    parameters.Add("P_Icon", creOrUpdPageLink.Icon);
                    parameters.Add("P_CreatedBy", creOrUpdPageLink.CreatedBy);
                }
                else
                {
                    // Updating an existing page link
                    parameters.Add("P_PageId", creOrUpdPageLink.PageId);
                    parameters.Add("P_PageNameEng", creOrUpdPageLink.PageNameEng);
                    parameters.Add("P_PageNameHin", creOrUpdPageLink.PageNameHin);
                    parameters.Add("P_IsSubMenu", creOrUpdPageLink.IsSubMenu);
                    parameters.Add("P_ParentId", creOrUpdPageLink.ParentId);
                    parameters.Add("P_Position", creOrUpdPageLink.Position);
                    parameters.Add("P_IsExternal", creOrUpdPageLink.IsExternal);
                    parameters.Add("P_URL", creOrUpdPageLink.URL);
                    parameters.Add("P_Icon", creOrUpdPageLink.Icon);
                    parameters.Add("P_CreatedBy", creOrUpdPageLink.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ManagePageMenus", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for Page Menu exists.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }        

        public async Task<List<PageLinkModel>> GetPageLinkAsync()
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetAll");
                parameters.Add("P_PageId", null);
                parameters.Add("P_PageNameEng", null);
                parameters.Add("P_PageNameHin", null);
                parameters.Add("P_IsSubMenu", null);
                parameters.Add("P_ParentId", null);
                parameters.Add("P_Position", null);
                parameters.Add("P_IsExternal", null);
                parameters.Add("P_URL", null);
                parameters.Add("P_Icon", null);
                parameters.Add("P_CreatedBy", null);

                // Execute the stored procedure and fetch all page menus
                var result = await Connection.QueryAsync<PageLinkModel>("USP_ManagePageMenus", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the page menu links.", ex);
            }
        }

        public async Task<List<PageLinkModel>> GetPageLinkByIdAsync(int pageId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetById");
                parameters.Add("P_PageId", pageId);
                parameters.Add("P_PageNameEng", null);
                parameters.Add("P_PageNameHin", null);
                parameters.Add("P_IsSubMenu", null);
                parameters.Add("P_ParentId", null);
                parameters.Add("P_Position", null);
                parameters.Add("P_IsExternal", null);
                parameters.Add("P_URL", null);
                parameters.Add("P_Icon", null);
                parameters.Add("P_CreatedBy", null);

                // Execute the stored procedure and get the menu by its ID
                var result = await Connection.QueryAsync<PageLinkModel>("USP_ManagePageMenus", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the header menu by ID.", ex);
            }
        }

        public async Task<int> DeletePageLinkAsync(int pageId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_PageId", pageId);
                parameters.Add("P_PageNameEng", null);
                parameters.Add("P_PageNameHin", null);
                parameters.Add("P_IsSubMenu", null);
                parameters.Add("P_ParentId", null);
                parameters.Add("P_Position", null);
                parameters.Add("P_IsExternal", null);
                parameters.Add("P_URL", null);
                parameters.Add("P_Icon", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_ManagePageMenus", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the page nemu link.", ex);
            }
        }

        public async Task<List<PageLinkModel>> GetParentMenusAsync()
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetParentMenu");
                parameters.Add("P_PageId", null);
                parameters.Add("P_PageNameEng", null);
                parameters.Add("P_PageNameHin", null);
                parameters.Add("P_IsSubMenu", null);
                parameters.Add("P_ParentId", null);
                parameters.Add("P_Position", null);
                parameters.Add("P_IsExternal", null);
                parameters.Add("P_URL", null);
                parameters.Add("P_Icon", null);
                parameters.Add("P_CreatedBy", null);

                // Execute the stored procedure and fetch all page menus
                var result = await Connection.QueryAsync<PageLinkModel>("USP_ManagePageMenus", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the page menu links.", ex);
            }
        }

        public async Task<PageManagementModel> GetMenuSubmenuAsync()
        {
            var parameters = new DynamicParameters();
            var response = new PageManagementModel
            {
                Success = true,
                Data = new List<Page>()
            };

            try
            {
                parameters.Add("P_Action", "GetAllMenuSubmenu");
                parameters.Add("P_PageId", null);
                parameters.Add("P_PageNameEng", null);
                parameters.Add("P_PageNameHin", null);
                parameters.Add("P_IsSubMenu", null);
                parameters.Add("P_ParentId", null);
                parameters.Add("P_Position", null);
                parameters.Add("P_IsExternal", null);
                parameters.Add("P_URL", null);
                parameters.Add("P_Icon", null);
                parameters.Add("P_CreatedBy", null);

                // Execute the stored procedure and fetch all header menus
                var result = await Connection.QueryAsync<PageManagementDto>("USP_ManagePageMenus", parameters, commandType: CommandType.StoredProcedure);

                // Group submenus under their respective main menus
                var groupedData = result
                    .GroupBy(x => new { x.PageId, x.MainMenuEng, x.MainMenuHin, x.URL })
                    .Select(g => new Page
                    {
                        MenuId = g.Key.PageId!.Value,
                        MainMenuEng = g.Key.MainMenuEng,
                        MainMenuHin = g.Key.MainMenuHin,
                        URL = g.Key.URL,
                        Submenus = result
                            .Where(x => x.ParentId == g.Key.PageId) // Assuming you have a ParentId to relate submenus
                            .Select(x => new ChildPage
                            {
                                SubmenuId = x.PageId!.Value, // Ensure it is valid
                                SubmenuEng = x.SubmenuEng,
                                SubmenuHin = x.SubmenuHin,
                                URL = x.URL
                            }).ToList()
                    }).Where(m => !string.IsNullOrEmpty(m.MainMenuEng)).ToList();

                response.Data = groupedData;
            }
            catch (Exception ex)
            {
                response.Success = false;
                // Log the exception details here for further investigation
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }

            return response;
        }
        #endregion        

        #region Banner Master Page Repository
        public async Task<int> CreateOrUpdateBannerDetailsAsync(BannerModel creOrUpdBanner)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdBanner.BannerId == 0)
                {
                    parameters.Add("P_BannerId", null);
                    parameters.Add("P_BannerImage", creOrUpdBanner.BannerImage);
                    parameters.Add("P_BannerHeadingEng", creOrUpdBanner.BannerHeadingEng);
                    parameters.Add("P_BannerHeadingHin", creOrUpdBanner.BannerHeadingHin);
                    parameters.Add("P_BannerContentEng", creOrUpdBanner.BannerContentEng);
                    parameters.Add("P_BannerContentHin", creOrUpdBanner.BannerContentHin);
                    parameters.Add("P_SerialNo", creOrUpdBanner.SerialNo);
                    parameters.Add("P_IsPublish", creOrUpdBanner.IsPublish);
                    parameters.Add("P_CreatedBy", creOrUpdBanner.CreatedBy);
                }
                else
                {
                    parameters.Add("P_BannerId", creOrUpdBanner.BannerId);
                    parameters.Add("P_BannerImage", creOrUpdBanner.BannerImage);
                    parameters.Add("P_BannerHeadingEng", creOrUpdBanner.BannerHeadingEng);
                    parameters.Add("P_BannerHeadingHin", creOrUpdBanner.BannerHeadingHin);
                    parameters.Add("P_BannerContentEng", creOrUpdBanner.BannerContentEng);
                    parameters.Add("P_BannerContentHin", creOrUpdBanner.BannerContentHin);
                    parameters.Add("P_SerialNo", creOrUpdBanner.SerialNo);
                    parameters.Add("P_IsPublish", creOrUpdBanner.IsPublish);
                    parameters.Add("P_CreatedBy", creOrUpdBanner.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ManageBannerDetails", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for Banner.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }        

        public async Task<List<BannerModel>> GetBannersAsync()
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetAll");
                dyParam.Add("P_BannerId", null, DbType.Int32);
                dyParam.Add("P_BannerImage", null, DbType.String);
                dyParam.Add("P_BannerHeadingEng", null, DbType.String);
                dyParam.Add("P_BannerHeadingHin", null, DbType.String);
                dyParam.Add("P_BannerContentEng", null, DbType.String);
                dyParam.Add("P_BannerContentHin", null, DbType.String);
                dyParam.Add("P_SerialNo", null, DbType.Int32);
                dyParam.Add("P_IsPublish", null, DbType.Boolean);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);

                var banners = await Connection.QueryAsync<BannerModel>("USP_ManageBannerDetails", dyParam, commandType: CommandType.StoredProcedure);

                return banners.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching banner. Please try again later.", ex);
            }
        }

        public async Task<List<BannerModel>> GetBannerByIdAsync(int bannerId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetById");
                parameters.Add("P_BannerId", bannerId);
                parameters.Add("P_BannerImage", null, DbType.String);
                parameters.Add("P_BannerHeadingEng", null, DbType.String);
                parameters.Add("P_BannerHeadingHin", null, DbType.String);
                parameters.Add("P_BannerContentEng", null, DbType.String);
                parameters.Add("P_BannerContentHin", null, DbType.String);
                parameters.Add("P_SerialNo", null, DbType.Int32);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<BannerModel>("USP_ManageBannerDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the banner details by ID.", ex);
            }
        }

        public async Task<List<BannerModel>> GetBannerByNameAsync(string bannerName)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetByName");
                parameters.Add("P_BannerId", null, DbType.Int32);
                parameters.Add("P_BannerImage", null, DbType.String);
                parameters.Add("P_BannerHeadingEng", bannerName);
                parameters.Add("P_BannerHeadingHin", null, DbType.String);
                parameters.Add("P_BannerContentEng", null, DbType.String);
                parameters.Add("P_BannerContentHin", null, DbType.String);
                parameters.Add("P_SerialNo", null, DbType.Int32);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<BannerModel>("USP_ManageBannerDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the banner details by Name.", ex);
            }
        }

        public async Task<int> DeleteBannerDetailsAsync(int bannerId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_BannerId", bannerId);
                parameters.Add("P_BannerImage", null);
                parameters.Add("P_BannerHeadingEng", null);
                parameters.Add("P_BannerHeadingHin", null);
                parameters.Add("P_BannerContentEng", null);
                parameters.Add("P_BannerContentHin", null);
                parameters.Add("P_SerialNo", null);
                parameters.Add("P_IsPublish", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_ManageBannerDetails", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the banner details.", ex);
            }
        }
        #endregion

        #region Manage Page Content Repository
        public async Task<int> CreateOrUpdatePageContentAsync(PageContentModel creOrUpdPageContent)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdPageContent.ContentId == 0)
                {
                    // Creating a new page content
                    parameters.Add("P_ContentId", null);
                    parameters.Add("P_PageTitleEnglish", creOrUpdPageContent.PageTitleEnglish);
                    parameters.Add("P_PageTitleHindi", creOrUpdPageContent.PageTitleHindi);
                    parameters.Add("P_PageAlias", creOrUpdPageContent.PageAlias);
                    parameters.Add("P_ReadMore", creOrUpdPageContent.ReadMore);
                    parameters.Add("P_LinkType", creOrUpdPageContent.LinkType);
                    parameters.Add("P_WindowType", creOrUpdPageContent.WindowType);
                    parameters.Add("P_SnippetEnglish", creOrUpdPageContent.SnippetEnglish);
                    parameters.Add("P_SnippetHindi", creOrUpdPageContent.SnippetHindi);
                    parameters.Add("P_ContentEnglish", creOrUpdPageContent.ContentEnglish);
                    parameters.Add("P_ContentHindi", creOrUpdPageContent.ContentHindi);
                    parameters.Add("P_FeatureImage", creOrUpdPageContent.FeatureImage);
                    parameters.Add("P_MetaTitle", creOrUpdPageContent.MetaTitle);
                    parameters.Add("P_MetaKeyword", creOrUpdPageContent.MetaKeyword);
                    parameters.Add("P_MetaDescription", creOrUpdPageContent.MetaDescription);
                    parameters.Add("P_IsPublish", creOrUpdPageContent.IsPublish);
                    parameters.Add("P_PublishDate", creOrUpdPageContent.PublishDate);
                    parameters.Add("P_CreatedBy", creOrUpdPageContent.CreatedBy);
                }
                else
                {
                    // Updating an existing page content
                    parameters.Add("P_ContentId", creOrUpdPageContent.ContentId);
                    parameters.Add("P_PageTitleEnglish", creOrUpdPageContent.PageTitleEnglish);
                    parameters.Add("P_PageTitleHindi", creOrUpdPageContent.PageTitleHindi);
                    parameters.Add("P_PageAlias", creOrUpdPageContent.PageAlias);
                    parameters.Add("P_ReadMore", creOrUpdPageContent.ReadMore);
                    parameters.Add("P_LinkType", creOrUpdPageContent.LinkType);
                    parameters.Add("P_WindowType", creOrUpdPageContent.WindowType);
                    parameters.Add("P_SnippetEnglish", creOrUpdPageContent.SnippetEnglish);
                    parameters.Add("P_SnippetHindi", creOrUpdPageContent.SnippetHindi);
                    parameters.Add("P_ContentEnglish", creOrUpdPageContent.ContentEnglish);
                    parameters.Add("P_ContentHindi", creOrUpdPageContent.ContentHindi);
                    parameters.Add("P_FeatureImage", creOrUpdPageContent.FeatureImage);
                    parameters.Add("P_MetaTitle", creOrUpdPageContent.MetaTitle);
                    parameters.Add("P_MetaKeyword", creOrUpdPageContent.MetaKeyword);
                    parameters.Add("P_MetaDescription", creOrUpdPageContent.MetaDescription);
                    parameters.Add("P_IsPublish", creOrUpdPageContent.IsPublish);
                    parameters.Add("P_PublishDate", creOrUpdPageContent.PublishDate);
                    parameters.Add("P_CreatedBy", creOrUpdPageContent.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ManagePageContentDetails", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for Page Content.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }        

        public async Task<List<PageContentModel>> GetPageContentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PageContentModel>> GetPageContentByIdAsync(int contentId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePageContentAsync(int contentId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_PageTitleEnglish", null);
                parameters.Add("P_PageTitleHindi", null);
                parameters.Add("P_PageAlias", null);
                parameters.Add("P_ReadMore", null);
                parameters.Add("P_LinkType", null);
                parameters.Add("P_WindowType", null);
                parameters.Add("P_SnippetEnglish", null);
                parameters.Add("P_SnippetHindi", null);
                parameters.Add("P_ContentEnglish", null);
                parameters.Add("P_ContentHindi", null);
                parameters.Add("P_FeatureImage", null);
                parameters.Add("P_MetaTitle", null);
                parameters.Add("P_MetaKeyword", null);
                parameters.Add("P_MetaDescription", null);
                parameters.Add("P_IsPublish", null);
                parameters.Add("P_PublishDate", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_ManagePageContentDetails", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the what is new details.", ex);
            }
        }
        #endregion

        #region What is New Master Page Repository
        public async Task<int> CreateOrUpdateWhatIsNewAsync(WhatIsNewModel creOrUpdWhatIsNew)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdWhatIsNew.WhatIsNewId == 0 || creOrUpdWhatIsNew.WhatIsNewId == null)
                {
                    parameters.Add("P_WhatIsNewId", null);
                    parameters.Add("P_TitleEnglish", creOrUpdWhatIsNew.TitleEnglish);
                    parameters.Add("P_TitleHindi", creOrUpdWhatIsNew.TitleHindi);
                    parameters.Add("P_DescriptionEnglish", creOrUpdWhatIsNew.DescriptionEnglish);
                    parameters.Add("P_DescriptionHindi", creOrUpdWhatIsNew.DescriptionHindi);
                    parameters.Add("P_Document", creOrUpdWhatIsNew.DocumentFile);
                    parameters.Add("P_IsPublish", creOrUpdWhatIsNew.IsPublish);
                    parameters.Add("P_PublishDate", creOrUpdWhatIsNew.PublishDate);
                    parameters.Add("P_CreatedBy", creOrUpdWhatIsNew.CreatedBy);
                }
                else
                {
                    parameters.Add("P_WhatIsNewId", creOrUpdWhatIsNew.WhatIsNewId);
                    parameters.Add("P_TitleEnglish", creOrUpdWhatIsNew.TitleEnglish);
                    parameters.Add("P_TitleHindi", creOrUpdWhatIsNew.TitleHindi);
                    parameters.Add("P_DescriptionEnglish", creOrUpdWhatIsNew.DescriptionEnglish);
                    parameters.Add("P_DescriptionHindi", creOrUpdWhatIsNew.DescriptionHindi);
                    parameters.Add("P_Document", creOrUpdWhatIsNew.DocumentFile);
                    parameters.Add("P_IsPublish", creOrUpdWhatIsNew.IsPublish);
                    parameters.Add("P_PublishDate", creOrUpdWhatIsNew.PublishDate);
                    parameters.Add("P_CreatedBy", creOrUpdWhatIsNew.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_WhatIsNewDetails", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for what is new details.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }        

        public async Task<List<WhatIsNewModel>> GetWhatIsNewsAsync()
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetAll");
                dyParam.Add("P_WhatIsNewId", null, DbType.Int32);
                dyParam.Add("P_TitleEnglish", null, DbType.String);
                dyParam.Add("P_TitleHindi", null, DbType.String);
                dyParam.Add("P_DescriptionEnglish", null, DbType.String);
                dyParam.Add("P_DescriptionHindi", null, DbType.String);
                dyParam.Add("P_Document", null, DbType.String);
                dyParam.Add("P_IsPublish", null, DbType.Boolean);
                dyParam.Add("P_PublishDate", null, DbType.DateTime);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);

                var whatIsNews = await Connection.QueryAsync<WhatIsNewModel>("USP_WhatIsNewDetails", dyParam, commandType: CommandType.StoredProcedure);

                return whatIsNews.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching what is new details. Please try again later.", ex);
            }
        }

        public async Task<List<WhatIsNewModel>> GetWhatIsNewByIdAsync(int whatIsNewId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetById");
                parameters.Add("P_WhatIsNewId", whatIsNewId);
                parameters.Add("P_TitleEnglish", null, DbType.String);
                parameters.Add("P_TitleHindi", null, DbType.String);
                parameters.Add("P_DescriptionEnglish", null, DbType.String);
                parameters.Add("P_DescriptionHindi", null, DbType.String);
                parameters.Add("P_Document", null, DbType.String);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_PublishDate", null, DbType.DateTime);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<WhatIsNewModel>("USP_WhatIsNewDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the what is new details by ID.", ex);
            }
        }

        public async Task<List<WhatIsNewModel>> GetWhatIsNewByNameAsync(string whatIsNewName)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetByName");
                parameters.Add("P_WhatIsNewId", null, DbType.Int32);
                parameters.Add("P_TitleEnglish", whatIsNewName);
                parameters.Add("P_TitleHindi", null, DbType.String);
                parameters.Add("P_DescriptionEnglish", null, DbType.String);
                parameters.Add("P_DescriptionHindi", null, DbType.String);
                parameters.Add("P_Document", null, DbType.String);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_PublishDate", null, DbType.DateTime);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<WhatIsNewModel>("USP_WhatIsNewDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the what is new details by Name.", ex);
            }
        }

        public async Task<int> DeleteWhatIsNewAsync(int whatIsNewId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_WhatIsNewId", whatIsNewId);
                parameters.Add("P_TitleEnglish", null);
                parameters.Add("P_TitleHindi", null);
                parameters.Add("P_DescriptionEnglish", null);
                parameters.Add("P_DescriptionHindi", null);
                parameters.Add("P_Document", null);
                parameters.Add("P_IsPublish", null);
                parameters.Add("P_PublishDate", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_WhatIsNewDetails", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the what is new details.", ex);
            }
        }
        #endregion

        #region News & Events Master Page Repository
        public async Task<int> CreateOrUpdateEventAsync(NewsEventsModel creOrUpdEvent)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdEvent.EventId == 0 || creOrUpdEvent.EventId == null)
                {
                    parameters.Add("P_EventId", null);
                    parameters.Add("P_TitleEnglish", creOrUpdEvent.TitleEnglish);
                    parameters.Add("P_TitleHindi", creOrUpdEvent.TitleHindi);
                    parameters.Add("P_DescriptionEnglish", creOrUpdEvent.DescriptionEnglish);
                    parameters.Add("P_DescriptionHindi", creOrUpdEvent.DescriptionHindi);
                    parameters.Add("P_Thumbnail", creOrUpdEvent.Thumbnail);
                    parameters.Add("P_FeatureImage", creOrUpdEvent.FeatureImage);
                    parameters.Add("P_IsPublish", creOrUpdEvent.IsPublish);
                    parameters.Add("P_PublishDate", creOrUpdEvent.PublishDate);
                    parameters.Add("P_CreatedBy", creOrUpdEvent.CreatedBy);
                }
                else
                {
                    parameters.Add("P_EventId", creOrUpdEvent.EventId);
                    parameters.Add("P_TitleEnglish", creOrUpdEvent.TitleEnglish);
                    parameters.Add("P_TitleHindi", creOrUpdEvent.TitleHindi);
                    parameters.Add("P_DescriptionEnglish", creOrUpdEvent.DescriptionEnglish);
                    parameters.Add("P_DescriptionHindi", creOrUpdEvent.DescriptionHindi);
                    parameters.Add("P_Thumbnail", creOrUpdEvent.Thumbnail);
                    parameters.Add("P_FeatureImage", creOrUpdEvent.FeatureImage);
                    parameters.Add("P_IsPublish", creOrUpdEvent.IsPublish);
                    parameters.Add("P_PublishDate", creOrUpdEvent.PublishDate);
                    parameters.Add("P_CreatedBy", creOrUpdEvent.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ManageNewsEventsDetails", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for event details.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<List<NewsEventsModel>> GetEventsAsync()
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetAll");
                dyParam.Add("P_EventId", null, DbType.Int32);
                dyParam.Add("P_TitleEnglish", null, DbType.String);
                dyParam.Add("P_TitleHindi", null, DbType.String);
                dyParam.Add("P_DescriptionEnglish", null, DbType.String);
                dyParam.Add("P_DescriptionHindi", null, DbType.String);
                dyParam.Add("P_Thumbnail", null, DbType.String);
                dyParam.Add("P_FeatureImage", null, DbType.String);
                dyParam.Add("P_IsPublish", null, DbType.Boolean);
                dyParam.Add("P_PublishDate", null, DbType.DateTime);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);

                var banners = await Connection.QueryAsync<NewsEventsModel>("USP_ManageNewsEventsDetails", dyParam, commandType: CommandType.StoredProcedure);

                return banners.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching event details. Please try again later.", ex);
            }
        }

        public async Task<List<NewsEventsModel>> GetEventByIdAsync(int eventId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetById");
                parameters.Add("P_EventId", eventId);
                parameters.Add("P_TitleEnglish", null, DbType.String);
                parameters.Add("P_TitleHindi", null, DbType.String);
                parameters.Add("P_DescriptionEnglish", null, DbType.String);
                parameters.Add("P_DescriptionHindi", null, DbType.String);
                parameters.Add("P_Thumbnail", null, DbType.String);
                parameters.Add("P_FeatureImage", null, DbType.String);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_PublishDate", null, DbType.DateTime);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<NewsEventsModel>("USP_ManageNewsEventsDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the event details by ID.", ex);
            }
        }

        public async Task<List<NewsEventsModel>> GetEventByNameAsync(string eventName)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetByName");
                parameters.Add("P_EventId", null, DbType.Int32);
                parameters.Add("P_TitleEnglish", eventName);
                parameters.Add("P_TitleHindi", null, DbType.String);
                parameters.Add("P_DescriptionEnglish", null, DbType.String);
                parameters.Add("P_DescriptionHindi", null, DbType.String);
                parameters.Add("P_Thumbnail", null, DbType.String);
                parameters.Add("P_FeatureImage", null, DbType.String);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_PublishDate", null, DbType.DateTime);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<NewsEventsModel>("USP_ManageNewsEventsDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the event details by Name.", ex);
            }
        }

        public async Task<int> DeleteEventAsync(int eventId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_EventId", eventId);
                parameters.Add("P_TitleEnglish", null);
                parameters.Add("P_TitleHindi", null);
                parameters.Add("P_DescriptionEnglish", null);
                parameters.Add("P_DescriptionHindi", null);
                parameters.Add("P_Thumbnail", null);
                parameters.Add("P_FeatureImage", null);
                parameters.Add("P_IsPublish", null);
                parameters.Add("P_PublishDate", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_ManageNewsEventsDetails", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the event details.", ex);
            }
        }
        #endregion

        #region Gallery Master Page Repository
        public async Task<int> CreateOrUpdateGalleryDetailsAsync(GalleryModel creOrUpdGallery)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdGallery.GalleryId == 0)
                {
                    parameters.Add("P_GalleryId", null);
                    parameters.Add("P_GalleryCategory", creOrUpdGallery.GalleryCategory);
                    parameters.Add("P_GalleryType", creOrUpdGallery.GalleryType);
                    parameters.Add("P_Thumbnail", creOrUpdGallery.Thumbnail);
                    parameters.Add("P_Image", creOrUpdGallery.Image);
                    parameters.Add("P_VideoUrl", creOrUpdGallery.VideoUrl);
                    parameters.Add("P_CaptionEnglish", creOrUpdGallery.CaptionEnglish);
                    parameters.Add("P_CaptionHindi", creOrUpdGallery.CaptionHindi);
                    parameters.Add("P_SerialNo", creOrUpdGallery.SerialNo);
                    parameters.Add("P_IsPublish", creOrUpdGallery.IsPublish);
                    parameters.Add("P_CreatedBy", creOrUpdGallery.CreatedBy);
                }
                else
                {
                    parameters.Add("P_GalleryId", creOrUpdGallery.GalleryId);
                    parameters.Add("P_GalleryCategory", creOrUpdGallery.GalleryCategory);
                    parameters.Add("P_GalleryType", creOrUpdGallery.GalleryType);
                    parameters.Add("P_Thumbnail", creOrUpdGallery.Thumbnail);
                    parameters.Add("P_Image", creOrUpdGallery.Image);
                    parameters.Add("P_VideoUrl", creOrUpdGallery.VideoUrl);
                    parameters.Add("P_CaptionEnglish", creOrUpdGallery.CaptionEnglish);
                    parameters.Add("P_CaptionHindi", creOrUpdGallery.CaptionHindi);
                    parameters.Add("P_SerialNo", creOrUpdGallery.SerialNo);
                    parameters.Add("P_IsPublish", creOrUpdGallery.IsPublish);
                    parameters.Add("P_CreatedBy", creOrUpdGallery.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ManageGalleryDetails", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for Gallery.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }        

        public async Task<List<GalleryModel>> GetGalleryAsync()
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetAll");
                dyParam.Add("P_GalleryId", null, DbType.Int32);
                dyParam.Add("P_GalleryCategory", null, DbType.String);
                dyParam.Add("P_GalleryType", null, DbType.String);
                dyParam.Add("P_Thumbnail", null, DbType.String);
                dyParam.Add("P_Image", null, DbType.String);
                dyParam.Add("P_VideoUrl", null, DbType.String);
                dyParam.Add("P_CaptionEnglish", null, DbType.String);
                dyParam.Add("P_CaptionHindi", null, DbType.String);
                dyParam.Add("P_SerialNo", null, DbType.Int32);
                dyParam.Add("P_IsPublish", null, DbType.Boolean);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);

                var banners = await Connection.QueryAsync<GalleryModel>("USP_ManageGalleryDetails", dyParam, commandType: CommandType.StoredProcedure);

                return banners.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching gallery. Please try again later.", ex);
            }
        }

        public async Task<List<GalleryModel>> GetGalleryByIdAsync(int galleryId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetById");
                parameters.Add("P_GalleryId", galleryId);
                parameters.Add("P_GalleryCategory", null, DbType.String);
                parameters.Add("P_GalleryType", null, DbType.String);
                parameters.Add("P_Thumbnail", null, DbType.String);
                parameters.Add("P_Image", null, DbType.String);
                parameters.Add("P_VideoUrl", null, DbType.String);
                parameters.Add("P_CaptionEnglish", null, DbType.String);
                parameters.Add("P_CaptionHindi", null, DbType.String);
                parameters.Add("P_SerialNo", null, DbType.Int32);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<GalleryModel>("USP_ManageGalleryDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the gallery details by ID.", ex);
            }
        }

        public async Task<List<GalleryModel>> GetGalleryByNameAsync(string galleryName)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetByName");
                parameters.Add("P_GalleryId", null, DbType.Int32);
                parameters.Add("P_GalleryCategory", null, DbType.String);
                parameters.Add("P_GalleryType", null, DbType.String);
                parameters.Add("P_Thumbnail", null, DbType.String);
                parameters.Add("P_Image", null, DbType.String);
                parameters.Add("P_VideoUrl", null, DbType.String);
                parameters.Add("P_CaptionEnglish", galleryName);
                parameters.Add("P_CaptionHindi", null, DbType.String);
                parameters.Add("P_SerialNo", null, DbType.Int32);
                parameters.Add("P_IsPublish", null, DbType.Boolean);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the banner by its ID
                var result = await Connection.QueryAsync<GalleryModel>("USP_ManageGalleryDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the gallery details by Name.", ex);
            }
        }

        public async Task<int> DeleteGalleryDetailsAsync(int galleryId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_GalleryId", galleryId);
                parameters.Add("P_GalleryCategory", null);
                parameters.Add("P_GalleryType", null);
                parameters.Add("P_Thumbnail", null);
                parameters.Add("P_Image", null);
                parameters.Add("P_VideoUrl", null);
                parameters.Add("P_CaptionEnglish", null);
                parameters.Add("P_CaptionHindi", null);
                parameters.Add("P_SerialNo", null);
                parameters.Add("P_IsPublish", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_ManageGalleryDetails", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the gallery details.", ex);
            }
        }
        #endregion

        #region FAQ Master Page Repository
        public async Task<int> CreateOrUpdateFaqAsync(FaqModel creOrUpdFaq)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");
                if (creOrUpdFaq.FaqId == 0)
                {
                    // Creating a new FAQ
                    parameters.Add("P_FaqEng", creOrUpdFaq.FaqEng);
                    parameters.Add("P_FaqHin", creOrUpdFaq.FaqHin);
                    parameters.Add("P_FaqAnsEng", creOrUpdFaq.FaqAnsEng);
                    parameters.Add("P_FaqAnsHin", creOrUpdFaq.FaqAnsHin);
                    parameters.Add("P_CreatedBy", creOrUpdFaq.CreatedBy);
                    parameters.Add("P_FaqId", null, DbType.Int32);
                    parameters.Add("P_UpdatedBy", null, DbType.Int32);
                }
                else
                {
                    // Updating an existing FAQ
                    parameters.Add("P_FaqId", creOrUpdFaq.FaqId);
                    parameters.Add("P_FaqEng", creOrUpdFaq.FaqEng);
                    parameters.Add("P_FaqHin", creOrUpdFaq.FaqHin);
                    parameters.Add("P_FaqAnsEng", creOrUpdFaq.FaqAnsEng);
                    parameters.Add("P_FaqAnsHin", creOrUpdFaq.FaqAnsHin);
                    parameters.Add("P_UpdatedBy", creOrUpdFaq.CreatedBy);
                    parameters.Add("P_CreatedBy", null, DbType.Int32);
                }

                // Add output parameter for the result
                parameters.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute the stored procedure
                var faq = await Connection.ExecuteAsync("USP_ManageFAQ", parameters, commandType: CommandType.StoredProcedure);

                // Return the result from the output parameter
                return parameters.Get<int>("Result");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }        

        public async Task<List<FaqModel>> GetFaqsAsync()
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetFAQs");
                dyParam.Add("P_FaqId", null, DbType.Int32);
                dyParam.Add("P_FaqEng", null, DbType.String);
                dyParam.Add("P_FaqHin", null, DbType.String);
                dyParam.Add("P_FaqAnsEng", null, DbType.String);
                dyParam.Add("P_FaqAnsHin", null, DbType.String);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);
                dyParam.Add("P_UpdatedBy", null, DbType.Int32);

                // Add output parameter for the result
                dyParam.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute the stored procedure to get all FAQs
                var faqs = await Connection.QueryAsync<FaqModel>("USP_ManageFAQ", dyParam, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value if needed
                int result = dyParam.Get<int>("Result");

                // Return the list of FAQs
                return faqs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching FAQs. Please try again later.", ex);
            }
        }

        public async Task<List<FaqModel>> GetFaqByIdAsync(int faqId)
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetFAQById");
                dyParam.Add("P_FaqId", faqId);
                dyParam.Add("P_FaqEng", null, DbType.String);
                dyParam.Add("P_FaqHin", null, DbType.String);
                dyParam.Add("P_FaqAnsEng", null, DbType.String);
                dyParam.Add("P_FaqAnsHin", null, DbType.String);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);
                dyParam.Add("P_UpdatedBy", null, DbType.Int32);

                // Add output parameter for the result
                dyParam.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute the stored procedure to get the FAQ by ID
                var faqs = await Connection.QueryAsync<FaqModel>("USP_ManageFAQ", dyParam, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value if needed
                int result = dyParam.Get<int>("Result");

                // Return the list of FAQs (could be a single item or an empty list)
                return faqs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching the FAQ. Please try again later.", ex);
            }
        }

        public async Task<int> DeleteFaqAsync(FaqModel delFaqAsy)
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "Delete");
                dyParam.Add("P_FaqId", delFaqAsy.FaqId);
                dyParam.Add("P_FaqEng", null, DbType.String);
                dyParam.Add("P_FaqHin", null, DbType.String);
                dyParam.Add("P_FaqAnsEng", null, DbType.String);
                dyParam.Add("P_FaqAnsHin", null, DbType.String);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);
                dyParam.Add("P_UpdatedBy", delFaqAsy.CreatedBy);

                // Add output parameter for the result
                dyParam.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute the stored procedure to perform the soft delete
                await Connection.ExecuteAsync("USP_ManageFAQ", dyParam, commandType: CommandType.StoredProcedure);

                // Return the result from the output parameter
                return dyParam.Get<int>("@Result");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the FAQ. Please try again later.", ex);
            }
        }
        #endregion

        #region Contact Details Master Page Repository
        public async Task<int> CreateOrUpdateContactAsync(ContactDetailsModel creOrUpdContact)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdContact.ContactId == 0 || creOrUpdContact.ContactId == null)
                {
                    parameters.Add("P_ContactId", null);
                    parameters.Add("P_AddressEnglish", creOrUpdContact.AddressEnglish);
                    parameters.Add("P_AddressHindi", creOrUpdContact.AddressHindi);
                    parameters.Add("P_MobileEnglish", creOrUpdContact.MobileEnglish);
                    parameters.Add("P_MobileHindi", creOrUpdContact.MobileHindi);
                    parameters.Add("P_Email", creOrUpdContact.Email);
                    parameters.Add("P_EmbeddedUrl", creOrUpdContact.EmbeddedUrl);
                    parameters.Add("P_CreatedBy", creOrUpdContact.CreatedBy);
                }
                else
                {
                    parameters.Add("P_ContactId", creOrUpdContact.ContactId);
                    parameters.Add("P_AddressEnglish", creOrUpdContact.AddressEnglish);
                    parameters.Add("P_AddressHindi", creOrUpdContact.AddressHindi);
                    parameters.Add("P_MobileEnglish", creOrUpdContact.MobileEnglish);
                    parameters.Add("P_MobileHindi", creOrUpdContact.MobileHindi);
                    parameters.Add("P_Email", creOrUpdContact.Email);
                    parameters.Add("P_EmbeddedUrl", creOrUpdContact.EmbeddedUrl);
                    parameters.Add("P_CreatedBy", creOrUpdContact.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ManageContactDetails", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for contact details.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }        

        public async Task<List<ContactDetailsModel>> GetContactsAsync()
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetAll");
                dyParam.Add("P_ContactId", null, DbType.Int32);
                dyParam.Add("P_AddressEnglish", null, DbType.String);
                dyParam.Add("P_AddressHindi", null, DbType.String);
                dyParam.Add("P_MobileEnglish", null, DbType.String);
                dyParam.Add("P_MobileHindi", null, DbType.String);
                dyParam.Add("P_Email", null, DbType.String);
                dyParam.Add("P_EmbeddedUrl", null, DbType.String);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);

                var contacts = await Connection.QueryAsync<ContactDetailsModel>("USP_ManageContactDetails", dyParam, commandType: CommandType.StoredProcedure);

                return contacts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching contact details. Please try again later.", ex);
            }
        }

        public async Task<List<ContactDetailsModel>> GetContactByIdAsync(int eventId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetById");
                parameters.Add("P_ContactId", eventId);
                parameters.Add("P_AddressEnglish", null, DbType.String);
                parameters.Add("P_AddressHindi", null, DbType.String);
                parameters.Add("P_MobileEnglish", null, DbType.String);
                parameters.Add("P_MobileHindi", null, DbType.String);
                parameters.Add("P_Email", null, DbType.String);
                parameters.Add("P_EmbeddedUrl", null, DbType.String);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the contact details by its ID
                var result = await Connection.QueryAsync<ContactDetailsModel>("USP_ManageContactDetails", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the contact details by ID.", ex);
            }
        }

        public async Task<int> DeleteContactAsync(int contactId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_ContactId", contactId);
                parameters.Add("P_AddressEnglish", null);
                parameters.Add("P_AddressHindi", null);
                parameters.Add("P_MobileEnglish", null);
                parameters.Add("P_MobileHindi", null);
                parameters.Add("P_Email", null);
                parameters.Add("P_EmbeddedUrl", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_ManageContactDetails", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the contact details.", ex);
            }
        }
        #endregion

        #region Citizen Mobile App API Repository
        public async Task<ComplaintCountsModel> GetComplaintCountsAsync(string mobile)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Mobile", mobile, DbType.String);

                // Execute the stored procedure and retrieve the complaint details
                var result = await Connection.QueryFirstOrDefaultAsync<ComplaintCountsModel>("GetComplaintCountDetails", parameters, commandType: CommandType.StoredProcedure);

                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving complaint details.", ex);
            }
        }

        public async Task<ComplaintDetailsDrillDownModel> GetComplaintDetailsDrillDownAsync(string mobile, int complaintStatusId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Mobile", mobile, DbType.String);
                parameters.Add("P_ComplaintStatusId", complaintStatusId, DbType.Int32);

                // Execute the stored procedure and retrieve the complaint details drill down
                var result = await Connection.QueryFirstOrDefaultAsync<ComplaintDetailsDrillDownModel>("USP_ComplaintDetailsDrillDown", parameters, commandType: CommandType.StoredProcedure);

                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving complaint details.", ex);
            }
        }
        #endregion

        #region Demography Mapping Repository
        public async Task<List<CircleModel>> GetCirclesAsync()
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetCir");
                parameters.Add("P_CircleId", null, DbType.Int32);
                parameters.Add("P_DivisionId", null, DbType.Int32);
                parameters.Add("P_SubDivisionId", null, DbType.Int32);

                var contacts = await Connection.QueryAsync<CircleModel>("USP_DemographyMapping", parameters, commandType: CommandType.StoredProcedure);

                return contacts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching circle details. Please try again later.", ex);
            }
        }

        public async Task<List<DivisionModel>> GetDivisionsAsync(int circleId)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                // Adding circleId to the parameters to pass it to the stored procedure
                parameters.Add("P_CircleId", circleId);
                parameters.Add("P_Action", "GetDiv");
                parameters.Add("P_DivisionId", null, DbType.Int32);
                parameters.Add("P_SubDivisionId", null, DbType.Int32);

                // Execute stored procedure to get data
                var result = await Connection.QueryAsync<DivisionModel>("USP_DemographyMapping", parameters, commandType: CommandType.StoredProcedure);

                // Return the list of results or an empty list if no results are found
                return result.ToList();
            }
            catch (Exception ex)
            {
                // Handle exception and throw with a custom message
                throw new Exception("An unexpected error occurred while fetching division details. Please try again later.", ex);
            }
        }

        public async Task<List<SubDivisionModel>> GetSubDivisionsAsync(int divisionId)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                // Adding divisionId to the parameters to pass it to the stored procedure
                parameters.Add("P_DivisionId", divisionId);
                parameters.Add("P_Action", "GetSubDiv");
                parameters.Add("P_CircleId", null, DbType.Int32);
                parameters.Add("P_SubDivisionId", null, DbType.Int32);

                // Execute stored procedure to get data
                var result = await Connection.QueryAsync<SubDivisionModel>("USP_DemographyMapping", parameters, commandType: CommandType.StoredProcedure);

                // Return the list of results or an empty list if no results are found
                return result.ToList();
            }
            catch (Exception ex)
            {
                // Handle exception and throw with a custom message
                throw new Exception("An unexpected error occurred while fetching sub-division details. Please try again later.", ex);
            }
        }


        public async Task<List<SectionModel>> GetSectionsAsync(int subDivisionId)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                // Adding subDivisionId to the parameters to pass it to the stored procedure
                parameters.Add("P_SubDivisionId", subDivisionId);
                parameters.Add("P_Action", "GetSec");
                parameters.Add("P_CircleId", null, DbType.Int32);
                parameters.Add("P_DivisionId", null, DbType.Int32);

                // Execute stored procedure to get data
                var result = await Connection.QueryAsync<SectionModel>("USP_DemographyMapping", parameters, commandType: CommandType.StoredProcedure);

                // Return the list of results or an empty list if no results are found
                return result.ToList();
            }
            catch (Exception ex)
            {
                // Handle exception and throw with a custom message
                throw new Exception("An unexpected error occurred while fetching section details. Please try again later.", ex);
            }
        }
        #endregion

        #region Manage Designation (User Role Management) Master Page Repository
        public async Task<int> CreateOrUpdateDesignationAsync(DesignationModel creOrUpdDesignation)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "CreOrUpd");

                if (creOrUpdDesignation.DesignationId == 0 || creOrUpdDesignation.DesignationId == null)
                {
                    parameters.Add("P_DesigId", null);
                    parameters.Add("P_DesigName", creOrUpdDesignation.DesignationName);
                    parameters.Add("P_AliasName", creOrUpdDesignation.AliasName);
                    parameters.Add("P_UserType", creOrUpdDesignation.UserType);
                    parameters.Add("P_CreatedBy", creOrUpdDesignation.CreatedBy);
                }
                else
                {
                    parameters.Add("P_DesigId", creOrUpdDesignation.DesignationId);
                    parameters.Add("P_DesigName", creOrUpdDesignation.DesignationName);
                    parameters.Add("P_AliasName", creOrUpdDesignation.AliasName);
                    parameters.Add("P_UserType", creOrUpdDesignation.UserType);
                    parameters.Add("P_CreatedBy", creOrUpdDesignation.CreatedBy);
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ManageDesignation", parameters, commandType: CommandType.StoredProcedure);

                // If no exceptions, return a success code (1 for successful operation)
                return 1;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL exceptions like duplicate entry (error 45000 from stored procedure)
                if (ex.Number == 45000)
                {
                    throw new Exception("Duplicate entry for designation details.");
                }
                throw new Exception("An unexpected SQL error occurred. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<int> DeleteDesignationAsync(int desigId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "Delete");
                parameters.Add("P_DesigId", desigId);
                parameters.Add("P_DesigName", null);
                parameters.Add("P_AliasName", null);
                parameters.Add("P_UserType", null);
                parameters.Add("P_CreatedBy", 1);

                // Execute the stored procedure for deletion
                await Connection.ExecuteAsync("USP_ManageDesignation", parameters, commandType: CommandType.StoredProcedure);

                // Return success code
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the designation details.", ex);
            }
        }

        public async Task<List<DesignationModel>> GetDesignationsAsync()
        {
            DynamicParameters dyParam = new DynamicParameters();
            try
            {
                dyParam.Add("P_Action", "GetAll");
                dyParam.Add("P_DesigId", null, DbType.Int32);
                dyParam.Add("P_DesigName", null, DbType.String);
                dyParam.Add("P_AliasName", null, DbType.String);
                dyParam.Add("P_UserType", null, DbType.String);
                dyParam.Add("P_CreatedBy", null, DbType.Int32);

                var contacts = await Connection.QueryAsync<DesignationModel>("USP_ManageDesignation", dyParam, commandType: CommandType.StoredProcedure);

                return contacts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching designation details. Please try again later.", ex);
            }
        }

        public async Task<List<DesignationModel>> GetDesignationByIdAsync(int desigId)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_Action", "GetById");
                parameters.Add("P_DesigId", desigId);
                parameters.Add("P_DesigName", null, DbType.String);
                parameters.Add("P_AliasName", null, DbType.String);
                parameters.Add("P_UserType", null, DbType.String);
                parameters.Add("P_CreatedBy", null, DbType.Int32);

                // Execute the stored procedure and get the designation by its ID
                var result = await Connection.QueryAsync<DesignationModel>("USP_ManageDesignation", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList(); // Return the result as a list
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the designation details by ID.", ex);
            }
        }

        public async Task<int> ChangePasswordAsync(UserDetailsModel userDetails)
        {
            var parameters = new DynamicParameters();
            try
            {
                parameters.Add("P_UserId", userDetails.intUserId);
                parameters.Add("P_UserName", userDetails.vchUserName);
                parameters.Add("P_OldPassword", userDetails.vchPassWord);
                parameters.Add("P_NewPassword", userDetails.vchNewPassword);
                parameters.Add("P_CreatedBy", userDetails.intCreatedBy);

                // Execute the stored procedure and get the result
                var result = await Connection.ExecuteScalarAsync<int>("USP_ManagePassword", parameters, commandType: CommandType.StoredProcedure);

                // Return the result (1 for success, 0 for failure)
                return result;
            }
            catch (SqlException ex)
            {
                // Handle SQL-specific errors
                throw new Exception("A SQL error occurred while changing the password. Please try again.", ex);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }       
        #endregion
    }
}
