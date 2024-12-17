using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class BannerModel
    {
        public int? BannerId { get; set; }
        public string? BannerImage { get; set; }
        public string? BannerHeadingEng { get; set; }
        public string? BannerHeadingHin { get; set; }
        public string? BannerContentEng { get; set; }
        public string? BannerContentHin { get; set; }
        public int SerialNo { get; set; }
        public bool IsPublish { get; set; }
        public int? CreatedBy { get; set; }
    }
}
