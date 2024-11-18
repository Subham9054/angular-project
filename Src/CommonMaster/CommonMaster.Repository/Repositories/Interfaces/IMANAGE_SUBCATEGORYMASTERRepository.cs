using System.Collections.Generic;
using CommonMaster.Model.Entities.CommonMaster;
using PHED_CGRC.MANAGE_SUBCATEGORYMASTER;
namespace CommonMaster.Repository.Interfaces.MANAGE_SUBCATEGORYMASTER
{
    public interface IMANAGE_SUBCATEGORYMASTERRepository
    {
        Task<int> INSERT_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL);

        Task<int> UPDATE_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL);

        Task<int> DELETE_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL);

        Task<List<VIEWMANAGE_SUBCATEGORYMASTER>> VIEW_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL);

        Task<List<EDITMANAGE_SUBCATEGORYMASTER>> EDIT_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL);
        Task<bool> InsertComplaintSubCategory(ComplaintSubCategoryModel complaintSub);
        Task<List<ViewComplaintSubCategoryModel>> viewtComplaintSubCategory(int catid, int subcatid);
        Task<bool> UpdateComplaintSubCategory(int subcatid, UpdateComplaintSubCategoryModel complaintCategory);
        Task<int> getdeleteCatagorybyid(int catid, int subcatid);


    }
}
