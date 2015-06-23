using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testert
{
    class Cor
    {
        public double r;
        public double g;
        public double b;
        public Cor(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Cor(Color _cor)
        {
            r = _cor.R / 255.0;
            g = _cor.G / 255.0;
            b = _cor.B / 255.0;
        }

        public static Cor operator +(Cor a, double v)
        {
            return new Cor(a.r + v, a.g + v, a.b + v);
        }

        public static Cor operator *(Cor a, double v)
        {
            return new Cor(a.r * v, a.g * v, a.b * v);
        }

        public static Cor operator +(Cor a, Cor b)
        {
            return new Cor(a.r + b.r, a.g + b.g, a.b + b.b);
        }

        public static Cor operator *(Cor a, Cor b)
        {
            return new Cor(a.r * b.r, a.g * b.g, a.b * b.b);
        }

        public Color ToColor()
        {
            var ir = (int)(r * 255);
            var ig = (int)(g * 255);
            var ib = (int)(b * 255);
            if (ir > 255) ir = 255;
            if (ig > 255) ig = 255;
            if (ib > 255) ib = 255;
            return Color.FromArgb(ir, ig, ib);
        }
    }
}
