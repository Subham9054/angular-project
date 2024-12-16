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
        #region Manage Pages or Menu Interface
        Task<int> CreateOrUpdatePageLinkAsync(PageLinkModel creOrUpdPageLink);
        Task<int> DeletePageLinkAsync(int pageId);
        Task<List<PageLinkModel>> GetPageLinkAsync();
        Task<List<PageLinkModel>> GetPageLinkByIdAsync(int pageId);
        Task<List<PageLinkModel>> GetParentMenusAsync();
        Task<PageManagementModel> GetMenuSubmenuAsync();
        #endregion

        #region Manage Page Content Interface
        Task<int> CreateOrUpdatePageContentAsync(PageContentModel creOrUpdPageContent);
        Task<int> DeletePageContentAsync(int contentId);
        Task<List<PageContentModel>> GetPageContentsAsync();
        Task<List<PageContentModel>> GetPageContentByIdAsync(int contentId);
        #endregion

        #region Banner Master Page Interface
        Task<int> CreateOrUpdateBannerDetailsAsync(BannerModel creOrUpdBanner);
        Task<int> DeleteBannerDetailsAsync(int bannerId);
        Task<List<BannerModel>> GetBannersAsync();
        Task<List<BannerModel>> GetBannerByIdAsync(int bannerId);
        Task<List<BannerModel>> GetBannerByNameAsync(string bannerName);
        #endregion

        #region What is New Master Page Interface
        Task<int> CreateOrUpdateWhatIsNewAsync(WhatIsNewModel creOrUpdWhatIsNew);
        Task<int> DeleteWhatIsNewAsync(int whatIsNewId);
        Task<List<WhatIsNewModel>> GetWhatIsNewsAsync();
        Task<List<WhatIsNewModel>> GetWhatIsNewByIdAsync(int whatIsNewId);
        Task<List<WhatIsNewModel>> GetWhatIsNewByNameAsync(string whatIsNewName);
        #endregion

        #region News & Events Master Page Interface
        Task<int> CreateOrUpdateEventAsync(NewsEventsModel creOrUpdEvent);
        Task<int> DeleteEventAsync(int eventId);
        Task<List<NewsEventsModel>> GetEventsAsync();
        Task<List<NewsEventsModel>> GetEventByIdAsync(int eventId);
        Task<List<NewsEventsModel>> GetEventByNameAsync(string eventName);
        #endregion

        #region Contact Details Master Page Interface
        Task<int> CreateOrUpdateContactAsync(ContactDetailsModel creOrUpdContact);
        Task<int> DeleteContactAsync(int contactId);
        Task<List<ContactDetailsModel>> GetContactsAsync();
        Task<List<ContactDetailsModel>> GetContactByIdAsync(int eventId);
        #endregion

        #region Citizen Mobile App API Interface
        Task<ComplaintCountsModel> GetComplaintCountsAsync(string mobile);
        Task<ComplaintDetailsDrillDownModel> GetComplaintDetailsDrillDownAsync(string mobile, int complaintStatusId);
        #endregion

        #region Demography Mapping Interface
        Task<List<CircleModel>> GetCirclesAsync();        
        Task<List<DivisionModel>> GetDivisionsAsync(int circleId);
        Task<List<SubDivisionModel>> GetSubDivisionsAsync(int divisionId);
        Task<List<SectionModel>> GetSectionsAsync(int subDivisionId);
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
