using Dapper;
using Dropdown.Model;
using Dropdown.Repository.Factory;
using Dropdown.Repository.Repositories.Interfaces;
using Dropdown.Repository.Repositories.Repository.BaseRepository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dropdown.Repository.Repositories.Repository
{
    public class DropdownRepository : db_PHED_CGRCRepositoryBase, IDropdownRepository
    {
        public DropdownRepository(Idb_PHED_CGRCConnectionFactory connectionFactory) : base(connectionFactory)
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
        public async Task<List<Block>> GetBlocks(int distid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@distid", distid);
                    var blocks = await Connection.QueryAsync<Block>("GetActiveBlocksByDistrict", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return blocks.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<GP>> GetGp(int distid, int blockid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@distid", distid);
                    dynamicParameters.Add("@blockid", blockid);
                    var gps = await Connection.QueryAsync<GP>("GetActiveGPByDistrictAndBlock", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return gps.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Village>> Getvillage(int distid, int blockid, int gpid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@distid", distid);
                    dynamicParameters.Add("@blockid", blockid);
                    dynamicParameters.Add("@gpid", gpid);
                    var villages = await Connection.QueryAsync<Village>("GetActiveVillagesByDistrictBlockGP", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return villages.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ComplaintStatus>> GetComplaints()
        {
            try
            {
             
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var complaints = await Connection.QueryAsync<ComplaintStatus>("GetComplaintStatus", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return complaints.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Complaintlogtype>> GetComplaintslogtype()
        {
            try
            {
             
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var complaintslog = await Connection.QueryAsync<Complaintlogtype>("GetComplaintlogtype", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return complaintslog.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
