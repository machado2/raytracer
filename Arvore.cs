using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace testert
{
    class Arvore
    {
        bool calcLimites = false;

        class Nodo
        {

            public enum Dimensao
            {
                x = 0, y = 1, z = 2
            }

            Dimensao dimensao;
            double chave;

            Nodo esquerda;
            Nodo direita;
            Nodo meio;
            IInterceptavel valor;

            Limites _limites;
            internal void calculaLimites()
            {
                var lim = valor.limites;
                if (esquerda != null)
                {
                    esquerda.calculaLimites();
                    lim = lim.Uniao(esquerda._limites);
                }
                if (direita != null)
                {
                    direita.calculaLimites();
                    lim = lim.Uniao(direita._limites);
                }
                if (meio != null)
                {
                    meio.calculaLimites();
                    lim = lim.Uniao(meio._limites);
                }
                _limites = lim;
                //return lim;
            }

            public Nodo(IEnumerable<IInterceptavel> valores, Dimensao dim)
            {

                dimensao = dim;
                if (valores.Count() > 2)
                {
                    valor = valores.OrderBy(x => Meio(dim, x.limites)).Skip(valores.Count() / 2).First();
                    chave = Meio(dimensao, valor.limites);
                    var novaLista = valores.Except(new IInterceptavel[] { valor });
                    var listaEsquerda = novaLista.Where(x => Max(dimensao, x.limites) < chave).ToList();
                    var listaDireita = novaLista.Where(x => Min(dimensao, x.limites) > chave).ToList();
                    var listaMeio = novaLista.Except(listaDireita.Union(listaEsquerda)).ToList();
                    var proxd = proxDim();
                    if (listaEsquerda.Any())
                        esquerda = new Nodo(listaEsquerda, proxd);
                    if (listaDireita.Any())
                        direita = new Nodo(listaDireita, proxd);
                    if (listaMeio.Any())
                        meio = new Nodo(listaMeio, proxd);
                }
                else
                {
                    valor = valores.First();
                    chave = Meio(dimensao, valor.limites);
                    foreach (var v in valores)
                    {
                        if (v != valor)
                            Inserir(v);
                    }
                }
            }


            public Nodo(IInterceptavel valor, Dimensao dimensao)
            {
                this.dimensao = dimensao;
                this.valor = valor;
                chave = Max(dimensao, valor.limites);
            }

            static int ni = 0;

            void Interseccao(Raio raio, List<IInterceptavel> lista, double tmin, double tmax)
            {
                var lim = _limites;
                //var deltamin = lim.Minimos - raio.org;
                //var deltamax = lim.Maximos - raio.org
                for (int d = 0; d < 3; d++)
                {
                    if (raio.dir[d] != 0)
                    {
                        double t1 = (lim.Minimos[d] - raio.org[d]) / raio.dir[d];
                        double t2 = (lim.Maximos[d] - raio.org[d]) / raio.dir[d];
                        if (t2 < t1)
                        {
                            var taux = t1;
                            t1 = t2;
                            t2 = taux;
                        }
                        if (t1 > tmax)
                            return;
                        if (t2 < tmin)
                            return;

                        if (t1 > tmin)
                            tmin = t1;
                        if (t2 < tmax)
                            tmax = t2;
                    }
                    else
                    {
                        if (raio.org[d] < lim.Minimos[d] || raio.org[d] > lim.Maximos[d])
                            return;
                    }
                }
                Interlocked.Increment(ref ni);
                if (valor.limites.Interseccao(raio))
                    lista.Add(valor);
                if (esquerda != null)
                    esquerda.Interseccao(raio, lista, tmin, tmax);
                if (direita != null)
                    direita.Interseccao(raio, lista, tmin, tmax);
                if (meio != null)
                    meio.Interseccao(raio, lista, tmin, tmax);
            }

            public List<IInterceptavel> Interseccao(Raio raio)
            {
                var lista = new List<IInterceptavel>();
                var invdir = raio.dir.Inverso();
                Interseccao(raio, lista, 0, double.MaxValue);
                return lista;
            }

            Dimensao proxDim()
            {
                if (dimensao == Dimensao.z)
                    return Dimensao.x;
                return dimensao + 1;
            }

            double Min(Dimensao dimensao, Limites lim)
            {
                return lim.Minimos[(int)dimensao];
            }

            double Max(Dimensao dimensao, Limites lim)
            {
                return lim.Maximos[(int)dimensao];
            }

            double Meio(Dimensao dimensao, Limites lim)
            {
                return (Min(dimensao, lim) + Max(dimensao, lim)) / 2;
            }

            public void Inserir(IInterceptavel inter)
            {


                var lim = inter.limites;
                if (Max(dimensao, lim) < chave)
                {
                    if (esquerda == null)
                        esquerda = new Nodo(inter, proxDim());
                    else
                        esquerda.Inserir(inter);
                }
                else if (Min(dimensao, lim) > chave)
                {
                    if (direita == null)
                        direita = new Nodo(inter, proxDim());
                    else
                        direita.Inserir(inter);
                }
                else
                {
                    if (meio == null)
                        meio = new Nodo(inter, proxDim());
                    else
                        meio.Inserir(inter);
                }
            }

            public int Altura
            {
                get
                {
                    int maiorh = 0;
                    if (esquerda != null)
                    {
                        var h = esquerda.Altura;
                        if (h > maiorh) maiorh = h;
                    }
                    if (direita != null)
                    {
                        var h = direita.Altura;
                        if (h > maiorh) maiorh = h;
                    }
                    if (meio != null)
                    {
                        var h = meio.Altura;
                        if (h > maiorh) maiorh = h;
                    }
                    return maiorh + 1;
                }
            }

        }


        Nodo raiz = null;

        public void Inserir(IInterceptavel inter)
        {
            if (raiz == null)
                raiz = new Nodo(inter, Nodo.Dimensao.x);
            else
                raiz.Inserir(inter);
        }

        public Arvore(IEnumerable<IInterceptavel> valores)
        {
            raiz = new Nodo(valores, Nodo.Dimensao.x);
        }

        public List<IInterceptavel> Interseccao(Raio raio)
        {
            if (!calcLimites)
            {
                raiz.calculaLimites();
                calcLimites = true;
            }
            if (raiz == null)
                return new List<IInterceptavel>();
            return raiz.Interseccao(raio);
        }

    }

}
