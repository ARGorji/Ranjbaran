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
using Ranjbaran.Old_App_Code.DAL;

public partial class AccessLevel_ajxGroupResources : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strGroupCode = Request["GroupCode"];
        string strResourceCode = Request["ResourceCode"];
        string strAccessType = Request["AccessType"];
        string ApplyToChildNodes = Request["ApplyToChildNodes"];

        try
        {
            int GroupCode = Convert.ToInt32(strGroupCode);
            int ResourceCode = Convert.ToInt32(strResourceCode);

            BOLGroupResources GroupResourcesBOL = new BOLGroupResources(GroupCode);
            GroupResources MyGroup = GroupResourcesBOL.GetByGroupandResourceCode(GroupCode, ResourceCode);

            int AccessType;
            if (strAccessType != "")
                AccessType = Convert.ToInt32(strAccessType);
            else
            {
                if (MyGroup != null)
                    Response.Write(ResourceCode + ";" + MyGroup.AccessType);
                else
                    Response.Write(ResourceCode + ";0");
                return;
            }

            if (ApplyToChildNodes == "0") //Get All access Types to this child and child nodes
            {
                GroupResourcesBOL.AccessType = AccessType;
                GroupResourcesBOL.GroupCode = GroupCode;
                GroupResourcesBOL.ResourceCode = ResourceCode;
                if (AccessType > 0)
                {
                    if (MyGroup != null)
                    {
                        GroupResourcesBOL.Code = MyGroup.Code;
                        GroupResourcesBOL.SaveChanges(false);
                    }
                    else
                    {
                        GroupResourcesBOL.SaveChanges(true);
                    }
                }
                else
                {
                    if (MyGroup != null)
                    {
                        ((IBaseBOL<GroupResources>)GroupResourcesBOL).DeleteRecord(MyGroup.Code.ToString());
                    }
                }
            }
            else if(ApplyToChildNodes == "1")
            {
                GroupResourcesBOL.GetAccess(AccessType, GroupCode, ResourceCode);
            }
            Response.Write("Success");
        }
        catch
        {
            Response.Write("ERROR");

        }

    }
}
