using System.Collections.Generic;
using GMS.Repository.Factory;
using GMS.Repository.BaseRepository;
using GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Repository;

using PHED_CGRC.MANAGE_COMPLAINTDETAILS_CONFIG;
using Dapper;
using System.Data;
namespace GMS.Repository.Repositories.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG
{
    public class MANAGE_COMPLAINTDETAILS_CONFIGRepository : db_PHED_CGRCRepositoryBase, IMANAGE_COMPLAINTDETAILS_CONFIGRepository
    {
        public MANAGE_COMPLAINTDETAILS_CONFIGRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
        public async Task<int> INSERT_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL)
        {
            var p = new DynamicParameters();
            //p.Add("@Action", "INSERT");
            p.Add("@ComplaintId", TBL.ComplaintId);
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@HeirachyId", TBL.HeirachyId);
            p.Add("@CompalintLogType", TBL.CompalintLogType);
            p.Add("@ComplaintPriority", TBL.ComplaintPriority);
            p.Add("@Gender", TBL.Gender);
            p.Add("@ProoftypeId", TBL.ProoftypeId);
            p.Add("@IdProof", TBL.IdProof);
            p.Add("@TokenNo", TBL.TokenNo);
            p.Add("@ComplaintName", TBL.ComplaintName);
            p.Add("@ContactNo", TBL.ContactNo);
           // p.Add("@Email", TBL.Email);
            p.Add("@IdproofType", TBL.IdproofType);
            p.Add("@DistId", TBL.DistId);
            p.Add("@Address", TBL.Address);
            //p.Add("@Landmark", TBL.Landmark);
            p.Add("@Longitude", TBL.Longitude);
            p.Add("@Latitude", TBL.Latitude);
            p.Add("@ComplaintAgainstType", TBL.ComplaintAgainstType);
            p.Add("@ComplaintAgainstCode", TBL.ComplaintAgainstCode);
            p.Add("@ComplaintAgainstName", TBL.ComplaintAgainstName);
            p.Add("@ComplaintImage", TBL.ComplaintImage);
            p.Add("@ComplaintFile", TBL.ComplaintFile);
            p.Add("@ComplaintFile1", TBL.ComplaintFile1);
            p.Add("@Docnature", TBL.Docnature);
            p.Add("@Docnature1", TBL.Docnature1);
            p.Add("@ComplaintDeatils", TBL.ComplaintDeatils);
            p.Add("@ComplaintStatusId", TBL.ComplaintStatusId);
            p.Add("@PendingWith", TBL.PendingWith);
            p.Add("@Level", TBL.Level);
            p.Add("@EscaltionDate", TBL.EscaltionDate);
            p.Add("@NextAta", TBL.NextAta);
            //p.Add("@LastUpdatedDate", TBL.LastUpdatedDate);
            p.Add("@ApproxResDate", TBL.ApproxResDate);
            p.Add("@Remark", TBL.Remark);
            p.Add("@ActionFile", TBL.ActionFile);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            //p.Add("@UpdatedOn", TBL.UpdatedOn);
            p.Add("@DeletedFlag", 0);
            p.Add("@Block", TBL.Block);
            p.Add("@Panchayat", TBL.Panchayat);
            p.Add("@Village", TBL.Village);
            var results = await Connection.ExecuteAsync("MANAGE_COMPLAINTDETAILS_CONFIG", p, commandType: CommandType.StoredProcedure);
            return results;

        }
        public async Task<int> UPDATE_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "UPDATE");
            p.Add("@ComplaintId", TBL.ComplaintId);
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@HeirachyId", TBL.HeirachyId);
            p.Add("@CompalintLogType", TBL.CompalintLogType);
            p.Add("@ComplaintPriority", TBL.ComplaintPriority);
            p.Add("@Gender", TBL.Gender);
            p.Add("@ProoftypeId", TBL.ProoftypeId);
            p.Add("@IdProof", TBL.IdProof);
            p.Add("@TokenNo", TBL.TokenNo);
            p.Add("@ComplaintName", TBL.ComplaintName);
            p.Add("@ContactNo", TBL.ContactNo);
            p.Add("@Email", TBL.Email);
            p.Add("@IdproofType", TBL.IdproofType);
            p.Add("@DistId", TBL.DistId);
            p.Add("@Address", TBL.Address);
            p.Add("@Landmark", TBL.Landmark);
            p.Add("@Longitude", TBL.Longitude);
            p.Add("@Latitude", TBL.Latitude);
            p.Add("@ComplaintAgainstType", TBL.ComplaintAgainstType);
            p.Add("@ComplaintAgainstCode", TBL.ComplaintAgainstCode);
            p.Add("@ComplaintAgainstName", TBL.ComplaintAgainstName);
            p.Add("@ComplaintImage", TBL.ComplaintImage);
            p.Add("@ComplaintFile", TBL.ComplaintFile);
            p.Add("@ComplaintFile1", TBL.ComplaintFile1);
            p.Add("@Docnature", TBL.Docnature);
            p.Add("@Docnature1", TBL.Docnature1);
            p.Add("@ComplaintDeatils", TBL.ComplaintDeatils);
            p.Add("@ComplaintStatusId", TBL.ComplaintStatusId);
            p.Add("@PendingWith", TBL.PendingWith);
            p.Add("@Level", TBL.Level);
            p.Add("@EscaltionDate", TBL.EscaltionDate);
            p.Add("@NextAta", TBL.NextAta);
            p.Add("@LastUpdatedDate", TBL.LastUpdatedDate);
            p.Add("@ApproxResDate", TBL.ApproxResDate);
            p.Add("@Remark", TBL.Remark);
            p.Add("@ActionFile", TBL.ActionFile);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@Block", TBL.Block);
            p.Add("@Panchayat", TBL.Panchayat);
            p.Add("@Village", TBL.Village);
            var results = await Connection.ExecuteAsync("MANAGE_COMPLAINTDETAILS_CONFIG", p, commandType: CommandType.StoredProcedure);
            return results;

        }
        public async Task<int> DELETE_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "DELETE");
            p.Add("@ComplaintId", TBL.ComplaintId);
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@HeirachyId", TBL.HeirachyId);
            p.Add("@CompalintLogType", TBL.CompalintLogType);
            p.Add("@ComplaintPriority", TBL.ComplaintPriority);
            p.Add("@Gender", TBL.Gender);
            p.Add("@ProoftypeId", TBL.ProoftypeId);
            p.Add("@IdProof", TBL.IdProof);
            p.Add("@TokenNo", TBL.TokenNo);
            p.Add("@ComplaintName", TBL.ComplaintName);
            p.Add("@ContactNo", TBL.ContactNo);
            p.Add("@Email", TBL.Email);
            p.Add("@IdproofType", TBL.IdproofType);
            p.Add("@DistId", TBL.DistId);
            p.Add("@Address", TBL.Address);
            p.Add("@Landmark", TBL.Landmark);
            p.Add("@Longitude", TBL.Longitude);
            p.Add("@Latitude", TBL.Latitude);
            p.Add("@ComplaintAgainstType", TBL.ComplaintAgainstType);
            p.Add("@ComplaintAgainstCode", TBL.ComplaintAgainstCode);
            p.Add("@ComplaintAgainstName", TBL.ComplaintAgainstName);
            p.Add("@ComplaintImage", TBL.ComplaintImage);
            p.Add("@ComplaintFile", TBL.ComplaintFile);
            p.Add("@ComplaintFile1", TBL.ComplaintFile1);
            p.Add("@Docnature", TBL.Docnature);
            p.Add("@Docnature1", TBL.Docnature1);
            p.Add("@ComplaintDeatils", TBL.ComplaintDeatils);
            p.Add("@ComplaintStatusId", TBL.ComplaintStatusId);
            p.Add("@PendingWith", TBL.PendingWith);
            p.Add("@Level", TBL.Level);
            p.Add("@EscaltionDate", TBL.EscaltionDate);
            p.Add("@NextAta", TBL.NextAta);
            p.Add("@LastUpdatedDate", TBL.LastUpdatedDate);
            p.Add("@ApproxResDate", TBL.ApproxResDate);
            p.Add("@Remark", TBL.Remark);
            p.Add("@ActionFile", TBL.ActionFile);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@Block", TBL.Block);
            p.Add("@Panchayat", TBL.Panchayat);
            p.Add("@Village", TBL.Village);
            var results = await Connection.ExecuteAsync("MANAGE_COMPLAINTDETAILS_CONFIG", p, commandType: CommandType.StoredProcedure);
            return results;

        }
        public class VIEW_MANAGE_COMPLAINTDETAILS_CONFIGMapper
        {
            public Dapper.SqlMapper.ITypeMap VIEW_MANAGE_COMPLAINTDETAILS_CONFIGmapper()
            {
                var map = new CustomPropertyTypeMap(typeof(VIEWMANAGE_COMPLAINTDETAILS_CONFIG), (type, columnName) =>
                {
                    if (columnName == "ComplaintId") return type.GetProperty("ComplaintId");
                    if (columnName == "CategoryId") return type.GetProperty("CategoryId");
                    if (columnName == "SubCategoryId") return type.GetProperty("SubCategoryId");
                    if (columnName == "HeirachyId") return type.GetProperty("HeirachyId");
                    if (columnName == "CompalintLogType") return type.GetProperty("CompalintLogType");
                    if (columnName == "ComplaintPriority") return type.GetProperty("ComplaintPriority");
                    if (columnName == "Gender") return type.GetProperty("Gender");
                    if (columnName == "ProoftypeId") return type.GetProperty("ProoftypeId");
                    if (columnName == "IdProof") return type.GetProperty("IdProof");
                    if (columnName == "TokenNo") return type.GetProperty("TokenNo");
                    if (columnName == "ComplaintName") return type.GetProperty("ComplaintName");
                    if (columnName == "ContactNo") return type.GetProperty("ContactNo");
                    if (columnName == "Email") return type.GetProperty("Email");
                    if (columnName == "IdproofType") return type.GetProperty("IdproofType");
                    if (columnName == "DistId") return type.GetProperty("DistId");
                    if (columnName == "Address") return type.GetProperty("Address");
                    if (columnName == "Landmark") return type.GetProperty("Landmark");
                    if (columnName == "Longitude") return type.GetProperty("Longitude");
                    if (columnName == "Latitude") return type.GetProperty("Latitude");
                    if (columnName == "ComplaintAgainstType") return type.GetProperty("ComplaintAgainstType");
                    if (columnName == "ComplaintAgainstCode") return type.GetProperty("ComplaintAgainstCode");
                    if (columnName == "ComplaintAgainstName") return type.GetProperty("ComplaintAgainstName");
                    if (columnName == "ComplaintImage") return type.GetProperty("ComplaintImage");
                    if (columnName == "ComplaintFile") return type.GetProperty("ComplaintFile");
                    if (columnName == "ComplaintFile1") return type.GetProperty("ComplaintFile1");
                    if (columnName == "Docnature") return type.GetProperty("Docnature");
                    if (columnName == "Docnature1") return type.GetProperty("Docnature1");
                    if (columnName == "ComplaintDeatils") return type.GetProperty("ComplaintDeatils");
                    if (columnName == "ComplaintStatusId") return type.GetProperty("ComplaintStatusId");
                    if (columnName == "PendingWith") return type.GetProperty("PendingWith");
                    if (columnName == "Level") return type.GetProperty("Level");
                    if (columnName == "EscaltionDate") return type.GetProperty("EscaltionDate");
                    if (columnName == "NextAta") return type.GetProperty("NextAta");
                    if (columnName == "LastUpdatedDate") return type.GetProperty("LastUpdatedDate");
                    if (columnName == "ApproxResDate") return type.GetProperty("ApproxResDate");
                    if (columnName == "Remark") return type.GetProperty("Remark");
                    if (columnName == "ActionFile") return type.GetProperty("ActionFile");
                    if (columnName == "UserId") return type.GetProperty("UserId");
                    if (columnName == "CreatedOn") return type.GetProperty("CreatedOn");
                    if (columnName == "UpdatedOn") return type.GetProperty("UpdatedOn");
                    if (columnName == "DeletedFlag") return type.GetProperty("DeletedFlag");
                    if (columnName == "Block") return type.GetProperty("Block");
                    if (columnName == "Panchayat") return type.GetProperty("Panchayat");
                    if (columnName == "Village") return type.GetProperty("Village");
                    return null;
                });
                return map;
            }
        }
        public async Task<List<VIEWMANAGE_COMPLAINTDETAILS_CONFIG>> VIEW_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "VIEW");
            p.Add("@ComplaintId", TBL.ComplaintId);
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@HeirachyId", TBL.HeirachyId);
            p.Add("@CompalintLogType", TBL.CompalintLogType);
            p.Add("@ComplaintPriority", TBL.ComplaintPriority);
            p.Add("@Gender", TBL.Gender);
            p.Add("@ProoftypeId", TBL.ProoftypeId);
            p.Add("@IdProof", TBL.IdProof);
            p.Add("@TokenNo", TBL.TokenNo);
            p.Add("@ComplaintName", TBL.ComplaintName);
            p.Add("@ContactNo", TBL.ContactNo);
            p.Add("@Email", TBL.Email);
            p.Add("@IdproofType", TBL.IdproofType);
            p.Add("@DistId", TBL.DistId);
            p.Add("@Address", TBL.Address);
            p.Add("@Landmark", TBL.Landmark);
            p.Add("@Longitude", TBL.Longitude);
            p.Add("@Latitude", TBL.Latitude);
            p.Add("@ComplaintAgainstType", TBL.ComplaintAgainstType);
            p.Add("@ComplaintAgainstCode", TBL.ComplaintAgainstCode);
            p.Add("@ComplaintAgainstName", TBL.ComplaintAgainstName);
            p.Add("@ComplaintImage", TBL.ComplaintImage);
            p.Add("@ComplaintFile", TBL.ComplaintFile);
            p.Add("@ComplaintFile1", TBL.ComplaintFile1);
            p.Add("@Docnature", TBL.Docnature);
            p.Add("@Docnature1", TBL.Docnature1);
            p.Add("@ComplaintDeatils", TBL.ComplaintDeatils);
            p.Add("@ComplaintStatusId", TBL.ComplaintStatusId);
            p.Add("@PendingWith", TBL.PendingWith);
            p.Add("@Level", TBL.Level);
            p.Add("@EscaltionDate", TBL.EscaltionDate);
            p.Add("@NextAta", TBL.NextAta);
            p.Add("@LastUpdatedDate", TBL.LastUpdatedDate);
            p.Add("@ApproxResDate", TBL.ApproxResDate);
            p.Add("@Remark", TBL.Remark);
            p.Add("@ActionFile", TBL.ActionFile);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@Block", TBL.Block);
            p.Add("@Panchayat", TBL.Panchayat);
            p.Add("@Village", TBL.Village);
            VIEW_MANAGE_COMPLAINTDETAILS_CONFIGMapper _Mapper = new VIEW_MANAGE_COMPLAINTDETAILS_CONFIGMapper();
            Dapper.SqlMapper.SetTypeMap(typeof(VIEWMANAGE_COMPLAINTDETAILS_CONFIG), _Mapper.VIEW_MANAGE_COMPLAINTDETAILS_CONFIGmapper());
            var results = await Connection.QueryAsync<VIEWMANAGE_COMPLAINTDETAILS_CONFIG>("MANAGE_COMPLAINTDETAILS_CONFIG", p, commandType: CommandType.StoredProcedure);
            return results.ToList();

        }
        public class EDIT_MANAGE_COMPLAINTDETAILS_CONFIGMapper
        {
            public Dapper.SqlMapper.ITypeMap EDIT_MANAGE_COMPLAINTDETAILS_CONFIGmapper()
            {
                var map = new CustomPropertyTypeMap(typeof(EDITMANAGE_COMPLAINTDETAILS_CONFIG), (type, columnName) =>
                {
                    if (columnName == "ComplaintId") return type.GetProperty("ComplaintId");
                    if (columnName == "CategoryId") return type.GetProperty("CategoryId");
                    if (columnName == "SubCategoryId") return type.GetProperty("SubCategoryId");
                    if (columnName == "HeirachyId") return type.GetProperty("HeirachyId");
                    if (columnName == "CompalintLogType") return type.GetProperty("CompalintLogType");
                    if (columnName == "ComplaintPriority") return type.GetProperty("ComplaintPriority");
                    if (columnName == "Gender") return type.GetProperty("Gender");
                    if (columnName == "ProoftypeId") return type.GetProperty("ProoftypeId");
                    if (columnName == "IdProof") return type.GetProperty("IdProof");
                    if (columnName == "TokenNo") return type.GetProperty("TokenNo");
                    if (columnName == "ComplaintName") return type.GetProperty("ComplaintName");
                    if (columnName == "ContactNo") return type.GetProperty("ContactNo");
                    if (columnName == "Email") return type.GetProperty("Email");
                    if (columnName == "IdproofType") return type.GetProperty("IdproofType");
                    if (columnName == "DistId") return type.GetProperty("DistId");
                    if (columnName == "Address") return type.GetProperty("Address");
                    if (columnName == "Landmark") return type.GetProperty("Landmark");
                    if (columnName == "Longitude") return type.GetProperty("Longitude");
                    if (columnName == "Latitude") return type.GetProperty("Latitude");
                    if (columnName == "ComplaintAgainstType") return type.GetProperty("ComplaintAgainstType");
                    if (columnName == "ComplaintAgainstCode") return type.GetProperty("ComplaintAgainstCode");
                    if (columnName == "ComplaintAgainstName") return type.GetProperty("ComplaintAgainstName");
                    if (columnName == "ComplaintImage") return type.GetProperty("ComplaintImage");
                    if (columnName == "ComplaintFile") return type.GetProperty("ComplaintFile");
                    if (columnName == "ComplaintFile1") return type.GetProperty("ComplaintFile1");
                    if (columnName == "Docnature") return type.GetProperty("Docnature");
                    if (columnName == "Docnature1") return type.GetProperty("Docnature1");
                    if (columnName == "ComplaintDeatils") return type.GetProperty("ComplaintDeatils");
                    if (columnName == "ComplaintStatusId") return type.GetProperty("ComplaintStatusId");
                    if (columnName == "PendingWith") return type.GetProperty("PendingWith");
                    if (columnName == "Level") return type.GetProperty("Level");
                    if (columnName == "EscaltionDate") return type.GetProperty("EscaltionDate");
                    if (columnName == "NextAta") return type.GetProperty("NextAta");
                    if (columnName == "LastUpdatedDate") return type.GetProperty("LastUpdatedDate");
                    if (columnName == "ApproxResDate") return type.GetProperty("ApproxResDate");
                    if (columnName == "Remark") return type.GetProperty("Remark");
                    if (columnName == "ActionFile") return type.GetProperty("ActionFile");
                    if (columnName == "UserId") return type.GetProperty("UserId");
                    if (columnName == "CreatedOn") return type.GetProperty("CreatedOn");
                    if (columnName == "UpdatedOn") return type.GetProperty("UpdatedOn");
                    if (columnName == "DeletedFlag") return type.GetProperty("DeletedFlag");
                    if (columnName == "Block") return type.GetProperty("Block");
                    if (columnName == "Panchayat") return type.GetProperty("Panchayat");
                    if (columnName == "Village") return type.GetProperty("Village");
                    return null;
                });
                return map;
            }
        }
        public async Task<List<EDITMANAGE_COMPLAINTDETAILS_CONFIG>> EDIT_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "EDIT");
            p.Add("@ComplaintId", TBL.ComplaintId);
            p.Add("@CategoryId", TBL.CategoryId);
            p.Add("@SubCategoryId", TBL.SubCategoryId);
            p.Add("@HeirachyId", TBL.HeirachyId);
            p.Add("@CompalintLogType", TBL.CompalintLogType);
            p.Add("@ComplaintPriority", TBL.ComplaintPriority);
            p.Add("@Gender", TBL.Gender);
            p.Add("@ProoftypeId", TBL.ProoftypeId);
            p.Add("@IdProof", TBL.IdProof);
            p.Add("@TokenNo", TBL.TokenNo);
            p.Add("@ComplaintName", TBL.ComplaintName);
            p.Add("@ContactNo", TBL.ContactNo);
            p.Add("@Email", TBL.Email);
            p.Add("@IdproofType", TBL.IdproofType);
            p.Add("@DistId", TBL.DistId);
            p.Add("@Address", TBL.Address);
            p.Add("@Landmark", TBL.Landmark);
            p.Add("@Longitude", TBL.Longitude);
            p.Add("@Latitude", TBL.Latitude);
            p.Add("@ComplaintAgainstType", TBL.ComplaintAgainstType);
            p.Add("@ComplaintAgainstCode", TBL.ComplaintAgainstCode);
            p.Add("@ComplaintAgainstName", TBL.ComplaintAgainstName);
            p.Add("@ComplaintImage", TBL.ComplaintImage);
            p.Add("@ComplaintFile", TBL.ComplaintFile);
            p.Add("@ComplaintFile1", TBL.ComplaintFile1);
            p.Add("@Docnature", TBL.Docnature);
            p.Add("@Docnature1", TBL.Docnature1);
            p.Add("@ComplaintDeatils", TBL.ComplaintDeatils);
            p.Add("@ComplaintStatusId", TBL.ComplaintStatusId);
            p.Add("@PendingWith", TBL.PendingWith);
            p.Add("@Level", TBL.Level);
            p.Add("@EscaltionDate", TBL.EscaltionDate);
            p.Add("@NextAta", TBL.NextAta);
            p.Add("@LastUpdatedDate", TBL.LastUpdatedDate);
            p.Add("@ApproxResDate", TBL.ApproxResDate);
            p.Add("@Remark", TBL.Remark);
            p.Add("@ActionFile", TBL.ActionFile);
            p.Add("@UserId", TBL.UserId);
            p.Add("@CreatedOn", TBL.CreatedOn);
            p.Add("@UpdatedOn", TBL.UpdatedOn);
            p.Add("@DeletedFlag", TBL.DeletedFlag);
            p.Add("@Block", TBL.Block);
            p.Add("@Panchayat", TBL.Panchayat);
            p.Add("@Village", TBL.Village);
            EDIT_MANAGE_COMPLAINTDETAILS_CONFIGMapper _Mapper = new EDIT_MANAGE_COMPLAINTDETAILS_CONFIGMapper();
            Dapper.SqlMapper.SetTypeMap(typeof(EDITMANAGE_COMPLAINTDETAILS_CONFIG), _Mapper.EDIT_MANAGE_COMPLAINTDETAILS_CONFIGmapper());
            var results = await Connection.QueryAsync<EDITMANAGE_COMPLAINTDETAILS_CONFIG>("MANAGE_COMPLAINTDETAILS_CONFIG", p, commandType: CommandType.StoredProcedure);
            return results.ToList();

        }
    }
}
