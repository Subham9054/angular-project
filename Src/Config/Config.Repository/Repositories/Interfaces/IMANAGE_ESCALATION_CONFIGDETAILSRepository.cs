using System.Collections.Generic;
using Config.Model.Entities.Config;
using PHED_CGRC.MANAGE_ESCALATION_CONFIGDETAILS;
namespace Config.Repository.Interfaces.MANAGE_ESCALATION_CONFIGDETAILS
{
    public interface IMANAGE_ESCALATION_CONFIGDETAILSRepository
    {
        Task<int> UPDATE_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL);

        Task<int> DELETE_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL);

        Task<List<VIEWMANAGE_ESCALATION_CONFIGDETAILS>> VIEW_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL);

        Task<List<EDITMANAGE_ESCALATION_CONFIGDETAILS>> EDIT_MANAGE_ESCALATION_CONFIGDETAILS(MANAGE_ESCALATION_CONFIGDETAILS_Model TBL);
        Task<int> InsertEscalation(Escalationinsert request);
        Task<int> CheckEscalationExist(int INT_CATEGORY_ID, int INT_SUB_CATEGORY_ID);
        public Task<List<EscalationViewModel>> GetEscalations(int categoryid, int subcategoryid);
        public Task<List<EscalationViewModel>> GetEscalationseye(int categoryid, int subcategoryid);
    }
}
