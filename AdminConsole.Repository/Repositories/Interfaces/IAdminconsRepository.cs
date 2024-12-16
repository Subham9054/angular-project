
using AdminConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminConsole.Repository.Repositories.Interfaces
{
    public interface IAdminconsRepository
    {
        public Task<List<District>> GetDistricts();
        public Task<List<Designation>> getDesignation();
    }
}
