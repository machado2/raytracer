using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    class Limites
    {
        public double[] Minimos;
        public double[] Maximos;
        public Limites Uniao(Limites b) {
            return Ponto.CalculaLimites((Ponto)Minimos, (Ponto)Maximos, (Ponto)b.Minimos, (Ponto)b.Maximos);
        }

        //public bool Contem(Ponto p)
        //{
        //    for (int i = 0
        //        p.x >= Minimos.x
        //        && p.x <= Maximos.x
        //        && p.y >= Minimos.y
        //        && p.y <= Maximos.y
        //        && p.z >= Minimos.z
        //        && p.z <= Maximos.z;
        //}

        public Tuple<double, double> TemposPossiveis(Raio r)
        {
            double? tmin = null;
            double? tmax = null;

            for (int d = 0; d < 3; d++)
            {
                if (r.dir[d] != 0)
                {
                    double t1 = (Minimos[d] - r.org[d]) / r.dir[d];
                    double t2 = (Maximos[d] - r.org[d]) / r.dir[d];
                    if (t2 < t1)
                    {
                        var taux = t1;
                        t1 = t2;
                        t2 = taux;
                    }

                    if (tmin.HasValue)
                    {
                        if (t2 < tmin.Value)
                            return null;
                        if (t1 > tmax.Value)
                            return null;

                        if (t1 > tmin.Value)
                            tmin = t1;
                        if (t2 < tmax.Value)
                            tmax = t2;
                    }
                    else
                    {
                        tmin = t1;
                        tmax = t2;
                    }
                }
                else
                {
                    if (r.org[d] < Minimos[d] || r.org[d] > Maximos[d])
                        return null;
                }
            }
            return new Tuple<double, double>(tmin.Value, tmax.Value);
        }

        public bool Interseccao(Limites lim)
        {
            for (int d = 0; d < 3; d++)
            {
                if (Minimos[d] > lim.Maximos[d]) return false;
                if (Maximos[d] < lim.Minimos[d]) return false;
            }
            return true;
        }

        public bool Interseccao(int d, Raio r)
        {
            double tmin = -1;
            double tmax = -1;
            var invdir = r.invdir;
            var org = r.org;
            unchecked
            {
                if (r.dir[0] != 0)
                {
                    double t1 = (Minimos[0] - org[0]) * invdir[0];
                    double t2 = (Maximos[0] - org[0]) * invdir[0];
                    if (t2 < t1)
                    {
                        var taux = t1;
                        t1 = t2;
                        t2 = taux;
                    }
                    tmin = t1;
                    tmax = t2;
                }
                else
                {
                    if (r.org[0] < Minimos[0] || r.org[0] > Maximos[0])
                        return false;
                }

                if (r.dir[1] != 0)
                {
                    double t1 = (Minimos[1] - org[1]) * invdir[1];
                    double t2 = (Maximos[1] - org[1]) * invdir[1];
                    if (t2 < t1)
                    {
                        var taux = t1;
                        t1 = t2;
                        t2 = taux;
                    }

                    if (t2 < tmin)
                        return false;
                    if (t1 > tmax)
                        return false;

                    if (t1 > tmin)
                        tmin = t1;
                    if (t2 < tmax)
                        tmax = t2;
                }
                else
                {
                    if (r.org[1] < Minimos[1] || r.org[1] > Maximos[1])
                        return false;
                }
                if (r.dir[2] != 0)
                {
                    double t1 = (Minimos[2] - org[2]) * invdir[2];
                    double t2 = (Maximos[2] - org[2]) * invdir[2];
                    if (t2 < t1)
                    {
                        var taux = t1;
                        t1 = t2;
                        t2 = taux;
                    }
                    if (t2 < tmin)
                        return false;
                    if (t1 > tmax)
                        return false;
                }
                else
                {
                    if (r.org[2] < Minimos[2] || r.org[2] > Maximos[2])
                        return false;
                }
            }
            return true;
        }

        public bool Interseccao(Raio r)
        {
            double tmin = -1;
            double tmax = -1;
            var invdir = r.invdir;
            var org = r.org;
            unchecked
            {
                if (r.dir[0] != 0)
                {
                    double t1 = (Minimos[0] - org[0]) * invdir[0];
                    double t2 = (Maximos[0] - org[0]) * invdir[0];
                    if (t2 < t1)
                    {
                        var taux = t1;
                        t1 = t2;
                        t2 = taux;
                    }
                    tmin = t1;
                    tmax = t2;
                }
                else
                {
                    if (r.org[0] < Minimos[0] || r.org[0] > Maximos[0])
                        return false;
                }

                if (r.dir[1] != 0)
                {
                    double t1 = (Minimos[1] - org[1]) * invdir[1];
                    double t2 = (Maximos[1] - org[1]) * invdir[1];
                    if (t2 < t1)
                    {
                        var taux = t1;
                        t1 = t2;
                        t2 = taux;
                    }

                    if (t2 < tmin)
                        return false;
                    if (t1 > tmax)
                        return false;

                    if (t1 > tmin)
                        tmin = t1;
                    if (t2 < tmax)
                        tmax = t2;
                }
                else
                {
                    if (r.org[1] < Minimos[1] || r.org[1] > Maximos[1])
                        return false;
                }
                if (r.dir[2] != 0)
                {
                    double t1 = (Minimos[2] - org[2]) * invdir[2];
                    double t2 = (Maximos[2] - org[2]) * invdir[2];
                    if (t2 < t1)
                    {
                        var taux = t1;
                        t1 = t2;
                        t2 = taux;
                    }
                    if (t2 < tmin)
                        return false;
                    if (t1 > tmax)
                        return false;
                }
                else
                {
                    if (r.org[2] < Minimos[2] || r.org[2] > Maximos[2])
                        return false;
                }
            }
            return true;
        }


        public bool InterseccaoB(Raio r)
        {
            double tmin = -1;
            double tmax = -1;
            bool init = false;
            unchecked
            {
                for (int d = 0; d < 3; d++)
                {
                    if (r.dir[d] != 0)
                    {
                        double t1 = (Minimos[d] - r.org[d]) / r.dir[d];
                        double t2 = (Maximos[d] - r.org[d]) / r.dir[d];
                        if (t2 < t1)
                        {
                            var taux = t1;
                            t1 = t2;
                            t2 = taux;
                        }

                        if (init)
                        {
                            if (t2 < tmin)
                                return false;
                            if (t1 > tmax)
                                return false;

                            if (t1 > tmin)
                                tmin = t1;
                            if (t2 < tmax)
                                tmax = t2;
                        }
                        else
                        {
                            tmin = t1;
                            tmax = t2;
                            init = true;
                        }
                    }
                    else
                    {
                        if (r.org[d] < Minimos[d] || r.org[d] > Maximos[d])
                            return false;
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            return "{" + Minimos.ToString() + ", " + Maximos.ToString() + "}";
        }

    }
}
