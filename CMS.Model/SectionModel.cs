using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class SectionModel
    {
        public int CircleId { get; set; }
        public int DivisionId { get; set; }
        public int SubDivisionId { get; set; }
        public int SectionId { get; set; }
        public string? SectionName { get; set; }
    }
}
