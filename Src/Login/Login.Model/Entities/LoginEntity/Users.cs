using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model.Entities.LoginEntity
{
    public class Users
    {
        public string vchUserName { get; set; }
        public string vchPassWord { get; set; }
        public string Role { get; set; }
        public int intUserId { get; set; }
        public string vchFullName { get; set; }
        public int intIsCmnMst {  get; set; }
        public int intIsConfig {  get; set; }
        public int intIsGms {  get; set; }
        public int intIsMisReport { get; set; }
        public int intIsCms { get; set; }
    }
}
