using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class ComplaintCountsModel
    {
        public string? Mobile { get; set; }
        public int? RegisteredOpen { get; set; }
        public int? Inprogress { get; set; }
        public int? Resolved { get; set; }
        public int? Discarded { get; set; }
        public int? RegisteredToBePublished { get; set; }
        public int? Escalated { get; set; }
        public int? NonResolvable { get; set; }
        public int? Reopen { get; set; }
        public int? RejectedByCallCenterExecutive { get; set; }
        public int? Forwarded { get; set; }
    }
}
