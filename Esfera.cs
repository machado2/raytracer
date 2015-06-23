using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    class Esfera : IInterceptavel
    {

        Ponto centro;
        double r;
        IMaterial _material;

        public Esfera(IMaterial material, Ponto c, double raio)
        {
            this.r = raio;
            centro = c;
            _material = material;
        }

        static double quad(double v)
        {
            return v * v;
        }

        public double? Interseccao(Raio raio)
        {
            // http://www.siggraph.org/education/materials/HyperGraph/raytrace/rtinter1.htm
            var xd = raio.dir.x;
            var yd = raio.dir.y;
            var zd = raio.dir.z;

            var x0 = raio.org.x;
            var y0 = raio.org.y;
            var z0 = raio.org.z;

            var xc = centro.x;
            var yc = centro.y;
            var zc = centro.z;

            var a = xd*xd + yd*yd + zd*zd;
            var b = 2* (xd * (x0-xc) + yd * (y0-yc) + zd *(z0-zc));
            var c = quad(x0-xc) + quad(y0-yc) + quad(z0-zc) - quad(r);

            var discr = quad(b) - 4*c;

            if (discr < 0)
                return null;

            var t0 = (-b + Math.Sqrt(discr)) / 2;
            var t1 = (-b - Math.Sqrt(discr)) / 2;
            if (t0 <= t1)
                return t0;
            else
                return t1;
        }

        public Limites limites
        {
            get {
                var min = new Ponto(centro.x - r, centro.y - r, centro.z - r);
                var max = new Ponto(centro.x + r, centro.y + r, centro.z + r);
                return new Limites()
                {
                    Minimos = (double[]) min,
                    Maximos = (double[]) max
                };
            }
        }

        public IMaterial material
        {
            get { return _material; }
        }

        public Ponto normal(Ponto pos)
        {
            return (centro - pos).Normaliza();
        }
    }
}
