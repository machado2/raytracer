using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testert
{
    interface IInterceptavel 
    {
        double? Interseccao(Raio raio);
        Limites limites { get;}
        IMaterial material { get; }
        Ponto normal(Ponto pos);
    }
}
