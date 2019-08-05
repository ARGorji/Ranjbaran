using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;


	/// <summary>
	/// Summary description for Imager.
	/// </summary>
	public partial class Imager : System.Web.UI.Page
	{
		public bool ThumbnailCallback()
		{
			return false;
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			string L = Request["l"];
            string StaticHW = Request["StaticHW"];

            string StaticWidth = Request["StaticWidth"];
            string StaticHeight = Request["StaticHeight"];

            //Response.ContentType = "images/jpeg";
			System.Drawing.Image oI;
			Graphics oGraphics;
			int StaticVal = 100;
            if (StaticHW == null)
                StaticVal = 100;
            else
                StaticVal = Convert.ToInt32(StaticHW);

			if(L == "1")
				StaticVal = 140;
			int NewHeight = StaticVal;
			int NewWidth = StaticVal;

			string ImgFilePath = "";

            ImgFilePath = Request["ImgFilePath"];
            ImgFilePath = Server.UrlDecode(Tools.Decode(ImgFilePath));

			try
			{
                ImgFilePath = ImgFilePath;
                oI = System.Drawing.Image.FromFile(Server.MapPath(ImgFilePath)); 
				System.Drawing.Image image = new Bitmap(oI);

				//			oI.Save(Response.OutputStream,ImageFormat.Jpeg);
				int width = oI.Width;
				int height = oI.Height;
				if(width > height)
				{
					NewWidth = StaticVal;
					NewHeight = (StaticVal*height)/width;
				}
				else
				{
					NewHeight = StaticVal;
					NewWidth = (StaticVal*width)/height;
				}

                if (StaticWidth != null)
                {
                    NewWidth = Convert.ToInt32(StaticWidth);
                    //NewHeight = (StaticVal * height) / width;
                }
                if (StaticHeight != null)
                {
                    NewHeight = Convert.ToInt32(StaticHeight);
                    //NewHeight = (StaticVal * height) / width;
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
					GraphicsUnit.Pixel );

				BulkBmp.Save(Response.OutputStream,ImageFormat.Jpeg);
                oI.Dispose();
				oGraphics.Dispose();
			}
			catch(Exception e1)
			{

				Response.Redirect("~/images/spacer.gif");
			}


/*			
			// Draw the image with no shrinking or stretching.
			oGraphics.DrawImage(
				image,
				new Rectangle(10, 10, width, height),  // destination rectangle  
				0,
				0,           // upper-left corner of source rectangle
				width,       // width of source rectangle
				height,      // height of source rectangle
				GraphicsUnit.Pixel,
				null);
			//Response.ContentType = "image/gif";
			
			// Shrink the image using low-quality interpolation. 
			oGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			oGraphics.DrawImage(
				image,
				new Rectangle(10, 250, (int)(0.6 * width), (int)(0.6*height)), 
				// destination rectangle 
				0,
				0,           // upper-left corner of source rectangle
				width,       // width of source rectangle
				height,      // height of source rectangle
				GraphicsUnit.Pixel);

			// Shrink the image using medium-quality interpolation.
			oGraphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
			oGraphics.DrawImage(
				image,
				new Rectangle(150, 250, (int)(0.6 * width), (int)(0.6 * height)),
				// destination rectangle 
				0,
				0,           // upper-left corner of source rectangle
				width,       // width of source rectangle
				height,      // height of source rectangle
				GraphicsUnit.Pixel);
*/
			// Shrink the image using high-quality interpolation.
/*
			
			int ImgWidth = oI.Width ;
			int ImgHeight = oI.Height;
			int NewHeight = 0;
			NewHeight = (80*ImgHeight)/ImgWidth;


			oGraphics = Graphics.FromImage(oI);
//			bm = new Bitmap(80, 55, oGraphics);

			
			bm = new Bitmap(oI, 80, NewHeight);
			System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
			System.Drawing.Image myThumbnail = bm.GetThumbnailImage(120, 120, myCallback, IntPtr.Zero);

			oGraphics.DrawImage(myThumbnail , 80, 55);

			//			objImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
			myThumbnail.Save(Response.OutputStream,ImageFormat.Gif );
///////////////////
/*
			
			oItemp=oI;
			oImg=new Bitmap(oI.Width,oI.Height,PixelFormat.Format24bppRgb);
			oGraphics = Graphics.FromImage(oImg);

			string XPos, YPos;
			string[] tempArrX=null;
			string[] tempArrY=null;

			XPos = Request.QueryString["x"].ToString();
			YPos = Request.QueryString["y"].ToString();

			if (XPos!=string.Empty && YPos!=string.Empty)
			{
				string delimStr = ",";
				char [] delimiter = delimStr.ToCharArray();

				tempArrX=XPos.Split(delimiter);
				tempArrY=YPos.Split(delimiter);

				//we store the x and y positions clicked everytime
				//by user in a hidden field. 
				//so if user clicks 3 times.. we enlarge the image thrice.
				//appropriately everytime at the portions user clicked.
				int iIndex;
				for(iIndex=0;iIndex<tempArrX.Length ;iIndex++)
				{
					//the logic followed here is.. 
					//get the portion around the click areaa..
					//60% of the size of actual image
					/////
					//enlarge this portion to the size of the image itself.

					//get the 60% area around the clik
					float iPortionWidth=(1.60f*oI.Width);
					float iPortionHeight=(1.60f*oI.Height);

					//calculate top x,y of the portion to enlarge.
					float iStartXpos = float.Parse(tempArrX[iIndex])-(iPortionWidth/2);
					float iStartYPos = float.Parse(tempArrY[iIndex])-(iPortionHeight/2);
							
					//set destination and source rectangle areas
					RectangleF desRect = new RectangleF(
						0,
						0,
						oI.Width,
						oI.Height);

					RectangleF sourceRect = new RectangleF(
						iStartXpos,
						iStartYPos,
						iPortionWidth,
						iPortionHeight);

					//get the portion of image(defined by sourceRect)
					//and enlarge it to desRect size...gives a zoom effect.
					////if image has high resolution.. effect will be good.
					oGraphics.DrawImage(oItemp,desRect,sourceRect,GraphicsUnit.Pixel);

					//copy the enlarged portion into oItemp 
					//so that further zooming operation uses this image.
					////this is to ensure that when image is enlarged multiple times
					////it doesnt enlarge from the original everytime.
					IntPtr hBitmap = oImg.GetHbitmap(Color.Blue);  //create pointer to bitmap
					oItemp=System.Drawing.Image.FromHbitmap(hBitmap);  //use pointer and load bitmap into oItemp
				}						
			}				
			oGraphics = Graphics.FromImage(oImg);
			oGraphics.DrawImage(oItemp,oI.Width, oI.Height);

			oGraphics.DrawImage(oI, 80, 55);
			bm = new Bitmap(80, 55, oGraphics);

			
			bm.Save(Response.OutputStream,ImageFormat.Gif);
			oImg.Dispose();
			oI.Dispose();
			oItemp.Dispose();
*/			
////////////////////



		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
