using Ranjbaran.Old_App_Code.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.Admin.NewsLetter
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            msgBox.Text = "";
        }

        //
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string Subject = txtSubject.Text;
            string MailBody = txtMailBody.Content;

            if (string.IsNullOrEmpty(Subject))
            {
                msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgBox.Text = "لطفا عنوان ایمیل ر وارد کنید";
                return;
            }


            if (string.IsNullOrEmpty(MailBody))
            {
                msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgBox.Text = "لطفا متن ایمیل ر وارد کنید";
                return;
            }

            Tools tools = new Tools();
            UsersDataContext dc = new UsersDataContext();
            var Result = (from p in dc.Users
                          select new { p.Email }
                         ).Distinct();
            int Counter = 0;
            int SuccessSent = 0;
            string BCCList = "";
            foreach (var item in Result)
            {
                Counter++;
                string CurEmail = item.Email;
                if (Counter % 50 != 0)
                {
                    if (BCCList == "")
                        BCCList = CurEmail;
                    else
                        BCCList += "," + CurEmail;
                }
                    
                else
                {
                    bool SendResult = tools.SendMessageWithAttachment(MailBody, Subject, "info@hadiranjbaran.com", CurEmail, BCCList, "", "");
                    if (SendResult)
                        SuccessSent += 50;
                    BCCList = "";
                }

            }

            msgBox.Text = SuccessSent + " ایمیل با موفقیت ارسال شد";




        }
    }
}