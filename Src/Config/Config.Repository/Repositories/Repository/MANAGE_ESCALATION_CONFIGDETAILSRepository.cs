using System.Collections.Generic;
using Config.Repository.Factory;
using Config.Repository.BaseRepository;
using Config.Repository.Interfaces.MANAGE_ESCALATION_CONFIGDETAILS;
using Config.Repository;

using PHED_CGRC.MANAGE_ESCALATION_CONFIGDETAILS;
using Dapper;
using System.Data;
namespace Config.Repository.Repositories.Interfaces.MANAGE_ESCALATION_CONFIGDETAILS
{
    public class MANAGE_ESCALATION_CONFIGDETAILSRepository : db_PHED_CGRCRepositoryBase, IMANAGE_ESCALATION_CONFIGDETAILSRepository
    {
        public MANAGE_ESCALATION_CONFIGDETAILSRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
        public async Task<int> INSERT_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "INSERT");
            p.Add("@ConfigId", TBL.ConfigId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@EscalationNo", TBL.EscalationNo);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            var results = await Connection.ExecuteAsync("MANAGE_ESCALATION_CONFIGDETAILS", p, commandType: CommandType.StoredProcedure);
            return results;

        }
        public async Task<int> UPDATE_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "UPDATE");
            p.Add("@ConfigId", TBL.ConfigId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@EscalationNo", TBL.EscalationNo);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            var results = await Connection.ExecuteAsync("MANAGE_ESCALATION_CONFIGDETAILS", p, commandType: CommandType.StoredProcedure);
            return results;

        }
        public async Task<int> DELETE_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "DELETE");
            p.Add("@ConfigId", TBL.ConfigId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@EscalationNo", TBL.EscalationNo);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            var results = await Connection.ExecuteAsync("MANAGE_ESCALATION_CONFIGDETAILS", p, commandType: CommandType.StoredProcedure);
            return results;

        }
        public class VIEW_MANAGE_ESCALATION_CONFIGDETAILSMapper
        {
            public Dapper.SqlMapper.ITypeMap VIEW_MANAGE_ESCALATION_CONFIGDETAILSmapper()
            {
                var map = new CustomPropertyTypeMap(typeof(VIEWMANAGE_ESCALATION_CONFIGDETAILS), (type, columnName) =>
                {
                    if (columnName == "ConfigId") return type.GetProperty("ConfigId");
                    if (columnName == "SubCategoryId") return type.GetProperty("SubCategoryId");
                    if (columnName == "EscalationNo") return type.GetProperty("EscalationNo");
                    if (columnName == "CreatedBy") return type.GetProperty("CreatedBy");
                    if (columnName == "CreatedOn") return type.GetProperty("CreatedOn");
                    if (columnName == "UpdatedBy") return type.GetProperty("UpdatedBy");
                    if (columnName == "UpdatedOn") return type.GetProperty("UpdatedOn");
                    return null;
                });
                return map;
            }
        }
        public async Task<List<VIEWMANAGE_ESCALATION_CONFIGDETAILS>> VIEW_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "VIEW");
            p.Add("@ConfigId", TBL.ConfigId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@EscalationNo", TBL.EscalationNo);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            VIEW_MANAGE_ESCALATION_CONFIGDETAILSMapper _Mapper = new VIEW_MANAGE_ESCALATION_CONFIGDETAILSMapper();
            Dapper.SqlMapper.SetTypeMap(typeof(VIEWMANAGE_ESCALATION_CONFIGDETAILS), _Mapper.VIEW_MANAGE_ESCALATION_CONFIGDETAILSmapper());
            var results = await Connection.QueryAsync<VIEWMANAGE_ESCALATION_CONFIGDETAILS>("MANAGE_ESCALATION_CONFIGDETAILS", p, commandType: CommandType.StoredProcedure);
            return results.ToList();

        }
        public class EDIT_MANAGE_ESCALATION_CONFIGDETAILSMapper
        {
            public Dapper.SqlMapper.ITypeMap EDIT_MANAGE_ESCALATION_CONFIGDETAILSmapper()
            {
                var map = new CustomPropertyTypeMap(typeof(EDITMANAGE_ESCALATION_CONFIGDETAILS), (type, columnName) =>
                {
                    if (columnName == "ConfigId") return type.GetProperty("ConfigId");
                    if (columnName == "SubCategoryId") return type.GetProperty("SubCategoryId");
                    if (columnName == "EscalationNo") return type.GetProperty("EscalationNo");
                    if (columnName == "CreatedBy") return type.GetProperty("CreatedBy");
                    if (columnName == "CreatedOn") return type.GetProperty("CreatedOn");
                    if (columnName == "UpdatedBy") return type.GetProperty("UpdatedBy");
                    if (columnName == "UpdatedOn") return type.GetProperty("UpdatedOn");
                    return null;
                });
                return map;
            }
        }
        public async Task<List<EDITMANAGE_ESCALATION_CONFIGDETAILS>> EDIT_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "EDIT");
            p.Add("@ConfigId", TBL.ConfigId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@EscalationNo", TBL.EscalationNo);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            EDIT_MANAGE_ESCALATION_CONFIGDETAILSMapper _Mapper = new EDIT_MANAGE_ESCALATION_CONFIGDETAILSMapper();
            Dapper.SqlMapper.SetTypeMap(typeof(EDITMANAGE_ESCALATION_CONFIGDETAILS), _Mapper.EDIT_MANAGE_ESCALATION_CONFIGDETAILSmapper());
            var results = await Connection.QueryAsync<EDITMANAGE_ESCALATION_CONFIGDETAILS>("MANAGE_ESCALATION_CONFIGDETAILS", p, commandType: CommandType.StoredProcedure);
            return results.ToList();

        }
    }
}
