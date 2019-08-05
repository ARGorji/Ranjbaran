using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace Ranjbaran.UsersFolder
{
    public partial class ForgotPassword2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string GenKey = Request["GenKey"];
            UsersDataContext dc = new UsersDataContext();

            vValidForgotPasswords ValidForgotPass = dc.vValidForgotPasswords.SingleOrDefault(p => p.GenKey.Equals(GenKey));
            if (ValidForgotPass == null)
            {
                pnlNewPassword.Visible = false;
                msgMessage.Text = "مدت زمان برای بازیابی منقضی شده است یا لینک اشتباه میباشد.";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
            }

        }
        protected void btnSetNewPassword_Click(object sender, EventArgs e)
        {
            string GenKey = Request["GenKey"];
            UsersDataContext dc = new UsersDataContext();
            vValidForgotPasswords ValidForgotPass = dc.vValidForgotPasswords.SingleOrDefault(p => p.GenKey.Equals(GenKey));
            if (ValidForgotPass == null)
            {
                pnlNewPassword.Visible = false;
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = " لینک اشتباه میباشد.";
            }

            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                msgMessage.Text = "کلمه عبور و تکرار کلمه عبور یکسان نیستند";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
            }
            else if (txtPassword.Text.Length < 6)
            {
                msgMessage.Text = "طول کلمه عبور حداقل باید شش کاراکتر باشد.";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
            }
            else
            {
                Users CurUser = dc.Users.SingleOrDefault(p => p.Code.Equals(ValidForgotPass.UserCode));
                CurUser.Password = Tools.Encode(txtPassword.Text);
                dc.SubmitChanges();

                UsersDataContext dcFP = new UsersDataContext();

                ForgotPasswords CurForgotPass = dcFP.ForgotPasswords.SingleOrDefault(p => p.GenKey.Equals(GenKey));
                CurForgotPass.Used = true;
                dcFP.SubmitChanges();

                msgMessage.Text = "کلمه عبور با موفقیت تغییر یافت";
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                pnlNewPassword.Visible = false;
            }
        }
    
    }
}