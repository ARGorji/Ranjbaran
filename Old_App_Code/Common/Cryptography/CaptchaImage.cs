using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace App_Code.Cryptography
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class CaptchaImage
    {
        // Public properties (all read-only).
        public string Text
        {
            get { return this.text; }
        }
        public Color BackgroundColor
        {
            get { return this.bc; }
        }
        public Bitmap Image
        {
            get { return this.image; }
        }
        public int Width
        {
            get { return this.width; }
        }
        public int Height
        {
            get { return this.height; }
        }

        // Internal properties.
        private string text;
        private Color bc;
        private int width;
        private int height;
        private Bitmap image;

        public CaptchaImage(string s, Color bc, int width, int height)
        {
            this.text = s;
            this.bc = bc;
            this.width = width;
            this.height = height;
            this.GenerateImage();
        }

        ~CaptchaImage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                // Dispose of the bitmap.
                this.image.Dispose();
        }
        private readonly FontFamily[] fonts = {
                                         new FontFamily("Times New Roman"),
                                         new FontFamily("Georgia"),
                                         new FontFamily("Arial"),
                                         new FontFamily("Comic Sans MS")
                                     };
        private void GenerateImage()
        {
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);


            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush b = new SolidBrush(bc))
            {
                g.FillRectangle(b, rect);
            }

            // Set up the text font.
            int emSize = this.width*2/(text.Length + 1);
            FontFamily family = fonts[RNG.Next(fonts.Length - 1)];
            Font font = new Font(family, emSize);
            // Adjust the font size until the text fits within the image.
            SizeF measured = new SizeF(0, 0);
            SizeF workingSize = new SizeF(this.width, this.height);
            while (emSize > 2 &&
                   (measured = g.MeasureString(text, font)).Width > workingSize.Width ||
                   measured.Height > workingSize.Height)
            {
                font.Dispose();
                font = new Font(family, emSize -= 2);
            }

            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, (int) font.Style, font.Size, rect, format);

            // Set font color to a color that is visible within background color
            int bcR = Convert.ToInt32(bc.R);
            // This prevents font color from being near the bg color
            int red = RNG.Next(150), green = RNG.Next(150), blue = RNG.Next(150);
            while (red >= bcR && red - 20 <= bcR ||
                   red < bcR && red + 20 >= bcR)
            {
                red = RNG.Next(0, 255);
            }
            SolidBrush sBrush = new SolidBrush(Color.FromArgb(red, green, blue));
            g.FillPath(sBrush, path);

            // Iterate over every pixel
            double distort = RNG.Next(5, 20)*(RNG.Next(10) == 1 ? 1 : -1);

            // Copy the image so that we're always using the original for source color
            using (Bitmap copy = (Bitmap) bitmap.Clone())
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Adds a simple wave
                        int newX = (x + RNG.Next(2));
                        int newY = (y + RNG.Next(2));
                        if (newX < 0 || newX >= width) newX = 0;
                        if (newY < 0 || newY >= height) newY = 0;
                        bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }

                }
            }

            Random r = new Random();
            int temp = 0;
            for (int y = 0; y < height; y += 10)
                for (int x = 0; x < width; x += 10)
                {
                    if (temp + 10 < x)
                        temp += 10;
                    else temp = x;
                    bitmap.SetPixel(r.Next(temp, x), r.Next(y), Color.Blue);
                    bitmap.SetPixel(r.Next(temp,x), r.Next(y), Color.Green);
                    bitmap.SetPixel(r.Next(temp, x), r.Next(y), Color.Yellow);
                    bitmap.SetPixel(r.Next(temp, x), r.Next(y), Color.Pink);
                }


            // Clean up.
            font.Dispose();
            sBrush.Dispose();
            g.Dispose();

            // Set the image.
            this.image = bitmap;
        }
    }
}