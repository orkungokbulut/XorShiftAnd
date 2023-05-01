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
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;


namespace MedPic
{
    class Resim
    {
        public string DosyaAdi;
        public  Color p9, c1, c2;
        public double memTresh = 0.85;
        public bool Pixelsecimi ;
        public int[,] data; 
        public Bitmap resim1, resim2;
        public string chaincode = ""; 
        public int xs = 0; public int ys = 0;
        public int xc = 0; public int yc = 0;
        bool kenarvar = true;
        public  ArrayList ComplexChain = new ArrayList();
        public Color[] segmentColors;

     
        public Resim()
        {   resim1 = new Bitmap("c:\\Medpic\\lena.bmp");
            DosyaAdi = "c:\\Medpic\\lena.bmp";
        }
        public Resim(string dosya)
        { resim1 = new Bitmap(dosya); Pixelsecimi = false; }

        public struct bilgi
        {
            public int rt, gt, bt;
            public int ra, ga, ba;
            public int rc, gc, bc;
            public int N;

        };



        public int ikomsu(int komsuno, int i)
        {
            if (komsuno == 0)
            { return i - 1; }
            else if (komsuno == 1)
            { return i; }
            else if (komsuno == 2)
            { return i + 1; }
            else if (komsuno == 3)
            { return i - 1; }
            else if (komsuno == 4)
            { return i + 1; }
            else if (komsuno == 5)
            { return i - 1; }
            else if (komsuno == 6)
            { return i; }
            else if (komsuno == 7)
            { return i + 1; }
            else if (komsuno == 8)
            { return i; }
            else if (komsuno == 9)
            { return i - 2; }
            else if (komsuno == 10)
            { return i - 1; }
            else if (komsuno == 11)
            { return i; }
            else if (komsuno == 12)
            { return i + 1; }
            else if (komsuno == 13)
            { return i + 2; }
            else if (komsuno == 14)
            { return i - 2; }
            else if (komsuno == 15)
            { return i + 2; }
            else if (komsuno == 16)
            { return i - 2; }
            else if (komsuno == 17)
            { return i + 2; }
            else if (komsuno == 18)
            { return i - 2; }
            else if (komsuno == 19)
            { return i + 2; }
            else if (komsuno == 20)
            { return i - 2; }
            else if (komsuno == 21)
            { return i - 1; }
            else if (komsuno == 22)
            { return i; }
            else if (komsuno == 23)
            { return i + 1; }
            else if (komsuno == 24)
            { return i + 2; }
            return 0;
        }

        public int jkomsu(int komsuno, int j)
        {
            if (komsuno == 0)
            { return j - 1; }
            else if (komsuno == 1)
            { return j - 1; }
            else if (komsuno == 2)
            { return j - 1; }
            else if (komsuno == 3)
            { return j; }
            else if (komsuno == 4)
            { return j; }
            else if (komsuno == 5)
            { return j + 1; }
            else if (komsuno == 6)
            { return j + 1; }
            else if (komsuno == 7)
            { return j + 1; }
            else if (komsuno == 8)
            { return j; }
            else if (komsuno == 9)
            { return j - 2; }
            else if (komsuno == 10)
            { return j - 2; }
            else if (komsuno == 11)
            { return j - 2; }
            else if (komsuno == 12)
            { return j - 2; }
            else if (komsuno == 13)
            { return j - 2; }
            else if (komsuno == 14)
            { return j - 1; }
            else if (komsuno == 15)
            { return j - 1; }
            else if (komsuno == 16)
            { return j; }
            else if (komsuno == 17)
            { return j; }
            else if (komsuno == 18)
            { return j + 1; }
            else if (komsuno == 19)
            { return j + 1; }
            else if (komsuno == 20)
            { return j + 2; }
            else if (komsuno == 21)
            { return j + 2; }
            else if (komsuno == 22)
            { return j + 2; }
            else if (komsuno == 23)
            { return j + 2; }
            else if (komsuno == 24)
            { return j + 2; }
            return 0;
        }

        public int resimdemi(int i, int j, int w, int h)
        {
            if (i < 0 || i > w - 1 || j < 0 || j > h - 1)
            { return 0; }
            else { return 1; }
        }



        public Bitmap GetCom(Bitmap bmp1, int x, int y, int z)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color p9; int i, j; int r, g, b;
            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    if (x == 1) r = p9.R;
                    else if (x == 0) r = 0;
                    else if (x == -1) r = 255 - p9.R;
                    else if (x == 10) r = p9.R;
                    else if (x == 20) r = p9.G;
                    else if (x == 30) r = p9.B;
                    else if (x == -10) r = 255 - p9.R;
                    else if (x == -20) r = 255 - p9.G;
                    else if (x == -30) r = 255 - p9.B;
                    else r = 1000;

                    if (y == 1) g = p9.G;
                    else if (y == 0) g = 0;
                    else if (y == -1) g = 255 - p9.G;
                    else if (y == 10) g = p9.R;
                    else if (y == 20) g = p9.G;
                    else if (y == 30) g = p9.B;
                    else if (y == -10) g = 255 - p9.R;
                    else if (y == -20) g = 255 - p9.G;
                    else if (y == -30) g = 255 - p9.B;
                    else g = 1000;

                    if (z == 1) b = p9.B;
                    else if (z == 0) b = 0;
                    else if (z == -1) b = 255 - p9.B;
                    else if (z == 10) b = p9.R;
                    else if (z == 20) b = p9.G;
                    else if (z == 30) b = p9.B;
                    else if (z == -10) b = 255 - p9.R;
                    else if (z == -20) b = 255 - p9.G;
                    else if (z == -30) b = 255 - p9.B;
                    else b = 1000;

                    p9 = Color.FromArgb(r, g, b);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }

        public Bitmap GetComf(Bitmap bmp1, double x, double y, double z)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color p9; int i, j; int r, g, b;
            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    if (x > 0) r = (int)(x * p9.R);
                    else if (x == 0) r = 0;
                    else if (x < 0) r = 255 - (int)(x * p9.R);
                    else r = 255;

                    if (y > 0) g = (int)(y * p9.G);
                    else if (y == 0) g = 0;
                    else if (y < 0) g = 255 - (int)(y * p9.G);
                    else g = 255;

                    if (z > 0) b = (int)(z * p9.B);
                    else if (z == 0) b = 0;
                    else if (z < 0) b = 255 - (int)(z * p9.B);
                    else b = 255;

                    p9 = Color.FromArgb(r, g, b);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }

        public Bitmap Gray(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color p9; int i, j; double q;
            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    q = 0.299 * p9.R + 0.587 * p9.G + 0.114 * p9.B;
                    p9 = Color.FromArgb((int)q, (int)q, (int)q);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }



        public Bitmap Mean3(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double q1, q2, q3;
            double[] w;
            w = new double[9];
            int i, j, b, x, y;

            w[0] = 1; w[1] = 1; w[2] = 1;
            w[3] = 1; w[8] = 1; w[4] = 1;
            w[5] = 1; w[6] = 1; w[7] = 1;


            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i);
                        y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }


                    q1 = 0; q2 = 0; q3 = 0;
                    for (b = 0; b <= 8; b++)
                    {
                        q1 = q1 + rnkkom[b].R * w[b] * resim[b];
                        q2 = q2 + rnkkom[b].G * w[b] * resim[b];
                        q3 = q3 + rnkkom[b].B * w[b] * resim[b];
                    }
                    q1 = q1 / 9; q2 = q2 / 9; q3 = q3 / 9;

                    p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }




        public Bitmap MeanFilter(Bitmap bmp1, int MaskSize)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, masketoplam;
            int resim, x, y, i, j, m; Color p9;
            int ip, jp;
            double[,] w;
            w = new double[MaskSize, MaskSize];

            q1 = (double)MaskSize / 2;
            q1 = q1 - 0.5;
            m = (int)(q1);


            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                { w[x + m, y + m] = 1.0; }
            }


            masketoplam = 0;
            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                { masketoplam = masketoplam + w[x + m, y + m]; }
            }


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    q1 = 0; q2 = 0; q3 = 0;
                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(ip, jp);
                                q1 = q1 + p9.R * w[x + m, y + m];
                                q2 = q2 + p9.G * w[x + m, y + m];
                                q3 = q3 + p9.B * w[x + m, y + m];
                            }
                        }
                    }

                    q1 = q1 / masketoplam;
                    q2 = q2 / masketoplam;
                    q3 = q3 / masketoplam;

                    p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    bmp2.SetPixel(i, j, p9);


                }
            }
            return bmp2;
        }

        public Bitmap LBP(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int res, x, y, i, j, b, z; Color p9;
            int[] q1, q2, q3;
            q1 = new int[10]; q2 = new int[10]; q3 = new int[10];
            p9 = Color.FromArgb(0, 0, 0); c2 = Color.FromArgb(0, 0, 0);
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        {
                            p9 = bmp1.GetPixel(x, y); q1[b] = p9.R;
                        }
                        else { q1[b] = 0; }
                    }

                    for (b = 0; b < 8; b++)
                    {
                        if (q1[b] > q1[8])
                        { q2[b] = 1; }
                        else { q2[b] = 0; }
                    }
                   
                    z = (int)(128 * q2[3]) + (int)(64 * q2[5]) + (int)(32 * q2[6]) + (int)(16 * q2[7]) + (int)(8 * q2[4]) + (int)(4 * q2[2]) + (int)(2 * q2[1]) + (int)(1 * q2[0]);
                    

                    p9 = Color.FromArgb(z, z, z);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }



        public Bitmap LBP2(Bitmap bmp1, int[] w)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int res, x, y, i, j, b, z; Color p9;
            int[] q1, q2, q3;
            q1 = new int[10]; q2 = new int[10]; q3 = new int[10];
            p9 = Color.FromArgb(0, 0, 0);
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        { p9 = bmp1.GetPixel(x, y); q1[b] = p9.R; }
                        else { q1[b] = 0; }
                    }

                    for (b = 0; b < 8; b++)
                    {
                        if (q1[b] >= q1[8])
                        { q2[b] = 1; }
                        else { q2[b] = 0; }
                    }
                    z = 0;
                    for (b = 0; b < 8; b++)
                    { z = z + q2[b] * w[b]; }
                    p9 = Color.FromArgb(z, z, z);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }


        public void LBPMaske(int[] m, int s)
        {
            int i;
            int[] w = new int[8];

            if (s == 0)
            {
                w[0] = 32; w[1] = 64; w[2] = 128;
                w[3] = 16; w[4] = 1;
                w[5] = 8; w[6] = 4; w[7] = 2;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }
            else if (s == 1)
            {
                w[0] = 64; w[1] = 128; w[2] = 1;
                w[3] = 32; w[4] = 2;
                w[5] = 16; w[6] = 8; w[7] = 4;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }
            else if (s == 2)
            {
                w[0] = 128; w[1] = 1; w[2] = 2;
                w[3] = 64; w[4] = 4;
                w[5] = 32; w[6] = 16; w[7] = 8;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }

            else if (s == 3)
            {
                w[0] = 1; w[1] = 2; w[2] = 4;
                w[3] = 128; w[4] = 8;
                w[5] = 64; w[6] = 32; w[7] = 16;
                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }


            else if (s == 4)
            {
                w[0] = 2; w[1] = 4; w[2] = 8;
                w[3] = 1; w[4] = 16;
                w[5] = 128; w[6] = 64; w[7] = 32;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }
            else if (s == 5)
            {
                w[0] = 4; w[1] = 8; w[2] = 16;
                w[3] = 2; w[4] = 32;
                w[5] = 1; w[6] = 128; w[7] = 64;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }
            else if (s == 6)
            {
                w[0] = 8; w[1] = 16; w[2] = 32;
                w[3] = 4; w[4] = 64;
                w[5] = 2; w[6] = 1; w[7] = 128;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }

            else if (s == 7)
            {
                w[0] = 16; w[1] = 32; w[2] = 64;
                w[3] = 8; w[4] = 128;
                w[5] = 4; w[6] = 2; w[7] = 1;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }
            else
            {
                w[0] = 0; w[1] = 0; w[2] = 0;
                w[3] = 0; w[4] = 0;
                w[5] = 0; w[6] = 0; w[7] = 0;

                for (i = 0; i < 8; i++)
                { m[i] = w[i]; }
            }


        }

        public Bitmap LBP3(Bitmap bmp1, int[] w)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int res, x, y, i, j, b, z1,z2,z3; Color p9;
            int[] q1, q2, q3;
            q1 = new int[10]; q2 = new int[10]; q3 = new int[10];
            p9 = Color.FromArgb(0, 0, 0);
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        { p9 = bmp1.GetPixel(x, y); q1[b] = p9.R; }
                        else { q1[b] = 0; }
                    }

                    for (b = 0; b < 8; b++)
                    {
                        if (q1[b] > q1[8])
                        { q2[b] = 1; }
                        else { q2[b] = 0; }
                    }
                    z1 = 0;
                    for (b = 0; b < 8; b++)
                    { z1 = z1 + q2[b] * w[b]; }

                  
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        { p9 = bmp1.GetPixel(x, y); q1[b] = p9.G; }
                        else { q1[b] = 0; }
                    }

                    for (b = 0; b < 8; b++)
                    {
                        if (q1[b] > q1[8])
                        { q2[b] = 1; }
                        else { q2[b] = 0; }
                    }
                    z2 = 0;
                    for (b = 0; b < 8; b++)
                    { z2 = z2 + q2[b] * w[b]; }



                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        { p9 = bmp1.GetPixel(x, y); q1[b] = p9.B; }
                        else { q1[b] = 0; }
                    }

                    for (b = 0; b < 8; b++)
                    {
                        if (q1[b] > q1[8])
                        { q2[b] = 1; }
                        else { q2[b] = 0; }
                    }
                    z3 = 0;
                    for (b = 0; b < 8; b++)
                    { z3 = z3 + q2[b] * w[b]; }





                    p9 = Color.FromArgb(z1, z2, z3);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }



        public Bitmap LBPC1(Bitmap bmp1, int[] w)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom = new Color[9]; Color p9;
            int[] resim = new int[9];
            int[] q1, q2, q3; q1 = new int[10]; q2 = new int[10]; q3 = new int[10];
            int z1, z2, z3;
            int x, y, i, j, b;


            p9 = Color.FromArgb(0, 0, 0);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }



                    for (b = 0; b < 8; b++)
                    {
                        if ((resim[b] == 1) && (rnkkom[b].R < rnkkom[8].R))
                        { q1[b] = 1; }
                        else { q1[b] = 0; }

                        if ((resim[b] == 1) && (rnkkom[b].G < rnkkom[8].G))
                        { q2[b] = 1; }
                        else { q2[b] = 0; }

                        if ((resim[b] == 1) && (rnkkom[b].B < rnkkom[8].B))
                        { q3[b] = 1; }
                        else { q3[b] = 0; }

                    }


                    z1 = 0; z2 = 0; z3 = 0;
                    for (b = 0; b < 8; b++)
                    {
                        z1 = z1 + q1[b] * w[b];
                        z2 = z2 + q2[b] * w[b];
                        z3 = z3 + q3[b] * w[b];
                    }

                    p9 = Color.FromArgb(z1, z2, z2);
                    bmp2.SetPixel(i, j, p9);

                }
            }
            return bmp2;
        }

        public Bitmap LBPC1t(Bitmap bmp1, int[] w, int com)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom = new Color[9]; Color p9;
            int[] resim = new int[9];
            int[] q1, q2, q3; q1 = new int[10]; q2 = new int[10]; q3 = new int[10];
            int z1, z2, z3;
            int x, y, i, j, b;


            p9 = Color.FromArgb(0, 0, 0);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }



                    for (b = 0; b < 8; b++)
                    {
                        if ((resim[b] == 1) && (rnkkom[b].R < rnkkom[8].R))
                        { q1[b] = 1; }
                        else { q1[b] = 0; }

                        if ((resim[b] == 1) && (rnkkom[b].G < rnkkom[8].G))
                        { q2[b] = 1; }
                        else { q2[b] = 0; }

                        if ((resim[b] == 1) && (rnkkom[b].B < rnkkom[8].B))
                        { q3[b] = 1; }
                        else { q3[b] = 0; }

                    }


                    z1 = 0; z2 = 0; z3 = 0;


                    if (com == 1)
                    {
                        for (b = 0; b < 8; b++)
                        {
                            z1 = z1 + q1[b] * w[b];
                            z2 = z2 + q2[b] * w[b];
                            z3 = z3 + q3[b] * w[b];
                        }
                    }

                    else if (com == 0)
                    {
                        for (b = 0; b < 8; b++)
                        {
                            z1 = z1 + q1[b] * w[b];
                            z2 = z2 + q2[b] * w[b];
                            z3 = z3 + q3[b] * w[b];
                        }
                    }

                    else
                    {
                        z1 = 0; z2 = 0; z3 = 0;
                    }


                    p9 = Color.FromArgb(z1, z2, z2);
                    bmp2.SetPixel(i, j, p9);

                }
            }
            return bmp2;
        }


        public Bitmap LBPC2(Bitmap bmp1, int start)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom = new Color[9]; Color p9;
            int[] resim = new int[9];
            int[] q1, q2, q3; q1 = new int[10]; q2 = new int[10]; q3 = new int[10];
            int z1, z2, z3;
            int x, y, i, j, b;


            int[] w = new int[8];
            LBPMaske(w, start);


            p9 = Color.FromArgb(0, 0, 0);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {



                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }



                    for (b = 0; b < 8; b++)
                    {
                        if ((resim[b] == 1) && (rnkkom[b].R < rnkkom[8].R))
                        { q1[b] = 1; }
                        else { q1[b] = 0; }

                        if ((resim[b] == 1) && (rnkkom[b].G < rnkkom[8].G))
                        { q2[b] = 1; }
                        else { q2[b] = 0; }

                        if ((resim[b] == 1) && (rnkkom[b].B < rnkkom[8].B))
                        { q3[b] = 1; }
                        else { q3[b] = 0; }

                    }


                    z1 = 0; z2 = 0; z3 = 0;
                    for (b = 0; b < 8; b++)
                    {
                        z1 = z1 + q1[b] * w[b];
                        z2 = z2 + q2[b] * w[b];
                        z3 = z3 + q3[b] * w[b];
                    }

                    p9 = Color.FromArgb(z1, z2, z2);
                    bmp2.SetPixel(i, j, p9);

                }
            }
            return bmp2;
        }

      



        public void sirayadiz(int[] x, int n)
        {
            int iMin, iPos;
            for (iPos = 0; iPos < n; iPos++)
            {
                int temp;
                iMin = iPos;
                /* test against all other elements */
                for (int i = iPos + 1; i < n; i++)
                {
                    /* if this element is less, then it is the new minimum */
                    if (x[i] < x[iMin])
                    {
                        /* found new minimum; remember its index */
                        iMin = i;
                    }
                }

                /* iMin is the index of the minimum element. Swap it with the current position */
                if (iMin != iPos)
                {
                    //swap(a, iPos, iMin);
                    temp = x[iMin];
                    x[iMin] = x[iPos];
                    x[iPos] = temp;
                }
            }
            // int j,k,sorted;
            //  int temp;
            // k=0;

            //do {
            //    k=k+1;
            //    sorted=1;
            //    for(j=1;j<=n-k;j++)
            //   if( x[j-1]>x[j])
            //     {
            //        temp=x[j-1];
            //        x[j-1]=x[j];
            //        x[j]=temp;
            //        sorted=0;
            //     }
            // }while (sorted !=1);

        }

        public Bitmap MedyanFilter(Bitmap bmp1, int MaskSize)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int[] q1, q2, q3;
            int resim, x, y, i, j, m, h; Color p9;
            int ip, jp, r, g, b;
            double z;
            q1 = new int[MaskSize * MaskSize + 3];
            q2 = new int[MaskSize * MaskSize + 3];
            q3 = new int[MaskSize * MaskSize + 3];

            z = (double)MaskSize / 2;
            z = z - 0.5;
            m = (int)(z);

            for (h = 0; h < (MaskSize * MaskSize); h++)
            { q1[h] = 0; q2[h] = 0; q3[h] = 0; }


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    h = 0;

                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                h++;
                                p9 = bmp1.GetPixel(ip, jp);
                                q1[h] = p9.R;
                                q2[h] = p9.G;
                                q3[h] = p9.B;
                            }
                            else
                            {

                                h++;
                                q1[h] = 0;
                                q2[h] = 0;
                                q3[h] = 0;
                            }
                        }
                    }


                    // Array.Sort(q1); Array.Sort(q2); Array.Sort(q3);

                    sirayadiz(q1, MaskSize * MaskSize);
                    sirayadiz(q2, MaskSize * MaskSize);
                    sirayadiz(q3, MaskSize * MaskSize);
                    int t = (int)((MaskSize * MaskSize + 1) / 2);
                    r = q1[t];
                    g = q2[t];
                    b = q3[t];


                    p9 = Color.FromArgb(r, g, b);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }







        public Bitmap FilterDiffusion(Bitmap bmp1, int Tr, int Tg, int Tb, int ftipi, int t)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim, d; double[] cd;
            resim = new int[9]; d = new int[9]; cd = new double[9];
            int q1, q2, q3, i, j, b, n, x, y;
            double toplam, V;

            for (n = 0; n < t; n++)
            {

                for (i = 0; i < bmp1.Width; i++)
                {
                    for (j = 0; j < bmp1.Height; j++)
                    {


                        for (b = 0; b <= 8; b++)
                        {
                            x = ikomsu(b, i);
                            y = jkomsu(b, j);
                            resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                            if (resim[b] == 1)
                            { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                        }



                        for (b = 0; b < 8; b++)
                        {
                            d[b] = rnkkom[b].R * resim[b] - rnkkom[8].R * resim[8];
                            V = (double)(d[b] * d[b]) / (Tr * Tr);
                            if (ftipi == 1)
                                cd[b] = 1 / (1 + V);
                            else if (ftipi == 2)
                                cd[b] = Math.Exp(-V);
                            else if (ftipi == 3)
                                cd[b] = 1 / Math.Sqrt(1 + V);
                            else if (ftipi == 4)
                            { if (V <= 0) cd[b] = 1; else { cd[b] = 1 - Math.Exp(-3.315 / V); } }
                            else if (ftipi == 5)
                            { V = Math.Abs(d[b]); cd[b] = 1 / (1 + Math.Exp((V - Tr) / 10)); } // (Tr / 3)));
                            else if (ftipi == 6)
                            { V = Math.Abs(d[b]); if (V >= Tr) cd[b] = 0; else { V = (V * V) / (Tr * Tr); cd[b] = 0.5 * (1 - V) * (1 - V); } }  // black
                        }

                        toplam = 0;
                        for (b = 0; b < 8; b++)
                        {
                            if (b == 1 || b == 3 || b == 4 || b == 6)
                                toplam = toplam + cd[b] * d[b];
                        }
                        toplam = toplam / 4;
                        q1 = (int)toplam;
                        q1 = rnkkom[8].R + q1;
                        if (q1 > 255) q1 = 255;



                        for (b = 0; b < 8; b++)
                        {
                            d[b] = rnkkom[b].G * resim[b] - rnkkom[8].G * resim[8];
                            V = (double)(d[b] * d[b]) / (Tg * Tg);
                            if (ftipi == 1)
                                cd[b] = 1 / (1 + V);
                            else if (ftipi == 2)
                                cd[b] = Math.Exp(-V);
                            else if (ftipi == 3)
                                cd[b] = 1 / Math.Sqrt(1 + V);
                            else if (ftipi == 4)
                            { if (V <= 0) cd[b] = 1; else { cd[b] = 1 - Math.Exp(-3.315 / V); } }
                            else if (ftipi == 5)
                            { V = Math.Abs(d[b]); cd[b] = 1 / (1 + Math.Exp((V - Tg) / (10))); }  //(Tg / 3)
                            else if (ftipi == 6)
                            { V = Math.Abs(d[b]); if (V >= Tg) cd[b] = 0; else { V = (V * V) / (Tg * Tg); cd[b] = 0.5 * (1 - V) * (1 - V); } }  // black
                        }


                        toplam = 0;
                        for (b = 0; b < 8; b++)
                        {
                            if (b == 1 || b == 3 || b == 4 || b == 6)
                                toplam = toplam + cd[b] * d[b];
                        }
                        toplam = toplam / 4;
                        q2 = (int)toplam;
                        q2 = rnkkom[8].G + q2;
                        if (q2 > 255) q2 = 255;


                        for (b = 0; b < 8; b++)
                        {
                            d[b] = rnkkom[b].B * resim[b] - rnkkom[8].B * resim[8];
                            V = (double)(d[b] * d[b]) / (Tb * Tb);
                            if (ftipi == 1)
                                cd[b] = 1 / (1 + V);
                            else if (ftipi == 2)
                                cd[b] = Math.Exp(-V);
                            else if (ftipi == 3)
                                cd[b] = 1 / Math.Sqrt(1 + V);
                            else if (ftipi == 4)
                            { if (V <= 0) cd[b] = 1; else { cd[b] = 1 - Math.Exp(-3.315 / V); } }
                            else if (ftipi == 5)
                            { V = Math.Abs(d[b]); cd[b] = 1 / (1 + Math.Exp((V - Tb) / (10))); }  //(Tb / 3)
                            else if (ftipi == 6)
                            { V = Math.Abs(d[b]); if (V >= Tb) cd[b] = 0; else { V = (V * V) / (Tb * Tb); cd[b] = 0.5 * (1 - V) * (1 - V); } }  // black
                        }



                        toplam = 0;
                        for (b = 0; b < 8; b++)
                        {
                            if (b == 1 || b == 3 || b == 4 || b == 6)
                                toplam = toplam + cd[b] * d[b];
                        }
                        toplam = toplam / 4;
                        q3 = (int)toplam;
                        q3 = rnkkom[8].B + q3;
                        if (q3 > 255) q3 = 255;

                        p9 = Color.FromArgb(q1, q2, q3);
                        bmp2.SetPixel(i, j, p9);
                    }
                }
                bmp1 = (Bitmap)bmp2.Clone();

            }
            return bmp2;

        }



        public Bitmap Pooling(Bitmap bmp1, int MaskSize, int tip)
        {
            int m, n, r1, r2;
            r1 = bmp1.Width % MaskSize; m = (bmp1.Width - r1) / MaskSize;
            r2 = bmp1.Height % MaskSize; n = (bmp1.Height - r2) / MaskSize;
            Bitmap bmp2 = new Bitmap(m, n);
            double q1, q2, q3;
            int resim, x, y, i, j; Color p9;
            int ip, jp;
            int rmax, gmax, bmax;
            int rmin, gmin, bmin;

            for (j = 0; j < (bmp1.Height - r2); j = j + MaskSize)
            {
                for (i = 0; i < (bmp1.Width - r1); i = i + MaskSize)
                {
                    q1 = 0; q2 = 0; q3 = 0;
                    rmax = 0; gmax = 0; bmax = 0;
                    rmin = 255; gmin = 255; bmin = 255;
                    for (y = 0; y < MaskSize; y++)
                    {
                        for (x = 0; x < MaskSize; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(ip, jp);
                                q1 = q1 + p9.R;
                                q2 = q2 + p9.G;
                                q3 = q3 + p9.B;
                                if (p9.R > rmax) rmax = p9.R;
                                if (p9.G > gmax) gmax = p9.G;
                                if (p9.B > bmax) bmax = p9.B;
                                if (p9.R < rmin) rmin = p9.R;
                                if (p9.G < gmin) gmin = p9.G;
                                if (p9.B < bmin) bmin = p9.B;
                            }
                        }
                    }
                    if (tip == 1)
                    {
                        q1 = q1 / (MaskSize * MaskSize);
                        q2 = q2 / (MaskSize * MaskSize);
                        q3 = q3 / (MaskSize * MaskSize);
                        p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    }
                    else if (tip == 2)
                    {
                        q1 = rmax;
                        q2 = gmax;
                        q3 = bmax;
                        p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    }
                    else if (tip == 3)
                    {
                        q1 = rmin;
                        q2 = gmin;
                        q3 = bmin;
                        p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    }
                    else
                    {
                        q1 = 0.0;
                        q2 = 0.0;
                        q3 = 0.0;
                        p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    }
                    bmp2.SetPixel(i / (MaskSize), j / (MaskSize), p9);
                }
            }
            return bmp2;
        }



        public Bitmap GaussFilter(Bitmap bmp1, int MaskSize, double zigma)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, masketoplam;
            int resim, x, y, i, j, m; Color p9;
            int ip, jp;
            double[,] w;
            w = new double[MaskSize, MaskSize];

            q1 = (double)MaskSize / 2;
            q1 = q1 - 0.5;
            m = (int)(q1);

            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                {
                    q1 = (double)(x * x); q2 = (double)(y * y);
                    q3 = (double)Math.Exp(-(q1 + q2) / (2 * zigma * zigma));
                    q3 = q3 / (2 * 3.14 * zigma * zigma);
                    w[x + m, y + m] = 64 * q3;

                }
            }


            masketoplam = 0;
            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                { masketoplam = masketoplam + w[x + m, y + m]; }
            }


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    q1 = 0; q2 = 0; q3 = 0;
                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(ip, jp);
                                q1 = q1 + p9.R * w[x + m, y + m];
                                q2 = q2 + p9.G * w[x + m, y + m];
                                q3 = q3 + p9.B * w[x + m, y + m];
                            }
                        }
                    }

                    q1 = q1 / masketoplam;
                    q2 = q2 / masketoplam;
                    q3 = q3 / masketoplam;

                    p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    bmp2.SetPixel(i, j, p9);


                }
            }

            return bmp2;
        }


        public Bitmap LogFilter(Bitmap bmp1, int MaskSize, double zigma)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, masketoplam,negatifmasketoplam,d;
            int resim, x, y, i, j, ip, jp, m; Color p9;
            double[,] w;
            w = new double[MaskSize, MaskSize];

            q1 = (double)MaskSize / 2;
            q1 = q1 - 0.5;
            m = (int)(q1);

            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                {
                    q1 = (double)(x * x); q2 = (double)(y * y);
                    q3 = (double)Math.Exp(-(q1 + q2) / (2 * zigma * zigma));
                    
                    q3 =(1- (q1 + q2)/ (2 * zigma * zigma));
                    q3 =-1.0*q3 / (3.14 * zigma * zigma* zigma * zigma);
                    w[x + m, y + m] =256*q3;

                }
            }


            masketoplam = 0; negatifmasketoplam = 0;
            for (y = -m; y <= m; y++)
            {    for (x = -m; x <= m; x++)
                {
                    if (w[x + m, y + m] > 0)
                    { masketoplam = masketoplam + w[x + m, y + m]; }

                    if (w[x + m, y + m] < 0)
                    { negatifmasketoplam = negatifmasketoplam + w[x + m, y + m]; }
                }
            }




            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    q1 = 0; q2 = 0; q3 = 0;
                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(ip, jp);
                                q1 = q1 + p9.R * w[x + m, y + m];
                                q2 = q2 + p9.G * w[x + m, y + m];
                                q3 = q3 + p9.B * w[x + m, y + m];
                            }
                        }
                    }
                    /*
                    q1 = q1 / masketoplam;
                    q2 = q2 / masketoplam;
                    q3 = q3 / masketoplam;
                    */
                    //    q1 = q1 / d;
                    //   if (q1 < 0) q1 =-q1;

                    //  q1 = 255 + (q1 - 255 * masketoplam) / d;


                     d=masketoplam-negatifmasketoplam;                  
              
                     q1 = 255 + (q1 - 255 * masketoplam) / d;
                     q2 = 255 + (q2 - 255 * masketoplam) / d;
                     q3 = 255 + (q3 - 255 * masketoplam) / d;

                    p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    bmp2.SetPixel(i, j, p9);


                }
            }

            return bmp2;
        }

        public Bitmap GaussFunction(Bitmap bmp1, double zigmax, double zigmay, double teta)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, q4;
            double xr, yr, sx, sy, u, v;
            int x, y; Color p9;
            sx = 3.0 * zigmax; sy = 3.0 * zigmay;  //  sx = 3.0 ; sy = 3.0;  
            for (y = -bmp1.Height / 2; y < bmp1.Height / 2; y++)
            {
                for (x = -bmp1.Width / 2; x < bmp1.Width / 2; x++)
                {

                    xr = (x * sx) / (double)(bmp1.Width / 2); yr = (y * sy) / (double)(bmp1.Height / 2);
                    u = xr * Math.Cos(teta * Math.PI / 180) + yr * Math.Sin(teta * Math.PI / 180);
                    v = -1.0 * xr * Math.Sin(teta * Math.PI / 180) + yr * Math.Cos(teta * Math.PI / 180);
                    q1 = (double)(u * u); q2 = (double)(v * v);
                    q4 = q1 / (2 * zigmax * zigmax) + q2 / (2 * zigmay * zigmay);
                    q3 = (double)Math.Exp(-(q4));
                    q3 = (255 * q3) / (2 * 3.14 * zigmax * zigmay);
                    p9 = Color.FromArgb((int)q3, (int)q3, (int)q3);
                    bmp2.SetPixel(x + bmp1.Width / 2, y + bmp1.Height / 2, p9);
                }
            }
            return bmp2;
        }

        public Bitmap GaborFunction(Bitmap bmp1, double zigmax, double zigmay, double teta, double fx, double fy)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, q4, q5;
            double xr, yr, sx, sy, u, v;
            int x, y; Color p9;
            //http://kgeorge.github.io/2016/02/04/understanding-gabor-filter
            //  sx = 3.0 ; sy = 3.0; 
            sx = 5.0 * zigmax; sy = 5.0 * zigmay;
            for (y = -bmp1.Height / 2; y < bmp1.Height / 2; y++)
            {
                for (x = -bmp1.Width / 2; x < bmp1.Width / 2; x++)
                {
                    xr = (x * sx) / (double)(bmp1.Width / 2); yr = (y * sy) / (double)(bmp1.Height / 2);
                    //  q1 = (double)(xr * xr); q2 = (double)(yr * yr);
                    u = xr * Math.Cos(teta * Math.PI / 180) + yr * Math.Sin(teta * Math.PI / 180);
                    v = -1.0 * xr * Math.Sin(teta * Math.PI / 180) + yr * Math.Cos(teta * Math.PI / 180);
                    q1 = (double)(u * u); q2 = (double)(v * v);
                    q4 = q1 / (2 * zigmax * zigmax) + q2 / (2 * zigmay * zigmay);
                    q3 = (double)Math.Exp(-q4);
                    q5 = Math.Abs(Math.Cos(2 * 3.14 * (fx * xr + fy * yr) + 0 * Math.PI / 180));  // fx=1.5 ve fy=1.5 makul
                    q3 = q5 * (255 * q3) / (2 * 3.14 * zigmax * zigmay);

                    p9 = Color.FromArgb((int)q3, (int)q3, (int)q3);
                    bmp2.SetPixel(x + bmp1.Width / 2, y + bmp1.Height / 2, p9);
                }
            }
            return bmp2;
        }



        public Bitmap GaborFilter(Bitmap bmp1, int MaskSize, double zigmax, double zigmay, double teta, double fx, double fy)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, q4, q5, u, v; int x, y, m;
            double[,] w; w = new double[MaskSize, MaskSize];
            q1 = (double)MaskSize / 2; q1 = q1 - 0.5; m = (int)(q1);
            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                {
                    u = x * Math.Cos(teta * Math.PI / 180) + y * Math.Sin(teta * Math.PI / 180);
                    v = -1.0 * x * Math.Sin(teta * Math.PI / 180) + y * Math.Cos(teta * Math.PI / 180);
                    q1 = (double)(u * u); q2 = (double)(v * v);
                    q4 = q1 / (2 * zigmax * zigmax) + q2 / (2 * zigmay * zigmay);
                    q3 = (double)Math.Exp(-q4);
                    q5 = q3 * Math.Cos(2 * 3.14 * (fx * u + fy * v));
                    q5 = q5 / (2 * 3.14 * zigmax * zigmay);
                    w[x + m, y + m] = 64 * q5;
                }
            }

            bmp2 = Convulation(bmp1, w, 1);
            return bmp2;
        }
        public Bitmap GaborFilterBank(Bitmap bmp1, int MaskSize, double[] zigmaxp, double[] zigmayp, double[] tetap, double[] fxp, double[] fyp, int tip)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone(); Color p9;
            double zigmax, zigmay, teta, fx, fy;
            double q1, q2, q3, q4, q5, u, v, s;
            int resim, x, y, i, j, k, ip, jp, m, msayi; msayi = zigmaxp.Length;

            double[, ,] w; w = new double[msayi, MaskSize, MaskSize];
            double[] masketoplam; masketoplam = new double[msayi];
            double[] nmaske; nmaske = new double[msayi];
            double[] d; d = new double[msayi];

            double[] outputr; outputr = new double[msayi];
            double[] outputg; outputg = new double[msayi];
            double[] outputb; outputb = new double[msayi];

            q1 = (double)MaskSize / 2; q1 = q1 - 0.5; m = (int)(q1);
            for (k = 0; k < msayi; k++)
            {
                zigmax = zigmaxp[k];
                zigmay = zigmayp[k];
                teta = tetap[k];
                fx = fxp[k];
                fy = fyp[k];
                for (y = -m; y <= m; y++)
                {
                    for (x = -m; x <= m; x++)
                    {
                        u = x * Math.Cos(teta * Math.PI / 180) + y * Math.Sin(teta * Math.PI / 180);
                        v = -1.0 * x * Math.Sin(teta * Math.PI / 180) + y * Math.Cos(teta * Math.PI / 180);
                        q1 = (double)(u * u); q2 = (double)(v * v);
                        q4 = q1 / (2 * zigmax * zigmax) + q2 / (2 * zigmay * zigmay);
                        q3 = (double)Math.Exp(-q4);
                        q5 = q3 * Math.Cos(2 * 3.14 * (fx * u + fy * v));
                        q5 = q5 / (2 * 3.14 * zigmax * zigmay);
                        w[k, x + m, y + m] = 64 * q5;
                    }
                }
            }

            for (k = 0; k < msayi; k++)
            {
                masketoplam[k] = 0.0; nmaske[k] = 0.0; d[k] = 0.0;
                for (y = -m; y <= m; y++)
                {
                    for (x = -m; x <= m; x++)
                    {
                        if (w[k, x + m, y + m] > 0)
                            masketoplam[k] = masketoplam[k] + w[k, x + m, y + m];
                        if (w[k, x + m, y + m] < 0)
                            nmaske[k] = nmaske[k] + w[k, x + m, y + m];
                    }
                }
            }

            for (k = 0; k < msayi; k++)
            { d[k] = Math.Abs(masketoplam[k] - nmaske[k]); }

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    for (k = 0; k < msayi; k++)
                    {
                        outputr[k] = 0.0; outputg[k] = 0.0; outputb[k] = 0.0;
                        for (y = -m; y <= m; y++)
                        {
                            for (x = -m; x <= m; x++)
                            {
                                jp = j + y; ip = i + x;
                                resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                                if (resim == 1)
                                {
                                    p9 = bmp1.GetPixel(ip, jp);
                                    outputr[k] = outputr[k] + p9.R * w[k, x + m, y + m];
                                    outputg[k] = outputg[k] + p9.G * w[k, x + m, y + m];
                                    outputb[k] = outputb[k] + p9.B * w[k, x + m, y + m];
                                }
                            }
                        }

                        if (tip == 1 && d[k] != 0.0)
                        {
                            if (masketoplam[k] > 0 && nmaske[k] == 0)
                            {
                                outputr[k] = outputr[k] / d[k];
                                outputg[k] = outputg[k] / d[k];
                                outputb[k] = outputb[k] / d[k];
                            }
                            else if (masketoplam[k] > 0 && nmaske[k] < 0)
                            {
                                outputr[k] = 255 + (outputr[k] - 255 * masketoplam[k]) / d[k];
                                outputg[k] = 255 + (outputg[k] - 255 * masketoplam[k]) / d[k];
                                outputb[k] = 255 + (outputb[k] - 255 * masketoplam[k]) / d[k];
                            }
                            else if (masketoplam[k] == 0 && nmaske[k] < 0)
                            {
                                outputr[k] = 255 + (outputr[k] - 255 * masketoplam[k]) / d[k];
                                outputg[k] = 255 + (outputg[k] - 255 * masketoplam[k]) / d[k];
                                outputb[k] = 255 + (outputb[k] - 255 * masketoplam[k]) / d[k];
                            }
                            else
                            {
                                outputr[k] = 0.0;
                                outputg[k] = 0.0;
                                outputb[k] = 0.0;
                            }
                        }
                        else if (tip == 2 && d[k] != 0.0)
                        {
                            outputr[k] = Math.Abs(outputr[k] / d[k]);
                            outputg[k] = Math.Abs(outputg[k] / d[k]);
                            outputb[k] = Math.Abs(outputb[k] / d[k]);
                        }
                        else
                        {
                            outputr[k] = 0.0;
                            outputg[k] = 0.0;
                            outputb[k] = 0.0;
                        }
                    }



                    q1 = 0; q2 = 0; q3 = 0;
                    for (k = 0; k < msayi; k++)
                    {
                        if (outputr[k] > q1) q1 = outputr[k];
                        if (outputg[k] > q2) q2 = outputg[k];
                        if (outputb[k] > q3) q3 = outputb[k];
                    }

                    p9 = Color.FromArgb((int)q1, (int)q1, (int)q1);
                    bmp2.SetPixel(i, j, p9);
                }
            }

            return bmp2;
        }




      


        public Bitmap Convulation(Bitmap bmp1, double[,] Maske, int tip)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, masketoplam, negatifmasketoplam, d;
            int resim, x, y, i, j, ip, jp, m; Color p9;
            int MaskSize; MaskSize = Maske.GetLength(0) + 1;
            q1 = (double)MaskSize / 2; q1 = q1 - 0.5; m = (int)(q1);
            masketoplam = 0; negatifmasketoplam = 0;
            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                {
                    if (Maske[x + m, y + m] > 0)
                    { masketoplam = masketoplam + Maske[x + m, y + m]; }
                    if (Maske[x + m, y + m] < 0)
                    { negatifmasketoplam = negatifmasketoplam + Maske[x + m, y + m]; }
                }
            }
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    q1 = 0; q2 = 0; q3 = 0;
                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(ip, jp);
                                q1 = q1 + p9.R * Maske[x + m, y + m];
                                q2 = q2 + p9.G * Maske[x + m, y + m];
                                q3 = q3 + p9.B * Maske[x + m, y + m];
                            }
                        }
                    }
                    d = masketoplam - negatifmasketoplam;
                    if (tip == 1 && d != 0.0)
                    {
                        if (masketoplam > 0 && negatifmasketoplam == 0)
                        {
                            q1 = q1 / d;
                            q2 = q2 / d;
                            q3 = q3 / d;
                        }
                        else if (masketoplam > 0 && negatifmasketoplam < 0)
                        {
                            q1 = 255 + (q1 - 255 * masketoplam) / d;
                            q2 = 255 + (q2 - 255 * masketoplam) / d;
                            q3 = 255 + (q3 - 255 * masketoplam) / d;
                        }
                        else if (masketoplam == 0 && negatifmasketoplam < 0)
                        {
                            q1 = 255 + (q1 - 255 * masketoplam) / d;
                            q2 = 255 + (q2 - 255 * masketoplam) / d;
                            q3 = 255 + (q3 - 255 * masketoplam) / d;
                        }
                        else
                        {
                            q1 = 0.0;
                            q2 = 0.0;
                            q3 = 0.0;
                        }
                    }
                    else if (tip == 2 && d != 0.0)
                    {
                        q1 = Math.Abs(q1 / d);
                        q2 = Math.Abs(q2 / d);
                        q3 = Math.Abs(q3 / d);
                    }
                    else
                    {
                        q1 = 0;
                        q2 = 0;
                        q3 = 0;
                    }

                    p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }






        public Bitmap BilateralFilter(Bitmap bmp1, int MaskSize, double D, double dx)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, mem, memxy, dr;
            double mr, mg, mb;
            int resim, x, y, i, j, m; Color p9, c1;
            int ip, jp;
            double[,] w;
            w = new double[MaskSize, MaskSize];

            q1 = (double)MaskSize / 2;
            q1 = q1 - 0.5;
            m = (int)(q1);
            //D = 254;
            /*
            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                { w[x + m, y + m] = 1.0; }
            }


            masketoplam = 0;
            for (y = -m; y <= m; y++)
            {
                for (x = -m; x <= m; x++)
                { masketoplam = masketoplam + w[x + m, y + m]; }
            }
            */

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    q1 = 0; q2 = 0; q3 = 0; mr = 0; mg = 0; mb = 0;

                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(i, j); c1 = bmp1.GetPixel(ip, jp);
                                dr = Math.Sqrt(Math.Pow(ip - i, 2) + Math.Pow(jp - j, 2)); memxy = Math.Exp(-0.5 * (dr / dx) * (dr / dx)); //dx=4;

                                dr = Math.Abs(c1.R - p9.R); mem = Math.Exp(-0.5 * (dr / D) * (dr / D));
                                q1 = q1 + c1.R * mem * memxy; mr = mr + mem * memxy;

                                dr = Math.Abs(c1.G - p9.G); mem = Math.Exp(-0.5 * (dr / D) * (dr / D));
                                q2 = q2 + c1.G * mem * memxy; mg = mg + mem * memxy;

                                dr = Math.Abs(c1.B - p9.B); mem = Math.Exp(-0.5 * (dr / D) * (dr / D));
                                q3 = q3 + c1.B * mem * memxy; mb = mb + mem * memxy;


                            }
                        }
                    }

                    q1 = q1 / mr;
                    q2 = q2 / mg;
                    q3 = q3 / mb;

                    p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    bmp2.SetPixel(i, j, p9);


                }
            }
            return bmp2;
        }









        public double MemberShip3(Color p1, Color p2, int a, int ftipi)
        {
            int dr, dg, db;
            double dR, mem;
            dr = p1.R - p2.R; if (dr < 0) dr = -dr;
            dg = p1.G - p2.G; if (dg < 0) dg = -dg;
            db = p1.B - p2.B; if (db < 0) db = -db;
            dR = Math.Sqrt(dr * dr + dg * dg + db * db); dR = dR / (1.73);
            if (ftipi == 1)
            { if (dR > a) mem = 0; else mem = 1 - ((double)dR / a); }
            else if (ftipi == 2)
                mem = Math.Exp(-dR / a);
            else if (ftipi == 3)
                mem = Math.Exp(-dR * dR / (a * a));
            else mem = 0;
            return mem;
        }

        public double Distance3(Color p1, Color p2)
        {
            int dr, dg, db; double dR;
            dr = p1.R - p2.R; dg = p1.G - p2.G; db = p1.B - p2.B;
            dR = Math.Sqrt(dr * dr + dg * dg + db * db); dR = dR / (1.73);
            return dR;
        }


        public double GriFuzzyMin(double p1, double p2)
        {
            double s1, g1, w1;
            double s2, g2, w2;
            double t1, t2;
            double benzerlik;

            double[] prem = new double[9];
            double[] b = new double[9];


            s1 =FuzzyMem(0, 0, 128, 255, p1);
            g1 =FuzzyMem(0, 128, 255, 255, p1);
            w1 =FuzzyMem(128, 255, 255, 255, p1);

            s2 =FuzzyMem(0, 0, 128, 255, p2);
            g2 =FuzzyMem(0, 128, 255, 255, p2);
            w2 =FuzzyMem(128, 255, 255, 255, p2);

            if (s1 > 0 && s2 > 0)
            {
                prem[0] = Math.Min(s1, s2);
                b[0] = 100.0;
            }

            if (s1 > 0 && g2 > 0)
            {
                prem[1] = Math.Min(s1, g2);
                b[1] = 50.0;
            }

            if (s1 > 0 && w2 > 0)
            {
                prem[2] = Math.Min(s1, w2);
                b[2] = 0.0;
            }

            if (g1 > 0 && s2 > 0)
            {
                prem[3] = Math.Min(g1, s2);
                b[3] = 50.0;
            }

            if (g1 > 0 && g2 > 0)
            {
                prem[4] = Math.Min(g1, g2);
                b[4] = 100.0;
            }

            if (g1 > 0 && w2 > 0)
            {
                prem[5] = Math.Min(g1, w2);
                b[5] = 50.0;
            }

            if (w1 > 0 && s2 > 0)
            {
                prem[6] = Math.Min(w1, s2);
                b[6] = 0.0;
            }

            if (w1 > 0 && g2 > 0)
            {
                prem[7] = Math.Min(w1, g2);
                b[7] = 50.0;
            }

            if (w1 > 0 && w2 > 0)
            {
                prem[8] = Math.Min(w1, w2);
                b[8] = 100.0;
            }

            t1 = 0; t2 = 0;
            for (int i = 0; i < 9; i++)
            {
                t1 = t1 + b[i] * prem[i];
                t2 = t2 + prem[i];
            }
            if (t2 == 0) t2 = 0.001;
            benzerlik = t1 / t2;
            return benzerlik;

        }
        public double GriFuzzyProduct(double p1, double p2)
        {
            double s1, g1, w1;
            double s2, g2, w2;
            double t1, t2;
            double benzerlik;

            double[] prem = new double[9];
            double[] b = new double[9];


            s1 =FuzzyMem(0, 0, 128, 255, p1);
            g1 =FuzzyMem(0, 128, 255, 255, p1);
            w1 =FuzzyMem(128, 255, 255, 255, p1);

            s2 =FuzzyMem(0, 0, 128, 255, p2);
            g2 =FuzzyMem(0, 128, 255, 255, p2);
            w2 =FuzzyMem(128, 255, 255, 255, p2);

            if (s1 > 0 && s2 > 0)
            {
                prem[0] = Math.Min(s1, s2);
                b[0] = 100.0;
            }

            if (s1 > 0 && g2 > 0)
            {
                prem[1] = s1 * g2;
                b[1] = 50.0;
            }

            if (s1 > 0 && w2 > 0)
            {
                prem[2] = s1 * w2;
                b[2] = 0.0;
            }

            if (g1 > 0 && s2 > 0)
            {
                prem[3] = g1 * s2;
                b[3] = 50.0;
            }

            if (g1 > 0 && g2 > 0)
            {
                prem[4] = g1 * g2;
                b[4] = 100.0;
            }

            if (g1 > 0 && w2 > 0)
            {
                prem[5] = g1 * w2;
                b[5] = 50.0;
            }

            if (w1 > 0 && s2 > 0)
            {
                prem[6] = w1 * s2;
                b[6] = 0.0;
            }

            if (w1 > 0 && g2 > 0)
            {
                prem[7] = w1 * g2;
                b[7] = 50.0;
            }

            if (w1 > 0 && w2 > 0)
            {
                prem[8] = w1 * w2;
                b[8] = 100.0;
            }

            t1 = 0; t2 = 0;
            for (int i = 0; i < 9; i++)
            {
                t1 = t1 + b[i] * prem[i];
                t2 = t2 + prem[i];
            }
            if (t2 == 0) t2 = 0.001;
            benzerlik = t1 / t2;
            return benzerlik;

        }



        public double FuzzyMem(double low, double mid, double high, double univers, double x)
        {
            double mem;

            if (low == (-univers) && mid == (-univers))
            {
                if (x <= mid)
                { mem = 1; }
                else if (x > mid && x < high)
                { mem = 1 + (x - mid) / (mid - high); }
                else
                { mem = 0.0; }
            }
            else if (mid == univers && high == univers)
            {
                if (x >= mid)
                { mem = 1; }
                else if (x > low && x < mid)
                { mem = 1 + (x - mid) / (mid - low); }
                else
                { mem = 0.0; }
            }
            else
            {
                if (x > low && x < mid)
                { mem = 1 + (x - mid) / (mid - low); }
                else if (x == mid)
                { mem = 1; }
                else if (x > mid && x < high)
                { mem = 1 + (x - mid) / (mid - high); }
                else
                { mem = 0.0; }
            }

            return mem;
        }



        public double FuzzyMemGaus(double low, double mid, double high, double univers, double x)
        {
            double mem, y, zig;
            zig = 50;
            y = (x - mid) * (x - mid) / (zig * zig);

            if (low == (-univers) && mid == (-univers))
            {
                if (x <= mid)
                { mem = 1; }
                else if (x > mid && x < high)
                { mem = Math.Exp(-0.5 * y); }
                else
                { mem = 0.0; }
            }
            else if (mid == univers && high == univers)
            {
                if (x >= mid)
                { mem = 1; }
                else if (x > low && x < mid)
                { mem = Math.Exp(-0.5 * y); }
                else
                { mem = 0.0; }
            }
            else
            {
                if (x > low && x < mid)
                { mem = mem = Math.Exp(-0.5 * y); }
                else if (x == mid)
                { mem = 1; }
                else if (x > mid && x < high)
                { mem = mem = Math.Exp(-0.5 * y); }
                else
                { mem = 0.0; }
            }
            return mem;

        }



        public double FuzzyPerceptionBolgef(Color c1, Color c2, double c)
        {
            int x1, x2, x3;
            double[] mem1, mem2, mem3, b, prem; double tot1, tot2, benzerlik;
            int k, j, l, m; mem1 = new double[3]; mem2 = new double[3]; mem3 = new double[3];
            b = new double[27]; prem = new double[27];

            x1 = Math.Abs(c1.R - c2.R);
            x2 = Math.Abs(c1.G - c2.G);
            x3 = Math.Abs(c1.B - c2.B);
            x1 = (int)c * x1; x2 = (int)c * x2; x2 = (int)c * x2;

            b[0] = 100.0; b[1] = 100.0; b[2] = 100.0;
            b[3] = 100.0; b[4] = 100.0; b[5] = 100.0;
            b[6] = 100.0; b[7] = 100.0; b[8] = 100.0;

            b[9] = 75.0; b[10] = 75.0; b[11] = 50.0;
            b[12] = 75.0; b[13] = 50.0; b[14] = 50.0;
            b[15] = 50.0; b[16] = 50.0; b[17] = 25.0;

            b[18] = 50.0; b[19] = 25.0; b[20] = 25.0;
            b[21] = 25.0; b[22] = 0.0; b[23] = 0.0;
            b[24] = 25.0; b[25] = 0.0; b[26] = 0.0;



            for (k = 0; k < 3; k = k + 1)
            { mem1[k] = 0.0; mem2[k] = 0.0; mem3[k] = 0.0; }

            for (k = 0; k < 27; k = k + 1)
            { prem[k] = 0.0; }
            tot1 = 0.0; tot2 = 0.0; benzerlik = 0;


            mem1[0] = FuzzyMem(0, 0, 127, 255, x1);
            mem1[1] = FuzzyMem(0, 127, 255, 255, x1);
            mem1[2] = FuzzyMem(127, 255, 255, 255, x1);


            mem2[0] = FuzzyMem(0, 0, 127, 255, x2);
            mem2[1] = FuzzyMem(0, 127, 255, 255, x2);
            mem2[2] = FuzzyMem(127, 255, 255, 255, x2);

            mem3[0] = FuzzyMem(0, 0, 127, 255, x3);
            mem3[1] = FuzzyMem(0, 127, 255, 255, x3);
            mem3[2] = FuzzyMem(127, 255, 255, 255, x3);

            j = 0;
            for (k = 0; k < 3; k = k + 1)
            {
                for (l = 0; l < 3; l = l + 1)
                {
                    for (m = 0; m < 3; m = m + 1)
                    {
                        prem[j] = mem3[k] * mem2[l] * mem1[m];
                        b[j] = 100 * (6 - (k + l + m)) / 6;
                        j = j + 1;
                    }
                }
            }


            for (j = 0; j < 27; j = j + 1)
            {
                tot2 = tot2 + b[j] * prem[j];
                tot1 = tot1 + prem[j];
            }
            if (tot1 > 0)
            { benzerlik = tot2 / tot1; }
            benzerlik = (benzerlik / 100);

            return benzerlik;

        }





        public Bitmap Benzerlik(Bitmap bmp1, double D, int fonktipi)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double mem, memxy, dr, memtoplam, Di;
            int res, x, y, i, j, b; Color p9, c2;
            int[] q1, q2, q3;
            q1 = new int[10]; q2 = new int[10]; q3 = new int[10];

            p9 = Color.FromArgb(0, 0, 0); c2 = Color.FromArgb(0, 0, 0);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {


                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        {
                            p9 = bmp1.GetPixel(x, y);
                            q1[b] = p9.R; q2[b] = p9.G; q3[b] = p9.B;
                        }
                        else { q1[b] = 0; q2[b] = 0; q3[b] = 0; }
                    }



                    sirayadiz(q1, 9); sirayadiz(q2, 9); sirayadiz(q2, 9);
                    Di = (q1[4] + q2[4] + q3[4]) / 3; if (Di == 0) Di = 1;

                    memtoplam = 0; mem = 0;
                    p9 = bmp1.GetPixel(i, j);
                    for (b = 0; b < 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        {
                            dr = Math.Sqrt(Math.Pow(i - x, 2) + Math.Pow(y - j, 2)); memxy = Math.Exp(-0.5 * (dr / 1.0) * (dr / 1.0));
                            c2 = bmp1.GetPixel(x, y);
                            if (D > 0) mem = MemberShip3(p9, c2, (int)D, fonktipi);
                            else mem = MemberShip3(p9, c2, (int)Di, fonktipi);
                        }
                        else { mem = 0; memxy = 0; }
                        memtoplam = memtoplam + mem * memxy;
                    }

                    memtoplam = 255 * memtoplam / 8;
                    c2 = Color.FromArgb((int)memtoplam, (int)memtoplam, (int)memtoplam);
                    bmp2.SetPixel(i, j, c2);

                }
            }
            return bmp2;
        }



        public Bitmap FuzzyBenzerlik(Bitmap bmp1, double c)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double mem, memtoplam, m1, m2, m3, mx, my;
            int res, x, y, i, j, b; Color p9, c2;
            int[] q1, q2, q3;
            q1 = new int[10]; q2 = new int[10]; q3 = new int[10];
            Color[] renk = new Color[10];

            p9 = Color.FromArgb(0, 0, 0); c2 = Color.FromArgb(0, 0, 0);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    memtoplam = 0; mem = 0;
                    p9 = bmp1.GetPixel(i, j);
                    for (b = 0; b < 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        {
                            c2 = bmp1.GetPixel(x, y);
                            mem = FuzzyPerceptionBolgef(p9, c2, c);
                        }
                        else mem = 0;
                        memtoplam = memtoplam + mem;
                    }
                    memtoplam = 255 * memtoplam / 8;


                    /*
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        {
                            renk[b] = bmp1.GetPixel(x, y); q1[b] = 1;
                            mem = FuzzyPerceptionBolgef(p9, c2, c);
                        }
                        else { renk[b] = c2; q1[b] = 0; }
                    }

                    m1 = FuzzyPerceptionBolgef(renk[8], renk[3], c);
                    m2 = FuzzyPerceptionBolgef(renk[8], renk[4], c);
                    m3 = FuzzyPerceptionBolgef(renk[3], renk[4], c);
                    mx =255 * (m1+m2+m3) /3;
                    m1 = FuzzyPerceptionBolgef(renk[8], renk[1], c);
                    m2 = FuzzyPerceptionBolgef(renk[8], renk[6], c);
                    m3 = FuzzyPerceptionBolgef(renk[1], renk[6], c);
                    my = 255 * (m1 + m2 + m3) / 3;
                    memtoplam = (mx + my) / 2;
                     */
                    c2 = Color.FromArgb((int)memtoplam, (int)memtoplam, (int)memtoplam);
                    bmp2.SetPixel(i, j, c2);

                }
            }
            return bmp2;
        }




        public double FuzzySobel(int x1, int x2, int x3, int x4, int x5, int x6)
        {
            double[] mem1, mem2, mem3, mem4, mem5, mem6, prem; double tot1, tot2, benzerlik;
            int j, k, l, m, n, p, r, y;
            int[] b;
            mem1 = new double[3]; mem2 = new double[3]; mem3 = new double[3];
            mem4 = new double[3]; mem5 = new double[3]; mem6 = new double[3];
            prem = new double[729]; b = new int[729];

            for (k = 0; k < 3; k = k + 1)
            { mem1[k] = 0.0; mem2[k] = 0.0; mem3[k] = 0.0; mem4[k] = 0.0; mem5[k] = 0.0; mem6[k] = 0.0; }

            for (k = 0; k < 729; k = k + 1)
            { prem[k] = 0.0; }
            tot1 = 0.0; tot2 = 0.0; benzerlik = 0;


            /*
            mem1[0] = FuzzyMem(0, 0, 127, 255, x1);
            mem1[1] = FuzzyMem(0, 127, 255, 255, x1);
            mem1[2] = FuzzyMem(127, 255, 255, 255, x1);

            mem2[0] = FuzzyMem(0, 0, 127, 255, x2);
            mem2[1] = FuzzyMem(0, 127, 255, 255, x2);
            mem2[2] = FuzzyMem(127, 255, 255, 255, x2);

            mem3[0] = FuzzyMem(0, 0, 127, 255, x3);
            mem3[1] = FuzzyMem(0, 127, 255, 255, x3);
            mem3[2] = FuzzyMem(127, 255, 255, 255, x3);

            mem4[0] = FuzzyMem(0, 0, 127, 255, x4);
            mem4[1] = FuzzyMem(0, 127, 255, 255, x4);
            mem4[2] = FuzzyMem(127, 255, 255, 255, x4);

            mem5[0] = FuzzyMem(0, 0, 127, 255, x5);
            mem5[1] = FuzzyMem(0, 127, 255, 255, x5);
            mem5[2] = FuzzyMem(127, 255, 255, 255, x5);

            mem6[0] = FuzzyMem(0, 0, 127, 255, x6);
            mem6[1] = FuzzyMem(0, 127, 255, 255, x6);
            mem6[2] = FuzzyMem(127, 255, 255, 255, x6);
            */


            
            mem1[0]=FuzzyMemGaus(0,0,127,255,x1);
            mem1[1]=FuzzyMemGaus(0,127,255,255,x1);
            mem1[2]=FuzzyMemGaus(127,255,255,255,x1);

            mem2[0]=FuzzyMemGaus(0,0,127,255,x2);
            mem2[1]=FuzzyMemGaus(0,127,255,255,x2);
            mem2[2]=FuzzyMemGaus(127,255,255,255,x2);

            mem3[0]=FuzzyMemGaus(0,0,127,255,x3);
            mem3[1]=FuzzyMemGaus(0,127,255,255,x3);
            mem3[2]=FuzzyMemGaus(127,255,255,255,x3);

            mem4[0]=FuzzyMemGaus(0,0,127,255,x4);
            mem4[1]=FuzzyMemGaus(0,127,255,255,x4);
            mem4[2]=FuzzyMemGaus(127,255,255,255,x4);

            mem5[0]=FuzzyMemGaus(0,0,127,255,x5);
            mem5[1]=FuzzyMemGaus(0,127,255,255,x5);
            mem5[2]=FuzzyMemGaus(127,255,255,255,x5);

            mem6[0]=FuzzyMemGaus(0,0,127,255,x6);
            mem6[1]=FuzzyMemGaus(0,127,255,255,x6);
            mem6[2]=FuzzyMemGaus(127,255,255,255,x6);
               


            j = 0;
            for (k = 0; k < 3; k = k + 1)
            {
                for (l = 0; l < 3; l = l + 1)
                {
                    for (m = 0; m < 3; m = m + 1)
                    {
                        for (n = 0; n < 3; n = n + 1)
                        {
                            for (p = 0; p < 3; p = p + 1)
                            {
                                for (r = 0; r < 3; r = r + 1)
                                {
                                    prem[j] = mem1[k] * mem2[l] * mem3[m] * mem4[n] * mem5[p] * mem6[r];
                                    y = (n + p + r) - (k + l + m);
                                    b[j] = y * 255 / 6;
                                    j = j + 1;
                                }
                            }
                        }
                    }
                }
            }


            for (j = 0; j < 729; j = j + 1)
            {
                tot2 = tot2 + b[j] * prem[j];
                tot1 = tot1 + prem[j];
            }
            if (tot1 > 0)
            { benzerlik = tot2 / tot1; }

            return benzerlik;
        }





        public int ExOr(int a, int b)
        {
            int q; if (a == b) q = 0; else q = 255;
            return q;
        }


        public Bitmap BlockCode(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color p9, c1; int i, j, w;
            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    if (i == 0 && j == 0) bmp2.SetPixel(i, j, p9);
                    else if (i > 0 && j == 0)
                    {
                        p9 = bmp1.GetPixel(i, j); c1 = bmp1.GetPixel(i - 1, j); w = ExOr(p9.R, c1.R);
                        c1 = Color.FromArgb(w, w, w); bmp2.SetPixel(i, j, c1);
                    }
                    else if (j > 0)
                    {
                        p9 = bmp1.GetPixel(i, j); c1 = bmp1.GetPixel(i, j - 1); w = ExOr(p9.R, c1.R);
                        c1 = Color.FromArgb(w, w, w); bmp2.SetPixel(i, j, c1);
                    }

                }
            }
            return bmp2;
        }


        public void HistogramtFile(Bitmap bmp1, double[] hr, double[] hg, double[] hb)
        {
            FileStream fs = new FileStream("c:\\Medpic\\histo.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            int i, j; double d, y1, y2; Color p9;

            int[] Hr, Hg, Hb;
            Hr = new int[256]; Hg = new int[256]; Hb = new int[256];

            for (i = 0; i < 256; i++)
            { Hr[i] = 0; Hg[i] = 0; Hb[i] = 0; }

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    Hr[p9.R] = Hr[p9.R] + 1; Hg[p9.G] = Hg[p9.G] + 1; Hb[p9.B] = Hb[p9.B] + 1;
                }
            }

            for (i = 0; i < 256; i++)
            {
                hr[i] = (double)(Hr[i]) / (bmp1.Width * bmp1.Height);
                hg[i] = (double)(Hg[i]) / (bmp1.Width * bmp1.Height);
                hb[i] = (double)(Hb[i]) / (bmp1.Width * bmp1.Height);
            }
            y1 = 0; y2 = 0;
            for (i = 0; i < 256; i++)
            {
              //  d = (double)(i - 13); d = d * d; y1 = Math.Exp(-d / (2 * 42 * 42));
              //  d = (double)(i - 163); d = d * d; y2 = Math.Exp(-d / (2 * 42 * 42));
            //    dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", i, hr[i], y1, y2);
               dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", i, hr[i], hg[i], hb[i]);

            }
            dosya.Close();

        }

        ////////////////////Gaussian kernel ///////////////
        public double[] createFilter(int mask, double sigma)
        {
            // set standard deviation to 1.0
            double[] gKernel = new double[mask];
            double r, s = 2.0 * sigma * sigma;

            // sum is for normalization
            double sum = 0.0;
            int m = (mask / 2);
            // generate 5x5 kernel
            for (int x = -(m); x <= (m); x++)
            {
                r = Math.Sqrt(x * x);
                gKernel[x + m] = (Math.Exp(-(r * r) / s)) / (Math.PI * s);
                sum += gKernel[x + m];
            }

            // normalize the Kernel
            for (int i = 0; i < mask; ++i)
                gKernel[i] /= sum;
            return gKernel;
        }
        ////////////////////Gaussian Kernel ///////////////

        /////////////Get gause hist
        public double[] getGaussHist(double[] h, double[] Mask, int masksize)
        {

            int m = (masksize / 2);
            double[] histGaus = new double[256];
            for (int i = 0; i < 256; i++)
            {
                double sumMask = 0;

                for (int j = -m; j <= m; j++)
                {
                    int position = i + j;
                    if ((position >= 0) && (position <= 255))
                    {
                        sumMask += h[position] * Mask[j + m];
                    }
                }
                histGaus[i] = sumMask;
            }
            return histGaus;
        }
        ///////////// Get gauss gist


        //public void Scatter(Bitmap bmp1, double[] hr, double[] hg, double[] hb)
        //{
        //    int i, j; Color p9;
        //    int[] Hr, Hg, Hb, Hry; Hr = new int[256]; Hg = new int[256]; Hb = new int[256]; Hry = new int[256];

        //    FileStream fs = new FileStream("c:\\Medpic\\Scatter.txt", FileMode.Create, FileAccess.Write);
        //    StreamWriter dosya = new StreamWriter(fs);

        //    for (j = 0; j < bmp1.Height; j++)
        //    {
        //        for (i = 0; i < bmp1.Width; i++)
        //        {
        //            p9 = bmp1.GetPixel(i, j);
        //            Hr[i] = p9.R;
        //        }
        //    }
        //    for (j = 0; j < bmp1.Height; j++)
        //    {
        //        for (i = 0; i < bmp1.Width; i++)
        //        {
        //            Hry[i] = Hr[(i + 1) % 256];
        //        }
        //    }
        //    for (var k = 0; k < 256; k++)
        //    {
        //        dosya.WriteLine("{0:N4}\t{1:N3}\t{2:N3}", k, Hr[k], Hry[k]);
        //    }
        //    dosya.Close();
        //}
        public Bitmap Scatter(Bitmap bmp1, int yon, int kanal, int n)
        {
            FileStream fs = new FileStream("c:\\Medpic\\Scatter.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            Bitmap bmp2 = new Bitmap(256, 256);
            Color p9, px, py, pxy, c3;
            int resim, i, j, x, y, t;


            for (i = 0; i < bmp2.Width; i++)
            {
                for (j = 0; j < bmp2.Height; j++)
                {
                    p9 = Color.FromArgb(255, 255, 255);
                    bmp2.SetPixel(i, j, p9);
                }
            }

            px = Color.FromArgb(255, 255, 255);
            py = Color.FromArgb(255, 255, 255);
            pxy = Color.FromArgb(255, 255, 255);
            c3 = Color.FromArgb(255, 255, 255);

            t = 0;

            for (i = 1; i < bmp1.Width - 1; i++)
            {
                for (j = 1; j < bmp1.Height - 1; j++)
                {

                    p9 = bmp1.GetPixel(i, j);

                    resim = resimdemi(i + 1, j, bmp1.Width, bmp1.Height);
                    if (resim == 1)
                        px = bmp1.GetPixel(i + 1, j);

                    resim = resimdemi(i, j + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1)
                        py = bmp1.GetPixel(i, j + 1);

                    resim = resimdemi(i + 1, j + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1)
                        pxy = bmp1.GetPixel(i + 1, j + 1);

                    if (yon == 1)
                    {
                        if (kanal == 1)
                        {
                            x = p9.R; y = px.R;
                            c3 = Color.FromArgb(255, 0, 0);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.R, px.R, py.R, pxy.R); }
                        }
                        else if (kanal == 2)
                        {
                            x = p9.G; y = px.G;
                            c3 = Color.FromArgb(0, 255, 0);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.G, px.G, py.G, pxy.G); }
                        }
                        else if (kanal == 3)
                        {
                            x = p9.B; y = px.B;
                            c3 = Color.FromArgb(0, 0, 255);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.B, px.R, py.B, pxy.B); }
                        }



                    }
                    else if (yon == 2)
                    {
                        if (kanal == 1)
                        {
                            x = p9.R; y = py.R;
                            c3 = Color.FromArgb(255, 0, 0);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.R, px.R, py.R, pxy.R); }
                        }
                        else if (kanal == 2)
                        {
                            x = p9.G; y = py.G;
                            c3 = Color.FromArgb(0, 255, 0);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.G, px.G, py.G, pxy.G); }
                        }
                        else if (kanal == 3)
                        {
                            x = p9.B; y = py.B;
                            c3 = Color.FromArgb(0, 0, 255);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.B, px.R, py.B, pxy.B); }
                        }

                    }
                    else if (yon == 3)
                    {
                        if (kanal == 1)
                        {
                            x = p9.R; y = pxy.R;
                            c3 = Color.FromArgb(255, 0, 0);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.R, px.R, py.R, pxy.R); }
                        }
                        else if (kanal == 2)
                        {
                            x = p9.G; y = pxy.G;
                            c3 = Color.FromArgb(0, 255, 0);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.G, px.G, py.G, pxy.G); }
                        }
                        else if (kanal == 3)
                        {
                            x = p9.B; y = pxy.B;
                            c3 = Color.FromArgb(0, 0, 255);
                            bmp2.SetPixel(x, 255 - y, c3);
                            if (t <= n)
                            { dosya.WriteLine("{0:N4}\t{1:N5}\t{2:N5}\t{3:N5}", p9.B, px.R, py.B, pxy.B); }
                        }

                    }


                    //t = t + 1;
                }
            }
            dosya.Close();
            return bmp2;
        }
        public void GetNPCRandUAC(Bitmap bmp1, Bitmap bmp2, double[] NPCR, double[] UACI)
        {
            int i, j; Color c1, c2;
            int q1, q2, q3;

            q1 = 0; q2 = 0; q3 = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                    if (c1.R != c2.R) q1 = q1 + 1;
                    if (c1.G != c2.G) q2 = q2 + 1;
                    if (c1.B != c2.B) q3 = q3 + 1;
                }
            }

            NPCR[0] = (double)(100 * q1 / (double)(bmp1.Height * bmp1.Width));
            NPCR[1] = (double)(100 * q2 / (double)(bmp1.Height * bmp1.Width));
            NPCR[2] = (double)(100 * q3 / (double)(bmp1.Height * bmp1.Width));


            q1 = 0; q2 = 0; q3 = 0;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                    q1 = q1 + Math.Abs(c1.R - c2.R);
                    q2 = q2 + Math.Abs(c1.G - c2.G);
                    q3 = q3 + Math.Abs(c1.B - c2.B);
                }
            }

            UACI[0] = (double)(100 * q1 / (double)(255 * bmp1.Height * bmp1.Width));
            UACI[1] = (double)(100 * q2 / (double)(255 * bmp1.Height * bmp1.Width));
            UACI[2] = (double)(100 * q3 / (double)(255 * bmp1.Height * bmp1.Width));



        }
        public void Histogram(Bitmap bmp1, double[] hr, double[] hg, double[] hb)
        {
            int i, j; Color p9;
            int[] Hr, Hg, Hb; Hr = new int[256]; Hg = new int[256]; Hb = new int[256];

            for (i = 0; i < 256; i++)
            { Hr[i] = 0; Hg[i] = 0; Hb[i] = 0; }

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    Hr[p9.R] = Hr[p9.R] + 1; Hg[p9.G] = Hg[p9.G] + 1; Hb[p9.B] = Hb[p9.B] + 1;
                }
            }

            for (i = 0; i < 256; i++)
            {
                hr[i] = (double)(Hr[i]) / (bmp1.Width * bmp1.Height);
                hg[i] = (double)(Hg[i]) / (bmp1.Width * bmp1.Height);
                hb[i] = (double)(Hb[i]) / (bmp1.Width * bmp1.Height);
            }
        }

        public double HistogramKarsilastir(double[] h1, double[] h2)
        {
            double uab, ua, ub, benzer; int i;
            ua = 0; ub = 0; uab = 0;
            for (i = 0; i < h1.Length; i++)
            {
                ua = ua + h1[i] * h1[i]; ub = ub + h2[i] * h2[i];
                uab = uab + h1[i] * h2[i];
            }
            benzer = uab / (Math.Sqrt(ua) * Math.Sqrt(ub));
            return benzer;
        }

        public Bitmap HistogramEsitle(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int[] Hr, kum, norm; Color p9;
            Hr = new int[256]; kum = new int[256]; norm = new int[256];
            int i, j, k, max;

            for (k = 0; k < 256; k++) { Hr[k] = 0; }
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    Hr[p9.R] = Hr[p9.R] + 1;
                }
            }

            kum[0] = Hr[0];
            for (k = 1; k < 256; k++)
            { kum[k] = kum[k - 1] + Hr[k]; }

            for (k = 0; k < 256; k++)
            { norm[k] = (int)kum[k] * 255 / (bmp1.Width * bmp1.Height); }


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    max = norm[p9.R];
                    Hr[p9.R] = Hr[p9.R] + 1;
                    p9 = Color.FromArgb(max, max, max);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }

        public void HistogramKum(double[] hr, double[] hg, double[] hb)
        {
            int i;
            double[] htr, htg, htb; htr = new double[256]; htg = new double[256]; htb = new double[256];
            double[] kumr, kumg, kumb; kumr = new double[256]; kumg = new double[256]; kumb = new double[256];

            for (i = 0; i < 256; i++)
            { htr[i] = hr[i]; htg[i] = hg[i]; htb[i] = hb[i]; }

            kumr[0] = htr[0]; kumg[0] = htg[0]; kumb[0] = htb[0];
            for (i = 1; i < 256; i++)
            { kumr[i] = kumr[i - 1] + htr[i]; kumg[i] = kumg[i - 1] + htg[i]; kumb[i] = kumb[i - 1] + htb[i]; }

            for (i = 0; i < 256; i++)
            { hr[i] = kumr[i]; hg[i] = kumg[i]; hb[i] = kumb[i]; }

        }

        public double[] HistogramKum2(double[] hr)
        {   int i;
            double[] kumr; kumr = new double[256];
            kumr[0] = hr[0]; 
            for (i = 1; i < 256; i++)
            { kumr[i] = kumr[i - 1] + hr[i];  }
            return kumr;
        }

        public double[] HistogramD2(double[] hr)
        {
            int i;
            double[] dr; dr = new double[256];
            dr[0] = hr[0];
            for (i = 1; i < 256; i++)
            { dr[i] =hr[i]-dr[i - 1];  }
            return dr;
        }



        public double[] HistogramI2(double[] hr,int konT)
        {    int i, j, k;
            double z1;
            double[] htr; htr = new double[256];
            
           for (i = 0; i < 256; i++)
            {
                z1 = 0;
                for (j = -konT; j <= konT; j++)    
                {
                    k = i + j;
                    if (k >= 0 && k <= 255)
                    { z1 = z1 + hr[k]; }
                }
                htr[i] = z1;
            }

            return htr;
        }


        public double[] HistogramEkle(double[] h1, double[] h2)
        {
            int i, a1; a1 = h1.Length;
            int a2; a2 = h2.Length;
            int a; a = a1 + a2;
            double[] h = new double[a];
            for (i = 0; i < a1; i++)
            { h[i] = h1[i]; }

            for (i = 0; i < a2; i++)
            { h[a1 + i] = h2[i]; }
            return h;
        }


        public double HistogramMax(double[] hr)
        {
            double max; int i;
            max = hr[0];
            for (i = 0; i < hr.Length; i++)
            { if (hr[i] > max) max = hr[i]; }
            return max;
        }
        public double HistogramMin(double[] hr)
        {
            double min; int i;
            min = hr[0];
            for (i = 0; i < hr.Length; i++)
            { if (hr[i] < min) min = hr[i]; }
            return min;
        }

        public double Ex(int L1, int L2, double[] hist)
        {
            double w, E; w = 0; E = 0; int i;
            for (i = L1; i <= L2; i++)
            { w = w + hist[i]; }
            for (i = L1; i <= L2; i++)
            { if (hist[i] > 0 && w > 0) E = E - (hist[i] / w) * Math.Log(hist[i] / w); }
            return E;
        }


        public int KapurFunction(double[] hr, double[] Jr)
        {
            int i, t, EsikSayisi; EsikSayisi = 1;
            double[] Er = new double[EsikSayisi + 1];
            for (i = 0; i < (EsikSayisi + 1); i++)
            { Er[i] = 0; }
            int[] Ttr = new int[EsikSayisi + 2];
            Ttr[0] = 0; Ttr[EsikSayisi + 1] = 255;

            for (t = 0; t < 256; t++)
            {
                Ttr[1] = t;

                for (i = 0; i < (EsikSayisi + 1); i++)
                {
                    if (Ttr[i] == 0)
                        Er[i] = Ex(Ttr[i], Ttr[i + 1], hr);
                    else
                        Er[i] = Ex(Ttr[i] + 1, Ttr[i + 1], hr);
                }
                Jr[t] = Er[0] + Er[1];
            }

            t = HistogramNax(Jr);
            return t;
        }


        public int HistogramNax(double[] hr)
        {
            double max; int i, n;
            max = hr[0]; n = 0;
            for (i = 0; i < hr.Length; i++)
            { if (hr[i] > max) { max = hr[i]; n = i; } }
            return n;
        }

        public int HistogramNin(double[] hr)
        {
            double min; int i, n;
            min = hr[0]; n = 0;
            for (i = 0; i < hr.Length; i++)
            { if (hr[i] < min) { min = hr[i]; n = i; } }
            return n;
        }

        public double Histogram2DNax(double[,] A, int[] T)
        {
            double max; int i, j, n, m;
            max = A[0, 0]; n = 0; m = 0;
            for (i = 0; i < A.GetLength(0); i++)
            {
                for (j = 0; j < A.GetLength(1); j++)
                {
                    if (A[i, j] > max) { max = A[i, j]; n = i; m = j; }

                }
            }
            T[0] = n; T[1] = m;
            return max;
        }

        public void Histogram3DNax(double[, ,] A, int[] T)
        {
            double max; int i, j, k, n, m, t;
            max = A[0, 0, 0]; n = 0; m = 0; t = 0;
            for (i = 0; i < A.GetLength(0); i++)
            {
                for (j = 0; j < A.GetLength(1); j++)
                {
                    for (k = 0; k < A.GetLength(2); k++)
                    {
                        if (A[i, j, k] > max) { max = A[i, j, k]; n = i; m = j; t = k; }
                    }
                }
            }
            T[0] = n; T[1] = m; T[2] = t;
        }


        public int Mdx(double[] hist)  //medyan
        {
            double toplam = 0;
            int i; i = 0;
            do
            {
                toplam = toplam + hist[i]; i++;
            } while (toplam <= 0.5);
            return i;
        }


        public double Px(int L1, int L2, double[] hist)
        {
            double toplam = 0;
            int i;
            for (i = L1; i <= L2; i++)
                toplam = toplam + hist[i];
            return toplam;
        }

        public void PmaxNormal(double[] hr, double[] Jr)
        {
            double max; int i;
            max = hr[0];
            for (i = 0; i < hr.Length; i++)
            { if (hr[i] > max) max = hr[i]; }

            for (i = 0; i < hr.Length; i++)
            { Jr[i] = hr[i] / max; }
        }



        public int Mx(int L1, int L2, double[] hist)  //Mean
        {
            double w, m; int i, t;
            w = 0; m = 0;
            for (i = L1; i <= L2; i++)
            {
                w = w + hist[i];
                m = m + i * hist[i];
            }
            if (w > 0) t = (int)(m / w); else t = 0;
            return t;
        }


        public double Vx(int L1, int L2, double[] hist)  //variance
        {
            double toplam, w, m, t; int i;
            w = 0; m = 0;
            for (i = L1; i <= L2; i++)
            {
                w = w + hist[i];
                m = m + i * hist[i];
            }
            if (w > 0) t = m / w; else t = 0;
            if (w > 0)
            {
                toplam = 0;
                for (i = L1; i <= L2; i++)
                { toplam = toplam + (i - t) * (i - t) * hist[i] / w; }
            }
            else
                toplam = 0;
            return toplam;
        }

        public double MADx(int L1, int L2, double[] hist)  //MAD
        {
            double toplam, w, m, t; int i;
            w = 0; m = 0;
            for (i = L1; i <= L2; i++)
            {
                w = w + hist[i];
                m = m + i * hist[i];
            }
            if (w > 0) t = m / w; else t = 0;
            if (w > 0)
            {
                toplam = 0;
                for (i = L1; i <= L2; i++)
                { toplam = toplam + Math.Abs(i - t) * hist[i] / w; }
            }
            else
                toplam = 0;
            return toplam;
        }



        public double Zigx(int L1, int L2, double[] hist)  //otsu local variance
        {
            double w, zig, m, mT; int i;
            w = 0; m = 0; mT = 0; zig = 0;
            for (i = 0; i < hist.Length; i++)
            { mT = mT + i * hist[i]; }

            for (i = L1; i <= L2; i++)
            {
                w = w + hist[i];
                m = m + i * hist[i];
            }
            if (w > 0) m = m / w;
            zig = w * (m - mT) * (m - mT);
            return zig;
        }


        public int OtsuFunction(double[] hr, double[] Jr)
        {
            int i, t, EsikSayisi; EsikSayisi = 1;
            double[] Sr = new double[EsikSayisi + 1];
            double zigT;
            zigT = Vx(0, 255, hr);

            for (i = 0; i < (EsikSayisi + 1); i++)
            { Sr[i] = 0; }

            int[] Ttr = new int[EsikSayisi + 2];
            Ttr[0] = 0; Ttr[EsikSayisi + 1] = 255;

            for (t = 0; t < 256; t++)
            {
                Ttr[1] = t;

                for (i = 0; i < (EsikSayisi + 1); i++)
                {
                    if (Ttr[i] == 0)
                        Sr[i] = Zigx(Ttr[i], Ttr[i + 1], hr);
                    else
                        Sr[i] = Zigx(Ttr[i] + 1, Ttr[i + 1], hr);
                }
                Jr[t] = (Sr[0] + Sr[1]) / zigT;
            }

            t = HistogramNax(Jr);
            return t;
        }





        public void getMedyan(Bitmap bmp1, int[] T)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256]; int t;
            Histogram(bmp1, hr, hg, hb);
            t = Mdx(hr); T[0] = t;
            t = Mdx(hg); T[1] = t;
            t = Mdx(hb); T[2] = t;
        }

        public void getMean(Bitmap bmp1, int[] T)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256]; double t;
            Histogram(bmp1, hr, hg, hb);
            t = Mx(0, 255, hr); T[0] = (int)t;
            t = Mx(0, 255, hg); T[1] = (int)t;
            t = Mx(0, 255, hb); T[2] = (int)t;
        }

        public void getVariance(Bitmap bmp1, int[] T)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double ar, ag, ab, t1, t2, t3; int i;
            Histogram(bmp1, hr, hg, hb);
            ar = Mx(0, 255, hr); ag = Mx(0, 255, hg); ab = Mx(0, 255, hb);
            t1 = 0; t2 = 0; t3 = 0;
            for (i = 0; i <= 255; i++)
            {
                t1 = t1 + (i - ar) * (i - ar) * hr[i];
                t2 = t2 + (i - ag) * (i - ag) * hg[i];
                t3 = t3 + (i - ab) * (i - ab) * hb[i];
            }
            T[0] = (int)t1;
            T[1] = (int)t2;
            T[2] = (int)t3;
        }


        public void getEntropy(Bitmap bmp1, double[] T)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double t;
            Histogram(bmp1, hr, hg, hb);
            t = Ex(0, 255, hr); T[0] = t;
            t = Ex(0, 255, hg); T[1] = t;
            t = Ex(0, 255, hb); T[2] = t;
        }

        public Bitmap getMAD(Bitmap bmp1, int[] T)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double mr, mg, mb; double madr, madg, madb;
            int i, j; Color cc1, cc2; int q1, q2, q3;

            Histogram(bmp1, hr, hg, hb);
            mr = Mdx(hr); mg = Mdx(hg); mb = Mdx(hb);

            q1 = 0; q2 = 0; q3 = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    cc1 = bmp1.GetPixel(i, j);
                    q1 = Math.Abs(cc1.R - (int)mr);
                    q2 = Math.Abs(cc1.G - (int)mg);
                    q3 = Math.Abs(cc1.B - (int)mb);
                    cc2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(i, j, cc2);
                }
            }

            Histogram(bmp2, hr, hg, hb);
            madr = Mdx(hr); madg = Mdx(hg); madb = Mdx(hb);
            T[0] = (int)madr; T[1] = (int)madg; T[2] = (int)madb;
            return bmp2;
        }


        public Bitmap getAAD(Bitmap bmp1, int[] T)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double mr, mg, mb; double aadr, aadg, aadb;
            int i, j; Color cc1, cc2; int q1, q2, q3;

            Histogram(bmp1, hr, hg, hb);
            mr = Mx(0, 255, hr); mg = Mx(0, 255, hg); mb = Mx(0, 255, hb);

            q1 = 0; q2 = 0; q3 = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    cc1 = bmp1.GetPixel(i, j);
                    q1 = Math.Abs(cc1.R - (int)mr);
                    q2 = Math.Abs(cc1.G - (int)mg);
                    q3 = Math.Abs(cc1.B - (int)mb);
                    cc2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(i, j, cc2);
                }
            }

            Histogram(bmp2, hr, hg, hb);
            aadr = Mx(0, 255, hr); aadg = Mx(0, 255, hg); aadb = Mx(0, 255, hb);
            T[0] = (int)aadr; T[1] = (int)aadg; T[2] = (int)aadb;
            return bmp2;
        }

        public double getAAD3(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double mr, mg, mb; double q1, q2, q3;
            int i, j; Color p9, cc2;

            Histogram(bmp1, hr, hg, hb);
            mr = Mx(0, 255, hr); mg = Mx(0, 255, hg); mb = Mx(0, 255, hb);

            cc2 = Color.FromArgb((int)mr, (int)mg, (int)mb);
            q1 = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    q2 = Distance3(p9, cc2);
                    q1 = q1 + q2;
                }
            }

            q3 = q1 / (bmp1.Width * bmp1.Height);
            return q3;
        }

        public Bitmap getMMAD(Bitmap bmp1, int MaskSize)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double q1, q2, q3, q1a, q2a, q3a;
            int resim, x, y, i, j, ip, jp, m; Color p9;
            q1 = (double)MaskSize / 2;
            q1 = q1 - 0.5;
            m = (int)(q1);


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    q1 = 0; q2 = 0; q3 = 0;
                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(ip, jp);
                                q1 = q1 + p9.R;
                                q2 = q2 + p9.G;
                                q3 = q3 + p9.B;
                            }
                        }
                    }

                    q1a = q1 / (MaskSize * MaskSize);
                    q2a = q2 / (MaskSize * MaskSize);
                    q3a = q3 / (MaskSize * MaskSize);

                    q1 = 0; q2 = 0; q3 = 0;
                    for (y = -m; y <= m; y++)
                    {
                        for (x = -m; x <= m; x++)
                        {
                            jp = j + y; ip = i + x;
                            resim = resimdemi(ip, jp, bmp1.Width, bmp1.Height);
                            if (resim == 1)
                            {
                                p9 = bmp1.GetPixel(ip, jp);
                                q1 = q1 + Math.Abs(q1a - p9.R);
                                q2 = q2 + Math.Abs(q2a - p9.G);
                                q3 = q3 + Math.Abs(q3a - p9.B);
                            }
                        }
                    }

                    q1 = q1 / (MaskSize * MaskSize);
                    q2 = q2 / (MaskSize * MaskSize);
                    q3 = q3 / (MaskSize * MaskSize);

                    p9 = Color.FromArgb((int)q1, (int)q2, (int)q3);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }




        public Bitmap Logaritma(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    x1 = (int)(105.98 * Math.Log10(1 + c1.R));
                    x2 = (int)(105.98 * Math.Log10(1 + c1.G));
                    x3 = (int)(105.98 * Math.Log10(1 + c1.B));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap Exp(Bitmap bmp1, double a)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    x1 = (int)(255 * (1 - Math.Exp(-c1.R / a)));
                    x2 = (int)(255 * (1 - Math.Exp(-c1.G / a)));
                    x3 = (int)(255 * (1 - Math.Exp(-c1.B / a)));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap Ln(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    x1 = (int)(45.98 * Math.Log(1 + (double)c1.R));
                    x2 = (int)(45.98 * Math.Log(1 + (double)c1.G));
                    x3 = (int)(45.98 * Math.Log(1 + (double)c1.B));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap PM1(Bitmap bmp1, int Kr, int Kg, int Kb)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2; double v;
            if (Kr <= 0) Kr = 1; if (Kg <= 0) Kg = 1; if (Kb <= 0) Kb = 1;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    v = (double)(c1.R * c1.R) / (Kr * Kr);
                    x1 = (int)(255 * 1 / (1 + v));
                    v = (double)(c1.G * c1.G) / (Kg * Kg);
                    x2 = (int)(255 * 1 / (1 + v));
                    v = (double)(c1.B * c1.B) / (Kb * Kb);
                    x3 = (int)(255 * 1 / (1 + v));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap PM2(Bitmap bmp1, int Kr, int Kg, int Kb)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2; double v;
            if (Kr <= 0) Kr = 1; if (Kg <= 0) Kg = 1; if (Kb <= 0) Kb = 1;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    v = (double)(c1.R * c1.R) / (Kr * Kr);
                    x1 = (int)(255 * Math.Exp(-v));
                    v = (double)(c1.G * c1.G) / (Kg * Kg);
                    x2 = (int)(255 * Math.Exp(-v));
                    v = (double)(c1.B * c1.B) / (Kb * Kb);
                    x3 = (int)(255 * Math.Exp(-v));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap CB(Bitmap bmp1, int Kr, int Kg, int Kb)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2; double v;
            if (Kr <= 0) Kr = 1; if (Kg <= 0) Kg = 1; if (Kb <= 0) Kb = 1;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    v = (double)(c1.R * c1.R) / (Kr * Kr);
                    x1 = (int)(255 * 1 / Math.Sqrt(1 + v));
                    v = (double)(c1.G * c1.G) / (Kg * Kg);
                    x2 = (int)(255 * 1 / Math.Sqrt(1 + v));
                    v = (double)(c1.B * c1.B) / (Kb * Kb);
                    x3 = (int)(255 * 1 / Math.Sqrt(1 + v));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap WC(Bitmap bmp1, int Kr, int Kg, int Kb)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2; double v;
            if (Kr <= 0) Kr = 1; if (Kg <= 0) Kg = 1; if (Kb <= 0) Kb = 1;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    v = (double)(c1.R * c1.R) / (Kr * Kr);
                    if (v == 0) x1 = 255; else x1 = (int)(255 * (1 - Math.Exp(-3.315 / v)));
                    v = (double)(c1.G * c1.G) / (Kg * Kg);
                    if (v == 0) x2 = 255; else x2 = (int)(255 * (1 - Math.Exp(-3.315 / v)));
                    v = (double)(c1.B * c1.B) / (Kb * Kb);
                    if (v == 0) x3 = 255; else x3 = (int)(255 * (1 - Math.Exp(-3.315 / v)));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap LGS(Bitmap bmp1, int Kr, int Kg, int Kb, int b)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2;
            if (Kr <= 0) Kr = 1; if (Kg <= 0) Kg = 1; if (Kb <= 0) Kb = 1;
            if (b < 0) b = Math.Abs(b); if (b == 0) b = 1;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    x1 = (int)(255 * 1 / (1 + Math.Exp((double)(c1.R - Kr) / b)));
                    x2 = (int)(255 * 1 / (1 + Math.Exp((double)(c1.G - Kg) / b)));
                    x3 = (int)(255 * 1 / (1 + Math.Exp((double)(c1.B - Kb) / b)));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap Sigmoid(Bitmap bmp1, int Kr, int Kg, int Kb, int b)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j, x1, x2, x3; Color c1, c2;
            if (Kr <= 0) Kr = 1; if (Kg <= 0) Kg = 1; if (Kb <= 0) Kb = 1;
            if (b < 0) b = Math.Abs(b); if (b == 0) b = 1;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    x1 = (int)(255 * 1 / (1 + Math.Exp(-(double)(c1.R - Kr) / b)));
                    x2 = (int)(255 * 1 / (1 + Math.Exp(-(double)(c1.G - Kg) / b)));
                    x3 = (int)(255 * 1 / (1 + Math.Exp(-(double)(c1.B - Kb) / b)));
                    c2 = Color.FromArgb(x1, x2, x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap LevelAdjust(Bitmap bmp1, double bottom, double top)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double Rmin, Rmax, Gmin, Gmax, Bmin, Bmax; Color c1, c2;
            double x1, x2, x3;
            int i, j;

            c1 = bmp1.GetPixel(0, 0);
            Rmin = c1.R; Rmax = c1.R;
            Gmin = c1.G; Gmax = c1.G;
            Bmin = c1.B; Bmax = c1.B;



            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    if (c1.R <= Rmin) Rmin = c1.R; if (c1.R >= Rmax) Rmax = c1.R;
                    if (c1.G <= Rmin) Gmin = c1.G; if (c1.G >= Gmax) Gmax = c1.G;
                    if (c1.B <= Bmin) Bmin = c1.B; if (c1.B >= Bmax) Bmax = c1.B;
                }
            }

            if (Rmax == 0) Rmax = 2;
            if (Gmax == 0) Gmax = 2;
            if (Bmax == 0) Bmax = 2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);

                    x1 = bottom + (top - bottom) * (c1.R - Rmin) / (Rmax - Rmin);
                    x2 = bottom + (top - bottom) * (c1.G - Gmin) / (Gmax - Gmin);
                    x3 = bottom + (top - bottom) * (c1.B - Bmin) / (Bmax - Bmin);

                    c2 = Color.FromArgb((int)x1, (int)x2, (int)x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }



        public Bitmap GammaCorection(Bitmap bmp1, double g)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int i, j; Color c1, c2;
            double x1, x2, x3, c;

            c = 1.0 / Math.Pow(255, g);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);


                    x1 = 255 * 1 * Math.Pow((double)(c1.R) / 255, 1 / g);
                    x2 = 255 * 1 * Math.Pow((double)(c1.G) / 255, 1 / g);
                    x3 = 255 * 1 * Math.Pow((double)(c1.B) / 255, 1 / g);

                    /*
                    x1 =255 *c* Math.Pow((double)(c1.R),g);
                    x2 =255 *c*  Math.Pow((double)(c1.G),g);
                    x3 =255 *c* Math.Pow((double)(c1.B),g);
                    */
                    c2 = Color.FromArgb((int)x1, (int)x2, (int)x3);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

      


        public Bitmap GriEsik1(Bitmap bmp1, int t1)
        {
            Bitmap bmp2 = new Bitmap(bmp1);
            int i, j; Color c1, c2;

            c2 = Color.FromArgb(0, 0, 0);
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    if (c1.R > t1)
                        c2 = Color.FromArgb(255, 255, 255);
                    else c2 = Color.FromArgb(0, 0, 255);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap GriEsik2(Bitmap bmp1, int t1, int t2)
        {
            Bitmap bmp2 = new Bitmap(bmp1);
            int i, j; Color c1, c2;
            c2 = Color.FromArgb(0, 0, 0);
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    if (c1.R > t2)
                        c2 = Color.FromArgb(255, 0, 0);
                    if (c1.R > t1 && c1.R < t2)
                        c2 = Color.FromArgb(0, 255, 0);
                    if (c1.R < t1)
                        c2 = Color.FromArgb(0, 0, 255);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

       
        public Bitmap Threshold(Bitmap bmp1, int Tr, int Tg, int Tb)
        {
            Bitmap bmp2 = new Bitmap(bmp1);
            int i, j; Color c1, c2;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j);
                    if (c1.R > Tr && c1.G > Tg && c1.B > Tb)
                        c2 = Color.FromArgb(255, 255, 255);
                    else c2 = Color.FromArgb(0, 0, 0);
                    bmp2.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap MakeBinary(Bitmap bmp1, int ftipi)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone(); Color p9, c2;
            int x, y, Tr, Tg, Tb;
            int[] T; T = new int[3];

            if (ftipi == 1) getMean(bmp1, T);
            else if (ftipi == 2) OtsuKapurTekEsik(bmp1, T, 1);
            else if (ftipi == 3) OtsuKapurTekEsik(bmp1, T, 2);
            else if (ftipi ==4) getMedyan(bmp1, T);
            else if (ftipi == 5) getMAD(bmp1, T);
            else { T[0] = 0; T[1] = 0; T[2] = 0; }

            Tr = T[0]; Tg = T[1]; Tb = T[2];
         
            c2 = Color.FromArgb(0, 0, 0);
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);

                    if (p9.R <= Tr && p9.G <= Tg && p9.B <= Tb)
                    { c2 = Color.FromArgb(0, 0, 0); }
                    else if (p9.R <= Tr && p9.G <= Tg && p9.B >= Tb)
                    { c2 = Color.FromArgb(0, 0, 255); }
                    else if (p9.R <= Tr && p9.G >= Tg && p9.B <= Tb)
                    { c2 = Color.FromArgb(0, 255, 0); }
                    else if (p9.R <= Tr && p9.G >= Tg && p9.B >= Tb)
                    { c2 = Color.FromArgb(0, 255, 255); }
                    else if (p9.R >= Tr && p9.G <= Tg && p9.B <= Tb)
                    { c2 = Color.FromArgb(255, 0, 0); }
                    else if (p9.R >= Tr && p9.G <= Tg && p9.B >= Tb)
                    { c2 = Color.FromArgb(255, 0, 255); }
                    else if (p9.R >= Tr && p9.G >= Tg && p9.B <= Tb)
                    { c2 = Color.FromArgb(255, 255, 0); }
                    else if (p9.R >= Tr && p9.G >= Tg && p9.B >= Tb)
                    { c2 = Color.FromArgb(255, 255, 255); }
                    /*
                     if (p9.R <= Tr && p9.G <= Tg && p9.B <= Tb)
                     { c2 = Color.FromArgb(0, 0, 0); }
                     else if (p9.R <= Tr && p9.G <= Tg && p9.B >= Tb)
                     { c2 = Color.FromArgb(0, 0, Tb); }
                     else if (p9.R <= Tr && p9.G >= Tg && p9.B <= Tb)
                     { c2 = Color.FromArgb(0, Tg, 0); }
                     else if (p9.R <= Tr && p9.G >= Tg && p9.B >= Tb)
                     { c2 = Color.FromArgb(0, Tg,Tb); }
                     else if (p9.R >= Tr && p9.G <= Tg && p9.B <= Tb)
                     { c2 = Color.FromArgb(Tr, 0, 0); }
                     else if (p9.R >= Tr && p9.G <= Tg && p9.B >= Tb)
                     { c2 = Color.FromArgb(Tr, 0, Tb); }
                     else if (p9.R >= Tr && p9.G >= Tg && p9.B <= Tb)
                     { c2 = Color.FromArgb(Tr, Tg, 0); }
                     else if (p9.R >= Tr && p9.G >= Tg && p9.B >= Tb)
                     { c2 = Color.FromArgb(Tr,Tg,Tb); }*/

                    bmp2.SetPixel(x, y, c2);
                }
            }
            return bmp2;
        }


        public Bitmap MakeMultiEsikGray(Bitmap bmp1, int[,] label, double[] h, int[] tr, int renktipi)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone(); Color p9, cmin, cmax, cort;
            double[] hr; hr = new double[256]; double m0;
            int i, j, k, n, t, a, x, y, q1, EsikSayisi, PasifEsikSayisi, ClusterSayisi;
            EsikSayisi = tr.Length; PasifEsikSayisi = EsikSayisi + 2;
            ClusterSayisi = (int)Math.Pow((EsikSayisi + 1), 3);

            int[] Tr = new int[PasifEsikSayisi];
            int[] np = new int[ClusterSayisi + 1];
            Color[] cn = new Color[ClusterSayisi + 1];


            for (k = 0; k < tr.Length; k++)
            { Tr[1 + k] = tr[k]; }

            Tr[0] = 0; Tr[PasifEsikSayisi - 1] = 256;


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }


            for (i = 0; i < ClusterSayisi; i++)
            { np[i] = 0; hr[i] = 0; }

            cmin = Color.FromArgb(255, 255, 255); cmax = Color.FromArgb(255, 255, 255); cort = Color.FromArgb(0, 0, 0);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y); a = 0;
                    for (t = 0; t < (Tr.Length - 1); t++)
                    { if (p9.R >= Tr[t] && p9.R < Tr[t + 1]) a = t; }
                    n = a; label[x, y] = n; hr[n] += p9.R; np[n] += 1;

                    q1 = Tr[a]; if (q1 > 255) q1 = 255;
                    cmin = Color.FromArgb(q1, q1, q1);
                    q1 = Tr[a + 1]; if (q1 > 255) q1 = 255;
                    cmax = Color.FromArgb(q1, q1, q1);
                    cort = Color.FromArgb((int)((cmin.R + cmax.R) / 2), (int)((cmin.R + cmax.R) / 2), (int)((cmin.R + cmax.R) / 2));

                    if (renktipi == 1) bmp2.SetPixel(x, y, cmin);
                    if (renktipi == 2) bmp2.SetPixel(x, y, cort);
                    if (renktipi == 3) bmp2.SetPixel(x, y, cmax);
                }
            }


            if (renktipi == 4)
            {
                m0 = 0;
                for (i = 0; i < ClusterSayisi; i++)
                {
                    if (np[i] > 0) m0 = hr[i] / np[i];
                    cn[i] = Color.FromArgb((int)m0, (int)m0, (int)m0);
                }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    { n = label[x, y]; bmp2.SetPixel(x, y, cn[n]); }
                }
            }

            for (i = 0; i < ClusterSayisi; i++)
            { h[i] = (double)np[i] / (bmp1.Width * bmp1.Height); }

          
            return bmp2;
        }




        public Bitmap MakeMultiEsik(Bitmap bmp1, int[,] label, double[] h, int[] tr, int[] tg, int[] tb, int renktipi)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone(); Color p9, cmin, cmax, cort;
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double m0, m1, m2; int q1, q2, q3;
            int i, j, k, n, t, a, b, c, x, y;
            int EsikSayisi, PasifEsikSayisi, ClusterSayisi;
            EsikSayisi = tr.Length; PasifEsikSayisi = EsikSayisi + 2;
            ClusterSayisi = (int)Math.Pow((EsikSayisi + 1), 3);

            int[] Tr = new int[PasifEsikSayisi];
            int[] Tg = new int[PasifEsikSayisi];
            int[] Tb = new int[PasifEsikSayisi];
            int[] np = new int[ClusterSayisi];
            Color[] cn = new Color[ClusterSayisi];

            for (k = 0; k < tr.Length; k++)
            {
                Tr[1 + k] = tr[k];
                Tg[1 + k] = tg[k];
                Tb[1 + k] = tb[k];
            }

            Tr[0] = 0; Tr[PasifEsikSayisi - 1] = 256;
            Tg[0] = 0; Tg[PasifEsikSayisi - 1] = 256;
            Tb[0] = 0; Tb[PasifEsikSayisi - 1] = 256;


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }


            for (i = 0; i < ClusterSayisi; i++)
            { np[i] = 0; hr[i] = 0; hg[i] = 0; hb[i] = 0; }

            cmin = Color.FromArgb(255, 255, 255);
            cmax = Color.FromArgb(255, 255, 255);
            cort = Color.FromArgb(0, 0, 0);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y); a = 0; b = 0; c = 0;
                    for (t = 0; t < (Tr.Length - 1); t++)
                    {
                        if (p9.R >= Tr[t] && p9.R < Tr[t + 1]) a = t;
                        if (p9.G >= Tg[t] && p9.G < Tg[t + 1]) b = t;
                        if (p9.B >= Tb[t] && p9.B < Tb[t + 1]) c = t;
                    }
                    n = (int)(c * Math.Pow((Tr.Length - 1), 2) + b * Math.Pow((Tr.Length - 1), 1) + a * Math.Pow((Tr.Length - 1), 0));
                    label[x, y] = n; hr[n] += p9.R; hg[n] += p9.G; hb[n] += p9.B; np[n] += 1;

                    q1 = Tr[a]; if (q1 > 255) q1 = 255;
                    q2 = Tg[b]; if (q2 > 255) q2 = 255;
                    q3 = Tb[c]; if (q3 > 255) q3 = 255;
                    cmin = Color.FromArgb(q1, q2, q3);

                    q1 = Tr[a + 1]; if (q1 > 255) q1 = 255;
                    q2 = Tg[b + 1]; if (q2 > 255) q2 = 255;
                    q3 = Tb[c + 1]; if (q3 > 255) q3 = 255;
                    cmax = Color.FromArgb(q1, q2, q3);
                    cort = Color.FromArgb((int)((cmin.R + cmax.R) / 2), (int)((cmin.G + cmax.G) / 2), (int)((cmin.B + cmax.B) / 2));

                    if (renktipi == 1) bmp2.SetPixel(x, y, cmin);
                    if (renktipi == 2) bmp2.SetPixel(x, y, cort);
                    if (renktipi == 3) bmp2.SetPixel(x, y, cmax);
                }
            }


            if (renktipi == 4)
            {
                m0 = 0; m1 = 0; m2 = 0;
                for (i = 0; i < ClusterSayisi; i++)
                {
                    if (np[i] > 0)
                    { m0 = hr[i] / np[i]; m1 = hg[i] / np[i]; m2 = hb[i] / np[i]; }
                  //  cn[i] = Color.FromArgb((int)(255 - m0), (int)(m1), (int)(m2));
                     cn[i] = Color.FromArgb((int)m0, (int)m1, (int)m2);
                }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    { n = label[x, y]; bmp2.SetPixel(x, y, cn[n]); }
                }
            }



            if (renktipi == 5)
            {
                m0 = 0; m1 = 0; m2 = 0;
                for (i = 0; i < ClusterSayisi; i++)
                {
                    if (np[i] > 0)
                    { m0 = hr[i] / np[i]; m1 = hg[i] / np[i]; m2 = hb[i] / np[i]; }
                    cn[i] = Color.FromArgb((int)(255-m0), (int)(255-m1), (int)(m2));
                }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    { n = label[x, y]; bmp2.SetPixel(x, y, cn[n]); }
                }
            }

            for (i = 0; i < ClusterSayisi; i++)
            { h[i] = (double)np[i] / (bmp1.Width * bmp1.Height); }

            return bmp2;
        }


        public void MakeMultiEsikHis(Bitmap bmp1, double[] h, int[] tr, int[] tg, int[] tb)
        {
            Color p9; int i, j, k, n, t, a, b, c, x, y;
            int EsikSayisi, PasifEsikSayisi, ClusterSayisi;
            EsikSayisi = tr.Length; PasifEsikSayisi = EsikSayisi + 2;
            ClusterSayisi = (int)Math.Pow((EsikSayisi + 1), 3);

            int[] Tr = new int[PasifEsikSayisi];
            int[] Tg = new int[PasifEsikSayisi];
            int[] Tb = new int[PasifEsikSayisi];
            int[] np = new int[ClusterSayisi];


            for (k = 0; k < tr.Length; k++)
            {
                Tr[1 + k] = tr[k];
                Tg[1 + k] = tg[k];
                Tb[1 + k] = tb[k];
            }

            Tr[0] = 0; Tr[PasifEsikSayisi - 1] = 256;
            Tg[0] = 0; Tg[PasifEsikSayisi - 1] = 256;
            Tb[0] = 0; Tb[PasifEsikSayisi - 1] = 256;


            for (i = 0; i < ClusterSayisi; i++)
            { np[i] = 0; }


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    a = 0; b = 0; c = 0;
                    for (t = 0; t < (Tr.Length - 1); t++)
                    {
                        if (p9.R >= Tr[t] && p9.R < Tr[t + 1]) a = t;
                        if (p9.G >= Tg[t] && p9.G < Tg[t + 1]) b = t;
                        if (p9.B >= Tb[t] && p9.B < Tb[t + 1]) c = t;
                    }
                    n = (int)(c * Math.Pow((Tr.Length - 1), 2) + b * Math.Pow((Tr.Length - 1), 1) + a * Math.Pow((Tr.Length - 1), 0));
                    np[n] += 1;
                }
            }


            for (i = 0; i < ClusterSayisi; i++)
            { h[i] = (double)np[i] / (double)(bmp1.Width * bmp1.Height); }

        }

        public String Array2String(double[] h, string Ad)
        {
            String sline = "";
            String satir = "";

            satir = String.Format("{0:0.000000}", h[0]); sline = satir;

            for (int i = 1; i < h.Length; i++)
            {
                satir = String.Format("{0:0.000000}", h[i]);
                sline = sline + ";" + satir;
            }
            sline = sline + ";" + Ad;
            return sline;
        }




        public Bitmap Octree(Bitmap bmp1, int[,] label, Color[] renkler, double[] h, int degree, int renktipi)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone(); Color p9;
            double[] hr, hg, hb; hr = new double[512]; hg = new double[512]; hb = new double[512];
            double m0, m1, m2;
            int i, j, n, x, y, seviye; seviye = degree;
            int ClusterSayisi = 1;

            if (degree == 1)
                ClusterSayisi = 8;
            if (degree == 2)
                ClusterSayisi = 64;
            if (degree == 3)
                ClusterSayisi = 512;
            if (degree == 4)
                ClusterSayisi = 256;

            int[] np = new int[ClusterSayisi];
            Color[] cn = new Color[ClusterSayisi];


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }


            for (i = 0; i < ClusterSayisi; i++)
            {
                np[i] = 1; hr[i] = 0; hg[i] = 0; hb[i] = 0;
                cn[i] = Color.FromArgb(0, 0, 0);
            }




            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    n = Ctree(p9, seviye);
                    label[x, y] = n;
                    hr[n] += p9.R;
                    hg[n] += p9.G;
                    hb[n] += p9.B;
                    np[n] += 1;
                }
            }



            m0 = 0; m1 = 0; m2 = 0;
            for (i = 0; i < ClusterSayisi; i++)
            {
                if (np[i] > 0)
                { m0 = hr[i] / np[i]; m1 = hg[i] / np[i]; m2 = hb[i] / np[i]; }
                cn[i] = Color.FromArgb((int)m0, (int)m1, (int)m2);
            }

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    n = label[x, y];
                    if (renktipi == 4)
                        bmp2.SetPixel(x, y, cn[n]);
                    if (renktipi == 5)
                    {
                        p9 = Color.FromArgb(255 - cn[n].R, cn[n].G, 255 - cn[n].B);
                        bmp2.SetPixel(x, y, p9);
                    }


                }
            }


            for (i = 0; i < ClusterSayisi; i++)
            {
                h[i] = (double)np[i] / (bmp1.Width * bmp1.Height);
                renkler[i] = cn[i];
            }



            return bmp2;
        }

        public int Ctree(Color p9, int degree)
        {
            string Rstr = string.Empty;
            string Gstr = string.Empty;
            string Bstr = string.Empty;
            string Dstr = string.Empty;
            int n, a, b, c, etiket;

            Rstr =Decimaltobin(p9.R);
            Gstr =Decimaltobin(p9.G);
            Bstr =Decimaltobin(p9.B);

            char[] Ra = Rstr.ToCharArray();
            char[] Ga = Gstr.ToCharArray();
            char[] Ba = Bstr.ToCharArray();

            etiket = 0;

            n = 0;
            if (Ra[0] == '0' && Ga[0] == '0' && Ba[0] == '0') n = 0;
            if (Ra[0] == '0' && Ga[0] == '0' && Ba[0] == '1') n = 1;
            if (Ra[0] == '0' && Ga[0] == '1' && Ba[0] == '0') n = 2;
            if (Ra[0] == '0' && Ga[0] == '1' && Ba[0] == '1') n = 3;
            if (Ra[0] == '1' && Ga[0] == '0' && Ba[0] == '0') n = 4;
            if (Ra[0] == '1' && Ga[0] == '0' && Ba[0] == '1') n = 5;
            if (Ra[0] == '1' && Ga[0] == '1' && Ba[0] == '0') n = 6;
            if (Ra[0] == '1' && Ga[0] == '1' && Ba[0] == '1') n = 7;
            a = n;

            n = 0;
            if (Ra[1] == '0' && Ga[1] == '0' && Ba[1] == '0') n = 0;
            if (Ra[1] == '0' && Ga[1] == '0' && Ba[1] == '1') n = 1;
            if (Ra[1] == '0' && Ga[1] == '1' && Ba[1] == '0') n = 2;
            if (Ra[1] == '0' && Ga[1] == '1' && Ba[1] == '1') n = 3;
            if (Ra[1] == '1' && Ga[1] == '0' && Ba[1] == '0') n = 4;
            if (Ra[1] == '1' && Ga[1] == '0' && Ba[1] == '1') n = 5;
            if (Ra[1] == '1' && Ga[1] == '1' && Ba[1] == '0') n = 6;
            if (Ra[1] == '1' && Ga[1] == '1' && Ba[1] == '1') n = 7;
            b = n;

            n = 0;
            if (Ra[2] == '0' && Ga[2] == '0' && Ba[2] == '0') n = 0;
            if (Ra[2] == '0' && Ga[2] == '0' && Ba[2] == '1') n = 1;
            if (Ra[2] == '0' && Ga[2] == '1' && Ba[2] == '0') n = 2;
            if (Ra[2] == '0' && Ga[2] == '1' && Ba[2] == '1') n = 3;
            if (Ra[2] == '1' && Ga[2] == '0' && Ba[2] == '0') n = 4;
            if (Ra[2] == '1' && Ga[2] == '0' && Ba[2] == '1') n = 5;
            if (Ra[2] == '1' && Ga[2] == '1' && Ba[2] == '0') n = 6;
            if (Ra[2] == '1' && Ga[2] == '1' && Ba[2] == '1') n = 7;
            c = n;

            if (degree == 1)
                etiket = a;
            if (degree == 2)
                etiket = 8 * a + b;
            if (degree == 3)
                etiket = 8 * 8 * c + 8 * a + b;

            if (degree == 4)
            {
                n = 0;
                if (Ra[2] == '0' && Ga[2] == '0' && Ba[2] == '0') n = 0;
                if (Ra[2] == '0' && Ga[2] == '0' && Ba[2] == '1') n = 0;
                if (Ra[2] == '0' && Ga[2] == '1' && Ba[2] == '0') n = 1;
                if (Ra[2] == '0' && Ga[2] == '1' && Ba[2] == '1') n = 1;
                if (Ra[2] == '1' && Ga[2] == '0' && Ba[2] == '0') n = 2;
                if (Ra[2] == '1' && Ga[2] == '0' && Ba[2] == '1') n = 2;
                if (Ra[2] == '1' && Ga[2] == '1' && Ba[2] == '0') n = 3;
                if (Ra[2] == '1' && Ga[2] == '1' && Ba[2] == '1') n = 3;
                c = n;


                etiket = 8 * 8 * c + 8 * a + b;
            }
            return etiket;
        }






        public string Decimaltobin(int a)
        {
            string sonuc = string.Empty;
            string tampon = string.Empty;
            int n, t = 0; n = a;
            for (int i = 0; n > 0; i++)
            {
                sonuc = n % 2 + sonuc;
                n = n / 2;
            }

            if (sonuc.Length < 8)
                t = 8 - sonuc.Length;

            for (int i = 0; i < t; i++)
            { tampon = tampon + '0'; }
            sonuc = tampon + sonuc;


            if (a == 0) sonuc = "00000000";
            return sonuc;
        }

        public int BintoDecimal(int num)
        {
            int binary_val, decimal_val = 0, base_val = 1, rem;

            binary_val = num;
            while (num > 0)
            {
                rem = num % 10;
                decimal_val = decimal_val + rem * base_val;
                num = num / 10;
                base_val = base_val * 2;
            }

            return decimal_val;
        }


        public int Bintodecimal(int num)
        {
            int b, d = 0, taban = 1, rem;

            b = num;
            while (num > 0)
            {
                rem = num % 10;
                d = d + rem * taban;
                num = num / 10;
                taban = taban * 2;
            }

            return d;
        }

        public int Bintodecimal2(int n)
        {
            int dec = 0, i = 0, rem;
            while (n != 0)
            {
                rem = n % 10;
                n /= 10;
                dec = dec + rem * (int)(Math.Pow(2, i));
                ++i;
            }
            return dec;
        }
      

    

       




        public void OtsuKapurTekEsik(Bitmap bmp1, int[] T, int ftipi)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double[] Jr, Jg, Jb; Jr = new double[256]; Jg = new double[256]; Jb = new double[256];
            int i, t, EsikSayisi; EsikSayisi = 1;
            double[] Sr = new double[EsikSayisi + 1];
            double[] Sg = new double[EsikSayisi + 1];
            double[] Sb = new double[EsikSayisi + 1];


            for (i = 0; i < (EsikSayisi + 1); i++)
            { Sr[i] = 0; Sg[i] = 0; Sb[i] = 0; }

            int[] Ttr = new int[EsikSayisi + 2];
            int[] Ttg = new int[EsikSayisi + 2];
            int[] Ttb = new int[EsikSayisi + 2];

            Ttr[0] = 0; Ttr[EsikSayisi + 1] = 255;
            Ttg[0] = 0; Ttg[EsikSayisi + 1] = 255;
            Ttb[0] = 0; Ttb[EsikSayisi + 1] = 255;


            Histogram(bmp1, hr, hg, hb);

            for (t = 0; t < 256; t++)
            {
                Ttr[1] = t; Ttg[1] = t; Ttb[1] = t;

                if (ftipi == 1)
                {
                    for (i = 0; i < (EsikSayisi + 1); i++)
                    {
                        if (Ttr[i] == 0)
                            Sr[i] = Zigx(Ttr[i], Ttr[i + 1], hr);
                        else
                            Sr[i] = Zigx(Ttr[i] + 1, Ttr[i + 1], hr);

                        if (Ttg[i] == 0)
                            Sg[i] = Zigx(Ttg[i], Ttg[i + 1], hg);
                        else
                            Sg[i] = Zigx(Ttg[i] + 1, Ttg[i + 1], hg);

                        if (Ttb[i] == 0)
                            Sb[i] = Zigx(Ttb[i], Ttb[i + 1], hb);
                        else
                            Sb[i] = Zigx(Ttb[i] + 1, Ttb[i + 1], hb);
                    }
                    Jr[t] = Sr[0] + Sr[1];
                    Jg[t] = Sg[0] + Sg[1];
                    Jb[t] = Sb[0] + Sb[1];
                }

                if (ftipi == 2)
                {
                    for (i = 0; i < (EsikSayisi + 1); i++)
                    {
                        if (Ttr[i] == 0)
                            Sr[i] = Ex(Ttr[i], Ttr[i + 1], hr);
                        else
                            Sr[i] = Ex(Ttr[i] + 1, Ttr[i + 1], hr);

                        if (Ttg[i] == 0)
                            Sg[i] = Ex(Ttg[i], Ttg[i + 1], hg);
                        else
                            Sg[i] = Ex(Ttg[i] + 1, Ttg[i + 1], hg);

                        if (Ttb[i] == 0)
                            Sb[i] = Ex(Ttb[i], Ttb[i + 1], hb);
                        else
                            Sb[i] = Ex(Ttb[i] + 1, Ttb[i + 1], hb);
                    }
                    Jr[t] = Sr[0] + Sr[1];
                    Jg[t] = Sg[0] + Sg[1];
                    Jb[t] = Sb[0] + Sb[1];
                }

            }
            t = HistogramNax(Jr); T[0] = t;
            t = HistogramNax(Jg); T[1] = t;
            t = HistogramNax(Jb); T[2] = t;
        }

        public void OtsuKapurTekEsikNew(Bitmap bmp1, int[] T, int ftipi)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double Jr, Jg, Jb; double jrmax, jgmax, jbmax;
            int i, t, EsikSayisi, kr, kg, kb; EsikSayisi = 1;
            double[] Sr = new double[EsikSayisi + 1];
            double[] Sg = new double[EsikSayisi + 1];
            double[] Sb = new double[EsikSayisi + 1];

            for (i = 0; i < (EsikSayisi + 1); i++)
            { Sr[i] = 0; Sg[i] = 0; Sb[i] = 0; }

            int[] Ttr = new int[EsikSayisi + 2];
            int[] Ttg = new int[EsikSayisi + 2];
            int[] Ttb = new int[EsikSayisi + 2];

            Ttr[0] = 0; Ttr[EsikSayisi + 1] = 255;
            Ttg[0] = 0; Ttg[EsikSayisi + 1] = 255;
            Ttb[0] = 0; Ttb[EsikSayisi + 1] = 255;

            Histogram(bmp1, hr, hg, hb);
            jrmax = 0; jgmax = 0; jbmax = 0;
            kr = 0; kg = 0; kb = 0;
            for (t = 0; t < 256; t++)
            {
                Ttr[1] = t; Ttg[1] = t; Ttb[1] = t;

                if (ftipi == 1)
                {
                    for (i = 0; i < (EsikSayisi + 1); i++)
                    {
                        if (Ttr[i] == 0)
                            Sr[i] = Zigx(Ttr[i], Ttr[i + 1], hr);
                        else
                            Sr[i] = Zigx(Ttr[i] + 1, Ttr[i + 1], hr);

                        if (Ttg[i] == 0)
                            Sg[i] = Zigx(Ttg[i], Ttg[i + 1], hg);
                        else
                            Sg[i] = Zigx(Ttg[i] + 1, Ttg[i + 1], hg);

                        if (Ttb[i] == 0)
                            Sb[i] = Zigx(Ttb[i], Ttb[i + 1], hb);
                        else
                            Sb[i] = Zigx(Ttb[i] + 1, Ttb[i + 1], hb);
                    }
                    Jr = Sr[0] + Sr[1];
                    Jg = Sg[0] + Sg[1];
                    Jb = Sb[0] + Sb[1];

                    if (Jr > jrmax) { jrmax = Jr; kr = t; }
                    if (Jg > jgmax) { jgmax = Jg; kg = t; }
                    if (Jb > jbmax) { jbmax = Jb; kb = t; }
                }


                if (ftipi == 2)
                {
                    for (i = 0; i < (EsikSayisi + 1); i++)
                    {
                        if (Ttr[i] == 0)
                            Sr[i] = Ex(Ttr[i], Ttr[i + 1], hr);
                        else
                            Sr[i] = Ex(Ttr[i] + 1, Ttr[i + 1], hr);

                        if (Ttg[i] == 0)
                            Sg[i] = Ex(Ttg[i], Ttg[i + 1], hg);
                        else
                            Sg[i] = Ex(Ttg[i] + 1, Ttg[i + 1], hg);

                        if (Ttb[i] == 0)
                            Sb[i] = Ex(Ttb[i], Ttb[i + 1], hb);
                        else
                            Sb[i] = Ex(Ttb[i] + 1, Ttb[i + 1], hb);
                    }
                    Jr = Sr[0] + Sr[1];
                    Jg = Sg[0] + Sg[1];
                    Jb = Sb[0] + Sb[1];

                    if (Jr > jrmax) { jrmax = Jr; kr = t; }
                    if (Jg > jgmax) { jgmax = Jg; kg = t; }
                    if (Jb > jbmax) { jbmax = Jb; kb = t; }
                }


            }
            T[0] = kr; T[1] = kg; T[2] = kb;
        }





        double OtsuGenel(int[] Tr, double[] Hist)
        {
            int i, EsikSayisi = Tr.Length;
            int[] T = new int[EsikSayisi + 2];
            double[] zigma = new double[EsikSayisi + 1];
            T[0] = 0; T[EsikSayisi + 1] = 256;

            for (i = 0; i < EsikSayisi; i++)
            { T[i + 1] = Tr[i]; }

            for (i = 0; i < (EsikSayisi + 1); i++)
            { if (T[i] > T[i + 1])  return 0; }
            for (i = 0; i < (EsikSayisi + 1); i++)
            { zigma[i] = Zigx(T[i], T[i + 1] - 1, Hist); }
            return zigma.Sum();
        }

        double KapurGenel(int[] Tr, double[] Hist)
        {
            int i, EsikSayisi = Tr.Length;
            int[] T = new int[EsikSayisi + 2];
            double[] H = new double[EsikSayisi + 1];
            T[0] = 0; T[EsikSayisi + 1] = 256;

            for (i = 0; i < EsikSayisi; i++)
            { T[i + 1] = Tr[i]; }

            for (i = 0; i < (EsikSayisi + 1); i++)
            { if (T[i] > T[i + 1])  return 0; }
            for (i = 0; i < (EsikSayisi + 1); i++)
            { H[i] = Ex(T[i], T[i + 1] - 1, Hist); }
            return H.Sum();
        }

        public void OtsuKapurCoklu(Bitmap bmp1, int[] tr, int[] tg, int[] tb, int ftipi)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double Jr, Jg, Jb; double jrmax, jgmax, jbmax;
            int t1, t2, t3, EsikSayisi; EsikSayisi = tr.Length;
            int[] T = new int[EsikSayisi];
            Histogram(bmp1, hr, hg, hb);
            Jr = 0; Jg = 0; Jb = 0;
            jrmax = 0; jgmax = 0; jbmax = 0;

            if (EsikSayisi == 2)
            {
                for (t1 = 0; t1 < 256; t1++)
                {
                    for (t2 = t1 + 1; t2 < 256; t2++)
                    {
                        if (t2 > t1)
                        {
                            T[0] = t1; T[1] = t2;

                            if (ftipi == 1)
                            {
                                Jr = OtsuGenel(T, hr);
                                Jg = OtsuGenel(T, hg);
                                Jb = OtsuGenel(T, hb);
                            }
                            if (ftipi == 2)
                            {
                                Jr = KapurGenel(T, hr);
                                Jg = KapurGenel(T, hg);
                                Jb = KapurGenel(T, hb);
                            }

                            if (Jr > jrmax) { jrmax = Jr; tr[0] = t1; tr[1] = t2; }
                            if (Jg > jgmax) { jgmax = Jg; tg[0] = t1; tg[1] = t2; }
                            if (Jb > jbmax) { jbmax = Jb; tb[0] = t1; tb[1] = t2; }
                        }

                    }
                }
            }

            if (EsikSayisi == 3)
            {
                for (t1 = 0; t1 < 256; t1++)
                {
                    for (t2 = t1 + 1; t2 < 256; t2++)
                    {
                        for (t3 = t2 + 1; t3 < 256; t3++)
                        {
                            if (t2 > t1 && t3 > t2)
                            {
                                T[0] = t1; T[1] = t2; T[2] = t3;
                                if (ftipi == 1)
                                {
                                    Jr = OtsuGenel(T, hr);
                                    Jg = OtsuGenel(T, hg);
                                    Jb = OtsuGenel(T, hb);
                                }
                                if (ftipi == 2)
                                {
                                    Jr = KapurGenel(T, hr);
                                    Jg = KapurGenel(T, hg);
                                    Jb = KapurGenel(T, hb);
                                }
                                if (Jr > jrmax) { jrmax = Jr; tr[0] = t1; tr[1] = t2; tr[2] = t3; }
                                if (Jg > jgmax) { jgmax = Jg; tg[0] = t1; tg[1] = t2; tg[2] = t3; }
                                if (Jb > jbmax) { jbmax = Jb; tb[0] = t1; tb[1] = t2; tb[2] = t3; }
                            }
                        }
                    }
                }
            }

        }





        public Bitmap OtsuKapurEsik2(Bitmap bmp1, int[] tr, int[] tg, int[] tb, int ftipi)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double[,] Jr, Jg, Jb; Jr = new double[256, 256]; Jg = new double[256, 256]; Jb = new double[256, 256];
            int t1, t2, EsikSayisi; EsikSayisi = tr.Length;
            int[] T = new int[EsikSayisi];
            double jrmax, jgmax, jbmax;
            Histogram(bmp1, hr, hg, hb);

            jrmax = 0; jgmax = 0; jbmax = 0;

            for (t1 = 0; t1 < 256; t1++)
            {
                for (t2 = 0; t2 < 256; t2++)
                {
                    if (t2 > t1)
                    {
                        if (ftipi == 1)
                        {
                            T[0] = t1; T[1] = t2; Jr[t1, t2] = OtsuGenel(T, hr);
                            T[0] = t1; T[1] = t2; Jg[t1, t2] = OtsuGenel(T, hg);
                            T[0] = t1; T[1] = t2; Jb[t1, t2] = OtsuGenel(T, hb);

                            if (Jr[t1, t2] > jrmax) { jrmax = Jr[t1, t2]; }
                        }

                        if (ftipi == 2)
                        {
                            T[0] = t1; T[1] = t2; Jr[t1, t2] = KapurGenel(T, hr);
                            T[0] = t1; T[1] = t2; Jg[t1, t2] = KapurGenel(T, hg);
                            T[0] = t1; T[1] = t2; Jb[t1, t2] = KapurGenel(T, hb);

                        }

                    }
                    else
                    { Jr[t1, t2] = 0; Jg[t1, t2] = 0; Jb[t1, t2] = 0; }
                }
            }

            Histogram2DNax(Jr, T); tr[0] = T[0]; tr[1] = T[1];
            Histogram2DNax(Jg, T); tg[0] = T[0]; tg[1] = T[1];
            Histogram2DNax(Jb, T); tb[0] = T[0]; tb[1] = T[1];

           
            Bitmap bmp = new Bitmap(256, 256);
            for (t1 = 0; t1 < 256; t1++)
            {
                for (t2 = 0; t2 < 256; t2++)
                {
                    if (jrmax < 0) { jrmax = 1; }

                    double oran = Jr[t1, t2] / jrmax;
                    int r = (int)(255 * oran);

                    Color p9; p9 = Color.FromArgb(r, r, r);
                    bmp.SetPixel(t1, t2, p9);

                    if (r == 255)
                    { p9 = Color.FromArgb(0, 255, 255); bmp.SetPixel(t1, t2, p9); }

                    for (int i = 0; i <= 51; i++)
                    {
                        if (r == i * 5)
                        {
                            p9 = Color.FromArgb(0, 255, 0);
                            bmp.SetPixel(t1, t2, p9);
                        }
                    }

                    if (t1 > t2)
                    { p9 = Color.FromArgb(255, 0, 0); bmp.SetPixel(t1, t2, p9); }

                }
            }

            return bmp;
        }

        public void OtsuKapurEsik3(Bitmap bmp1, int[] tr, int[] tg, int[] tb, int ftipi)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            double[, ,] Jr, Jg, Jb; Jr = new double[256, 256, 256]; Jg = new double[256, 256, 256]; Jb = new double[256, 256, 256];
            int t1, t2, t3, EsikSayisi; EsikSayisi = tr.Length;
            int[] T = new int[EsikSayisi];

            Histogram(bmp1, hr, hg, hb);

            for (t1 = 0; t1 < 256; t1++)
            {
                for (t2 = 0; t2 < 256; t2++)
                {
                    for (t3 = 0; t3 < 256; t3++)
                    {
                        if (t2 > t1 && t3 > t2)
                        {
                            if (ftipi == 1)
                            {
                                T[0] = t1; T[1] = t2; T[2] = t3; Jr[t1, t2, t3] = OtsuGenel(T, hr);
                                T[0] = t1; T[1] = t2; T[2] = t3; Jg[t1, t2, t3] = OtsuGenel(T, hg);
                                T[0] = t1; T[1] = t2; T[2] = t3; Jb[t1, t2, t3] = OtsuGenel(T, hb);
                            }

                            if (ftipi == 2)
                            {
                                T[0] = t1; T[1] = t2; T[2] = t3; Jr[t1, t2, t3] = KapurGenel(T, hr);
                                T[0] = t1; T[1] = t2; T[2] = t3; Jg[t1, t2, t3] = KapurGenel(T, hg);
                                T[0] = t1; T[1] = t2; T[2] = t3; Jb[t1, t2, t3] = KapurGenel(T, hb);
                            }
                        }
                        else
                        { Jr[t1, t2, t3] = 0; Jg[t1, t2, t3] = 0; Jb[t1, t2, t3] = 0; }
                    }
                }
            }

            Histogram3DNax(Jr, T); tr[0] = T[0]; tr[1] = T[1]; tr[2] = T[2];
            Histogram3DNax(Jg, T); tg[0] = T[0]; tg[1] = T[1]; tg[2] = T[2];
            Histogram3DNax(Jb, T); tb[0] = T[0]; tb[1] = T[1]; tb[2] = T[2];
        }




        public void MeanMultiEsik2(Bitmap bmp1, int[] tr, int[] tg, int[] tb)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            int t1, t2, t3;

            Histogram(bmp1, hr, hg, hb);

            t2 = Mx(0, 255, hr);
            t1 = Mx(0, t2 - 1, hr);
            t3 = Mx(t2, 255, hr);
            tr[0] = (int)t1; tr[1] = (int)t3;

            t2 = Mx(0, 255, hg);
            t1 = Mx(0, t2 - 1, hg);
            t3 = Mx(t2, 255, hg);
            tg[0] = (int)t1; tg[1] = (int)t3;

            t2 = Mx(0, 255, hb);
            t1 = Mx(0, t2 - 1, hb);
            t3 = Mx(t2, 255, hb);
            tb[0] = (int)t1; tb[1] = (int)t3;
        }


        public void MeanMultiEsik3(Bitmap bmp1, int[] tr, int[] tg, int[] tb)
        {
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            int t1, t2, t3;

            Histogram(bmp1, hr, hg, hb);

            t2 = Mx(0, 255, hr);
            t1 = Mx(0, t2 - 1, hr);
            t3 = Mx(t2, 255, hr);
            tr[0] = (int)t1; tr[1] = (int)t2; tr[2] = (int)t3;

            t2 = Mx(0, 255, hg);
            t1 = Mx(0, t2 - 1, hg);
            t3 = Mx(t2, 255, hg);
            tg[0] = (int)t1; tg[1] = (int)t2; tg[2] = (int)t3;

            t2 = Mx(0, 255, hb);
            t1 = Mx(0, t2 - 1, hb);
            t3 = Mx(t2, 255, hb);
            tb[0] = (int)t1; tb[1] = (int)t2; tb[2] = (int)t3;
        }



        public Bitmap MakeMultiEsikGray(Bitmap bmp1, int[,] label, int[] tr, int renktipi)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone(); Color p9, cmin, cmax, cort;
            double[] hr; hr = new double[256]; double m0;
            int i, j, k, n, t, a, x, y, q1, EsikSayisi, PasifEsikSayisi, ClusterSayisi;
            EsikSayisi = tr.Length; PasifEsikSayisi = EsikSayisi + 2;
            ClusterSayisi = (int)Math.Pow((EsikSayisi + 1), 3);
            int[] Tr = new int[PasifEsikSayisi];
            int[] np = new int[ClusterSayisi + 1];
            Color[] cn = new Color[ClusterSayisi + 1];

            for (k = 0; k < tr.Length; k++)
            { Tr[1 + k] = tr[k]; }
            Tr[0] = 0; Tr[PasifEsikSayisi - 1] = 256;
            for (x = 0; x < bmp1.Width; x++)
            {   for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            
            for (i = 0; i < ClusterSayisi; i++)
            { np[i] = 0; hr[i] = 0; }
            cmin = Color.FromArgb(255, 255, 255); cmax = Color.FromArgb(255, 255, 255); cort = Color.FromArgb(0, 0, 0);
            for (x = 0; x < bmp1.Width; x++)
            { for (y = 0; y < bmp1.Height; y++)
                {   p9 = bmp1.GetPixel(x, y); a = 0;
                    for (t = 0; t < (Tr.Length - 1); t++)
                    { if (p9.R >= Tr[t] && p9.R < Tr[t + 1]) a = t; }
                    n = a; label[x, y] = n; hr[n] += p9.R; np[n] += 1;
                    q1 = Tr[a]; if (q1 > 255) q1 = 255;
                    cmin = Color.FromArgb(q1, q1, q1);
                    q1 = Tr[a + 1]; if (q1 > 255) q1 = 255;
                    cmax = Color.FromArgb(q1, q1, q1);
                    cort = Color.FromArgb((int)((cmin.R + cmax.R) / 2), (int)((cmin.R + cmax.R) / 2), (int)((cmin.R + cmax.R) / 2));
                    if (renktipi == 1) bmp2.SetPixel(x, y, cmin);
                    if (renktipi == 2) bmp2.SetPixel(x, y, cort);
                    if (renktipi == 3) bmp2.SetPixel(x, y, cmax);
                }
            }

            if (renktipi == 4)
            {   m0 = 0;
                for (i = 0; i < ClusterSayisi; i++)
                {   if (np[i] > 0) m0 = hr[i] / np[i];
                    cn[i] = Color.FromArgb((int)m0, (int)m0, (int)m0);
                }

                for (x = 0; x < bmp1.Width; x++)
                { for (y = 0; y < bmp1.Height; y++)
                    { n = label[x, y]; bmp2.SetPixel(x, y, cn[n]); }
                }
            }
            return bmp2;
        } 



        public void HistogramCompare(Bitmap bmp1, Bitmap bmp2, double[] SSIM)
        {
            double[] hr1, hg1, hb1; hr1 = new double[256]; hg1 = new double[256]; hb1 = new double[256];
            double[] hr2, hg2, hb2; hr2 = new double[256]; hg2 = new double[256]; hb2 = new double[256];

            double ssimr, ssimg, ssimb, t1, t2, t3; int i;


            Histogram(bmp1, hr1, hg1, hb1);
            Histogram(bmp2, hr2, hg2, hb2);

            t1 = 0; t2 = 0; t3 = 0;

            for (i = 0; i <= 255; i++)
            {
                t1 = t1 + hr1[i] * Math.Abs(hr1[i] - hr2[i]);
                t2 = t2 + hg1[i] * Math.Abs(hg1[i] - hg2[i]); ;
                t3 = t3 + hb1[i] * Math.Abs(hb1[i] - hb2[i]); ;
            }


            /*
            for (i = 0; i <= 255; i++)
            {
                t1 = t1 +  Math.Abs(hr1[i] - hr2[i])*Math.Abs(hr1[i] - hr2[i]);
                t2 = t2 +  Math.Abs(hg1[i] - hg2[i])*Math.Abs(hg1[i] - hg2[i]);
                t3 = t3 +  Math.Abs(hb1[i] - hb2[i])* Math.Abs(hb1[i] - hb2[i]); ;
            }
           */
            ssimr = 1 - Math.Sqrt(t1); ssimg = 1 - Math.Sqrt(t2); ssimb = 1 - Math.Sqrt(t3);
            SSIM[0] = ssimr; SSIM[1] = ssimg; SSIM[2] = ssimb;
        }


        public Bitmap Fusion(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap bmp3 = (Bitmap)bmp1.Clone();
            Color c1, c2, p9; int i, j; double q;
            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                    q =Math.Max(c1.R,c2.R);
                    p9 = Color.FromArgb((int)q, (int)q, (int)q);
                    bmp3.SetPixel(i, j, p9);
                }
            }
            return bmp3;
        }


        public Bitmap Benzer2(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap bmp3 = (Bitmap)bmp1.Clone();
            Color c1, c2, p9; int i, j; double q;
            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                    q = MemberShip3(c1, c2, 255, 2);
                    q = 255 * q;
                    p9 = Color.FromArgb((int)q, (int)q, (int)q);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }


        public Bitmap AbsDiff(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap bmp3 = (Bitmap)bmp1.Clone();
            Color c1, c2, p9; int i, j; double q;
            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                    // p9 = Color.FromArgb((int)Math.Abs(c1.R - c2.R), (int)Math.Abs(c1.G - c2.G), (int)Math.Abs(c1.B - c2.B));
                    p9 = Color.FromArgb((int)Math.Abs(c1.R - c2.R), (int)Math.Abs(c1.R - c2.R), (int)Math.Abs(c1.R - c2.R));
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }




        public Bitmap ResimBenzer(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap bmp3 = (Bitmap)bmp1.Clone();
            int i, j; Color c1, c2;
            double m;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                    m = 255 * MemberShip3(c1, c2, 255, 1);
                    c2 = Color.FromArgb((int)m, (int)m, (int)m);
                    bmp3.SetPixel(i, j, c2);
                }
            }

            return bmp3;

        }


        /*
      public Bitmap ResimBenzer(Bitmap bmp1, Bitmap bmp2)
      {    Bitmap bmp3 = (Bitmap)bmp1.Clone();
          int i, j; Color c1, c2;
          double m;        

          for (j = 0; j < bmp1.Height; j++)
          {
              for (i = 0; i < bmp1.Width; i++)
              {
                  c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                  m =255* MemberShip3(c1, c2, 255, 1);
                  c2 = Color.FromArgb((int)m, (int)m, (int)m);
                  bmp3.SetPixel(i, j, c2);                  
              }
          }

          return bmp3;
         
      }
       * */


        public double Correlation(Bitmap bmp1, Bitmap bmp2)
        {
            int x, y, m1, m2; Color p9, c2;
            double q1, q2, q3, s;
            int[] T = new int[3];

            getMean(bmp1, T); m1 = T[0];
            getMean(bmp2, T); m2 = T[0];


            q1 = 0; q2 = 0; q3 = 0;
            for (y = 0; y < bmp2.Height; y++)
            {
                for (x = 0; x < bmp2.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    c2 = bmp2.GetPixel(x, y);

                    s = (double)((p9.R - m1) * (c2.R - m2));
                    q1 = q1 + s / (255 * 255);

                    s = (double)((p9.R - m1) * (p9.R - m1));
                    q2 = q2 + s / (255 * 255);

                    s = (double)((c2.R - m2) * (c2.R - m2));
                    q3 = q3 + s / (255 * 255);
                }
            }

            q1 = q1 / ((double)(bmp2.Height * bmp2.Width));
            q2 = q2 / ((double)(bmp2.Height * bmp2.Width));
            q3 = q3 / ((double)(bmp2.Height * bmp2.Width));
            //s = q1 / Math.Sqrt(q2 * q3);
            s = q1 / (Math.Sqrt(q2) * Math.Sqrt(q3));
            return s;
        }  

        public void GetMSE(Bitmap bmp1, Bitmap bmp2, double[] MSE, double[] PSNR)
        {
            int i, j; Color c1, c2;
            int q1, q2, q3;
            q1 = 0; q2 = 0; q3 = 0;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    c1 = bmp1.GetPixel(i, j); c2 = bmp2.GetPixel(i, j);
                    q1 = q1 + (c1.R - c2.R) * (c1.R - c2.R);
                    q2 = q2 + (c1.G - c2.G) * (c1.G - c2.G);
                    q3 = q3 + (c1.B - c2.B) * (c1.B - c2.B);
                }
            }

            MSE[0] = (double)q1 / (bmp1.Height * bmp1.Width);
            MSE[1] = (double)q2 / (bmp1.Height * bmp1.Width);
            MSE[2] = (double)q3 / (bmp1.Height * bmp1.Width);
            PSNR[0] = 20 * Math.Log10(255 / Math.Sqrt(MSE[0]));
            PSNR[1] = 20 * Math.Log10(255 / Math.Sqrt(MSE[1]));
            PSNR[2] = 20 * Math.Log10(255 / Math.Sqrt(MSE[2]));
        }

        public void GetSSIM(Bitmap bmp1, Bitmap bmp2, double[] SSIM)
        {
            double[] hr1, hg1, hb1; hr1 = new double[256]; hg1 = new double[256]; hb1 = new double[256];
            double[] hr2, hg2, hb2; hr2 = new double[256]; hg2 = new double[256]; hb2 = new double[256];
            double a1r, a1g, a1b, a2r, a2g, a2b, v1r, v1g, v1b, v2r, v2g, v2b, v12r, v12g, v12b;
            double ssimr, ssimg, ssimb, c1, c2, t1, t2, t3; int i, j; Color cc1, cc2; int q1, q2, q3;

            c1 = (0.01 * 255) * (0.01 * 255); c2 = (0.03 * 255) * (0.03 * 255);

            Histogram(bmp1, hr1, hg1, hb1);
            a1r = Mx(0, 255, hr1); a1g = Mx(0, 255, hg1); a1b = Mx(0, 255, hb1);
            t1 = 0; t2 = 0; t3 = 0;
            for (i = 0; i <= 255; i++)
            {
                t1 = t1 + (i - a1r) * (i - a1r) * hr1[i];
                t2 = t2 + (i - a1g) * (i - a1g) * hg1[i];
                t3 = t3 + (i - a1b) * (i - a1b) * hb1[i];
            } v1r = t1; v1g = t2; v1b = t3;

            Histogram(bmp2, hr2, hg2, hb2);

            a2r = Mx(0, 255, hr2); a2g = Mx(0, 255, hg2); a2b = Mx(0, 255, hb2);
            t1 = 0; t2 = 0; t3 = 0;
            for (i = 0; i <= 255; i++)
            {
                t1 = t1 + (i - a2r) * (i - a2r) * hr2[i];
                t2 = t2 + (i - a2g) * (i - a2g) * hg2[i];
                t3 = t3 + (i - a2b) * (i - a2b) * hb2[i];
            } v2r = t1; v2g = t2; v2b = t3;


            q1 = 0; q2 = 0; q3 = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    cc1 = bmp1.GetPixel(i, j); cc2 = bmp2.GetPixel(i, j);
                    q1 = q1 + (cc1.R - (int)a1r) * (cc2.R - (int)a2r);
                    q2 = q2 + (cc1.G - (int)a1g) * (cc2.G - (int)a2g);
                    q3 = q3 + (cc1.B - (int)a1b) * (cc2.B - (int)a2b);
                }
            }
            v12r = q1 / (bmp1.Height * bmp1.Width);
            v12g = q2 / (bmp1.Height * bmp1.Width);
            v12b = q3 / (bmp1.Height * bmp1.Width);

            ssimr = (2 * a1r * a2r + c1) * (2 * v12r + c2) / ((a1r * a1r + a2r * a2r + c1) * (v1r + v2r + c2));
            ssimg = (2 * a1g * a2g + c1) * (2 * v12g + c2) / ((a1g * a1g + a2g * a2g + c1) * (v1g + v2g + c2));
            ssimb = (2 * a1b * a2b + c1) * (2 * v12b + c2) / ((a1b * a1b + a2b * a2b + c1) * (v1b + v2b + c2));
            SSIM[0] = ssimr; SSIM[1] = ssimg; SSIM[2] = ssimb;

        }



        public void GetPCC(Bitmap bmp1, Bitmap bmp2, double[] Pcc)
        {
            int x, y, m1, m2; Color p9, c2;
            double q1, q2, q3, s;
            int[] T = new int[3];

            getMean(bmp1, T); m1 = T[0];
            getMean(bmp2, T); m2 = T[0];


            q1 = 0; q2 = 0; q3 = 0; s = 0;
            for (y = 0; y < bmp2.Height; y++)
            {
                for (x = 0; x < bmp2.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    c2 = bmp2.GetPixel(x, y);

                    s = (double)((p9.R - m1) * (c2.R - m2));
                    q1 = q1 + s / (255 * 255);

                    s = (double)((p9.R - m1) * (p9.R - m1));
                    q2 = q2 + s / (255 * 255);

                    s = (double)((c2.R - m2) * (c2.R - m2));
                    q3 = q3 + s / (255 * 255);
                }
            }

            q1 = q1 / ((double)(bmp2.Height * bmp2.Width));
            q2 = q2 / ((double)(bmp2.Height * bmp2.Width));
            q3 = q3 / ((double)(bmp2.Height * bmp2.Width));
            //s = q1 / Math.Sqrt(q2 * q3);
            s = q1 / (Math.Sqrt(q2) * Math.Sqrt(q3));

            Pcc[0] = s;


            q1 = 0; q2 = 0; q3 = 0; s = 0;
            for (y = 0; y < bmp2.Height; y++)
            {
                for (x = 0; x < bmp2.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    c2 = bmp2.GetPixel(x, y);

                    s = (double)((p9.G - m1) * (c2.G - m2));
                    q1 = q1 + s / (255 * 255);

                    s = (double)((p9.G - m1) * (p9.G - m1));
                    q2 = q2 + s / (255 * 255);

                    s = (double)((c2.G - m2) * (c2.G - m2));
                    q3 = q3 + s / (255 * 255);
                }
            }

            q1 = q1 / ((double)(bmp2.Height * bmp2.Width));
            q2 = q2 / ((double)(bmp2.Height * bmp2.Width));
            q3 = q3 / ((double)(bmp2.Height * bmp2.Width));
            //s = q1 / Math.Sqrt(q2 * q3);
            s = q1 / (Math.Sqrt(q2) * Math.Sqrt(q3));

            Pcc[1] = s;



            q1 = 0; q2 = 0; q3 = 0; s = 0;
            for (y = 0; y < bmp2.Height; y++)
            {
                for (x = 0; x < bmp2.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    c2 = bmp2.GetPixel(x, y);

                    s = (double)((p9.B - m1) * (c2.B - m2));
                    q1 = q1 + s / (255 * 255);

                    s = (double)((p9.B - m1) * (p9.B - m1));
                    q2 = q2 + s / (255 * 255);

                    s = (double)((c2.B - m2) * (c2.B - m2));
                    q3 = q3 + s / (255 * 255);
                }
            }

            q1 = q1 / ((double)(bmp2.Height * bmp2.Width));
            q2 = q2 / ((double)(bmp2.Height * bmp2.Width));
            q3 = q3 / ((double)(bmp2.Height * bmp2.Width));
            //s = q1 / Math.Sqrt(q2 * q3);
            s = q1 / (Math.Sqrt(q2) * Math.Sqrt(q3));

            Pcc[2] = s;

        }


     
        public Bitmap Laplace(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double dq1, dq2, dq3;
            double[] wx;
            wx = new double[9];
            int i, j, b, x, y;

            wx[0] = 0; wx[1] = -1; wx[2] = 0;
            wx[3] = -1; wx[8] = 4; wx[4] = -1;
            wx[5] = 0; wx[6] = -1; wx[7] = 0;



            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }


                    dq1 = 0; dq2 = 0; dq3 = 0;
                    for (b = 0; b <= 8; b++)
                    {
                        dq1 = dq1 + rnkkom[b].R * wx[b] * resim[b];
                        dq2 = dq2 + rnkkom[b].G * wx[b] * resim[b];
                        dq3 = dq3 + rnkkom[b].B * wx[b] * resim[b];

                    }


                    dq1 = Math.Abs(dq1 / 4);
                    dq2 = Math.Abs(dq2 / 4);
                    dq3 = Math.Abs(dq3 / 4);

                    p9 = Color.FromArgb((int)dq1, (int)dq2, (int)dq3);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }


        public Bitmap GradientX(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double q1x, q1y, dq1;
            double[] wx;
            wx = new double[9];
            int i, j, b, x, y;

            wx[0] = -1; wx[1] = -2; wx[2] = -1;
            wx[3] = 0; wx[8] = 0; wx[4] = 0;
            wx[5] = 1; wx[6] = 2; wx[7] = 1;

            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }
                    q1x = 0; q1y = 0;
                    for (b = 0; b <= 8; b++)
                    { q1x = q1x + rnkkom[b].R * wx[b] * resim[b]; }
                    dq1 = Math.Abs(q1x) / 4;
                    p9 = Color.FromArgb((int)dq1, (int)dq1, (int)dq1);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }


        public Bitmap GradientY(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double q1x, q1y, dq1;
            double[] wy;
            wy = new double[9];
            int i, j, b, x, y;

            wy[0] = -1; wy[1] = 0; wy[2] = 1;
            wy[3] = -2; wy[8] = 0; wy[4] = 2;
            wy[5] = -1; wy[6] = 0; wy[7] = 1;

            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }
                    q1x = 0; q1y = 0;
                    for (b = 0; b <= 8; b++)
                    { q1y = q1y + rnkkom[b].R * wy[b] * resim[b]; }
                    dq1 = Math.Abs(q1y) / 4;
                    p9 = Color.FromArgb((int)dq1, (int)dq1, (int)dq1);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }

        public Bitmap Gradient(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double q1x, q2x, q3x, q1y, q2y, q3y, dq1, dq2, dq3;
            double[] wx, wy;
            wx = new double[9];
            wy = new double[9];
            int i, j, b, x, y;

            wx[0] = -1; wx[1] = -2; wx[2] = -1;
            wx[3] = 0; wx[8] = 0; wx[4] = 0;
            wx[5] = 1; wx[6] = 2; wx[7] = 1;

            wy[0] = -1; wy[1] = 0; wy[2] = 1;
            wy[3] = -2; wy[8] = 0; wy[4] = 2;
            wy[5] = -1; wy[6] = 0; wy[7] = 1;



            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }


                    q1x = 0; q2x = 0; q3x = 0; q1y = 0; q2y = 0; q3y = 0;
                    for (b = 0; b <= 8; b++)
                    {
                        q1x = q1x + rnkkom[b].R * wx[b] * resim[b];
                        q2x = q2x + rnkkom[b].G * wx[b] * resim[b];
                        q3x = q3x + rnkkom[b].B * wx[b] * resim[b];

                        q1y = q1y + rnkkom[b].R * wy[b] * resim[b];
                        q2y = q2y + rnkkom[b].G * wy[b] * resim[b];
                        q3y = q3y + rnkkom[b].B * wy[b] * resim[b];
                    }


                    dq1 = Math.Sqrt(q1x * q1x + q1y * q1y) / 5.6568;
                    dq2 = Math.Sqrt(q2x * q2x + q2y * q2y) / 5.6568;
                    dq3 = Math.Sqrt(q3x * q3x + q3y * q3y) / 5.6568;

                    p9 = Color.FromArgb((int)dq1, (int)dq2, (int)dq3);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }


        public Bitmap GradientAci(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double q1x, q2x, q3x, q1y, q2y, q3y, dq1, dq2, dq3;
            double[] wx, wy;
            wx = new double[9];
            wy = new double[9];
            int i, j, b, x, y;

            wx[0] = -1; wx[1] = -2; wx[2] = -1;
            wx[3] = 0; wx[8] = 0; wx[4] = 0;
            wx[5] = 1; wx[6] = 2; wx[7] = 1;

            wy[0] = -1; wy[1] = 0; wy[2] = 1;
            wy[3] = -2; wy[8] = 0; wy[4] = 2;
            wy[5] = -1; wy[6] = 0; wy[7] = 1;



            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }


                    q1x = 0; q2x = 0; q3x = 0; q1y = 0; q2y = 0; q3y = 0;
                    for (b = 0; b <= 8; b++)
                    {
                        q1x = q1x + rnkkom[b].R * wx[b] * resim[b];
                        q2x = q2x + rnkkom[b].G * wx[b] * resim[b];
                        q3x = q3x + rnkkom[b].B * wx[b] * resim[b];

                        q1y = q1y + rnkkom[b].R * wy[b] * resim[b];
                        q2y = q2y + rnkkom[b].G * wy[b] * resim[b];
                        q3y = q3y + rnkkom[b].B * wy[b] * resim[b];
                    }


                    double alfa, edgeDirection; ;
                    if (q1x == 0)
                    {
                        if (q1y == 0) { alfa = 0.0; }
                        else if (q1y < 0) { q1y = -q1y; alfa = 90.0; }
                        else alfa = 90.0;
                    }
                    else if (q1x < 0 && q1y > 0)
                    { q1x = -q1x; alfa = 180 - ((Math.Atan((double)(q1y) / (double)(q1x))) * (180 / Math.PI)); }
                    else if (q1x > 0 && q1y < 0)
                    { q1y = -q1y; alfa = 180 - ((Math.Atan((double)(q1y) / (double)(q1x))) * (180 / Math.PI)); }
                    else
                    { alfa = (Math.Atan((double)(q1y) / (double)(q1x))) * (180 / Math.PI); }

                    if (alfa < 22.5)
                    { edgeDirection = 0; }
                    else if (alfa < 67.5)
                    { edgeDirection = 45; }
                    else if (alfa < 112.5)
                    { edgeDirection = 90; }
                    else if (alfa < 157.5)
                    { edgeDirection = 135; }
                    else { edgeDirection = 0; }
                    dq1 = edgeDirection;


                    if (q2x == 0)
                    {
                        if (q2y == 0) { alfa = 0.0; }
                        else if (q2y < 0) { q2y = -q2y; alfa = 90.0; }
                        else alfa = 90.0;
                    }
                    else if (q2x < 0 && q2y > 0)
                    { q2x = -q2x; alfa = 180 - ((Math.Atan((double)(q2y) / (double)(q2x))) * (180 / Math.PI)); }
                    else if (q2x > 0 && q2y < 0)
                    { q2y = -q2y; alfa = 180 - ((Math.Atan((double)(q2y) / (double)(q2x))) * (180 / Math.PI)); }
                    else
                    { alfa = (Math.Atan((double)(q2y) / (double)(q2x))) * (180 / Math.PI); }
                    if (alfa < 22.5)
                    { edgeDirection = 0; }
                    else if (alfa < 67.5)
                    { edgeDirection = 45; }
                    else if (alfa < 112.5)
                    { edgeDirection = 90; }
                    else if (alfa < 157.5)
                    { edgeDirection = 135; }
                    else { edgeDirection = 0; }
                    dq2 = edgeDirection;

                    if (q3x == 0)
                    {
                        if (q3y == 0) { alfa = 0.0; }
                        else if (q3y < 0) { q3y = -q3y; alfa = 90.0; }
                        else alfa = 90.0;
                    }
                    else if (q3x < 0 && q3y > 0)
                    { q3x = -q3x; alfa = 180 - ((Math.Atan((double)(q3y) / (double)(q3x))) * (180 / Math.PI)); }
                    else if (q3x > 0 && q3y < 0)
                    { q3y = -q3y; alfa = 180 - ((Math.Atan((double)(q3y) / (double)(q3x))) * (180 / Math.PI)); }
                    else
                    { alfa = (Math.Atan((double)(q3y) / (double)(q3x))) * (180 / Math.PI); }
                    if (alfa < 22.5)
                    { edgeDirection = 0; }
                    else if (alfa < 67.5)
                    { edgeDirection = 45; }
                    else if (alfa < 112.5)
                    { edgeDirection = 90; }
                    else if (alfa < 157.5)
                    { edgeDirection = 135; }
                    else { edgeDirection = 0; }
                    dq3 = edgeDirection;

                    p9 = Color.FromArgb((int)dq1, (int)dq2, (int)dq3);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }


        public Bitmap CannyEdge(Bitmap bmp1, double Tl, double Th)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Bitmap bmp3 = (Bitmap)bmp1.Clone();
            Bitmap bmp4 = (Bitmap)bmp1.Clone();


            Color p9, c1;
            double te1, te2, te3, edgeDirection, leftPixel, rightPixel;
            int[] resim; resim = new int[9];
            double[] grad1, grad2, grad3; grad1 = new double[9]; grad2 = new double[9]; grad3 = new double[9];
            int i, j, b, x, y;

            bmp3 = Gradient(bmp1);
            bmp4 = GradientAci(bmp1);
            edgeDirection = 0;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {

                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        {
                            c1 = bmp3.GetPixel(x, y);
                            grad1[b] = (double)c1.R; grad2[b] = (double)c1.G; grad3[b] = (double)c1.B;
                        }
                    }

                    p9 = bmp4.GetPixel(i, j); edgeDirection = (double)p9.R;

                    if (edgeDirection == 0)
                    { leftPixel = grad1[1]; rightPixel = grad1[6]; }
                    else if (edgeDirection == 45)
                    { leftPixel = grad1[0]; rightPixel = grad1[7]; }
                    else if (edgeDirection == 90)
                    { leftPixel = grad1[3]; rightPixel = grad1[4]; }
                    else
                    { leftPixel = grad1[5]; rightPixel = grad1[2]; }


                    if (grad1[8] < leftPixel || grad1[8] < rightPixel)
                    { te1 = 0; }
                    else
                    {
                        if (grad1[8] >= Th)
                        { te1 = 255; }
                        else if (grad1[8] < Tl)
                        { te1 = 0; }
                        else
                        {
                            if (grad1[0] > Th || grad1[1] > Th || grad1[2] > Th || grad1[3] > Th
                             || grad1[4] > Th || grad1[5] > Th || grad1[6] > Th || grad1[7] > Th)
                            { te1 = 255; }
                            else { te1 = 0; }
                        }

                    }

                    edgeDirection = (double)p9.G;

                    if (edgeDirection == 0)
                    { leftPixel = grad2[1]; rightPixel = grad2[6]; }
                    else if (edgeDirection == 45)
                    { leftPixel = grad2[0]; rightPixel = grad2[7]; }
                    else if (edgeDirection == 90)
                    { leftPixel = grad2[3]; rightPixel = grad2[4]; }
                    else
                    { leftPixel = grad2[5]; rightPixel = grad2[2]; }


                    if (grad2[8] < leftPixel || grad2[8] < rightPixel)
                    { te2 = 0; }
                    else
                    {
                        if (grad2[8] >= Th)
                        { te2 = 255; }
                        else if (grad2[8] < Tl)
                        { te2 = 0; }
                        else
                        {
                            if (grad2[0] > Th || grad2[1] > Th || grad2[2] > Th || grad2[3] > Th
                             || grad2[4] > Th || grad2[5] > Th || grad2[6] > Th || grad2[7] > Th)
                            { te2 = 255; }
                            else { te2 = 0; }
                        }

                    }


                    edgeDirection = (double)p9.B;

                    if (edgeDirection == 0)
                    { leftPixel = grad3[1]; rightPixel = grad3[6]; }
                    else if (edgeDirection == 45)
                    { leftPixel = grad3[0]; rightPixel = grad3[7]; }
                    else if (edgeDirection == 90)
                    { leftPixel = grad3[3]; rightPixel = grad3[4]; }
                    else
                    { leftPixel = grad3[5]; rightPixel = grad3[2]; }


                    if (grad3[8] < leftPixel || grad3[8] < rightPixel)
                    { te3 = 0; }
                    else
                    {
                        if (grad3[8] >= Th)
                        { te3 = 255; }
                        else if (grad3[8] < Tl)
                        { te3 = 0; }
                        else
                        {
                            if (grad3[0] > Th || grad3[1] > Th || grad3[2] > Th || grad3[3] > Th
                             || grad3[4] > Th || grad3[5] > Th || grad3[6] > Th || grad3[7] > Th)
                            { te3 = 255; }
                            else { te3 = 0; }
                        }

                    }

                    if ((te1 == 255) && (te2 == 255) && (te3 == 255))
                    { te1 = 255; te2 = 255; te3 = 255; }
                    else { te1 = 0; te2 = 0; te3 = 0; }

                    p9 = Color.FromArgb((int)te1, (int)te2, (int)te3);
                    bmp2.SetPixel(i, j, p9);

                }
            }
            return bmp2;
        }


        public Bitmap FuzzyGradient(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            Color[] rnkkom; Color p9;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double qx, qy, dq1;//dq2,dq3;
            int i, j, b, x, y;


            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }



                    qx = FuzzySobel(rnkkom[0].R, rnkkom[1].R, rnkkom[2].R, rnkkom[5].R, rnkkom[6].R, rnkkom[7].R);
                    qy = FuzzySobel(rnkkom[0].R, rnkkom[3].R, rnkkom[5].R, rnkkom[2].R, rnkkom[4].R, rnkkom[7].R);
                    dq1 = Math.Sqrt(qx * qx + qy * qy) / 1.41;
                    /*
                     qx = FuzzySobel(rnkkom[0].G, rnkkom[1].G, rnkkom[2].G,rnkkom[5].G, rnkkom[6].G, rnkkom[7].G);
                     qy = FuzzySobel(rnkkom[0].G, rnkkom[3].G, rnkkom[5].G,rnkkom[2].G, rnkkom[4].G, rnkkom[7].G);
                     dq2 = Math.Sqrt(qx * qx + qy * qy) / 1.41;

                     qx = FuzzySobel(rnkkom[0].B, rnkkom[1].B, rnkkom[2].B,rnkkom[5].B, rnkkom[6].B, rnkkom[7].B);
                     qy = FuzzySobel(rnkkom[0].B, rnkkom[3].B, rnkkom[5].B,rnkkom[2].B, rnkkom[4].B, rnkkom[7].B);
                     dq3 = Math.Sqrt(qx * qx + qy * qy) / 1.41;
                     */

                    p9 = Color.FromArgb((int)dq1, (int)dq1, (int)dq1);
                    bmp2.SetPixel(i, j, p9);
                }
            }
            return bmp2;
        }



        public void haarmy1d(double[] vec, int n)
        {
            int i = 0;
            int w = 0;
            double[] vecp;
            vecp = new double[n];
            for (i = 0; i < n; i++)
                vecp[i] = 0;


            w = n / 2;
            for (i = 0; i < w; i++)
            {
                vecp[i] = (vec[2 * i] + vec[2 * i + 1]) / Math.Sqrt(2.0);
                vecp[i + w] = (vec[2 * i] - vec[2 * i + 1]) / Math.Sqrt(2.0);
            }

            for (i = 0; i < (w * 2); i++)
            {
                if (i < w) vec[i] = 255 * vecp[i] / 362;
                else
                {
                    if (vecp[i] >= 0) vec[i] = 255 * vecp[i] / 362; else vec[i] = -255 * vecp[i] / 362;
                    //  vec[i] = 255*(180+vecp[i])/360;
                    // vec[i] = 255 - vec[i];
                }
            }

            // Array.Clear(vecp,0,n);
        }

        public void haarmy2D(double[,] matrix, int W, int H)
        {
            double[] temp_row;
            double[] temp_col;
            temp_row = new double[W];
            temp_col = new double[H];

            int i = 0, j = 0;

            for (i = 0; i < W; i++)
            {
                for (j = 0; j < H; j++)
                { temp_col[j] = matrix[i, j]; }
                haarmy1d(temp_col, H);
                for (j = 0; j < H; j++)
                { matrix[i, j] = temp_col[j]; }
            }

            for (j = 0; j < H; j++)
            {
                for (i = 0; i < W; i++)
                { temp_row[i] = matrix[i, j]; }
                haarmy1d(temp_row, W);
                for (i = 0; i < W; i++)
                { matrix[i, j] = temp_row[i]; }
            }
            //delete[] temp_row;
            // delete[] temp_col;
        }





        public Bitmap Haar(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double[,] data1;
            double[,] data2;
            double[,] data3;

            data1 = new double[bmp1.Width, bmp1.Height];
            data2 = new double[bmp1.Width, bmp1.Height];
            data3 = new double[bmp1.Width, bmp1.Height];
            Color p1;
            int x, y, r, g, b;
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { data1[x, y] = 0; data2[x, y] = 0; data3[x, y] = 0; }
            }


            for (int j = 0; j < bmp1.Height; j++)
            {
                for (int i = 0; i < bmp1.Width; i++)
                {
                    p1 = bmp1.GetPixel(i, j);
                    data1[i, j] = p1.R;
                    data2[i, j] = p1.G;
                    data3[i, j] = p1.B;
                }
            }

            haarmy2D(data1, bmp1.Width, bmp1.Height);
            haarmy2D(data2, bmp1.Width, bmp1.Height);
            haarmy2D(data3, bmp1.Width, bmp1.Height);

            for (int j = 0; j < bmp1.Height; j++)
            {
                for (int i = 0; i < bmp1.Width; i++)
                {
                    r = (int)data1[i, j];
                    g = (int)data2[i, j];
                    b = (int)data3[i, j];
                    p1 = Color.FromArgb(r, g, b);
                    bmp2.SetPixel(i, j, p1);
                }
            }
            return bmp2;
        }

        public Bitmap Kesal(Bitmap img, Rectangle d)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = bmp1.Clone(d, bmp1.PixelFormat);
            return (bmp2);
        }

        public Bitmap KesalHaar(Bitmap img, int bolge)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = new Bitmap(bmp1);
            bmp2 = Haar(bmp1);
            Bitmap bmph = new Bitmap(bmp1.Width / 2, bmp1.Height / 2);
            Bitmap bmpv = new Bitmap(bmph);
            Bitmap bmpd = new Bitmap(bmph);
            Bitmap bmpo = new Bitmap(bmph);

            Rectangle o = new Rectangle(0, 0, bmp1.Width / 2, bmp1.Height / 2);
            Rectangle h = new Rectangle(bmp1.Width / 2, 0, bmp1.Width / 2, bmp1.Height / 2);
            Rectangle v = new Rectangle(0, bmp1.Height / 2, bmp1.Width / 2, bmp1.Height / 2);
            Rectangle d = new Rectangle(bmp1.Width / 2, bmp1.Height / 2, bmp1.Width / 2, bmp1.Height / 2);
            if (bolge == 1)
                return bmpo = Kesal(bmp2, o);
            else if (bolge == 2)
                return bmph = Kesal(bmp2, h);
            else if (bolge == 3)
                return bmpv = Kesal(bmp2, v);
            else if (bolge == 4)
                return bmpd = Kesal(bmp2, d);
            else
                return bmp1;
        }  

        public Bitmap Hough(Bitmap bmp1)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int w = bmp1.Width;
            int h = bmp1.Height;
            int x, y, j;
            /* the middle of img is made the origin */
            double rmax = Math.Sqrt(((w / 2) * (w / 2)) + ((h / 2) * (h / 2)));
            double tetamax = Math.PI * 2;

            // step sizes
            double dr = rmax / (double)w;
            double dteta = tetamax / (double)h;

            int[,] A = new int[w * 2, h * 2]; // accumulator array

            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    if (bmp1.GetPixel(x, h - y - 1).B == 255) // pixel is white - h-y flips incoming img
                    {
                        // book claims it's j = 1, i think it should be j = 0
                        for (j = 0; j < h; j++)
                        {
                            double row = ((double)(x - (w / 2)) * Math.Cos(dteta * (double)j)) + ((double)(y - (h / 2)) * Math.Sin(dteta * (double)j));
                            // find index k of A closest to row
                            int k = (int)((row / rmax) * w);
                            if (k >= 0 && k < w) A[k, j]++;
                        }
                    }
                }
            }

            // find max of A, to normalize colors
            int amax = 0;
            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    if (A[x, y] > amax) amax = A[x, y];
                }
            }

            // make us a greyscale bitmap
            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    int b = 0;
                    if (amax != 0) b = (int)(((double)A[x, y] / (double)amax) * 255.0);
                    bmp2.SetPixel(x, y, System.Drawing.Color.FromArgb(b, b, b));
                }
            }

            return bmp2;
        }

        public Bitmap Hough2(Bitmap bmp1, int ro, int aci)
        {
            int W = ro; int H = aci;
            Bitmap bmp2 = new Bitmap(W, H);
            int x, y, teta;

            double rmax = Math.Sqrt(W * W + H * H);
            double tetamax = Math.PI;
            double dr = rmax / (double)W;
            double dteta = tetamax / (double)H;

            int[,] A = new int[W, H]; // accumulator array

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y).B == 255) // pixel is edge
                    {

                        for (teta = 0; teta < H; teta++)
                        {
                            double yaricap = (double)(x * Math.Cos(dteta * (double)teta)) + (double)(y * Math.Sin(dteta * (double)teta));
                            int r = (int)((yaricap / rmax) * W);   // find index k of A closest to row

                            if (r >= 0 && r < W)
                                A[r, teta] = A[r, teta] + 1;
                        }
                    }
                }
            }

            // find max of A, to normalize colors
            int amax = 0;
            for (x = 0; x < W; x++)
            {
                for (y = 0; y < H; y++)
                {
                    if (A[x, y] > amax) amax = A[x, y];
                }
            }

            // make us a greyscale bitmap
            for (x = 0; x < W; x++)
            {
                for (y = 0; y < H; y++)
                {
                    int b = 0;
                    if (amax != 0) b = (int)(((double)A[x, y] / (double)amax) * 255.0);
                    bmp2.SetPixel(x, y, System.Drawing.Color.FromArgb(0, b, 0));
                }
            }

            return bmp2;
        }

        public Bitmap Hough3(Bitmap bmp1, int aci, int ro)
        {
            int W = aci; int H = ro;
            Bitmap bmp2 = new Bitmap(W, H);
            int x, y, teta;

            x = bmp1.Width; y = bmp1.Height;
            double rmax = Math.Sqrt((double)(x * x + y * y));
            double tetamax = Math.PI;
            //  double dr = rmax / (double)H;
            double dteta = tetamax / (double)W;

            int[,] A = new int[W, H]; // accumulator array

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y).B == 255) // pixel is edge
                    {
                        for (teta = 0; teta < W; teta++)
                        {
                            double yaricap = (double)(x * Math.Cos(dteta * (double)teta)) + (double)(y * Math.Sin(dteta * (double)teta));
                            int r = (int)((yaricap / rmax) * H);   // find index k of A closest to row

                            if (r >= 0 && r < H)
                                A[teta, r] = A[teta, r] + 1;
                        }
                    }
                }
            }

            // find max of A, to normalize colors
            int amax = 0;
            for (x = 0; x < W; x++)
            {
                for (y = 0; y < H; y++)
                {
                    if (A[x, y] > amax) amax = A[x, y];
                }
            }

            // make us a greyscale bitmap
            for (x = 0; x < W; x++)
            {
                for (y = 0; y < H; y++)
                {
                    int b = 0;
                    if (amax != 0) b = (int)(((double)A[x, y] / (double)amax) * 255.0);
                    bmp2.SetPixel(x, y, System.Drawing.Color.FromArgb(b, b, b));
                }
            }

            return bmp2;
        }



        public Bitmap Hough4(Bitmap bmp1, int aci, int ro, int eksen)
        {
            int W = aci; int H = ro;
            Bitmap bmp2 = new Bitmap(W, H);
            int x, y, teta, r;
            double alfa, yaricap;
            x = bmp1.Width; y = bmp1.Height;
            double rmax = Math.Sqrt((double)(x * x + y * y));   //  double dr = rmax / (double)H;
            double tetamax = 180;
            double dteta = tetamax / (double)W;


            int[,] A = new int[W, H]; // accumulator array

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y).B == 255) // pixel is edge
                    {
                        for (teta = 0; teta < W; teta++)
                        {
                            alfa = (double)(dteta * teta); alfa = 3.14 * alfa / 180;
                            yaricap = (double)(x * Math.Cos(alfa)) + (double)(y * Math.Sin(alfa));
                            r = (int)((yaricap / rmax) * H / 2);   // find index k of A closest to row
                            r = H / 2 + r;
                            A[teta, r] = A[teta, r] + 1;
                        }
                    }
                }
            }





            // find max of A, to normalize colors
            int amax = 0;
            for (x = 0; x < W; x++)
            {
                for (y = 0; y < H; y++)
                {
                    if (A[x, y] > amax) amax = A[x, y];
                }
            }

            // make us a greyscale bitmap
            for (x = 0; x < W; x++)
            {
                for (y = 0; y < H; y++)
                {
                    int b = 0;
                    if (amax != 0) b = (int)(((double)A[x, y] / (double)amax) * 255.0);
                    bmp2.SetPixel(x, y, System.Drawing.Color.FromArgb(b, b, b));

                    if (eksen == 1)
                    {
                        if (x == W / 2 || y == H / 2)
                        { p9 = Color.FromArgb(0, 0, 255); bmp2.SetPixel(x, y, p9); }
                    }
                }
            }

            return bmp2;
        }


       


        public void RecursiveGrowing()
        {
            ThreadStart ts = new ThreadStart(SimpleWork);
            Thread T = new Thread(ts, 500000000);
            T.Start();
            Pixelsecimi = true;
        }



        public void SimpleWork()
        {
            int x, y, counter;

            for (x = 0; x < resim1.Width; x++)
            {
                for (y = 0; y < resim1.Height; y++)
                { data[x, y] = -1; }
            }

            counter = -1;
            for (x = 0; x < resim1.Width; x++)
            {
                for (y = 0; y < resim1.Height; y++)
                {
                    if (data[x, y] == -1)
                    {
                        counter = counter + 1;
                        c1 = resim1.GetPixel(x, y);
                        BolgeniBul(x, y, counter);

                    }
                }
            }

            MessageBox.Show(counter.ToString());

        }

        public void BolgeniBul(int x, int y, int counter)
        {
            double mem; int resim;

            if (data[x, y] < 0)
            {
                data[x, y] = counter;

                resim = resimdemi(x + 1, y, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x + 1, y] < 0)
                    {
                        c2 = resim1.GetPixel(x + 1, y);
                        mem = MemberShip3(c1, c2, 128, 3);
                        if (mem > memTresh) BolgeniBul(x + 1, y, counter);
                    }
                }

                resim = resimdemi(x + 1, y + 1, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x + 1, y + 1] < 0)
                    {
                        c2 = resim1.GetPixel(x + 1, y + 1);
                        mem = MemberShip3(c1, c2, 128, 3);
                        if (mem > memTresh) BolgeniBul(x + 1, y + 1, counter);
                    }
                }

                resim = resimdemi(x, y + 1, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x, y + 1] < 0)
                    {
                        c2 = resim1.GetPixel(x, y + 1);
                        mem = MemberShip3(c1, c2, 128, 3);
                        if (mem > memTresh) BolgeniBul(x, y + 1, counter);
                    }
                }

                resim = resimdemi(x - 1, y + 1, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x - 1, y + 1] < 0)
                    {
                        c2 = resim1.GetPixel(x - 1, y + 1);
                        mem = MemberShip3(c1, c2, 128, 2);
                        if (mem > memTresh) BolgeniBul(x - 1, y + 1, counter);
                    }
                }



                resim = resimdemi(x - 1, y, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x - 1, y] < 0)
                    {
                        c2 = resim1.GetPixel(x - 1, y);
                        mem = MemberShip3(c1, c2, 128, 2);
                        if (mem > memTresh) BolgeniBul(x - 1, y, counter);
                    }
                }


                resim = resimdemi(x - 1, y - 1, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x - 1, y - 1] < 0)
                    {
                        c2 = resim1.GetPixel(x - 1, y - 1);
                        mem = MemberShip3(c1, c2, 128, 3);
                        if (mem > memTresh) BolgeniBul(x - 1, y - 1, counter);
                    }
                }

                resim = resimdemi(x, y - 1, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x, y - 1] < 0)
                    {
                        c2 = resim1.GetPixel(x, y - 1);
                        mem = MemberShip3(c1, c2, 128, 2);
                        if (mem > memTresh) BolgeniBul(x, y - 1, counter);
                    }
                }

                resim = resimdemi(x + 1, y - 1, resim1.Width, resim1.Height);
                if (resim == 1)
                {
                    if (data[x + 1, y - 1] < 0)
                    {
                        c2 = resim1.GetPixel(x + 1, y - 1);
                        mem = MemberShip3(c1, c2, 128, 3);
                        if (mem > memTresh) BolgeniBul(x + 1, y - 1, counter);
                    }
                }



            }
        }


        public void SimpleWork2()
        {
            int x, y, counter; double mem;

            for (x = 0; x < resim1.Width; x++)
            {
                for (y = 0; y < resim1.Height; y++)
                { data[x, y] = -1; }
            }

            counter = 0;
            for (x = 0; x < resim1.Width; x++)
            {
                for (y = 0; y < resim1.Height; y++)
                {
                    c2 = resim1.GetPixel(x, y); mem = MemberShip3(c1, c2, 128, 2);
                    if (data[x, y] == -1 && mem > memTresh)
                    {
                        counter = counter + 1;
                        BolgeniBul(x, y, counter);
                    }
                }
            }
            MessageBox.Show(counter.ToString());
        }


        public Bitmap NesneSay(Bitmap bmp1, int xm, int ym)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            c1 = bmp1.GetPixel(xm, ym);
            ThreadStart ts2 = new ThreadStart(SimpleWork2);
            Thread T = new Thread(ts2, 500000000); T.Start();
            int y,x = 1;
            do
            {
                if ((T.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                {  x = 1; }
                else x = 0;
            } while (x != 0);

          
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (data[x, y] > 0) p9 = Color.FromArgb(0, 255, 0); else p9 = Color.FromArgb(0, 0, 0); //p9 = bmp1.GetPixel(x, y);
                    bmp2.SetPixel(x, y, p9);
                }
            }
           
            return bmp2;
           
           /*  Goruntu.c1 = bmp1.GetPixel(xm, ym);
             ThreadStart ts2 = delegate() { Goruntu.SimpleWork2(); };
             Thread T = new Thread(ts2, 500000000);  T.Start();
             x = 1;
             do
             {  if ((T.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                 { textBox1.Text = "running"; x = 1; }  else x = 0;  } while (x != 0);
              for (x = 0; x < bmp1.Width; x++)
             {  for (y = 0; y < bmp1.Height; y++)
                 {   if (Goruntu.data[x, y] >0)  p9 = Color.FromArgb(0, 0, 255);else p9 = bmp1.GetPixel(x, y);
                     bmp2.SetPixel(x, y, p9);
                 }
             }  pictureBox2.Image = bmp2;*/        
        }


        public Bitmap PointGrowing(Bitmap bmp1, int xm, int ym)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();

            int x, y, w, n = 5;
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { data[x, y] = -1; }
            }

            ThreadStart ts2 = delegate() { BolgeniBul(xm, ym, n); };
            Thread T = new Thread(ts2, 500000000); T.Start();
            x = 1;
            do
            {
                if ((T.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                { x = 1; }
                else x = 0;
            } while (x != 0);

            w = 0;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (data[x, y] == n)
                    { p9 = Color.FromArgb(0, 0, 255); w = w + 1; }
                    else p9 = bmp1.GetPixel(x, y);
                    bmp2.SetPixel(x, y, p9);
                }
            }
            return bmp2;
        }

        public Bitmap NesneSec(Bitmap bmp1, int xm, int ym)
        {   Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int x,y,n = data[xm, ym]; 
            for (x = 0; x < bmp1.Width; x++)
            {  for (y = 0; y < bmp1.Height; y++)
                {  if (data[x, y] == n)p9 = Color.FromArgb(0, 255, 0); else p9 = bmp1.GetPixel(x, y); 
                    bmp2.SetPixel(x, y, p9);
                }
            }           
            return bmp2;
        }

        public Bitmap NesneTani(Bitmap bmp1, int xm, int ym)
        {   Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int x, y, xmin, ymin, xmax, ymax, wp, hp ,n;
            n = data[xm, ym]; 

            xmin = xm; ymin = ym; xmax = xm; ymax = ym;
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (data[x, y] == n)
                    {   if (x < xmin) xmin = x; if (x > xmax) xmax = x;
                        if (y < ymin) ymin = y; if (y > ymax) ymax = y;
                    }
                }
            }


            wp = xmax - xmin; hp = ymax - ymin;
            Bitmap bmp3 = new Bitmap(wp, hp);         
            for (x = xmin; x < xmax; x++)
            {
                for (y = ymin; y < ymax; y++)
                {
                    if (data[x, y] == n)
                    { p9 = bmp1.GetPixel(x, y); 
                      //  p9 = Color.FromArgb(255, 0, 255); 
                    }   // p9 = bmp1.GetPixel(x, y);      
                    else p9 = Color.FromArgb(0, 0, 0);
                    bmp3.SetPixel(x - xmin, y - ymin, p9);
                }
            }
            return bmp3;
        }



        public Point AgirlikMerkeziBul(Bitmap img)
        {
            Point merkez = new Point(0,0);
            Color p9; int x, y,xc, yc, n;
           
            double xt, yt;

            xt = 0; yt = 0; n = 1; merkez.X = img.Width / 2; merkez.Y = img.Height/2;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        xt = xt + x * 255; yt = yt + y * 255; n++;
                    }
                }
            }

            xc = (int)(xt / (255 *n));
            yc = (int)(yt / (255 *n));
            merkez.X = xc; merkez.Y = yc;
            return merkez;
        }
        public double OryantasyonBul(Bitmap img)
        {
            double aci;
            int x, y, Ix, Iy, Ixy, n;
            Ix = 0; Iy = 0; Ixy = 0; n = 0;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        Ix = Ix + y * y * 255; 
                        Iy = Iy + x * x * 255; 
                        Ixy = Ixy + x * y * 255; 
                        n++;
                    }
                }
            }
            Ix = Ix / (255 * n);
            Iy = Iy / (255 * n);
            Ixy = Ixy / (255 * n);
            aci = 3.14 - 0.5 * Math.Atan((double)(2 * Ixy) / (double)(Ix - Iy));
            return aci;
        }
        public double OryantasyonBul2(Bitmap img)
        {
            double aci;
            int x, y, Ix, Iy, Ixy, n;
            Color p9; int xm, ym, h;
            Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;

            Ix = 0; 
            Iy = 0; 
            Ixy = 0; 
            n = 0;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        Ix = Ix + (y - ym) * (y - ym) * 255;
                        Iy = Iy + (x - xm) * (x - xm) * 255; 
                        //Ix = Ix + (x - xm) * (x - xm) * 255;
                        //Iy = Iy + (y - ym) * (y - ym) * 255;
                        Ixy = Ixy + (x - xm) * (y - ym) * 255; 
                        n++;
                    }
                }
            }
            Ix = Ix / (255 * n);
            Iy = Iy / (255 * n);
            Ixy =Ixy / (255 * n);
           
          
            if (Ix == Iy)
                aci = 3.14 / 2;
            else
                aci =-0.5 * Math.Atan((double)(2 * Ixy) / (double)(Ix - Iy));
                       
            return aci;
        }

        public Bitmap Oryantasyon2(Bitmap img)
        {
            Bitmap bmp2 = (Bitmap)img.Clone();
            Color p9; int x, y, xm, ym, h; double aci;
            Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;
            aci = OryantasyonBul2(img);
            bmp2 = CannyEdge(img, 10, 20);
            bmp2 = GetCom(bmp2, -1, -1, -1);
            // aci=3.14+aci;
            for (x = 0; x < img.Width; x++)
            {
                h = (int)(ym + Math.Tan(aci) * (x - xm));
                if (h > 0 && h < img.Height)
                { p9 = Color.FromArgb(255, 0, 0); bmp2.SetPixel(x, h, p9); }

                h = (int)(ym + Math.Tan(aci + 3.14 / 2) * (x - xm));
                if (h > 0 && h < img.Height)
                { p9 = Color.FromArgb(0, 0, 255); bmp2.SetPixel(x, h, p9); }

            }
            return bmp2;
        }
        public double OryantasyonBul3(Bitmap img)
        {
            double aci;
            int x, y, Ix, Iy, Ixy, n;
            Color p9; int xm, ym, h;
            Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;

            Ix = 0;
            Iy = 0;
            Ixy = 0;
            n = 0;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        /*
                       Ix = Ix + (y - ym) * (y - ym) * 255;
                        Iy = Iy + (x - xm) * (x - xm) * 255;
                        Ixy = Ixy + (x - xm) * (y - ym) * 255;
                        */

                       Ix = Ix + x*x* 255;
                       Iy = Iy + y*y*255;
                        Ixy = Ixy + x *y * 255;

                        n++;
                    }
                }
            }
            Ix = Ix / (255 * n);
            Iy = Iy / (255 * n);
            Ixy = Ixy / (255 * n);

          
            double q1x, q1y;

            q1y =(double)(2 * Ixy);
            q1x = (double)(Ix - Iy);

            if (q1x == 0)
            {
                if (q1y == 0) { aci = 0.0; }
                else if (q1y < 0) { q1y = -q1y; aci = 90.0; }
                else aci = 90.0;
            }
            else if (q1x < 0 && q1y > 0)
            { q1x = -q1x; aci = 180 - ((Math.Atan((double)(q1y) / (double)(q1x))) * (180 / Math.PI)); }
            else if (q1x > 0 && q1y < 0)
            { q1y = -q1y; aci = 180 - ((Math.Atan((double)(q1y) / (double)(q1x))) * (180 / Math.PI)); }
            else
            { aci = (Math.Atan((double)(q1y) / (double)(q1x))) * (180 / Math.PI); }

            aci = 0.5 * aci;
            return aci;
        }


    

        public Bitmap Oryantasyon3(Bitmap img)
        {
            Bitmap bmp2 = (Bitmap)img.Clone();
            Color p9; int x, y, xm, ym, h; double aci;
            Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;
            aci = OryantasyonBul3(img);
            bmp2 = CannyEdge(img, 10, 20);
            bmp2 = GetCom(bmp2, -1, -1, -1);
           
            for (x = 0; x < img.Width; x++)
            {
                h = (int)(ym + Math.Tan(aci * Math.PI / 180) * (x - xm));
                if (h > 0 && h < img.Height)
                { p9 = Color.FromArgb(255, 0, 0); bmp2.SetPixel(x, h, p9); }

              
                h = (int)(ym + Math.Tan((3.14 /2)+aci * Math.PI / 180  ) * (x - xm));
                if (h > 0 && h < img.Height)
                { p9 = Color.FromArgb(0, 255,0); bmp2.SetPixel(x, h, p9); }

              

            }
            return bmp2;
        }



        public double ShellHistogram1(Bitmap img, double[] h)
        {
            double aci, r, rmax, dx, dy, dr;
            int x, y, n, xm, ym, i, ri;
            Color p9; Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;
            ri = h.Length;
            n = 0; rmax = 0;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        dx = (double)(x - xm); dy = (double)(y - ym);
                        r = Math.Sqrt(dx * dx + dy * dy);
                        if (r > rmax) rmax = r;
                        n++;
                    }
                }
            }
            dr = (double)(rmax / ri);
            for (i = 0; i < h.Length; i++)
            { h[i] = 0; }
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        dx = (double)(x - xm); dy = (double)(y - ym);
                        r = Math.Sqrt(dx * dx + dy * dy);
                        for (i = 0; i < h.Length; i++)
                        {
                            if (r > i * dr && r < (i + 1) * dr)
                                h[i] = h[i] + 1;
                        }

                    }
                }
            }
            for (i = 0; i < h.Length; i++)
            { h[i] = h[i] / n; }
            // aci = -0.5 * Math.Atan((double)(2 * Ixy) / (double)(Ix - Iy));
            return rmax;
        }
        public Bitmap ShellHistogram2(Bitmap img, double[] h)
        {
            Bitmap bmp2 = (Bitmap)img.Clone();
            double aci, r, rmax, dx, dy, dr;
            int x, y, n, xm, ym, i, ri;
            Color p9; Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;
            ri = h.Length;
            n = 0; rmax = 0;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        dx = (double)(x - xm); dy = (double)(y - ym);
                        r = Math.Sqrt(dx * dx + dy * dy);
                        if (r > rmax) rmax = r;
                        n++;
                    }
                }
            }
            dr = (double)(rmax / (double)ri);
            for (i = 0; i < h.Length; i++)
            { h[i] = 0; }
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        dx = (double)(x - xm); dy = (double)(y - ym);
                        r = Math.Sqrt(dx * dx + dy * dy);
                        for (i = 0; i < h.Length; i++)
                        {
                            if (r >= i * dr && r < (i + 1) * dr)
                            {
                                h[i] = h[i] + 1;
                                p9 = Color.FromArgb(0, i * 255 / ri, 255 - i * 255 / ri);
                                bmp2.SetPixel(x, y, p9);
                            }
                        }
                    }
                    else
                    {
                        p9 = Color.FromArgb(255, 255, 255);
                        bmp2.SetPixel(x, y, p9);
                    }
                    /*
                    dx = (double)(x - xm); dy = (double)(y - ym);
                    r = Math.Sqrt(dx * dx + dy * dy);
                    for (i = 0; i < h.Length; i++)
                    { 
                    if (r <= (2 * dr))
                    { p9 = Color.FromArgb(255, 255, 0);
                    bmp2.SetPixel(x, y, p9);
                    } 
                    else
                    { p9 = Color.FromArgb(0, 0, 255);
                    bmp2.SetPixel(x, y, p9); } 
                    }
                    */

                }
            }
            for (i = 0; i < h.Length; i++)
            { h[i] = h[i] / n; }

            Pen pen1 = new System.Drawing.Pen(Color.Brown, 1F);
            Graphics g; g = Graphics.FromImage(bmp2);
            // g.DrawRectangle(pen1, 20, 10, 50, 60);
            // g.DrawEllipse(pen1, xm, ym, 10, 10);
            //g.DrawEllipse(pen1, new Rectangle(xm-10, ym-10, 10, 10));
            g.DrawEllipse(pen1, (int)(xm - 1 * dr), (int)(ym - 1 * dr), (int)(2 * dr), (int)(2 * dr));
            g.DrawEllipse(pen1, (int)(xm - 2 * dr), (int)(ym - 2 * dr), (int)(4 * dr), (int)(4 * dr));
            for (i = 1; i <= h.Length; i++)
            {
                g.DrawEllipse(pen1, (int)(xm - i * dr), (int)(ym - i * dr), (int)(2 * i * dr), (int)(2 * i * dr));
            }
            g.DrawImage(bmp2, 0, 0);



            return bmp2;
        }


        public Bitmap ShellHistogram3(Bitmap img, double[] h)
        {
            Bitmap bmp2 = (Bitmap)img.Clone();
            double alfa, dx, dy, dteta, r, rmax;
            int x, y, n, xm, ym, i, ri;
            Color p9; Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;
            ri = h.Length;
            n = 0; dteta = (double)(360 / ri);
            for (i = 0; i < h.Length; i++)
            { h[i] = 0; }

            n = 0; rmax = 0;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        dx = (double)(x - xm); dy = (double)(y - ym);
                        r = Math.Sqrt(dx * dx + dy * dy);
                        if (r > rmax) rmax = r;
                        n++;
                    }
                }
            }

            if (n == 0) n = 1;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {

                        dx = (double)(x - xm); dy = (double)(y - ym);
                        alfa = acibul(dx, dy);
                        for (i = 0; i < h.Length; i++)
                        {
                            if (alfa >= i * dteta && alfa < (i + 1) * dteta)
                            {
                                h[i] = h[i] + 1;
                                p9 = Color.FromArgb(200, 255-i * 255 / ri,  i * 255 / ri);
                                bmp2.SetPixel(x, y, p9);
                            }
                        }
                    }
                    else
                    {
                        p9 = Color.FromArgb(0, 0, 0);
                        bmp2.SetPixel(x, y, p9);
                    }

                }
            }
            for (i = 0; i < h.Length; i++)
            { h[i] = h[i] / n; }
           /*
            Pen pen1 = new System.Drawing.Pen(Color.Brown, 1F);
            Graphics g; g = Graphics.FromImage(bmp2); 
            for (i = 0; i < h.Length; i++)
            {
            g.DrawLine(pen1, (int)(xm), (int)(ym), (int)(xm+rmax*Math.Cos(i * dteta)), (int)(ym+rmax*Math.Sin(i * dteta))); 
            }
            g.DrawImage(bmp2, 0, 0);
          */
            return bmp2;
        }



        public String HarfiTaniShell(Bitmap img)
        {
            double[] q = new double[16];
            double[] gelen = new double[16];
            double[] sonuc = new double[16];
            double sim; double enbenzer = 0;
            String bulunan = "";
            Bitmap bmp1 = new Bitmap(img);
            Bitmap bmp2 = new Bitmap(img);

           bmp1 = GetCom(bmp2, -1, -1, -1);
            ShellHistogram2(bmp1, q);
            using (StreamReader dosya = new StreamReader("c:\\Medpic\\alfabeSZShell16.txt"))
            {
                string line;
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (int i = 0; i < bil.Length - 1; i++)
                    { gelen[i] = Convert.ToDouble(bil[i]); }

                    sim = HistogramKarsilastir(q, gelen);
                    if (sim > enbenzer)
                    {
                        enbenzer = sim;
                        for (int i = 0; i < bil.Length - 1; i++)
                        {
                            sonuc[i] = gelen[i];
                            bulunan = bil[16];
                        }
                    }
                }
            }
            return bulunan;
        }

        public Bitmap SectorHistogram(Bitmap img, double[] h)
        {
            Bitmap bmp2 = (Bitmap)img.Clone();
            double alfa, dx, dy, dteta, r, rmax;
            int x, y, n, xm, ym, i, ri;
            Color p9; Point agirlikmerkezi;
            agirlikmerkezi = AgirlikMerkeziBul(img);
            xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;
            ri = h.Length;
            n = 0; dteta = (double)(360 / ri);
            for (i = 0; i < h.Length; i++)
            { h[i] = 0; }

            n = 0; rmax = 0;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {
                        dx = (double)(x - xm); dy = (double)(y - ym);
                        r = Math.Sqrt(dx * dx + dy * dy);
                        if (r > rmax) rmax = r;
                        n++;
                    }
                }
            }

            if (n == 0) n = 1;
            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    if (p9.R == 255)
                    {

                        dx = (double)(x - xm); dy = (double)(y - ym);
                        alfa = acibul(dx, dy);
                        for (i = 0; i < h.Length; i++)
                        {
                            if (alfa >= i * dteta && alfa < (i + 1) * dteta)
                            {
                                h[i] = h[i] + 1;
                                p9 = Color.FromArgb(200, 255 - i * 255 / ri, i * 255 / ri);
                                bmp2.SetPixel(x, y, p9);
                            }
                        }
                    }
                    else
                    {
                        p9 = Color.FromArgb(0, 0, 0);
                        bmp2.SetPixel(x, y, p9);
                    }

                }
            }
            for (i = 0; i < h.Length; i++)
            { h[i] = h[i] / n; }
            /*
             Pen pen1 = new System.Drawing.Pen(Color.Brown, 1F);
             Graphics g; g = Graphics.FromImage(bmp2); 
             for (i = 0; i < h.Length; i++)
             {
             g.DrawLine(pen1, (int)(xm), (int)(ym), (int)(xm+rmax*Math.Cos(i * dteta)), (int)(ym+rmax*Math.Sin(i * dteta))); 
             }
             g.DrawImage(bmp2, 0, 0);
           */
            return bmp2;
        }



        public String HarfiTaniSector(Bitmap img)
        {
            double[] q = new double[16];
            double[] gelen = new double[16];
            double[] sonuc = new double[16];
            double sim; double enbenzer = 0;
            String bulunan = "";
            Bitmap bmp1 = new Bitmap(img);
            Bitmap bmp2 = new Bitmap(img);

            bmp1 = GetCom(bmp2, -1, -1, -1);
            SectorHistogram(bmp1, q);
            using (StreamReader dosya = new StreamReader("c:\\Medpic\\alfabeSZSector16.txt"))
            {
                string line;
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (int i = 0; i < bil.Length - 1; i++)
                    { gelen[i] = Convert.ToDouble(bil[i]); }

                    sim = HistogramKarsilastir(q, gelen);
                    if (sim > enbenzer)
                    {
                        enbenzer = sim;
                        for (int i = 0; i < bil.Length - 1; i++)
                        {
                            sonuc[i] = gelen[i];
                            bulunan = bil[16];
                        }
                    }
                }
            }

            return bulunan;
        }

        public Bitmap ShellHistogram4(Bitmap img, double[] h)
        {
            Bitmap bmp2 = (Bitmap)img.Clone();
            double r, rmax, dx, dy, dr;
            int x, y, n, xm, ym, i, ri; Color p9;
            xm = img.Width / 2; ym = img.Height / 2; ri = h.Length;
            n = img.Width * img.Height; rmax = 0;

            dx = (double)(img.Width - xm); dy = (double)(img.Height - ym);
            r = Math.Sqrt(dx * dx + dy * dy); if (r > rmax) rmax = r;

            dr = (double)(rmax / ri);
            for (i = 0; i < h.Length; i++)
            { h[i] = 0; }

            for (x = 0; x < img.Width; x++)
            {
                for (y = 0; y < img.Height; y++)
                {
                    p9 = img.GetPixel(x, y);
                    dx = (double)(x - xm); dy = (double)(y - ym);
                    r = Math.Sqrt(dx * dx + dy * dy);

                    for (i = 0; i < h.Length; i++)
                    {
                        if (r > i * dr && r < (i + 1) * dr)
                        {
                            h[i] = h[i] + 1;
                            //   p9 = Color.FromArgb(255,255,255);
                            bmp2.SetPixel(x, y, p9);
                        }
                    }


                }
            }
            for (i = 0; i < h.Length; i++)
            { h[i] = h[i] / n; }
            Pen pen1 = new System.Drawing.Pen(Color.Brown, 1F);
            Graphics g; g = Graphics.FromImage(bmp2);
            // g.DrawRectangle(pen1, 20, 10, 50, 60);
            // g.DrawEllipse(pen1, xm, ym, 10, 10);
            //g.DrawEllipse(pen1, new Rectangle(xm-10, ym-10, 10, 10));
            g.DrawEllipse(pen1, (int)(xm - 1 * dr), (int)(ym - 1 * dr), (int)(2 * dr), (int)(2 * dr));
            g.DrawEllipse(pen1, (int)(xm - 2 * dr), (int)(ym - 2 * dr), (int)(4 * dr), (int)(4 * dr));
            for (i = 1; i <= h.Length; i++)
            {
                g.DrawEllipse(pen1, (int)(xm - i * dr), (int)(ym - i * dr), (int)(2 * i * dr), (int)(2 * i * dr));
            }
            g.DrawImage(bmp2, 0, 0);
            return bmp2;
        }



        public double acibul(double dx, double dy)
        {
            double alfa;
            alfa = 0;
            if (dx == 0)
            {
                if (dy == 0) { alfa = 0.0; }
                else if (dy > 0) { alfa = 90.0; }
                else if (dy < 0) { alfa = 270.0; }
            }
            else if (dy == 0)
            {
                if (dx == 0) { alfa = 0.0; }
                else if (dx > 0) { alfa = 0.0; }
                else if (dx < 0) { alfa = 180.0; }
            }
            else if (dx < 0 && dy > 0)
            { dx = -dx; alfa = 180 - ((Math.Atan((double)(dy) / (double)(dx))) * (180 / Math.PI)); }
            else if (dx < 0 && dy < 0)
            { alfa = 180 + ((Math.Atan((double)(dy) / (double)(dx))) * (180 / Math.PI)); }
            else if (dx > 0 && dy < 0)
            { dy = -dy; alfa = 360 - ((Math.Atan((double)(dy) / (double)(dx))) * (180 / Math.PI)); }
            else
            { alfa = (Math.Atan((double)(dy) / (double)(dx))) * (180 / Math.PI); }
            return alfa;
        }



        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }
        public void Retriev(double[] h, int[] sonuc)
        {
            int[] dosyaindisi = new int[sonuc.Length];
            double[] gelen = new double[h.Length];
            double[] sim = new double[1001];

            using (StreamReader dosya = new StreamReader("c:\\Medpic\\kodakHAM3.txt"))
            {
                int n = 0; String line = "";
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (int i = 0; i < bil.Length - 1; i++)
                    { gelen[i] = Convert.ToDouble(bil[i]); }

                    sim[n] = HistogramKarsilastir(h, gelen);
                    n++;
                }
            }//dosya

            BenzerlikSirala(dosyaindisi, sim);

            for (int i = 0; i < sonuc.Length; i++)
            { sonuc[i] = dosyaindisi[i]; }
        }



        public void BenzerlikSirala(int[] index, double[] h)
        {
            int i, t;
            int[] d; d = new int[h.Length];
            for (i = 0; i < h.Length; i++)
            { d[i] = i; }

            int td = d[0]; double tn = h[0];

            for (i = 0; i < h.Length; i++)
            {

                for (t = 0; t < h.Length; t++)
                {
                    if (h[t] < h[i])
                    {
                        td = d[i];
                        d[i] = d[t];
                        d[t] = td;

                        tn = h[i];
                        h[i] = h[t];
                        h[t] = tn;
                    }
                }

            }

            for (i = 0; i < index.Length; i++)
            { index[i] = d[i]; }

        }




        public void RetrievPRcurves(double[,] Pricisions, int GetirilenSayi)
        {
            int i, j, k, n, N, K;
            N = 8; K = GetirilenSayi;

            double[] gelen = new double[N];
            double[] sorgu = new double[N];
            double[] sim = new double[1000];
            double[,] veri = new double[1000, N];
            int[,] Sonuc = new int[1000, K];

            int[] indisler = new int[K];
            double[] Pint = new double[11];
            double[,] Pintcurves = new double[1000, 11];


            using (StreamReader dosya = new StreamReader("c:\\Medpic\\kodakHAM1.txt"))
            {
                String line = "";
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (k = 0; k < bil.Length - 1; k++)
                    { gelen[k] = Convert.ToDouble(bil[k]); }

                    n = int.Parse(bil[bil.Length - 1]);

                    for (k = 0; k < bil.Length - 1; k++)
                    { veri[n, k] = Convert.ToDouble(bil[k]); }
                }
            }//dosya


            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < N; k++)
                { sorgu[k] = veri[i, k]; }

                for (j = 0; j < 1000; j++)
                {
                    for (k = 0; k < N; k++)
                    { gelen[k] = veri[j, k]; }

                    sim[j] = HistogramKarsilastir(gelen, sorgu);
                }

                BenzerlikSirala(indisler, sim);

                for (k = 0; k < K; k++)
                { Sonuc[i, k] = (int)(indisler[k] / 100); }

            }



            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < K; k++)
                { indisler[k] = Sonuc[i, k]; }

                PRCurve((int)(i / 100), indisler, Pint);

                for (j = 0; j < 11; j++)
                {
                    Pintcurves[i, j] = Pint[j];
                    Pricisions[i, j] = Pintcurves[i, j];
                }

            }




        }

        public void RetrievPRSinifcurves(double[,] PRSiniflar, int GetirilenSayi)
        {
            int i, j, k, n, N, K;
            N = 8; K = GetirilenSayi;

            double[] gelen = new double[N];
            double[] sorgu = new double[N];
            double[] sim = new double[1000];
            double[,] veri = new double[1000, N];
            int[,] Sonuc = new int[1000, K];

            int[] indisler = new int[K];
            double[] Pint = new double[11];
            double[,] Pintcurves = new double[1000, 11];
            double[,] PRClass = new double[10, 11];

            double[,] Ptoplam = new double[10, 11];


            using (StreamReader dosya = new StreamReader("c:\\Medpic\\kodakHAM1.txt"))
            {
                String line = "";
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (k = 0; k < bil.Length - 1; k++)
                    { gelen[k] = Convert.ToDouble(bil[k]); }

                    n = int.Parse(bil[bil.Length - 1]);

                    for (k = 0; k < bil.Length - 1; k++)
                    { veri[n, k] = Convert.ToDouble(bil[k]); }
                }
            }//dosya


            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < N; k++)
                { sorgu[k] = veri[i, k]; }

                for (j = 0; j < 1000; j++)
                {
                    for (k = 0; k < N; k++)
                    { gelen[k] = veri[j, k]; }

                    sim[j] = HistogramKarsilastir(gelen, sorgu);
                }

                BenzerlikSirala(indisler, sim);

                for (k = 0; k < K; k++)
                { Sonuc[i, k] = (int)(indisler[k] / 100); }

            }



            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < K; k++)
                { indisler[k] = Sonuc[i, k]; }

                PRCurve((int)(i / 100), indisler, Pint);

                for (j = 0; j < 11; j++)
                {
                    Pintcurves[i, j] = Pint[j];
                }

            }

            // siniflarin PR curves
            for (i = 0; i < 10; i = i + 1)
            {

                for (k = 0; k < 11; k++)
                { Ptoplam[i, k] = 0.0; }


                for (j = 0; j < 100; j = j + 1)
                {
                    for (k = 0; k < 11; k++)
                    {
                        Ptoplam[i, k] = Ptoplam[i, k] + Pintcurves[100 * i + j, k];
                    }
                }


                for (k = 0; k < 11; k++)
                { PRClass[i, k] = Ptoplam[i, k] / 100.0; }

            }


            // sınıflar:0-9   
            for (i = 0; i < 10; i = i + 1)
            {
                for (k = 0; k < 11; k++)
                {
                    PRSiniflar[i, k] = PRClass[i, k];
                }
            }



        }


        public void RetrievPRGlobalcurve(double[] PRGlobal, int GetirilenSayi)
        {
            int i, j, k, n, N, K;
            N = 8; K = GetirilenSayi;

            double[] gelen = new double[N];
            double[] sorgu = new double[N];
            double[] sim = new double[1000];
            double[,] veri = new double[1000, N];
            int[,] Sonuc = new int[1000, K];

            int[] indisler = new int[K];
            double[] Pint = new double[11];
            double[,] Pintcurves = new double[1000, 11];
            double[] PRGenel = new double[11];

            double[] toplam = new double[11];


            using (StreamReader dosya = new StreamReader("c:\\Medpic\\kodakHAM1.txt"))
            {
                String line = "";
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (k = 0; k < bil.Length - 1; k++)
                    { gelen[k] = Convert.ToDouble(bil[k]); }

                    n = int.Parse(bil[bil.Length - 1]);

                    for (k = 0; k < bil.Length - 1; k++)
                    { veri[n, k] = Convert.ToDouble(bil[k]); }
                }
            }//dosya


            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < N; k++)
                { sorgu[k] = veri[i, k]; }

                for (j = 0; j < 1000; j++)
                {
                    for (k = 0; k < N; k++)
                    { gelen[k] = veri[j, k]; }

                    sim[j] = HistogramKarsilastir(gelen, sorgu);
                }

                BenzerlikSirala(indisler, sim);

                for (k = 0; k < K; k++)
                { Sonuc[i, k] = (int)(indisler[k] / 100); }

            }



            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < K; k++)
                { indisler[k] = Sonuc[i, k]; }

                PRCurve((int)(i / 100), indisler, Pint);

                for (j = 0; j < 11; j++)
                {
                    Pintcurves[i, j] = Pint[j];
                }

            }


            //Global
            for (k = 0; k < 11; k++)
            { toplam[k] = 0.0; }

            for (i = 0; i < 1000; i = i + 1)
            {
                for (k = 0; k < 11; k++)
                {
                    toplam[k] = toplam[k] + Pintcurves[i, k];
                }
            }

            for (k = 0; k < 11; k++)
            {
                PRGenel[k] = toplam[k] / 1000.0;
                PRGlobal[k] = PRGenel[k];
            }


        }



        public void PRCurve(int Dogru, int[] Getirilen, double[] Pent)
        {
            int j, k, s, td, TD, K;
            K = Getirilen.Length;

            int[] indisler = new int[K];
            double[] P = new double[K];
            double[] R = new double[K];
            double[] Pint = new double[11];
            double[] Rint = new double[11];

            for (k = 0; k < K; k++)
            {
                indisler[k] = Getirilen[k];
            }

            s = Dogru;

            td = 0;
            for (k = 0; k < K; k++)
            {
                if (s == indisler[k])
                    td = td + 1;
            }

            TD = td;

            td = 0;
            for (k = 0; k < K; k++)
            {
                if (s == indisler[k]) td = td + 1;
                P[k] = (double)td / (double)(k + 1);
            }

            td = 0;
            for (k = 0; k < K; k++)
            {
                if (s == indisler[k]) td = td + 1;
                R[k] = (double)td / (double)TD;
            }

            double en;

            Pint[0] = 1.0;
            Rint[0] = 0.0;

            for (j = 1; j < 11; j++)
            {
                Rint[j] = Rint[j - 1] + 0.1;
                en = 0;
                for (k = 0; k < K; k++)
                {
                    if (R[k] > Rint[j])
                    {
                        if (P[k] > en)
                        { en = P[k]; }
                    }
                }
                Pint[j] = en;
            }


            for (k = 0; k < 11; k++)
            {
                Pent[k] = Pint[k];
            }


        }


        public void RetrievMatrixCon(int[,] sonuc, int GetirilenSayi)
        {
            int i, j, k, n, s, N, K;
            N = 8; K = GetirilenSayi;

            double[] gelen = new double[N];
            double[] sorgu = new double[N];
            double[] sim = new double[1000];
            double[,] veri = new double[1000, N];
            double[,] Sonuclar = new double[1000, K];
            int[] indisler = new int[K];

            int[,] Con = new int[10, 10];

            using (StreamReader dosya = new StreamReader("c:\\Medpic\\kodakHAM1.txt"))
            {
                String line = "";
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (k = 0; k < bil.Length - 1; k++)
                    { gelen[k] = Convert.ToDouble(bil[k]); }

                    n = int.Parse(bil[bil.Length - 1]);

                    for (k = 0; k < bil.Length - 1; k++)
                    { veri[n, k] = Convert.ToDouble(bil[k]); }
                }
            }//dosya


            for (j = 0; j < 10; j++)
            {
                for (k = 0; k < 10; k++)
                { Con[j, k] = 0; }
            }


            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < N; k++)
                { sorgu[k] = veri[i, k]; }

                for (j = 0; j < 1000; j++)
                {
                    for (k = 0; k < N; k++)
                    { gelen[k] = veri[j, k]; }

                    sim[j] = HistogramKarsilastir(gelen, sorgu);
                }

                BenzerlikSirala(indisler, sim);

                for (k = 0; k < K; k++)
                { Sonuclar[i, k] = indisler[k] / 100; }

            }




            for (i = 0; i < 1000; i++)
            {
                s = i / 100;
                for (k = 0; k < K; k++)
                {
                    n = (int)Sonuclar[i, k];
                    Con[s, n] = Con[s, n] + 1;
                    sonuc[s, n] = Con[s, n];
                }
            }

        }



        public void RetrievMatrixRaw(int[,] sonuc, int GetirilenSayi)
        {
            int i, j, k, n, s, N, K;
            N = 8; K = GetirilenSayi;

            double[] gelen = new double[N];
            double[] sorgu = new double[N];
            double[] sim = new double[1000];
            double[,] veri = new double[1000, N];
            double[,] Sonuclar = new double[1000, K];
            int[] indisler = new int[K];



            using (StreamReader dosya = new StreamReader("c:\\Medpic\\kodakHAM1.txt"))
            {
                String line = "";
                while ((line = dosya.ReadLine()) != null)
                {
                    string[] bil = line.Split(';');
                    for (k = 0; k < bil.Length - 1; k++)
                    { gelen[k] = Convert.ToDouble(bil[k]); }

                    n = int.Parse(bil[bil.Length - 1]);

                    for (k = 0; k < bil.Length - 1; k++)
                    { veri[n, k] = Convert.ToDouble(bil[k]); }
                }
            }//dosya



            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < N; k++)
                { sorgu[k] = veri[i, k]; }

                for (j = 0; j < 1000; j++)
                {
                    for (k = 0; k < N; k++)
                    { gelen[k] = veri[j, k]; }

                    sim[j] = HistogramKarsilastir(gelen, sorgu);
                }

                BenzerlikSirala(indisler, sim);

                for (k = 0; k < K; k++)
                { Sonuclar[i, k] = indisler[k] / 100; }

            }

            for (i = 0; i < 1000; i++)
            {
                for (k = 0; k < K; k++)
                {
                    sonuc[i, k] = (int)Sonuclar[i, k];
                }
            }

        }    

























        public void KenarBul(int xg, int yg)
        {
            int[] q= new int[10];
            int resim,b,bmin,x,y;

            for (b = 0; b < 8; b++)
            {   x = ikomsu(b, xg); y = jkomsu(b, yg);
                resim = resimdemi(x, y, resim1.Width, resim1.Height);
                if (resim == 1)
                 {  c2 = resim1.GetPixel(x, y); 
                    if (c2.R == 255)  q[b] = b; else q[b] = 100;
                 }   else q[b] = 100; 
            }

            bmin=100;
            for (b = 0; b < 8; b++)
            { if (q[b] < bmin) bmin = b; }

            if (bmin<8)
            {
                chaincode = chaincode + Convert.ToString(bmin);  x = ikomsu(bmin, xg); y = jkomsu(bmin, yg);
                c2 = Color.FromArgb(0, 0, 0);   resim1.SetPixel(x, y, c2);

                if (bmin==0) c2 = Color.FromArgb(255, 0, 0);
                if (bmin ==1) c2 = Color.FromArgb(0, 255, 0);
                if (bmin ==2) c2 = Color.FromArgb(0, 0, 255);
                if (bmin ==3) c2 = Color.FromArgb(255, 255, 0);
                if (bmin ==4) c2 = Color.FromArgb(255, 0, 255);
                if (bmin ==5) c2 = Color.FromArgb(0, 255, 255);
                if (bmin ==6) c2 = Color.FromArgb(255, 255, 255);
                if (bmin ==7) c2 = Color.FromArgb(128, 0, 255);
                resim2.SetPixel(x, y, c2);
                KenarBul(x , y); 
            }

           // chain = chain + "a";  c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(xg, yg, c2);
        }



        public void KenarBul2(int xg, int yg)
        {    int resim,  x, y;

               x = ikomsu(0, xg); y = jkomsu(0, yg);
               resim = resimdemi(x, y, resim1.Width, resim1.Height);
               if (resim == 1)
                {    c2 = resim1.GetPixel(x, y);
                     if (c2.R == 255)
                         {   chaincode = chaincode + "0";
                             c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                             c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(x, y, c2);
                             KenarBul2(x, y);
                          }
                }


              x = ikomsu(1, xg); y = jkomsu(1, yg);
              resim = resimdemi(x, y, resim1.Width, resim1.Height);
              if (resim == 1)
              {   c2 = resim1.GetPixel(x, y);
                  if (c2.R == 255)
                  {   chaincode = chaincode + "1";
                      c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                      c2 = Color.FromArgb(0, 255, 0); resim2.SetPixel(x, y, c2);
                      KenarBul2(x, y);
                  }
              }


              x = ikomsu(2, xg); y = jkomsu(2, yg);
              resim = resimdemi(x, y, resim1.Width, resim1.Height);
             if (resim == 1)
                {   c2 = resim1.GetPixel(x, y);
                    if (c2.R == 255)
                    {   chaincode = chaincode + "2";
                        c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                        c2 = Color.FromArgb(0, 0, 255); resim2.SetPixel(x, y, c2);
                        KenarBul2(x, y);
                    }
                }


             x = ikomsu(3, xg); y = jkomsu(3, yg);
             resim = resimdemi(x, y, resim1.Width, resim1.Height);
             if (resim == 1)
                {   c2 = resim1.GetPixel(x, y);
                    if (c2.R == 255)
                    {   chaincode = chaincode + "3";
                        c2 = Color.FromArgb(0, 0, 0);     resim1.SetPixel(x, y, c2);
                        c2 = Color.FromArgb(255, 255, 0); resim2.SetPixel(x, y, c2);
                        KenarBul2(x, y);
                    }
                }

             x = ikomsu(4, xg); y = jkomsu(4, yg);
             resim = resimdemi(x, y, resim1.Width, resim1.Height);
             if (resim == 1)
                {   c2 = resim1.GetPixel(x, y);
                    if (c2.R == 255)
                    {   chaincode = chaincode + "4";
                        c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                        c2 = Color.FromArgb(255, 0, 255); resim2.SetPixel(x, y, c2);
                        KenarBul2(x, y);
                    }
                }

             x = ikomsu(5, xg); y = jkomsu(5, yg);
             resim = resimdemi(x, y, resim1.Width, resim1.Height);
              if (resim == 1)
              {   c2 = resim1.GetPixel(x, y);
                  if (c2.R == 255)
                  {   chaincode = chaincode + "5";
                      c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                      c2 = Color.FromArgb(0, 255, 255); resim2.SetPixel(x, y, c2);
                      KenarBul2(x, y);
                  }
                }

              x = ikomsu(6, xg); y = jkomsu(6, yg);
              resim = resimdemi(x, y, resim1.Width, resim1.Height);
               if (resim == 1)
                {   c2 = resim1.GetPixel(x, y);
                    if (c2.R == 255)
                    {   chaincode = chaincode + "6";
                        c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                        c2 = Color.FromArgb(255, 255, 255); resim2.SetPixel(x, y, c2);
                        KenarBul2(x, y);
                    }
                }

               x = ikomsu(7, xg); y = jkomsu(7, yg);
               resim = resimdemi(x, y, resim1.Width, resim1.Height);
             if (resim == 1)
             {   c2 = resim1.GetPixel(x, y);
                 if (c2.R == 255)
                 {    chaincode = chaincode + "7";
                     c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                     c2 = Color.FromArgb(128, 0, 255); resim2.SetPixel(x, y, c2);
                     KenarBul2(x, y);
                 }
             }     

           
        }


        public void KenarBulComplex(int xm, int ym,int xg, int yg)
        {   int resim, x, y;

       
            x = ikomsu(0, xg); y = jkomsu(0, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "0"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(x, y, c2);
                    KenarBulComplex(xm, ym, x, y);
                    kenarvar = true;
                }
                else kenarvar = false;
            }


            x = ikomsu(1, xg); y = jkomsu(1, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "1"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(0, 255, 0); resim2.SetPixel(x, y, c2);
                     KenarBulComplex(xm, ym, x, y);
                     kenarvar = true;
                }
                else kenarvar = false;

            }


            x = ikomsu(2, xg); y = jkomsu(2, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "2"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(0, 0, 255); resim2.SetPixel(x, y, c2);
                    KenarBulComplex(xm, ym, x, y);
                    kenarvar = true;
                }
                else kenarvar = false;
            }


            x = ikomsu(3, xg); y = jkomsu(3, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "3"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(255, 255, 0); resim2.SetPixel(x, y, c2);
                    KenarBulComplex(xm, ym, x, y);
                     kenarvar = true;
                }
                else kenarvar = false;
            }

            x = ikomsu(4, xg); y = jkomsu(4, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "4"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(255, 0, 255); resim2.SetPixel(x, y, c2);
                    KenarBulComplex(xm, ym, x, y);
                    kenarvar = true;
                }
                else kenarvar = false;
            }

            x = ikomsu(5, xg); y = jkomsu(5, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "5"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(0, 255, 255); resim2.SetPixel(x, y, c2);
                    KenarBulComplex(xm, ym, x, y);
                    kenarvar = true;
                }
                else kenarvar = false;

            }

            x = ikomsu(6, xg); y = jkomsu(6, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "6"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(255, 255, 255); resim2.SetPixel(x, y, c2);
                    KenarBulComplex(xm, ym, x, y);
                    kenarvar = true;
                }
                else kenarvar = false;
            }

            x = ikomsu(7, xg); y = jkomsu(7, yg);
            resim = resimdemi(x, y, resim1.Width, resim1.Height);
            if (resim == 1)
            {
                c2 = resim1.GetPixel(x, y);
                if (c2.R == 255)
                {
                    chaincode = chaincode + "7"; Complex z = new Complex((double)(x - xm), (double)(y - ym)); ComplexChain.Add(z);
                    c2 = Color.FromArgb(0, 0, 0); resim1.SetPixel(x, y, c2);
                    c2 = Color.FromArgb(128, 0, 255); resim2.SetPixel(x, y, c2);
                    KenarBulComplex(xm, ym, x, y);
                    kenarvar = true;
                }
                else kenarvar = false;
            }
           

        }


        public Bitmap StringToImage(Bitmap img,int xg, int yg,string Zincir)
        {    Bitmap bmp1 = (Bitmap)img.Clone();
             int xson,yson,x, y, i,resim;
             for (x = 0; x < bmp1.Width; x++)
             {  for (y = 0; y < bmp1.Height; y++)
               { data[x, y] =-1; c2 = Color.FromArgb(0, 0, 0); bmp1.SetPixel(x, y, c2); }   }

             x = 0; y = 0; resim = 0; xson = xg; yson = yg;

             char[] chars = Zincir.ToCharArray();
          //   var chars = Zincir.ToCharArray(0, Zincir.Length);
            // MessageBox.Show(Convert.ToString(chars.Length));    
           
            for (i = 0; i < (chars.Length); i++)
            {
                if (chars[i] == '0')
                {
                    x = ikomsu(0, xson); y = jkomsu(0, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 &&  data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }

                if (chars[i] == '1')
                {
                    x = ikomsu(1, xson); y = jkomsu(1, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 &&  data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }

                if (chars[i] == '2')
                {
                    x = ikomsu(2, xson); y = jkomsu(2, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 &&  data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }

                if (chars[i] == '3')
                {
                    x = ikomsu(3, xson); y = jkomsu(3, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 &&  data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }

                if (chars[i] == '4')
                {
                    x = ikomsu(4, xson); y = jkomsu(4, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 &&  data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }

                if (chars[i] == '5')
                {
                    x = ikomsu(5, xson); y = jkomsu(5, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 &&  data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }

                if (chars[i] == '6')
                {
                    x = ikomsu(6, xson); y = jkomsu(6, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 && x != xg && y != yg && data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }

                if (chars[i] == '7')
                {
                    x = ikomsu(7, xson); y = jkomsu(7, yson); resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    if (resim == 1 &&  data[x, y] == -1) { data[x, y] = 0; c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2); xson = x; yson = y; }
                }
               
            }

            return bmp1;
        }



        public Bitmap ComplexToImage(Bitmap img, int xg, int yg, ArrayList Z)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int  x, y, resim;
            for (x = 0; x < bmp1.Width; x++)
            {   for (y = 0; y < bmp1.Height; y++)
                {  c2 = Color.FromArgb(0, 0, 0); bmp1.SetPixel(x, y, c2); }
            }

            x = 0; y = 0; 
            foreach (Complex z in Z)
            {   x = (int)(xg+z.real); y = (int)(yg+z.imag);
                resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                if (resim == 1) 
                {  c2 = Color.FromArgb(255, 0, 0); bmp1.SetPixel(x, y, c2);  }
                  
            }           

            return bmp1;
        }


        public Complex[] ComplexDiziKatla(Complex[] input)
        {
            double r, r1, r2, aci, aci1, aci2; int i; double x, y, x1, y1, x2, y2; int N = input.Length;
            Complex[] output = new Complex[2 * N];
            for (i = 0; i < (input.Length - 1); i++)
            {
                if (i < (input.Length - 1))
                {
                    output[2 * i] = input[i];
                    r1 = input[i].magnitude; aci1 = input[i].Phase;
                    x1 = r1 * Math.Cos((aci1) * (Math.PI / 180)); y1 = r1 * Math.Sin((aci1) * (Math.PI / 180));
                    r2 = input[i + 1].magnitude; aci2 = input[i + 1].Phase;
                    x2 = r2 * Math.Cos((aci2) * (Math.PI / 180)); y2 = r2 * Math.Sin((aci2) * (Math.PI / 180));
                    // x = (x1 + x2) / 2; y = (y1 + y2) / 2; Complex z = new Complex(x, y); 
                    r = (r1 + r2) / 2; aci = (aci1 + aci2) / 2;
                    x = r * Math.Cos((aci) * (Math.PI / 180));
                    y = r * Math.Sin((aci) * (Math.PI / 180)); Complex z = new Complex(x, y);
                    output[2 * i + 1] = z;
                }
                if (i == (input.Length - 2))
                {
                    output[2 * (i + 1)] = input[i + 1];
                    output[2 * (i + 1) + 1] = input[i + 1];
                }
            }
            return output;
        }

        public Complex[] ComplexDiziKatlaN(Complex[] input, int n)
        {
            int i; int N = input.Length;
            Complex[] output = new Complex[n * 2 * N];
            Complex[] Z2 = new Complex[N];
            Z2 = input;
            for (i = 0; i < n; i++)
            {
                Z2 = ComplexDiziKatla(Z2);
            }
            return output = Z2;
        }


        public void ChainCodeHistogram(string chaincode, double[] h)
        {   int i;
            
             char[] chars = chaincode.ToCharArray();
            for (i = 0; i < 8; i++)
            { h[i] = 0; }
            
            for (i = 0; i <chars.Length; i++)
            {   if (chars[i] =='0') h[0]=h[0]+1;
                if (chars[i]=='1') h[1]++;
                if (chars[i]=='2') h[2]++;
                if (chars[i]=='3') h[3]++;
                if (chars[i]=='4') h[4]++;
                if (chars[i]=='5') h[5]++;
                if (chars[i]=='6') h[6]++;
                if (chars[i]=='7') h[7]++;
            }

            for (i = 0; i < h.Length; i++)
            { h[i] = h[i] / chars.Length; }
        }



        public void FreemanChain8(Bitmap img)
        {   Color p9; int x, y; chaincode = "";
            for (x = 0; x < resim1.Width; x++)
            {  for (y = 0; y < resim1.Height; y++)
                {  c2 = Color.FromArgb(0, 0, 0); resim2.SetPixel(x, y, c2); }
            }

            resim1 = CannyEdge(img, 10, 20);
            for (x = 0; x < resim1.Width; x++)
            {   for (y = 0; y < resim1.Height; y++)
                {   p9 = resim1.GetPixel(x, y);
                    if (p9.R == 255)  { xs = x; ys = y; KenarBul(xs, ys); }
                }
            }
        }


        public void FreemanChain81(Bitmap img)
        {   Bitmap bmp1 = (Bitmap)img.Clone();           
            Color p9;    int x, y, xt, yt, xc, yc,xb,yb,n; 
            chaincode="";
            for (x = 0; x < resim1.Width; x++)
            {   for (y = 0; y < resim1.Height; y++)
                { data[x, y] = -1;   c2 = Color.FromArgb(0, 0, 0); resim2.SetPixel(x, y, c2);  } }


            xt = 0; yt = 0; n = 0;
            for (x = 0; x < img.Width; x++)
            {   for (y = 0; y < img.Height; y++)
                {   p9 = img.GetPixel(x, y);
                if (p9.R == 255) { xt = xt + x; yt = yt + y; n++; }
                }
            }
            xc =(int)  ( xt /n);
            yc = (int) (yt /n);
            
           resim1 = CannyEdge(img, 10, 20);
          //  x = xc; yb = yc;            
           x =resim1.Width/2; yb =resim1.Height/2;
            do 
            {    p9 = resim1.GetPixel(x, yb);    xb = x;  c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(x, yb, c2);
                x++; 
            } while(p9.R != 255  && x<resim1.Width);
         
           KenarBul(xb,yb);           
        }




       public void FreemanChain82(Bitmap img)
        {   Bitmap bmp1 = (Bitmap)img.Clone();           
            Color p9;    int x, y, xt, yt, xc, yc,xb,yb,n; 
            chaincode="";

            for (x = 0; x < resim1.Width; x++)
            {   for (y = 0; y < resim1.Height; y++)
                { data[x, y] = -1;   c2 = Color.FromArgb(0, 0, 0); resim2.SetPixel(x, y, c2);  } }


            xt = 0; yt = 0; n = 0;
            for (x = 0; x < img.Width; x++)
            {   for (y = 0; y < img.Height; y++)
                {   p9 = img.GetPixel(x, y);
                if (p9.R == 255) { xt = xt + x; yt = yt + y; n++; }
                }
            }
            xc =(int)  ( xt /n);
            yc = (int) (yt /n);
            
           resim1 = CannyEdge(img, 10, 20);
           x = xc; yb = yc;            
          // x =resim1.Width/2; yb =resim1.Height/2;
            do 
            {    p9 = resim1.GetPixel(x, yb);    xb = x;  c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(x, yb, c2);
                x++; 
            } while(p9.R != 255  && x<resim1.Width);          


            ThreadStart ts2 = delegate() { KenarBul2(xb, yb); };
            Thread T = new Thread(ts2, 500000000); T.Start();
            x = 1;
            do
            {   if ((T.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                { x = 1; } else x = 0;
            } while (x != 0);
           
        }



       public void FreemanChain83(Bitmap img)
       {   Color p9; int x, y, xt, yt, xc, yc, xb, yb, n;
           chaincode = "";
        
           xt = 0; yt = 0; n = 0;
           for (x = 0; x < img.Width; x++)
           {   for (y = 0; y < img.Height; y++)
               {   c2 = Color.FromArgb(0, 0, 0); resim2.SetPixel(x, y, c2);
                   p9 = img.GetPixel(x, y);
                   if (p9.R == 255) { xt = xt + x; yt = yt + y; n++; }
               }
           }
           xc = (int)(xt / n);  yc = (int)(yt / n);
           resim1 = CannyEdge(img, 10, 20);
           x = xc; yb = yc;    
           do
           {  p9 = resim1.GetPixel(x, yb); xb = x; c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(x, yb, c2);  x++;
           } while (p9.R != 255 && x < resim1.Width);


           ThreadStart ts2 = delegate() { KenarBul2(xb, yb); };
           Thread T = new Thread(ts2, 500000000); T.Start();
           x = 1;
           do
           {   if ((T.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                { x = 1; }  else x = 0;
           } while (x != 0);

       }


       public void FreemanChain84(Bitmap img)
       {   Color p9; int x, y, xm, ym, xb, yb;
           chaincode = ""; Point agirlikmerkezi;
           agirlikmerkezi = AgirlikMerkeziBul(img);
           xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;
          
           for (x = 0; x < img.Width; x++)
           {   for (y = 0; y < img.Height; y++)
            {    c2 = Color.FromArgb(0, 0, 0); resim2.SetPixel(x, y, c2);  }   }
      
           resim1 = CannyEdge(img, 10, 20);
           x = xm; yb = ym;
           do
           {   p9 = resim1.GetPixel(x, yb); xb = x;
               c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(x, yb, c2); x++;
           } while (p9.R != 255 && x < resim1.Width);

           ThreadStart ts2 = delegate() { KenarBul2(xb, yb); };
           Thread T = new Thread(ts2, 500000000); T.Start();
           x = 1;
           do
           {    if ((T.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
               { x = 1; }  else x = 0;
           } while (x != 0);

       }

       public void FreemanChain85(Bitmap img)
       {
           Color p9; int x, y, xm, ym, xb, yb;
           chaincode = ""; Point agirlikmerkezi;
           agirlikmerkezi = AgirlikMerkeziBul(img);
           xm = agirlikmerkezi.X; ym = agirlikmerkezi.Y;

           for (x = 0; x < img.Width; x++)
           {
               for (y = 0; y < img.Height; y++)
               { c2 = Color.FromArgb(0, 0, 0); resim2.SetPixel(x, y, c2); }
           }

           resim1 = CannyEdge(img, 10, 20);
           x = xm; yb = ym;
           do
           {
               p9 = resim1.GetPixel(x, yb); xb = x;
               c2 = Color.FromArgb(255, 0, 0); resim2.SetPixel(x, yb, c2); x++;
           } while (p9.R != 255 && x < resim1.Width);

           xs = xb; ys = yb; xc = xm; yc = ym;
           ThreadStart ts2 = delegate() { KenarBulComplex(xm, ym, xb, yb); };
           Thread T = new Thread(ts2, 500000000); T.Start();
           x = 1;
           do
           {
               if ((T.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
               { x = 1; }
               else x = 0;
           } while (x != 0);

       }


        public void transclosure(int[,] adjmat, int[,] path)
        {
            int i, j, k, N; N = path.GetLength(0);
            for (i = 0; i < N; i++)
            {
                for (j = 0; j < N; j++)
                { path[i, j] = adjmat[i, j]; }
            }
            for (i = 0; i < N; i++)
            {
                for (j = 0; j < N; j++)
                {
                    if (path[i, j] == 1)
                    {
                        for (k = 0; k < N; k++)
                        {
                            if (path[j, k] == 1) path[i, k] = 1;
                        }
                    }
                }
            }
        }



        public void OpticFlow2(Bitmap SonKare, Bitmap Onceki, double[,] Vx, double[,] Vy, double alfa, int iteration)
        {
            Bitmap bmp2 = (Bitmap)SonKare.Clone();
            Bitmap bmp1 = (Bitmap)Onceki.Clone();
            Color[] Once = new Color[9];
            Color[] Simdi = new Color[9];
            Color p9;

            int[] resim; resim = new int[9];
            double qx, qy, qt, Ix, Iy, It, hx, hy;
            double[] ax = new double[9];
            double[] ay = new double[9];
            double[] w = new double[9];
            w[0] = 1 / 12; w[1] = 1 / 6; w[2] = 1 / 12;
            w[3] = 1 / 6; w[8] = -1; w[4] = 1 / 6;
            w[5] = 1 / 12; w[6] = 1 / 6; w[7] = 1 / 12;

            int i, j, b, k, x, y;

            double[,] data1 = new double[bmp1.Width, bmp1.Height];
            double[,] data2 = new double[bmp1.Width, bmp1.Height];

            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                { data1[i, j] = 0; data2[i, j] = 0; }
            }

            hx = 0.0; hy = 0.0;
            for (k = 0; k < iteration; k++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    for (j = 0; j < bmp1.Height; j++)
                    {
                        for (b = 0; b <= 8; b++)
                        {
                            x = ikomsu(b, i); y = jkomsu(b, j);
                            resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                            if (resim[b] == 1)
                            {
                                p9 = bmp1.GetPixel(x, y); Once[b] = p9;
                                p9 = bmp2.GetPixel(x, y); Simdi[b] = p9;

                            }
                        }
                        qx = 0;
                        qx = (Once[4].R * resim[4] + Simdi[4].R * resim[4] + Once[7].R * resim[7] + Simdi[7].R * resim[7])
                              - (Once[8].R * resim[8] + Simdi[8].R * resim[8] + Once[6].R * resim[6] + Simdi[6].R * resim[6]);
                        Ix = qx / 4;

                        qy = 0;
                        qy = (Once[6].R * resim[6] + Simdi[6].R * resim[6] + Once[7].R * resim[7] + Simdi[7].R * resim[7])
                              - (Once[8].R * resim[8] + Simdi[8].R * resim[8] + Once[4].R * resim[4] + Simdi[4].R * resim[4]);
                        Iy = qy / 4;

                        qt = 0;
                        qt = (Simdi[8].R * resim[8] + Simdi[6].R * resim[6] + Simdi[4].R * resim[4] + Simdi[7].R * resim[7])
                              - (Once[8].R * resim[8] + Once[6].R * resim[6] + Once[4].R * resim[4] + Once[4].R * resim[4]);
                        It = qt / 4;

                        for (b = 0; b <= 8; b++)
                        {
                            x = ikomsu(b, i); y = jkomsu(b, j);
                            resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                            if (resim[b] == 1)
                            {
                                ax[b] = data1[x, y];
                                ay[b] = data2[x, y];
                            }
                        }

                        qx = 0; qy = 0;
                        for (b = 0; b <= 8; b++)
                        {
                            qx = qx + ax[b] * w[b] * resim[b];
                            qy = qy + ay[b] * w[b] * resim[b];
                        }

                        //  hx = vx[i, j]; hy = vy[i, j];
                        hx = qx - Ix * (Ix * hx + Iy * hy + It) / (alfa * alfa + Ix * Ix + Iy * Iy);
                        hy = qy - Iy * (Ix * hx + Iy * hy + It) / (alfa * alfa + Ix * Ix + Iy * Iy);
                        Vx[i, j] = hx; data1[i, j] = hx;
                        Vy[i, j] = hy; data2[i, j] = hy;

                    }
                }
            }
        }




        public Bitmap RegionA(Bitmap bmp1, int xm, int ym,bool pikselsecim)
        {   Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int x, y, n = data[xm, ym]; double mem;
            Color p9;

            if (pikselsecim == true)
            {   c1 = bmp1.GetPixel(xm, ym);
                for (x = 0; x < bmp1.Width; x++)
                {  for (y = 0; y < bmp1.Height; y++)
                    {    p9 = bmp1.GetPixel(x, y); mem = MemberShip3(p9, c1, 42, 3);
                        if (mem > memTresh) c1 = Color.FromArgb(0, 255, 0); else c1 = p9;
                        bmp2.SetPixel(x, y, c1);
                    }
                }
            }
            else MessageBox.Show("Önce Secim yapınız....!");           
            return bmp2;     
        }


        public Bitmap Regionb(Bitmap img, int[,] label, double Tmem)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            FileStream fs = new FileStream("c:\\Medpic\\komsu.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            int x, y, c, n, Di, resim; Color p9, c2;
            double mem0, mem1, mem2, mem3, mmax;
            bilgi[] bolbilgi;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }


            c = -1; Di = 42;
            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {

                    if (label[x, y] < 0)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        resim = resimdemi(x - 1, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x - 1, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x - 1, y - 1); mem0 = MemberShip3(p9, c2, Di, 3); }
                        else mem0 = -1.0;

                        resim = resimdemi(x, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x, y - 1); mem1 = MemberShip3(p9, c2, Di, 3); }
                        else mem1 = -1.0;

                        resim = resimdemi(x + 1, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x + 1, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x + 1, y - 1); mem2 = MemberShip3(p9, c2, Di, 3); }
                        else mem2 = -1.0;

                        resim = resimdemi(x - 1, y, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x - 1, y] >= 0)
                        { c2 = bmp1.GetPixel(x - 1, y); mem3 = MemberShip3(p9, c2, Di, 3); }
                        else mem3 = -1.0;


                        mmax = Math.Max(Math.Max(mem0, mem1), Math.Max(mem2, mem3));
                        if (mmax > Tmem)
                        {
                            if (mmax == mem0) label[x, y] = label[x - 1, y - 1];
                            else if (mmax == mem1) label[x, y] = label[x, y - 1];
                            else if (mmax == mem2) label[x, y] = label[x + 1, y - 1];
                            else if (mmax == mem3) label[x, y] = label[x - 1, y];
                        }
                        else { c = c + 1; label[x, y] = c; }
                    }
                }
            }
          

         
            for (y = 0; y < bmp1.Height; y++)
            {
                dosya.WriteLine("\n");
                for (x = 0; x < bmp1.Width; x++)
                {
                    dosya.Write("{0:N0}", label[x, y]);
                  

                }
            }
            dosya.Close();

            bolbilgi = new bilgi[c + 1];
            for (n = 0; n <= c; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 0;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                    bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                    bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                    bolbilgi[n].N = bolbilgi[n].N + 1;
                }
            }

            for (n = 0; n <= c; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / bolbilgi[n].N;
                bolbilgi[n].ga = bolbilgi[n].gt / bolbilgi[n].N;
                bolbilgi[n].ba = bolbilgi[n].bt / bolbilgi[n].N;
            }


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y]; p9 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                    bmp2.SetPixel(x, y, p9);
                }
            }
            return bmp2;

        }




        public Bitmap RegionbP(Bitmap img, int[,] label, double Tmem)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            int x, y, c, resim, n, m, Di; Color p9, c2;
            double mem, mem0, mem1, mem2, mem3, mmax;
            bilgi[] bolbilgi; int[,] K; int[,] T; int[] L;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            c = -1; Di = 42;
            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    if (label[x, y] < 0)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        resim = resimdemi(x - 1, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x - 1, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x - 1, y - 1); mem0 = MemberShip3(p9, c2, Di, 3); }
                        else mem0 = -1.0;
                        resim = resimdemi(x, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x, y - 1); mem1 = MemberShip3(p9, c2, Di, 3); }
                        else mem1 = -1.0;
                        resim = resimdemi(x + 1, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x + 1, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x + 1, y - 1); mem2 = MemberShip3(p9, c2, Di, 3); }
                        else mem2 = -1.0;
                        resim = resimdemi(x - 1, y, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x - 1, y] >= 0)
                        { c2 = bmp1.GetPixel(x - 1, y); mem3 = MemberShip3(p9, c2, Di, 3); }
                        else mem3 = -1.0;
                        mmax = Math.Max(Math.Max(mem0, mem1), Math.Max(mem2, mem3));
                        if (mmax > Tmem)
                        {
                            if (mmax == mem0) label[x, y] = label[x - 1, y - 1];
                            else if (mmax == mem1) label[x, y] = label[x, y - 1];
                            else if (mmax == mem2) label[x, y] = label[x + 1, y - 1];
                            else if (mmax == mem3) label[x, y] = label[x - 1, y];
                        }
                        else { c = c + 1; label[x, y] = c; }
                    }

                }
            }
          
            L = new int[c + 1]; K = new int[c + 1, c + 1]; T = new int[c + 1, c + 1]; bolbilgi = new bilgi[c + 1];
            for (n = 0; n <= c; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 0;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                    bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                    bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                    bolbilgi[n].N = bolbilgi[n].N + 1;
                }
            }

            for (n = 0; n <= c; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / bolbilgi[n].N;
                bolbilgi[n].ga = bolbilgi[n].gt / bolbilgi[n].N;
                bolbilgi[n].ba = bolbilgi[n].bt / bolbilgi[n].N;
            }


            for (m = 0; m <= c; m++)
            {
                for (n = 0; n <= c; n++)
                {
                    if (n == m)
                    { K[m, n] = 1; K[n, m] = 1; }
                    else
                    { K[m, n] = 0; K[n, m] = 0; }
                    T[n, m] = 0; T[m, n] = 0;
                    L[n] = n;
                }
            }



            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y];
                    resim = resimdemi(x - 1, y - 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x - 1, y - 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x, y - 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x, y - 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x + 1, y - 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x + 1, y - 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x - 1, y, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x - 1, y]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x + 1, y, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x + 1, y]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x - 1, y + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x - 1, y + 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x, y + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x, y + 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x + 1, y + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x + 1, y + 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                }
            }




            for (n = 0; n <= c; n++)
            { L[n] = -1; }

            for (m = 0; m <= c; m++)
            {
                for (n = 0; n <= c; n++)
                {
                    p9 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                    c2 = Color.FromArgb(bolbilgi[m].ra, bolbilgi[m].ga, bolbilgi[m].ba);
                    mem = MemberShip3(p9, c2, Di, 3);
                    if (K[m, n] == 1 && mem > Tmem)
                    { K[m, n] = 1; K[n, m] = 1; }
                    else
                    { K[m, n] = 0; K[n, m] = 0; }
                }
            }

            transclosure(K, T);

            x = 0;
            for (m = 0; m <= c; m++)
            {
                if (L[m] < 0)
                {
                    L[m] = x;
                    for (n = m; n <= c; n++)
                    {
                        if (L[n] < 0 && T[m, n] == 1)
                            L[n] = x;
                    }
                }
                x = x + 1;
            }






            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                { n = label[x, y]; label[x, y] = L[n]; }
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y]; c2 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                    bmp2.SetPixel(x, y, c2);
                }
            }
            return bmp2;
        }


        public Bitmap RegionbParam(Bitmap img, int[,] label, double Tmem, int merging)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            int x, y, c, resim, n, m, Di; Color p9, c2;
            double mem, mem0, mem1, mem2, mem3, mmax;
            bilgi[] bolbilgi; int[,] K; int[,] T; int[] L;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            c = -1; Di = 42;
            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    if (label[x, y] < 0)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        resim = resimdemi(x - 1, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x - 1, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x - 1, y - 1); mem0 = MemberShip3(p9, c2, Di, 3); }
                        else mem0 = -1.0;
                        resim = resimdemi(x, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x, y - 1); mem1 = MemberShip3(p9, c2, Di, 3); }
                        else mem1 = -1.0;
                        resim = resimdemi(x + 1, y - 1, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x + 1, y - 1] >= 0)
                        { c2 = bmp1.GetPixel(x + 1, y - 1); mem2 = MemberShip3(p9, c2, Di, 3); }
                        else mem2 = -1.0;
                        resim = resimdemi(x - 1, y, bmp1.Width, bmp1.Height);
                        if (resim == 1 && label[x - 1, y] >= 0)
                        { c2 = bmp1.GetPixel(x - 1, y); mem3 = MemberShip3(p9, c2, Di, 3); }
                        else mem3 = -1.0;
                        mmax = Math.Max(Math.Max(mem0, mem1), Math.Max(mem2, mem3));
                        if (mmax > Tmem)
                        {
                            if (mmax == mem0) label[x, y] = label[x - 1, y - 1];
                            else if (mmax == mem1) label[x, y] = label[x, y - 1];
                            else if (mmax == mem2) label[x, y] = label[x + 1, y - 1];
                            else if (mmax == mem3) label[x, y] = label[x - 1, y];
                        }
                        else { c = c + 1; label[x, y] = c; }
                    }

                }
            }


          
            L = new int[c + 1]; K = new int[c + 1, c + 1]; T = new int[c + 1, c + 1]; bolbilgi = new bilgi[c + 1];
            for (n = 0; n <= c; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 0;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                    bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                    bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                    bolbilgi[n].N = bolbilgi[n].N + 1;
                }
            }

            for (n = 0; n <= c; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / bolbilgi[n].N;
                bolbilgi[n].ga = bolbilgi[n].gt / bolbilgi[n].N;
                bolbilgi[n].ba = bolbilgi[n].bt / bolbilgi[n].N;
            }


            for (m = 0; m <= c; m++)
            {
                for (n = 0; n <= c; n++)
                {
                    if (n == m)
                    { K[m, n] = 1; K[n, m] = 1; }
                    else
                    { K[m, n] = 0; K[n, m] = 0; }
                    T[n, m] = 0; T[m, n] = 0;
                    L[n] = n;
                }
            }



            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y];
                    resim = resimdemi(x - 1, y - 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x - 1, y - 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x, y - 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x, y - 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x + 1, y - 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x + 1, y - 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x - 1, y, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x - 1, y]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x + 1, y, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x + 1, y]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x - 1, y + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x - 1, y + 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x, y + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x, y + 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                    resim = resimdemi(x + 1, y + 1, bmp1.Width, bmp1.Height);
                    if (resim == 1) { m = label[x + 1, y + 1]; if (n != m) { K[n, m] = 1; K[m, n] = 1; } }
                }
            }



            if (merging == 1)
            {
                for (m = 0; m <= c; m++)
                {
                    for (n = 0; n <= c; n++)
                    {
                        p9 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                        c2 = Color.FromArgb(bolbilgi[m].ra, bolbilgi[m].ga, bolbilgi[m].ba);
                        mem = MemberShip3(p9, c2, Di, 3);
                        if (mem > Tmem && K[n, m] == 1)
                        {
                            L[m] = Math.Min(L[m], L[n]);
                            L[n] = Math.Min(L[m], L[n]);
                        }
                    }
                }

            }

            if (merging == 2)
            {
                for (n = 0; n <= c; n++)
                { L[n] = -1; }

                for (m = 0; m <= c; m++)
                {
                    for (n = 0; n <= c; n++)
                    {
                        p9 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                        c2 = Color.FromArgb(bolbilgi[m].ra, bolbilgi[m].ga, bolbilgi[m].ba);
                        mem = MemberShip3(p9, c2, Di, 3);
                        if (K[m, n] == 1 && mem > Tmem)
                        { K[m, n] = 1; K[n, m] = 1; }
                        else
                        { K[m, n] = 0; K[n, m] = 0; }
                    }
                }

                transclosure(K, T);

                x = 0;
                for (m = 0; m <= c; m++)
                {
                    if (L[m] < 0)
                    {
                        L[m] = x;
                        for (n = m; n <= c; n++)
                        {
                            if (L[n] < 0 && T[m, n] == 1)
                                L[n] = x;
                        }
                    }
                    x = x + 1;
                }

            }

            if (merging == 3)
            {
                for (n = 0; n <= c; n++)
                { L[n] = -1; }

                for (m = 0; m <= c; m++)
                {
                    for (n = 0; n <= c; n++)
                    {
                        p9 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                        c2 = Color.FromArgb(bolbilgi[m].ra, bolbilgi[m].ga, bolbilgi[m].ba);
                        mem = MemberShip3(p9, c2, Di, 3);
                        if (mem > Tmem) { K[m, n] = 1; K[n, m] = 1; }
                        else { K[m, n] = 0; K[n, m] = 0; }
                    }
                }



                x = 0;
                for (m = 0; m <= c; m++)
                {
                    if (L[m] < 0)
                    {
                        L[m] = x;
                        for (n = m; n <= c; n++)
                        {
                            if (L[n] < 0 && K[m, n] == 1)
                                L[n] = x;
                        }
                    }
                    x = x + 1;
                }

            }


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                { n = label[x, y]; label[x, y] = L[n]; }
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y]; c2 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                    bmp2.SetPixel(x, y, c2);
                }
            }
            return bmp2;
        }








        public double Performans1(Bitmap img, int[,] label)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int x, y, min, max, counter, n; Color p9;
            double var, Gvar, Jn, J;
            bilgi[] bolbilgi;
            double[] bvaryans;
            int[] T = new int[3];

            getAAD(bmp1, T); Gvar = (double)T[0];

            min = label[0, 0]; max = label[0, 0];
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] < min) min = label[x, y];
                    if (label[x, y] > max) max = label[x, y];
                }
            }

            //  MessageBox.Show(max.ToString());
            MessageBox.Show(T[0].ToString());
            counter = 0;
            if (max > 0) counter = max + 1; else MessageBox.Show("etiketleme yok");

            bolbilgi = new bilgi[counter];
            bvaryans = new double[counter];

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 0;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    if (n >= 0)
                    {
                        bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                        bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                        bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                        bolbilgi[n].N = bolbilgi[n].N + 1;
                    }
                }
            }

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                bolbilgi[n].ga = bolbilgi[n].gt / (bolbilgi[n].N + 1);
                bolbilgi[n].ba = bolbilgi[n].bt / (bolbilgi[n].N + 1);
            }

            for (n = 0; n < counter; n++)
            { bvaryans[n] = 0; }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y];
                    if (n >= 0)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        var = Math.Abs(bolbilgi[n].ra - p9.R);
                        bvaryans[n] = bvaryans[n] + var;
                    }
                }
            }

            J = 0;
            for (n = 0; n < counter; n++)
            {
                bvaryans[n] = bvaryans[n] / (bolbilgi[n].N + 1);
                Jn = (double)bolbilgi[n].N / (bmp1.Width * bmp1.Height);
                Jn = Jn * bvaryans[n] / 255;      /// / (1 + bvaryans[n]/255);                   
                J = J + Jn;
            }
            J = 1 - J;
            return J;
        }

        public double Performans2(Bitmap img, int[,] label)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int x, y, min, max, counter, n; Color p9;
            double wi, mi, Jn, J, mT, zigT;
            bilgi[] bolbilgi;
            double[] bvaryans;
            int[] T = new int[3];


            getMean(bmp1, T); mT = (double)T[0];
            getVariance(bmp1, T); zigT = (double)T[0];

            min = label[0, 0]; max = label[0, 0];
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] < min) min = label[x, y];
                    if (label[x, y] > max) max = label[x, y];
                }
            }

            //  MessageBox.Show(max.ToString());
            MessageBox.Show(T[0].ToString());
            counter = 0;
            if (max > 0) counter = max + 1; else MessageBox.Show("etiketleme yok");

            bolbilgi = new bilgi[counter];
            bvaryans = new double[counter];

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 0;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    if (n >= 0)
                    {
                        bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                        bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                        bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                        bolbilgi[n].N = bolbilgi[n].N + 1;
                    }
                }
            }

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                bolbilgi[n].ga = bolbilgi[n].gt / (bolbilgi[n].N + 1);
                bolbilgi[n].ba = bolbilgi[n].bt / (bolbilgi[n].N + 1);
            }

            for (n = 0; n < counter; n++)
            { bvaryans[n] = 0; }

            J = 0;
            for (n = 0; n < counter; n++)
            {
                mi = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                wi = (double)bolbilgi[n].N / (bmp1.Width * bmp1.Height);
                Jn = wi * (mi - mT) * (mi - mT);
                J = J + Jn;
            }
            return J / zigT;
        }

        public double Performans3(Bitmap img, int[,] label)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int x, y, min, max, counter, n; Color p9, center;
            double var, gMAD, Jn, J;
            bilgi[] bolbilgi;
            double[] bMAD;

            gMAD = getAAD3(bmp1) / 255;
            min = label[0, 0]; max = label[0, 0];
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] < min) min = label[x, y];
                    if (label[x, y] > max) max = label[x, y];
                }
            }

            //  MessageBox.Show(max.ToString());         
            MessageBox.Show(gMAD.ToString());
            counter = 0;
            if (max > 0) counter = max + 1; else MessageBox.Show("etiketleme yok");

            bolbilgi = new bilgi[counter];
            bMAD = new double[counter];

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 0;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    if (n >= 0)
                    {
                        bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                        bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                        bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                        bolbilgi[n].N = bolbilgi[n].N + 1;
                    }
                }
            }

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                bolbilgi[n].ga = bolbilgi[n].gt / (bolbilgi[n].N + 1);
                bolbilgi[n].ba = bolbilgi[n].bt / (bolbilgi[n].N + 1);
            }

            for (n = 0; n < counter; n++)
            { bMAD[n] = 0; }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y];
                    if (n >= 0)
                    {
                        p9 = bmp1.GetPixel(x, y); center = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                        var = Distance3(p9, center);
                        bMAD[n] = bMAD[n] + var;
                    }
                }
            }

            J = 0;
            for (n = 0; n < counter; n++)
            {
                bMAD[n] = bMAD[n] / (bolbilgi[n].N + 1);
                Jn = (double)bolbilgi[n].N / (bmp1.Width * bmp1.Height);
                Jn = Jn * bMAD[n] / 255;
                J = J + Jn;
            }
            //J = 1 - J;
            J = (gMAD - J) / gMAD;
            return J;
        }


        public double Performans4(Bitmap img, int[,] label)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int x, y, min, max, counter, n; Color p9, center;
            double e, Jn, J, a;
            bilgi[] bolbilgi;
            double[] ekare;


            min = label[0, 0]; max = label[0, 0];
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] < min) min = label[x, y];
                    if (label[x, y] > max) max = label[x, y];
                }
            }

            counter = 0;
            if (max > 0) counter = max + 1; else MessageBox.Show("etiketleme yok");

            bolbilgi = new bilgi[counter];
            ekare = new double[counter];

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 1;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    if (n >= 0)
                    {
                        bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                        bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                        bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                        bolbilgi[n].N = bolbilgi[n].N + 1;
                    }
                }
            }

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                bolbilgi[n].ga = bolbilgi[n].gt / (bolbilgi[n].N + 1);
                bolbilgi[n].ba = bolbilgi[n].bt / (bolbilgi[n].N + 1);
            }

            for (n = 0; n < counter; n++)
            { ekare[n] = 0; }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y];
                    if (n >= 0)
                    {
                        p9 = bmp1.GetPixel(x, y); center = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                        e = Distance3(p9, center);
                        ekare[n] = ekare[n] + e * e;
                    }
                }
            }

            J = 0;
            for (n = 0; n < counter; n++)
            {
                a = Math.Sqrt(bolbilgi[n].N);                // a = 1 + Math.Log10((bolbilgi[n].N));
                Jn = (double)ekare[n] / a;
                J = J + Jn;
            }
            a = Math.Sqrt(counter);
            e = (double)img.Width * (double)img.Height;
            J = a * J / (1000 * e);
            return J;
        }


        public double Performans5(Bitmap img, int[,] label)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int x, y, min, max, counter, n; Color p9, center;
            double e, Jn, J, a;
            bilgi[] bolbilgi;
            double[] ekare;


            min = label[0, 0]; max = label[0, 0];
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] < min) min = label[x, y];
                    if (label[x, y] > max) max = label[x, y];
                }
            }

            counter = 0;
            if (max > 0) counter = max + 1; else MessageBox.Show("etiketleme yok");

            bolbilgi = new bilgi[counter];
            ekare = new double[counter];

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 1;
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); n = label[x, y];
                    if (n >= 0)
                    {
                        bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                        bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                        bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                        bolbilgi[n].N = bolbilgi[n].N + 1;
                    }
                }
            }

            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].ra = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                bolbilgi[n].ga = bolbilgi[n].gt / (bolbilgi[n].N + 1);
                bolbilgi[n].ba = bolbilgi[n].bt / (bolbilgi[n].N + 1);
            }

            for (n = 0; n < counter; n++)
            { ekare[n] = 0; }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y];
                    if (n >= 0)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        center = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba);
                        e = Distance3(p9, center);
                        ekare[n] = ekare[n] + e * e;
                    }
                }
            }

            J = 0;
            for (n = 0; n < counter; n++)
            { J = J + (double)ekare[n]; }
            J = J / ((double)bmp1.Width + (double)bmp1.Height);
            return J;
        }

        public void RegionFilter(int[,] label, int MaskSize)
        {
            int W, H; W = label.GetLength(0); H = label.GetLength(1);
            int[,] data1; data1 = new int[W, H];
            int i, j, x, y, resim, m, b, ip, jp, max, M;

            if (MaskSize == 3) { m = 1; M = 9; }
            else if (MaskSize == 5) { m = 2; M = 25; }
            else if (MaskSize == 7) { m = 3; M = 49; }
            else { m = 0; M = 1; }

            int[] h; h = new int[M];
            int[] T; T = new int[M];

            if (m != 0)
            {
                for (j = 0; j < H; j++)
                {
                    for (i = 0; i < W; i++)
                    {
                        b = 0;
                        for (y = -m; y <= m; y++)
                        {
                            for (x = -m; x <= m; x++)
                            {
                                ip = i + x; jp = j + y;
                                resim = resimdemi(ip, jp, W, H);
                                if (resim == 1) h[b] = label[ip, jp]; else h[b] = -1;
                                b = b + 1;
                            }
                        }


                        for (y = 0; y < M; y++)
                        {
                            T[y] = 0;
                            for (x = 0; x < M; x++)
                            { if (h[x] == h[y]) T[y] = T[y] + 1; }
                        }

                        max = T[0]; b = 0;
                        for (y = 0; y < M; y++)
                        { if (T[y] > max) { max = T[y]; b = y; } }

                        label[i, j] = h[b];
                    }
                }
            }

        }


        public int RegionFilter2(int[,] label, int MaskSize)
        {
            int W, H; W = label.GetLength(0); H = label.GetLength(1);
            int[,] data1; data1 = new int[W, H];
            int i, j, k, x, y, resim, m, n, b, ip, jp, max, M, min, counter;

            if (MaskSize == 3) { m = 1; M = 9; }
            else if (MaskSize == 5) { m = 2; M = 25; }
            else if (MaskSize == 7) { m = 3; M = 49; }
            else { m = 0; M = 1; }

            int[] h; h = new int[M];
            int[] T; T = new int[M];

            if (m != 0)
            {
                for (j = 0; j < H; j++)
                {
                    for (i = 0; i < W; i++)
                    {
                        b = 0;
                        for (y = -m; y <= m; y++)
                        {
                            for (x = -m; x <= m; x++)
                            {
                                ip = i + x; jp = j + y;
                                resim = resimdemi(ip, jp, W, H);
                                if (resim == 1) h[b] = label[ip, jp]; else h[b] = -1;
                                b = b + 1;
                            }
                        }


                        for (y = 0; y < M; y++)
                        {
                            T[y] = 0;
                            for (x = 0; x < M; x++)
                            { if (h[x] == h[y]) T[y] = T[y] + 1; }
                        }

                        max = T[0]; b = 0;
                        for (y = 0; y < M; y++)
                        { if (T[y] > max) { max = T[y]; b = y; } }
                        label[i, j] = h[b];
                    }
                }
            }



            min = label[0, 0]; max = label[0, 0];
            for (x = 0; x < W; x++)
            {
                for (y = 0; y < H; y++)
                {
                    if (label[x, y] < min) min = label[x, y];
                    if (label[x, y] > max) max = label[x, y];
                }
            }


            counter = 0;
            if (max > 0) counter = max + 1;

            int[] Tb; Tb = new int[counter];

            for (y = 0; y < H; y++)
            {
                for (x = 0; x < W; x++)
                {
                    n = label[x, y]; if (n >= 0) Tb[n] = n;
                }
            }


            max = 0;
            for (n = 0; n < counter; n++)
            {
                if (Tb[n] >= 0)
                { Tb[n] = max; max = max + 1; }
            }


            for (y = 0; y < H; y++)
            {
                for (x = 0; x < W; x++)
                {
                    n = label[x, y];
                    if (n >= 0)
                    { k = Tb[n]; label[x, y] = k; }

                }
            }

            return max + 1;

        }


        public Bitmap BolgeGoster2(Bitmap img, int[,] label, int MaskSize, int ftipi, Color edge)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            int x, y, min, max, resim, counter, n, m, b, k, p, xk, yk; Color p9;
            bilgi[] bolbilgi; int[] T;

            RegionFilter2(label, MaskSize);


            min = label[0, 0]; max = label[0, 0];
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] < min) min = label[x, y];
                    if (label[x, y] > max) max = label[x, y];
                }
            }

            MessageBox.Show(max.ToString());
            counter = 0;
            if (max > 0) counter = max + 2; else MessageBox.Show("etiketleme yok");

            bolbilgi = new bilgi[counter];
            T = new int[counter];


            for (n = 0; n < counter; n++)
            {
                bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                bolbilgi[n].N = 0; T[n] = -1;
            }


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y]; if (n >= 0) T[n] = n;
                }
            }


            max = 0;
            for (n = 0; n < counter; n++)
            {
                if (T[n] >= 0)
                { T[n] = max; max = max + 1; }
            }

            MessageBox.Show(max.ToString());
            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    n = label[x, y];
                    if (n >= 0)
                    { k = T[n]; label[x, y] = k; }

                }
            }


            if (ftipi == 1)
            {

                for (y = 0; y < bmp1.Height; y++)
                {
                    for (x = 0; x < bmp1.Width; x++)
                    {
                        p9 = bmp1.GetPixel(x, y); n = label[x, y];
                        if (n >= 0)
                        {
                            bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                            bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                            bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                            bolbilgi[n].N = bolbilgi[n].N + 1;
                        }
                    }
                }

                for (n = 0; n < counter; n++)
                {
                    bolbilgi[n].ra = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                    bolbilgi[n].ga = bolbilgi[n].gt / (bolbilgi[n].N + 1);
                    bolbilgi[n].ba = bolbilgi[n].bt / (bolbilgi[n].N + 1);
                }


                for (y = 0; y < bmp1.Height; y++)
                {
                    for (x = 0; x < bmp1.Width; x++)
                    {
                        n = label[x, y];
                        if (n >= 0)
                        { p9 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba); bmp2.SetPixel(x, y, p9); }
                        else
                            bmp2.SetPixel(x, y, edge);
                    }
                }
            }


            if (ftipi == 0)
            {

                for (y = 0; y < bmp1.Height; y++)
                {
                    for (x = 0; x < bmp1.Width; x++)
                    {
                        n = label[x, y]; k = 0;

                        if (n >= 0)
                        {
                            for (b = 0; b < 8; b++)
                            {
                                xk = ikomsu(b, x); yk = jkomsu(b, y);
                                resim = resimdemi(xk, yk, bmp1.Width, bmp1.Height);
                                if (resim == 1)
                                { m = label[xk, yk]; if (m == n) k = k + 1; }
                            }

                            if (k != 8) p9 = edge; else p9 = bmp1.GetPixel(x, y); bmp2.SetPixel(x, y, p9);
                        }
                        // else bmp2.SetPixel(x, y, Color.FromArgb(255 - edge.R, 255 - edge.G, 255 - edge.B));

                    }
                }
            }

            return bmp2;
        }



        public Bitmap BolgeGoster(Bitmap img, int[,] label, int MaskSize, int ftipi, Color edge)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            int x, y, min, max, resim, counter, n, m, b, k, p, xk, yk; Color p9;
            bilgi[] bolbilgi;

            RegionFilter(label, MaskSize);

            if (ftipi == 1)
            {
                min = label[0, 0]; max = label[0, 0];
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        if (label[x, y] < min) min = label[x, y];
                        if (label[x, y] > max) max = label[x, y];
                    }
                }

                MessageBox.Show(max.ToString());
                counter = 0;
                if (max > 0) counter = max + 2; else MessageBox.Show("etiketleme yok");

                bolbilgi = new bilgi[counter];

                for (n = 0; n < counter; n++)
                {
                    bolbilgi[n].rt = 0; bolbilgi[n].gt = 0; bolbilgi[n].gt = 0;
                    bolbilgi[n].ra = 0; bolbilgi[n].ga = 0; bolbilgi[n].ga = 0;
                    bolbilgi[n].N = 0;
                }

                for (y = 0; y < bmp1.Height; y++)
                {
                    for (x = 0; x < bmp1.Width; x++)
                    {
                        p9 = bmp1.GetPixel(x, y); n = label[x, y];
                        if (n >= 0)
                        {
                            bolbilgi[n].rt = bolbilgi[n].rt + p9.R;
                            bolbilgi[n].gt = bolbilgi[n].gt + p9.G;
                            bolbilgi[n].bt = bolbilgi[n].bt + p9.B;
                            bolbilgi[n].N = bolbilgi[n].N + 1;
                        }
                    }
                }

                for (n = 0; n < counter; n++)
                {
                    bolbilgi[n].ra = bolbilgi[n].rt / (bolbilgi[n].N + 1);
                    bolbilgi[n].ga = bolbilgi[n].gt / (bolbilgi[n].N + 1);
                    bolbilgi[n].ba = bolbilgi[n].bt / (bolbilgi[n].N + 1);
                }


                for (y = 0; y < bmp1.Height; y++)
                {
                    for (x = 0; x < bmp1.Width; x++)
                    {
                        n = label[x, y];
                        if (n >= 0)
                        { p9 = Color.FromArgb(bolbilgi[n].ra, bolbilgi[n].ga, bolbilgi[n].ba); bmp2.SetPixel(x, y, p9); }
                        else
                            bmp2.SetPixel(x, y, edge);
                    }
                }
            }


            if (ftipi == 0)
            {

                for (y = 0; y < bmp1.Height; y++)
                {
                    for (x = 0; x < bmp1.Width; x++)
                    {
                        n = label[x, y]; k = 0;

                        if (n >= 0)
                        {
                            for (b = 0; b < 8; b++)
                            {
                                xk = ikomsu(b, x); yk = jkomsu(b, y);
                                resim = resimdemi(xk, yk, bmp1.Width, bmp1.Height);
                                if (resim == 1)
                                { m = label[xk, yk]; if (m == n) k = k + 1; }
                            }

                            if (k != 8) p9 = edge; else p9 = bmp1.GetPixel(x, y); bmp2.SetPixel(x, y, p9);
                        }
                        // else bmp2.SetPixel(x, y, Color.FromArgb(255 - edge.R, 255 - edge.G, 255 - edge.B));

                    }
                }
            }

            return bmp2;
        }


        public Bitmap BolgegosterEski(Bitmap img, int[,] label, Color edge)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            int x, y, resim, n, m, b, k, p, xk, yk;

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    k = 0; p = 0; n = label[x, y];

                    for (b = 0; b < 8; b++)
                    {
                        xk = ikomsu(b, x);
                        yk = jkomsu(b, y);
                        resim = resimdemi(xk, yk, bmp1.Width, bmp1.Height);
                        if (resim == 1)
                        { m = label[xk, yk]; k = k + 1; if (n == m) p = p + 1; }
                    }

                    if (k != p) c1 = edge; else c1 = bmp1.GetPixel(x, y);

                    bmp2.SetPixel(x, y, c1);
                }
            }
            return bmp2;

        }


        public Bitmap Erosion(Bitmap img)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            Color p9, c2;
            int x, y, xk, yk, b, resim, t, m;
            int[] w = new int[9];

            w[0] = 1; w[1] = 1; w[2] = 1;
            w[3] = 1; w[8] = 1; w[4] = 1;
            w[5] = 1; w[6] = 1; w[7] = 1;



            c2 = Color.FromArgb(0, 0, 0);
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    t = 0;
                    for (b = 0; b < 9; b++)
                    {
                        xk = ikomsu(b, x); yk = jkomsu(b, y);
                        resim = resimdemi(xk, yk, bmp1.Width, bmp1.Height);
                        if (resim == 1)
                        {
                            p9 = bmp1.GetPixel(x, y);
                            if (w[b] == 1 && p9.R == 255) m = 1; else m = 0;
                            t = t + m;
                        }
                    }

                    if (t == 9) c2 = Color.FromArgb(255, 255, 255); else c2 = Color.FromArgb(0, 0, 0);
                    bmp2.SetPixel(x, y, c2);
                }
            }
            return bmp2;
        }




        public Bitmap RegionGoster(Bitmap img, int[] clabel, Color kalan)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            Color p9, newColor;
            int x, y, n, k, cgercek;

            cgercek = clabel[0];
            for (n = 0; n < clabel.Length; n++)
            { if (clabel[n] > cgercek)  cgercek = clabel[n]; }

           
            cgercek = cgercek + 1;

            double[] r; r = new double[cgercek];
            double[] psayisi; psayisi = new double[cgercek];
            Color[] c; c = new Color[cgercek];

            for (n = 0; n < cgercek; n++)
            { c[n] = Color.FromArgb(0, 0, 0); r[n] = 0; psayisi[n] = 0; }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); k = p9.R; n = clabel[k];
                    if (n >= 0)
                    { r[n] = r[n] + p9.R; psayisi[n] = psayisi[n] + 1; }
                }
            }
            for (n = 0; n < cgercek; n++)
            { if (r[n] > 0) r[n] = r[n] / psayisi[n]; c[n] = Color.FromArgb((int)r[n], (int)r[n], (int)r[n]); }


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); k = p9.R; n = clabel[k];
                    if (n >= 0) newColor = c[n]; else newColor = kalan;
                    bmp2.SetPixel(x, y, newColor);
                }
            }
            return bmp2;

        }

        public Bitmap RegionGoster3(Bitmap img, int[, ,] clabel, Color kalan)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            Color p9, newColor;
            int x, y, n, i, j, k, cgercek;


            cgercek = clabel[0, 0, 0];
            for (i = 0; i < 256; i++)
            {
                for (j = 0; j < 256; j++)
                {
                    for (k = 0; k < 256; k++)
                    {
                        if (clabel[i, j, k] > cgercek) cgercek = clabel[i, j, k];
                    }
                }
            }

          
            cgercek = cgercek + 1;

            double[] r; r = new double[cgercek];
            double[] g; g = new double[cgercek];
            double[] b; b = new double[cgercek];
            double[] psayisi; psayisi = new double[cgercek];
            Color[] c; c = new Color[cgercek];

            for (n = 0; n < cgercek; n++)
            { c[n] = Color.FromArgb(0, 0, 0); r[n] = 0; psayisi[n] = 0; }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); i = p9.R; j = p9.G; k = p9.B; n = clabel[i, j, k];
                    if (n >= 0)
                    {
                        r[n] = r[n] + p9.R; g[n] = g[n] + p9.G; b[n] = b[n] + p9.B;
                        psayisi[n] = psayisi[n] + 1;
                    }
                }
            }
            for (n = 0; n < cgercek; n++)
            {
                if (r[n] > 0) r[n] = r[n] / psayisi[n];
                if (g[n] > 0) g[n] = g[n] / psayisi[n];
                if (b[n] > 0) b[n] = b[n] / psayisi[n];
                c[n] = Color.FromArgb((int)r[n], (int)g[n], (int)b[n]);
            }


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y); i = p9.R; j = p9.G; k = p9.B; n = clabel[i, j, k];
                    if (n >= 0) newColor = c[n]; else newColor = kalan;
                    bmp2.SetPixel(x, y, newColor);
                }
            }
            return bmp2;

        }


        public Bitmap Region1DH(Bitmap img, int[,] label, int[] clabel, int cluster,  double Tsim, int uyarla, Color kalan)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            Color p9, newColor; newColor = Color.FromArgb(0, 0, 0);
            Color[] c; c = new Color[cluster];
            double[] m; m = new double[cluster];
            double[] r; r = new double[cluster];
            double[] psayisi; psayisi = new double[cluster];
            double[] hr, hi; hr = new double[256]; hi = new double[256];
            double mem, mmax, z, zig, yuzde, ygiden;
            int x, y, i, j, k, n, Tr, cgercek, konT;

            for (i = 0; i < 256; i++)
            { hr[i] = 0; hi[i] = 0; clabel[i] = -1; }

            for (n = 0; n < cluster; n++)
            { c[n] = Color.FromArgb(0, 0, 0); m[n] = 0; r[n] = 0; psayisi[n] = 0; }

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { p9 = bmp1.GetPixel(x, y); hr[p9.R] = (double)hr[p9.R] + 1; }
            }

            for (i = 0; i < 256; i++)
            { hr[i] = (double)(hr[i]) / (bmp1.Width * bmp1.Height); }


            n = 0; cgercek = 0; yuzde = 100;
            do
            {

                Tr = 0; mmax = 0;
                for (i = 0; i < 256; i++)
                {
                    if (hr[i] > mmax)
                    { mmax = hr[i]; Tr = i; }
                }

              zig = 42;

                ygiden = 0;
                for (i = 0; i < 256; i++)
                {
                    if (clabel[i] < 0)
                    {
                        z = (double)(i - Tr) * (i - Tr) / (2 * zig * zig); mem = Math.Exp(-z);
                        if (mem > Tsim)
                        { ygiden = ygiden + 100 * hr[i]; cgercek = n; clabel[i] = n; hr[i] = 0; c[n] = Color.FromArgb(Tr, Tr, Tr); }
                    }
                }
                n = n + 1; yuzde = yuzde - ygiden;
            } while (n < cluster && yuzde >= 1);

           

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y); n = clabel[p9.R];
                    if (n >= 0) { psayisi[n] = psayisi[n] + 1; r[n] = r[n] + p9.R; }
                }
            }

            for (n = 0; n < cgercek; n++)
            {
                if (psayisi[n] > 0)
                {
                    z = (double)psayisi[n]; mem = (double)r[n]; mem = mem / z;
                    c[n] = Color.FromArgb((int)mem, (int)mem, (int)mem);
                }
            }

            if (uyarla == 1)
            {
                for (i = 0; i < 256; i++)
                {
                    if (clabel[i] < 0)
                    {
                        for (n = 0; n < cgercek; n++)
                        {
                            Tr = c[n].R; zig = 42;
                            z = (double)(i - Tr) * (i - Tr) / (2 * zig * zig);
                            mem = Math.Exp(-z); m[n] = mem;
                        }

                        Tr = 0; mmax = m[0];
                        for (n = 0; n < cgercek; n++)
                        {
                            if (m[n] > mmax)
                            { mmax = m[n]; Tr = n; }
                        }
                        clabel[i] = Tr;
                    }
                }

            }

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y); n = clabel[p9.R]; label[x, y] = n;
                    if (n >= 0) newColor = c[n]; else newColor = kalan;
                    bmp2.SetPixel(x, y, newColor);
                }
            }

            return bmp2;
        }



        public Bitmap RegionTDH(Bitmap img, int[,] label, int cluster, double Tsim, int uyarla, Color kalan)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            Color p9, newColor; newColor = Color.FromArgb(0, 0, 0);
            Color[] c = new Color[cluster];
            int[] r; r = new int[cluster];
            int[] g; g = new int[cluster];
            int[] b; b = new int[cluster];
            int[] psayisi; psayisi = new int[cluster];
            double[] memn; memn = new double[cluster];
            double mem, zig, eb, or, og, ob, yuzde;
            int x, y, i, j, k, n, cgercek;
            int[, ,] M; M = new int[256, 256, 256];


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            for (n = 0; n < cluster; n++)
            { psayisi[n] = 0; r[n] = 0; g[n] = 0; b[n] = 0; c[n] = Color.FromArgb(0, 0, 0); }

            n = 0; yuzde = 100; cgercek = 0; zig = 40;
            do
            {
                for (i = 0; i < 256; i++)
                {
                    for (j = 0; j < 256; j++)
                    {
                        for (k = 0; k < 256; k++)
                        { M[i, j, k] = 0; }
                    }
                }

                i = 0; newColor = Color.FromArgb(0, 0, 0);
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        if (label[x, y] < 0)
                        {
                            p9 = bmp1.GetPixel(x, y); M[p9.R, p9.G, p9.B] = M[p9.R, p9.G, p9.B] + 1; j = M[p9.R, p9.G, p9.B];
                            if (j > i) { i = j; newColor = p9; }
                        }
                    }
                }


                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        if (label[x, y] < 0)
                        {
                            p9 = bmp1.GetPixel(x, y); mem = MemberShip3(p9, newColor, (int)zig, 3);
                            if (mem > Tsim)
                            {
                                cgercek = n; label[x, y] = n; c[n] = newColor; M[p9.R, p9.G, p9.B] = 0;
                                psayisi[n] = psayisi[n] + 1; r[n] = r[n] + p9.R; g[n] = g[n] + p9.G; b[n] = b[n] + p9.B;
                            }
                        }
                    }
                }
                mem = 100 * psayisi[n] / (bmp1.Width * bmp1.Height); yuzde = yuzde - mem;
                n = n + 1;
            } while (n < cluster && yuzde >= 2);


            or = 0; og = 0; ob = 0;
            for (n = 0; n < cgercek; n++)
            {
                if (psayisi[n] > 0)
                {
                    eb = (double)psayisi[n];
                    mem = (double)r[n]; or = mem / eb; mem = (double)g[n]; og = mem / eb; mem = (double)b[n]; ob = mem / eb;
                    c[n] = Color.FromArgb((int)or, (int)og, (int)ob);
                }
            }


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] >= 0)
                    { n = label[x, y]; newColor = c[n]; }
                    else
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (n = 0; n < cgercek; n++)
                        { memn[n] = MemberShip3(p9, c[n], 42, 3); }

                        eb = memn[0]; k = 0;
                        for (n = 0; n < cgercek; n++)
                        { if (memn[n] > eb) { eb = memn[n]; k = n; } }

                        if (uyarla == 1) { newColor = c[k]; label[x, y] = k; } else newColor = kalan;
                    }
                    bmp2.SetPixel(x, y, newColor);
                }
            }
            return bmp2;
        }



        public Color ColorHistoMax(Bitmap img)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int[, ,] hr; hr = new int[256, 256, 256];
            int x, y, i, j, k; Color p9, newColor;

            for (i = 0; i < 256; i++)
            {
                for (j = 0; j < 256; j++)
                {
                    for (k = 0; k < 256; k++)
                    { hr[i, j, k] = 0; }
                }
            }

            i = 0; newColor = Color.FromArgb(0, 0, 0);
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y); hr[p9.R, p9.G, p9.B] = hr[p9.R, p9.G, p9.B] + 1; j = hr[p9.R, p9.G, p9.B];
                    if (j > i) { i = j; newColor = p9; }
                }
            }
            return newColor;
        }



        public Color ColorHistoMaxP(Bitmap img, int[,] label)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            int[, ,] hr; hr = new int[256, 256, 256];
            int x, y, i, j, k; Color p9, newColor;

            for (i = 0; i < 256; i++)
            {
                for (j = 0; j < 256; j++)
                {
                    for (k = 0; k < 256; k++)
                    { hr[i, j, k] = 0; }
                }
            }

            i = 0; newColor = Color.FromArgb(0, 0, 0);
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    if (label[x, y] < 0)
                    {
                        p9 = bmp1.GetPixel(x, y); hr[p9.R, p9.G, p9.B] = hr[p9.R, p9.G, p9.B] + 1; j = hr[p9.R, p9.G, p9.B];
                        if (j > i) { i = j; newColor = p9; }
                    }
                }
            }
            return newColor;
        }







        public Bitmap fcm(Bitmap img, int[,] label, int cluster, int t, double m)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            double[, ,] u = new double[bmp1.Width, bmp1.Height, cluster];
            double[, ,] d = new double[bmp1.Width, bmp1.Height, cluster];
            Color p9; double J, dr, dr1, payda;
            int x, y, j, k, z;
            double[] mem; mem = new double[cluster];
            Color[] c; c = new Color[cluster];
            double[] r, g, b; r = new double[cluster]; g = new double[cluster]; b = new double[cluster];
            int[] psayisi; psayisi = new int[cluster];


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            Random ur = new Random();
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    for (j = 0; j < cluster; j++)
                    { dr = ur.NextDouble(); u[x, y, j] = dr; }
                }
            }


            Random xr = new Random();
            for (j = 0; j < cluster; j++)
            {
                x = xr.Next(0, 255); y = xr.Next(0, 255); z = xr.Next(0, 255);
                c[j] = Color.FromArgb(x, y, z);
            }



            /*
            for (j = 0; j < cluster; j++)
            { c[j] = Color.FromArgb(0, 0, 0); r[j] = 0;  }
            ColorHistoMax1(bmp1, c, r, 0.90);             
            p9 = ColorHistoMax(bmp1);
            for (j = 0; j < cluster; j++)
            { c[j] =p9; }
            */

            z = 0;
            do
            {
                for (j = 0; j < cluster; j++)
                { r[j] = 0; g[j] = 0; b[j] = 0; mem[j] = 0; }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            r[j] = r[j] + (double)(p9.R * Math.Pow(u[x, y, j], m));
                            g[j] = g[j] + (double)(p9.G * Math.Pow(u[x, y, j], m));
                            b[j] = b[j] + (double)(p9.B * Math.Pow(u[x, y, j], m));
                            mem[j] = mem[j] + Math.Pow(u[x, y, j], m);
                        }
                    }
                }

                for (j = 0; j < cluster; j++)
                { r[j] = r[j] / mem[j]; g[j] = g[j] / mem[j]; b[j] = b[j] / mem[j]; }

                for (j = 0; j < cluster; j++)
                { c[j] = Color.FromArgb((int)r[j], (int)g[j], (int)b[j]); }



                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            dr = Distance3(p9, c[j]);
                            payda = 0;
                            for (k = 0; k < cluster; k++)
                            {
                                dr1 = Distance3(p9, c[k]);
                                if (dr1 == 0) dr1 = 0.001;
                                dr1 = Math.Pow(dr / dr1, 2 / (m - 1));  //if (j == k) dr1 = 0;
                                payda = payda + dr1;
                            }
                            if (payda == 0) payda = 0.001;
                            u[x, y, j] = 1 / payda;

                        }
                    }
                }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        { d[x, y, j] = Distance3(p9, c[j]); }
                    }
                }

                J = 0;
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        for (j = 0; j < cluster; j++)
                        {
                            dr = d[x, y, j];
                            J = J + dr * dr * Math.Pow(u[x, y, j], m);
                        }
                    }
                }
               
                z++;
            } while (z < t);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    for (j = 0; j < cluster; j++)
                    { mem[j] = u[x, y, j]; }
                    payda = mem[0]; z = 0;
                    for (k = 0; k < cluster; k++)
                    { if (mem[k] > payda) { payda = mem[k]; z = k; } }

                    p9 = c[z]; bmp2.SetPixel(x, y, p9); label[x, y] = z;
                }
            }

            return bmp2;
        }




        public Bitmap fcm2(Bitmap img, int[,] label, int cluster, Color[] renkler, int t, double m)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            double[, ,] u = new double[bmp1.Width, bmp1.Height, cluster];
            double[, ,] d = new double[bmp1.Width, bmp1.Height, cluster];
            Color p9; double J, dr, dr1, payda;
            int x, y, j, k, z;
            double[] mem; mem = new double[cluster];
            Color[] c; c = new Color[cluster];
            double[] r, g, b; r = new double[cluster]; g = new double[cluster]; b = new double[cluster];
            int[] psayisi; psayisi = new int[cluster];


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            Random ur = new Random();
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    for (j = 0; j < cluster; j++)
                    { dr = ur.NextDouble(); u[x, y, j] = dr; }
                }
            }


            Random xr = new Random();
            for (j = 0; j < cluster; j++)
            {
                x = xr.Next(0, 255); y = xr.Next(0, 255); z = xr.Next(0, 255);
                c[j] = Color.FromArgb(x, y, z);
            }




            /*
           for (j = 0; j < cluster; j++)
           { c[j] = Color.FromArgb(0, 0, 0);  }
           ColorHistoMax1(bmp1, c, r, 0.90);             
         */

            

            z = 0;
            do
            {


                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            dr = Distance3(p9, c[j]);
                            payda = 0;
                            for (k = 0; k < cluster; k++)
                            {
                                dr1 = Distance3(p9, c[k]);
                                if (dr1 == 0) dr1 = 0.001;
                                dr1 = Math.Pow(dr / dr1, 2 / (m - 1));  //if (j == k) dr1 = 0;
                                payda = payda + dr1;
                            }
                            if (payda == 0) payda = 0.001;
                            u[x, y, j] = 1 / payda;

                        }
                    }
                }


                for (j = 0; j < cluster; j++)
                { r[j] = 0; g[j] = 0; b[j] = 0; mem[j] = 0; }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            r[j] = r[j] + (double)(p9.R * Math.Pow(u[x, y, j], m));
                            g[j] = g[j] + (double)(p9.G * Math.Pow(u[x, y, j], m));
                            b[j] = b[j] + (double)(p9.B * Math.Pow(u[x, y, j], m));
                            mem[j] = mem[j] + Math.Pow(u[x, y, j], m);
                        }
                    }
                }

                for (j = 0; j < cluster; j++)
                { r[j] = r[j] / mem[j]; g[j] = g[j] / mem[j]; b[j] = b[j] / mem[j]; }

                for (j = 0; j < cluster; j++)
                {
                    c[j] = Color.FromArgb((int)r[j], (int)g[j], (int)b[j]);
                    renkler[j] = c[j];
                }





                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        { d[x, y, j] = Distance3(p9, c[j]); }
                    }
                }

                J = 0;
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        for (j = 0; j < cluster; j++)
                        {
                            dr = d[x, y, j];
                            J = J + dr * dr * Math.Pow(u[x, y, j], m);
                        }
                    }
                }
            
                z++;
            } while (z < t);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    for (j = 0; j < cluster; j++)
                    { mem[j] = u[x, y, j]; }
                    payda = mem[0]; z = 0;
                    for (k = 0; k < cluster; k++)
                    { if (mem[k] > payda) { payda = mem[k]; z = k; } }

                    p9 = c[z];
                    p9 = Color.FromArgb((int)(255 - p9.R), (int)(p9.G), (int)(255 - p9.B));
                    bmp2.SetPixel(x, y, p9); label[x, y] = z;
                }
            }

            return bmp2;
        }



        public Bitmap Kmeans(Bitmap img, int[,] label, int cluster, int t)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            double[, ,] d = new double[bmp1.Width, bmp1.Height, cluster];
            Color p9; double J, dr, payda;
            int x, y, j, k, z;
            double[] mem; mem = new double[cluster];
            Color[] c; c = new Color[cluster];
            double[] r, g, b; r = new double[cluster]; g = new double[cluster]; b = new double[cluster];
            int[] psayisi; psayisi = new int[cluster];


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }


            Random xr = new Random();
            for (j = 0; j < cluster; j++)
            {
                x = xr.Next(0, 255); y = xr.Next(0, 255); z = xr.Next(0, 255);
                c[j] = Color.FromArgb(x, y, z);
            }

            z = 0;
            do
            {

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        { d[x, y, j] = Distance3(p9, c[j]); }

                        payda = d[x, y, 0]; k = 0;
                        for (j = 0; j < cluster; j++)
                        {
                            if (d[x, y, j] < payda)
                            { payda = d[x, y, j]; k = j; }
                        }
                        p9 = c[k]; bmp2.SetPixel(x, y, p9); label[x, y] = k;
                    }
                }


                J = 0;
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                            J = J + dr * dr;
                        }
                    }
                }




                for (j = 0; j < cluster; j++)
                { r[j] = 0; g[j] = 0; b[j] = 0; mem[j] = 1; }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        j = label[x, y]; p9 = bmp1.GetPixel(x, y);
                        r[j] = r[j] + (double)(p9.R);
                        g[j] = g[j] + (double)(p9.G);
                        b[j] = b[j] + (double)(p9.B);
                        mem[j] = mem[j] + 1;
                    }
                }

                for (j = 0; j < cluster; j++)
                { r[j] = r[j] / mem[j]; g[j] = g[j] / mem[j]; b[j] = b[j] / mem[j]; }

                for (j = 0; j < cluster; j++)
                { c[j] = Color.FromArgb((int)r[j], (int)g[j], (int)b[j]); }

             
                z++;
            } while (z < t);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    
                     k=label[x, y];         p9 = c[k];
                    p9 = Color.FromArgb((int)(255 - p9.R), (int)(p9.G), (int)(255 - p9.B));
                    bmp2.SetPixel(x, y, p9); 
                }
            }

            return bmp2;
        }


        public Bitmap Kmeans2(Bitmap img, int[,] label, Color[] renkler, double[] h, int cluster, int t)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            double[, ,] d = new double[bmp1.Width, bmp1.Height, cluster];
            Color p9; double J, dr, payda;
            int x, y, j, k, z;
            double[] mem; mem = new double[cluster];
            Color[] c; c = new Color[cluster];
            double[] r, g, b; r = new double[cluster]; g = new double[cluster]; b = new double[cluster];
            int[] psayisi; psayisi = new int[cluster];

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            Random xr = new Random();
            for (j = 0; j < cluster; j++)
            {
                x = xr.Next(0, 255); y = xr.Next(0, 255); z = xr.Next(0, 255);
                c[j] = Color.FromArgb(x, y, z);
            }
            z = 0;
            do
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        { d[x, y, j] = Distance3(p9, c[j]); }
                        payda = d[x, y, 0]; k = 0;
                        for (j = 0; j < cluster; j++)
                        {
                            if (d[x, y, j] < payda)
                            { payda = d[x, y, j]; k = j; }
                        }
                        p9 = c[k]; bmp2.SetPixel(x, y, p9); label[x, y] = k;
                    }
                }

                J = 0;
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                            J = J + dr * dr;
                        }
                    }
                }


                for (j = 0; j < cluster; j++)
                { r[j] = 0; g[j] = 0; b[j] = 0; mem[j] = 1; }
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        j = label[x, y]; p9 = bmp1.GetPixel(x, y);
                        r[j] = r[j] + (double)(p9.R);
                        g[j] = g[j] + (double)(p9.G);
                        b[j] = b[j] + (double)(p9.B);
                        mem[j] = mem[j] + 1;
                    }
                }
                for (j = 0; j < cluster; j++)
                { r[j] = r[j] / mem[j]; g[j] = g[j] / mem[j]; b[j] = b[j] / mem[j]; }
                for (j = 0; j < cluster; j++)
                {
                    c[j] = Color.FromArgb((int)r[j], (int)g[j], (int)b[j]);
                    renkler[j] = c[j];
                    h[j] = mem[j] / (double)(img.Width * img.Height);
                }

                z++;
            } while (z < t);


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {

                    k = label[x, y]; p9 = c[k];
                    p9 = Color.FromArgb((int)(255 - p9.R), (int)(p9.G), (int)(255 - p9.B));
                    bmp2.SetPixel(x, y, p9);
                }
            }
            return bmp2;
        }




        public Bitmap LBG(Bitmap img, int[,] label, Color[] renkler, double[] h, int cluster)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            double[, ,] d = new double[bmp1.Width, bmp1.Height, cluster];
            Color p9; double eps,D,D1, dr, payda,oran;
            int x, y, j, k,z;
            double[] mem; mem = new double[cluster];
            Color[] c; c = new Color[cluster];
            double[] r, g, b; r = new double[cluster]; g = new double[cluster]; b = new double[cluster];
            int[] psayisi; psayisi = new int[cluster];

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            Random xr = new Random();
            for (j = 0; j < cluster; j++)
            {
                x = xr.Next(0, 255); y = xr.Next(0, 255); z = xr.Next(0, 255);
                c[j] = Color.FromArgb(x, y, z);
            }



            //D1 = 100000;  
            eps = 0.001;


            D1 = 0;
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    for (j = 0; j < cluster; j++)
                    {
                        d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                        D1 = D1 + dr;
                    }
                }
            }

            do
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        { d[x, y, j] = Distance3(p9, c[j]); }
                        payda = d[x, y, 0]; k = 0;
                        for (j = 0; j < cluster; j++)
                        {
                            if (d[x, y, j] <= payda)
                            { payda = d[x, y, j]; k = j; }
                        }
                        p9 = c[k]; bmp2.SetPixel(x, y, p9); label[x, y] = k;
                    }
                }

                /*
                D = 0;
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                            D = D + dr;
                        }
                    }
                }
               */

                for (j = 0; j < cluster; j++)
                { r[j] = 0; g[j] = 0; b[j] = 0; mem[j] = 1; }
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        j = label[x, y]; p9 = bmp1.GetPixel(x, y);
                        r[j] = r[j] + (double)(p9.R);
                        g[j] = g[j] + (double)(p9.G);
                        b[j] = b[j] + (double)(p9.B);
                        mem[j] = mem[j] + 1;
                    }
                }
                for (j = 0; j < cluster; j++)
                { r[j] = r[j] / mem[j]; g[j] = g[j] / mem[j]; b[j] = b[j] / mem[j]; }
                for (j = 0; j < cluster; j++)
                {
                    c[j] = Color.FromArgb((int)r[j], (int)g[j], (int)b[j]);
                    renkler[j] = c[j];
                    h[j] = mem[j] / (double)(img.Width * img.Height);
                }


              
                D = 0;
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                            D = D + dr;
                        }
                    }
                }
             
                oran = (D1 - D) / D;
                D1 = D;
                MessageBox.Show((oran- eps).ToString());

            } while (oran > eps);

            /*
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {

                    k = label[x, y]; p9 = c[k];
                    p9 = Color.FromArgb((int)(255 - p9.R), (int)(p9.G), (int)(255 - p9.B));
                    bmp2.SetPixel(x, y, p9);
                }
            }
             * 
             * */
            return bmp2;
        }





        public double[] LBG2(Bitmap img, int[,] label, Color[] renkler, double[] h, int cluster)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            double[, ,] d = new double[bmp1.Width, bmp1.Height, cluster];
            Color p9; double eps, D, D1, dr, payda, oran;
            int x, y, j, k, z;
            double[] mem; mem = new double[cluster];
            Color[] c; c = new Color[cluster];
            double[] r, g, b; r = new double[cluster]; g = new double[cluster]; b = new double[cluster];
            int[] psayisi; psayisi = new int[cluster];

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            Random xr = new Random();
            for (j = 0; j < cluster; j++)
            {
                x = xr.Next(0, 255); y = xr.Next(0, 255); z = xr.Next(0, 255);
                c[j] = Color.FromArgb(x, y, z);
            }
            //D1 = 100000;  
            eps = 0.005;
            D1 = 0; D = 0;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    for (j = 0; j < cluster; j++)
                    {
                        d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                        D1 = D1 + dr;
                    }
                }
            }
            int sayac = 0;
            do
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        { d[x, y, j] = Distance3(p9, c[j]); }
                        payda = d[x, y, 0]; k = 0;
                        for (j = 0; j < cluster; j++)
                        {
                            if (d[x, y, j] < payda)
                            { payda = d[x, y, j]; k = j; }
                        }
                        p9 = c[k]; bmp2.SetPixel(x, y, p9); label[x, y] = k;
                    }
                }

                //D = 0;
                //for (x = 0; x < bmp1.Width; x++)
                //{
                //    for (y = 0; y < bmp1.Height; y++)
                //    {
                //        p9 = bmp1.GetPixel(x, y);
                //        for (j = 0; j < cluster; j++)
                //        {
                //            d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                //            D = D + dr;
                //        }
                //    }
                //}
                for (j = 0; j < cluster; j++)
                { r[j] = 0; g[j] = 0; b[j] = 0; mem[j] = 1; }
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        j = label[x, y]; p9 = bmp1.GetPixel(x, y);
                        r[j] = r[j] + (double)(p9.R);
                        g[j] = g[j] + (double)(p9.G);
                        b[j] = b[j] + (double)(p9.B);
                        mem[j] = mem[j] + 1;
                    }
                }

                int mn = bmp1.Width * bmp1.Height;
                for (j = 0; j < cluster; j++)
                { r[j] = r[j] / mem[j]; g[j] = g[j] / mem[j]; b[j] = b[j] / mem[j]; }

                for (j = 0; j < cluster; j++)
                {
                    c[j] = Color.FromArgb((int)r[j], (int)g[j], (int)b[j]);
                    renkler[j] = c[j];
                    h[j] = mem[j] / (double)(img.Width * img.Height);
                }

                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                            D = D + dr;
                        }
                    }
                }


                oran = Math.Abs((D1 - D)) / D1;
                D1 = D;
                sayac++;
               MessageBox.Show((oran- eps).ToString());

            } while (oran > eps);
            sortAlgorithm s = new sortAlgorithm();
            h = s.sortedArray(c, mem, bmp1.Width, bmp1.Height);
            return h;
        }


        class sortAlgorithm
        {
            public Color[] colorvalue;
            public double[] colorcount;
            public double[] distance;
            public double[] nH;


            public double[] sortedArray(Color[] c, double[] cc, int w, int h)
            {
                distance = new double[c.Length];
                colorcount = new double[c.Length];
                colorvalue = new Color[c.Length];

                for (int i = 0; i < c.Length; i++)
                {
                    distance[i] = Math.Sqrt((c[i].R * c[i].R) + (c[i].G * c[i].G) + (c[i].B * c[i].B));
                }
                double gecici = 0;
                Color colorgecici;
                double colorcountgecici;
                for (int i = 0; i < distance.Length; i++)
                {
                    for (int t = 0; t < distance.Length; t++)
                    {
                        if (distance[t] > distance[i])
                        {
                            gecici = distance[i];
                            distance[i] = distance[t];
                            distance[t] = gecici;

                            colorgecici = c[i];
                            c[i] = c[t];
                            c[t] = colorgecici;

                            colorcountgecici = cc[i];
                            cc[i] = cc[t];
                            cc[t] = colorcountgecici;

                        }
                    }
                }
                nH = new double[cc.Length];
                for (int i = 0; i < cc.Length; i++)
                {
                    nH[i] = cc[i] / (w * h);
                }
                return nH;
            }
        }

        public Bitmap LBG3(Bitmap img, int[,] label, Color[] renkler, double[] h, int cluster)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            double[, ,] d = new double[bmp1.Width, bmp1.Height, cluster];
            Color p9; double eps, D, D1, dr, payda, oran;
            int x, y, j, k, z;
            double[] mem; mem = new double[cluster];
            Color[] c; c = new Color[cluster];
            double[] r, g, b; r = new double[cluster]; g = new double[cluster]; b = new double[cluster];
            int[] psayisi; psayisi = new int[cluster];
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }
            Random xr = new Random();
            for (j = 0; j < cluster; j++)
            {
                x = xr.Next(0, 255); y = xr.Next(0, 255); z = xr.Next(0, 255);
                c[j] = Color.FromArgb(x, y, z);
            }

            eps = 0.005;
            D1 = 0;
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    for (j = 0; j < cluster; j++)
                    {
                        d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                        D1 = D1 + dr;
                    }
                }
            }

            do
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        { d[x, y, j] = Distance3(p9, c[j]); }
                        payda = d[x, y, 0]; k = 0;
                        for (j = 0; j < cluster; j++)
                        {
                            if (d[x, y, j] < payda)
                            { payda = d[x, y, j]; k = j; }
                        }
                        p9 = c[k]; bmp2.SetPixel(x, y, p9); label[x, y] = k;
                    }
                }


                for (j = 0; j < cluster; j++)
                { r[j] = 0; g[j] = 0; b[j] = 0; mem[j] = 1; }
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        j = label[x, y]; p9 = bmp1.GetPixel(x, y);
                        r[j] = r[j] + (double)(p9.R);
                        g[j] = g[j] + (double)(p9.G);
                        b[j] = b[j] + (double)(p9.B);
                        mem[j] = mem[j] + 1;
                    }
                }
                int mn = bmp1.Width * bmp1.Height;
                for (j = 0; j < cluster; j++)
                { r[j] = r[j] / mem[j]; g[j] = g[j] / mem[j]; b[j] = b[j] / mem[j]; }

                for (j = 0; j < cluster; j++)
                {
                    c[j] = Color.FromArgb((int)r[j], (int)g[j], (int)b[j]);
                    renkler[j] = c[j];
                    h[j] = mem[j] / (double)(img.Width * img.Height);
                }
                D = 0;
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        for (j = 0; j < cluster; j++)
                        {
                            d[x, y, j] = Distance3(p9, c[j]); dr = d[x, y, j];
                            D = D + dr;
                        }
                    }
                }
                
                oran = Math.Abs((D1 - D)) / D1;
                D1 = D;

                 MessageBox.Show((oran).ToString());
            } while (oran > eps);
            
            /*
            c[0] = Color.FromArgb(250, 250, 250);   h[0] = 30;
            c[1] = Color.FromArgb(100, 100, 100);   h[1] = 15;
            c[2] = Color.FromArgb(200, 200, 200);   h[2] = 7;
            c[3] = Color.FromArgb(10, 10, 10);      h[3] = 80;
            */
            sortColor s = new sortColor();
            s.sortedColors(c, h);
            for (j = 0; j < cluster; j++)
            { renkler[j] = c[j]; }
            return bmp2;
        }






        class sortColor
        {
            public double[] d;
            public void sortedColors(Color[] codeBook, double[] h)
            {
                d = new double[codeBook.Length];
                for (int i = 0; i < codeBook.Length; i++)
                {
                    d[i] = Math.Sqrt((codeBook[i].R * codeBook[i].R) + (codeBook[i].G * codeBook[i].G) + (codeBook[i].B * codeBook[i].B));
                }

                double td = 0;
                Color trenk;
                double tn;
                for (int i = 0; i < d.Length; i++)
                {
                    for (int t = 0; t < d.Length; t++)
                    {
                        if (d[t] > d[i])
                        {
                            td = d[i];
                            d[i] = d[t];
                            d[t] = td;
                            trenk = codeBook[i];
                            codeBook[i] = codeBook[t];
                            codeBook[t] = trenk;
                            tn = h[i];
                            h[i] = h[t];
                            h[t] = tn;
                        }
                    }
                }


            }
        }

        public struct Nokta3D
        {
            public double x, y, z;
            public Nokta3D(double px, double py, double pz)
            { x = px; y = py; z = pz; }
        };

        public Nokta3D Translate3D(Nokta3D input, double dx, double dy, double dz)
        {
            Nokta3D output = new Nokta3D(0, 0, 0);
            double[,] P = new double[3, 3];
            double[] v1 = new double[3];
            double[] v2 = new double[3];

            P[0, 0] = 1; P[0, 1] = 0; P[0, 2] = 0;
            P[1, 0] = 0; P[1, 1] = 1; P[1, 2] = 0;
            P[2, 0] = 0; P[2, 1] = 0; P[2, 2] = 1;

            v1[0] = input.x;
            v1[1] = input.y;
            v1[2] = input.z;
            for (int i = 0; i < 3; i++)
            {
                v2[i] = 0;
                for (int k = 0; k < 3; k++)
                { v2[i] += P[i, k] * v1[k]; }
            }
            output.x = v2[0] + dx;
            output.y = v2[1] + dy;
            output.z = v2[2] + dz;
            return (output);
        }

        public Nokta3D Scale3D(Nokta3D input, double sx, double sy, double sz)
        {
            Nokta3D output = new Nokta3D(0, 0, 0);
            double[,] P = new double[4, 4];
            double[] v1 = new double[4];
            double[] v2 = new double[4];

            P[0, 0] = sx; P[0, 1] = 0; P[0, 2] = 0;
            P[1, 0] = 0; P[1, 1] = sy; P[1, 2] = 0;
            P[2, 0] = 0; P[2, 1] = 0; P[2, 2] = sz;
            v1[0] = input.x;
            v1[1] = input.y;
            v1[2] = input.z;
            for (int i = 0; i < 3; i++)
            {
                v2[i] = 0;
                for (int k = 0; k < 3; k++)
                { v2[i] += P[i, k] * v1[k]; }
            }
            output.x = v2[0];
            output.y = v2[1];
            output.z = v2[2];

            return (output);
        }

        public Nokta3D Rotate3D(Nokta3D input, double alfa)
        {
            Nokta3D output = new Nokta3D(0, 0, 0);
            double[,] P = new double[3, 3];
            double[] v1 = new double[3];
            double[] v2 = new double[3];
            double teta; teta = 3.14 * alfa / 180;

            P[0, 0] = Math.Cos(teta); P[0, 1] = -Math.Sin(teta); P[0, 2] = 0;
            P[1, 0] = Math.Sin(teta); P[1, 1] = Math.Cos(teta);  P[1, 2] = 0;
            P[2, 0] = 0;              P[2, 1] = 0;               P[2, 2] = 1;

            v1[0] = input.x;
            v1[1] = input.y;
            v1[2] = input.z;
            for (int i = 0; i < 3; i++)
            {
                v2[i] = 0;
                for (int k = 0; k < 3; k++)
                { v2[i] += P[i, k] * v1[k]; }
            }
            output.x = v2[0];
            output.y = v2[1];
            output.z = v2[2];
            return (output);
        }



        public Bitmap TranslateImage(Bitmap bmp1, Nokta3D yeni)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int resim, x, y, xp, yp; Color p9;
            Nokta3D Nokta1 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta2 = new Nokta3D(0, 0, 0);


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = Color.FromArgb(0, 0, 0); bmp2.SetPixel(x, y, p9);
                }
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    Nokta1.x = (double)x; Nokta1.y = (double)y;
                    Nokta2 = Translate3D(Nokta1, yeni.x, yeni.y, 0);
                    yp = (int)Nokta2.y; xp = (int)Nokta2.x;
                    resim = resimdemi(xp, yp, bmp1.Width, bmp1.Height);
                    if (resim == 1)
                    { p9 = bmp1.GetPixel(x, y); bmp2.SetPixel(xp, yp, p9); }
                }
            }

            return bmp2;
        }


        public Bitmap ScaleImage(Bitmap bmp1, double sx, double sy)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int resim, x, y, xp, yp; Color p9;
            Nokta3D Nokta1 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta2 = new Nokta3D(0, 0, 0);


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = Color.FromArgb(0, 0, 0); bmp2.SetPixel(x, y, p9);
                }
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    Nokta1.x = (double)x; Nokta1.y = (double)y;
                    Nokta2 = Scale3D(Nokta1, sx, sy, 0);
                    yp = (int)Nokta2.y; xp = (int)Nokta2.x;
                    resim = resimdemi(xp, yp, bmp1.Width, bmp1.Height);
                    if (resim == 1)
                    { p9 = bmp1.GetPixel(x, y); bmp2.SetPixel(xp, yp, p9); }
                }
            }

            return bmp2;
        }

        public Bitmap RotateImage(Bitmap bmp1, double alfa)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int resim, x, y, xp, yp; Color p9;
            Nokta3D Nokta1 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta2 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta3 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta4 = new Nokta3D(0, 0, 0);
            Nokta3D pivot;

            pivot = new Nokta3D((double)bmp1.Width / 2, (double)bmp1.Height / 2, 0);

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = Color.FromArgb(0, 0, 0); bmp2.SetPixel(x, y, p9);
                }
            }


            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    Nokta1.x = (double)x; Nokta1.y = (double)y;
                    Nokta2 = Translate3D(Nokta1, -pivot.x, -pivot.y, 0);
                    Nokta3 = Rotate3D(Nokta2, alfa);
                    Nokta4 = Translate3D(Nokta3, pivot.x, pivot.y, 0);
                    yp = (int)Nokta4.y; xp = (int)Nokta4.x;
                    resim = resimdemi(xp, yp, bmp1.Width, bmp1.Height);
                    if (resim == 1)
                    { p9 = bmp1.GetPixel(x, y); bmp2.SetPixel(xp, yp, p9); }
                }
            }
            return bmp2;
        }

        public Bitmap ScaleImageA(Bitmap bmp1, double sx, double sy)
        {
            int resim, x, y, xp, yp, w, h; Color p9;
            Nokta3D Nokta1 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta2 = new Nokta3D(0, 0, 0);

            w = (int)(sx * bmp1.Width); h = (int)(sy * bmp1.Height);
            Bitmap bmp2 = new Bitmap(w, h);
            for (y = 0; y < bmp2.Height; y++)
            {
                for (x = 0; x < bmp2.Width; x++)
                { p9 = Color.FromArgb(0, 0, 255); bmp2.SetPixel(x, y, p9); }
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Nokta1.x = (double)x; Nokta1.y = (double)y;
                    Nokta2 = Scale3D(Nokta1, sx, sy, 0);
                    yp = (int)Nokta2.y; xp = (int)Nokta2.x;
                    resim = resimdemi(xp, yp, bmp2.Width, bmp2.Height);
                    if (resim == 1) bmp2.SetPixel(xp, yp, p9);
                }
            }
            return bmp2;
        }

        public Bitmap RotateImageA(Bitmap bmp1, double alfa)
        {
            int x, y, xp, yp, xmin, xmax, ymin, ymax, w, h; Color p9;
            Nokta3D Nokta1 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta2 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta3 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta4 = new Nokta3D(0, 0, 0);
            Nokta3D pivot;

            pivot = new Nokta3D((double)bmp1.Width / 2, (double)bmp1.Height / 2, 0);
            xmin = 0; xmax = 0; ymin = 0; ymax = 0;
            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    Nokta1.x = (double)x; Nokta1.y = (double)y;
                    Nokta2 = Translate3D(Nokta1, -pivot.x, -pivot.y, 0);
                    Nokta3 = Rotate3D(Nokta2, alfa);
                    Nokta4 = Translate3D(Nokta3, pivot.x, pivot.y, 0);
                    xp = (int)Nokta4.x; yp = (int)Nokta4.y;
                    if (xp < xmin) xmin = xp; if (xp > xmax) xmax = xp;
                    if (yp < ymin) ymin = yp; if (yp > ymax) ymax = yp;
                }
            }

            w = xmax - xmin; h = ymax - ymin; w = w + 1; h = h + 1;
            Bitmap bmp2 = new Bitmap(w, h);

            for (y = 0; y < bmp2.Height; y++)
            {
                for (x = 0; x < bmp2.Width; x++)
                {
                    p9 = Color.FromArgb(0, 255, 0); bmp2.SetPixel(x, y, p9);
                }
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    Nokta1.x = (double)x; Nokta1.y = (double)y;
                    Nokta2 = Translate3D(Nokta1, -pivot.x, -pivot.y, 0);
                    Nokta3 = Rotate3D(Nokta2, alfa);
                    Nokta4 = Translate3D(Nokta3, pivot.x, pivot.y, 0);
                    yp = (int)Nokta4.y; xp = (int)Nokta4.x;
                    xp = xp + Math.Abs(xmin); yp = yp + Math.Abs(ymin);
                    p9 = bmp1.GetPixel(x, y); bmp2.SetPixel(xp, yp, p9);
                }
            }
            return bmp2;
        }


        public Bitmap RotateImagePivot(Bitmap bmp1, Nokta3D pivot, double alfa)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            int resim, x, y, xp, yp; Color p9;
            Nokta3D Nokta1 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta2 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta3 = new Nokta3D(0, 0, 0);
            Nokta3D Nokta4 = new Nokta3D(0, 0, 0);

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    p9 = Color.FromArgb(0, 0, 0); bmp2.SetPixel(x, y, p9);
                }
            }

            for (y = 0; y < bmp1.Height; y++)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    Nokta1.x = (double)x; Nokta1.y = (double)y;
                    Nokta2 = Translate3D(Nokta1, -pivot.x, -pivot.y, 0);
                    Nokta3 = Rotate3D(Nokta2, alfa);
                    Nokta4 = Translate3D(Nokta3, pivot.x, pivot.y, 0);
                    yp = (int)Nokta4.y; xp = (int)Nokta4.x;
                    resim = resimdemi(xp, yp, bmp1.Width, bmp1.Height);
                    if (resim == 1)
                    { p9 = bmp1.GetPixel(x, y); bmp2.SetPixel(xp, yp, p9); }
                }
            }
            return bmp2;
        }

        public Bitmap MandelbrotSet(Bitmap bmp1, double xmax, double xmin, double ymax, double ymin)
        {
            Bitmap img = new Bitmap(bmp1);
            double zx = 0; double zy = 0;
            double cx = 0; double cy = 0;
            double dx = ((xmax - xmin) / Convert.ToDouble(img.Width));
            double dy = ((ymax - ymin) / Convert.ToDouble(img.Height));
            double tempzx = 0; int Nmax = 4000;
            int n = 0;
            for (int x = 0; x < img.Width; x++)
            {
                cx = (dx * x) - Math.Abs(xmin);
                for (int y = 0; y < img.Height; y++)
                {
                    zx = 0; zy = 0;
                    cy = (dy * y) - Math.Abs(ymin);
                    n = 0;
                    while (zx * zx + zy * zy <= 4 && n < Nmax)
                    {
                        n++;
                        tempzx = zx;
                        zx = (zx * zx) - (zy * zy) + cx;
                        zy = (2 * tempzx * zy) + cy;
                    }

                    if (n != Nmax)
                        img.SetPixel(x, y, Color.FromArgb(n % 128 * 2, n % 32 * 7, n % 16 * 14));
                    else img.SetPixel(x, y, Color.Black);
                }
            }
            return img;
        }




        public void Sekil(Bitmap img)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Color p9, c1; double teta, rmax, dteta, alfa;
            int x, y, resim, d, i, r, ric, n, N;
            N = 360;
            double[] rg = new Double[N];
            double[] rm = new Double[N];
            double[] ri = new Double[N];
            double[] aci = new Double[N];
            double[] dolu = new Double[N];


            p9 = bmp1.GetPixel(0, 0);
            dteta = 360 / (double)N; teta = 0; alfa = 0; rmax = 0;
            for (i = 0; i < N; i = i + 1)
            {
                c1 = bmp1.GetPixel(bmp1.Width / 2, bmp1.Height / 2);
                d = 0; r = 0; ric = 0; n = 0;
                do
                {
                    x = (int)(bmp1.Width / 2 + r * Math.Cos(alfa));
                    y = (int)(bmp1.Height / 2 - r * Math.Sin(alfa));
                    resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    rg[i] = r; if (r > rmax) rmax = r; r = r + 1;
                    if (resim == 1)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        if (p9.R == 255)
                        {
                            rm[i] = r;
                            d = d + 1;
                            dolu[i] = d;
                        }
                    }

                    if (c1.R != p9.R) n = n + 1;
                    if (resim == 1 && p9.R == 0 && n == 0)
                    { ric = ric + 1; ri[i] = ric; }
                    else
                    {
                        //   if (c1.R != p9.R)  n = n + 1;
                    }

                    c1 = Color.FromArgb(p9.R, p9.G, p9.B);
                } while (resim == 1);

                teta = teta + dteta;
                aci[i] = teta;
                alfa = 3.14 * teta / 180;
            }


            teta = 0;
            for (i = 0; i < rg.Length; i++)
            { teta = teta + rg[i] / rmax; }
          
        }



        public Bitmap Sekil2(Bitmap img, int eksen)
        {
            Bitmap bmp1 = (Bitmap)img.Clone();
            Bitmap bmp2 = (Bitmap)img.Clone();
            Bitmap bmp3 = (Bitmap)img.Clone();
            Color p9, c1; double teta, rmax, dteta, alfa;
            int x, y, resim, i, r, ric, n, N;
            N = 720;
            double[] rg = new Double[N];
            double[] rm = new Double[N];
            double[] ri = new Double[N];
            double[] aci = new Double[N];

            p9 = bmp1.GetPixel(0, 0);
            dteta = 360 / (double)N;
            teta = 0; alfa = 0; rmax = 0;
            for (i = 0; i < N; i = i + 1)
            {
                c1 = bmp1.GetPixel(bmp1.Width / 2, bmp1.Height / 2);
                r = 0; ric = 0; n = 0;
                do
                {
                    x = (int)(bmp1.Width / 2 + r * Math.Cos(alfa));
                    y = (int)(bmp1.Height / 2 - r * Math.Sin(alfa));

                    resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                    rg[i] = r; if (r > rmax) rmax = r; r = r + 1;
                    if (resim == 1)
                    {
                        p9 = bmp1.GetPixel(x, y);
                        if (p9.R == 255) rm[i] = r;
                    }

                    if (c1.R != p9.R) n = n + 1;
                    if (resim == 1 && p9.R == 0 && n == 0)
                    { ric = ric + 1; ri[i] = ric; }
                    else
                    {
                        //   if (c1.R != p9.R)  n = n + 1;
                    }
                    c1 = Color.FromArgb(p9.R, p9.G, p9.B);
                } while (resim == 1);

                teta = teta + dteta;
                aci[i] = teta;
                alfa = 3.14 * teta / 180;
            }

            alfa = 0; teta = 0;
            for (i = 0; i < N; i = i + 1)
            {
                rmax = rm[i];
                x = (int)(bmp1.Width / 2 + rmax * Math.Cos(alfa));
                y = (int)(bmp1.Height / 2 - rmax * Math.Sin(alfa));
                resim = resimdemi(x, y, bmp1.Width, bmp1.Height);
                if (resim == 1)
                { p9 = Color.FromArgb(255, 0, 0); bmp2.SetPixel(x, y, p9); }
                teta = teta + dteta;
                alfa = 3.14 * teta / 180;
            }

            bmp2 = CannyEdge(img, 10, 20);

            if (eksen == 1)
            {
                for (x = 0; x < bmp1.Width; x++)
                {
                    for (y = 0; y < bmp1.Height; y++)
                    {
                        if (x == bmp1.Width / 2 || y == bmp1.Height / 2)
                        { p9 = Color.FromArgb(0, 0, 255); bmp2.SetPixel(x, y, p9); }
                    }
                }
            }


            teta = 0;
            for (i = 0; i < rg.Length; i++)
            { teta = teta + rg[i] / rmax; }         

            return bmp2;
        }


       




        int factorial(int n)
        {
            if (n == 0)
                return 1;
            else
                return (n * factorial(n - 1));
        }

        int binom(int n, int k)
        {
            int value = 0;
            value = factorial(n) / (factorial(n - k) * factorial(k));
            return value;
        }



        public string RLEencode(string input)
        {
            char tmp1, tmp2;
            string output;
            int i, count;

            output = "";
            tmp1 = input[0];
            tmp2 = tmp1;
            count = 1;

            for (i = 1; i < input.Length; i++)
            {
                tmp1 = input[i];
                if (tmp1 != tmp2)
                {
                    output += count.ToString() + tmp2;
                    tmp2 = tmp1;
                    count = 1;
                }
                else
                {
                    count++;
                }
            }

            output += count.ToString() + tmp2;
            return (output);
        }


        public string RLEencode2(string input)
        {
            char enson, onceki;
            string output;
            int i, n;

            output = "";
            enson = input[0];
            onceki = enson;
            n = 1;

            for (i = 1; i < input.Length; i++)
            {
                enson = input[i];
                if (enson != onceki)
                {
                    output = output + n.ToString() + ":" + onceki;
                    onceki = enson;
                    n = 1;
                }
                else
                { n = n + 1; }
            }

            output = output + n.ToString() + onceki;
            return (output);
        }


        public  string RLEdecode(string s)
        {
            string a = "";
            int count = 0;
            StringBuilder sb = new StringBuilder();
            char current = char.MinValue;
            for (int i = 0; i < s.Length; i++)
            {
                current = s[i];
                if (char.IsDigit(current))
                    a += current;
                else
                {
                    count = int.Parse(a);
                    a = "";
                    for (int j = 0; j < count; j++)
                        sb.Append(current);
                }
            }
            return sb.ToString();
        }



        public static string RunLengthEncode(string s)
        {
            try
            {
                string srle = string.Empty;
                int ccnt = 1; //char counter 
                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (s[i] != s[i + 1] || i == s.Length - 2) //..a break in character repetition or the end of the string 
                    {
                        if (s[i] == s[i + 1] && i == s.Length - 2) //end of string condition 
                            ccnt++;
                        // srle += ccnt + ("1234567890".Contains(s[i]) ? "" + ESCAPE : "") + s[i]; //escape digits 
                        if (s[i] != s[i + 1] && i == s.Length - 2) //end of string condition 
                            //    srle += ("1234567890".Contains(s[i + 1]) ? "1" + ESCAPE : "") + s[i + 1];  
                            ccnt = 1; //reset char repetition counter 
                    }
                    else
                    {
                        ccnt++;
                    }


                }
                return srle;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in RLE:" + e.Message);
                return null;
            }
        }
        public static string RunLengthDecode(string s)
        {
            try
            {
                string dsrle = string.Empty
                        , ccnt = string.Empty; //char counter 
                for (int i = 0; i < s.Length; i++)
                {
                    if ("1234567890".Contains(s[i])) //extract repetition counter 
                    {
                        ccnt += s[i];
                    }
                    else
                    {
                        //   if (s[i] == ESCAPE) 
                        {
                            i++;
                        }
                        dsrle += new String(s[i], int.Parse(ccnt));
                        ccnt = "";
                    }


                }
                return dsrle;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in RLD:" + e.Message);
                return null;
            }
        } 





        public int Hangisi2(int a, int t1)
        {
            int b; b = -1;
            if (a >= 0 && a < t1) b = 0;
            else if (a >= t1 && a < 256) b = 1;
            return b;
        }
        public Bitmap Make2E(Bitmap bmp1, int ftipi)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            int[] T; T = new int[3]; int tr1, tg1, tb1; double m0;
            int c, a, x, y, br, bg, bb; Color p9;
            int[,] label; label = new int[bmp1.Width, bmp1.Height];
            double[] np; np = new double[8]; Color[] cn = new Color[8];

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                { label[x, y] = -1; }
            }

            if (ftipi == 1) getMean(bmp1, T);
            else if (ftipi == 2) OtsuKapurTekEsik(bmp1, T, 1);
            else if (ftipi == 3) OtsuKapurTekEsik(bmp1, T, 2);
            else if (ftipi == 4) getMedyan(bmp1, T);
            else if (ftipi == 5) getMAD(bmp1, T);
            else { T[0] = 0; T[1] = 0; T[2] = 0; }

            tr1 = T[0]; tg1 = T[1]; tb1 = T[2];

            for (x = 0; x < 8; x++)
            { hr[x] = 0; hg[x] = 0; hb[x] = 0; np[x] = 0; }


            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y); br = Hangisi2(p9.R, tr1); bg = Hangisi2(p9.G, tg1); bb = Hangisi2(p9.B, tb1);

                    if (br == 0 && bg == 0 && bb == 0)
                    { c = 0; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                    else if (br == 0 && bg == 0 && bb == 1)
                    { c = 1; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                    else if (br == 0 && bg == 1 && bb == 0)
                    { c = 2; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                    else if (br == 0 && bg == 1 && bb == 1)
                    { c = 3; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                    else if (br == 1 && bg == 0 && bb == 0)
                    { c = 4; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                    else if (br == 1 && bg == 0 && bb == 1)
                    { c = 5; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                    else if (br == 1 && bg == 1 && bb == 0)
                    { c = 6; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                    else if (br == 1 && bg == 1 && bb == 1)
                    { c = 7; label[x, y] = c; hr[c] += p9.R; hg[c] += p9.G; hb[c] += p9.B; np[c] += 1; }
                }
            }


            for (x = 0; x < 8; x++)
            {
                if (np[x] > 0)
                {
                    m0 = hr[x] / np[x]; tr1 = (int)m0;
                    m0 = hg[x] / np[x]; tg1 = (int)m0;
                    m0 = hb[x] / np[x]; tb1 = (int)m0;
                }
                cn[x] = Color.FromArgb(tr1, tg1, tb1);
            }



            a = HistogramNin(np);
            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    c = label[x, y];
                    // if (label[x, y] == a)
                    //      bmp2.SetPixel(x, y, Color.FromArgb(0, 0, 255));
                    // else
                    bmp2.SetPixel(x, y, cn[c]);
                }
            }
            return bmp2;
        }






        public double MaskeVaryas(Color[] c, int kanal)
        {
            double var, q1, q2, q3, m1, m2, m3; int b;
            q1 = 0; q2 = 0; q3 = 0;
            for (b = 0; b < c.Length; b++)
            { q1 = q1 + (double)c[b].R; q2 = q2 + (double)c[b].G; q3 = q3 + (double)c[b].B; }

            m1 = q1 / c.Length; m2 = q2 / c.Length; m3 = q3 / c.Length;

            q1 = 0; q2 = 0; q3 = 0;
            for (b = 0; b < c.Length; b++)
            {
                q1 = q1 + (c[b].R - m1) * (c[b].R - m1);
                q2 = q2 + (c[b].G - m2) * (c[b].G - m2);
                q3 = q3 + (c[b].B - m3) * (c[b].B - m3);
            }

            q1 = Math.Sqrt(q1) / (c.Length);
            q2 = Math.Sqrt(q2) / (c.Length);
            q3 = Math.Sqrt(q3) / (c.Length);

            if (kanal == 1) var = q1;
            else if (kanal == 2) var = q2;
            else if (kanal == 3) var = q3;
            else if (kanal == 4) var = q1 + q2 + q3;
            else var = 0;
            return var;
        }

        public Bitmap Varimage(Bitmap bmp1, int kanal)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double v, varmax; int res, x, y, i, j, b; Color p9, c2;
            Color[] c; c = new Color[9];
            double[,] V; V = new double[bmp1.Width, bmp1.Height];
            c2 = Color.FromArgb(0, 0, 0);

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    for (b = 0; b < 9; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        { p9 = bmp1.GetPixel(x, y); c[b] = p9; }
                        else { p9 = bmp1.GetPixel(i, j); c[b] = p9; }
                    }
                    v = MaskeVaryas(c, kanal); V[i, j] = v;
                }
            }

            varmax = V[0, 0];
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                { v = V[i, j]; if (v > varmax) varmax = v; }
            }


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    if (varmax > 0) V[i, j] = V[i, j] / varmax;
                    v = 255 * V[i, j]; if (v < 256) c2 = Color.FromArgb((int)v, (int)v, (int)v);
                    bmp2.SetPixel(i, j, c2);
                }
            }

            return bmp2;
        }

        public Bitmap VarimageN(Bitmap bmp1, int kanal)
        {
            Bitmap bmp2 = (Bitmap)bmp1.Clone();
            double v, varmax; int res, x, y, i, j, b; Color p9, c2;
            Color[] c; c = new Color[9];
            double[,] V; V = new double[bmp1.Width, bmp1.Height];
            c2 = Color.FromArgb(0, 0, 0); varmax = 240.40;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    for (b = 0; b < 9; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        res = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (res == 1)
                        { p9 = bmp1.GetPixel(x, y); c[b] = p9; }
                        else { p9 = bmp1.GetPixel(i, j); c[b] = p9; }
                    }
                    v = MaskeVaryas(c, kanal); V[i, j] = v;
                }
            }


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    v = 255 * V[i, j] / varmax; if (v < 256) c2 = Color.FromArgb((int)v, (int)v, (int)v);
                    bmp2.SetPixel(i, j, c2);
                }
            }

            return bmp2;
        }   



        /*
        public void OpticFlow(Bitmap SonKare, Bitmap Onceki, double[,] Vx, double[,] Vy, double alfa)
        {
            Bitmap bmp1 = (Bitmap)SonKare.Clone();
            Bitmap bmp2 = (Bitmap)Onceki.Clone();
            Bitmap bmp3 = (Bitmap)bmp1.Clone();

            Color[] rnkkom; Color p9, c1, c2;
            rnkkom = new Color[9];
            int[] resim;
            resim = new int[9];
            double q1x, q1y, Ix, Iy, It, hx, hy;
            double[] wx, wy;
            wx = new double[9];
            wy = new double[9];
            int i, j, b, x, y;

            wx[0] = -1; wx[1] = -2; wx[2] = -1;
            wx[3] = 0; wx[8] = 0; wx[4] = 0;
            wx[5] = 1; wx[6] = 2; wx[7] = 1;

            wy[0] = -1; wy[1] = 0; wy[2] = 1;
            wy[3] = -2; wy[8] = 0; wy[4] = 2;
            wy[5] = -1; wy[6] = 0; wy[7] = 1;



            bmp1 = Gray(SonKare);


            for (i = 0; i < bmp1.Width; i++)
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (b = 0; b <= 8; b++)
                    {
                        x = ikomsu(b, i); y = jkomsu(b, j);
                        resim[b] = resimdemi(x, y, bmp1.Width, bmp1.Height);
                        if (resim[b] == 1)
                        { p9 = bmp1.GetPixel(x, y); rnkkom[b] = p9; }
                    }

                    q1x = 0; q1y = 0;
                    for (b = 0; b <= 8; b++)
                    {
                        q1x = q1x + rnkkom[b].R * wx[b] * resim[b];
                        q1y = q1y + rnkkom[b].R * wy[b] * resim[b];
                    }

                    Ix = Math.Abs(q1x) / 4;
                    Iy = Math.Abs(q1y) / 4;

                    c1 = bmp1.GetPixel(i, j);
                    c2 = bmp2.GetPixel(i, j);
                    It = (c1.R - c2.R) / 0.5;

                    hx = vx[i, j]; hy = vy[i, j];
                    hx = hx - Ix * (Ix * hx + Iy * hy + It) / (alfa * alfa + Ix * Ix + Iy * Iy);
                    hy = hy - Iy * (Ix * hx + Iy * hy + It) / (alfa * alfa + Ix * Ix + Iy * Iy);

                    Vx[i, j] = hx; Vy[i, j] = hy;

                    p9 = Color.FromArgb((int)Ix, (int)Ix, (int)Ix);
                    bmp3.SetPixel(i, j, p9);
                }
            }
            //pictureBox1.Image = bmp1;
           // pictureBox2.Image = bmp3;
            // resim2 = bmp3;      
        }
        
        */

        public Bitmap RotateImage(float Angle)
        {
            // The original bitmap needs to be drawn onto a new bitmap which will probably be bigger 
            // because the corners of the original will move outside the original rectangle.
            // An easy way (OK slightly 'brute force') is to calculate the new bounding box is to calculate the positions of the 
            // corners after rotation and get the difference between the maximum and minimum x and y coordinates.
            float wOver2 = this.resim1.Width / 2.0f;
            float hOver2 = this.resim1.Height / 2.0f;
            float radians = -(float)(Angle / 180.0 * System.Math.PI);
            // Get the coordinates of the corners, taking the origin to be the centre of the bitmap.
            PointF[] corners = new PointF[]{
            new PointF(-wOver2, -hOver2),
            new PointF(+wOver2, -hOver2),
            new PointF(+wOver2, +hOver2),
            new PointF(-wOver2, +hOver2)
        };

            for (int i = 0; i < 4; i++)
            {
                PointF p = corners[i];
                PointF newP = new PointF((float)(p.X * System.Math.Cos(radians) - p.Y * System.Math.Sin(radians)), (float)(p.X * System.Math.Sin(radians) + p.Y * System.Math.Cos(radians)));
                corners[i] = newP;
            }

            // Find the min and max x and y coordinates.
            float minX = corners[0].X;
            float maxX = minX;
            float minY = corners[0].Y;
            float maxY = minY;
            for (int i = 1; i < 4; i++)
            {
                PointF p = corners[i];
                minX = System.Math.Min(minX, p.X);
                maxX = System.Math.Max(maxX, p.X);
                minY = System.Math.Min(minY, p.Y);
                maxY = System.Math.Max(maxY, p.Y);
            }

            // Get the size of the new bitmap.
            SizeF newSize = new SizeF(maxX - minX, maxY - minY);
            // ...and create it.
            Bitmap returnBitmap = new Bitmap((int)System.Math.Ceiling(newSize.Width), (int)System.Math.Ceiling(newSize.Height));
            // Now draw the old bitmap on it.
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.TranslateTransform(newSize.Width / 2.0f, newSize.Height / 2.0f);
                g.RotateTransform(-Angle);
                g.TranslateTransform(-this.resim1.Width / 2.0f, -this.resim1.Height / 2.0f);
              //  g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(this.resim1, 0, 0);
            }

            return returnBitmap;
        }


        public Bitmap Resize(double size)
        {
            int sourceWidth = this.resim1.Width;
            int sourceHeight = this.resim1.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size * sourceWidth / (float)sourceWidth);
            nPercentH = ((float)size * sourceHeight / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
           // g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(this.resim1, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }
        /// <summary>
        /// Creates the gabor filter.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="sigma">The sigma.</param>
        /// <returns></returns>
        public double[,] CreateGaborFilter(int size, double angle, double wavelength, double sigma)
        {
            double[,] filter = new double[size, size];
            double frequency = 7 + (100 - (wavelength * 100)) * 0.03;

            int windowSize =5 / 2;

            for (int y = 0; y < size; ++y)
            {
                for (int x = 0; x < size; ++x)
                {
                    int dy = -windowSize + y;
                    int dx = -windowSize + x;

                    filter[x, y] = GaborFilterValue(dy, dx, frequency, angle, 0, sigma, 0.80);
                }
            }

            return filter;
        }



        /// <summary>
        /// Gabor filter values generation.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="lambda">The wavelength.</param>
        /// <param name="theta">The orientation.</param>
        /// <param name="phi">The phaseoffset.</param>
        /// <param name="sigma">The gaussvar.</param>
        /// <param name="gamma">The aspectratio.</param>
        /// <returns></returns>
        double GaborFilterValue(int x, int y, double lambda, double theta, double phi, double sigma, double gamma)
        {
            double xx = x * Math.Cos(theta) + y * Math.Sin(theta);
            double yy = -x * Math.Sin(theta) + y * Math.Cos(theta);

            double envelopeVal = Math.Exp(-((xx * xx + gamma * gamma * yy * yy) / (2.0f * sigma * sigma)));

            double carrierVal = Math.Cos(2.0f * (float)Math.PI * xx / lambda + phi);

            double g = envelopeVal * carrierVal;

            return g;
        }


/*
        /// <summary>
        /// Convolve the image with the different filters depending on the orientation and density of the pixel.
        /// </summary>
        /// <param name="image">The image to be filtered.</param>
        /// <param name="directionalMap">The directional map.</param>
        /// <param name="densityMap">The density map.</param>
        /// <returns></returns>
        public double[,] Filter(double[,] image, double[,] directionalMap, double[,] densityMap)
        {
            int midX = FILTER_SIZE / 2;
            int midY = FILTER_SIZE / 2;
            double[,] filteredImage = new double[image.GetLength(0), image.GetLength(1)];
            double[,] filteredImageWithValuesScaled = new double[image.GetLength(0), image.GetLength(1)];
            double[,] finalImage = new double[image.GetLength(0), image.GetLength(1)];

            for (int i = 0; i < image.GetLength(0); i++)
                for (int j = 0; j < image.GetLength(1); j++)
                {

                    double pixelValue = GetPixelConvolutionValue(image, this.filterBank[(int)Math.Floor((directionalMap[i, j] * 180 / Math.PI))][Math.Round(densityMap[i, j], 2)], i - midX, j - midY);

                    filteredImage[i, j] = pixelValue;
                }

            filteredImageWithValuesScaled = this.RescaleValues(filteredImage, 0.0, 255.0);

            return filteredImageWithValuesScaled;
        }


/// <summary>
    /// Gets the pixel convolution value.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="sourceX">The source X.</param>
    /// <param name="sourceY">The source Y.</param>
    /// <returns></returns>
    private double GetPixelConvolutionValue(double[,] image, double[,] filter, int sourceX, int sourceY)
    {
        double result      = 0.0;
        int    totalPixels = 0;

        for (int i = 0; i < filter.GetLength(0); i++)
        {
            if(i + sourceX < 0 || i + sourceX >= image.GetLength(0))
                continue;

            for (int j = 0; j < filter.GetLength(1); j++)
            {
                if(j + sourceY < 0 || j + sourceY >= image.GetLength(1))
                    continue;

                double deltaResult = image[sourceX + i,sourceY + j] * filter[i, j];
                result += deltaResult;

                ++totalPixels;
            }
        }

        double filteredValue = result / totalPixels;
        return filteredValue;
    }


*/



    }
}
