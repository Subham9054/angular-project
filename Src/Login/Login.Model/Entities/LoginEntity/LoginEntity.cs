using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model.Entities.LoginEntity
{
    public class LoginEntity
    {
        public string? vchUserName { get; set; }
        public string? vchPassWord { get; set; }
        public string? Role { get; set; }
    }
    public class Registration
    {
        public string? vchUserName { get; set; }
        public string? vchPassWord { get; set; }
        public string? vchFullName { get; set; }
        public int intLevelDetailId { get; set; }
        public int intDesignationId { get; set; }
        public string? vchMobTel { get; set; }
        public string? vchEmail { get; set; }
        public string? vchGender { get; set; }
        public int intGroupID { get; set; }
        public string? vchOffTel { get; set; }
        public int bitStatus { get; set; }
        public string? intCreatedBy { get; set; }


    }
}
