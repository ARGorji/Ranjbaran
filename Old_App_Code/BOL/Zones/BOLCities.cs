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

public class BOLCities : BaseBOLCities, IBaseBOL<Cities>
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

    internal string GetCityByID(int CityCode)
    {
        Cities CutCity = dataContext.Cities.SingleOrDefault(p => p.PayeganCode.Equals(CityCode));
        if (CutCity != null)
            return CutCity.Name;
        else
            return "";

    }

    internal string GetCityByID(int ProvinceCode, int CityCode)
    {
        vCities CutCity = dataContext.vCities.SingleOrDefault(p => p.IranMCCityCode.Equals(CityCode) && p.IranMCCode.Equals(ProvinceCode));
        if (CutCity != null)
            return CutCity.Name;
        else
            return "";

    }


    internal int GetCityCode(string CityCode)
    {
        Cities CurRecord = dataContext.Cities.SingleOrDefault(p => p.PayeganCode.Equals(CityCode));
        if (CurRecord != null)
            return CurRecord.Code;
        else
            return 0;
    }
}
