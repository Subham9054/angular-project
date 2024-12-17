using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class DesignationModel
    {
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public string? AliasName { get; set; }
        public string? UserType { get; set; }
        public int? CreatedBy { get; set; }
    }
}
