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
                    parameters.Add("P_OpenWindow", creOrUpdPageContent.OpenWindow);
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
                    parameters.Add("P_OpenWindow", creOrUpdPageContent.OpenWindow);
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
            throw new NotImplementedException();
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
