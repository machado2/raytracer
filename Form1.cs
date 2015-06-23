using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace testert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Desenhar()
        {
            var arv = new List<IInterceptavel>();

            arv.AddRange(new TesteFract().GerarTriangulos(new Ponto(0, 0, 0), 200, 200, 5));
            //var figs = new Arvore(); figs.Inserir(new Ondas(0, 0, 25, 3));

            var matEsf1 = new Solido(new Cor(Color.DarkRed), 0.8);
            var matEsf2 = new Solido(new Cor(Color.DarkBlue), 0.8);
            var matEsf3 = new Solido(new Cor(Color.Beige), 0.8);
            var matEsf4 = new Solido(new Cor(Color.Gold), 0.8);

            arv.Add(new Esfera(matEsf1, new Ponto(-10, -20, -8), 8));
            arv.Add(new Esfera(matEsf2, new Ponto(20, -20, -8), 8));
            arv.Add(new Esfera(matEsf3, new Ponto(-10, 10, -8), 8));
            arv.Add(new Esfera(matEsf4, new Ponto(20, 10, -8), 8));

            //arv.Add(new Ondas(0, 0, 5, 1));

            var figs = new Arvore(arv);




            var bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //var bmp = new Bitmap(640, 480, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            

            var rt = new Traco(100, 100, 100, bmp.Width, bmp.Height);

            var luzes = new List<Luz>();
            luzes.Add(new Luz(new Ponto(-20, 10, -50), 1000));

            //var teste = rt.Tracar(bmp.Width / 2, bmp.Height / 2, figs, luzes);

            rt.Tracar(bmp.Width / 2, bmp.Height / 2, figs, luzes, -60, 10);

            int bmpw = bmp.Width;
            int bmph = bmp.Height;

            for (int ang = 0; ang > -60; ang--)
            {
                var sw = Stopwatch.StartNew();
                var linhas = Enumerable.Range(0, bmph)
                    .AsParallel().AsOrdered()
                    .Select(y =>
                    {
                        Color[] cores = new Color[bmpw];
                        //for (int y = 0; y < bmp.Height; y++)
                        for (int x = 0; x < bmpw; x++)
                        {
                            cores[x] = rt.Tracar(x, y, figs, luzes, ang, 10);
                            //return cor; //bmp.SetPixel(x, y, cor);
                        }
                        return cores;
                    }).ToArray();

                sw.Stop();
                this.Text = sw.ElapsedMilliseconds.ToString();

                using (var g = pictureBox1.CreateGraphics())
                {
                    int y = 0;
                    foreach (var linha in linhas)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            bmp.SetPixel(x, y, linha[x]);
                        }
                        y++;
                    }
                }

                pictureBox1.Image = bmp;
                Application.DoEvents();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Desenhar();
        }
    }
}
