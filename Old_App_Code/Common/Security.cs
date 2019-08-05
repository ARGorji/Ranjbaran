using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{
	public Security()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void Check()
    {
        HttpContext context = HttpContext.Current;
        if (context.Session["UserCode"] == null)
        {
            HttpContext.Current.Response.Redirect("~/Users/Login.aspx");
            HttpContext.Current.Response.End();
        }

    }
}