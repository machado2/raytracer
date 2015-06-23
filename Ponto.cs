using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    struct Ponto
    {

        public Ponto Copia()
        {
            return new Ponto(x, y, z);
        }

        //public double x { get; protected set; }
        //public double y { get;protected set; }
        //public double z { get;protected set; }

        public double x { get { return v[0]; } }
        public double y { get { return v[1]; } }
        public double z { get { return v[2]; } }

        public double this[int i]
        {
            get
            {
                    return v[i];
                /*
                if (i == 0)
                    return x;
                else if (i == 1)
                    return y;
                else if (i == 2)
                    return z;
                else
                    throw new ArgumentOutOfRangeException();*/
            }
        }

        double[] v;


        public Ponto(double x, double y, double z)
        {
            v = new double[] { x, y, z};
            //v = new double[3] { x, y, z };

            // this.x = x;
            // this.y = y;
            // this.z = z;
        }


        public double dot(Ponto b)
        {
            return x * b.x + y * b.y + z * b.z;
        }

        public Ponto cross(Ponto b)
        {
            double v1 = y * b.z - z * b.y;
            double v2 = z * b.x - x * b.z;
            double v3 = x * b.y - y * b.x;
            return new Ponto(v1, v2, v3);
        }

        public Ponto sub(Ponto b)
        {
            return new Ponto(x - b.x, y - b.y, z - b.z);
        }

        public Ponto soma(Ponto b)
        {
            return new Ponto(x + b.x, y + b.y, z + b.z);
        }

        public Ponto mult(double n)
        {
            return new Ponto(x * n, y * n, z * n);
        }

        public Ponto ParaTras()
        {
            return new Ponto(-x, -y, -z);
        }

        public Ponto Inverso()
        {
            double nx, ny, nz;
            if (x != 0)
                nx = 1 / x;
            else
                nx = 0;
            if (y != 0)
                ny = 1 / y;
            else 
                ny = 0;
            if (z != 0)
                nz = 1 / z;
            else
                nz = 0;
            return new Ponto(nx, ny, nz);
        }

        public Ponto Normaliza()
        {
            var m = Math.Sqrt(x * x + y * y + z * z);
            if (m == 0)
                return this;
            return new Ponto(x / m, y / m, z / m);
        }

        public static Ponto operator +(Ponto a, Ponto b)
        {
            return a.soma(b);
        }

        public static Ponto operator -(Ponto a, Ponto b)
        {
            return a.sub(b);
        }

        public static Ponto operator *(Ponto a, Ponto b)
        {
            return a.cross(b);
        }

        public static Ponto operator *(Ponto a, double b)
        {
            return a.mult(b);
        }

        public static double distancia(Ponto a, Ponto b)
        {
            var d = b - a;
            return Math.Sqrt(d.x*d.x + d.y*d.y + d.z*d.z);
        }

        public static double qdist(Ponto a, Ponto b)
        {
            var d = b - a;
            return d.x * d.x + d.y * d.y + d.z * d.z;
        }

        public override string ToString()
        {
            return string.Format("({0:0.00}, {1:0.00}, {2:0.00})", x, y, z);
        }

        public static explicit operator Ponto(double[] vals)
        {
            return new Ponto(vals[0], vals[1], vals[2]);
        }

        public static explicit operator double[](Ponto p)
        {
            return p.v;
        }

        public static Limites CalculaLimites(params Ponto[] pontos)
        {
            var minx = pontos[0].x;
            var miny = pontos[0].y;
            var minz = pontos[0].z;
            var maxx = minx;
            var maxy = miny;
            var maxz = minz;
            for (int i = 1; i < pontos.Length; i++)
            {
                var p = pontos[i];
                if (p.x < minx) minx = p.x;
                if (p.x > maxx) maxx = p.x;
                if (p.y < miny) miny = p.y;
                if (p.y > maxy) maxy = p.y;
                if (p.z < minz) minz = p.z;
                if (p.z > maxz) maxz = p.z;
            }
            return new Limites()
            {
                Minimos = (double[]) new Ponto(minx, miny, minz),
                Maximos = (double[]) new Ponto(maxx, maxy, maxz)
            };
        }
    }
}
