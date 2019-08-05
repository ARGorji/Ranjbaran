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
  
public class BOLUsers : BaseBOLUsers, IBaseBOL<Users>
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

    public System.Linq.IQueryable<vUserAccess> GetUserAccessByBaseID(int Code, string BaseID)
    {
        if (BaseID != null)
            return from u in dataContext.vUserAccesses
                   where u.UserCode.Equals(Code) && u.ResName.StartsWith(BaseID)
                   select u;
        else
            return from u in dataContext.vUserAccesses
                   where u.UserCode.Equals(Code) && (u.HCResourceTypeCode.Equals(1) || u.HCResourceTypeCode.Equals(2))
                   select u;

    }
    internal int InsertRecord(string FirstName, string LastName, string Email, string Password)
    {
        UsersDataContext dc = new UsersDataContext();
        Users ObjTable;
        ObjTable = new Users();
        dataContext.Users.InsertOnSubmit(ObjTable);
        try
        {
            #region Save Controls
            ObjTable.FirstName = FirstName;
            ObjTable.LastName = LastName;
            ObjTable.Password = Tools.Encode(Password);
            ObjTable.Email = Email;
            ObjTable.Active = true;

            ObjTable.HCGenderCode = HCGenderCode;

            #endregion

            dataContext.SubmitChanges();
        }
        catch (Exception exp)
        {
            return -1;
        }

        return ObjTable.Code;
    }


    public Users GetDataByUsername(string Username)
    {
        return dataContext.Users.SingleOrDefault(p => p.Email.Equals(Username));
    }

    public void ChangePassword(int? Code, string NewPassword)
    {
        Users CurUser = dataContext.Users.SingleOrDefault(p => p.Code.Equals((int)Code));
        if (CurUser != null)
        {
            CurUser.Password = Tools.Encode(NewPassword);
            dataContext.SubmitChanges();
        }
    }


    public bool EmailExists(string Email)
    {
        UsersDataContext dc = new UsersDataContext();
        IQueryable<Users> Result = dataContext.Users.Where(p => p.Email.Equals(Email));
        if (Result.Count() > 0)
            return true;
        else
            return false;
    }

    internal int InsertRecord(string FirstName, string LastName, string Email, string Password, string ContactNumber, int HCGenderCode, int HCStudyFieldCode)
    {

        UsersDataContext dc = new UsersDataContext();
        Users ObjTable;
        ObjTable = new Users();
        dataContext.Users.InsertOnSubmit(ObjTable);
        try
        {
            #region Save Controls
            ObjTable.FirstName = FirstName;
            ObjTable.LastName = LastName;
            ObjTable.Password = Tools.Encode(Password);
            ObjTable.Email = Email;
            ObjTable.Active = true;

            ObjTable.ContactNumber = ContactNumber;
            if(HCStudyFieldCode != 0)
                ObjTable.HCStudyFieldCode = HCStudyFieldCode;
            ObjTable.HCGenderCode = HCGenderCode;

            #endregion

            dataContext.SubmitChanges();
        }
        catch (Exception exp)
        {
            return -1;
        }

        return ObjTable.Code;
    }

    internal Users GetUserByEmail(string Email)
    {
        return dataContext.Users.SingleOrDefault(p => p.Email.Equals(Email));
    }
}
