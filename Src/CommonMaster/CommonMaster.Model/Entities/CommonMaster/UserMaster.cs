using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMaster.Model.Entities.CommonMaster
{
    public class UserMaster
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public int LevelDetailId { get; set; }
        public int DesignationId { get; set; }
        public string PreAddress { get; set; }
        public string OffcTel { get; set; }
        public string MobTel { get; set; }
        public string UserEmail { get; set; }
        public int RaUserId { get; set; }
        public bool AdminPrevil { get; set; }
        public int LocationId { get; set; }
        public string Gender { get; set; }
        public string vchUserImage { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool bitStatus { get; set; }
        public int GroupId { get; set; }
    }
}
