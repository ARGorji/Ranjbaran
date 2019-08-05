using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.UserControls
{
    public partial class UCRegister : System.Web.UI.UserControl
    {
        protected string _afterRegUrl = null;
        public string AfterRegUrl
        {
            set
            {
                _afterRegUrl = value;

            }
            get
            {
                return _afterRegUrl;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ((System.Web.UI.HtmlControls.HtmlForm)Page.Master.FindControl("form1")).DefaultButton = btnSubmit.UniqueID;


                string RefPage = Request["RefPage"];
                if (!string.IsNullOrEmpty(RefPage))
                    ViewState["RefPage"] = RefPage;

            }
            msgMessage.Text = "";
        }

        protected void btnBackToRefPage_Click(object sender, EventArgs e)
        {
            if (ViewState["RefPage"] != null)
                Response.Redirect(ViewState["RefPage"].ToString());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string FirstName = txtFirstName.Text;
            string LastName = txtLastName.Text;
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;
            string ConfirmPassword = txtConfirmPassword.Text;




            if (string.IsNullOrEmpty(FirstName))
            {
                msgMessage.Text = "لطفا نام را وارد کنید";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                msgMessage.Text = "لطفا نام خانوادگی را وارد کنید";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                return;
            }
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

            if (Password != ConfirmPassword)
            {
                msgMessage.Text = "کلمه عبور و تایید کلمه عبور با یکدیگر برابر نیستند";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                return;
            }

            BOLUsers UsersBOL = new BOLUsers();
            if (UsersBOL.EmailExists(Email))
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = "این ایمیل قبلا ثبت شده است";
                return;
            }

            int UserCode = UsersBOL.InsertRecord(FirstName, LastName, Email, Password);

            Session["UserCode"] = UserCode;
            Session["Email"] = Email;
            Session["UserFullName"] = FirstName + " " + LastName;

            EmailTools emailTools = new EmailTools();
            Tools tools = new Tools();
            string EmailTemplate = emailTools.LoadTemplate("UserRegister");//Suggest
            EmailTemplate = EmailTemplate.Replace("[UserFullName]", Session["Gender"] + " " + Session["UserFullName"]);

            bool SendSellResult = tools.SendEmail(EmailTemplate, "ثبت نام شما در سایت اثبات با موفقیت انجام شد", "info@Ranjbaran.ir", txtEmail.Text, "bidaad@gmail.com", "");


            if (_afterRegUrl != null)
                Response.Redirect(_afterRegUrl);

            if (UserCode != -1)
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                msgMessage.Text = "ثبت نام شما با موفقیت انجام شد.";
                btnBackToRefPage.Visible = true;
            }
            else
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = "بروز خطا در فرایند ثبت نام";
            }

        }
    }
}