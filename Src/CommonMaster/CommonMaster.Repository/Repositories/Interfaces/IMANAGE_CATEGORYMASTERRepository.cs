using System.Collections.Generic;
using CommonMaster.Model.Entities.CommonMaster;
using PHED_CGRC.MANAGE_CATEGORYMASTER ;

namespace CommonMaster.Repository.Interfaces.MANAGE_CATEGORYMASTER
{
  public interface IMANAGE_CATEGORYMASTERRepository
  {
        public Task<bool> ComplaintCatagory(ComplaintCategory complaintCategory);
        public Task<List<ComplaintCategory>> getCatagory();
        public Task<bool> UpdateComplaintCatagory(int id, ComplaintCategory complaintCategory);
        public Task<int> getdeleteCatagorybyid(int id);

    }
}
