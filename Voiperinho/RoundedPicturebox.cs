using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Voiperinho
{
    public class RoundedPicturebox : PictureBox
    {
        public RoundedPicturebox() { }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Brush imgBrush = null;
            GraphicsPath gPath = new GraphicsPath();

            try
            {
                Bitmap image = new Bitmap(this.Image, new Size(this.Width, this.Height));
                imgBrush = new TextureBrush(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Bitmap image = new Bitmap(this.Width, this.Height, PixelFormat.Format24bppRgb);

                using (Graphics g = Graphics.FromImage(image))
                {
                    g.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);
                    image = new Bitmap(this.Width, this.Height);
                }

                imgBrush = new TextureBrush(image);
            }

            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gPath.AddEllipse(this.DisplayRectangle);

            pe.Graphics.FillPath(imgBrush, gPath);
        }
    }
}
