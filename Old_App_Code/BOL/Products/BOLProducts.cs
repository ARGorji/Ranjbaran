using System;
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
using DataAccess;

public class BOLProducts : BaseBOLProducts, IBaseBOL<Products>
{
    string CatList = "";
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

    public IQueryable<vProducts> GetProducts(int? CatCode, int PageSize, int PageNo, bool OnlyShowAvailPros)
    {
        int SkipCount = (PageNo - 1) * PageSize;
        IQueryable<vProducts> Result = dataContext.vProducts.Where(p => p.Active.Equals(true) && p.SmallPicFile != null);
        //if(CatCode == null)
        //    return dataContext.vProducts.Where(p => p.Active.Equals(true) && p.SmallPicFile != null).OrderBy(p => p.ShowOrder).Skip(SkipCount).Take(PageSize);
        //else
        //    return dataContext.vProducts.Where(p => p.Active.Equals(true) && p.SmallPicFile != null && p.ProductCatCode.Equals(CatCode)).OrderBy(p => p.ShowOrder).Skip(SkipCount).Take(PageSize);

        if (CatCode != null && CatCode != 0)
            Result = Result.Where(p => p.ProductCatCode.Equals(CatCode));
        if (OnlyShowAvailPros)
            Result = Result.Where(p => p.HCProductAvailabilityCode.Equals(1));

        Result = Result.OrderByDescending(p => p.Code);
        Result = Result.Skip(SkipCount).Take(PageSize);
        return Result;
    }

    public int GetProductsCount(int? CatCode, bool OnlyShowAvailPros)
    {
        //if (CatCode == null)
        //    return dataContext.vProducts.Where(p => p.Active.Equals(true) && p.SmallPicFile != null).Count();
        //else
        //    return dataContext.vProducts.Where(p => p.Active.Equals(true) && p.ProductCatCode.Equals(CatCode) && p.SmallPicFile != null).Count();
        IQueryable<vProducts> Result = dataContext.vProducts.Where(p => p.Active.Equals(true) && p.SmallPicFile != null && p.HCProductAvailabilityCode.Equals(1));
        if (CatCode != null && CatCode != 0)
            Result = Result.Where(p => p.ProductCatCode.Equals(CatCode));
        if (OnlyShowAvailPros)
            Result = Result.Where(p => p.HCProductAvailabilityCode.Equals(1));

        return Result.Count();
    }

    public IQueryable<vProducts> SearchProducts(string Keyword, int PageSize, int PageNo)
    {
        Keyword = Keyword.Trim();
        while (Keyword.IndexOf("  ") > -1)
        {
            Keyword = Keyword.Replace("  ", " ");
        }
        //string[] KeywordArray = Keyword.Split(' ');

        IQueryable<vProducts> Result = dataContext.vProducts.Where(p => p.Active.Equals(true));
        foreach (var SingleKeyword in Keyword.Split(' '))
        {
            Result = Result.Where(p => p.Description.Contains(SingleKeyword));
        }

        IQueryable<vProducts> ResultTitle = dataContext.vProducts.Where(p => p.Active.Equals(true));
        foreach (var SingleKeyword in Keyword.Split(' '))
        {
            ResultTitle = ResultTitle.Where(p => p.FaTitle.Contains(SingleKeyword) || p.EnTitle.Contains(SingleKeyword));
        }

        //Result = Result.Where(p => p.Title.Contains(Keyword) || p.Description.Contains(Keyword));
        Result = ResultTitle.Union(Result);

        int SkipCount = (PageNo - 1) * PageSize;
        Result = Result.Skip(SkipCount).Take(PageSize);

        return Result;
    }

    public int SearchProductsCount(string Keyword)
    {
        IQueryable<vProducts> Result = dataContext.vProducts.Where(p => p.Active.Equals(true));
        foreach (var SingleKeyword in Keyword.Split(' '))
        {
            Result = Result.Where(p => p.Description.Contains(SingleKeyword));
        }

        IQueryable<vProducts> ResultTitle = dataContext.vProducts.Where(p => p.Active.Equals(true));
        foreach (var SingleKeyword in Keyword.Split(' '))
        {
            ResultTitle = ResultTitle.Where(p => p.FaTitle.Contains(SingleKeyword) || p.EnTitle.Contains(SingleKeyword));
        }

        Result = ResultTitle.Union(Result);

        return Result.Count();
    }

    public void IncrementView(int Code)
    {
        Products CurProduct = dataContext.Products.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurProduct.ViewNum == null)
            CurProduct.ViewNum = 1;
        else
            CurProduct.ViewNum = CurProduct.ViewNum + 1;
        dataContext.SubmitChanges();
    }


    internal void ChangeSmallPic(int Code, string SmallPic)
    {
        Products CurProduct = dataContext.Products.SingleOrDefault(p => p.Code.Equals(Code));
        CurProduct.SmallPicFile = SmallPic;
        dataContext.SubmitChanges();
    }

    internal void UpdateProduct(int ProductCode, int? SelectedCatCode, int CompanyCode, string Price, string Weight, int HCProductAvailabilitCode, int? ProductCatCode)
    {
        Products CurProduct = dataContext.Products.SingleOrDefault(p => p.Code.Equals(ProductCode));
        if (SelectedCatCode != null)
            CurProduct.ProductCatCode = SelectedCatCode;
        if (!string.IsNullOrEmpty(Weight))
            CurProduct.Weight = Convert.ToInt32(Weight);
        if (!string.IsNullOrEmpty(Price))
            CurProduct.Price = Convert.ToInt32(Price);
        CurProduct.HCProductAvailabilityCode = HCProductAvailabilitCode;
        CurProduct.ProductCatCode = ProductCatCode;
        dataContext.SubmitChanges();
    }


    internal object GetDuplicateProducts(string Title, int? CatCode)
    {
        return dataContext.Products.Where(p => p.FaTitle.Contains(Title) && p.ProductCatCode.Equals(CatCode)).Take(10);
    }


    internal string GetProductavailStatus(int Code)
    {
        vProducts CurProduct = dataContext.vProducts.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurProduct != null)
        {
            return CurProduct.Availabality;
        }
        else
            return "";
    }


    internal object GetProductsByViewNum(int TakeCount)
    {
        return dataContext.Products.OrderByDescending(p => p.ViewNum).Take(TakeCount);
    }

    internal void IncProVisits(int ProductCode)
    {
        try
        {
            ProductVisits NewRecord = new ProductVisits();
            dataContext.ProductVisits.InsertOnSubmit(NewRecord);
            NewRecord.ProductCode = ProductCode;
            NewRecord.VisitDate = DateTime.Now;
            dataContext.SubmitChanges();
        }
        catch
        {
        }
    }

    internal DataTable GetProductsByViewNum(DateTime? FromDate, DateTime? ToDate, int TakeCount)
    {
        string cnStr = ConfigurationManager.ConnectionStrings["RanjbaranConnectionString"].ConnectionString;
        SQLServer dal = new SQLServer(cnStr);

        string SQLStr= @"SELECT     TOP (100) PERCENT dbo.ProductVisits.ProductCode, COUNT(dbo.ProductVisits.Code) AS ProCount, dbo.Products.Title
                        FROM         dbo.ProductVisits INNER JOIN
                                              dbo.Products ON dbo.ProductVisits.ProductCode = dbo.Products.Code
                        where 1=1 ";
        if(FromDate != null)
            SQLStr = SQLStr + @"AND ProductVisits.VisitDate > '" + FromDate + "'";
        if (ToDate != null)
            SQLStr = SQLStr + @"and ProductVisits.VisitDate < '" + ToDate + "'";
        SQLStr = SQLStr + @"GROUP BY dbo.ProductVisits.ProductCode, dbo.Products.Title ORDER BY ProCount DESC";
        DataSet ds = dal.runSQLDataSet(SQLStr, "Result");
        return ds.Tables[0];
    }

    internal IQueryable<vProducts> GetList(int PageNo, int PageSize, string Keyword, int? CatCode, int? CompanyCode, int SortType, int AscDesc)
    {
        int SkipCount = (PageNo - 1) * PageSize;

        IQueryable<vProducts> Result = dataContext.vProducts.Where(p=> p.Active.Equals(true));
        if (!string.IsNullOrEmpty(Keyword))
        {
            while (Keyword.IndexOf("  ") != -1)
            {
                Keyword = Keyword.Replace("  ", " ");
            }

            string[] KeywordArray = Keyword.Split(' ');
            foreach (var item in KeywordArray)
            {
                Result = Result.Where(p => p.EnTitle.Contains(item) || p.FaTitle.Contains(item) || p.Description.Contains(item));
            }

        }

        string strCatCodeList = GetInnerCats(CatCode);
        if (strCatCodeList.Length > 0)
        {
            if (strCatCodeList.Substring(strCatCodeList.Length - 1, 1) == ",")
                strCatCodeList = strCatCodeList.Substring(0, strCatCodeList.Length - 1);
        }
        string[] strCatCodeListArray = strCatCodeList.Split(',');

        //if (CatCode != null)
        //    Result = Result.Where(p => p.ProductCatCode.Equals(CatCode));

        if (CatCode != null)
            Result = Result.Where(p => strCatCodeListArray.Contains( p.ProductCatCode.ToString()));


        if (AscDesc == 2)//صعودی
        {
            if (SortType == 1)//پربازدیدترین
                Result = Result.OrderBy(p => p.ViewNum);
            else if (SortType == 2)//جدیدترین
                Result = Result.OrderBy(p => p.CreateDate);
            else if (SortType == 3)//محبوب ترین
                Result = Result.OrderBy(p => p.ViewNum);
            else if (SortType == 4)//پیشنهاد ویژه
                Result = Result.OrderBy(p => p.Special);
            else if (SortType == 5)//قیمت
                Result = Result.OrderBy(p => p.Price);
        }
        else
        {
            if (SortType == 1)//پربازدیدترین
                Result = Result.OrderByDescending(p => p.ViewNum);
            else if (SortType == 2)//جدیدترین
                Result = Result.OrderByDescending(p => p.CreateDate);
            else if (SortType == 3)//محبوب ترین
                Result = Result.OrderByDescending(p => p.ViewNum);
            else if (SortType == 4)//پیشنهاد ویژه
                Result = Result.OrderByDescending(p => p.Special);
            else if (SortType == 5)//قیمت
                Result = Result.OrderByDescending(p => p.Price);
        }


        return Result.Skip(SkipCount).Take(PageSize);
    }

    private string GetInnerCats(int? CatCode)
    {
        CatList = CatCode.ToString() + ",";
        var Result = dataContext.ProductCats.Where(p => p.MasterCode.Equals(CatCode));
        foreach (var item in Result)
        {
            CatList += GetInnerCats(item.Code);
            //CatList += item.Code + ",";
        }
        return CatList;
    }

    internal int GetListCount(string Keyword, int? CatCode, int? CompanyCode)
    {
        
        IQueryable<vProducts> Result = dataContext.vProducts;
        
        if (!string.IsNullOrEmpty(Keyword))
        {
            while (Keyword.IndexOf("  ") != -1)
            {
                Keyword = Keyword.Replace("  ", " ");
            }

            string[] KeywordArray = Keyword.Split(' ');
            foreach (var item in KeywordArray)
            {
                Result = Result.Where(p => p.EnTitle.Contains(item) || p.FaTitle.Contains(item) || p.Description.Contains(item));
            }

        }

        string strCatCodeList = GetInnerCats(CatCode);
        if (strCatCodeList.Length > 0)
        {
            if (strCatCodeList.Substring(strCatCodeList.Length - 1, 1) == ",")
                strCatCodeList = strCatCodeList.Substring(0, strCatCodeList.Length - 1);
        }
        string[] strCatCodeListArray = strCatCodeList.Split(',');

        //if (CatCode != null)
        //    Result = Result.Where(p => p.ProductCatCode.Equals(CatCode));

        if (CatCode != null)
            Result = Result.Where(p => strCatCodeListArray.Contains(p.ProductCatCode.ToString()));


        //if (CatCode != null)
        //    Result = Result.Where(p => p.ProductCatCode.Equals(CatCode));

        return Result.Count();
    }

    internal object ShowHomeProducts()
    {
        return dataContext.vProducts.Where(p => p.Special == true || p.IsNew == true || p.IsDiscount == true || p.IsMostSold == true);
    }

    internal object ShowSpecialProducts()
    {
        return dataContext.vProducts.Where(p => p.Special == true ).Take(5);
    }
    internal object ShowNewProducts()
    {
        return dataContext.vProducts.Where(p => p.IsNew == true).Take(5);
    }
    internal object ShowDiscountProducts()
    {
        return dataContext.vProducts.Where(p => p.IsDiscount == true).Take(5);
    }
    internal object ShowMostSoldProducts()
    {
        return dataContext.vProducts.Where(p => p.IsMostSold == true).Take(5);
    }



    internal Products GetDetails(int Code)
    {
        return dataContext.Products.SingleOrDefault(p => p.Code.Equals(Code));
    }

    internal object GetAllProducts()
    {
        return dataContext.vProducts;
    }
}
