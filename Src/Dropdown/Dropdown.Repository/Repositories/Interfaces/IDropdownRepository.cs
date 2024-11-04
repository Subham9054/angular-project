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
        public Task<List<GP>> GetGp(int distid, int blockid);
        public Task<List<Village>> Getvillage(int distid, int blockid, int gpid);
        public Task<List<ComplaintStatus>> GetComplaints();
        public Task<List<Complaintlogtype>> GetComplaintslogtype();
    }
}
