using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config.Model.Entities.Config
{
    public class EscalationDetail
    {
        public int INT_DESIG_ID { get; set; }  // Assuming you have an integer ID for designation
        public int INT_DESIG_LEVELID { get; set; } // Assuming you have an integer ID for location level
        public string VCH_STANDARD_DAYS { get; set; } // Action hours can be a string, adjust accordingly
    }

    public class EscalationModel
    {
        public int INT_CATEGORY_ID { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public int INT_ESCALATION_LEVELID { get; set; }
        //public int INT_DESIG_ID { get; set; }  // Assuming you have an integer ID for designation
        //public int INT_DESIG_LEVELID { get; set; } // Assuming you have an integer ID for location level
        //public string VCH_STANDARD_DAYS { get; set; } // Action hours can be a string, adjust accordingly
        public int INT_CREATED_BY { get; set; }
        public DateTime DTM_CREATED_ON {  get; set; }    
        public List<EscalationDetail> EscalationDetails { get; set; } // Add this property
    }
    public class Escalationinsert 
    {
        public int INT_DESIG_ID { get; set; }  // Assuming you have an integer ID for designation
        public int INT_DESIG_LEVELID { get; set; } // Assuming you have an integer ID for location level
        public string VCH_STANDARD_DAYS { get; set; } // Action hours can be a string, adjust accordingly
        public int INT_CATEGORY_ID { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public int INT_ESCALATION_LEVELID { get; set; }
        public int INT_CREATED_BY { get; set; }
        public DateTime DTM_CREATED_ON { get; set; }
    }
    public class EscalationViewModel
    {
        public string VCH_CATEGORY { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public string nvchDesigName { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public int INT_DESIG_LEVELID { get; set; }
        public int INT_DESIG_ID { get; set; }
        public int INT_ESCALATION_LEVELID { get; set; }
        public string VCH_STANDARD_DAYS { get; set; }
    }
}
