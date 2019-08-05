using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Enums
{
    public enum LogTypes
    {
        enm_badLogin = 1,
        enm_Login = 2,
        enm_Logout = 3,
        enm_InsertRec = 4,
        enm_UpdateRec = 5,
        enm_DeleteRec = 6,
        enm_View = 7,
        enm_SessionEnd = 8,
        enm_LoginNotConfirmedYet = 9,
        enm_LoginNotConfirmed = 10,
        enm_UserIsDisabled = 11,
        enm_NoGroupAssigned = 12,
        enm_NoZoneAssigned = 13,
        enm_Moshaveramlakisunknown = 14,
        enm_InvalidLogin = 15,
        enm_ErrorInNetwork = 16,
        enm_ForceLogout = 17,
        enm_Menu = 18,
        enm_InvalidSign = 19,
        enm_SignedWithAnotherCertificate = 20,
        enm_Blocked = 21,
        enm_InvalidLoginCount = 22,
        enm_ErrorInVerify = 23
    }
}