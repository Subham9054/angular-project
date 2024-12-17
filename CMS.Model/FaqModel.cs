using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class FaqModel
    {
        public int? FaqId { get; set; }
        public string? FaqEng { get; set; }
        public string? FaqHin { get; set; }
        public string? FaqAnsEng { get; set; }
        public string? FaqAnsHin { get; set; }
        public int CreatedBy { get; set; }
    }
}
