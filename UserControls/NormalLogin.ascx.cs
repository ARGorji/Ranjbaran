using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace IONS.UserControls
{
    public partial class NormalLogin : System.Web.UI.UserControl
    {
        protected string _afterLoginUrl = null;
        public string AfterLoginUrl
        {
            set
            {
                _afterLoginUrl = value;

            }
            get
            {
                return _afterLoginUrl;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            msgMessage.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;


            if (string.IsNullOrEmpty(Email))
            {
                msgMessage.Text = "لطفا ایمیل را وارد کنید";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                msgMessage.Text = "لطفا کلمه عبور را وارد کنید";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                return;
            }

            BOLUsers UsersBOL = new BOLUsers();
            string strEncPass = Tools.Encode(Password);
            Users CurUser = UsersBOL.GetUserByEmail(Email);
            if (CurUser == null)
            {
                msgMessage.Text = "ایمیل اشتباه است";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                return;
            }


            if (strEncPass != CurUser.Password)
            {
                msgMessage.Text = "کلمه عبور اشتباه است";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                return;
            }


            Session["UserCode"] = CurUser.Code;
            Session["UserFullName"] = CurUser.FirstName + " " + CurUser.LastName;
            Session["Email"] = CurUser.Email;

            if (_afterLoginUrl != null)
            {
                Response.Redirect(_afterLoginUrl);
            }
            else
            {
                if (ViewState["Referer"] != null)
                {
                    if (!ViewState["Referer"].ToString().Contains("Users/Login.aspx"))
                        Response.Redirect(ViewState["Referer"].ToString());
                    else
                        Response.Redirect("~/Default.aspx");
                }
                else
                    Response.Redirect("~/Default.aspx");
            }




        }

    }
}
