using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class PageManagementModel
    {
        public bool Success { get; set; }
        public List<Page>? Data { get; set; }
    }

    public class Page
    {
        public int MenuId { get; set; }
        public string? MainMenuEng { get; set; }
        public string? MainMenuHin { get; set; }
        public string? URL { get; set; }
        public List<ChildPage>? Submenus { get; set; }
    }

    public class ChildPage
    {
        public int SubmenuId { get; set; }
        public string? SubmenuEng { get; set; }
        public string? SubmenuHin { get; set; }
        public string? URL { get; set; }
    }
}
