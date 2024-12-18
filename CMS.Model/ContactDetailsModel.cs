using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class ContactDetailsModel
    {
        public int? ContactId { get; set; }
        public string? AddressEnglish { get; set; }
        public string? AddressHindi { get; set; }
        public string? MobileEnglish { get; set; }
        public string? MobileHindi { get; set; }
        public string? Email { get; set; }
        public string? EmbeddedUrl { get; set; }
        public int? CreatedBy { get; set; }
    }
}
