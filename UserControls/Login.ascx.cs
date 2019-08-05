using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Ranjbaran.Old_App_Code.DAL;
using System.Collections.Generic;
using System.Linq;

public partial class UserControls_Login : System.Web.UI.UserControl
{
    protected LoginModes _mode;
    public LoginModes Mode
    {
        set
        {
            _mode = value;
        }
    }

    protected string _afterLoginUrl = "";
    public string AfterLoginUrl
    {
        set
        {
            _afterLoginUrl = value;
        }
    }


    BOLUsers BOLUsers = new BOLUsers();
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtPassword.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imgBtnLogin.ClientID + "').click();return false;}} else {return true}; ");

        if (!Page.IsPostBack)
        {
            Page.Form.DefaultFocus = txtUsername.UniqueID;
            Page.Form.DefaultButton = imgBtnLogin.UniqueID;
            if (Request.UrlReferrer != null)
            {
                hfPrePage.Value = Request.UrlReferrer.ToString();
            }

            if (Request.Cookies["ASNoorDB"] != null)
            {
                string Username = Request.Cookies["ASNoorDB"]["IONSUser"];
                if (Username != "" && Username != null)
                {
                    txtUsername.Text = Username;
                    chkRemLoginInfo.Checked = true;
                }
                else
                    chkRemLoginInfo.Checked = false;

                string Password = Request.Cookies["ASNoorDB"]["IONSPass"];
                if (Password != "" && Password != null)
                    txtPassword.Attributes.Add("value", Request.Cookies["ASNoorDB"]["IONSPass"]);

            }
        }

    }

    public static bool IsUserSessionStillValid()
    {
        //bool Result = false;
        //if (HttpContext.Current.Session["New"] != null ||
        //   HttpContext.Current.Session["Edit"] != null ||
        //   HttpContext.Current.Session["Delete"] != null ||
        //   HttpContext.Current.Session["View"] != null ||
        //   HttpContext.Current.Session["Export"] != null)
        //    Result = true;
        //return Result;
        return (HttpContext.Current.Session["UserCode"] != null);
    }
    void LoginUser(Users User)
    {
        Session["Username"] = User.Email;
        Session["Email"] = User.Email;
        Session["FirstName"] = User.FirstName;
        Session["LastName"] = User.LastName;
        Session["UserCode"] = User.Code;

        if(User.HCGenderCode == 1)
            Session["GenderName"] = "آقای ";
        else
            Session["GenderName"] = "خانم ";

        Response.Redirect("~/Admin/Main/Panel.aspx");

    }
    protected void btnReg_Click(object sender, EventArgs e)
    {
        if(hfPrePage.Value != "")
            Response.Redirect("~/Reg/?RefPage=" + HttpUtility.UrlEncode( hfPrePage.Value));
        else
            Response.Redirect("~/Reg");
    }
    protected void imgBtnLogin_Click(object sender, EventArgs e)
    {
        msgBox.Text = "";
        if (!RadCaptcha1.IsValid)
        {
            msgBox.Visible = true;
            msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
            msgBox.Text = "کد امنیتی به درستی وارد نشده است.";
            return;
        }

        string DBPassword;
        bool SuccessLogin = false;
        string Username = txtUsername.Text;
        string Password = txtPassword.Text;

        //UsersBR bolUsers = new UsersBR();


        if (chkRemLoginInfo.Checked)
        {
            Response.Cookies["ASNoorDB"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["ASNoorDB"]["IONSUser"] = txtUsername.Text;

            Response.Cookies["ASNoorDB"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["ASNoorDB"]["IONSPass"] = txtPassword.Text;
        }
        else
        {
            Response.Cookies["ASNoorDB"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["ASNoorDB"]["IONSUser"] = "";

            Response.Cookies["ASNoorDB"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["ASNoorDB"]["IONSPass"] = "";
        }

        Users ValidUser = BOLUsers.GetDataByUsername(Username);

        if (ValidUser != null)
        {

            string HashedPass = Tools.GetHashString(txtPassword.Text);
            DBPassword = ValidUser.Password;
            if (HashedPass == DBPassword && (bool)ValidUser.Active)
            {
                SuccessLogin = true;
                LoginUser(ValidUser);
            }
            else
            {
                SuccessLogin = false;
                msgBox.Text = Messages.ShowMessage(MessagesEnum.InvalidPassword);
            }
        }
        else
        {
            SuccessLogin = false;
            msgBox.Text = Messages.ShowMessage(MessagesEnum.InvalidLogin);
        }


    }
}

public enum LoginModes
{
    RegularUser,
    AdminUser
}