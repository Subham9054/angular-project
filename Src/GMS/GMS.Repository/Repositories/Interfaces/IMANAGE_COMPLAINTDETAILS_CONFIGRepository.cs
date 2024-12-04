using System.Collections.Generic;
using GMS.Model.Entities.GMS;
using PHED_CGRC.MANAGE_COMPLAINTDETAILS_CONFIG;
namespace GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG
{
    public interface IMANAGE_COMPLAINTDETAILS_CONFIGRepository
    {
        Task<bool> ComplaintRegistrationdetail(Complaint complaint);
        Task<List<gmsComplaintdetails>> getGmscomplaintdetail();
        Task<List<gmsComplaintdetails>> Getupdatetakeaction(string token);
        Task<int> INSERT_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<int> UPDATE_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<int> DELETE_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<List<VIEWMANAGE_COMPLAINTDETAILS_CONFIG>> VIEW_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<List<EDITMANAGE_COMPLAINTDETAILS_CONFIG>> EDIT_MANAGE_COMPLAINTDETAILS_CONFIG(MANAGE_COMPLAINTDETAILS_CONFIG_Model TBL);
        Task<List<GetCitizen>> GetCitizendetails(string token);
        Task<bool> UpdateCitizendetails(string token, UpdateCitizen updateCitizen);
        Task<List<GetCitizenall>> GetallCitizendetails(string token, string mobno);
        Task<bool> GenerateOtp(string phoneNumber);
        Task<OTPDetails> ValidateOtp(string phoneNumber, string otp);
        Task<bool> MarkOtpAsUsedAsync(int otpId);
    }
}
