using System.Collections.Generic;
using CommonMaster.Repository.Factory;
using CommonMaster.Repository.BaseRepository;
using CommonMaster.Repository.Interfaces.MANAGE_CATEGORYMASTER;
using CommonMaster.Repository;

using PHED_CGRC.MANAGE_CATEGORYMASTER;
using Dapper;
using System.Data;
namespace CommonMaster.Repository.Repositories.Interfaces.MANAGE_CATEGORYMASTER
{
    public class MANAGE_CATEGORYMASTERRepository : db_PHED_CGRCRepositoryBase, IMANAGE_CATEGORYMASTERRepository
    {
        public MANAGE_CATEGORYMASTERRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
        public async Task<int> INSERT_MANAGE_CATEGORYMASTER(MANAGE_CATEGORYMASTER_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action ", "INSERT");
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@CategoryE", TBL.CategoryE);
            p.Add("@CategoryH", TBL.CategoryH);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@UserId", TBL.UserId);
            p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            var results = await Connection.ExecuteAsync("MANAGE_CATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
            int Param = Convert.ToInt32(p.Get<string>("@Msg"));
            return Param;

        }
        public async Task<int> UPDAE_MANAGE_CATEGORYMASTER(MANAGE_CATEGORYMASTER_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action ", "UPDAE");
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@CategoryE", TBL.CategoryE);
            p.Add("@CategoryH", TBL.CategoryH);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@UserId", TBL.UserId);
            p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            var results = await Connection.ExecuteAsync("MANAGE_CATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
            int Param = Convert.ToInt32(p.Get<string>("@Msg"));
            return Param;

        }
        public async Task<int> DELETE_MANAGE_CATEGORYMASTER(MANAGE_CATEGORYMASTER_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action ", "DELETE");
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@CategoryE", TBL.CategoryE);
            p.Add("@CategoryH", TBL.CategoryH);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@UserId", TBL.UserId);
            p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            var results = await Connection.ExecuteAsync("MANAGE_CATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
            int Param = Convert.ToInt32(p.Get<string>("@Msg"));
            return Param;

        }
        public class VIEW_MANAGE_CATEGORYMASTERMapper
        {
            public Dapper.SqlMapper.ITypeMap VIEW_MANAGE_CATEGORYMASTERmapper()
            {
                var map = new CustomPropertyTypeMap(typeof(VIEWMANAGE_CATEGORYMASTER), (type, columnName) =>
                {
                    if (columnName == "CategoryId") return type.GetProperty("CategoryId");
                    if (columnName == "CategoryE") return type.GetProperty("CategoryE");
                    if (columnName == "CategoryH") return type.GetProperty("CategoryH");
                    if (columnName == "CreatedBy") return type.GetProperty("CreatedBy");
                    if (columnName == "CreatedOn") return type.GetProperty("CreatedOn");
                    if (columnName == "UpdatedBy") return type.GetProperty("UpdatedBy");
                    if (columnName == "UpdatedOn") return type.GetProperty("UpdatedOn");
                    if (columnName == "DeletedFlag") return type.GetProperty("DeletedFlag");
                    return null;
                });
                return map;
            }
        }
        public async Task<List<VIEWMANAGE_CATEGORYMASTER>> VIEW_MANAGE_CATEGORYMASTER(MANAGE_CATEGORYMASTER_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action ", "VIEW");
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@CategoryE", TBL.CategoryE);
            p.Add("@CategoryH", TBL.CategoryH);
            p.Add("@UserId", TBL.UserId);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            
            p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            VIEW_MANAGE_CATEGORYMASTERMapper _Mapper = new VIEW_MANAGE_CATEGORYMASTERMapper();
            Dapper.SqlMapper.SetTypeMap(typeof(VIEWMANAGE_CATEGORYMASTER), _Mapper.VIEW_MANAGE_CATEGORYMASTERmapper());
            var results = await Connection.QueryAsync<VIEWMANAGE_CATEGORYMASTER>("MANAGE_CATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
            return results.ToList();

        }
        public class EDIT_MANAGE_CATEGORYMASTERMapper
        {
            public Dapper.SqlMapper.ITypeMap EDIT_MANAGE_CATEGORYMASTERmapper()
            {
                var map = new CustomPropertyTypeMap(typeof(EDITMANAGE_CATEGORYMASTER), (type, columnName) =>
                {
                    if (columnName == "CategoryId") return type.GetProperty("CategoryId");
                    if (columnName == "CategoryE") return type.GetProperty("CategoryE");
                    if (columnName == "CategoryH") return type.GetProperty("CategoryH");
                    if (columnName == "CreatedBy") return type.GetProperty("CreatedBy");
                    if (columnName == "CreatedOn") return type.GetProperty("CreatedOn");
                    if (columnName == "UpdatedBy") return type.GetProperty("UpdatedBy");
                    if (columnName == "UpdatedOn") return type.GetProperty("UpdatedOn");
                    if (columnName == "DeletedFlag") return type.GetProperty("DeletedFlag");
                    return null;
                });
                return map;
            }
        }
        public async Task<List<EDITMANAGE_CATEGORYMASTER>> EDIT_MANAGE_CATEGORYMASTER(MANAGE_CATEGORYMASTER_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action ", "EDIT");
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@CategoryE", TBL.CategoryE);
            p.Add("@CategoryH", TBL.CategoryH);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@UserId", TBL.UserId);
            p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            EDIT_MANAGE_CATEGORYMASTERMapper _Mapper = new EDIT_MANAGE_CATEGORYMASTERMapper();
            Dapper.SqlMapper.SetTypeMap(typeof(EDITMANAGE_CATEGORYMASTER), _Mapper.EDIT_MANAGE_CATEGORYMASTERmapper());
            var results = await Connection.QueryAsync<EDITMANAGE_CATEGORYMASTER>("MANAGE_CATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
            return results.ToList();

        }
    }
}
