using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class ComplaintDetailsDrillDownModel
    {
        public int CompliantId { get; set; }
        public string? TokenNo { get; set; }
        public string? CompliantName { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? CompliantFile { get; set; }
        public string? Address { get; set; }
        public string? Landmark { get; set; }
        public int? WardId { get; set; }
        public string? Ward { get; set; }
        public int? VillageId { get; set; }
        public string? Village { get; set; }
        public int? PanchayatId { get; set; }
        public string? Panchayat { get; set; }
        public int? BlockId { get; set; }
        public string? Block { get; set; }
        public int? DistrictId { get; set; }
        public string? District { get; set; }
        public DateTime? ComplaintDate { get; set; }
        public string? ComplaintDetails { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public int? ComplaintLogId { get; set; }
        public string? ComplaintLogtype { get; set; }
        public string? ComplaintPriority { get; set; }
        public string? ComplaintStatus { get; set; }
    }
}
