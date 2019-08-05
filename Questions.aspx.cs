using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran
{
    public partial class Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BOLQuestions QuestionsBOL = new BOLQuestions();
            rptQuestions.DataSource = QuestionsBOL.GetQuestions();
            rptQuestions.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserCode"] == null)
            {
                msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msg.Text = "برای ارسال پرسش لازم است ابتدا در سایت عضو شوید";
                return;
            }

            string Question = txtQuestion.Text;
            if (string.IsNullOrEmpty(Question))
            {
                msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msg.Text = "لطفا سوال را وارد کنید";
                return;
            }

            int UserCode = Convert.ToInt32(Session["UserCode"]);
            BOLQuestions QuestionsBOL = new BOLQuestions();
            bool Result = QuestionsBOL.Insert(Question, UserCode);
            if (Result)
            {
                msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                msg.Text = "سوال شما با موفقیت ثبت شد";
            }
            else
            {
                msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msg.Text = "بروز خطا در ثبت سوال";
            }
            
        }
    }
}