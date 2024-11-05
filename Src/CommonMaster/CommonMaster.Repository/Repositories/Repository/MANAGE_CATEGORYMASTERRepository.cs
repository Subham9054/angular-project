using System.Collections.Generic;
using CommonMaster.Repository.Factory;
using CommonMaster.Repository.BaseRepository;
using CommonMaster.Repository.Interfaces.MANAGE_CATEGORYMASTER;
using CommonMaster.Repository;

using PHED_CGRC.MANAGE_CATEGORYMASTER;
using Dapper;
using System.Data;
using CommonMaster.Model.Entities.CommonMaster;
using MySqlConnector;
namespace CommonMaster.Repository.Repositories.Interfaces.MANAGE_CATEGORYMASTER
{
    public class MANAGE_CATEGORYMASTERRepository : db_PHED_CGRCRepositoryBase, IMANAGE_CATEGORYMASTERRepository
    {
        public MANAGE_CATEGORYMASTERRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
        public async Task<bool> ComplaintCatagory(ComplaintCategory complaintCategory)
        {
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@vchcategory", complaintCategory.VCH_CATEGORY);
                    dynamicParameters.Add("@nvchcategory", complaintCategory.NVCH_CATEGORY);
                    dynamicParameters.Add("@createdby", 1);
                    dynamicParameters.Add("@createdon", DateTime.Now);
                    dynamicParameters.Add("@action", "CI");
                    var result = await Connection.QueryAsync<int>("USP_ComplaintCategory", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return result.Contains(1);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<List<ComplaintCategory>> getCatagory()
        {
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@action", "CG");
                    var categories = await Connection.QueryAsync<ComplaintCategory>("USP_getallComplaintCategory", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return categories.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<int> getdeleteCatagorybyid(int id)
        {
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@id", id);

                    // Execute the stored procedure and retrieve the affected row count
                    var result = await Connection.ExecuteScalarAsync<int>("USP_deleteComplaintCategorybyid", dynamicParameters, commandType: CommandType.StoredProcedure);

                    return result;  // This will return the number of rows affected (e.g., 1 for success)
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<bool> UpdateComplaintCatagory(int id, ComplaintCategory complaintCategory)
        {
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@id", id);
                    dynamicParameters.Add("@vchcategory", complaintCategory.VCH_CATEGORY);
                    dynamicParameters.Add("@nvchcategory", complaintCategory.NVCH_CATEGORY);
                    dynamicParameters.Add("@updatedby", 1);
                    dynamicParameters.Add("@updatedon", DateTime.Now);
                    dynamicParameters.Add("@action", "CU");
                    var result = await Connection.QueryAsync<int>("USP_UpdateComplaintCategory", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return result.Contains(1);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
