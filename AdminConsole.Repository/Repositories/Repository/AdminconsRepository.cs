using AdminConsole.Repository.Factory;
using AdminConsole.Repository.Repositories.Interfaces;

using AdminConsole.Repository.Repositories.Repository.BaseRepository;
using MySql.Data.MySqlClient;
using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminConsole.Model;

namespace AdminConsole.Repository.Repositories.Repository
{
    public class AdminconsRepository : db_PHED_CGRCRepositoryBase, IAdminconsRepository
    {
        public AdminconsRepository(Idb_PHED_CGRCConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public async Task<List<District>> GetDistricts()
        {
            try
            {

                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var districts = await Connection.QueryAsync<District>("GetActiveDistricts", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return districts.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Designation>> getDesignation()
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var designations = await Connection.QueryAsync<Designation>("GetDesignation", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return designations.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }






    }
   
}
