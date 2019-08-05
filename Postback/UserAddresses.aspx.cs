using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace Ranjbaran.Postback
{
    public partial class UserAddresses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Url = Request.Url.Host.Trim().ToLower();
            string HTTP_REFERER = Request.ServerVariables["HTTP_REFERER"];
            if (HTTP_REFERER != null && HTTP_REFERER.IndexOf(Url) != -1)
            {
                Security.Check();
                string input = Request.Form["i"];
                switch (input)
                {
                    case "1":
                        SubmitNewAddress();
                        break;
                    case "2":
                        DeleteAddress();
                        break;

                }
            }
        }

        private void DeleteAddress()
        {
            string Code = Request["Code"];

            int UserCode = Convert.ToInt32(Session["UserCode"]);

            BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(1);
            bool SaveResult = UserAddressesBOL.DeleteAddress(UserCode, Code);

            op_result _op_result = new op_result();

            if (SaveResult)
            {
                _op_result.result = "آدرس  با موفقیت حذف شد.";
                _op_result.success = "1";
            }
            else
            {
                _op_result.result = "بروز خطا در ثبت آدرس جدید.";
                _op_result.success = "0";
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize((object)_op_result);
            Response.Write(json);
            Response.End();
        }
        public void SubmitNewAddress()
        {
            string NewAddressFullName = Request["NewAddressFullName"];
            string NewAddressCellPhone = Request["NewAddressCellPhone"];
            string NewAddressTel = Request["NewAddressTel"];
            string NewAddressAddress = Request["NewAddressAddress"];
            string NewAddressPostalCode = Request["NewAddressPostalCode"];
            string NewAddressProvinceCode = Request["NewAddressProvinceCode"];
            string NewAddressCityCode = Request["NewAddressCityCode"];

            int UserCode = Convert.ToInt32(Session["UserCode"]);

            BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(1);
            bool SaveResult = UserAddressesBOL.Insert(UserCode, NewAddressFullName, NewAddressCellPhone, NewAddressTel, NewAddressAddress, NewAddressPostalCode, NewAddressProvinceCode, NewAddressCityCode);

            op_result _op_result = new op_result();

            if (SaveResult)
            {
                _op_result.result = "آدرس جدید با موفقیت ثبت شد.";
                _op_result.success = "1";
            }
            else
            {
                _op_result.result = "بروز خطا در ثبت آدرس جدید.";
                _op_result.success = "0";
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize((object)_op_result);
            Response.Write(json);
            Response.End();
        }
    }


}