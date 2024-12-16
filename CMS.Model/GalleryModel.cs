using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class GalleryModel
    {
        public int? GalleryId { get; set; }
        public string? GalleryCategory { get; set; }
        public string? GalleryType { get; set; }
        public string? Thumbnail { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? VideoUrl { get; set; }
        public string? CaptionEnglish { get; set; }
        public string? CaptionHindi { get; set; }
        public int SerialNo { get; set; }
        public bool IsPublish { get; set; }
        public int? CreatedBy { get; set; }
    }
}
