using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testert
{
    class MatrizTransf
    {
        double[,] v;

        public MatrizTransf(double[,] valores)
        {
            if (valores.GetLength(0) != 4) throw new ArgumentException();
            if (valores.GetLength(1) != 4) throw new ArgumentException();
            this.v = valores;
        }

        public static Ponto operator*(Ponto a, MatrizTransf b)
        {
            var v = b.v;
            double x = a.x * v[0, 0] + a.y * v[0, 1] + a.z * v[0, 2] + v[0, 3];
            double y = a.x * v[1, 0] + a.y * v[1, 1] + a.z * v[1, 2] + v[1, 3];
            double z = a.x * v[2, 0] + a.y * v[2, 1] + a.z * v[2, 2] + v[2, 3];
            return new Ponto(x, y, z);
        }

        public static MatrizTransf operator *(MatrizTransf a, MatrizTransf b)
        {
            double[,] r = new double[4, 4];
            var va = a.v;
            var vb = b.v;

            for (int l = 0; l < 4; l++)
            {
                for (int c = 0; c < 4; c++)
                {
                    // l = linha de a
                    // c == col de b
                    r[l, c] = va[l, 0] * vb[0, c]
                            + va[l, 1] * vb[1, c]
                            + va[l, 2] * vb[2, c]
                            + va[l, 3] * vb[3, c];
                }
            }
            return new MatrizTransf(r);
        }

        public static MatrizTransf RotacaoX(double phi)
        {
            var cos = Math.Cos(phi);
            var sin = Math.Sin(phi);
            double[,] v = new double[,] {
                {1, 0, 0, 0},
                {0, cos, sin, 0},
                {0, -sin, cos, 0 },
                {0, 0, 0, 1} };
            return new MatrizTransf(v);
        }

        public static MatrizTransf RotacaoY(double theta)
        {
            var cos = Math.Cos(theta);
            var sin = Math.Sin(theta);
            double[,] v = new double[,] {
                {cos, 0, -sin, 0},
                {0, 1, 0, 0},
                {sin, 0, cos, 0 },
                {0, 0, 0, 1} };
            return new MatrizTransf(v);
        }

        public static MatrizTransf RotacaoZ(double psi)
        {
            var cos = Math.Cos(psi);
            var sin = Math.Sin(psi);
            double[,] v = new double[,] {
                {cos, sin, 0, 0},
                {-sin, cos, 0, 0},
                {0, 0, 1, 0 },
                {0, 0, 0, 1} };
            return new MatrizTransf(v);
        }

        public static MatrizTransf RotacaoArbit(double theta, Ponto eixo)
        {
            var c = Math.Cos(theta);
            var s = Math.Sin(theta);
            var t = 1 - c;
            var x = eixo.x;
            var y = eixo.y;
            var z = eixo.z;

            var sx = s*x;
            var sy = s*y;
            var sz = s*z;

            var tx2 = t * x * x;
            var txy = t * x * y;
            var txz = t * x * z;
            var tyz = t * y * z;
            var ty2 = t * y * y;
            var tz2 = t * z * z;
            
            double[,] v = new double[4, 4] {
                {tx2 + c, txy + sz, txz - sy, 0},
                {txy- sz, ty2 + c, tyz + sx, 0},
                {txz + sy, tyz - sx, tz2 + c, 0},
                {0, 0, 0, 1} };

            return new MatrizTransf(v);
        }

    }

}
