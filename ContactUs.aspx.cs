using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            msgMessage.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Name = txtName.Text;
            string Email = txtEmail.Text;
            string Comment = txtComment.Text;
            string ContactNumber = txtContactNumber.Text;

            if (!string.IsNullOrEmpty(Comment))
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = "لطفا متن را وارد کنید";
                return;
            }

            string MailBody = Comment;
            Tools tools = new Tools();
            bool SendResult = tools.SendEmail(MailBody, "بازیابی کلمه عبور", Email, "info@hadiranjbaran.com", "", "");
            if (SendResult)
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                msgMessage.Text = "پیام شما با موفقی ارسال شد.";
            }

        }
    }
}