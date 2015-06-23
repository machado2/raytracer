using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testert
{
    class MatSimples : IMaterial
    {

        //Color _cor;
        //static Random rnd = new Random();

        public MatSimples()
        {
            //_cor = Color.FromArgb(rnd.Next(100) + 55, rnd.Next(100) + 55, rnd.Next(100) + 55);
        }

        public Cor cor(Ponto p)
        {
            int x = (int)(p.x / 5) % 2;
            int y = (int)(p.y / 5) % 2;
            int z = (int)(p.z / 5) % 2;

            var bx = x == 0;
            var by = y == 0;
            var bz = z == 0;

            bool pinta = bx;
            if (by) pinta = !pinta;
            if (bz) pinta = !pinta;

            Color cor;
            //if (bx && by && bz)
            //    cor = Color.Blue;
            //else
            cor = pinta ? Color.DarkGreen : Color.Green;
            return new Cor(cor);
        }



        public double reflex(Ponto p)
        {
            return 0.2;
        }
    }
}
