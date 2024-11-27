using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Model.Entities.GMS
{
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
        public int INT_WARD {  get; set; }
        public string NVCH_ADDRESS { get; set; }
        public string NVCH_COMPLIANT_DETAILS { get; set; }
        public string VCH_COMPLAINT_FILE { get; set; }
        public string VCH_TOKENNO { get; set; }
        public DateTime DTM_CREATED_ON { get; set; } // Assuming it will be provided from frontend or generated in the repository
        public string VCH_EMAIL { get; set; }
        public string NVCH_LANDMARK { get;set; }
        public int INT_COMPLAINT_PRIORITY { get; set; }
    }
    public class gmsComplaintdetails
    {
        public int INT_COMPLIANT_ID { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_TOKENNO { get; set; }
        public string NVCH_COMPLIANTANT_NAME { get; set; }
        public string vchUserName { get; set; }
        public int INT_COMPLAINT_PRIORITY { get; set; }
        public int INT_COMPLIANT_LOG_TYPE { get; set; }
        public DateTime DTM_CREATED_ON { get; set; }
        public string VCH_COMPLIANT_STATUS { get; set; }
        public string NVCH_COMPLIANT_DETAILS { get; set; }
        public string NVCH_LANDMARK { get; set; }
    }

}
