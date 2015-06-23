using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testert
{
    class Traco
    {
        double dist;
        double larg;
        double alt;
        int rx;
        int ry;

        public Traco(double dist, double larg, double alt, int rx, int ry)
        {
            this.dist = dist;
            this.larg = larg;
            this.alt = alt;
            this.rx = rx;
            this.ry = ry;
        }

        void EncontraInterseccao(Raio r, List<IInterceptavel> triangulos, out IInterceptavel tri, out double dist)
        {
            var inter = triangulos
                .Select(t => new
                {
                    triangulo = t,
                    t = t.Interseccao(r)
                })
                .Where(x => x.t.HasValue && x.t.Value > 0.00001)
                .OrderBy(x => x.t.Value)
                .FirstOrDefault();

            if (inter != null)
            {
                tri = inter.triangulo;
                dist = inter.t.Value;
            }
            else
            {
                tri = null;
                dist = 0;
            }
        }

        void EncontraInterseccao(Raio r, Arvore triangulos, out IInterceptavel tri, out double dist)
        {
            var triangs = triangulos.Interseccao(r);
            EncontraInterseccao(r, triangs, out tri, out dist);
        }

        const double rad = Math.PI / 180;

        Cor CalculaRaio(Raio raio, Arvore triangulos, List<Luz> luzes, int nivel)
        {
            IInterceptavel encontrado;
            double distEncontrado;
            EncontraInterseccao(raio, triangulos, out encontrado, out distEncontrado);

            if (encontrado == null)
                return null;

            Ponto posEncontrado = raio.org + raio.dir.mult(distEncontrado);
            var normal = encontrado.normal(posEncontrado);

            var totLuz = luzes.Select(luz =>
            {
                var dir = (luz.posicao - posEncontrado).Normaliza();
                var rluz = new Raio(posEncontrado + dir*0.1, dir);

                IInterceptavel objObstruindo;
                double distObjObstruindo;
                EncontraInterseccao(rluz, triangulos, out objObstruindo, out distObjObstruindo);

                double qdist = Ponto.qdist(rluz.org, luz.posicao);

                if (distObjObstruindo > 0 && distObjObstruindo < Math.Sqrt(qdist))
                    return 0;

                double intens = luz.intensidade / qdist;

                var dirLuz = dir.ParaTras();
                // double dot = Math.Abs(normal.dot(dirLuz));
                double dot = normal.dot(dirLuz);
                if (dot <= 0)
                    return 0;
                intens = intens * dot;

                return intens;
            }).Sum();

            var cor = new Cor(totLuz, totLuz, totLuz);
            cor += 0.3;
            cor = cor * encontrado.material.cor(posEncontrado);

            if (nivel < 3)
            {
                var r2 = raio.Reflexao(posEncontrado, normal);
                var c2 = CalculaRaio(r2, triangulos, luzes, nivel + 1);
                if (c2 != null)
                    return cor + c2 * encontrado.material.reflex(posEncontrado);
            }

            return cor;
        }


        public Color Tracar(int x, int y, Arvore triangulos, List<Luz> luzes, int angx, int angy)
        {
            double dx = x * larg / rx - larg / 2;
            double dy = y * alt / ry - alt / 2;
            
            var dest = new Ponto(dx, dy, 0);
            var org = new Ponto(0, 0, -dist);
            var rot = MatrizTransf.RotacaoX(rad * angx) * MatrizTransf.RotacaoY(rad * angy);
            org = org * rot;
            dest = dest * rot;
            var v = (dest - org).Normaliza();
            var raio = new Raio(org, v);

            var cor = CalculaRaio(raio, triangulos, luzes, 0);
            if (cor == null)
                cor = new Cor(0, 0, 0);
            return cor.ToColor();
        }

    }
}
