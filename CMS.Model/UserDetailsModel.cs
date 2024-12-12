using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class UserDetailsModel
    {
        public int? intUserId { get; set; }
        public string? vchUserName { get; set; }
        public string? vchPassWord { get; set; }
        public string? vchNewPassword { get; set; }
        public int? intCreatedBy { get; set; }
    }
}
