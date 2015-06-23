using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    class Solido : IMaterial
    {
        Cor _cor;
        double _reflex;

        public Solido(Cor cor, double reflex)
        {
            _cor = cor;
            _reflex = reflex;
        }

        public Cor cor(Ponto p)
        {
            return _cor;
        }

        public double reflex(Ponto p)
        {
            return _reflex;
        }
    }
}
