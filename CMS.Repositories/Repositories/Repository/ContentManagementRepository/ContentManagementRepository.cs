using CMS.Model;
using CMS.Repository.Factory;
using CMS.Repository.Repositories.Repository.BaseRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository.Repositories.Repository.ContentManagementRepository
{
    public class ContentManagementRepository : db_PHED_CGRCRepositoryBase, IContentManagementRepository
    {
        public ContentManagementRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {
        }

        #region Manage Pages Repository
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
    }
}
