using System.Collections.Generic;
using GMS.Model.Entities.GMS;
using PHED_CGRC.MANAGE_COMPLAINTDETAILS_CONFIG;
namespace GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG
{
    public interface IMANAGE_COMPLAINTDETAILS_CONFIGRepository
    {
        Task<string> ComplaintRegistrationdetail(Complaint complaint);
        Task<List<gmsComplaintdetails>> getGmscomplaintdetail(int? userid);
        Task<List<gmsComplaintdetails>> Getupdatetakeaction(string token);
        Task<List<ComplaintDetails>> Getgmsactionhistory(string token);
        Task<int> INSERT_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<int> UPDATE_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<int> DELETE_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<List<VIEWMANAGE_COMPLAINTDETAILS_CONFIG>> VIEW_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<List<EDITMANAGE_COMPLAINTDETAILS_CONFIG>> EDIT_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<List<GetCitizen>> GetCitizendetails(string token);
        Task<bool> UpdateCitizendetails(string token, UpdateCitizen updateCitizen);
        Task<List<GetCitizenall>> GetallCitizendetails(string token, string mobno);
        Task<List<GetCitizenall>> GetallComplaints(string mobno);
        Task<string> GenerateOtp(string phoneNumber);
        Task<OTPDetails> ValidateOtp(string phoneNumber, string otp);
        Task<bool> MarkOtpAsUsedAsync(int otpId);
        Task<List<ComplaintDetailsTokenResponse>> Getalldetailagaintstoken(string token, int catid, int subcatid);
        Task<int> UpdatecomplainRep(ComplaintLog complaintLog);
        Task<int> UpdatecomplainCont(ComplaintLog complaintLog);
    }
}
