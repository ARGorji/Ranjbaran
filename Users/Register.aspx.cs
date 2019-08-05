using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.UsersFolder
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboGenderCode.DataSource = new BOLHardCode().GetHCDataTable("HCGenders");
                cboGenderCode.DataBind();

                cboHCStudyFieldCode.DataSource = new BOLHardCode().GetHCDataTable("HCStudyFields");
                cboHCStudyFieldCode.DataBind();

            }
            msgMessage.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if(!chkAcceptRegRules.Checked)
            //{
            //    msgMessage.Text = "برای ثبت نام در سایت لازم است تعهدنامه ثبت را بپذیرید.";
            //    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
            //    return;
            //}

            string FirstName = txtFirstName.Text;
            string LastName = txtLastName.Text;
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;
            string ConfirmPassword = txtConfirmPassword.Text;

            string strHCStudyFieldCode = cboHCStudyFieldCode.SelectedValue;
            int HCStudyFieldCode = 0;
            Int32.TryParse(strHCStudyFieldCode, out HCStudyFieldCode);

            string strGenderCode = cboGenderCode.SelectedValue;
            int GenderCode = 0;
            Int32.TryParse(strGenderCode, out GenderCode);
            string ContactNumber = txtContactNumber.Text;


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

            int UserCode = UsersBOL.InsertRecord(FirstName, LastName, Email, Password, ContactNumber, GenderCode, HCStudyFieldCode);
            if (UserCode != -1)
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                msgMessage.Text = "ثبت نام شما با موفقیت انجام شد.";
            }

        }
    }

}