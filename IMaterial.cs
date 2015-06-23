using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testert
{
    interface IMaterial
    {
        Cor cor(Ponto p);
        double reflex(Ponto p);
    }
}
