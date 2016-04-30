using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DrawHelper
{
    public static class DrawHelper
    {
        public static Image DrawBorder(Size size, Color color)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            bitmap.MakeTransparent();
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawRectangle(new Pen(color, 1), 0, 0, size.Width-1, size.Height-1);
            return bitmap;
        }

        public static Image DrawShelf(Size size, Color color)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            bitmap.MakeTransparent();
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(color, 1);
            Point start = new Point();
            Point end = new Point();
            for (int i = 0; i < 9; i++)
            {
                start.X = 41 * i;
                start.Y = 0;
                end.X = 41 * i;
                end.Y = size.Height;
                g.DrawLine(pen, start, end);

                start.X = 0;
                start.Y = 41 * i;
                end.X = size.Width;
                end.Y = 41 * i;
                g.DrawLine(pen, start, end);
            }
            return bitmap;
        }
    }
}
