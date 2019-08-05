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

public class BOLUserAddresses : BaseBOLUserAddresses, IBaseBOL<UserAddresses>
{
    public BOLUserAddresses(params int[] MCodes)
        : base(MCodes)
    {

    }
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

    internal bool Insert(int UserCode, string NewAddressFullName, string NewAddressCellPhone, string NewAddressTel, string NewAddressAddress, string NewAddressPostalCode, string NewAddressProvinceCode, string NewAddressCityCode)
    {
        try
        {
            UserAddresses NewRecord = new UserAddresses();
            dataContext.UserAddresses.InsertOnSubmit(NewRecord);

            BOLProvinces ProvincesBOL = new BOLProvinces();
            int ProvinceCode = ProvincesBOL.GetProvinceCode(NewAddressProvinceCode);

            BOLCities CitiesBOL = new BOLCities();
            int CityCode = CitiesBOL.GetCityCode(NewAddressCityCode);

            NewRecord.UserCode = UserCode;
            NewRecord.FullName = NewAddressFullName;
            NewRecord.CellPhone = NewAddressCellPhone;
            NewRecord.Tel = NewAddressTel;
            NewRecord.Address = NewAddressAddress;
            NewRecord.PostalCode = NewAddressPostalCode;
            NewRecord.ProvinceCode = ProvinceCode;
            NewRecord.CityCode = CityCode;

            dataContext.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }

    }

    internal object GetUserAddresses(int UserCode)
    {
        return dataContext.vUserAddresses.Where(p => p.UserCode.Equals(UserCode));
    }

    internal bool DeleteAddress(int UserCode, string Code)
    {
        try
        {
            UserAddresses CurRecord = dataContext.UserAddresses.SingleOrDefault(p => p.UserCode.Equals(UserCode) && p.Code.Equals(Code));
            dataContext.UserAddresses.DeleteOnSubmit(CurRecord);
            dataContext.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    internal vUserAddresses GetFullDetails(int Code)
    {
        return dataContext.vUserAddresses.SingleOrDefault(p => p.Code.Equals(Code));
    }
}
