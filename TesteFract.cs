using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testert
{
    class TesteFract
    {
        Random rnd = new Random();
        double[, ] valores;


        void Gerar(int x1, int y1, int x2, int y2, double ruidoMax)
        {
            if ((x2 - x1) < 2 && (y2 - y1) < 2)
                return;

            int xmeio = (x1 + x2) / 2;
            int ymeio = (y1 + y2) / 2;
            
            double media = (valores[x1, y1] + valores[x1, y2] + valores[x2, y1] + valores[x2, y2]) / 4;
            media += rnd.NextDouble() * ruidoMax;

            double meioRuido = ruidoMax / 2;

            valores[xmeio, ymeio] = media + rnd.NextDouble() * ruidoMax - meioRuido;
            valores[xmeio, y1] = media + rnd.NextDouble() * ruidoMax - meioRuido;
            valores[xmeio, y2] = media + rnd.NextDouble() * ruidoMax - meioRuido;
            valores[x1, ymeio] = media + rnd.NextDouble() * ruidoMax - meioRuido;
            valores[x2, ymeio] = media + rnd.NextDouble() * ruidoMax - meioRuido;

            Gerar(x1, y1,    xmeio, ymeio, meioRuido);
            Gerar(x1, ymeio, xmeio, y2,    meioRuido);
            Gerar(xmeio, y1, x2, ymeio, meioRuido);
            Gerar(xmeio, ymeio, x2, y2, meioRuido);
        }

        public double[,] GerarMapaZB(int larg, int alt, double maxRuido)
        {
            valores = new double[larg, alt];
            valores[0, 0] = rnd.NextDouble() * maxRuido;
            valores[0, alt-1] = rnd.NextDouble() * maxRuido;
            valores[larg-1, 0] = rnd.NextDouble() * maxRuido;
            valores[larg-1, alt-1] = rnd.NextDouble() * maxRuido;
            Gerar(0, 0, larg - 1, alt - 1, maxRuido);
            return valores;
        }

        public double[,] GerarMapaZ(int larg, int alt, double maxRuido)
        {
            valores = new double[larg, alt];
            for (int x = 0; x < larg; x++)
                for (int y = 0; y < alt; y++)
                {
                    double vx = x - larg / 2;
                    double vy = y - larg / 2;
                    double d = Math.Sqrt(vx * vx + vy * vy);
                    valores[x, y] = Math.Sin(d / 2) * 2;

                    
                }

            return valores;

        }

        public double[,] GerarMapaZPlano(int larg, int alt, double maxRuido)
        {
            valores = new double[larg, alt];
            for (int x = 0; x < larg; x++)
                for (int y = 0; y < alt; y++)
                {
                    valores[x, y] = maxRuido;
                }
            return valores;
        }



        public List<Triangulo> GerarTriangulos(Ponto centro, int larg, int comp, int alt)
        {
            Ponto desl = centro - new Ponto(larg / 2, comp / 2, alt / 2);
            var lista = new List<Triangulo>();
            var mapaz = GerarMapaZB(larg, comp, alt);
            for (int y = 0; y < comp-1; y++)
            {
                for (int x = 0; x < larg-1; x++)
                {
                    var pa = new Ponto(x, y, mapaz[x, y]) + desl;
                    var pb = new Ponto(x + 1, y, mapaz[x + 1, y]) + desl;
                    var pc = new Ponto(x, y + 1, mapaz[x, y + 1]) + desl;
                    lista.Add(new Triangulo(pa, pb, pc, new MatSimples()));

                    pa = new Ponto(x + 1, y + 1, mapaz[x + 1, y + 1]) + desl;
                    lista.Add(new Triangulo(pb,pa, pc, new MatSimples()));
                }
            }
            return lista;
        }

    }
}
