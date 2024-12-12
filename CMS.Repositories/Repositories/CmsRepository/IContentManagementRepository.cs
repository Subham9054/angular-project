using CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repositories.Repositories.CmsRepository
{
    public interface IContentManagementRepository
    {

        #region Manage Page Content Interface
        Task<int> CreateOrUpdatePageContentAsync(PageContentModel creOrUpdPageContent);
        Task<int> DeletePageContentAsync(int contentId);
        Task<List<PageContentModel>> GetPageContentsAsync();
        Task<List<PageContentModel>> GetPageContentByIdAsync(int contentId);
        #endregion

        #region Citizen Mobile App API Interface
        Task<ComplaintCountsModel> GetComplaintCountsAsync(string mobile);
        Task<ComplaintDetailsDrillDownModel> GetComplaintDetailsDrillDownAsync(string mobile, int complaintStatusId);
        #endregion

        #region Manage Designation (User Role Management) Master Page Interface
        Task<int> CreateOrUpdateDesignationAsync(DesignationModel creOrUpdDesignation);
        Task<int> DeleteDesignationAsync(int desigId);
        Task<List<DesignationModel>> GetDesignationsAsync();
        Task<List<DesignationModel>> GetDesignationByIdAsync(int desigId);

        Task<int> ChangePasswordAsync(UserDetailsModel userDetails);
        #endregion
    }
}
