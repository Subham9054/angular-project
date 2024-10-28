using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dropdown.Model
{
    public class Dropdown
    {
    }
    public class Complaint
    {
        public int INT_CATEGORY_ID { get; set; }
        public int INT_COMPLIANT_LOG_TYPE { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_CONTACT_NO { get; set; }
        public string NVCH_COMPLIANTANT_NAME { get; set; }
        public int INT_DIST_ID { get; set; }
        public int INT_BLOCK { get; set; }
        public int INT_PANCHAYAT { get; set; }
        public int INT_VILLAGE { get; set; }
        public string NVCH_ADDRESS { get; set; }
        public string NVCH_COMPLIANT_DETAILS { get; set; }
        public string VCH_COMPLAINT_FILE { get; set; }
        public string VCH_TOKENNO { get; set; }
        public DateTime DTM_CREATED_ON { get; set; } // Assuming it will be provided from frontend or generated in the repository
    }
    public class District
    {
        public int INT_DIST_ID { get; set; }
        public string VCH_DIST_NAME { get; set; }
    }
    public class Block
    {
        public int INT_BLOCK_ID { get; set; }
        public string VCH_BLOCK_NAME { get; set; }
    }
    public class GP
    {
        public int INT_GP_ID { get; set; }
        public string VCH_GP_NAME { get; set; }
    }
    public class Village
    {
        public int INT_VILLAGE_ID { get; set; }
        public string VCH_VILLAGE_NAME { get; set; }
    }
    public class Category
    {
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
    }
    public class subCategory
    {
        public string INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
    }
    public class ComplaintStatus
    {
        public int INT_COMPLIANT_STATUS_ID { get; set; }
        public string VCH_COMPLIANT_STATUS { get; set; }
    }
    public class Complaintlogtype
    {
        public int INT_COMPLIANT_LOG_TYPE_ID { get; set; }
        public string VCH_COMPLIANT_LOG_TYPE { get; set; }
    }
}
