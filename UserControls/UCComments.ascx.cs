using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASNoor.UserControls
{
    public partial class UCComments : System.Web.UI.UserControl
    {
        protected int _hcSectionCode;
        public int HCSectionCode
        {
            get
            {
                return _hcSectionCode;

            }
            set
            {
                _hcSectionCode = value;
                ViewState["HCSectionCode"] = value;
            }

        }

        protected int _itemCode;
        public int ItemCode
        {
            get
            {
                return _itemCode;

            }
            set
            {
                _itemCode = value;
                ViewState["ItemCode"] = value;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int LangCode = Tools.GetLang();

            if (!Page.IsPostBack)
            {
                if (LangCode == 1 || LangCode == 2  || LangCode == 3)
                {
                    txtComment.Text = Tools.Translate(LangCode, "Comment");
                    txtName.Text = Tools.Translate(LangCode, "First Name");
                    lblCaptcha.Text = Tools.Translate(LangCode, "Captcha");
                    txtEmail.Text = Tools.Translate(LangCode, "Email");
                    ltrTitle.Text = Tools.Translate(LangCode, "Comments");

                    txtComment.Attributes.Add("onblur", "this.className='CommentText';if(this.value == '') this.value = '" + Tools.Translate(LangCode, "Comment") + "';");
                    txtName.Attributes.Add("onblur", "this.className='CommentText';if(this.value == '') this.value = '" + Tools.Translate(LangCode, "First Name") + "';");
                    txtEmail.Attributes.Add("onblur", "this.className='CommentText';if(this.value == '') this.value = '" + Tools.Translate(LangCode, "Email") + "';");
                }
            }

            if (LangCode == 1 || LangCode == 2)
            {
                btnSendComment.ImageUrl = "~/images/btnUCSendComment.png";
            }
            else if (LangCode == 3)
            {
                btnSendComment.ImageUrl = "~/images/btnUCSendCommentEng.png";
            }

            BOLComments CommentsBOL = new BOLComments();
            rptComments.DataSource = CommentsBOL.GetCommentsByStatusCode(_itemCode, 2);
            rptComments.DataBind();

            if (rptComments.Items.Count == 0)
            {
                rptComments.Visible = false;
                PublishInfo.Visible = false;
            }

            int PublishedCount = CommentsBOL.GetCommentByStatusCodeCount(_itemCode, 2);
            int WillNotBePublishedCount = CommentsBOL.GetCommentByStatusCodeCount(_itemCode, 3);

            lblPublishedCount.Text = Tools.Translate(LangCode, "Published") + ": " + Tools.ChangeEnc(PublishedCount.ToString());
            lblWillNotBePublishedCount.Text = Tools.Translate(LangCode, "Non Released") + ": " + Tools.ChangeEnc(WillNotBePublishedCount.ToString());

        }

        public string PersianDate(Object obj)
        {
            try
            {
                if (obj != null)
                {
                    DateTimeMethods dtm = new DateTimeMethods();
                    DateTime objDate = Convert.ToDateTime(obj);

                    return Tools.ChangeEnc(objDate.ToShortTimeString().Replace("AM", "").Replace("PM", "") + " - " + dtm.GetPersianDate(objDate));
                }
                else
                    return "";

            }
            catch
            {
                return "";
            }
        }
        protected void btnSendComment_Click(object sender, EventArgs e)
        {
            int LangCode = Tools.GetLang();


            msgBox.Text = "";
            string Name = txtName.Text.Trim();
            string Email = txtEmail.Text.Trim();
            string Comment = txtComment.Text.Trim();
            if (Comment == "")
            {
                msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgBox.Text = Tools.Translate(LangCode, "Please enter Comment");
                return;
            }

            if (ViewState["ItemCode"] != null && ViewState["HCSectionCode"] != null)
            {
                
                int HCSectionCode = Convert.ToInt32(ViewState["HCSectionCode"]);
                int ItemCode = Convert.ToInt32(ViewState["ItemCode"]);
                BOLComments CommentsBOL = new BOLComments();
                bool Result = CommentsBOL.SaveComment(HCSectionCode, ItemCode, Name, Email, Comment);
                if (Result)
                {
                    msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                    msgBox.Text = Tools.Translate(LangCode, "Your Comment Successfully Submitted.");
                    btnSendComment.Visible = false;
                }
                else
                {
                    msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgBox.Text = Tools.Translate(LangCode, "Unfortunately there was an error submitting your comment.");
                }


            }
        }
    }
}