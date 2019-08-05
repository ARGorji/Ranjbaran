using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using Ranjbaran.Old_App_Code.DAL;

/// <summary>
/// Summary description for BOLExceptions
/// </summary>
public class BOLExceptions : ErrorLogs
{

    protected string TableOrViewName = "Exceptions";


    public static void Log(Exception ex, Dictionary<string, string> ControlsAndValues)
    {
        string ClientIP = HttpContext.Current.Request.UserHostAddress;
        int UserCode = HttpContext.Current.Session["UserCode"] != null ? Convert.ToInt32(HttpContext.Current.Session["UserCode"].ToString()) : 0;

        #region Browser Version and Properties
        HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
        string MyVersion = browser.Browser + " " + browser.Version;
        if (browser.EcmaScriptVersion == null || browser.EcmaScriptVersion.Major < 1)
            MyVersion += ";NO JAVASCRIPT";
        if (browser.Beta)
            MyVersion += ";Beta";
        if (!browser.Cookies)
            MyVersion += ";No Cookies";
        if (!browser.Frames)
            MyVersion += ";No Frame Support";
        if (browser.IsMobileDevice)
            MyVersion += ";Mobile Browser";


        #endregion
        #region Data

        StringBuilder sb = new StringBuilder("PhysicalPath= " + HttpContext.Current.Request.PhysicalPath + "\r\n");
        sb.Append("RawUrl= " + HttpContext.Current.Request.RawUrl + "\r\n");
        sb.Append("UserAgent= " + HttpContext.Current.Request.UserAgent + "\r\n");

        foreach (KeyValuePair<string, string> entry in ControlsAndValues)
            sb.Append(entry.Key + "=" + entry.Value + "\r\n");

        string Data = sb.ToString();

        #endregion


    }


    

}

