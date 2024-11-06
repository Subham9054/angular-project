using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMaster.Model.Entities.CommonMaster
{
    public class ComplaintCategory
    {
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
        public string NVCH_CATEGORY { get; set; }
        public int INT_CREATED_BY { get; set; }
        public int INT_UPDATED_BY { get; set; }
        public int DTM_CREATED_ON { get; set; }
        public int DTM_UPDATED_ON { get; set; }
        public int INT_CATEGORY_ID_OLD { get; set; }

    }
}
