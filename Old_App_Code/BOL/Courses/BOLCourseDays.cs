﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using Ranjbaran.Old_App_Code.DAL;
  
public class BOLCourseDays : BaseBOLCourseDays, IBaseBOL<CourseDays>
{
    public BOLCourseDays(params int[] MCodes): base(MCodes)
    {
        
    }
    public IList CheckBusinessRules()
    {
        var messages = new List<string>();
        
        #region Business Rules
        //Example
        //if (string.IsNullOrEmpty(this.FirstName))
        //    messages.Add("Please fill FirstName.");

        #endregion
        return messages;
    }

    internal object GetDays(int CourseCode)
    {
        return dataContext.vCourseDays.Where(p => p.CourseCode.Equals(CourseCode));
    }
}
