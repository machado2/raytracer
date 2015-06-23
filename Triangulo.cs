using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testert
{
    class Triangulo : IInterceptavel
    {
        public Ponto a;
        public Ponto b;
        public Ponto c;
        public IMaterial material { get; protected set; }

        public Ponto CalculaNormal()
        {
            var pv1 = b.sub(a);
            var pv2 = c.sub(b);
            var norm = pv1.cross(pv2);
            return norm.Normaliza();
        }

        Limites _limites;

        public Triangulo(Ponto A, Ponto B, Ponto C, IMaterial m)
        {
            a = A;
            b = B;
            c = C;
            material = m;
            _limites = Ponto.CalculaLimites(A, B, C);
        }

        public override string ToString()
        {
            return a.ToString() + ", " + b.ToString() + ", " + c.ToString();
        }

        public double? Interseccao(Raio raio)
        {
            var org = raio.org;
            var dir = raio.dir;

            var u = this.b - this.a;
            var v = this.c - this.a;
            var n = u * v;

            var w0 = org - this.a;
            var j = n.dot(dir);
            if (j == 0) return null;
            var i = -n.dot(w0);
            double k = i / j;
            var hit = org + dir * k;

            var w = hit - this.a;
            var uu = u.dot(u);
            var uv = u.dot(v);
            var uw = u.dot(w);
            var vv = v.dot(v);
            var vw = v.dot(w);
            var invdenom = 1.0 / (uu * vv - uv * uv);

            var U = (vv * uw - uv * vw) * invdenom;
            var V = (uu * vw - uv * uw) * invdenom;
            var W = U + V;

            var A = U >= 0;
            var B = V >= 0;
            var C = W < 1;
            if (A && B && C)
                return k;
            return null;
        }

        public Limites limites
        {
            get { return _limites; }
        }



        public Ponto normal(Ponto pos)
        {
            var u = this.b - this.a;
            var v = this.c - this.a;
            return u * v;
        }
    }
}
