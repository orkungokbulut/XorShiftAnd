using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedPic
{
    public class Crypto
    {
        private Bitmap Orijinal;
        private Bitmap Maske;
        private Bitmap EncrptBmp;
        private Bitmap DecrptBmp;

        private int m, N;
        public int[] Sayilar;
        public int[] fi;
        public double[] pi;


        public ulong[] KeyMler = new ulong[32]; 

        Random xr = new Random();

        


        public string X0bilgiString;
        public string X0bilgiHex;
        public string X0bilgiBinary;
        public double X0bilgiDouble;
        public ulong X0bilgiUlong;

        
        public string Y1bilgiHex;
        public string Y1bilgiBinary;
        public ulong Y1bilgiUlong;

        public string Y2bilgiHex;
        public string Y2bilgiBinary;
        public ulong Y2bilgiUlong;

        public string MubilgiString;
        public string MubilgiHex;
        public string MubilgiBinary;
        public double MubilgiDouble;

        public string M1bilgiHex;
        public string M1bilgiBinary;
        public ulong M1bilgiUlong;

        public double abilgiDouble;
        public string abilgiHex;
        public string abilgiBinary;
        public string abilgiString;
        public ulong abilgiUlong;

        public string KeyFinal;
        public string binaryKey;

        public Crypto(Bitmap bmp, int Diziboyutu, int kbits)
        {
            Orijinal = new Bitmap(bmp);
            Maske = new Bitmap(bmp);
            EncrptBmp = new Bitmap(bmp);
            DecrptBmp = new Bitmap(bmp);

            N = Diziboyutu; m = (int)Math.Pow(2, (double)(kbits));

            ulong M1;       

            int i;
            Sayilar = new int[N];
            fi = new int[m];
            pi = new double[m];

            for (i = 0; i < N; i++)
            { Sayilar[i] = 0; }

            for (i = 0; i < m; i++)
            { fi[i] = 0; pi[i] = 0.0; }

            for (i = 0; i < KeyMler.Length; i++)
            { KeyMler[i] = 0; }


            X0bilgiString = "";
            X0bilgiHex = "";
            X0bilgiBinary = "";
            X0bilgiDouble = 0.0;

            MubilgiString = "";
            MubilgiHex = "";
            MubilgiBinary = "";
            MubilgiDouble = 3.56;

            M1bilgiHex = "";
            M1bilgiBinary = "";
            M1bilgiUlong = 0;         

            abilgiString = "";
            abilgiHex = "";
            abilgiBinary = "";
            abilgiDouble = 0.0;

            Y1bilgiHex = "";
            Y1bilgiBinary = "";
            Y1bilgiUlong = 0;

            Y2bilgiHex = "";
            Y2bilgiBinary = "";
            Y2bilgiUlong = 0;

            KeyFinal = "";

            X0stringUreteci2();
            MustringUreteci2();
          
            aUreteci2();
            Y0stringUreteci1();
            Y0stringUreteci2();

            M1 = UlongKeyUreteci();
            M1bilgiUlong = M1;
            M1bilgiHex = Decimaltohex64(M1);
            M1bilgiBinary = Decimaltobin64(M1);


            //Y1bilgiHex = "0000000000000011"; Y1bilgiUlong = 17;

            //XorShiftAnd192
            
               //abilgiDouble =0.81787566346257;
               //Y1bilgiHex ="448c9dec62489800";      
               //Y1bilgiUlong = ulong.Parse(Y1bilgiHex, System.Globalization.NumberStyles.HexNumber);        
               //Y2bilgiHex = "dee4f9b88862d800";
               //Y2bilgiUlong = ulong.Parse(Y2bilgiHex, System.Globalization.NumberStyles.HexNumber);
        
             
               //XorShiftAnd256

            abilgiDouble = 0.81787566346257;
            Y1bilgiHex = "24006b91d495fa00";
            Y1bilgiUlong = ulong.Parse(Y1bilgiHex, System.Globalization.NumberStyles.HexNumber);
            Y2bilgiHex = "3b38d61f11a25400";
            Y2bilgiUlong = ulong.Parse(Y2bilgiHex, System.Globalization.NumberStyles.HexNumber);
            M1bilgiHex = "043080020004c000";
            M1bilgiUlong = ulong.Parse(M1bilgiHex, System.Globalization.NumberStyles.HexNumber);        
           

        }


        public string X0stringUreteci2()
        {
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);

            X0bilgiString = bilR;
            X0bilgiDouble = double.Parse(X0bilgiString);

            string bilgi = Double2hex(X0bilgiDouble);
            X0bilgiHex = bilgi;
            X0bilgiBinary = Hex2binary(bilgi);

            return bilR;
        }


        public string MustringUreteci2()
        {
            int k, n; char c;
            char[] Rkeys = new char[16];

            Rkeys[0] = '3'; Rkeys[1] = ',';

            n = xr.Next(53, 57); c = (char)n;
            Rkeys[2] = c;

            n = xr.Next(54, 57); c = (char)n;
            Rkeys[3] = c;


            for (k = 4; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 4; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57); c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);

            MubilgiString = bilR;

            MubilgiDouble = double.Parse(MubilgiString);

            string bilgi = Double2hex(MubilgiDouble);
            MubilgiHex = bilgi;
            MubilgiBinary = Hex2binary(bilgi);

            return bilR;
        }


      

        public string aUreteci2()
        {
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);

            abilgiString = bilR;
            abilgiDouble = double.Parse(abilgiString);

            string bilgi = Double2hex(abilgiDouble);
            abilgiHex = bilgi;
            abilgiBinary = Hex2binary(bilgi);

            return bilR;
        }




        public ulong Y0stringUreteci1()
        {
            ulong z;
            double q;
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);
            q = double.Parse(bilR);

            z = (ulong)(q * ulong.MaxValue);

            Y1bilgiUlong = z;

            string bilgi; bilgi = Decimaltohex64(z);
            Y1bilgiHex = bilgi;

            string bilgibinary = Hex2binary(bilgi);
            Y1bilgiBinary = bilgibinary;
            return z;
        }


        public ulong Y0stringUreteci2()
        {
            ulong z;
            double q;
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);
            q = double.Parse(bilR);

            z = (ulong)(q * ulong.MaxValue);

            Y2bilgiUlong = z;

            string bilgi; bilgi = Decimaltohex64(z);
            Y2bilgiHex = bilgi;

            string bilgibinary = Hex2binary(bilgi);
            Y2bilgiBinary = bilgibinary;
            return z;
        }

        
        public ulong UlongKeyUreteci()
        {   ulong q1;
            int  k, n, birsayisi,cevap;
            int[] bitsirasi = new int[8];
            char[] Rkeys = new char[64];

            for (k = 0; k < bitsirasi.Length; k++)
               { bitsirasi[k] =0; }

            for (k = 0; k < Rkeys.Length; k++)
               { Rkeys[k] = '0'; }
             

          //  Random xr = new Random();

            for (k = 0; k < bitsirasi.Length; k++)
            {                
                n = xr.Next(0, 63);
                cevap = DizideVarmi(n, bitsirasi);
                if (cevap == 0)
                {   bitsirasi[k] = n;
                    Rkeys[n] = '1';
                }
            }

            birsayisi = 0;
            do
            {
                   birsayisi = 0;
                  for (k = 0; k < bitsirasi.Length; k++)
                  {   if (bitsirasi[k] > 0)
                      birsayisi = birsayisi + 1;   }

                if (birsayisi != 8)
                 {   n = xr.Next(0, 63);
                     cevap = DizideVarmi(n, bitsirasi);
                     if (cevap == 0)
                     {
                         for (k = 0; k < bitsirasi.Length; k++)
                          {
                            if (bitsirasi[k] == 0)
                               {   bitsirasi[k] = n;
                                   Rkeys[n] = '1';
                              }                              
                          }                     
                     }                  
                 }

            } while (birsayisi != 8);

            string bilR = new string(Rkeys);          
            // bilR = "1100111100000000000011110000000000001111000000000000111100000000";
            q1 = Convert.ToUInt64(bilR, 2);
            q1 = q1 & ulong.MaxValue;
            return q1;
        }

        public int DizideVarmi(int x, int[] sayilar)
        {
            int k, varmi;
            varmi = 0;
            for (k = 0; k < sayilar.Length; k++)
            {
                if (sayilar[k] == x)
                    varmi = 1;
            }
            return varmi;
        }




        public int UlongMaskKey(ulong x, ulong M)
        {
            int n, k, q1;
            char[] key = new char[8];
            char[] Rkeys = new char[64];
            char[] Gkeys = new char[64];

            for (k = 0; k < key.Length; k++)
            { key[k] = '0'; }

            for (k = 0; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; Gkeys[k] = '0'; }

            string Rstr = Decimaltobin64(x);
            char[] Ra = Rstr.ToCharArray();

            string Gstr = Decimaltobin64(M);
            char[] Ga = Gstr.ToCharArray();

            for (k = 0; k < Ra.Length; k++)
            { Rkeys[k] = Ra[k]; }

            for (k = 0; k < Ga.Length; k++)
            { Gkeys[k] = Ga[k]; }


            n = 0;
            for (k = 0; k < Rkeys.Length; k++)
            {
                if (Gkeys[k] == '1' && n < 8)
                {
                    if (Rkeys[k] == '0')
                        key[n] = '0';
                    if (Rkeys[k] == '1')
                        key[n] = '1';
                    n = n + 1;
                }
            }

            string bilR = new string(key);
            q1 = int.Parse(bilR);
            q1 = q1 & 255;
            return q1;
        }



        public Color ColorCaesarEnc(Color p9, int key)
        {
            int q1, q2, q3; Color c1;
            q1 = (p9.R + key) % 255;
            q2 = (p9.G + key) % 255;
            q3 = (p9.B + key) % 255;
            c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            return c1;
        }


        public Color ColorCaesarDcp(Color p9, int key)
        {
            int q1, q2, q3, d; Color c1;

            d = p9.R - key;
            if (d > 0)
            { q1 = (p9.R - key) % 255; }
            else
            {
                q1 = ((-1) * (p9.R - key)) % 255;
                q1 = 255 - q1;
            }

            d = p9.G - key;
            if (d > 0)
            { q2 = (p9.G - key) % 255; }
            else
            {
                q2 = ((-1) * (p9.G - key)) % 255;
                q2 = 255 - q2;
            }

            d = p9.B - key;
            if (d > 0)
            { q3 = (p9.B - key) % 255; }
            else
            {
                q3 = ((-1) * (p9.B - key)) % 255;
                q3 = 255 - q3;
            }

            c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);

            return c1;
        }



        public Color ColorEXOR(Color p9, int key)
        {
            int q1, q2, q3; Color c1;
            q1 = p9.R ^ key;
            q2 = p9.G ^ key;
            q3 = p9.B ^ key;
            c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            return c1;
        }

        public Bitmap EncryptionCaesarMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarEnc(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionCaesarMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarDcp(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap EncryptionCaesarMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            Bitmap veri = (Bitmap)Maske.Clone();


            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarEnc(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionCaesarMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarDcp(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap EncryptionExorMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionExorMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap EncryptionExorMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionExorMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public int GCDRecursive(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            if (a > b)
                return GCDRecursive(a % b, b);
            else
                return GCDRecursive(a, b % a);
        }


        public int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            if (a == 0)
                return b;
            else
                return a;
        }

        public int EulerTotient(int n)
        {
            int i, say = 0;
            for (i = 0; i < n; i++)
            {
                if (GCD(i, n) == 1)
                { say = say + 1; }
            }
            return say;
        }
        public int ModuloTersi(int a, int m)
        {
            int i, ai = 0;
            int flag = 0;
            for (i = 0; i < m; i++)
            {
                flag = (a * i) % m;
                if (flag == 1)
                { ai = i; }
            }
            return ai;
        }

        public Color ColorAfineEnc(Color p9, int a, int b)
        {
            int q1, q2, q3, test; Color c1;
            test = GCD(a, 256);
            if (test == 1)
            {
                q1 = (a * p9.R + b) % 255;
                q2 = (a * p9.G + b) % 255;
                q3 = (a * p9.B + b) % 255;
                c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            }
            else
            {
                c1 = Color.FromArgb(0, 0, 0);
                // c1 = p9;
            }
            return c1;
        }

        public Color ColorAfineDcp(Color p9, int a, int b)
        {
            int q1, q2, q3, d, at; Color c1;

            at = ModuloTersi(a, 255);

            if (at != 0)
            {
                d = p9.R - b;
                if (d > 0)
                { q1 = (at * (p9.R - b)) % 255; }
                else
                {
                    q1 = (at * (-1) * (p9.R - b)) % 255;
                    q1 = 255 - q1;
                }

                d = p9.G - b;
                if (d > 0)
                { q2 = (at * (p9.G - b)) % 255; }
                else
                {
                    q2 = (at * (-1) * (p9.G - b)) % 255;
                    q2 = 255 - q2;
                }

                d = p9.B - b;
                if (d > 0)
                { q3 = (at * (p9.B - b)) % 255; }
                else
                {
                    q3 = (at * (-1) * (p9.B - b)) % 255;
                    q3 = 255 - q3;
                }

                c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            }
            else
            {
                c1 = Color.FromArgb(0, 0, 0);
                // c1 = p9;
            }
            return c1;
        }

        public Bitmap EncryptionAffineMaskesiz(int a, int b)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineEnc(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap DecryptionAffineMaskesiz(int a, int b)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineDcp(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap EncryptionAffineMaskeli(int a)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, b; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    b = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineEnc(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionAffineMaskeli(int a)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();
            int i, j, b; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    b = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineDcp(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


   

        public void SayilarFile()
        {
            FileStream fs = new FileStream("c:\\Medpic\\histosayi.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            int i;
            for (i = 0; i < Sayilar.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i, Sayilar[i]); }
            dosya.Close();
        }


        public double EntropiHesapla()
        {
            int i; double E; E = 0;
            for (i = 0; i < m; i++)
            { if (pi[i] > 0) E = E - (pi[i] * Math.Log(pi[i])); }
            return E;
        }


        public int OrtalamaHesapla()  //Mean
        {
            double w; int i, t;
            w = 0;
            for (i = 0; i < m; i++)
            { w = w + i * pi[i]; }
            t = (int)w;
            return t;
        }


        public double VaryansHesapla()  //variance
        {
            double w, t; int i;
            t = 0;
            for (i = 0; i < m; i++)
            { t = t + i * pi[i]; }
            w = 0;
            for (i = 0; i < m; i++)
            { w = w + (i - t) * (i - t) * pi[i]; }

            return w;
        }

        public void DagilimHesapla()
        {
            int i, x;
            for (i = 0; i < Sayilar.Length; i++)
            {
                x = (int)Sayilar[i];
                fi[x] = fi[x] + 1;
                pi[x] = (double)fi[x] / (double)Sayilar.Length;
            }
        }

        public void DagilimFile()
        {
            FileStream fs = new FileStream("c:\\Medpic\\histopi.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            int i;
            for (i = 0; i < pi.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i, pi[i]); }
            dosya.Close();
        }  

        public string Hex2binary(string hexvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt64(hexvalue, 16), 2);
            return binaryval;
        }
        public string Binary2hex(string binvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt64(binvalue, 2), 16);
            return binaryval;
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



        public string Decimaltobin64(ulong a)
        {
            string sonuc = string.Empty;
            string tampon = string.Empty;
            ulong n; n = a;
            int t = 0;
            for (ulong i = 0; n > 0; i++)
            {
                sonuc = n % 2 + sonuc;
                n = n / 2;
            }

            if (sonuc.Length < 64)
                t = 64 - sonuc.Length;

            for (int k = 0; k < t; k++)
            { tampon = tampon + '0'; }
            sonuc = tampon + sonuc;


            if (a == 0) sonuc = "0000000000000000000000000000000000000000000000000000000000000000";
            return sonuc;
        }


        public string Binarytohex64(string binaryvalue)
        {
            int i, k, n;
            string output;
            string sHex = "";

            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            char[] bits = binaryvalue.ToCharArray();

            i = keys.Length - binaryvalue.Length;

            for (k = 0; k < bits.Length; k++)
            {
                if (bits[k] == '0')
                    keys[i + k] = '0';
                if (bits[k] == '1')
                    keys[i + k] = '1';
            }

            i = 0; n = 0; output = "";
            do
            {
                if (keys[i] == '0')
                    output = output + "0";
                if (keys[i] == '1')
                    output = output + "1";

                n = n + 1;
                if (n == 4)
                {
                    string hexvalue = "";
                    hexvalue = Convert.ToString(Convert.ToInt64(output, 2), 16);
                    sHex = sHex + hexvalue;
                    output = ""; n = 0;
                }
                i++;
            } while (i < keys.Length);

            return sHex;
        }


        public string Decimaltohex64(ulong a)
        {
            int i, k, n, t;
            string output;
            string sHex = "";
            string binaryvalue = "";

            char[] keys = new char[64];
            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            binaryvalue = Decimaltobin64(a);

            char[] bits = binaryvalue.ToCharArray();
            for (k = 0; k < bits.Length; k++)
            {
                if (bits[k] == '0')
                    keys[k] = '0';
                if (bits[k] == '1')
                    keys[k] = '1';
            }

            i = 0; n = 0; output = "";
            do
            {
                if (keys[i] == '0')
                    output = output + "0";
                if (keys[i] == '1')
                    output = output + "1";

                n = n + 1;
                if (n == 4)
                {
                    string hexvalue = "";
                    hexvalue = Convert.ToString(Convert.ToInt64(output, 2), 16);
                    sHex = sHex + hexvalue;
                    output = ""; n = 0;
                }
                i++;
            } while (i < keys.Length);

            return sHex;
        }
        public string Double2hex(double x)
        {
            long m = BitConverter.DoubleToInt64Bits(x);  //double to Int64

            string str = Convert.ToString(m, 2);         //Int64 to binarystring

            string strhex = Convert.ToString(Convert.ToInt64(str, 2), 16);   //binarystring to hexstring

            return strhex;
        }

        public double Hex2Double(string hexstrg)
        {
            string binarystrg = Convert.ToString(Convert.ToInt64(hexstrg, 16), 2);      //hexstring to binary string  

            long n = Convert.ToInt64(binarystrg, 2);                                    //binary string to long (Int64)

            double x = BitConverter.Int64BitsToDouble(n);                             //  Int64 to double           
            return x;
        }


        public void PRNGUreteciLcg(int a, int x0, int c)
        {
            int i, x, x1;
            x1 = x0;
            for (i = 0; i < Sayilar.Length; i++)
            {
                x = (a * x1 + c) % m;
                Sayilar[i] = x;
                x1 = x;
            }
        }



        public Bitmap MaskeUreteciLcg(int a, int x0, int c)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            int x, x1;
            x1 = x0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);

                    x = (a * x1 + c) % 255;
                    p9 = Color.FromArgb((int)x, (int)x, (int)x);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;
                }
            }
            return bmp2;
        }


        public void PRNGUreteciLogistic(double x0, double mu)
        {
            double x, x1, z; int i;
            x1 = x0;
            for (i = 0; i < Sayilar.Length; i++)
            {
                x = mu * x1 * (1 - x1);
                z = m * x;
                Sayilar[i] = (int)z;
                x1 = x;
            }
        }

        public Bitmap MaskeUreteciLogistic(double x0, double mu)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            double x, x1, z;
            x1 = x0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    x = mu * x1 * (1 - x1);
                    z = x * 255;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;
                }
            }
            return bmp2;
        }
       


        public void KeyGeneraterExorShift64()
        {
            Y1bilgiHex = "";
            Y1bilgiBinary = "";
            Y1bilgiUlong = 0;

            Y0stringUreteci1();

            KeyFinal = "";
            KeyFinal = Y1bilgiHex;
            File.WriteAllText("c:\\Medpic\\KeyHex.txt", KeyFinal);
        }

        public void AnahtarAlExorShiftHex64()
        {
            int n, q;
            string KeyDizisi = File.ReadAllText("c:\\Medpic\\KeyHex.txt");
            n = KeyDizisi.Length % 16;
            if (n == 0)
            {
                q = (int)KeyDizisi.Length / 16;
                if (q == 1)
                {
                    Y1bilgiHex = KeyDizisi.Substring(0, 16);
                    Y1bilgiUlong = ulong.Parse(Y1bilgiHex, System.Globalization.NumberStyles.HexNumber);
                    //  Y1bilgiUlong = Convert.ToUInt64(Y1bilgiHex, 16);           
                }
            }
            else
            {
                Y1bilgiUlong = 0;

            }

        }




        public void KeyGeneraterExorShift192()
        {
            abilgiString = "";
            abilgiHex = "";
            abilgiBinary = "";
            abilgiDouble = 0.0;

            Y1bilgiHex = "";
            Y1bilgiBinary = "";
            Y1bilgiUlong = 0;

            Y2bilgiHex = "";
            Y2bilgiBinary = "";
            Y2bilgiUlong = 0;

            aUreteci2();
            Y0stringUreteci1();
            Y0stringUreteci2();

            KeyFinal = "";
            KeyFinal = abilgiHex + Y1bilgiHex + Y2bilgiHex;
            File.WriteAllText("c:\\Medpic\\KeyHex.txt", KeyFinal);
        }


        public void AnahtarAlExorShiftHex192()
        {
            int n, q;
            string KeyDizisi = File.ReadAllText("c:\\Medpic\\KeyHex.txt");
            n = KeyDizisi.Length % 16;
            if (n == 0)
            {
                q = (int)KeyDizisi.Length / 16;
                if (q == 3)
                {
                    abilgiHex = KeyDizisi.Substring(0, 16);
                    abilgiDouble = Hex2Double(abilgiHex);

                    Y1bilgiHex = KeyDizisi.Substring(16, 16);
                    Y1bilgiUlong = ulong.Parse(Y1bilgiHex, System.Globalization.NumberStyles.HexNumber);
                    //  Y1bilgiUlong = Convert.ToUInt64(Y1bilgiHex, 16);

                    Y2bilgiHex = KeyDizisi.Substring(32, 16);
                    Y2bilgiUlong = ulong.Parse(Y2bilgiHex, System.Globalization.NumberStyles.HexNumber);
                    // Y2bilgiUlong = Convert.ToUInt64(Y2bilgiHex, 16);               
                }
            }
            else
            {
                abilgiDouble = 0.0;
                Y1bilgiUlong = 0;
                Y2bilgiUlong = 0;
            }

        }



        public void KeyGeneraterExorShift256()
        {
            ulong M1;

            abilgiString = "";
            abilgiHex = "";
            abilgiBinary = "";
            abilgiDouble = 0.0;

            Y1bilgiHex = "";
            Y1bilgiBinary = "";
            Y1bilgiUlong = 0;

            Y2bilgiHex = "";
            Y2bilgiBinary = "";
            Y2bilgiUlong = 0;

            M1bilgiHex = "";
            M1bilgiBinary = "";
            M1bilgiUlong = 0;         

            aUreteci2();
            Y0stringUreteci1();
            Y0stringUreteci2();

            M1 = UlongKeyUreteci();
            M1bilgiUlong = M1;
            M1bilgiHex = Decimaltohex64(M1);
            M1bilgiBinary = Decimaltobin64(M1);

            KeyFinal = "";
            KeyFinal = abilgiHex + Y1bilgiHex + Y2bilgiHex + M1bilgiHex;
            File.WriteAllText("c:\\Medpic\\KeyHex.txt", KeyFinal);
        }



        public void AnahtarAlExorShiftHex256()
        {
            int n, q;
            string KeyDizisi = File.ReadAllText("c:\\Medpic\\KeyHex.txt");
            n = KeyDizisi.Length % 16;
            if (n == 0)
            {
                q = (int)KeyDizisi.Length / 16;
                if (q == 4)
                {
                    abilgiHex = KeyDizisi.Substring(0, 16);
                    abilgiDouble = Hex2Double(abilgiHex);

                    Y1bilgiHex = KeyDizisi.Substring(16, 16);
                    Y1bilgiUlong = ulong.Parse(Y1bilgiHex, System.Globalization.NumberStyles.HexNumber);
                    //  Y1bilgiUlong = Convert.ToUInt64(Y1bilgiHex, 16);

                    Y2bilgiHex = KeyDizisi.Substring(32, 16);
                    Y2bilgiUlong = ulong.Parse(Y2bilgiHex, System.Globalization.NumberStyles.HexNumber);
                    // Y2bilgiUlong = Convert.ToUInt64(Y2bilgiHex, 16);     

                    M1bilgiHex = KeyDizisi.Substring(48, 16);
                    M1bilgiUlong = ulong.Parse(M1bilgiHex, System.Globalization.NumberStyles.HexNumber);
                    // Y2bilgiUlong = Convert.ToUInt64(Y2bilgiHex, 16);               
                }
            }
            else
            {
                abilgiDouble = 0.0;
                Y1bilgiUlong = 0;
                Y2bilgiUlong = 0;
                M1bilgiUlong = 0;
            }

        }


        public void KeyGeneraterExorMerdiveni()
        {    int i; ulong M;
            string bilgi, mkey;         

            abilgiString = "";
            abilgiHex = "";
            abilgiBinary = "";
            abilgiDouble = 0.0;

            Y1bilgiHex = "";
            Y1bilgiBinary = "";
            Y1bilgiUlong = 0;

            Y2bilgiHex = "";
            Y2bilgiBinary = "";
            Y2bilgiUlong = 0;

            for (i = 0; i < KeyMler.Length; i++)
            { KeyMler[i] = 0; }

            //aUreteci2();
            //Y0stringUreteci1();
            //Y0stringUreteci2();    

            abilgiDouble = 0.81787566346257;
            string aBilgi = Double2hex(abilgiDouble);
            abilgiHex = aBilgi;
            abilgiBinary = Hex2binary(aBilgi);
            Y1bilgiHex = "24006b91d495fa00";
            Y1bilgiUlong = ulong.Parse(Y1bilgiHex, System.Globalization.NumberStyles.HexNumber);
            Y2bilgiHex = "3b38d61f11a25400";
            Y2bilgiUlong = ulong.Parse(Y2bilgiHex, System.Globalization.NumberStyles.HexNumber);


            KeyFinal = "";
            KeyFinal = abilgiHex + Y1bilgiHex + Y2bilgiHex;

            if (KeyMler.Length >3)
            {
                mkey = ""; bilgi = "";
                for (i = 3; i < KeyMler.Length; i++)
                {
                    M = UlongKeyUreteci();
                    KeyMler[i] = M;
                    bilgi = Decimaltohex64(M);
                    mkey = mkey + bilgi;
                }
                KeyFinal = KeyFinal + mkey;
            }
            File.WriteAllText("c:\\Medpic\\KeyHex.txt", KeyFinal); 
            
        }

        public void AnahtarAlExorMerdiveniHex()
        {
            ulong t; int i, n, q;
            string bilgi = "";
            string KeyDizisi = File.ReadAllText("c:\\Medpic\\KeyHex.txt");

            n = KeyDizisi.Length % 16;
            if (n == 0)
            {
                q = (int)KeyDizisi.Length / 16;

                for (i = 0; i < q; i++)
                {
                    bilgi = KeyDizisi.Substring(16 * i, 16);

                    if (i == 0)
                    {   abilgiHex = bilgi;
                        abilgiDouble = Hex2Double(abilgiHex);
                        KeyMler[0] = 0;
                    }

                    if (i == 1)
                    {   Y1bilgiHex = bilgi;
                        Y1bilgiUlong = ulong.Parse(Y1bilgiHex, System.Globalization.NumberStyles.HexNumber);                      
                        KeyMler[1] = 0;                      
                    }

                    if (i == 2)
                    {   Y2bilgiHex = bilgi;
                        Y2bilgiUlong = ulong.Parse(Y2bilgiHex, System.Globalization.NumberStyles.HexNumber);
                        KeyMler[2] = 0;
                    }

                    if (i > 2)
                    {
                        t = ulong.Parse(bilgi, System.Globalization.NumberStyles.HexNumber);
                        KeyMler[i] = t;
                    }
                }
            }
            else
            {
                abilgiDouble = 0.0;
                Y1bilgiUlong = 0;
                Y2bilgiUlong = 0;
                for (i = 0; i < KeyMler.Length; i++)
                { KeyMler[i] = 0; }                
            }

        }




        public void PRNGUreteciExor64()
        {
            int i, k, q1; ulong y, y1; y1 = 0;

            y1 = Y1bilgiUlong;

            char[] key = new char[8];
            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            for (i = 0; i < Sayilar.Length; i++)
            {
                y = (y1 ^ (y1 << 3));
                y = y & ulong.MaxValue;
                string Rstr = Decimaltobin64(y);
                char[] Ra = Rstr.ToCharArray();
                for (k = 0; k < Ra.Length; k++)
                { keys[k] = Ra[k]; }
                key[0] = keys[0];
                key[1] = keys[1];
                key[2] = keys[2];
                key[3] = keys[3];
                key[4] = keys[4];
                key[5] = keys[5];
                key[6] = keys[6];
                key[7] = keys[7];
                string bilR = new string(key);
                q1 = int.Parse(bilR);
                q1 = q1 & 255;
                Sayilar[i] = q1;
                y1 = y;
            }

        }

        public void PRNGUreteciExor192()
        {
            int i, k, q1; double a;
            ulong y, y1, y2; y1 = 0; y2 = 0;

            a = abilgiDouble;
            y1 = Y1bilgiUlong;
            y2 = Y2bilgiUlong;

            char[] key = new char[8];
            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            for (i = 0; i < Sayilar.Length; i++)
            {

                y = (y1 ^ (y1 << 3)) + (ulong)(a * y2);

                y = y & ulong.MaxValue;

                string Rstr = Decimaltobin64(y);
                char[] Ra = Rstr.ToCharArray();
                for (k = 0; k < Ra.Length; k++)
                { keys[k] = Ra[k]; }

                key[0] = keys[0];
                key[1] = keys[8];
                key[2] = keys[16];
                key[3] = keys[24];
                key[4] = keys[32];
                key[5] = keys[40];
                key[6] = keys[48];
                key[7] = keys[56];

                string bilR = new string(key);
                q1 = int.Parse(bilR);
                q1 = q1 & 255;
                Sayilar[i] = q1;
                y2 = y1;
                y1 = y;
            }

        }

        public void PRNGUreteciExor256()
        {
            int i, k, q1; double a;
            ulong y, y1, y2, M1; y1 = 0; y2 = 0;

            a = abilgiDouble;
            y1 = Y1bilgiUlong;
            y2 = Y2bilgiUlong;
            M1 = M1bilgiUlong;

            char[] key = new char[8];
            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            for (i = 0; i < Sayilar.Length; i++)
            {
                y = (y1 ^ (y1 << 3)) + (ulong)(a * y2);
                y = y & ulong.MaxValue;
                q1 = UlongMaskKey(y, M1);
                q1 = q1 & 255;
                Sayilar[i] = q1;
                y2 = y1;
                y1 = y;
            }

        }


        public void PRNGUreteciExorMerdiveni()
        {   int i, q1; double a;
            ulong y, y1, y2; 
            a = 0.0;y1 = 0; y2 = 0;
            a = abilgiDouble;
            y1 = Y1bilgiUlong;
            y2 = Y2bilgiUlong;
           
            for (i = 0; i < Sayilar.Length; i++)
            {
                y = (y1 ^ (y1 << 3)) + (ulong)(a * y2);
                y = y & ulong.MaxValue;
                q1 = Exormerdiveni(y, KeyMler);
                q1 = q1 & 255;
                Sayilar[i] = q1;
                y2 = y1;
                y1 = y;
            }

        }


        public Bitmap MaskeUreteciExor64()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j, k, q1; Color p9;
            ulong y, y1; y1 = 0;

            y1 = Y1bilgiUlong;


            char[] key = new char[8];
            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);

                    y = (y1 ^ (y1 << 3));

                    y = y & ulong.MaxValue;


                    string Rstr = Decimaltobin64(y);

                    char[] Ra = Rstr.ToCharArray();

                    for (k = 0; k < Ra.Length; k++)
                    {
                        keys[k] = Ra[k];
                    }

                    key[0] = keys[0];
                    key[1] = keys[1];
                    key[2] = keys[2];
                    key[3] = keys[3];
                    key[4] = keys[4];
                    key[5] = keys[5];
                    key[6] = keys[6];
                    key[7] = keys[7];

                    string bilR = new string(key);

                    q1 = int.Parse(bilR);
                    q1 = q1 & 255;
                    p9 = Color.FromArgb((int)q1, (int)q1, (int)q1);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    y1 = y;
                }
            }
            return bmp2;
        }

        public Bitmap MaskeUreteciExor192()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j, k, q1; Color p9;
            double a;
            ulong y, y1, y2; y1 = 0; y2 = 0;

            a = abilgiDouble;
            y1 = Y1bilgiUlong;
            y2 = Y2bilgiUlong;

            char[] key = new char[8];
            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);

                    y = (y1 ^ (y1 << 3)) + (ulong)(a * y2);

                    y = y & ulong.MaxValue;


                    string Rstr = Decimaltobin64(y);

                    char[] Ra = Rstr.ToCharArray();

                    for (k = 0; k < Ra.Length; k++)
                    {
                        keys[k] = Ra[k];
                    }

                    key[0] = keys[0];
                    key[1] = keys[8];

                    key[2] = keys[16];
                    key[3] = keys[24];

                    key[4] = keys[32];
                    key[5] = keys[40];

                    key[6] = keys[48];
                    key[7] = keys[56];

                    string bilR = new string(key);

                    q1 = int.Parse(bilR);
                    q1 = q1 & 255;
                    p9 = Color.FromArgb((int)q1, (int)q1, (int)q1);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    y2 = y1;
                    y1 = y;
                }
            }
            return bmp2;
        }

        public Bitmap MaskeUreteciExor256()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j, k, q1; Color p9;
            double a;
            ulong y, y1, y2, M1; y1 = 0; y2 = 0; M1 = 0;


            a = abilgiDouble;
            y1 = Y1bilgiUlong;
            y2 = Y2bilgiUlong;
            M1 = M1bilgiUlong;

            char[] key = new char[8];
            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);

                    y = (y1 ^ (y1 << 3)) + (ulong)(a * y2);

                    y = y & ulong.MaxValue;

                    q1 = UlongMaskKey(y, M1);
                    q1 = q1 & 255;
                    p9 = Color.FromArgb((int)q1, (int)q1, (int)q1);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);

                    y2 = y1;
                    y1 = y;
                }
            }
            return bmp2;
        }


        public Bitmap MaskeUreteciExorMerdiveni()
        {
            Bitmap bmp1 = (Bitmap)Maske.Clone();
            Bitmap bmp2 = (Bitmap)Maske.Clone();
            int i, j, q1, q3; Color p9;          
            double a;   ulong y, y1, y2;
            a = 0.0; y1 = 0; y2 = 0; 

            a = abilgiDouble;
            y1 = Y1bilgiUlong;
            y2 = Y2bilgiUlong;               

            if (KeyMler.Length >2)
            {
                y = 0;
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (i = 0; i < bmp1.Width; i++)
                    {
                        p9 = bmp1.GetPixel(i, j);

                        y = (y1 ^ (y1 << 3)) + (ulong)(a * y2);
                        y = y & ulong.MaxValue; 
                        
                        q3 = Exormerdiveni(y, KeyMler);
                        p9 = Color.FromArgb((int)q3, (int)q3, (int)q3);
                        bmp2.SetPixel(i, j, p9);
                        Maske.SetPixel(i, j, p9);
                        y2 = y1;
                        y1 = y;
                    }
                }
            }

            else
            {
                for (j = 0; j < bmp1.Height; j++)
                {
                    for (i = 0; i < bmp1.Width; i++)
                    {   q1 =0;
                        p9 = Color.FromArgb((int)q1, (int)q1, (int)q1);
                        bmp2.SetPixel(i, j, p9);
                        Maske.SetPixel(i, j, p9);                       
                    }
                }
            }

            return bmp2;
        }




        public int Exormerdiveni(ulong y, ulong[] M)
        {
            int i, q1, q2;
            ulong Maske;
            q1 = 0; q2 = 0;
            for (i = 3; i < M.Length; i++)
            {
                Maske = M[i];
                q1 = UlongMaskKey(y, Maske); q1 = q1 & 255;
                q2 = q2 ^ q1;
                q2 = q2 & 255;
            }

            return q2;
        }










       

    }
}
