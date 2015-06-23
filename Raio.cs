using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    class Raio
    {
        public Ponto org;
        public Ponto dir;

        public Ponto invdir;

        public Raio(Ponto origem, Ponto direcao)
        {
            this.org = origem;
            this.dir = direcao;
            invdir = direcao.Inverso();
        }

        public override string ToString()
        {
            return org.ToString() + "-" + dir.ToString();
        }


        public Raio Reflexao(Ponto p, Ponto normal) {
            var c1 = -normal.dot(dir);
            var ndir = dir + (normal * 2 * c1);
            return new Raio(p, ndir);
        }
    }
}
