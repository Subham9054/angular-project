using Dropdown.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dropdown.Repository.Repositories.Interfaces
{
    public interface IDropdownRepository
    {
        public Task<List<District>> GetDistricts();
        public Task<List<Block>> GetBlocks(int distid);
        public Task<List<GP>> GetGp(int blockid);
        public Task<List<Village>> Getvillage(int gpid);
        public Task<List<Ward>> Getward(int villageid);
        public Task<List<ComplaintStatus>> GetComplaints();
        public Task<List<Complaintlogtype>> GetComplaintslogtype();
        public Task<List<Category>> GetCategories();
        public Task<List<subCategory>> GetSubCategories(int catid);
        public Task<List<Designation>> getDesignation();
        public Task<List<Location>> getLocation();
        public Task<List<ComplaintPriority>> GetComplaintPriority();
    }
}
