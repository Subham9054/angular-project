using System.Collections.Generic;
using GMS.Repository.Factory;
using GMS.Repository.BaseRepository;
using GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Repository;

using PHED_CGRC.MANAGE_COMPLAINTDETAILS_CONFIG;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using GMS.Model.Entities.GMS;
namespace GMS.Repository.Repositories.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG
{
#pragma warning disable
    public class MANAGE_COMPLAINTDETAILS_CONFIGRepository : db_PHED_CGRCRepositoryBase, IMANAGE_COMPLAINTDETAILS_CONFIGRepository
    {
        public MANAGE_COMPLAINTDETAILS_CONFIGRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
        public async Task<string> ComplaintRegistrationdetail(Complaint complaint)
        {
            try
            {


                DynamicParameters parameters = new DynamicParameters();
                var randomToken = new Random().Next(1000000000, 2147483647).ToString();
                parameters.Add("@createdon", DateTime.Now);
                parameters.Add("@createdby", 1);
                parameters.Add("@category", complaint.INT_CATEGORY_ID);
                parameters.Add("@subcategory", complaint.INT_SUB_CATEGORY_ID);
                if (complaint.INT_COMPLIANT_LOG_TYPE == "0")
                {
                    parameters.Add("@complainttype", "5");
                }
                else
                {
                    parameters.Add("@complainttype", complaint.INT_COMPLIANT_LOG_TYPE);
                }
                parameters.Add("@contactnumber", complaint.VCH_CONTACT_NO);
                parameters.Add("@name", complaint.NVCH_COMPLIANTANT_NAME);
                parameters.Add("@distid", complaint.INT_DIST_ID);
                parameters.Add("@blockid", complaint.INT_BLOCK);
                parameters.Add("@panchayatid", complaint.INT_PANCHAYAT);
                parameters.Add("@intVillage", complaint.INT_VILLAGE);
                parameters.Add("@intward", complaint.INT_WARD);
                parameters.Add("@address", complaint.NVCH_ADDRESS);
                parameters.Add("@complaintdetail", complaint.NVCH_COMPLIANT_DETAILS);
                parameters.Add("@filename", complaint.VCH_COMPLAINT_FILE);
                parameters.Add("@token", randomToken);
                parameters.Add("email", complaint.VCH_EMAIL);
                parameters.Add("landMark", complaint.NVCH_LANDMARK);
                parameters.Add("priority", 1);
                parameters.Add("@action", "INSERT");
                var result = await Connection.QueryAsync<string>("USP_complaintRegistration_insert", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<gmsComplaintdetails>> getGmscomplaintdetail(int? userid)
        {
            try
            {

                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("p_userid", userid);
                    var districts = await Connection.QueryAsync<gmsComplaintdetails>("GetGmsComplaintDetails", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return districts.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<gmsComplaintdetails>> Getupdatetakeaction(string token)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("token", token);
                var result = await Connection.QueryAsync<gmsComplaintdetails>("GetGmsupdatetakeaction", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ComplaintDetails>> Getgmsactionhistory(string token)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_TokenNo", token);
                var result = await Connection.QueryAsync<ComplaintDetails>("GetComplaintDetailsByTokenNo", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetCitizen>> GetCitizendetails(string token)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("token", token);
                var result = await Connection.QueryAsync<GetCitizen>("GetCitizenAddressDetails", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetCitizenall>> GetallCitizendetails(string token, string mobno)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_token", token);
                parameters.Add("p_mobno", mobno);

                var result = await Connection.QueryAsync<GetCitizenall>("GetCitizenallDetails", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetCitizenall>> GetallComplaints(string mobno)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("mobno", mobno);
                var result = await Connection.QueryAsync<GetCitizenall>("GetCitizenallComplaints", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> UpdateCitizendetails(string token, UpdateCitizen updateCitizen)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@updatedon", DateTime.Now);
                parameters.Add("@updatedby", 1);
                //parameters.Add("@name", updateCitizen.NVCH_COMPLIANTANT_NAME);
                parameters.Add("@distid", updateCitizen.INT_DIST_ID);
                parameters.Add("@blockid", updateCitizen.INT_BLOCK);
                parameters.Add("@panchayatid", updateCitizen.INT_PANCHAYAT);
                parameters.Add("@intVillage", updateCitizen.INT_VILLAGE);
                parameters.Add("@intward", updateCitizen.INT_WARD);
                parameters.Add("@address", updateCitizen.NVCH_ADDRESS);
                parameters.Add("@token", token);
                parameters.Add("landMark", updateCitizen.NVCH_LANDMARK);
                var result = await Connection.QueryAsync<int>("USP_UpdateCitizen", parameters, commandType: CommandType.StoredProcedure);
                return result.Contains(1);


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GenerateOtp(string phoneNumber)
        {
            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString(); ;
            var expirationTime = DateTime.Now.AddMinutes(30);

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PhoneNumber", phoneNumber);
                parameters.Add("@OTP", otp);
                parameters.Add("@CreatedOn", DateTime.Now);
                parameters.Add("@ExpiresOn", expirationTime);
                parameters.Add("@IsUsed", false);
                var result = await Connection.QueryAsync<string>("GenerateOtp", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<OTPDetails> ValidateOtp(string phoneNumber, string otp)
        {
            try
            {
                Console.WriteLine($"Parameters: PhoneNumber = {phoneNumber}, OTP = {otp}");
                OTPDetails res = new OTPDetails();
                var parameters = new DynamicParameters();
                parameters.Add("p_phoneNumber", phoneNumber.Trim());
                parameters.Add("p_otp", otp.Trim());
                res = await Connection.QueryFirstOrDefaultAsync<OTPDetails>("ValidateOtp", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"Query Result: {res}");
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while validating the OTP.", ex);
            }
        }



        public async Task<bool> MarkOtpAsUsedAsync(int otpId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("otpId", otpId);  // The parameter should match the stored procedure definition

                // Execute the stored procedure and retrieve the result (1 or 0)
                var result = await Connection.QueryFirstOrDefaultAsync<int>(
                    "MarkOtpAsUsed", // Stored procedure name
                    parameters,      // Parameters
                    commandType: CommandType.StoredProcedure
                );

                // Return true if the result is 1 (success), otherwise false
                return result == 1;
            }
            catch (Exception ex)
            {
                // Handle exception (you may want to log it)
                throw new Exception("An error occurred while marking OTP as used.", ex);
            }
        }

        public async Task<List<ComplaintDetailsTokenResponse>> Getalldetailagaintstoken(string token, int catid, int subcatid)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_VCH_TOKENNO", token);
                parameters.Add("categoryid", catid);
                parameters.Add("subcategoryid", subcatid);

                using (var multi = await Connection.QueryMultipleAsync("USP_GetComplaintDetailstoken", parameters, commandType: CommandType.StoredProcedure))
                {
                    var complaintDetails = multi.Read<ComplaintDetailsTokenResponse>().ToList();

                    foreach (var complaint in complaintDetails)
                    {
                        complaint.Intimations = multi.Read<IntimationDetails>().ToList();
                    }

                    foreach (var complaint in complaintDetails)
                    {
                        complaint.ActionSummaries = multi.Read<ActionSummary>().ToList();
                    }

                    foreach (var complaint in complaintDetails)
                    {
                        complaint.Escalations = multi.Read<EscalationDetails>().ToList();
                    }

                    return complaintDetails;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (optional: log the exception)
                throw;
            }
        }
        #region UPDATEREP
        public async Task<int> UpdatecomplainRep(ComplaintLog complaintLog)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@p_compliant_id", complaintLog.INT_COMPLIANT_ID);
                parameters.Add("@p_compliant_status_id", 6);
                parameters.Add("@p_vch_file", complaintLog.VCH_FILE);
                parameters.Add("@p_nvch_remark", complaintLog.NVCH_REMARK);
                // parameters.Add("@p_int_pending_with", complaintLog.INT_PENDING_WITH); // Add this parameter
                parameters.Add("@p_int_created_by", complaintLog.INT_CREATED_BY);
                parameters.Add("@p_int_deleted_flag", 0);
                var result = await Connection.QueryAsync<int>(
                    "USP_complaint_log_insert_REP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region UPDATECONT
        public async Task<int> UpdatecomplainCont(ComplaintLog complaintLog)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@p_compliant_id", complaintLog.INT_COMPLIANT_ID);
                parameters.Add("@p_compliant_status_id", 6);
                parameters.Add("@p_vch_file", complaintLog.VCH_FILE);
                parameters.Add("@p_nvch_remark", complaintLog.NVCH_REMARK);
                // parameters.Add("@p_int_pending_with", complaintLog.INT_PENDING_WITH); // Add this parameter
                parameters.Add("@p_int_created_by", complaintLog.INT_CREATED_BY);
                parameters.Add("@p_int_deleted_flag", 0);
                var result = await Connection.QueryAsync<int>(
                    "USP_complaint_log_insert_CONT",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region Codegencode
        public async Task<int> INSERT_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL)
        {
            var p = new DynamicParameters();
            p.Add("@Action", "INSERT");
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
        #endregion

    }
}
