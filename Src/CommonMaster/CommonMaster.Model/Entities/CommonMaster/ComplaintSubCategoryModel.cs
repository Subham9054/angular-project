using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMaster.Model.Entities.CommonMaster
{
    public class ComplaintSubCategoryModel
    {
        public string VCH_CATEGORY { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string NVCH_SUB_CATEGORY { get; set; }
        public int INT_ESCALATION_LEVEL { get; set; }
        public int INT_COMPLAINT_PRIORITY { get; set; }
        public int INT_CREATED_BY { get; set; }
        public DateTime DTM_CREATED_ON { get; set; }
        public string VCH_COMPLIANT_PRORITY { get; set; }

    }
}
