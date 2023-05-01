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
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace MedPic
{
   public  class FFT
   {   int N, M;
       Bitmap bmp;
       Bitmap bmp1;
       Bitmap bmp2;
       Color p9;
        public class Matris
        {    Complex[,] bilgi;
            public Matris(int N, int M)
            {
                bilgi = new Complex[N, M];

                for (int n = 0; n < M; n++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        Complex temp = new Complex(0, 0);
                        bilgi[k, n] = temp;

                    }
                }
            }

            public Complex this[int x, int y]
            {
                get { return bilgi[x, y]; }
                set { bilgi[x, y] = value; }
            }
        }

      

         public Matris Fuv;      
         public Matris FuvShifted;
         public Matris FuvDonen;
         public Matris FuvdeShifted;
         public Matris iFuv;

        public FFT(Bitmap img)
        {  int n, k;
           N =(int) img.Width; M = (int)img.Height;
           bmp = new Bitmap(img);
           bmp1 = new Bitmap(img);
           bmp2 = new Bitmap(img);

            Fuv = new Matris(N, M);           
            FuvShifted = new Matris(N, M);
            FuvDonen = new Matris(N, M);
            FuvdeShifted = new Matris(N, M);
            iFuv = new Matris(N, M);

           
            for ( n = 0; n < M; n++)
            {
                for ( k = 0; k < N; k++)
                {  
                    Complex temp = new Complex(0, 0);

                     Fuv[k, n].real = temp.real;
                     Fuv[k, n].imag = temp.imag;

                     FuvShifted[k, n].real = temp.real;
                     FuvShifted[k, n].imag = temp.imag;

                     FuvDonen[k, n].real = temp.real;
                     FuvDonen[k, n].imag = temp.imag;

                     FuvdeShifted[k, n].real = temp.real;
                     FuvdeShifted[k, n].imag = temp.imag;

                     iFuv[k, n].real = temp.real;
                     iFuv[k, n].imag = temp.imag;                     
                }
            }           
         
          
          MatrisFFT2D(-1);   
          MatrisFuvShift();                

        }



        public void MatrisFuvShift()
        {   int i, j;       

            for (i = 0; i <= (N / 2) - 1; i++)
            {
                for (j = 0; j <= (M / 2) - 1; j++)
                {
                    
                    Complex temp = new Complex(0, 0);

                    temp.real = Fuv[i, j].real;
                    temp.imag = Fuv[i, j].imag;
                    FuvShifted[i + (N / 2), j + (M / 2)].real =temp.real;
                    FuvShifted[i + (N / 2), j + (M / 2)].imag = temp.imag;

                    temp.real = Fuv[i + (N / 2), j + (M / 2)].real;
                    temp.imag = Fuv[i + (N / 2), j + (M / 2)].imag;
                    FuvShifted[i, j].real = temp.real;
                    FuvShifted[i, j].imag= temp.imag;


                    temp.real = Fuv[i, j + (M / 2)].real;
                    temp.imag = Fuv[i, j + (M / 2)].imag;
                    FuvShifted[i + (N / 2), j].real =temp.real;
                    FuvShifted[i + (N / 2), j].imag = temp.imag;


                    temp.real = Fuv[i + (N / 2), j].real;
                    temp.imag = Fuv[i + (N / 2), j].imag;
                    FuvShifted[i, j + (M / 2)].real =temp.real;
                    FuvShifted[i, j + (M / 2)].imag = temp.imag;
                }
            }           
        }
      
       

        public void RemoveMatrisShift()
        {
            int i, j;        

            for (i = 0; i <= (N / 2) - 1; i++)
            {
                for (j = 0; j <= (M / 2) - 1; j++)
                {
                    Complex temp = new Complex(0, 0);

                    temp.real = FuvDonen[i, j].real;
                    temp.imag = FuvDonen[i, j].imag;
                    FuvdeShifted[i + (N / 2), j + (M / 2)].real = temp.real;
                    FuvdeShifted[i + (N / 2), j + (M / 2)].imag = temp.imag;

                    temp.real = FuvDonen[i + (N / 2), j + (M / 2)].real;
                    temp.imag = FuvDonen[i + (N / 2), j + (M / 2)].imag;
                    FuvdeShifted[i, j].real = temp.real;
                    FuvdeShifted[i, j].imag = temp.imag;

                    temp.real = FuvDonen[i, j + (M / 2)].real;
                    temp.imag = FuvDonen[i, j + (M / 2)].imag;
                    FuvdeShifted[i + (N / 2), j].real = temp.real;
                    FuvdeShifted[i + (N / 2), j].imag = temp.imag;

                    temp.real = FuvDonen[i + (N / 2), j].real;
                    temp.imag = FuvDonen[i + (N / 2), j].imag;
                    FuvdeShifted[i, j + (M / 2)].real = temp.real;
                    FuvdeShifted[i, j + (M / 2)].imag= temp.imag;
                }
            }            
        }



        public void MatrisFFT2Da( int dir)
        {   int k, n;
            Complex[,] input = new Complex[N, M];
            Complex[,] output = new Complex[N, M];



            for ( n = 0; n < M; n++)
            {
                for ( k = 0; k < N; k++)
                {
                    Complex temp = new Complex(0, 0);
                    temp.real = FuvdeShifted[k, n].real;
                    temp.imag = FuvdeShifted[k, n].imag;                
                    input[k, n] = temp;
                }
            }       
            
          
            output = input;

            Complex[] rowin = new Complex[N]; Complex[] rowout = new Complex[N];
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = output[k, n]; }
                rowout = fft(rowin, dir);
                for (k = 0; k < N; k++)
                {
                    if (dir == -1)
                        output[k, n] = rowout[k];
                    else if (dir == 1)
                    {
                        output[k, n].real = rowout[k].real / N;
                        output[k, n].imag = rowout[k].imag / N;
                    }
                }
            }

            Complex[] colin = new Complex[M]; Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = output[n, k]; }
                colout = fft(colin, dir);

                for (k = 0; k < M; k++)
                {
                    if (dir == -1)
                        output[n, k] = colout[k];
                    else if (dir == 1)
                    {
                        output[n, k].real = colout[k].real / M;
                        output[n, k].imag = colout[k].imag / M;
                    }
                }
            }


            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    Complex temp = new Complex(0, 0);
                    temp =output[k, n];
                    iFuv[k, n].real = temp.real;
                    iFuv[k, n].imag = temp.imag;
                }
            }       
            
            
           
        }




        public void MatrisFFT2D(int dir)
        {   int k, n;
            Complex[,] input = new Complex[N, M];
            Complex[,] output = new Complex[N, M];




            if (dir ==-1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        p9 = bmp.GetPixel(k, n);
                        Complex temp = new Complex(p9.R, 0);
                        input[k, n] = temp;
                    }
                }
            }


            if (dir == 1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        Complex temp = new Complex(0, 0);
                        temp.real = FuvdeShifted[k, n].real;
                        temp.imag = FuvdeShifted[k, n].imag;
                        input[k, n] = temp;
                    }
                }
            }



            output = input;

            Complex[] rowin = new Complex[N]; Complex[] rowout = new Complex[N];
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = output[k, n]; }
                rowout = fft(rowin, dir);
                for (k = 0; k < N; k++)
                {
                    if (dir == -1)
                        output[k, n] = rowout[k];
                    else if (dir == 1)
                    {
                        output[k, n].real = rowout[k].real / N;
                        output[k, n].imag = rowout[k].imag / N;
                    }
                }
            }

            Complex[] colin = new Complex[M]; Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = output[n, k]; }
                colout = fft(colin, dir);

                for (k = 0; k < M; k++)
                {
                    if (dir == -1)
                        output[n, k] = colout[k];
                    else if (dir == 1)
                    {
                        output[n, k].real = colout[k].real / M;
                        output[n, k].imag = colout[k].imag / M;
                    }
                }
            }

            if (dir == -1)
            {
                double max;

                max = output[0, 0].magnitude;
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        if (output[k, n].magnitude > max)
                            max = output[k, n].magnitude;
                    }
                }

                if (max <= 0) max = 1;
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        output[k, n].real = output[k, n].real / max;
                        output[k, n].imag = output[k, n].imag / max;
                    }
                }

            }


            if (dir ==-1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        Complex temp = new Complex(0, 0);
                        temp = output[k, n];
                        Fuv[k, n].real = temp.real;
                        Fuv[k, n].imag = temp.imag;
                    }
                }
            }



             if (dir == 1)
             {
                  for (n = 0; n < M; n++)
                  {
                    for (k = 0; k < N; k++)
                    {   Complex temp = new Complex(0, 0);
                        temp = output[k, n];
                        iFuv[k, n].real = temp.real;
                        iFuv[k, n].imag = temp.imag;
                    }
                }
             }


     }




        public Bitmap BitmapFromFuv(int tip, int ftipi, double gain)
        {
            int k, n; Bitmap bmp3 = new Bitmap(N, M);
            double[,] z; z = new double[N, M];
            double h, mag, phase, max;

            Complex z1 = new Complex(0, 0);

            if (tip == 1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {

                        z1.real = Fuv[k, n].real;
                        z1.imag= Fuv[k, n].imag;

                        h = z1.magnitude;
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
                        bmp3.SetPixel(k, n, p9);

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
                        z1.real = Fuv[k, n].real;
                        z1.imag = Fuv[k, n].imag;
                        //  h =z1.Phase;
                        h = z1.Aci;
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
                        bmp3.SetPixel(k, n, p9);

                        if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        { p9 = Color.FromArgb(0, 255, 0); bmp3.SetPixel(k, n, p9); }
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
                        bmp3.SetPixel(k, n, p9);

                        // if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        // { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }

            }

            return bmp3;
        }

        public Bitmap BitmapFromFuvShifted(int tip, int ftipi, double gain)
        {
            int k, n; Bitmap bmp3 = new Bitmap(N, M);
            double[,] z; z = new double[N, M];
            double h, mag, phase, max;

            Complex z1 = new Complex(0, 0);

            if (tip == 1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        
                        z1.real = FuvShifted[k, n].real;
                        z1.imag = FuvShifted[k, n].imag;

                        h = z1.magnitude;
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
                        bmp3.SetPixel(k, n, p9);

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
                        z1.real = FuvShifted[k, n].real;
                        z1.imag = FuvShifted[k, n].imag;

                        //  h =z1.Phase;
                        h = z1.Aci;
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
                        bmp3.SetPixel(k, n, p9);

                        if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        { p9 = Color.FromArgb(0, 255, 0); bmp3.SetPixel(k, n, p9); }
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
                        bmp3.SetPixel(k, n, p9);

                        // if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        // { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }

            }

            return bmp3;
        }


        public Bitmap BitmapFromFuvDonen(int tip, int ftipi, double gain)
        {
            int k, n; Bitmap bmp3 = new Bitmap(N, M);
            double[,] z; z = new double[N, M];
            double h, mag, phase, max;

            Complex z1 = new Complex(0, 0);

            if (tip == 1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                        z1.real = FuvDonen[k, n].real;
                        z1.imag = FuvDonen[k, n].imag;
                        h = z1.magnitude;
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
                        bmp3.SetPixel(k, n, p9);

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
                        z1.real = FuvDonen[k, n].real;
                        z1.imag = FuvDonen[k, n].imag;
                        //  h =z1.Phase;
                        h = z1.Aci;
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
                        bmp3.SetPixel(k, n, p9);

                        if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        { p9 = Color.FromArgb(0, 255, 0); bmp3.SetPixel(k, n, p9); }
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
                        bmp3.SetPixel(k, n, p9);

                        // if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        // { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }

            }

            return bmp3;
        }

        public Bitmap BitmapFromFuvdeShifted(int tip, int ftipi, double gain)
        {
            int k, n; Bitmap bmp3 = new Bitmap(N, M);
            double[,] z; z = new double[N, M];
            double h, mag, phase, max;

            Complex z1 = new Complex(0, 0);

            if (tip == 1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                       
                        z1.real = FuvdeShifted[k, n].real;
                        z1.imag = FuvdeShifted[k, n].imag;

                        h = z1.magnitude;
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
                        bmp3.SetPixel(k, n, p9);

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
                        z1.real = FuvdeShifted[k, n].real;
                        z1.imag = FuvdeShifted[k, n].imag;
                        //  h =z1.Phase;
                        h = z1.Aci;
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
                        bmp3.SetPixel(k, n, p9);

                        if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        { p9 = Color.FromArgb(0, 255, 0); bmp3.SetPixel(k, n, p9); }
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
                        bmp3.SetPixel(k, n, p9);

                        // if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        // { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }

            }

            return bmp3;
        }


        public Bitmap BitmapFromiFuv(int tip, int ftipi, double gain)
        {
            int k, n; Bitmap bmp3 = new Bitmap(N, M);
            double[,] z; z = new double[N, M];
            double h, mag, phase, max;

            Complex z1 = new Complex(0, 0);

            if (tip == 1)
            {
                for (n = 0; n < M; n++)
                {
                    for (k = 0; k < N; k++)
                    {
                       
                        z1.real = iFuv[k, n].real;
                        z1.imag = iFuv[k, n].imag;

                        h = z1.magnitude;
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
                        bmp3.SetPixel(k, n, p9);

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
                        z1.real = iFuv[k, n].real;
                        z1.imag = iFuv[k, n].imag;
                        //  h =z1.Phase;
                        h = z1.Aci;
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
                        bmp3.SetPixel(k, n, p9);

                        if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        { p9 = Color.FromArgb(0, 255, 0); bmp3.SetPixel(k, n, p9); }
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
                        bmp3.SetPixel(k, n, p9);

                        // if (k == bmp.Width / 2 || n == bmp.Height / 2)
                        // { p9 = Color.FromArgb(0, 0, 255); bmp.SetPixel(k, n, p9); }
                    }
                }

            }

            return bmp3;
        }

      
        public void ComplexArraytoFuv(Complex[,] input)
        {
              for (int n = 0; n < M; n++)
              {
                for (int k = 0; k < N; k++)
                {   Complex temp = new Complex(0, 0);
                    temp = input[k, n];
                    Fuv[k, n] = temp;                   
                }
            }       
        }


        public void ComplexArraytoFuvShifted(Complex[,] input)
        {
            for (int n = 0; n < M; n++)
            {
                for (int k = 0; k < N; k++)
                {
                    Complex temp = new Complex(0, 0);
                    temp = input[k, n];
                    FuvShifted[k, n] = temp;
                }
            }
        }

        public Complex[] fft(Complex[] input, int dir)  //  dir:1 saat ibresi yön, -1 saat ibresi tersi yön
        {
            int N = input.Length; double yon; yon = (double)dir;
            Complex[] X = new Complex[N];
            Complex[] d, D, e, E;
            if (N == 1)
            { X[0] = input[0]; return X; }

            int k;
            e = new Complex[N / 2];
            d = new Complex[N / 2];

            for (k = 0; k < N / 2; k++)
            {
                e[k] = input[2 * k];
                d[k] = input[2 * k + 1];
            }

            D = fft(d, dir);
            E = fft(e, dir);

            for (k = 0; k < N / 2; k++)
            {
                Complex temp = Complex.from_polar(1, yon * 2 * Math.PI * k / N);
                D[k] *= temp;
            }

            for (k = 0; k < N / 2; k++)
            {
                X[k] = E[k] + D[k];
                X[k + N / 2] = E[k] - D[k];
            }

            return X;
        }





        public Complex[,] FFT2Da(Complex[,] input, int dir)
        {
            int k, n, N, M; N = input.GetLength(0); M = input.GetLength(1);
            Complex[,] output = new Complex[N, M];
            output = input;

            Complex[] rowin = new Complex[N]; Complex[] rowout = new Complex[N];
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = output[k, n]; }
                rowout = fft(rowin, dir);
                for (k = 0; k < N; k++)
                {
                    if (dir == -1)
                        output[k, n] = rowout[k];
                    else if (dir == 1)
                    {
                        output[k, n].real = rowout[k].real / N;
                        output[k, n].imag = rowout[k].imag / N;
                    }
                }
            }

            Complex[] colin = new Complex[M]; Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = output[n, k]; }
                colout = fft(colin, dir);

                for (k = 0; k < M; k++)
                {
                    if (dir == -1)
                        output[n, k] = colout[k];
                    else if (dir == 1)
                    {
                        output[n, k].real = colout[k].real / M;
                        output[n, k].imag = colout[k].imag / M;
                    }
                }
            }
            return output;
        }

        public Complex[,] FFTShift(Complex[,] input)
        {   int  i, j; 
            Complex[,] output = new Complex[N, M];

            for (i = 0; i <= (N / 2) - 1; i++)
            {
                for (j = 0; j <= (M / 2) - 1; j++)
                {
                    output[i + (N / 2), j + (M / 2)] = input[i, j];
                    output[i, j] = input[i + (N / 2), j + (M / 2)];
                    output[i + (N / 2), j] = input[i, j + (M / 2)];
                    output[i, j + (N / 2)] = input[i + (N / 2), j];
                }
            }

            return output;
        }

        public Complex[,] RemoveFFTShift(Complex[,] input)
        {   int  i, j; 
            Complex[,] output = new Complex[N, M];

            for (i = 0; i <= (N / 2) - 1; i++)
            {
                for (j = 0; j <= (M / 2) - 1; j++)
                {
                    output[i + (N / 2), j + (M / 2)] = input[i, j];
                    output[i, j] = input[i + (N / 2), j + (M / 2)];
                    output[i + (N / 2), j] = input[i, j + (M / 2)];
                    output[i, j + (N / 2)] = input[i + (N / 2), j];
                }
            }

            return output;
        }


        public Complex[,] FFTNormalize(Complex[,] input)
        {
            int k, n; int N = input.GetLength(0); int M = input.GetLength(1);
            Complex[,] z = new Complex[N, M];
            double max;

            max = input[0, 0].magnitude;
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    if (input[k, n].magnitude > max)
                        max = input[k, n].magnitude;
                }
            }

            if (max <= 0) max = 1;
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    z[k, n].real = input[k, n].real / max;
                    z[k, n].imag = input[k, n].imag / max;
                }
            }

            return z;

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

        public Complex[] ArrayShift(Complex[] input)
        {
            int N, i; N = input.GetLength(0);
            Complex[] output = new Complex[N];
            for (i = 0; i <= (N / 2) - 1; i++)
            { output[i] = input[i + (N / 2)]; output[i + (N / 2)] = input[i]; }
            return output;
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


        public Complex[,] ComplexFromBitmap(Bitmap bmp)
        {  
            Complex[,] x = new Complex[N, M];
            for (int n = 0; n < M; n++)
            {
                for (int k = 0; k < N; k++)
                {
                    p9 = bmp.GetPixel(k, n);
                    Complex temp = new Complex(p9.R,0);
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
                      //  h = x[k, n].Phase;
                        h = x[k, n].Aci;
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

                         if (k == bmp.Width / 2 || n == bmp.Height / 2)
                         { p9 = Color.FromArgb(0, 255, 0); bmp.SetPixel(k, n, p9); }
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


        public Complex[,] getFFTa(Bitmap bmp)
        {
            int k, n; int N = bmp.Width; int M = bmp.Height;
            Complex[,] x = new Complex[N, M];

            x = ComplexFromBitmap(bmp);
            Complex[] rowin = new Complex[N]; Complex[] rowout = new Complex[N];
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = x[k, n]; }
                rowout = fft(rowin, -1);
                for (k = 0; k < N; k++)
                {
                    x[k, n].real = rowout[k].real / N;
                    x[k, n].imag = rowout[k].imag / N;
                }
            }

            Complex[] colin = new Complex[M]; Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = x[n, k]; }
                colout = fft(colin, -1);

                for (k = 0; k < M; k++)
                {
                    x[n, k].real = colout[k].real / M;
                    x[n, k].imag = colout[k].imag / M;
                }
            }

            return x;
        }


        public Complex[,] getFFTaNx(Bitmap bmp)
        {
            int k, n; int N = bmp.Width; int M = bmp.Height;
            Complex[,] x = new Complex[N, M];

            x = ComplexFromBitmap(bmp);
            Complex[] rowin = new Complex[N]; Complex[] rowout = new Complex[N];
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = x[k, n]; }
                rowout = fft(rowin, -1);
                for (k = 0; k < N; k++)
                {
                    x[k, n].real = rowout[k].real / N;
                    x[k, n].imag = rowout[k].imag / N;
                }
            }

            Complex[] colin = new Complex[M]; Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = x[n, k]; }
                colout = fft(colin, -1);

                for (k = 0; k < M; k++)
                {
                    x[n, k].real = colout[k].real / M;
                    x[n, k].imag = colout[k].imag / M;
                }
            }

            double max;

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
                    x[k, n].real = x[k, n].real / max;
                    x[k, n].imag = x[k, n].imag / max;
                }
            }

            return x;
        }





        public Complex[,] getFFTaN(Bitmap bmp)
        {
            int k, n; int N = bmp.Width; int M = bmp.Height;
            Complex[,] x = new Complex[N, M];


            x = ComplexFromBitmap(bmp);
            Complex[] rowin = new Complex[N]; Complex[] rowout = new Complex[N];
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                { rowin[k] = x[k, n]; }
                rowout = fft(rowin, -1);
                for (k = 0; k < N; k++)
                {
                    x[k, n].real = rowout[k].real / N;
                    x[k, n].imag = rowout[k].imag / N;
                }
            }

            Complex[] colin = new Complex[M]; Complex[] colout = new Complex[M];
            for (n = 0; n < N; n++)
            {
                for (k = 0; k < M; k++)
                { colin[k] = x[n, k]; }
                colout = fft(colin, -1);

                for (k = 0; k < M; k++)
                {
                    x[n, k].real = colout[k].real / M;
                    x[n, k].imag = colout[k].imag / M;
                }
            }

            double max;

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
                    x[k, n].real = x[k, n].real / max;
                    x[k, n].imag = x[k, n].imag / max;
                }
            }

           
            return x;
        }




        public Complex[,] FFTSakla(Bitmap bmp1, Bitmap bmp2)
        {
            int k, n, N, M; N = bmp1.Width; M = bmp1.Height;
            Color p9;
            Complex[,] input = new Complex[N, M];

            input = getFFTaN(bmp1);


            for (n = 0; n < bmp2.Height; n++)
            {
                for (k = 0; k < bmp2.Width; k++)
                {
                    p9 = bmp2.GetPixel(k, n);
                    input[k + N / 2, n + M / 2].real = (double)(0.7 * p9.R / (255 * 100));
                    input[k + N / 2, n + M / 2].imag = (double)(0.7 * p9.R / (255 * 100));
                }

            }

            return input;
        }





        public Complex[,] FFTSakla2(Bitmap bmp1, Bitmap bmp2)
        {
            int k, n, N, M; N = bmp1.Width; M = bmp1.Height;
            Color p9;
            Complex[,] input = new Complex[N, M];
            Complex[,] output = new Complex[N, M];

            input = getFFTaN(bmp1);

            output = FFTShift(input);


            for (n = 0; n < bmp2.Height; n++)
            {
                for (k = 0; k < bmp2.Width; k++)
                {
                    p9 = bmp2.GetPixel(k, n);
                    output[k, n].real = (double)(0.7 * p9.R / (255 * 100));
                    output[k, n].imag = (double)(0.7 * p9.R / (255 * 100));
                }

            }

            return output;
        }


        public Complex[,] FFTSakla3(Bitmap bmp1, Bitmap bmp2)
        {
            int k, n, N, M; N = bmp1.Width; M = bmp1.Height;
            Color p9;
            Complex[,] input = new Complex[N, M];
            Complex[,] output = new Complex[N, M];

            input = getFFTaN(bmp1);
            output = FFTShift(input);

            for (n = 0; n < bmp2.Height; n++)
            {
                for (k = 0; k < bmp2.Width; k++)
                {
                    p9 = bmp2.GetPixel(k, n);
                    output[k, n].real = (double)(0.7 * p9.R / (255 * 100));
                    output[k, n].imag = (double)(0.7 * p9.R / (255 * 100));
                }

            }

            input = RemoveFFTShift(output);

            return input;
        }


        public Complex[,] FFTSakla4(Bitmap bmp1, Bitmap bmp2)
        {
            int k, n, N, M; N = bmp1.Width; M = bmp1.Height;
            Color p9; double r,alfa;
            Complex[,] input = new Complex[N, M];
            Complex[,] output = new Complex[N, M];

            input = getFFTaN(bmp1);
            output = FFTShift(input);

            for (n = 0; n < bmp1.Height; n++)
            {
                for (k = 0; k < bmp1.Width; k++)
                {
                    p9 = bmp2.GetPixel(k, n);
                    r = input[k, n].magnitude; alfa =input[k, n].Aci;
                    alfa=alfa+p9.R;
                    alfa=alfa* Math.PI/180;

                    output[k, n].real = r * Math.Cos(alfa);
                    output[k, n].imag =r * Math.Sin(alfa);
                }

            }

            input = RemoveFFTShift(output);

            return input;
        }


        public Complex[] Hilbert(Complex[] input, int dir)  // dir:1 saat ibresi yön, -1 saat ibresi tersi yön
        {
            int N = input.Length; double yon; yon = (double)dir;
            Complex[] X = new Complex[N];
            Complex temp; int q, k;

            X = fft(input, -1);

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




        public Bitmap Hilbert(Bitmap img)
        {
            Bitmap bmp1 = new Bitmap(img);
            Bitmap bmp2 = new Bitmap(img);
            int k, n, N, M; N = bmp1.Width; M = bmp1.Height;
          

            Complex[,] input = new Complex[N, M];
            Complex[,] output = new Complex[N, M];
            Complex[,] son = new Complex[N, M];         
 
             //output = getHilbert2D(img);

            input= ComplexFromBitmap(img);
            output = Hilbert2D(input, -1);
            son = FFT2Da(output, 1);
            bmp2 = BitmapFromComplex(son, 1, 2, 200);
            return bmp2;
        }




        public void MatriskareLP(int a, int b)
        {
            int k, n;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    if ((k < (a + N / 2)) && k > ((N / 2) - a) && n < (b + M / 2) && n > ((M / 2) - b))
                    {
                        FuvDonen[k, n].real = FuvShifted[k, n].real;
                        FuvDonen[k, n].imag = FuvShifted[k, n].imag;
                    }
                    else
                    {
                        FuvDonen[k, n].real = 0.0;
                        FuvDonen[k, n].imag = 0.0;
                    }

                }
            }

        }
        public Bitmap mFilterkareLP(int tercih, double rc1, double rc2)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r1, r2;

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
            { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
            { r2 = rc2; }
            else if (rc2 >= N / 2)
            { r2 = (double)(N / 2); }
            else
                r2 = 0.0;

            MatriskareLP((int)r1, (int)r2);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }



        public void MatriskareHP(int a, int b)
        {
            int k, n;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    if ((k < (a + N / 2)) && k > ((N / 2) - a) && n < (b + M / 2) && n > ((M / 2) - b))
                    {
                        FuvDonen[k, n].real = 0.0;
                        FuvDonen[k, n].imag = 0.0;
                    }
                    else
                    {

                        FuvDonen[k, n].real = FuvShifted[k, n].real;
                        FuvDonen[k, n].imag = FuvShifted[k, n].imag;
                    }

                }
            }

        }

        public Bitmap mFilterkareHP(int tercih, double rc1, double rc2)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r1, r2;

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
            { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
            { r2 = rc2; }
            else if (rc2 >= N / 2)
            { r2 = (double)(N / 2); }
            else
                r2 = 0.0;

            MatriskareHP((int)r1, (int)r2);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }

       

        public void MatrisdaireLP( double r)
        {    int k, n;
             double d;          

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));

                    if (d <= r)
                    {
                        FuvDonen[k, n].real = FuvShifted[k, n].real;
                        FuvDonen[k, n].imag = FuvShifted[k, n].imag;
                    }
                    else
                    {
                        FuvDonen[k, n].real = 0.0;
                        FuvDonen[k, n].imag = 0.0;
                    }

                }
            }           
        }


        public Bitmap mFilterdaireLP(int tercih, double rc)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double  r;           

            if (rc <= 1.0)
            { r = (double)N * rc / 2; }
            else if (rc > 1.0 && rc <= N / 2)
            { r = rc; }
            else if (rc >= N / 2)
            { r = (double)N / 2; }
            else
                r = 0.0;           

            MatrisdaireLP((int)r);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;

        }




       

        public void MatrisdaireHP(double r)
        {
            int k, n;
            double d;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));

                    if (d >= r)
                    {
                        FuvDonen[k, n].real = FuvShifted[k, n].real;
                        FuvDonen[k, n].imag = FuvShifted[k, n].imag;
                    }
                    else
                    {
                        FuvDonen[k, n].real = 0.0;
                        FuvDonen[k, n].imag = 0.0;
                    }
                }
            }
        }

      


        public Bitmap mFilterdaireHP(int tercih, double rc)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r;

            if (rc <= 1.0)
            { r = (double)N * rc / 2; }
            else if (rc > 1.0 && rc <= N / 2)
            { r = rc; }
            else if (rc >= N / 2)
            { r = (double)N / 2; }
            else
                r = 0.0;

            MatrisdaireHP((int)r);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }


       


        public void  MatrisdaireBP( double r1, double r2)
        {   int k, n;           
            double d;          

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));

                    if (d >= r1 && d <= r2)
                    {
                        FuvDonen[k, n].real = FuvShifted[k, n].real;
                        FuvDonen[k, n].imag = FuvShifted[k, n].imag;
                    }
                    else
                    {
                        FuvDonen[k, n].real = 0.0;
                        FuvDonen[k, n].imag = 0.0;
                    }

                }
            }

           
        }

     

        public Bitmap mFilterdaireBP(int tercih, double rc1, double rc2)
        {
            Bitmap bmp3 = new Bitmap(bmp);               
            double  r1, r2;
         
            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
            { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
            { r2 = rc2; }
            else if (rc2 >= N / 2)
            { r2 = (double)(N / 2); }
            else
                r2 = 0.0;          

            MatrisdaireBP(r1,r2);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }


      

        public void MatrisdaireBS(double r1, double r2)
        {   int k, n;    double d;
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));

                    if (d >= r1 && d <= r2)
                    {
                        FuvDonen[k, n].real = 0.0;
                        FuvDonen[k, n].imag = 0.0;
                    }
                    else
                    {
                        FuvDonen[k, n].real = FuvShifted[k, n].real;
                        FuvDonen[k, n].imag = FuvShifted[k, n].imag;
                    }

                }
            }


        }




        public Bitmap mFilterdaireBS(int tercih, double rc1, double rc2)
        {   Bitmap bmp3 = new Bitmap(bmp);
            double r1, r2;

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
            { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
            { r2 = rc2; }
            else if (rc2 >= N / 2)
            { r2 = (double)(N / 2); }
            else
                r2 = 0.0;

            MatrisdaireBS(r1, r2);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }


       /*
        public Complex[,] FFTgaussLP(Complex[,] input, double r)
        {
            int k, n, N, M; N = input.GetLength(0); M = input.GetLength(1);
            Complex[,] output = new Complex[N, M];
            Complex[,] son = new Complex[N, M];
            output = input;
            son = input;
            double d, a;
            output = FFTShift(input);

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {

                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));
                    a = Math.Exp(-(d * d) / (2 * r * r));
                    output[k, n].real = a * output[k, n].real;
                    output[k, n].imag = a * output[k, n].imag;

                }
            }

            son = RemoveFFTShift(output);
            return son;
        }
       */
        /*
         public Bitmap FiltergaussLP(Bitmap img, double rc)
         {
             Bitmap bmp1 = new Bitmap(img);
             Bitmap bmp2 = new Bitmap(img);
             int k, n, N, M; N = bmp1.Width; M = bmp1.Height;
             double d, a, r;

             Complex[,] input = new Complex[N, M];
             Complex[,] output = new Complex[N, M];
             Complex[,] son = new Complex[N, M];
             Complex[,] ifxy = new Complex[N, M];

             if (rc <= 1.0)
             { r = (double)N * rc / 2; }
             else if (rc > 1.0 && rc <= N / 2)
             { r = rc; }
             else if (rc >= N / 2)
             { r = (double)N / 2; }
             else
                 r = 0.0;

             input = getFFTaN(bmp1);
             output = input;
             son = input;
             ifxy = input;

             output = FFTgaussLP(input, r);
             son = RemoveFFTShift(output);
             ifxy = FFT2Da(son, 1);
             bmp2 = BitmapFromComplex(ifxy, 1, 2, 1000);
             return bmp2;
         }
        */


        public Bitmap Gauss(Bitmap bmp1, double a)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double b, d, x, r, y;
            int i, j, N, M, q; Color p9;
            N = bmp1.Width; M = bmp1.Height;

            if (a <= 1.0)
            { r = (double)N * a / 2; }
            else if (a > 1.0 && a <= N / 2)
            { r = a; }
            else if (a >= N / 2)
            { r = (double)N / 2; }
            else
                r = 0.0;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    x = (double)(i - N / 2); y = (double)(j - M / 2);

                    d = Math.Sqrt(x * x + y * y);

                    b = 255 * Math.Exp(-(d * d) / (1 + 2 * r * r));

                    q = (int)b;
                    p9 = Color.FromArgb(q, q, q);

                    bmp2.SetPixel(i, j, p9);




                    if (i == bmp1.Width / 2 || j == bmp1.Height / 2)
                    { p9 = Color.FromArgb(0, 0, 255); bmp2.SetPixel(i, j, p9); }

                }
            }

            return bmp2;
        }



        public void  MatrisGaussLP( double r)
        {   int k, n;
            double d, a;
            
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));
                    a = Math.Exp(-(d * d) / (2 * r * r));

                    FuvDonen[k, n].real =a* FuvShifted[k, n].real;
                    FuvDonen[k, n].imag =a* FuvShifted[k, n].imag;
                }
            }
           
        }

       
        public Bitmap mFilterGaussLP(int tercih, double rc)
        {
            Bitmap bmp3 = new Bitmap(bmp);           
            double  r;           

            if (rc <= 1.0)
            { r = (double)N * rc / 2; }
            else if (rc > 1.0 && rc <= N / 2)
            { r = rc; }
            else if (rc >= N / 2)
            { r = (double)N / 2; }
            else
                r = 0.0;      


            MatrisGaussLP(r);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }


        public void MatrisGaussHP(double r)
        {
            int k, n;
            double d, a;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));
                    a =1.0- Math.Exp(-(d * d) / (2 * r * r));

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
                }
            }

        } 


        public void MatrisGaussBP(double r1, double r2)
        {    int k, n; 
            double d, a, r0, W, D;
            r0 = Math.Sqrt(r1 * r2);
            W = r2 - r1;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                  
                    d = Math.Sqrt((double)((n - M / 2) * (n - M / 2)) + (double)((k - N / 2) * (k - N / 2)));
                    if (d == 0) d = 1;

                    D = (d * d - r0 * r0) / (d * W);
                    a = Math.Exp(-(D * D) / 2);

                    /*
                    D = (d  - r0 )*(d  - r0 ) / (W * W);
                    a = Math.Exp(-D / 2);
                    if (a <= 0.6) a = 0;
                     */

                    FuvDonen[k, n].real =a* FuvShifted[k, n].real;
                    FuvDonen[k, n].imag =a* FuvShifted[k, n].imag;
                }
            }
        }



        public Bitmap mFilterGaussHP(int tercih, double rc)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r;

            if (rc <= 1.0)
            { r = (double)N * rc / 2; }
            else if (rc > 1.0 && rc <= N / 2)
            { r = rc; }
            else if (rc >= N / 2)
            { r = (double)N / 2; }
            else
                r = 0.0;


            MatrisGaussHP(r);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }


       


        public Bitmap mFilterGaussBP(int tercih, double rc1, double rc2)
        {
            Bitmap bmp3 = new Bitmap(bmp);          
            double  r1, r2;            

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
            { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
            { r2 = rc2; }
            else if (rc2 >= N / 2)
            { r2 = (double)(N / 2); }
            else
                r2 = 0.0;           

            MatrisGaussBP(r1, r2);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }




        public void MatrisGaussBS(double r1, double r2)
        {   int k, n;
            double d, a, r0, W, D;
            r0 = Math.Sqrt(r1 * r2);
            W = r2 - r1;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));
                    D = (d * d - r0 * r0) / (d * W);
                    a = 1.0 - Math.Exp(-(D * D) / 2);

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
                }
            }
        }


        public Bitmap mFilterGaussBS(int tercih, double rc1, double rc2)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r1, r2;

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
            { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
            { r2 = rc2; }
            else if (rc2 >= N / 2)
            { r2 = (double)(N / 2); }
            else
                r2 = 0.0;

            MatrisGaussBS(r1, r2);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }


        public void MatrisGaussHPlocal( double r, double u, double v)
        {    int k, n;          
            double d, a, x, y;
            x = (double)(N / 2) + u; y = (double)(M / 2) + v;
         

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {

                    d = Math.Sqrt((x - k) * (x - k) + (y - n) * (y - n));

                    a = Math.Exp(-(d * d) / (2 * r * r));

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                    /*
                    if (d <= r)
                    {
                        output[k, n].real = output[k, n].real;
                        output[k, n].imag = output[k, n].imag;
                    }
                    else
                    {
                        output[k, n].real = 0.0;
                        output[k, n].imag = 0.0;
                    }
                  */

                }
            }

           
        }


        public Bitmap mFilterGaussHPlocal(int tercih, double rc,double u,double v)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r;

            if (rc <= 1.0)
            { r = (double)(N * rc / 2); }
            else if (rc > 1.0 && rc <= N / 2)
            { r = rc; }
            else if (rc >= N / 2)
            { r = (double)(N / 2); }
            else
                r = 0.0;


            MatrisGaussHPlocal(r,u,v);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }


       


      /*
        public Complex[,] FFTbutterLP(Complex[,] input, double r, double nb)
        {
            int k, n, N, M; N = input.GetLength(0); M = input.GetLength(1);
            Complex[,] output = new Complex[N, M];
            Complex[,] son = new Complex[N, M];
            output = input;
            son = input;
            double d, a, b;
            output = FFTShift(input);

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));

                    b = Math.Pow(d / r, 2 * nb);
                    a = 1 / (1 + b);

                    output[k, n].real = a * output[k, n].real;
                    output[k, n].imag = a * output[k, n].imag;

                }
            }

            son = RemoveFFTShift(output);
            return son;
        }
       */


        /*
         public Bitmap FilterbutterLP(Bitmap img, double rc, double nb)
         {
             Bitmap bmp1 = new Bitmap(img);
             Bitmap bmp2 = new Bitmap(img);
             int k, n, N, M; N = bmp1.Width; M = bmp1.Height;
             double d, a, r;

             Complex[,] input = new Complex[N, M];
             Complex[,] output = new Complex[N, M];
             Complex[,] son = new Complex[N, M];
             Complex[,] ifxy = new Complex[N, M];

             if (rc <= 1.0)
             { r = (double)N * rc / 2; }
             else if (rc > 1.0 && rc <= N / 2)
             { r = rc; }
             else if (rc >= N / 2)
             { r = (double)N / 2; }
             else
                 r = 0.0;

             input = getFFTaN(bmp1);
             output = input;
             son = input;
             ifxy = input;

             output = FFTbutterLP(input, r, nb);
             son = RemoveFFTShift(output);
             ifxy = FFT2Da(son, 1);
             bmp2 = BitmapFromComplex(ifxy, 1, 2, 1000);
             return bmp2;
         }
        */




        public Bitmap Butter(Bitmap bmp1, double a, double nb)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double b, d, r, x, y;
            int i, j, N, M, q; Color p9;
            N = bmp1.Width; M = bmp1.Height;

            if (a <= 1.0)
            { r = (double)N * a / 2; }
            else if (a > 1.0 && a <= N / 2)
            { r = a; }
            else if (a >= N / 2)
            { r = (double)N / 2; }
            else
                r = 0.0;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    x = (double)(i - N / 2); y = (double)(j - M / 2);
                    d = Math.Sqrt(x * x + y * y);

                    b = Math.Pow(d / r, 2 * nb);
                    d = 255 * 1 / (1 + b);


                    q = (int)d;
                    p9 = Color.FromArgb(q, q, q);

                    bmp2.SetPixel(i, j, p9);

                    if (i == bmp1.Width / 2 || j == bmp1.Height / 2)
                    { p9 = Color.FromArgb(0, 0, 255); bmp2.SetPixel(i, j, p9); }

                }
            }

            return bmp2;
        }



        public void MatrisbutterLP( double r, double nb)
        {   int k, n;
            double d, a, b;           

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));

                    b = Math.Pow(d / r, 2 * nb);
                    a = 1 / (1 + b);

                    FuvDonen[k, n].real =a* FuvShifted[k, n].real;
                    FuvDonen[k, n].imag =a* FuvShifted[k, n].imag;
                }
            }          
        }
        public Bitmap mFilterbutterLP(int tercih, double rc, double nb)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double  r;           

            if (rc <= 1.0)
            { r = (double)(N * rc / 2); }
            else if (rc > 1.0 && rc <= N / 2)
            { r = rc; }
            else if (rc >= N / 2)
            { r = (double)(N / 2); }
            else
                r = 0.0;            

            MatrisbutterLP(r, nb);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }

      public void MatrisbutterHP(double r, double nb)
        {   int k, n;
            double d, a, b;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));

                    if (d == 0) d = 0.001;
                    b = Math.Pow(r /d, 2 * nb);
                    a = 1 / (1 + b);

                    FuvDonen[k, n].real =a* FuvShifted[k, n].real;
                    FuvDonen[k, n].imag =a* FuvShifted[k, n].imag;
                }
            }
        }

        public Bitmap mFilterbutterHP(int tercih, double rc, double nb)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r;

            if (rc <= 1.0)
            { r = (double)(N * rc / 2); }
            else if (rc > 1.0 && rc <= N / 2)
            { r = rc; }
            else if (rc >= N / 2)
            { r = (double)(N / 2); }
            else
                r = 0.0;

            MatrisbutterHP(r, nb);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 1000);
            else
                bmp3 = bmp;

            return bmp3;
        }
   
    

        public void MatrisbutterBS(double r1, double r2, double nb)
        {   int k, n;
            double d, a, b, r0, W, D;
            r0 = Math.Sqrt(r1 * r2);
            W = r2 - r1;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));
                    D = (d * W) / (d * d - r0 * r0);
                    b = Math.Pow(D, 2 * nb);
                    a = 1 / (1 + b);

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
                }
            }

        }

        public Bitmap mFilterbutterBS(int tercih, double rc1, double rc2, double nb)
        {   Bitmap bmp3 = new Bitmap(bmp);
            double  r1, r2;

            if (rc1 <= 1.0)
              { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
              { r1 = rc1; }
            else if (rc1 >= N / 2)
              { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
               { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
               { r2 = rc2; }
            else if (rc2 >= N / 2)
               { r2 = (double)(N / 2); }
            else
                r2 = 0.0;



            MatrisbutterBS(r1, r2, nb);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 1000);
            else
                bmp3 = bmp;

            return bmp3;
        }

      



        public void MatrisbutterBP( double r1, double r2, double nb)
        {    int k, n;           
            double d, a, b, r0, W, D;
            r0 = Math.Sqrt(r1 * r2);
            W = r2 - r1;
            
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));
                    D = (d * W) / (d * d - r0 * r0);
                    b = Math.Pow(D, 2 * nb);
                    a = 1.0 - 1 / (1 + b);

                    FuvDonen[k, n].real =a* FuvShifted[k, n].real;
                    FuvDonen[k, n].imag =a* FuvShifted[k, n].imag;
                }
            }
            
        }

        public Bitmap mFilterbutterBP(int tercih, double rc1, double rc2, double nb)
        {   Bitmap bmp3 = new Bitmap(bmp);
            double  r1, r2;         

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            if (rc2 <= 1.0)
            { r2 = (double)(N * rc2 / 2); }
            else if (rc2 > 1.0 && rc2 <= N / 2)
            { r2 = rc2; }
            else if (rc2 >= N / 2)
            { r2 = (double)(N / 2); }
            else
                r2 = 0.0;           

            MatrisbutterBP(r1, r2,nb);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 5000);
            else
                bmp3 = bmp;

            return bmp3;
        }

        public void MatrisCounterLet2c(int yon)
        {   int k, n;            
            double a, x, y, teta;
        
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }
                    a = 0.0;
                    if (yon == 0)
                    {
                        if ((teta >= 0 && teta < 45.0) || (teta >= (0 + 180.0) && teta < (45.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                        if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                        {
                            a = 1.0;
                        }

                    }
                    else if (yon == 1)
                    {
                        if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                        {
                            a = 1.0;
                        }

                        if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }
           
        }




        public Bitmap Lojistik(Bitmap bmp1, double a, double nb)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double b, d, r, beta, x, y;
            int i, j, N, M, q; Color p9;
            N = bmp1.Width; M = bmp1.Height;

            if (a <= 1.0)
            { r = (double)N * a / 2; }
            else if (a > 1.0 && a <= N / 2)
            { r = a; }
            else if (a >= N / 2)
            { r = (double)N / 2; }
            else
                r = 0.0;

            if (nb <= 1.0)
            { beta = (double)N * nb / 2; }
            else if (nb > 1.0 && nb <= N / 2)
            { beta = nb; }
            else if (nb >= N / 2)
            { beta = (double)N / 2; }
            else
                beta = 0.0;


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    x = (double)(i - N / 2); y = (double)(j - M / 2);
                    d = Math.Sqrt(x * x + y * y);

                    b = 255 * 1.0 / (1.0 + Math.Exp(1 * (d - r) / beta));


                    q = (int)b;
                    p9 = Color.FromArgb(q, q, q);

                    bmp2.SetPixel(i, j, p9);

                    if (i == bmp1.Width / 2 || j == bmp1.Height / 2)
                    { p9 = Color.FromArgb(0, 0, 255); bmp2.SetPixel(i, j, p9); }

                }
            }

            return bmp2;
        }


        public void MatrisLojistikLHP(double r, double b)
        {
            int k, n;
            double d, a;

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    d = Math.Sqrt((double)((M / 2 - n) * (M / 2 - n)) + (double)((N / 2 - k) * (N / 2 - k)));
                    a = 1.0 / (1.0 + Math.Exp(b * (d - r)));

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }

        }

        public Bitmap mFilterLojistikLHP(int tercih, double rc1, double nb)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            double r1;

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;


            MatrisLojistikLHP(r1, nb);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;
        }




        public Bitmap mFilterCounterLet2c(int tercih, int yon)
        {    Bitmap bmp3 = new Bitmap(bmp);
            
            MatrisCounterLet2c(yon);
             RemoveMatrisShift();
             MatrisFFT2D(1);

             if (tercih == 1)
                 bmp3 = BitmapFromFuv(1, 2, 5000);
             else if (tercih == 2)
                 bmp3 = BitmapFromFuvShifted(1, 2, 5000);
             else if (tercih == 3)
                 bmp3 = BitmapFromFuvDonen(1, 2, 5000);
             else if (tercih == 4)
                 bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
             else if (tercih == 5)
                 bmp3 = BitmapFromiFuv(1, 2, 5000);
             else
                 bmp3 = bmp;

             return bmp3;
         
        }



      


        public void MatrisCounterLet2a( double b, int yon, int scale)
        {   int k, n;            
            double a, x, y, teta;
           
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }
                    a = 0.0;

                    if (scale == 0)
                    {
                        if (k <= (b + N / 2) && k >= ((N / 2) - b) && n <= (b + M / 2) && n >= ((M / 2) - b))
                        {
                            if (yon == 0)
                            {
                                if ((teta >= 0 && teta < 45.0) || (teta >= (0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                        }
                    }
                    else if (scale == 1)
                    {
                        if (((k > (b + N / 2)) || k < ((N / 2) - b)) || (n > (b + M / 2) || n < ((M / 2) - b)))
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0 && teta < 45.0) || (teta >= (0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                        }
                    }

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }
           
        }


        public Bitmap mFilterCounterLet2a(int tercih, double rc1, int yon, int scale)
        {
            Bitmap bmp3 = new Bitmap(bmp);           
            double r1;        


            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            MatrisCounterLet2a( r1, yon, scale);

            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;      
           
        }    

       public void MatrisCounterLet2(int yon)
        {   int k, n;           
            double a, x, y, teta;
           
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }
                    a = 0.0;
                    if (yon == 0)
                    {
                        if ((teta >= 0.0 && teta < 90.0) || (teta >= (0.0 + 180.0) && teta < (90.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }
                    else if (yon == 1)
                    {
                        if ((teta >= 90.0 && teta < 180.0) || (teta >= (90.0 + 180.0) && teta < (180.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }


                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }
           
        }

        public Bitmap mFilterCounterLet2(int tercih, int yon)
        {
            Bitmap bmp3 = new Bitmap(bmp);
            
            MatrisCounterLet2(yon);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;         
        }     



       


        public void MatrisCounterLet4(int yon)
        {   int k, n;           
            double a, x, y, teta;
           
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }
                    a = 0.0;
                    if (yon == 0)
                    {
                        if ((teta >= 0.0 && teta < 45.0) || (teta >= (0.0 + 180.0) && teta < (45.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }
                    else if (yon == 1)
                    {
                        if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }
                    else if (yon == 2)
                    {
                        if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }
                    else if (yon == 3)
                    {
                        if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }


                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }
           
        }

        public Bitmap mFilterCounterLet4(int tercih, int yon)
        {   Bitmap bmp3 = new Bitmap(bmp);

            MatrisCounterLet4(yon);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;         
        }


       

       

        public void MatrisCounterLet4a(double b, int yon, int scale)
        {   int k, n;           
            double a, x, y, teta;
           
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }



                    a = 0.0;

                    if (scale == 0)
                    {
                        if (k <= (b + N / 2) && k >= ((N / 2) - b) && n <= (b + M / 2) && n >= ((M / 2) - b))
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 45.0) || (teta >= (0.0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 3)
                            {
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }



                        }
                    }

                    else if (scale == 1)
                    {
                        if (((k > (b + N / 2)) || k < ((N / 2) - b)) || (n > (b + M / 2) || n < ((M / 2) - b)))
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 45.0) || (teta >= (0.0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 3)
                            {
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                        }
                    }

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }
           
        }


        public Bitmap mFilterCounterLet4a(int tercih, double rc1, int yon, int scale)
        {   Bitmap bmp3 = new Bitmap(bmp);          
            double r1;          

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            MatrisCounterLet4a(r1, yon, scale);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;       
        }

      

        public void MatrisCounterLet8(int yon)
        {   int k, n;           
            double a, x, y, teta;
          
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }

                    a = 0.0;

                    if (yon == 0)
                    {
                        if ((teta >= 0.0 && teta < 22.5) || (teta >= (0.0 + 180.0) && teta < (22.5 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }
                    else if (yon == 1)
                    {
                        if ((teta >= 22.5 && teta < 45.0) || (teta >= (22.5 + 180.0) && teta < (45.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }
                    else if (yon == 2)
                    {
                        if ((teta >= 45.0 && teta < 67.5) || (teta >= (45.0 + 180.0) && teta < (67.5 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }

                    else if (yon == 3)
                    {
                        if ((teta >= 67.5 && teta < 90.0) || (teta >= (67.5 + 180.0) && teta < (90.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }

                    else if (yon == 4)
                    {
                        if ((teta >= 90.0 && teta < 112.5) || (teta >= (90.0 + 180.0) && teta < (112.5 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }

                    else if (yon == 5)
                    {
                        if ((teta >= 112.5 && teta < 135.0) || (teta >= (112.5 + 180.0) && teta < (135.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }

                    else if (yon == 6)
                    {

                        if ((teta >= 135.0 && teta < 157.5) || (teta >= (135.0 + 180.0) && teta < (157.5 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }

                    else if (yon == 7)
                    {
                        if ((teta >= 157.5 && teta < 180.0) || (teta >= (157.5 + 180.0) && teta < (180.0 + 180.0)))
                        {
                            a = 1.0;
                        }
                    }

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }         
        }

        public Bitmap mFilterCounterLet8(int tercih, int yon)
        {   Bitmap bmp3 = new Bitmap(bmp);

            MatrisCounterLet8(yon);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;         
        }

    

       

        public void  MatrisCounterLet8a( double b, int yon, int scale)
        {    int k, n;            
            double a, x, y, teta;           

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);


                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }


                    teta = teta + 225; if (teta > 360) teta = teta - 360;

                    a = 0.0;
                    if (scale == 0)
                    {
                        if (k <= (b + N / 2) && k >= ((N / 2) - b) && n <= (b + M / 2) && n >= ((M / 2) - b))
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 22.5) || (teta >= (0.0 + 180.0) && teta < (22.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 22.5 && teta < 45.0) || (teta >= (22.5 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 45.0 && teta < 67.5) || (teta >= (45.0 + 180.0) && teta < (67.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 3)
                            {
                                if ((teta >= 67.5 && teta < 90.0) || (teta >= (67.5 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 4)
                            {
                                if ((teta >= 90.0 && teta < 112.5) || (teta >= (90.0 + 180.0) && teta < (112.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 5)
                            {
                                if ((teta >= 112.5 && teta < 135.0) || (teta >= (112.5 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 6)
                            {

                                if ((teta >= 135.0 && teta < 157.5) || (teta >= (135.0 + 180.0) && teta < (157.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 7)
                            {
                                if ((teta >= 157.5 && teta < 180.0) || (teta >= (157.5 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                        }
                    }

                    else if (scale == 1)
                    {
                        if (((k > (b + N / 2)) || k < ((N / 2) - b)) || (n > (b + M / 2) || n < ((M / 2) - b)))
                        {
                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 22.5) || (teta >= (0.0 + 180.0) && teta < (22.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 22.5 && teta < 45.0) || (teta >= (22.5 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 45.0 && teta < 67.5) || (teta >= (45.0 + 180.0) && teta < (67.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 3)
                            {
                                if ((teta >= 67.5 && teta < 90.0) || (teta >= (67.5 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 4)
                            {
                                if ((teta >= 90.0 && teta < 112.5) || (teta >= (90.0 + 180.0) && teta < (112.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 5)
                            {
                                if ((teta >= 112.5 && teta < 135.0) || (teta >= (112.5 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 6)
                            {

                                if ((teta >= 135.0 && teta < 157.5) || (teta >= (135.0 + 180.0) && teta < (157.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 7)
                            {
                                if ((teta >= 157.5 && teta < 180.0) || (teta >= (157.5 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                        }
                    }

                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
                }
            }
         
        }

        public Bitmap mFilterCounterLet8a(int tercih, double rc1, int yon, int scale)
        {  Bitmap bmp3 = new Bitmap(bmp);
            double r1;
           
            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            MatrisCounterLet8a( r1, yon, scale);

            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;         
        }


        public void MatrisSector2(double b, int yon, int scale)
        {   int k, n;            
            double a, d, x, y, teta;
         

            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }



                    d = Math.Sqrt((double)(x * x) + (double)(y * y));

                    a = 0.0;

                    teta = teta + 0; if (teta > 360) teta = teta - 360;

                    if (scale == 0)
                    {
                        if (d <= b)
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 90.0) || (teta >= (0.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 90.0 && teta < 180.0) || (teta >= (90.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                        }
                    }

                    else if (scale == 1)
                    {
                        if (d >= b)
                        {
                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 90.0) || (teta >= (0.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 90.0 && teta < 180.0) || (teta >= (90.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }


                        }
                    }


                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
                }
            }
          
        }



        public Bitmap mFilterSector2(int tercih, double rc1, int yon, int scale)
        {   Bitmap bmp3 = new Bitmap(bmp);          
            double r1;         

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

           MatrisSector2( r1, yon, scale);

           RemoveMatrisShift();
           MatrisFFT2D(1);

           if (tercih == 1)
               bmp3 = BitmapFromFuv(1, 2, 5000);
           else if (tercih == 2)
               bmp3 = BitmapFromFuvShifted(1, 2, 5000);
           else if (tercih == 3)
               bmp3 = BitmapFromFuvDonen(1, 2, 5000);
           else if (tercih == 4)
               bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
           else if (tercih == 5)
               bmp3 = BitmapFromiFuv(1, 2, 100);
           else
               bmp3 = bmp;

           return bmp3;        
        }

        public void MatrisSector2a( double b, int yon, int scale)
        {   int k, n;            
            double a, d, x, y, teta;           
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }


                    d = Math.Sqrt((double)(x * x) + (double)(y * y));
                    a = 0.0;

                    if (scale == 0)
                    {
                        if (d <= b)
                        {
                            if (yon == 0)
                            {
                                if ((teta >= 0 && teta < 45.0) || (teta >= (0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                        }
                    }
                    else if (scale == 1)
                    {
                        if (d >= b)
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0 && teta < 45.0) || (teta >= (0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }

                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }


                        }
                    }


                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
                }
            }
           
        }


        public Bitmap mFilterSector2a(int tercih, double rc1, int yon, int scale)
        {   Bitmap bmp3 = new Bitmap(bmp);           
            double r1;

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            MatrisSector2a(r1, yon, scale);

            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;         
        }



        public void MatrisSector4a(double b, int yon, int scale)
        {   int k, n;            
            double a, d, x, y, teta;
           
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }



                    a = 0.0;
                    d = Math.Sqrt((double)(x * x) + (double)(y * y));

                    if (scale == 0)
                    {
                        if (d <= b)
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 45.0) || (teta >= (0.0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 3)
                            {
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }



                        }
                    }

                    else if (scale == 1)
                    {
                        if (d >= b)
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 45.0) || (teta >= (0.0 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 3)
                            {
                                if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                        }

                    }
                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
                }
            }
        
        }


        public Bitmap mFilterSector4a(int tercih, double rc1, int yon, int scale)
        {   Bitmap bmp3 = new Bitmap(bmp);
            double r1;
           
            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

            MatrisSector4a( r1, yon, scale);

            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;        
         
        }

        public void MatrisSector8a(double b, int yon, int scale)
        {   int k, n;           
            double a, d, x, y, teta;
          
            for (n = 0; n < M; n++)
            {
                for (k = 0; k < N; k++)
                {
                    x = (double)(k - N / 2); y = (double)(n - M / 2);


                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }


                    d = Math.Sqrt((double)(x * x) + (double)(y * y));

                    a = 0.0;
                    if (scale == 0)
                    {
                        if (d < b)
                        {

                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 22.5) || (teta >= (0.0 + 180.0) && teta < (22.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 22.5 && teta < 45.0) || (teta >= (22.5 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 45.0 && teta < 67.5) || (teta >= (45.0 + 180.0) && teta < (67.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 3)
                            {
                                if ((teta >= 67.5 && teta < 90.0) || (teta >= (67.5 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 4)
                            {
                                if ((teta >= 90.0 && teta < 112.5) || (teta >= (90.0 + 180.0) && teta < (112.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 5)
                            {
                                if ((teta >= 112.5 && teta < 135.0) || (teta >= (112.5 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 6)
                            {

                                if ((teta >= 135.0 && teta < 157.5) || (teta >= (135.0 + 180.0) && teta < (157.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 7)
                            {
                                if ((teta >= 157.5 && teta < 180.0) || (teta >= (157.5 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                        }
                    }

                    else if (scale == 1)
                    {
                        if (d >= b)
                        {
                            if (yon == 0)
                            {
                                if ((teta >= 0.0 && teta < 22.5) || (teta >= (0.0 + 180.0) && teta < (22.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 1)
                            {
                                if ((teta >= 22.5 && teta < 45.0) || (teta >= (22.5 + 180.0) && teta < (45.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }
                            else if (yon == 2)
                            {
                                if ((teta >= 45.0 && teta < 67.5) || (teta >= (45.0 + 180.0) && teta < (67.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 3)
                            {
                                if ((teta >= 67.5 && teta < 90.0) || (teta >= (67.5 + 180.0) && teta < (90.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 4)
                            {
                                if ((teta >= 90.0 && teta < 112.5) || (teta >= (90.0 + 180.0) && teta < (112.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 5)
                            {
                                if ((teta >= 112.5 && teta < 135.0) || (teta >= (112.5 + 180.0) && teta < (135.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 6)
                            {

                                if ((teta >= 135.0 && teta < 157.5) || (teta >= (135.0 + 180.0) && teta < (157.5 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }

                            else if (yon == 7)
                            {
                                if ((teta >= 157.5 && teta < 180.0) || (teta >= (157.5 + 180.0) && teta < (180.0 + 180.0)))
                                {
                                    a = 1.0;
                                }
                            }


                        }
                    }
                    FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                    FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

                }
            }

          
        }
     

       public Bitmap mFilterSector8a(int tercih, double rc1, int yon, int scale)
        {
            Bitmap bmp3 = new Bitmap(bmp);          
            double r1;          

            if (rc1 <= 1.0)
            { r1 = (double)(N * rc1 / 2); }
            else if (rc1 > 1.0 && rc1 <= N / 2)
            { r1 = rc1; }
            else if (rc1 >= N / 2)
            { r1 = (double)(N / 2); }
            else
                r1 = 0.0;

             MatrisCounterLet8a(r1, yon, scale);
            RemoveMatrisShift();
            MatrisFFT2D(1);

            if (tercih == 1)
                bmp3 = BitmapFromFuv(1, 2, 5000);
            else if (tercih == 2)
                bmp3 = BitmapFromFuvShifted(1, 2, 5000);
            else if (tercih == 3)
                bmp3 = BitmapFromFuvDonen(1, 2, 5000);
            else if (tercih == 4)
                bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
            else if (tercih == 5)
                bmp3 = BitmapFromiFuv(1, 2, 100);
            else
                bmp3 = bmp;

            return bmp3;       
       }

       public void MatrisSectorDilimi( double b, double alfa, double beta, int scale)
       {    int k, n;           
           double a, d, x, y, teta;
          
           for (n = 0; n < M; n++)
           {
               for (k = 0; k < N; k++)
               {
                   x = (double)(k - N / 2); y = (double)(n - M / 2);

                   if (x == 0)
                   {
                       if (y == 0)
                       { teta = 0.0; }
                       else if (y < 0)
                       { teta = 270.0; }
                       else teta = 90.0;
                   }
                   else if (x < 0 && y == 0) { teta = 180.0; }
                   else if (x < 0 && y > 0)
                   { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x > 0 && y < 0)
                   { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x < 0 && y < 0)
                   { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else
                   { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }




                   d = Math.Sqrt((double)(x * x) + (double)(y * y));
                   a = 0.0;

                   if (scale == 0)
                   {
                       if (d <= b && d >= 0.1 * b)
                       {

                           if ((teta >= beta && teta < (alfa + beta)) || (teta >= (beta + 180.0) && teta < (alfa + beta + 180.0)))
                           { a = 1.0; }

                       }
                   }
                   else if (scale == 1)
                   {
                       if (d >= b)
                       {
                           if ((teta >= beta && teta < (alfa + beta)) || (teta >= (beta + 180.0) && teta < (alfa + beta + 180.0)))
                           { a = 1.0; }

                       }
                   }

                   FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                   FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;
               }
           }
         
       }

       public Bitmap mFilterSectorDilimi(int tercih, double rc1, double alfa, double beta, int scale)
       {   Bitmap bmp3 = new Bitmap(bmp);
           double r1;         

           if (rc1 <= 1.0)
           { r1 = (double)(N * rc1 / 2); }
           else if (rc1 > 1.0 && rc1 <= N / 2)
           { r1 = rc1; }
           else if (rc1 >= N / 2)
           { r1 = (double)(N / 2); }
           else
               r1 = 0.0;

           MatrisSectorDilimi( r1, alfa, beta, scale);
           RemoveMatrisShift();
           MatrisFFT2D(1);

           if (tercih == 1)
               bmp3 = BitmapFromFuv(1, 2, 5000);
           else if (tercih == 2)
               bmp3 = BitmapFromFuvShifted(1, 2, 5000);
           else if (tercih == 3)
               bmp3 = BitmapFromFuvDonen(1, 2, 5000);
           else if (tercih == 4)
               bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
           else if (tercih == 5)
               bmp3 = BitmapFromiFuv(1, 2, 100);
           else
               bmp3 = bmp;

           return bmp3;       
          
       }


       public void MatrisCell( double r1, double r2, double beta, double alfa, int tip)
       {   int k, n;           
           double a, d, x, y, teta;
          
           for (n = 0; n < M; n++)
           {
               for (k = 0; k < N; k++)
               {
                   x = (double)(k - N / 2); y = (double)(n - M / 2);

                   if (x == 0)
                   {
                       if (y == 0)
                       { teta = 0.0; }
                       else if (y < 0)
                       { teta = 270.0; }
                       else teta = 90.0;
                   }
                   else if (x < 0 && y == 0) { teta = 180.0; }
                   else if (x < 0 && y > 0)
                   { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x > 0 && y < 0)
                   { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x < 0 && y < 0)
                   { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else
                   { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }


                   d = Math.Sqrt((double)(x * x) + (double)(y * y));

                   a = 0.0;

                   if (tip == 1)
                   {
                       a = 0.0;
                       if (d >= r1 && d <= r2)
                       {
                           if ((teta >= beta && teta < (alfa + beta)) || (teta >= (beta + 180.0) && teta < (alfa + beta + 180.0)))
                           { a = 1.0; }
                       }
                   }
                   else if (tip == 0)
                   {
                       a = 1.0;
                       if (d >= r1 && d <= r2)
                       {
                           if ((teta >= beta && teta < (alfa + beta)) || (teta >= (beta + 180.0) && teta < (alfa + beta + 180.0)))
                           { a = 0.0; }
                       }

                   }
                   else
                       a = 0.0;

                   FuvDonen[k, n].real = a * FuvShifted[k, n].real;
                   FuvDonen[k, n].imag = a * FuvShifted[k, n].imag;

               }
           }
         
       }



       public Bitmap mFilterCell(int tercih, double rc1, double rc2, double beta, double alfa, int tip)
       {   Bitmap bmp3 = new Bitmap(bmp);
           double  r1, r2;
         

           if (rc1 <= 1.0)
           { r1 = (double)(N * rc1 / 2); }
           else if (rc1 > 1.0 && rc1 <= N / 2)
           { r1 = rc1; }
           else if (rc1 >= N / 2)
           { r1 = (double)(N / 2); }
           else
               r1 = 0.0;

           if (rc2 <= 1.0)
           { r2 = (double)(N * rc2 / 2); }
           else if ((rc2 > 1.0 && rc2 <= 1.41))
           {
               r2 = (double)(N / 2);
               r2 = rc2 * r2;
           }
           else if (rc2 > 1.41)
           { r2 = rc2; }
           else
               r2 = 0.0;
            
           MatrisCell(r1, r2, beta, alfa, tip);

           RemoveMatrisShift();
           MatrisFFT2D(1);

           if (tercih == 1)
               bmp3 = BitmapFromFuv(1, 2, 5000);
           else if (tercih == 2)
               bmp3 = BitmapFromFuvShifted(1, 2, 5000);
           else if (tercih == 3)
               bmp3 = BitmapFromFuvDonen(1, 2, 5000);
           else if (tercih == 4)
               bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
           else if (tercih == 5)
               bmp3 = BitmapFromiFuv(1, 2, 100);
           else
               bmp3 = bmp;

           return bmp3;         

       }




       public Bitmap Lemmiscate(Bitmap bmp1, double a, double beta)
       {
           Bitmap bmp2 = (Bitmap)bmp1.Clone();
           double b, c, d, r, teta, x, y;
           int i, j, N, M; Color p9;
           N = bmp1.Width; M = bmp1.Height;
           for (j = 0; j < bmp1.Height; j++)
           {
               for (i = 0; i < bmp1.Width; i++)
               {
                   // p9 = Color.FromArgb(255, 0, 255);   bmp2.SetPixel(i, j, p9);
                   x = (double)(i - N / 2); y = (double)(j - M / 2);

                   if (x == 0)
                   {
                       if (y == 0)
                       { teta = 0.0; }
                       else if (y < 0)
                       { teta = 270.0; }
                       else teta = 90.0;
                   }
                   else if (x < 0 && y == 0) { teta = 180.0; }
                   else if (x < 0 && y > 0)
                   { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x > 0 && y < 0)
                   { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x < 0 && y < 0)
                   { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else
                   { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }

                   c = a * a * Math.Cos(2 * teta * (Math.PI / 180) + 2 * beta * 1 * (Math.PI / 180));
                   if (c < 0) c = 0;
                   r = Math.Sqrt(c);
                   d = Math.Sqrt(x * x + y * y);                 


                   if (d <= r)
                       p9 = Color.FromArgb(0, 255, 255);
                   else
                       p9 = Color.FromArgb(0, 0, 0);

                   bmp2.SetPixel(i, j, p9);                



                   if (i == bmp1.Width / 2 || j == bmp1.Height / 2)
                   { p9 = Color.FromArgb(255, 0, 0); bmp2.SetPixel(i, j, p9); }

               }
           }

           return bmp2;
       }




       public void MatrisLemniscateLP(double a, double beta)
       {    int k, n;          
           double d, r, teta, x, y;
         

           for (n = 0; n < M; n++)
           {
               for (k = 0; k < N; k++)
               {

                   x = (double)(k - N / 2); y = (double)(n - M / 2);


                   if (x == 0)
                   {
                       if (y == 0)
                       { teta = 0.0; }
                       else if (y < 0)
                       { teta = 270.0; }
                       else teta = 90.0;
                   }
                   else if (x < 0 && y == 0) { teta = 180.0; }
                   else if (x < 0 && y > 0)
                   { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x > 0 && y < 0)
                   { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else if (x < 0 && y < 0)
                   { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                   else
                   { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }



                   r = Math.Sqrt(a * a * Math.Cos(2 * teta * (Math.PI / 180) + 2 * beta * (Math.PI / 180)));
                   d = Math.Sqrt(x * x + y * y);

                   if (d <= r)
                   {
                       FuvDonen[k, n].real = FuvShifted[k, n].real;
                       FuvDonen[k, n].imag = FuvShifted[k, n].imag;
                   }
                   else
                   {
                       FuvDonen[k, n].real =0.0;
                       FuvDonen[k, n].imag =0.0;
                   }
               

               }
           }

        
       }





       public Bitmap mFilterlemniscateLP(int tercih, double rc, double beta)
       {
           Bitmap bmp3 = new Bitmap(bmp);            
           double r;          

           if (rc <= 1.0)
           { r = (double)(N * rc / 2); }
           else if (rc > 1.0 && rc <= N / 2)
           { r = rc; }
           else if (rc >= N / 2)
           { r = (double)(N / 2); }
           else
               r = 0.0;

          MatrisLemniscateLP( r, beta);

          RemoveMatrisShift();
          MatrisFFT2D(1);

          if (tercih == 1)
              bmp3 = BitmapFromFuv(1, 2, 5000);
          else if (tercih == 2)
              bmp3 = BitmapFromFuvShifted(1, 2, 5000);
          else if (tercih == 3)
              bmp3 = BitmapFromFuvDonen(1, 2, 5000);
          else if (tercih == 4)
              bmp3 = BitmapFromFuvdeShifted(1, 2, 5000);
          else if (tercih == 5)
              bmp3 = BitmapFromiFuv(1, 2, 100);
          else
              bmp3 = bmp;

          return bmp3; 
       }








        public Bitmap CounterDirection4(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double b, d, r, r2, teta, x, y, xm, ym;
            int i, j, N, M; Color p9;
            N = bmp1.Width; M = bmp1.Height;


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = Color.FromArgb(0, 0, 0);
                    bmp2.SetPixel(i, j, p9);
                    x = (double)(i - N / 2); y = (double)(j - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }



                    if ((teta >= 0.0 && teta <45.0) || (teta >= (0.0 + 180.0) && teta < (45.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 255, 0);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >=45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 0, 255);

                        bmp2.SetPixel(i, j, p9);
                    }

                   if ((teta >=90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 255, 255);

                        bmp2.SetPixel(i, j, p9);
                    }

                   if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 0, 0);

                        bmp2.SetPixel(i, j, p9);
                    }

                }
            }

            return bmp2;
        }




        public Bitmap CounterDirection4a(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double b, d, r, r2, teta, x, y, xm, ym;
            int i, j, N, M; Color p9;
            N = bmp1.Width; M = bmp1.Height;
             b = (double)( N / 4);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = Color.FromArgb(0, 0, 0);
                    bmp2.SetPixel(i, j, p9);
                    x = (double)(i - N / 2); y = (double)(j - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }



                    if ((teta >= 0.0 && teta < 45.0) || (teta >= (0.0 + 180.0) && teta < (45.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 0, 0);

                        if (x <= (b ) && x >= (- b) && y <= (b ) && y >= (- b))
                        {
                             p9 = Color.FromArgb(255, 255, 255);
                        }

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 45.0 && teta < 90.0) || (teta >= (45.0 + 180.0) && teta < (90.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 0, 255);

                        if (x <= (b + N / 2) && x >= ((N / 2) - b) && y <= (b + M / 2) && y >= ((M / 2) - b))
                        {  
                            p9 = Color.FromArgb(255,255,0);
                        }

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 90.0 && teta < 135.0) || (teta >= (90.0 + 180.0) && teta < (135.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0,255, 0);

                        if (x <= (b + N / 2) && x >= ((N / 2) - b) && y <= (b + M / 2) && y >= ((M / 2) - b))
                        { 
                            p9 = Color.FromArgb(255, 0,255);
                        }

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 135.0 && teta < 180.0) || (teta >= (135.0 + 180.0) && teta < (180.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(255, 0, 0);

                        if (x <= (b + N / 2) && x >= ((N / 2) - b) && y <= (b + M / 2) && y >= ((M / 2) - b))
                        { 
                            p9 = Color.FromArgb(0, 255,255);
                        }

                        bmp2.SetPixel(i, j, p9);
                    }

                }
            }

            return bmp2;
        }

        public Bitmap CounterDirection8(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double b, d, r, r2, teta, x, y, xm, ym;
            int i, j, N, M; Color p9;
            N = bmp1.Width; M = bmp1.Height;

            
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = Color.FromArgb(0, 0, 0);
                    bmp2.SetPixel(i, j, p9);
                    x = (double)(i - N / 2); y = (double)(j - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }



                    if ((teta >= 0.0 && teta < 22.5) || (teta >= (0.0 + 180.0) && teta < (22.5 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 255, 0);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 22.5 && teta < 45.0) || (teta >= (22.5 + 180.0) && teta < (45.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 0, 255);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 45.0 && teta < 67.5) || (teta >= (45.0 + 180.0) && teta < (67.5 + 180.0)))
                    {
                        p9 = Color.FromArgb(255, 0, 0);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 67.5 && teta < 90.0) || (teta >= (67.5 + 180.0) && teta < (90.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(255, 0, 255);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 90.0 && teta < 112.5) || (teta >= (90.0 + 180.0) && teta < (112.5 + 180.0)))
                    {
                        p9 = Color.FromArgb(255, 255, 0);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 112.5 && teta < 135.0) || (teta >= (112.5 + 180.0) && teta < (135.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 255, 255);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 135.0 && teta < 157.5) || (teta >= (135.0 + 180.0) && teta < (157.5 + 180.0)))
                    {
                        p9 = Color.FromArgb(128, 128, 0);

                        bmp2.SetPixel(i, j, p9);
                    }

                    if ((teta >= 157.5 && teta < 180.0) || (teta >= (157.5 + 180.0) && teta < (180.0 + 180.0)))
                    {
                        p9 = Color.FromArgb(0, 0, 0);

                        bmp2.SetPixel(i, j, p9);
                    }

                }
            }

            return bmp2;
        }



        public Bitmap SectorLP(Bitmap bmp1, double a)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double b, d, r, r2, teta, x, y, xm, ym;
            int i, j, N, M; Color p9;
            N = bmp1.Width; M = bmp1.Height;


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = Color.FromArgb(255, 255, 255);
                    bmp2.SetPixel(i, j, p9);
                    x = (double)(i - N / 2); y = (double)(j - M / 2);

                    if (x == 0)
                    {
                        if (y == 0)
                        { teta = 0.0; }
                        else if (y < 0)
                        { teta = 270.0; }
                        else teta = 90.0;
                    }
                    else if (x < 0 && y == 0) { teta = 180.0; }
                    else if (x < 0 && y > 0)
                    { x = -x; teta = 180 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x > 0 && y < 0)
                    { y = -y; teta = 360 - ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else if (x < 0 && y < 0)
                    { x = -x; y = -y; teta = 180 + ((Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI)); }
                    else
                    { teta = (Math.Atan((double)(y) / (double)(x))) * (180 / Math.PI); }


                    d = Math.Sqrt(x * x + y * y);
                    if (d >= a)
                    {
                        if ((teta >= 0.0 && teta < 22.5) || (teta >= (0.0 + 180.0) && teta < (22.5 + 180.0)))
                        {
                            p9 = Color.FromArgb(0, 255, 0);

                            bmp2.SetPixel(i, j, p9);
                        }

                        if ((teta >= 22.5 && teta < 45.0) || (teta >= (22.5 + 180.0) && teta < (45.0 + 180.0)))
                        {
                            p9 = Color.FromArgb(0, 0, 255);

                            bmp2.SetPixel(i, j, p9);
                        }

                        if ((teta >= 45.0 && teta < 67.5) || (teta >= (45.0 + 180.0) && teta < (67.5 + 180.0)))
                        {
                            p9 = Color.FromArgb(255, 0, 0);

                            bmp2.SetPixel(i, j, p9);
                        }

                        if ((teta >= 67.5 && teta < 90.0) || (teta >= (67.5 + 180.0) && teta < (90.0 + 180.0)))
                        {
                            p9 = Color.FromArgb(255, 0, 255);

                            bmp2.SetPixel(i, j, p9);
                        }

                        if ((teta >= 90.0 && teta < 112.5) || (teta >= (90.0 + 180.0) && teta < (112.5 + 180.0)))
                        {
                            p9 = Color.FromArgb(255, 255, 0);

                            bmp2.SetPixel(i, j, p9);
                        }

                        if ((teta >= 112.5 && teta < 135.0) || (teta >= (112.5 + 180.0) && teta < (135.0 + 180.0)))
                        {
                            p9 = Color.FromArgb(0, 255, 255);

                            bmp2.SetPixel(i, j, p9);
                        }

                        if ((teta >= 135.0 && teta < 157.5) || (teta >= (135.0 + 180.0) && teta < (157.5 + 180.0)))
                        {
                            p9 = Color.FromArgb(128, 128, 0);

                            bmp2.SetPixel(i, j, p9);
                        }

                        if ((teta >= 157.5 && teta < 180.0) || (teta >= (157.5 + 180.0) && teta < (180.0 + 180.0)))
                        {
                            p9 = Color.FromArgb(0, 0, 0);

                            bmp2.SetPixel(i, j, p9);
                        }

                        bmp2.SetPixel(i, j, p9);


                    }


                }
            }


            return bmp2;
        }















    }
}
