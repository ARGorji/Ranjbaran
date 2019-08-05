using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.Linq.Mapping;
using System.Collections;

namespace AKP.Base.Settings
{
    public static class UITools
    {
        public static bool IsUserSessionStillValid()
        {
            return (HttpContext.Current.Session["UserCode"] != null);
        }
        public static IBaseBOL GetBOLClass(string BaseID, params int[] MasterCodes)
        {

            if (IsHardCode(BaseID))
            {
                //UNDONE: return new BOLHardCode();
                return new BOLHardCode();
            }
            else
            {
                //switch (BaseID)
                Type t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);

                if (t == null)
                    throw new Exception("Invalid BaseID.");

                ConstructorInfo[] cArr = t.GetConstructors();
                if ((cArr.Length > 0) && (cArr[0].GetParameters().Length > 0))
                {
                    object[] oArr = { MasterCodes };
                    return (IBaseBOL)Activator.CreateInstance(t, oArr);
                }
                else
                    return (IBaseBOL)Activator.CreateInstance(t);

            }

        }
        public static bool IsHardCode(string BaseID)
        {
            return BaseID.Substring(0, 2) == "HC";
        }


    }
    public static class AKPSettings
    {
        public static string ProjectName = "وبسایت شخصی هادی رنجبران";
    }
}