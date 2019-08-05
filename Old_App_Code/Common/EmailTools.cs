using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ranjbaran.Old_App_Code.DAL;
using System.Configuration;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for EmailTools
/// </summary>
public class EmailTools
{
	public EmailTools()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool IsValidEmail(string CurEmail)
    {
        string _pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        Regex r = new Regex(_pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        Match m = r.Match(CurEmail);
        if (m.Success)
            return true;
        else
            return false;
    }
    public string LoadTemplate(int Code)
    {
        UtilDataContext dc = new UtilDataContext();
        EmailTemplates CurTemplate = dc.EmailTemplates.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurTemplate != null)
            return CurTemplate.Template;
        else
            return null;
    }

    public string LoadTemplate(string Title)
    {
        UtilDataContext dc = new UtilDataContext();
        EmailTemplates CurTemplate = dc.EmailTemplates.SingleOrDefault(p => p.Title.Equals(Title));
        if (CurTemplate != null)
            return CurTemplate.Template;
        else
            return null;
    }

}
