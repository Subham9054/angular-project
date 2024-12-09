using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class PageManagementDto
    {
        public int? PageId { get; set; }
        public int? ParentId { get; set; }
        public string? MainMenuEng { get; set; }
        public string? MainMenuHin { get; set; }
        public string? SubmenuEng { get; set; }
        public string? SubmenuHin { get; set; }
        public string? URL { get; set; }
    }
}
