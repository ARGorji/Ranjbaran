using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class MainMaster : System.Web.UI.MasterPage
{
    public string strTickerDirection = "rtl";
    protected void Page_Load(object sender, EventArgs e)
    {

        #region Load Scripts
        HtmlGenericControl scriptJQuery = new HtmlGenericControl("script");
        scriptJQuery.Attributes.Add("src", this.ResolveClientUrl("~/Scripts/jquery-1.11.3.min.js"));
        scriptJQuery.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(scriptJQuery);


        HtmlGenericControl scriptBootstrap = new HtmlGenericControl("script");
        scriptBootstrap.Attributes.Add("src", this.ResolveClientUrl("~/Scripts/bootstrap.minv4.js"));
        scriptBootstrap.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(scriptBootstrap);


        HtmlGenericControl scriptjssor = new HtmlGenericControl("script");
        scriptjssor.Attributes.Add("src", this.ResolveClientUrl("~/Scripts/jssor.slider-27.1.0.min.js"));
        scriptjssor.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(scriptjssor);


        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/Scripts/main.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);


        HtmlGenericControl scripthoverIntent = new HtmlGenericControl("script");
        scripthoverIntent.Attributes.Add("src", this.ResolveClientUrl("~/Scripts/hoverIntent.js"));
        scripthoverIntent.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(scripthoverIntent);






        HtmlGenericControl scriptFarsi = new HtmlGenericControl("script");
        scriptFarsi.Attributes.Add("src", this.ResolveClientUrl("~/Scripts/farsi.js"));
        scriptFarsi.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(scriptFarsi);




        #endregion

        //BOLNews NewsBOL = new BOLNews();
        //rptNewsTicker.DataSource = NewsBOL.GetLatestTelexNews(10);
        //rptNewsTicker.DataBind();

        //if (rptNewsTicker.Items.Count == 0)
        //    pnlTickNews.Visible = false;

        if (Session["UserCode"] != null)
        {
            pnlLoggedUser.Visible = true;
            pnlUserLogin.Visible = false;

            if(Session["UserFullName"] != null)
                lblUserInfo.Text = Session["UserFullName"].ToString();

        }
        DateTimeMethods dtm = new DateTimeMethods();
        DateTime IranDate = DateTime.Now;
        ltrPersianDate.Text = Tools.ChangeEnc(dtm.GetPersianLongDate(DateTime.Now));
        lblTime.Text = Tools.ChangeEnc( DateTime.Now.Hour + ":" + DateTime.Now.Minute);

        //BOLSpeeches SpeechesBOL = new BOLSpeeches();
        //Ranjbaran.Old_App_Code.DAL.vSpeeches LatestSpeech = SpeechesBOL.GetLatest();
        //if (LatestSpeech != null)
        //    ltrTodayWord.Text = Tools.FormatString(LatestSpeech.Title);
    }
}
