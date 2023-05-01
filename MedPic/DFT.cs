using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Collections;
namespace MedPic
{
    public class DFT
    {
        public  Complex[,] fuv;
     

        public DFT(Bitmap bmp)
        {
            int N =(int) bmp.Width; int M = (int)bmp.Height;
            Complex[,]  fuv = new Complex[N, M];
            Complex[,] sfuv = new Complex[N, M];

            for (int n = 0; n < M; n++)
            {
                for (int k = 0; k < N; k++)
                {
                    Complex temp = new Complex(0, 0);
                     fuv[k, n] = temp;
                    sfuv[k, n] = temp;
                }
            }
          fuv= getDFT2D(bmp);
        }


        public Complex[] ComplexFromArray(double[] x)
        {
            int N = x.Length;
            Complex[] X = new Complex[N];
            for (int k = 0; k < N; k++)
            {
                X[k] = new Complex(0, 0);
                X[k].real = x[k]; //X[k].imag = 0;
            }
            return X;
        }


        public double[] ArrayFromComplex(Complex[] x, int tip, int ftipi, double gain)
        {
            int k; int N = x.GetLength(0);
            double[] z; z = new double[N];
            double h, max;
            if (tip == 1)
            {
                for (k = 0; k < N; k++)
                {
                    h = x[k].magnitude;
                    if (ftipi == 1) z[k] = h;
                    else if (ftipi == 2) z[k] = Math.Log(1 + gain * h);
                    else z[k] = 1;
                }

                max = z[0];
                for (k = 0; k < N; k++)
                { if (z[k] > max) max = z[k]; }

                if (max <= 0) max = 1;
                for (k = 0; k < N; k++)
                { z[k] = 1 * z[k] / max; }
            }
            else if (tip == 2)
            {
                for (k = 0; k < N; k++)
                {
                    h = x[k].Phase;
                    if (ftipi == 1) z[k] = h;
                    else if (ftipi == 2) z[k] = Math.Log(1 + Math.Abs(h));
                    else z[k] = 1;
                }

                max = z[0];
                for (k = 0; k < N; k++)
                { if (z[k] > max) max = z[k]; }

                if (max <= 0) max = 1;
                for (k = 0; k < N; k++)
                { z[k] = 1 * z[k] / max; }
            }

            else
            {
                for (k = 0; k < N; k++)
                { z[k] = 1; }
            }
            return z;
        }


        public Complex[] dft(Complex[] input, int dir)  // dir:1 saat ibresi yön, -1 saat ibresi tersi yön
        {
            int N = input.Length; double yon; yon = (double)dir;
            Complex[] X = new Complex[N];

            for (int k = 0; k < N; k++)
            {
                X[k] = new Complex(0, 0);

                for (int n = 0; n < N; n++)
                {
                    Complex temp = Complex.from_polar(1, yon * 2 * Math.PI * n * k / N);
                    temp *= input[n];
                    X[k] += temp;
                }
                X[k].real = X[k].real / (double)N;
                X[k].imag = X[k].imag / (double)N;
            }

            return X;
        }

        public Complex[,] DFT2D(Complex[,] input, int dir)
        {
            int k, n, N, M; N = input.GetLength(0); M = input.GetLength(1);
            Complex[,] output = new Complex[N, M];
            output = input;

            Complex[] rowin = new Complex[N];
            Complex[] rowout = new Complex[N];


            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = output[k, n]; }


                rowout = dft(rowin, dir);

                for (k = 0; k < N; k++)
                {
                    if (dir == -1)
                        output[k, n] = rowout[k];
                    else if (dir == 1)
                    {
                        output[k, n].real = rowout[k].real;
                        output[k, n].imag = rowout[k].imag;
                    }
                }


            }


            Complex[] colin = new Complex[M];
            Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = output[n, k]; }
                colout = dft(colin, dir);
                for (k = 0; k < M; k++)
                {
                    if (dir == -1)
                        output[n, k] = colout[k];
                    else if (dir == 1)
                    {
                        output[n, k].real = colout[k].real;
                        output[n, k].imag = colout[k].imag;
                    }
                }
            }
            return output;
        }

        public Complex[,] getDFT2D(Bitmap bmp)
        {
            int N = bmp.Width; int M = bmp.Height;
            Complex[,] x = new Complex[N, M];
            Complex[,] y = new Complex[N, M];
            x = ComplexFromBitmap(bmp);
            y = DFT2D(x, -1);
            return y;
        }

        public Complex[] ArrayShift(Complex[] input)
        {
            int N, i; N = input.GetLength(0);
            Complex[] output = new Complex[N];

            int en;
            en = N % 2;

            if (en == 0)    // N:çift 
            {
                for (i = 0; i <= (N / 2) - 1; i++)
                {
                    output[i] = input[i + (N / 2)];
                    output[i + (N / 2)] = input[i];
                }
            }

            if (en == 1)   // N:tek 
            {
                for (i = 0; i <= (N - 1) / 2; i++)
                {
                    output[i] = input[i + (N - 1) / 2];
                    output[i + (N - 1) / 2] = input[i];
                }
            }

            return output;
        }

        public Complex[,] DFTShift(Complex[,] input)
        {
            int N, M, i, j; N = input.GetLength(0); M = input.GetLength(1);
            Complex[,] output = new Complex[N, M];
            int en, boy;
            en = N % 2; boy = M % 2;

            if (en == 0 && boy == 0)    // N:çift ve M çift
            {
                for (i = 0; i <= (N / 2) - 1; i++)
                {
                    for (j = 0; j <= (M / 2) - 1; j++)
                    {
                        output[i + (N / 2), j + (M / 2)] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N / 2), j + (M / 2)];    //D-->A
                        output[i + (N / 2), j] = input[i, j + (M / 2)];    //B-->C
                        output[i, j + (M / 2)] = input[i + (N / 2), j];    //C-->B
                    }
                }
            }

            if (en == 1 && boy == 1)   // N:tek ve M tek
            {
                for (i = 0; i <= (N - 1) / 2; i++)
                {
                    for (j = 0; j <= (M - 1) / 2; j++)
                    {
                        output[i + (N - 1) / 2, j + (M - 1) / 2] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N - 1) / 2, j + (M - 1) / 2];    //D-->A
                        output[i + (N - 1) / 2, j] = input[i, j + (M - 1) / 2];    //B-->C
                        output[i, j + (M - 1) / 2] = input[i + (N - 1) / 2, j];    //C-->B
                    }
                }
            }

            if (en == 0 && boy == 1)   // N:çift ve M tek
            {
                for (i = 0; i <= (N / 2) - 1; i++)
                {
                    for (j = 0; j <= (M - 1) / 2; j++)
                    {
                        output[i + (N / 2), j + (M - 1) / 2] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N / 2), j + (M - 1) / 2];    //D-->A
                        output[i + (N / 2), j] = input[i, j + (M - 1) / 2];    //B-->C
                        output[i, j + (M - 1) / 2] = input[i + (N / 2), j];    //C-->B
                    }
                }
            }

            if (en == 1 && boy == 0)   // N:tek ve M çift
            {
                for (i = 0; i <= (N - 1) / 2; i++)
                {
                    for (j = 0; j <= (M / 2) - 1; j++)
                    {
                        output[i + (N - 1) / 2, j + (M / 2)] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N - 1) / 2, j + (M / 2)];    //D-->A
                        output[i + (N - 1) / 2, j] = input[i, j + (M / 2)];    //B-->C
                        output[i, j + (M / 2)] = input[i + (N - 1) / 2, j];    //C-->B
                    }
                }
            }



            return output;
        }

        public Complex[,] RemoveDFTShift(Complex[,] input)
        {
            int N, M, i, j; N = input.GetLength(0); M = input.GetLength(1);
            Complex[,] output = new Complex[N, M];
            int en, boy;
            en = N % 2; boy = M % 2;

            if (en == 0 && boy == 0)    // N:çift ve M çift
            {
                for (i = 0; i <= (N / 2) - 1; i++)
                {
                    for (j = 0; j <= (M / 2) - 1; j++)
                    {
                        output[i + (N / 2), j + (M / 2)] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N / 2), j + (M / 2)];    //D-->A
                        output[i + (N / 2), j] = input[i, j + (M / 2)];    //B-->C
                        output[i, j + (M / 2)] = input[i + (N / 2), j];    //C-->B
                    }
                }
            }

            if (en == 1 && boy == 1)   // N:tek ve M tek
            {
                for (i = 0; i <= (N - 1) / 2; i++)
                {
                    for (j = 0; j <= (M - 1) / 2; j++)
                    {
                        output[i + (N - 1) / 2, j + (M - 1) / 2] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N - 1) / 2, j + (M - 1) / 2];    //D-->A
                        output[i + (N - 1) / 2, j] = input[i, j + (M - 1) / 2];    //B-->C
                        output[i, j + (M - 1) / 2] = input[i + (N - 1) / 2, j];    //C-->B
                    }
                }
            }

            if (en == 0 && boy == 1)   // N:çift ve M tek
            {
                for (i = 0; i <= (N / 2) - 1; i++)
                {
                    for (j = 0; j <= (M - 1) / 2; j++)
                    {
                        output[i + (N / 2), j + (M - 1) / 2] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N / 2), j + (M - 1) / 2];    //D-->A
                        output[i + (N / 2), j] = input[i, j + (M - 1) / 2];    //B-->C
                        output[i, j + (M - 1) / 2] = input[i + (N / 2), j];    //C-->B
                    }
                }
            }

            if (en == 1 && boy == 0)   // N:tek ve M çift
            {
                for (i = 0; i <= (N - 1) / 2; i++)
                {
                    for (j = 0; j <= (M / 2) - 1; j++)
                    {
                        output[i + (N - 1) / 2, j + (M / 2)] = input[i, j];    //A-->D
                        output[i, j] = input[i + (N - 1) / 2, j + (M / 2)];    //D-->A
                        output[i + (N - 1) / 2, j] = input[i, j + (M / 2)];    //B-->C
                        output[i, j + (M / 2)] = input[i + (N - 1) / 2, j];    //C-->B
                    }
                }
            }



            return output;
        }


        public Complex[,] ComplexFromBitmap(Bitmap bmp)
        {
            int N = bmp.Width; int M = bmp.Height; Color p9;
            Complex[,] x = new Complex[N, M];

            for (int n = 0; n < M; n++)
            {
                for (int k = 0; k < N; k++)
                {
                    p9 = bmp.GetPixel(k, n);
                    Complex temp = new Complex(p9.R, 0);
                    x[k, n] = temp;
                }
            }

            return x;
        }


        public Bitmap BitmapFromComplex(Complex[,] x, int tip, int ftipi, double gain)
        {
            int k, n; int N = x.GetLength(0); int M = x.GetLength(1);
            Bitmap bmp = new Bitmap(N, M); Color p9;
            double[,] z; z = new double[N, M];
            double h, mag, phase, max;

            if (tip == 1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        h = x[k, n].magnitude;
                        if (ftipi == 1) z[k, n] = h;
                        else if (ftipi == 2) z[k, n] = Math.Log(1 + gain * h);
                        else z[k, n] = 255;
                    }
                }

                max = z[0, 0];
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    { if (z[k, n] > max) max = z[k, n]; }
                }

                if (max <= 0) max = 1;
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        mag = 255 * z[k, n] / max;
                        p9 = Color.FromArgb((int)mag, (int)mag, (int)mag);
                        bmp.SetPixel(k, n, p9);

                        //  if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        // { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }
            }


            else if (tip == 2)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        h = x[k, n].Phase;
                        if (ftipi == 1) z[k, n] = h;
                        else if (ftipi == 2) z[k, n] = Math.Log(1 + Math.Abs(h));
                        else z[k, n] = 255;

                    }
                }

                max = z[0, 0];
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    { if (z[k, n] > max) max = z[k, n]; }
                }

                if (max <= 0) max = 1;
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        phase = 255 * z[k, n] / max;
                        p9 = Color.FromArgb((int)phase, (int)phase, (int)phase);
                        bmp.SetPixel(k, n, p9);

                        //  if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        //  { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }
            }

            else
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        p9 = Color.FromArgb(255, 0, 0);
                        bmp.SetPixel(k, n, p9);


                        // if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        // { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }

            }

            return bmp;
        }



        public Bitmap BitmapFromComplexN(Complex[,] x)
        {
            int k, n; int N = x.GetLength(0); int M = x.GetLength(1);
            Bitmap bmp = new Bitmap(N, M); Color p9;
            double mag, max;

            max = x[0, 0].magnitude;
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    if (x[k, n].magnitude > max)
                        max = x[k, n].magnitude;
                }
            }

            if (max <= 0) max = 1;
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    mag = 255 * (x[k, n].magnitude) / max;
                    p9 = Color.FromArgb((int)mag, (int)mag, (int)mag);
                    bmp.SetPixel(k, n, p9);
                }
            }
            return bmp;
        }




        public Complex[] Hilbert(Complex[] input, int dir)  // dir:1 saat ibresi yön, -1 saat ibresi tersi yön
        {
            int N = input.Length; double yon; yon = (double)dir;
            Complex[] X = new Complex[N];
            Complex temp; int q, k;

            X = dft(input, -1);

            q = N % 2;
            if (q == 0)
            {
                for (k = 1; k <= (N / 2) - 1; k++)
                {
                    temp = new Complex(0, -1.0);
                    X[k] = temp * X[k];
                }

                X[0].real = 0.0; X[0].imag = 0.0;
                X[N / 2].real = 0.0; X[N / 2].imag = 0.0;

                for (k = (N / 2) + 1; k < N; k++)
                {
                    temp = new Complex(0, 1.0);
                    X[k] = temp * X[k];
                }
            }

            if (q == 1)
            {
                for (k = 1; k <= (N - 1) / 2; k++)
                {
                    temp = new Complex(0, -1.0);
                    X[k] = temp * X[k];
                }

                X[0].real = 0.0; X[0].imag = 0.0;

                for (k = (N - 1) / 2; k < N; k++)
                {
                    temp = new Complex(0, 1.0);
                    X[k] = temp * X[k];
                }
            }

            return X;
        }




        public Complex[,] Hilbert2D(Complex[,] input, int dir)
        {
            int k, n, N, M; N = input.GetLength(0); M = input.GetLength(1);
            Complex[,] output = new Complex[N, M];
            output = input;
            Complex[] rowin = new Complex[N];
            Complex[] rowout = new Complex[N];
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = output[k, n]; }
                rowout = Hilbert(rowin, dir);

                for (k = 0; k < N; k++)
                {
                    if (dir == -1)
                        output[k, n] = rowout[k];
                    else if (dir == 1)
                    {
                        output[k, n].real = rowout[k].real;
                        output[k, n].imag = rowout[k].imag;
                    }
                }
            }
            Complex[] colin = new Complex[M];
            Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = output[n, k]; }
                colout = Hilbert(colin, dir);
                for (k = 0; k < M; k++)
                {
                    if (dir == -1)
                        output[n, k] = colout[k];
                    else if (dir == 1)
                    {
                        output[n, k].real = colout[k].real;
                        output[n, k].imag = colout[k].imag;
                    }
                }
            }
            return output;
        }



        public Complex[,] getHilbert2D(Bitmap bmp)
        {
            int N = bmp.Width; int M = bmp.Height;
            Complex[,] x = new Complex[N, M];
            Complex[,] y = new Complex[N, M];
            x = ComplexFromBitmap(bmp);
            y = Hilbert2D(x, -1);
            return y;
        }








    }
}
