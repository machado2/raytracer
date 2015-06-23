using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    class Luz
    {
        public Ponto posicao;
        public double intensidade;

        public Luz(Ponto pos, double intens)
        {
            posicao = pos;
            intensidade = intens;
        }

    }
}
