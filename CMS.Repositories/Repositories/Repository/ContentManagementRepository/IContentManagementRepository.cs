using CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository.Repositories.Repository.ContentManagementRepository
{
    public interface IContentManagementRepository
    {
        #region Manage Pages Interface
        Task<int> CreateOrUpdatePageLinkAsync(PageLinkModel creOrUpdPageLink);
        Task<int> DeletePageLinkAsync(int pageId);
        Task<List<PageLinkModel>> GetPageLinkAsync();
        Task<List<PageLinkModel>> GetPageLinkByIdAsync(int pageId);
        Task<List<PageLinkModel>> GetParentMenusAsync();
        Task<PageManagementModel> GetMenuSubmenuAsync();
        #endregion
    }
}
