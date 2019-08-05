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
using System.Collections;
using System.Collections.Generic;
using Ranjbaran.Old_App_Code.DAL;

public class BOLProvinces : BaseBOLProvinces, IBaseBOL<Provinces>
{
    public IList CheckBusinessRules()
    {
        var messages = new List<string>();

        #region Business Rules
        //Example
        //if (string.IsNullOrEmpty(this.FirstName))
        //    messages.Add("Please fill FirstName.");

        #endregion
        return messages;
    }

    internal string GetNameByMCCode(int IranMCCode)
    {
        return dataContext.Provinces.SingleOrDefault(p => p.IranMCCode.Equals(IranMCCode)).Name;
    }

    internal int GetProvinceCode(string ProvinceCode)
    {
        Provinces CurRecord = dataContext.Provinces.SingleOrDefault(p => p.PayeganCode.Equals(ProvinceCode));
        if (CurRecord != null)
            return CurRecord.Code;
        else
            return 0;
    }
}
