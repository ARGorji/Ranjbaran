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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;


public partial class Products_EditProducts : EditForm<Products>
{
    private string BaseID = "Products";
    IBaseBOL<Products> BaseBOL;

    public bool DuplicateHasBeenChecked
    {
        set
        {
            ViewState["DuplicateHasBeenChecked"] = value;
        }
        get
        {
            if (ViewState["DuplicateHasBeenChecked"] != null)
                return Convert.ToBoolean(ViewState["DuplicateHasBeenChecked"]);
            else
            {
                ViewState["DuplicateHasBeenChecked"] = false;
                return false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Tab Pages
        //if (!NewMode)
        //     ShowDetails();
        //else
        //{
        //     RadMultiPage1.SelectedIndex = 0;
        //     tsProducts.Tabs[0].Selected = true;
        //}
        #endregion
        BOLClass = new BOLProducts();

        if ((Code == null) && (!NewMode)) return;
        if (!Page.IsPostBack)
        {
            if (!NewMode)
                ShowDetails();
            else
                chkActive.Checked = true;

            #region Fill Combo
            cboHCProductAvailabilityCode.DataSource = new BOLHardCode().GetHCDataTable("HCProductAvailabilities");

            #endregion
            if (!NewMode)
            {
                LoadData((int)Code);
            }
        }


    }
    protected void btnConfirmDuplicate_Click(object sender, EventArgs e)
    {
        DuplicateHasBeenChecked = true;
        DoSave(null, null);
    }

    

    protected void DoSave(object sender, EventArgs e)
    {
        try
        {
            if (NewMode)
            {
                if (!DuplicateHasBeenChecked)
                {
                    msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Warning;
                    msgBox.Text = "محصولات زیر شبیه نام این محصول در این گروه قبلا وارد شده اند";
                    int DuplicateCount =  ShowDuplicateProducts();
                    if (DuplicateCount > 0)
                    {
                        pnlDuplicateProducts.Visible = true;
                        return;
                    }
                }
            }
            int ReturnCode = SaveControls("~/Main/Default.aspx?BaseID=" + BaseID);
            Products CurProduct;
            BOLProducts ProductsBOL = new BOLProducts();
            if (NewMode && ReturnCode != -1)
            {
                
                NewMode = false;
                Code = ReturnCode;
                ShowDetails();
            }

            if (ReturnCode != -1)
            {
                CurProduct = ((IBaseBOL<Products>)ProductsBOL).GetDetails(ReturnCode);
                if (uplLargePicFile.UploadedFiles.Count > 0 || string.IsNullOrEmpty( CurProduct.SmallPicFile))
                {
                    Guid newGd = Guid.NewGuid();
                    string RandSmallPic = newGd.ToString().Replace("-", "") + ".jpg";

                    SavePic(CurProduct.LargePicFile, RandSmallPic, HttpContext.Current.Request.MapPath("~/Files/Products/Small/"), 125);
                    ProductsBOL.ChangeSmallPic(ReturnCode, "/Files/Products/Small/" + RandSmallPic);
                }
                //Response.Redirect("~/Admin/Main/Default.aspx?BaseID=Products");
                GoToList(null, null);
            }

        }
        catch(Exception err)
        {
            msgBox.Text = "بروز خطا " + err.Message;
        }
    }

    private int ShowDuplicateProducts()
    {
        int? CatCode = treProductCatCode.Code;
        BOLProducts ProductsBOL = new BOLProducts();
        rptDuplicateProducts.DataSource = ProductsBOL.GetDuplicateProducts(txtFaTitle.Text, CatCode);
        rptDuplicateProducts.DataBind();
        return rptDuplicateProducts.Items.Count;
    }
    public void SavePic(string FileName, string RandName, string SavePath, int MaxAllowWidth)
    {
        string FirstChar = RandName.Substring(0, 1);
        int StaticVal = MaxAllowWidth;
        int NewWidth;
        int NewHeight;
        Graphics oGraphics;

        System.Drawing.Image image = new Bitmap(HttpContext.Current.Request.MapPath("~/") + FileName);
        if (image.Width > MaxAllowWidth)
        {
            int width = image.Width;
            int height = image.Height;
            //if (width > height)
            //{
            NewWidth = StaticVal;
            NewHeight = (StaticVal * height) / width;
            //}
            //else
            //{
            //    NewHeight = StaticVal;
            //    NewWidth = (StaticVal * width) / height;
            //}

            System.Drawing.Image BulkBmp = new Bitmap(NewWidth, NewHeight);
            oGraphics = Graphics.FromImage(BulkBmp);

            oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            oGraphics.DrawImage(
                image,
                new Rectangle(0, 0, NewWidth, NewHeight),
                // destination rectangle 
                0,
                0,           // upper-left corner of source rectangle
                width,       // width of source rectangle
                height,      // height of source rectangle
                GraphicsUnit.Pixel);

            BulkBmp.Save(SavePath + "\\" + RandName, ImageFormat.Jpeg);
            oGraphics.Dispose();
        }
        else
            File.Copy(HttpContext.Current.Request.MapPath("~/Files/Products/") + FileName, HttpContext.Current.Request.MapPath("~/Files/Products/Small/") + RandName);

    }

    private void ShowDetails()
    {
        string Tab1Click = "BrowseObj1.ShowDetail('ProductRelatedPros', '" + Code + "', true,'BrowseObj1');";

        //Tab1.Attributes.Add("onclick", Tab1Click);

        Tools.SetClientScript(this, "ActiveTab1", Tab1Click);
    }
}
