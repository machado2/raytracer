using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    class Ondas : IInterceptavel
    {
        double x;
        double y;
        double raio;
        double altura;
        double fdist;

        public Ondas(double cx, double cy, double _raio, double _altura)
        {
            x = cx;
            y = cy;
            raio = _raio;
            altura = _altura;
            fdist = 30 / raio;
        }

        public double? Interseccao(Raio r)
        {
            return Interseccao(r, 2);
        }

        public double? Interseccao(Raio r, double incremento)
        {
            var faixa = limites.TemposPossiveis(r);
            if (faixa == null)
                return null;

            double t = faixa.Item1;

            //double incremento = 0.01;

            var org = r.org;
            var dir = r.dir;
            var p = r.org + (r.dir * t);
            var lim = limites;
            double fdist = 30 / raio;

            dir = r.dir * incremento;

            

            Ponto pantes = p;
            Ponto projantes = ProjetaPonto(pantes);
            Ponto projatual = ProjetaPonto(p);
            while (t <= faixa.Item2)
            {
                var prox = p + dir;
                var projdepois = ProjetaPonto(prox);
                
                //var p2 = CalculaPonto(p.x, p.y);
                //var p3 = CalculaPonto(prox.x, prox.y);
                var limPonto = Ponto.CalculaLimites(pantes, prox);
                var limProj = Ponto.CalculaLimites(projantes, projdepois);

                if (limPonto.Interseccao(limProj))
                {
                    double dx = p.x - x;
                    double dy = p.y - y;
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    if (dist > raio)
                        return null;
                    else
                    {
                        if (incremento > 0.1)
                        {
                            faixa = limProj.TemposPossiveis(r);
                            if (faixa == null)
                                return null;
                            var nt = faixa.Item1;
                            var resp = Interseccao(new Raio(r.org + (r.dir * nt), r.dir), incremento/10);
                            if (resp == null)
                                return null;
                            return resp.Value + nt;
                        }
                        return t;
                    }
                }

                projantes = projatual;
                projatual = projdepois;

                pantes = p;
                p = prox;

                t = t + incremento;
            };
            return null;
        }

        public Limites limites
        {
            get
            {
                return new Limites()
                {
                    Minimos = (double[])new Ponto(x - raio, y - raio, -altura),
                    Maximos = (double[])new Ponto(x + raio, y + raio, altura)
                };
            }
        }

        IMaterial _mat = new MatSimples();

        public IMaterial material
        {
            get { return _mat; }
        }

        Ponto ProjetaPonto(Ponto porg)
        {
            return CalculaPonto(porg.x, porg.y);
        }

        Ponto CalculaPonto(double px, double py)
        {
            double dx = px - x;
            double dy = py - y;
            double dist = Math.Sqrt(dx * dx + dy * dy);
            var z = Math.Sin(dist * fdist) * altura;
            return new Ponto(px, py, z);
        }

        public Ponto normal(Ponto pos)
        {
            double dif = 0.00001;
            Ponto a = pos;
            Ponto b = CalculaPonto(pos.x + dif, pos.y);
            Ponto c = CalculaPonto(pos.x, pos.y + dif);
            return new Triangulo(a, b, c, null).CalculaNormal();
        }
    }
}
