using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserControls_PagerToolbar : System.Web.UI.UserControl
{
    int FrameSize = 9;
    protected int _pageNo;
    public int PageNo
    {
        get
        {
            return _pageNo;
        }
        set
        {
            _pageNo = value;
        }
    }
    protected int _pageCount;
    public int PageCount
    {
        get
        {
            return _pageCount;
        }
        set
        {
            _pageCount = value;
        }
    }
    protected string _concatUrl;
    public string ConcatUrl
    {
        get
        {
            return _concatUrl;
        }
        set
        {
            _concatUrl = value;
        }
    }
    protected string ReqPath;

    public void PageBind()
    {
        if (_pageCount < 2)
        {
            this.Visible = false;
            return;
        }
        else
            this.Visible = true;

        ReqPath = Page.Request.Path.Replace("default.aspx", "");
        DataTable dtPaging = new DataTable();
        DataColumn dcPageNo = new DataColumn("PageNo");
        dtPaging.Columns.Add(dcPageNo);
        DataRow dr;
        int StartPage = GetStartFrame(_pageNo, FrameSize, _pageCount);
        int StopPage = GetStopFrame(_pageNo, FrameSize, _pageCount);

        hplFirstPage.NavigateUrl = ReqPath + "?PageNo=1" + _concatUrl;
        hplLastPage.NavigateUrl = ReqPath + "?PageNo=" + _pageCount + ConcatUrl;
        if (_pageNo > 1)
            hplPrePage.NavigateUrl = ReqPath + "?PageNo=" + (_pageNo - 1) + ConcatUrl;
        else
        {
            hplPrePage.Visible = false;
            hplFirstPage.Visible = false;
        }

        if (_pageNo < _pageCount)
            hplNextPage.NavigateUrl = ReqPath + "?PageNo=" + (_pageNo + 1) + ConcatUrl;
        else
        {
            hplNextPage.Visible = false;
            hplLastPage.Visible = false;
        }


        for (int i = StartPage; i <= StopPage; i++)
        {
            dr = dtPaging.NewRow();
            dr["PageNo"] = i;
            dtPaging.Rows.Add(dr);
        }
        rptPaging.DataSource = dtPaging;
        rptPaging.DataBind();
    }
    protected void PagingDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string CurNum = DataBinder.Eval(e.Item.DataItem, "PageNo").ToString();
            HyperLink hplPageLink = (HyperLink)e.Item.FindControl("hplNum");
            if (CurNum == _pageNo.ToString())
                hplPageLink.CssClass = "current";
            else 
                hplPageLink.NavigateUrl = ReqPath + "?PageNo=" + CurNum + _concatUrl;


        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (_pageCount < 2)
        {
            this.Visible = false;
            return;
        }
        else
            this.Visible = true;

    }

    private int GetStartFrame(int CurrentPage, int FrameSize, int TotalPageCount)
    {
        int Result = 1;
        Result = CurrentPage - FrameSize;
        if (Result <= 0)
            Result = 1;
        return Result;

    }

    private int GetStopFrame(int CurrentPage, int FrameSize, int TotalPageCount)
    {
        int Result = 1;
        Result = CurrentPage + FrameSize - 1;
        if (Result > TotalPageCount)
            Result = TotalPageCount;
        return Result;
    }
}
