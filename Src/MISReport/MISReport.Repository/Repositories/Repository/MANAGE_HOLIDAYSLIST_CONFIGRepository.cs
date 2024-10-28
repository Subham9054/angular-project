using System.Collections.Generic;
using MISReport.Repository.Factory;
using MISReport.Repository.BaseRepository;
using MISReport.Repository.Interfaces.MANAGE_HOLIDAYSLIST_CONFIG;
using MISReport.Repository;

using PHED_CGRC.MANAGE_HOLIDAYSLIST_CONFIG;
using Dapper;
using System.Data;
namespace MISReport.Repository.Repositories.Interfaces.MANAGE_HOLIDAYSLIST_CONFIG
{
	public class MANAGE_HOLIDAYSLIST_CONFIGRepository:db_PHED_CGRCRepositoryBase,IMANAGE_HOLIDAYSLIST_CONFIGRepository
	{
		public MANAGE_HOLIDAYSLIST_CONFIGRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
		 	 public async Task<int> INSERT_MANAGE_HOLIDAYSLIST_CONFIG(MANAGE_HOLIDAYSLIST_CONFIG_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action", "INSERT");
		p.Add("@HolidayId", TBL.HolidayId);
		p.Add("@FromDate", TBL.FromDate);
		p.Add("@ToDate", TBL.ToDate);
		p.Add("@TotalDays", TBL.TotalDays);
		p.Add("@HolidayNameE", TBL.HolidayNameE);
		p.Add("@HolidayNameH", TBL.HolidayNameH);
		p.Add("@Description", TBL.Description);
		p.Add("@CreatedBy", TBL.CreatedBy);
		p.Add("@UpdatedBy", TBL.UpdatedBy);
		p.Add("@UserId", TBL.UserId);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		var results =await Connection.ExecuteAsync("MANAGE_HOLIDAYSLIST_CONFIG", p, commandType: CommandType.StoredProcedure);
		return results;

		}
	 	 public async Task<int> UPDATE_MANAGE_HOLIDAYSLIST_CONFIG(MANAGE_HOLIDAYSLIST_CONFIG_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action", "UPDATE");
		p.Add("@HolidayId", TBL.HolidayId);
		p.Add("@FromDate", TBL.FromDate);
		p.Add("@ToDate", TBL.ToDate);
		p.Add("@TotalDays", TBL.TotalDays);
		p.Add("@HolidayNameE", TBL.HolidayNameE);
		p.Add("@HolidayNameH", TBL.HolidayNameH);
		p.Add("@Description", TBL.Description);
		p.Add("@CreatedBy", TBL.CreatedBy);
		p.Add("@UpdatedBy", TBL.UpdatedBy);
		p.Add("@UserId", TBL.UserId);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		var results =await Connection.ExecuteAsync("MANAGE_HOLIDAYSLIST_CONFIG", p, commandType: CommandType.StoredProcedure);
		return results;

		}
	 	 public async Task<int> DELETE_MANAGE_HOLIDAYSLIST_CONFIG(MANAGE_HOLIDAYSLIST_CONFIG_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action", "DELETE");
		p.Add("@HolidayId", TBL.HolidayId);
		p.Add("@FromDate", TBL.FromDate);
		p.Add("@ToDate", TBL.ToDate);
		p.Add("@TotalDays", TBL.TotalDays);
		p.Add("@HolidayNameE", TBL.HolidayNameE);
		p.Add("@HolidayNameH", TBL.HolidayNameH);
		p.Add("@Description", TBL.Description);
		p.Add("@CreatedBy", TBL.CreatedBy);
		p.Add("@UpdatedBy", TBL.UpdatedBy);
		p.Add("@UserId", TBL.UserId);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
		var results =await Connection.ExecuteAsync("MANAGE_HOLIDAYSLIST_CONFIG", p, commandType: CommandType.StoredProcedure);
		return results;

		}
	 public class VIEW_MANAGE_HOLIDAYSLIST_CONFIGMapper
		{
		public Dapper.SqlMapper.ITypeMap  VIEW_MANAGE_HOLIDAYSLIST_CONFIGmapper()
		{
		var map = new CustomPropertyTypeMap(typeof(VIEWMANAGE_HOLIDAYSLIST_CONFIG), (type, columnName) =>
		{
				if(columnName == "HolidayId")return type.GetProperty("HolidayId");
		if(columnName == "FromDate")return type.GetProperty("FromDate");
		if(columnName == "ToDate")return type.GetProperty("ToDate");
		if(columnName == "TotalDays")return type.GetProperty("TotalDays");
		if(columnName == "HolidayName")return type.GetProperty("HolidayName");
		if(columnName == "HolidayName_NV")return type.GetProperty("HolidayName_NV");
		if(columnName == "Description")return type.GetProperty("Description");
		if(columnName == "CreatedBy")return type.GetProperty("CreatedBy");
		if(columnName == "UpdatedBy")return type.GetProperty("UpdatedBy");
		if(columnName == "CreatedOn")return type.GetProperty("CreatedOn");
		if(columnName == "UpdatedOn")return type.GetProperty("UpdatedOn");
		if(columnName == "DeletedFlag")return type.GetProperty("DeletedFlag");
return null;
	});
	return map;
	}
		}	 public async Task<List<VIEWMANAGE_HOLIDAYSLIST_CONFIG>> VIEW_MANAGE_HOLIDAYSLIST_CONFIG(MANAGE_HOLIDAYSLIST_CONFIG_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action", "VIEW");
		p.Add("@HolidayId", TBL.HolidayId);
		p.Add("@FromDate", TBL.FromDate);
		p.Add("@ToDate", TBL.ToDate);
		p.Add("@TotalDays", TBL.TotalDays);
		p.Add("@HolidayNameE", TBL.HolidayNameE);
		p.Add("@HolidayNameH", TBL.HolidayNameH);
		p.Add("@Description", TBL.Description);
		p.Add("@CreatedBy", TBL.CreatedBy);
		p.Add("@UpdatedBy", TBL.UpdatedBy);
		p.Add("@UserId", TBL.UserId);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
VIEW_MANAGE_HOLIDAYSLIST_CONFIGMapper _Mapper = new VIEW_MANAGE_HOLIDAYSLIST_CONFIGMapper();
		Dapper.SqlMapper.SetTypeMap(typeof(VIEWMANAGE_HOLIDAYSLIST_CONFIG), _Mapper.VIEW_MANAGE_HOLIDAYSLIST_CONFIGmapper());
		var results = await Connection.QueryAsync<VIEWMANAGE_HOLIDAYSLIST_CONFIG>("MANAGE_HOLIDAYSLIST_CONFIG", p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		}
	 public class EDIT_MANAGE_HOLIDAYSLIST_CONFIGMapper
		{
		public Dapper.SqlMapper.ITypeMap  EDIT_MANAGE_HOLIDAYSLIST_CONFIGmapper()
		{
		var map = new CustomPropertyTypeMap(typeof(EDITMANAGE_HOLIDAYSLIST_CONFIG), (type, columnName) =>
		{
				if(columnName == "HolidayId")return type.GetProperty("HolidayId");
		if(columnName == "FromDate")return type.GetProperty("FromDate");
		if(columnName == "ToDate")return type.GetProperty("ToDate");
		if(columnName == "TotalDays")return type.GetProperty("TotalDays");
		if(columnName == "HolidayName")return type.GetProperty("HolidayName");
		if(columnName == "HolidayName_NV")return type.GetProperty("HolidayName_NV");
		if(columnName == "Description")return type.GetProperty("Description");
		if(columnName == "CreatedBy")return type.GetProperty("CreatedBy");
		if(columnName == "UpdatedBy")return type.GetProperty("UpdatedBy");
		if(columnName == "CreatedOn")return type.GetProperty("CreatedOn");
		if(columnName == "UpdatedOn")return type.GetProperty("UpdatedOn");
		if(columnName == "DeletedFlag")return type.GetProperty("DeletedFlag");
return null;
	});
	return map;
	}
		}	 public async Task<List<EDITMANAGE_HOLIDAYSLIST_CONFIG>> EDIT_MANAGE_HOLIDAYSLIST_CONFIG(MANAGE_HOLIDAYSLIST_CONFIG_Model TBL)
	{
				var p = new DynamicParameters();
		p.Add("@Action", "EDIT");
		p.Add("@HolidayId", TBL.HolidayId);
		p.Add("@FromDate", TBL.FromDate);
		p.Add("@ToDate", TBL.ToDate);
		p.Add("@TotalDays", TBL.TotalDays);
		p.Add("@HolidayNameE", TBL.HolidayNameE);
		p.Add("@HolidayNameH", TBL.HolidayNameH);
		p.Add("@Description", TBL.Description);
		p.Add("@CreatedBy", TBL.CreatedBy);
		p.Add("@UpdatedBy", TBL.UpdatedBy);
		p.Add("@UserId", TBL.UserId);
		p.Add("@DeletedFlag", TBL.DeletedFlag);
EDIT_MANAGE_HOLIDAYSLIST_CONFIGMapper _Mapper = new EDIT_MANAGE_HOLIDAYSLIST_CONFIGMapper();
		Dapper.SqlMapper.SetTypeMap(typeof(EDITMANAGE_HOLIDAYSLIST_CONFIG), _Mapper.EDIT_MANAGE_HOLIDAYSLIST_CONFIGmapper());
		var results = await Connection.QueryAsync<EDITMANAGE_HOLIDAYSLIST_CONFIG>("MANAGE_HOLIDAYSLIST_CONFIG", p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		}
}
}
