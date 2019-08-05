using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PicTools
/// </summary>
public class PicTools
{
	public PicTools()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void SavePic(FileUpload FileUpload1, string RandName, string SavePath,int MaxAllowWidth)
    {
        string FirstChar = RandName.Substring(0, 1);
        int StaticVal = MaxAllowWidth;
        int NewWidth;
        int NewHeight;
        Graphics oGraphics;

        System.Drawing.Image image = new Bitmap(FileUpload1.PostedFile.InputStream);
        if (image.Width > MaxAllowWidth)
        {
            int width = image.Width;
            int height = image.Height;
            if (width > height)
            {
                NewWidth = StaticVal;
                NewHeight = (StaticVal * height) / width;
            }
            else
            {
                NewHeight = StaticVal;
                NewWidth = (StaticVal * width) / height;
            }

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
            FileUpload1.SaveAs(SavePath + "\\" + RandName);

    }

    public string ResizeAndSave(string FullPath, string SavePath, int MaxAllowWidth)
    {
        int StaticVal = MaxAllowWidth;
        int NewWidth;
        int NewHeight;
        Graphics oGraphics;

        System.Drawing.Image image = new Bitmap(FullPath);
        if (image.Width > MaxAllowWidth)
        {
            int width = image.Width;
            int height = image.Height;
            if (width > height)
            {
                NewWidth = StaticVal;
                NewHeight = (StaticVal * height) / width;
            }
            else
            {
                NewHeight = StaticVal;
                NewWidth = (StaticVal * width) / height;
            }

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

            BulkBmp.Save(SavePath, ImageFormat.Jpeg);
            oGraphics.Dispose();
            return SavePath;
        }
        else
            return FullPath;

    }


    public string ResizeAndSave(string FullPath, string SavePath, int NewWidth, int NewHeight)
    {
        try
        {
            Graphics oGraphics;

            System.Drawing.Image image = new Bitmap(FullPath);

            int width = image.Width;
            int height = image.Height;

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

            BulkBmp.Save(SavePath, ImageFormat.Jpeg);
            oGraphics.Dispose();
            return SavePath;
        }
        catch
        {
            return "";
        }
    }

}
