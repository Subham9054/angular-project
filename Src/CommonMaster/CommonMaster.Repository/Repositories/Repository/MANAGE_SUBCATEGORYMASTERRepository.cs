using System.Collections.Generic;
using CommonMaster.Repository.Factory;
using CommonMaster.Repository.BaseRepository;
using CommonMaster.Repository.Interfaces. MANAGE_SUBCATEGORYMASTER;
using CommonMaster.Repository;

using PHED_CGRC. MANAGE_SUBCATEGORYMASTER;
using Dapper;
using System.Data;
namespace CommonMaster.Repository.Repositories.Interfaces.MANAGE_SUBCATEGORYMASTER
{
	public class MANAGE_SUBCATEGORYMASTERRepository:db_PHED_CGRCRepositoryBase,IMANAGE_SUBCATEGORYMASTERRepository
	{
		public MANAGE_SUBCATEGORYMASTERRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
		 	 public async Task<int> INSERT_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action ", "INSERT");
		p.Add("@SubCategoryId", TBL.SubCategoryId);
		p.Add("@CategoryId", TBL.CategoryId);
		p.Add("@SubCategoryE", TBL.SubCategoryE);
		p.Add("@SubCategoryH", TBL.SubCategoryH);
		p.Add("@ComplaintPriority", TBL.ComplaintPriority);
		p.Add("@EscalationLevel", TBL.EscalationLevel);
		p.Add("@UserId", TBL.UserId);
		p.Add("@CreatedOn", TBL.CreatedOn);
		p.Add("@UpdatedOn", TBL.UpdatedOn);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		p.Add("@SubCategoryIdOld", TBL.SubCategoryIdOld);
		p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
		var results =await Connection.ExecuteAsync("MANAGE_SUBCATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
		int Param = Convert.ToInt32(p.Get<string>("@Msg"));
		return Param;

		}
	 	 public async Task<int> UPDATE_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action ", "UPDATE");
		p.Add("@SubCategoryId", TBL.SubCategoryId);
		p.Add("@CategoryId", TBL.CategoryId);
		p.Add("@SubCategoryE", TBL.SubCategoryE);
		p.Add("@SubCategoryH", TBL.SubCategoryH);
		p.Add("@ComplaintPriority", TBL.ComplaintPriority);
		p.Add("@EscalationLevel", TBL.EscalationLevel);
		p.Add("@UserId", TBL.UserId);
		p.Add("@CreatedOn", TBL.CreatedOn);
		p.Add("@UpdatedOn", TBL.UpdatedOn);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		p.Add("@SubCategoryIdOld", TBL.SubCategoryIdOld);
		p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
		var results =await Connection.ExecuteAsync("MANAGE_SUBCATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
		int Param = Convert.ToInt32(p.Get<string>("@Msg"));
		return Param;

		}
	 	 public async Task<int> DELETE_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action ", "DELETE");
		p.Add("@SubCategoryId", TBL.SubCategoryId);
		p.Add("@CategoryId", TBL.CategoryId);
		p.Add("@SubCategoryE", TBL.SubCategoryE);
		p.Add("@SubCategoryH", TBL.SubCategoryH);
		p.Add("@ComplaintPriority", TBL.ComplaintPriority);
		p.Add("@EscalationLevel", TBL.EscalationLevel);
		p.Add("@UserId", TBL.UserId);
		p.Add("@CreatedOn", TBL.CreatedOn);
		p.Add("@UpdatedOn", TBL.UpdatedOn);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		p.Add("@SubCategoryIdOld", TBL.SubCategoryIdOld);
		p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
		var results =await Connection.ExecuteAsync("MANAGE_SUBCATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
		int Param = Convert.ToInt32(p.Get<string>("@Msg"));
		return Param;

		}
	 public class VIEW_MANAGE_SUBCATEGORYMASTERMapper
		{
		public Dapper.SqlMapper.ITypeMap  VIEW_MANAGE_SUBCATEGORYMASTERmapper()
		{
		var map = new CustomPropertyTypeMap(typeof(VIEWMANAGE_SUBCATEGORYMASTER), (type, columnName) =>
		{
				if(columnName == "SubCategoryId")return type.GetProperty("SubCategoryId");
		if(columnName == "CategoryId")return type.GetProperty("CategoryId");
		if(columnName == "SubCategoryE")return type.GetProperty("SubCategoryE");
		if(columnName == "SubCategoryH")return type.GetProperty("SubCategoryH");
		if(columnName == "ComplaintPriority")return type.GetProperty("ComplaintPriority");
		if(columnName == "EscalationLevel")return type.GetProperty("EscalationLevel");
		if(columnName == "CreatedBy")return type.GetProperty("CreatedBy");
		if(columnName == "UpdatedBy")return type.GetProperty("UpdatedBy");
		if(columnName == "CreatedOn")return type.GetProperty("CreatedOn");
		if(columnName == "UpdatedOn")return type.GetProperty("UpdatedOn");
		if(columnName == "DeletedFlag")return type.GetProperty("DeletedFlag");
		if(columnName == "SubCategoryIdOld")return type.GetProperty("SubCategoryIdOld");
return null;
	});
	return map;
	}
		}	 public async Task<List<VIEWMANAGE_SUBCATEGORYMASTER>> VIEW_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action ", "VIEW");
		p.Add("@SubCategoryId", TBL.SubCategoryId);
		p.Add("@CategoryId", TBL.CategoryId);
		p.Add("@SubCategoryE", TBL.SubCategoryE);
		p.Add("@SubCategoryH", TBL.SubCategoryH);
		p.Add("@ComplaintPriority", TBL.ComplaintPriority);
		p.Add("@EscalationLevel", TBL.EscalationLevel);
		p.Add("@UserId", TBL.UserId);
		p.Add("@CreatedOn", TBL.CreatedOn);
		p.Add("@UpdatedOn", TBL.UpdatedOn);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		p.Add("@SubCategoryIdOld", TBL.SubCategoryIdOld);
		p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
VIEW_MANAGE_SUBCATEGORYMASTERMapper _Mapper = new VIEW_MANAGE_SUBCATEGORYMASTERMapper();
		Dapper.SqlMapper.SetTypeMap(typeof(VIEWMANAGE_SUBCATEGORYMASTER), _Mapper.VIEW_MANAGE_SUBCATEGORYMASTERmapper());
		var results = await Connection.QueryAsync<VIEWMANAGE_SUBCATEGORYMASTER>("MANAGE_SUBCATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		}
	 public class EDIT_MANAGE_SUBCATEGORYMASTERMapper
		{
		public Dapper.SqlMapper.ITypeMap  EDIT_MANAGE_SUBCATEGORYMASTERmapper()
		{
		var map = new CustomPropertyTypeMap(typeof(EDITMANAGE_SUBCATEGORYMASTER), (type, columnName) =>
		{
				if(columnName == "SubCategoryId")return type.GetProperty("SubCategoryId");
		if(columnName == "CategoryId")return type.GetProperty("CategoryId");
		if(columnName == "SubCategoryE")return type.GetProperty("SubCategoryE");
		if(columnName == "SubCategoryH")return type.GetProperty("SubCategoryH");
		if(columnName == "ComplaintPriority")return type.GetProperty("ComplaintPriority");
		if(columnName == "EscalationLevel")return type.GetProperty("EscalationLevel");
		if(columnName == "CreatedBy")return type.GetProperty("CreatedBy");
		if(columnName == "UpdatedBy")return type.GetProperty("UpdatedBy");
		if(columnName == "CreatedOn")return type.GetProperty("CreatedOn");
		if(columnName == "UpdatedOn")return type.GetProperty("UpdatedOn");
		if(columnName == "DeletedFlag")return type.GetProperty("DeletedFlag");
		if(columnName == "SubCategoryIdOld")return type.GetProperty("SubCategoryIdOld");
return null;
	});
	return map;
	}
		}	 public async Task<List<EDITMANAGE_SUBCATEGORYMASTER>> EDIT_MANAGE_SUBCATEGORYMASTER(MANAGE_SUBCATEGORYMASTER_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action ", "EDIT");
		p.Add("@SubCategoryId", TBL.SubCategoryId);
		p.Add("@CategoryId", TBL.CategoryId);
		p.Add("@SubCategoryE", TBL.SubCategoryE);
		p.Add("@SubCategoryH", TBL.SubCategoryH);
		p.Add("@ComplaintPriority", TBL.ComplaintPriority);
		p.Add("@EscalationLevel", TBL.EscalationLevel);
		p.Add("@UserId", TBL.UserId);
		p.Add("@CreatedOn", TBL.CreatedOn);
		p.Add("@UpdatedOn", TBL.UpdatedOn);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		p.Add("@SubCategoryIdOld", TBL.SubCategoryIdOld);
		p.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
EDIT_MANAGE_SUBCATEGORYMASTERMapper _Mapper = new EDIT_MANAGE_SUBCATEGORYMASTERMapper();
		Dapper.SqlMapper.SetTypeMap(typeof(EDITMANAGE_SUBCATEGORYMASTER), _Mapper.EDIT_MANAGE_SUBCATEGORYMASTERmapper());
		var results = await Connection.QueryAsync<EDITMANAGE_SUBCATEGORYMASTER>("MANAGE_SUBCATEGORYMASTER", p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		}
}
}
