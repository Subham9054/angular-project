using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class PageLinkModel
    {
        public int? PageId { get; set; }
        public string? PageNameEng { get; set; }
        public string? PageNameHin { get; set; }
        public string? ParentMenu { get; set; }
        public bool IsSubMenu { get; set; }
        public int? ParentId { get; set; }
        public string? ParentNameEng { get; set; }
        public string? ParentNameHin { get; set; }
        public int? Position { get; set; }
        public bool IsExternal { get; set; }
        public string? URL { get; set; }
        public string? Icon { get; set; }
        public int? CreatedBy { get; set; }
    }
}
