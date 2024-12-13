using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class WhatIsNewModel
    {
        public int? WhatIsNewId { get; set; }
        public string? TitleEnglish { get; set; }
        public string? TitleHindi { get; set; }
        public string? DescriptionEnglish { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? Document { get; set; }
        public bool IsPublish { get; set; }
        public DateTime PublishDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
