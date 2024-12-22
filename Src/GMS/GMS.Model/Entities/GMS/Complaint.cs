using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Model.Entities.GMS
{
    public class Complaint
    {
        public string? INT_CATEGORY_ID { get; set; }
        public string? INT_COMPLIANT_LOG_TYPE { get; set; }
        public string? INT_SUB_CATEGORY_ID { get; set; }
        public string? VCH_CONTACT_NO { get; set; }
        public string? NVCH_COMPLIANTANT_NAME { get; set; }
        public string? INT_DIST_ID { get; set; }
        public string? INT_BLOCK { get; set; }
        public string? INT_PANCHAYAT { get; set; }
        public string? INT_VILLAGE { get; set; }
        public string? INT_WARD { get; set; }
        public string? NVCH_ADDRESS { get; set; }
        public string? NVCH_COMPLIANT_DETAILS { get; set; }

        // Change this to IFormFile for handling file uploads
        public string? VCH_COMPLAINT_FILE { get; set; }
        //public IList<IFormFile> files { get; set; }

        public DateTime DTM_CREATED_ON { get; set; }
        public string? VCH_EMAIL { get; set; }
        public string? NVCH_LANDMARK { get; set; }
        public string? INT_COMPLAINT_PRIORITY { get; set; }
    }
    public class ComplaintDetails
    {
        public string VCH_DISTNAME { get; set; }
        public int INT_DIST_ID { get; set; }
        public string VCH_BLOCKNAME { get; set; }
        public int INT_BLOCK { get; set; }
        public string VCH_PANCHAYAT { get; set; }
        public int INT_PANCHAYAT { get; set; }
        public string VCH_VILLAGE { get; set; }
        public int INT_VILLAGE { get; set; }
        public string VCH_WARD { get; set; }
        public int INT_WARD { get; set; }
        public string NVCH_ADDRESS { get; set; }
        public string NVCH_LANDMARK { get; set; }
        public string VCH_COMPLAINT_FILE { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public string DTM_CREATED_ON { get; set; }
        public string VCH_EMAIL { get; set; }
        public string NVCH_COMPLIANTANT_NAME { get; set; }
        public long VCH_TOKENNO { get; set; }
        public string VCH_CONTACT_NO { get; set; }
        public int INT_COMPLAINT_PRIORITY { get; set; }
        public string VCH_COMPLIANT_STATUS { get; set; }
        public int INT_COMPLIANT_LOG_TYPE { get; set; }
        public string VCH_COMPLIANT_LOG_TYPE { get; set; }
        public string NVCH_COMPLIANT_DETAILS { get; set; }
    }

    public class gmsComplaintdetails
    {
        public int INT_COMPLIANT_ID { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_TOKENNO { get; set; }
        public string NVCH_COMPLIANTANT_NAME { get; set; }
        public string vchUserName { get; set; }
        public int INT_COMPLAINT_PRIORITY { get; set; }
        public int INT_COMPLIANT_LOG_TYPE { get; set; }
        public DateTime DTM_CREATED_ON { get; set; }
        public string VCH_COMPLIANT_STATUS { get; set; }
        public string NVCH_COMPLIANT_DETAILS { get; set; }
        public string NVCH_LANDMARK { get; set; }
        public string NVCH_REMARK { get; set; }
        public string VCH_FILE { get; set; }
    }
    public class LocationHierarchy
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public List<Result> Result { get; set; }

    }

    public class Result
    {
        public List<District> Districts { get; set; }
    }

    public class District
    {
        public string IntDistrictId { get; set; }
        public string VchDistrictName { get; set; }
        public List<Block> Blocks { get; set; }
    }

    public class Block
    {
        public string IntBlockId { get; set; }
        public string VchBlockName { get; set; }
        public List<Gp> Gps { get; set; }
    }

    public class Gp
    {
        public string IntGpId { get; set; }
        public string VchGpName { get; set; }
        public List<Village> Villages { get; set; }
    }

    public class Village
    {
        public string IntVillageId { get; set; }
        public string VchVillageName { get; set; }
        public List<Ward> WardList { get; set; }
    }

    public class Ward
    {
        public string IntWardId { get; set; }
        public string VchWardName { get; set; }
    }
    public class GetCitizen
    {

        public int INT_DIST_ID { get; set; }
        public string VCH_DISTNAME { get; set; }
        public int INT_BLOCK { get; set; }
        public string VCH_BLOCKNAME { get; set; }
        public int INT_PANCHAYAT { get; set; }
        public string VCH_PANCHAYAT { get; set; }
        public int INT_VILLAGE { get; set; }
        public string VCH_VILLAGE { get; set; }
        public int INT_WARD { get; set; }
        public string VCH_WARD { get; set; }
        public string NVCH_ADDRESS { get; set; }
        public string NVCH_LANDMARK { get; set; }
    }

    public class GetCitizenall
    {

        public int INT_DIST_ID { get; set; }
        public string VCH_DISTNAME { get; set; }
        public int INT_BLOCK { get; set; }
        public string VCH_BLOCKNAME { get; set; }
        public int INT_PANCHAYAT { get; set; }
        public string VCH_PANCHAYAT { get; set; }
        public int INT_VILLAGE { get; set; }
        public string VCH_VILLAGE { get; set; }
        public int INT_WARD { get; set; }
        public string VCH_WARD { get; set; }
        public string NVCH_ADDRESS { get; set; }
        public string NVCH_LANDMARK { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public DateTime DTM_CREATED_ON { get; set; }
        public string VCH_COMPLAINT_FILE { get; set; }
        public string VCH_EMAIL { get; set; }
        public string NVCH_COMPLIANTANT_NAME { get; set; }
        public string VCH_TOKENNO { get; set; }
        public string VCH_CONTACT_NO { get; set; }
        public string VCH_COMPLIANT_STATUS { get; set; }
        public int INT_COMPLAINT_PRIORITY { get; set; }
        public string VCH_COMPLIANT_LOG_TYPE { get; set; }
        public int INT_COMPLIANT_LOG_TYPE { get; set; }
        public string NVCH_COMPLIANT_DETAILS { get; set; }
    }

    public class UpdateCitizen
    {

        public int INT_DIST_ID { get; set; }
        public int INT_BLOCK { get; set; }
        public int INT_PANCHAYAT { get; set; }
        public int INT_VILLAGE { get; set; }
        public int INT_WARD { get; set; }
        public string NVCH_ADDRESS { get; set; }
        public string NVCH_LANDMARK { get; set; }
    }
    public class OTPDetails
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string OTP { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int IsUsed { get; set; }
    }
    public class OtpGenerate
    {
        public string mobno { get; set; }
    }
    public class ComplaintDetailsTokenResponse
    {
        // All Demo Details
        public string VCH_DISTNAME { get; set; }
        public int INT_DIST_ID { get; set; }
        public string VCH_BLOCKNAME { get; set; }
        public int INT_BLOCK { get; set; }
        public string VCH_PANCHAYAT { get; set; }
        public int INT_PANCHAYAT { get; set; }
        public string VCH_VILLAGE { get; set; }
        public int INT_VILLAGE { get; set; }
        public string VCH_WARD { get; set; }
        public int INT_WARD { get; set; }
        public string NVCH_ADDRESS { get; set; }
        public string NVCH_LANDMARK { get; set; }
        public string VCH_COMPLAINT_FILE { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public DateTime DTM_CREATED_ON { get; set; }
        public string VCH_EMAIL { get; set; }
        public string NVCH_COMPLIANTANT_NAME { get; set; }
        public string VCH_TOKENNO { get; set; }
        public string VCH_CONTACT_NO { get; set; }
        public int INT_COMPLAINT_PRIORITY { get; set; }
        public string VCH_COMPLIANT_STATUS { get; set; }
        public int INT_COMPLIANT_LOG_TYPE { get; set; }
        public string VCH_COMPLIANT_LOG_TYPE { get; set; }
        public string NVCH_COMPLIANT_DETAILS { get; set; }
        public List<IntimationDetails> Intimations { get; set; }
        public List<ActionSummary> ActionSummaries { get; set; }
        public List<EscalationDetails> Escalations { get; set; }
    }

    public class IntimationDetails
    {
        public string ActionTakenBy { get; set; }
        public DateTime? ActionTakenOn { get; set; }
        public string ComplaintStatus { get; set; }
    }

    public class ActionSummary
    {
        public string ActionTakenBy { get; set; }
        public string ComplaintStatus { get; set; }
        public string ActionTakingAuthority { get; set; }
        public string AttachedDocument { get; set; }
        public DateTime? DateOfEscalation { get; set; }
        public string Remark { get; set; }
    }

    public class EscalationDetails
    {
        public string VCH_CATEGORY { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public string NVCH_DESIGNATION_NAME { get; set; }
        public int INT_CATEGORY_ID { get; set; }
        public int INT_SUB_CATEGORY_ID { get; set; }
        public int INT_DESIG_LEVELID { get; set; }
        public int INT_DESIG_ID { get; set; }
        public int INT_ESCALATION_LEVELID { get; set; }
        public string VCH_STANDARD_DAYS { get; set; }
    }

    public class GetAllCitizenDetails
    {
        public string token { get; set; }
        public string mobno { get; set; }
    }
    public class Userdata
    {
        public int? userid { get; set; } // Nullable int for handling null values
    }
    public class ComplaintLog
    {
        public string? INT_COMPLIANT_ID { get; set; }
        public string? INT_COMPLIANT_STATUS_ID { get; set; }
        public string? VCH_FILE { get; set; }
        public string? NVCH_REMARK { get; set; }
        public string? INT_CREATED_BY { get; set; }
        public string? INT_DELETED_FLAG { get; set; }
        public string? designation { get; set; }
    }

}
