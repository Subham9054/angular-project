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

        public async Task<List<PageContentModel>> GetPageContentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PageContentModel>> GetPageContentByIdAsync(int contentId)
        {
            throw new NotImplementedException();
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
