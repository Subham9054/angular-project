using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model.Entities.LoginEntity
{
    public class Users
    {
        
        public string? vchUserName { get; set; }
        public string? vchPassWord { get; set; }
        public string Role { get; set; } = null;
    }
}
