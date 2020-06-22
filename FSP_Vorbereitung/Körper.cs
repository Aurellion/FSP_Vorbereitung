using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FSP_Vorbereitung
{
    class Körper
    {
        double x, y, vx, vy;
        Ellipse umriss;

        public Körper(double x, double y, double vx, double vy)
        {
            this.x = x;
            this.vx = vx;
            this.y = y;
            this.vy = vy;
            umriss = new Ellipse();
            umriss.Width = 5;
            umriss.Height = 5;
            umriss.Fill = Brushes.Magenta;
        }

        public void Bewegen(TimeSpan interval, Canvas ZF)
        {
            double t = interval.TotalSeconds;
            x += vx * t;
            y += vy * t;

            if (x > ZF.ActualWidth) x = 0;
            if (y > ZF.ActualHeight) y = 0;
            if (x < 0) x = ZF.ActualWidth;
            if (y < 0) y = ZF.ActualHeight;
        }

        public void Zeichnen(Canvas ZF)
        {
            ZF.Children.Add(umriss);
            Canvas.SetLeft(umriss, x);
            Canvas.SetTop(umriss, y);
        }

        public void Beschleunigen(bool schneller)
        {
            double faktor = schneller ? 1.1 : 0.9;
            vx *= faktor;
            vy *= faktor;
        }

        public void FarbeÄndern()
        {
            umriss.Fill = Brushes.Cyan;
        }
    }
}
