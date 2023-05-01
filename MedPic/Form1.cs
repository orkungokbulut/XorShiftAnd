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
    public partial class Entropi : Form
    {
        Resim   Goruntu = new Resim();
        public FilterInfoCollection webcams;//webcam isminde tanımladığımız değişken bilgisayara kaç kamera bağlıysa onları tutan bir dizi. 
        public VideoCaptureDevice cam;//cam ise bizim kullanacağımız aygıt.


      

        public Entropi()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
             
            
            pictureBox1.Image = Goruntu.resim1;
            Goruntu.HistogramEsitle(Goruntu.resim1);


           
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg;*.tif; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.tif;*.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {   Goruntu.DosyaAdi = open.FileName;
                Goruntu.resim1 = new Bitmap(open.FileName);
                Goruntu.resim2 = new Bitmap(open.FileName);
                Goruntu.data = new int[Goruntu.resim1.Width, Goruntu.resim1.Height];
                int x, y;
                for (x = 0; x < Goruntu.resim1.Width; x++)
                {   for (y = 0; y < Goruntu.resim1.Height; y++)
                    { Goruntu.data[x, y] = -1; }
                }
                pictureBox1.Image = Goruntu.resim1;
                pictureBox2.Image = Goruntu.resim2;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {   SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (save.ShowDialog() == DialogResult.OK)
            {   Goruntu.resim1 = (Bitmap)pictureBox1.Image.Clone();                
                Goruntu.resim1.Save(save.FileName);
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {   pictureBox1.Image = Image.FromFile(Goruntu.DosyaAdi);
            Goruntu.resim1 = (Bitmap)pictureBox1.Image.Clone();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {   Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            bmp2 = Goruntu.GetCom(bmp1, 1, 0, 0); pictureBox2.Image = bmp2;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            bmp2 = Goruntu.GetCom(bmp1, 0, 1, 0); pictureBox2.Image = bmp2;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            bmp2 = Goruntu.GetCom(bmp1, 0, 0, 1); pictureBox2.Image = bmp2;
        }
        private void cMYToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            bmp2 = Goruntu.GetCom(bmp1, -1, -1, -1); pictureBox2.Image = bmp2;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {   Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(pictureBox1.Image);
            double[] hr, hg, hb; hr = new double[256]; hg = new double[256]; hb = new double[256];
            int i; double[] H; H = new double[3]; 

            Goruntu.Histogram(bmp1, hr, hg, hb);
            Goruntu.HistogramtFile(bmp1, hr, hg, hb);

            Goruntu.getEntropy(bmp1,H);           

            textBox6.Text = Convert.ToString(H[0]);
            textBox9.Text = Convert.ToString(H[1]);
            textBox13.Text = Convert.ToString(H[2]);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < 256; i++)
            {
                chart1.Series["Series1"].Points.AddY(1 * hr[i]);
                chart1.Series["Series2"].Points.AddY(1 * hg[i]);
                chart1.Series["Series3"].Points.AddY(1 * hb[i]);
            }
        }
     
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        { pictureBox2.Image = (Bitmap)(pictureBox1.Image.Clone());  }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (Bitmap)(pictureBox2.Image.Clone());

        }


        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(pictureBox2.Image);
            double[] PSNR; PSNR = new double[3];
            double[] MSE; MSE = new double[3];
            double[] ssim; ssim = new double[3];
            double[] PCC;  PCC = new double[3];
          
            
            Goruntu.GetMSE(bmp1, bmp2, MSE, PSNR);
            
            textBox5.Text = Convert.ToString(PSNR[0]);
            textBox8.Text = Convert.ToString(PSNR[1]);
            textBox12.Text = Convert.ToString(PSNR[2]);

          /*
             Goruntu.GetSSIM(bmp1, bmp2, ssim);
             textBox1.Text = Convert.ToString(ssim[0]);
             textBox2.Text = Convert.ToString(ssim[1]);
             textBox3.Text = Convert.ToString(ssim[2]);
          */
            
            Goruntu.GetPCC(bmp1, bmp2, PCC);
            textBox4.Text = Convert.ToString(PCC[0]);
            textBox7.Text = Convert.ToString(PCC[1]);
            textBox11.Text = Convert.ToString(PCC[2]);      
            
        }   




        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
           // Bitmap bmp1 = (Bitmap)eventArgs.Frame.Clone();//kısaca bu event'ta kameradan alınan görüntüyü picturebox'a atıyoruz.
           // pictureBox1.Image = bmp1;
            Goruntu.resim1 = (Bitmap)eventArgs.Frame.Clone();

        }

        private void KameraBaslat()
        {
            textBox1.Text = "Kamera Başlatılıyor...";
            if (cam != null)
            {
                if (!cam.IsRunning)
                { cam.Start(); }
            }
        }

        private void KameraDurdur()
        {
            textBox1.Text = "Kamera Durduruluyor...";

            if (cam != null)
            {
                if (cam.IsRunning)
                {
                    cam.SignalToStop();
                    cam = null;
                }
            }
        }

        Bitmap bmpSaved;
        private void timer1_Tick(object sender, EventArgs e)
        {
           

            Bitmap bmp3 = Goruntu.resim1;
            Bitmap bmp4 = Goruntu.resim1;

            Bitmap bmp1 = Goruntu.Pooling(bmp3, 3, 1);
            Bitmap bmp2 = bmp1;

            pictureBox1.Image = bmp1;

            if (bmpSaved != null && bmp1 != null && bmpSaved.Width >= pictureBox1.Width && bmpSaved.Height >= pictureBox1.Height)
            {
                for (int w = 0; w != bmpSaved.Width; w++)
                {
                    for (int h = 0; h != bmpSaved.Height; h++)
                    {
                        Color c1 = bmp1.GetPixel(w, h);
                        Color c2 = bmpSaved.GetPixel(w, h);
                        double benzerlik = Goruntu.MemberShip3(c1, c2, 128, 2);
                        Color c3 = benzerlik < 0.75 ? Color.FromArgb(255, 0, 0) : c1;
                        bmp2.SetPixel(w, h, c3);
                    }
                }
               
                pictureBox2.Image = bmp2;
            }
            bmpSaved = bmp1;

           // pictureBox1.Image =bmp1;
           // pictureBox2.Image =bmp2;

        }

        public void DiziKatla(double []x, double [] y)
        {   for (int i = 0; i < (x.Length-1); i++)
              {   textBox2.Text = Convert.ToString(i);                
                  if (i < (x.Length - 1)) 
                 {  y[2 * i] = x[i];  y[2 * i + 1] = (x[i] + x[i + 1]) / 2;  }
                
                 if (i == (x.Length - 2))
                 {   y[2 * (i + 1)] = x[i + 1];     y[2 * (i +1)+1] = x[i + 1]; textBox1.Text = Convert.ToString(i); } 
              }
         }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
             OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Goruntu.DosyaAdi = open.FileName;
                Goruntu.resim2 = new Bitmap(open.FileName);
                pictureBox2.Image = Goruntu.resim2;
            }
        }

        private void shellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] q = new double[4]; int i;
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(pictureBox1.Image);
           
            bmp2 = Goruntu.ShellHistogram4(bmp1, q); pictureBox2.Image = bmp2;

            FileStream fs = new FileStream("c:\\Medpic\\Kabuk.txt", FileMode.Append, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            String sline = "";
            String satir = Convert.ToString(q[0]);
            if (satir.Length < 8) satir = satir + "00000000";
            satir = satir.Substring(0, 8);
            sline = satir;
            for (i = 1; i < q.Length; i++)
            {
                satir = Convert.ToString(q[i]);
                if (satir.Length < 8) satir = satir + "00000000";
                satir = satir.Substring(0, 8); textBox1.Text = satir;
                sline = sline + ":" + satir;
            }
            satir = textBox3.Text;
            sline = sline + ":" + satir; textBox2.Text = sline;
            dosya.WriteLine("{0}", sline);
            dosya.Close();

            chart1.Series["Series1"].Points.Clear();
            for (i = 0; i < q.Length; i++)
            { chart1.Series["Series1"].Points.AddXY(i+1, q[i]); }

        }

        private void caesarEncpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            int key; key = 173;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            bmp2 = Sifre.EncryptionCaesarMaskesiz(key); pictureBox2.Image = bmp2;

            double benzerlik = Goruntu.Correlation(bmp1, bmp2); textBox1.Text = Convert.ToString(benzerlik);
           
        }

        private void caesarDcpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            int key; key = 173;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            bmp2 = Sifre.DecryptionCaesarMaskesiz(key); pictureBox2.Image = bmp2;   
        }

        private void eXORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            int key; key = 173;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            bmp2 = Sifre.EncryptionExorMaskesiz(key); pictureBox2.Image = bmp2;

            double benzerlik = Goruntu.Correlation(bmp1, bmp2); textBox1.Text = Convert.ToString(benzerlik);
        }

        private void affineEncpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            int a, b;

            a = 37; b = 39; ;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            bmp2 = Sifre.EncryptionAffineMaskesiz(a, b); pictureBox2.Image = bmp2;

            double benzerlik = Goruntu.Correlation(bmp1, bmp2); textBox1.Text = Convert.ToString(benzerlik);


            int at, d, y, test, q1, q2, bilgi;
            bilgi = 254;

            test = Sifre.GCD(a, 256); textBox1.Text = Convert.ToString(test);
            q1 = (a * bilgi + b) % 255; textBox2.Text = Convert.ToString(q1);
            at = Sifre.ModuloTersi(a, 255); textBox2.Text = Convert.ToString(at);
            d = q1 - b;
            if (d > 0)
            { q2 = (at * (q1 - b)) % 255; }
            else
            {
                q2 = (at * (-1) * (q1 - b)) % 255;
                q2 = 255 - q2;
            }

            textBox3.Text = Convert.ToString(q2);
        }

        private void affineDcpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            int a, b; a = 37; b = 39; ;
            Crypto Sifre = new Crypto(bmp1, 200, 8);
            bmp2 = Sifre.DecryptionAffineMaskesiz(a, b); pictureBox2.Image = bmp2;       
        
        }




        private void pRNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            

        }

        private void ımageEncToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

           
        }

        private void ımageDcpToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void griEsiklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);

         //   bmp2 = Goruntu.GriEsik1(bmp1, 20); pictureBox2.Image = bmp2;
            bmp2 = Goruntu.GriEsik2(bmp1, 100,200); pictureBox2.Image = bmp2;
        }

        private void eXORToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        private void lCGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N;  N = 400; k = 8;
            double E, varyas, ort;          

            Crypto Sifre = new Crypto(bmp1, N, k);
            
            Sifre.PRNGUreteciLcg(37, 13, 43);
            Sifre.SayilarFile();

            Sifre.DagilimHesapla();
            Sifre.DagilimFile();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }


            /*
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {   chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
               // chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }
            */

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);         


            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;
        }

        private void lojistikHaritaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N;  N = 400; k = 8;
            double E, varyas, ort;         

            Crypto Sifre = new Crypto(bmp1, N, k);

            Sifre.PRNGUreteciLogistic(0.201,3.786);
            Sifre.SayilarFile();           

            /*
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }
             */
            
            Sifre.DagilimHesapla();
            Sifre.DagilimFile();
           
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {   chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
               // chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }
            
            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);          

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;
        }

        private void exorShiftAndToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        

        private void eXORToolStripMenuItem2_Click(object sender, EventArgs e)
        {
           
        }

        private void lCGToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; double E, varyas, ort;
            N = 400; k = 8;

            Crypto Sifre = new Crypto(bmp1, N, k);


            Sifre.PRNGUreteciLcg(37, 13, 43);

            Sifre.DagilimHesapla();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }


            
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {   chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
               // chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }
            

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);

            /*
            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i <N; i++)
            {   Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i <N; i++)
            {   
               //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }
            */


            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;
        }

        private void lojistikHaritaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; double E, varyas, ort;
            N = 400; k = 8;

            Crypto Sifre = new Crypto(bmp1, N, k);

            Sifre.PRNGUreteciLogistic(0.201, 3.786);

            Sifre.DagilimHesapla();


            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {   chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
               // chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }
            

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);

            /*
            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i <N; i++)
            {   Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i <N; i++)
            {   
               //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }
            */


            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;
        }

        private void exorShiftAndToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

       

        private void exorshiftToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            //Sifre.AnahtarAlExorShiftHex64();
            veri = Sifre.MaskeUreteciExor64(); pictureBox1.Image = veri;
            bmp2 = Sifre.DecryptionExorMaskeli(); pictureBox2.Image = bmp2;
        }

        private void lojistikHarita128ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1);
            Crypto Sifre = new Crypto(bmp1, 400, 8);

            veri = Sifre.MaskeUreteciLogistic(0.201, 3.786); pictureBox1.Image = veri;
            bmp2 = Sifre.DecryptionExorMaskeli(); pictureBox2.Image = bmp2;          
        }

        private void lojistikHarita128ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); 
            Crypto Sifre = new Crypto(bmp1, 400, 8);
         
            veri = Sifre.MaskeUreteciLogistic(0.201, 3.786); pictureBox1.Image = veri;
            bmp2 = Sifre.EncryptionExorMaskeli(); pictureBox2.Image = bmp2; 
        }

        private void exorShiftAnd192ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 400, 8);

           //Sifre.KeyGeneraterExorShift192();
            veri = Sifre.MaskeUreteciExor192(); pictureBox1.Image = veri;
            bmp2 = Sifre.EncryptionExorMaskeli(); pictureBox2.Image = bmp2; 
            
        }

        private void exorshiftAnd192ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            Sifre.AnahtarAlExorShiftHex192();
            veri = Sifre.MaskeUreteciExor192(); pictureBox1.Image = veri;
            bmp2 = Sifre.DecryptionExorMaskeli(); pictureBox2.Image = bmp2;
        }

        private void lCGToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 400, 8);

            veri = Sifre.MaskeUreteciLcg(37, 13, 43); pictureBox1.Image = veri;
            bmp2 = Sifre.EncryptionExorMaskeli(); pictureBox2.Image = bmp2;           

        }

        private void lCGToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 400, 8);

            veri = Sifre.MaskeUreteciLcg(37, 13, 43); pictureBox1.Image = veri;           
            bmp2 = Sifre.DecryptionExorMaskeli(); pictureBox2.Image = bmp2;  
        }

        private void lojistikHaritaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; double E, varyans, ort;
            N = 400; k = 8;

            Crypto Sifre = new Crypto(bmp1, N, k);
       
            Sifre.PRNGUreteciLogistic(0.201, 3.786);
            Sifre.SayilarFile();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }
         
            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;        
            
        }

        private void lCGToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            double E, varyans, ort;            

            Crypto Sifre = new Crypto(bmp1, N, k);         

            Sifre.PRNGUreteciLcg(37, 13, 43); 
            Sifre.SayilarFile();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }  
     
            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;        
        }

        private void exorShiftAndToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; double E, varyans, ort;
            N = 400; k = 8;

            Crypto Sifre = new Crypto(bmp1, N, k);

           // Sifre.KeyGeneraterExorShift192();
            Sifre.PRNGUreteciExor192();
            Sifre.SayilarFile();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }   

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;         
        }

        private void eXORToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void lCGToolStripMenuItem3_Click(object sender, EventArgs e)
        {
           
        }

        private void lojistikHaritaToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void exorShiftAndToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void eXORToolStripMenuItem3_Click(object sender, EventArgs e)
        {   
                 
        }

        private void lCGToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            double E, varyas, ort;     

            Crypto Sifre = new Crypto(bmp1, N, k);

            Sifre.PRNGUreteciLcg(37, 13, 43);
            Sifre.SayilarFile();
          
            Sifre.DagilimHesapla();
            Sifre.DagilimFile();

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {  
                //chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
                  chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }        

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;        
        }

        private void lojistikHaritaToolStripMenuItem2_Click(object sender, EventArgs e)
        {   Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            double E, varyas, ort;           

            Crypto Sifre = new Crypto(bmp1, N, k);

            Sifre.PRNGUreteciLogistic(0.201, 3.786);
            Sifre.SayilarFile();
          
            Sifre.DagilimHesapla();
            Sifre.DagilimFile();

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);
        
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {  
                //chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
                  chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }
         
            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;        
        }

        private void exorShiftAndToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
        }

        private void eXORToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            
        }




        private void lCGToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            Crypto Sifre = new Crypto(bmp1, N, k);
           
            Sifre.PRNGUreteciLcg(37, 13, 43);
            Sifre.SayilarFile();
          
            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i < N; i++)
            {
                Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < N; i++)
            {
                //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }

            FileStream fs = new FileStream("c:\\Medpic\\histodft.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            for (i = 0; i < donus.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i - 200, donus[i].magnitude); }
            dosya.Close();

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;    

        }

        private void lojistikHaritaToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            Crypto Sifre = new Crypto(bmp1, N, k);

            Sifre.PRNGUreteciLogistic(0.201, 3.786);
            Sifre.SayilarFile();

            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i < N; i++)
            {
                Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < N; i++)
            {
                //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }

            FileStream fs = new FileStream("c:\\Medpic\\histodft.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            for (i = 0; i < donus.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i - 200, donus[i].magnitude); }
            dosya.Close();

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;    
        }

        private void exorShiftAndToolStripMenuItem3_Click(object sender, EventArgs e)
        {
          
        }

        private void exorShiftAnd256ToolStripMenuItem_Click(object sender, EventArgs e)
        {   Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 400, 8);

            //Sifre.KeyGeneraterExorShift256();
            veri = Sifre.MaskeUreteciExor256(); pictureBox1.Image = veri;
            bmp2 = Sifre.EncryptionExorMaskeli(); pictureBox2.Image = bmp2;     
        }

        private void exorShiftAnd256ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            Sifre.AnahtarAlExorShiftHex256();
            veri = Sifre.MaskeUreteciExor256(); pictureBox1.Image = veri;
            bmp2 = Sifre.DecryptionExorMaskeli(); pictureBox2.Image = bmp2;
        }

        private void scatterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            
            bmp1 = Goruntu.Scatter(bmp1, 1, 3, 2000); pictureBox1.Image = bmp1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);

            if (radioButton1.Checked == true && radioButton4.Checked == true)
            {
                bmp1 = Goruntu.Scatter(bmp1, 1, 1, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton1.Checked == true && radioButton5.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 1, 2, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton1.Checked == true && radioButton6.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 1, 3, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton2.Checked == true && radioButton4.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 2, 1, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton2.Checked && radioButton5.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 2, 2, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton2.Checked && radioButton6.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 2, 3, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton3.Checked && radioButton4.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 3, 1, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton3.Checked && radioButton5.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 3, 2, 2000); pictureBox2.Image = bmp1;
            }
            else if (radioButton3.Checked && radioButton6.Checked)
            {
                bmp1 = Goruntu.Scatter(bmp1, 3, 3, 2000); pictureBox2.Image = bmp1;
            }
        }

        private void exorShiftAnd256ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; double E, varyans, ort;
            N = 400; k = 8;

            Crypto Sifre = new Crypto(bmp1, N, k);

            //Sifre.KeyGeneraterExorShift256();
            Sifre.PRNGUreteciExor256();
            Sifre.SayilarFile();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }
            
            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;            
            
        }

        private void dFDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sayıÜretToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exorShiftAnd64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; double E, varyans, ort;
            N = 400; k = 8;

            Crypto Sifre = new Crypto(bmp1, N, k);

           //Sifre.KeyGeneraterExorShift64();
            Sifre.PRNGUreteciExor64();
            Sifre.SayilarFile();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }
     
            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;            
        }

        private void exorShiftAnd64ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 400, 8);

           //Sifre.KeyGeneraterExorShift64();
            veri = Sifre.MaskeUreteciExor64(); pictureBox1.Image = veri;
            bmp2 = Sifre.EncryptionExorMaskeli(); pictureBox2.Image = bmp2;    

        }

        private void exorShiftAnd64ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            double E, varyas, ort;

            Crypto Sifre = new Crypto(bmp1, N, k);

          //Sifre.KeyGeneraterExorShift64();
            Sifre.PRNGUreteciExor64();
            Sifre.SayilarFile();

            Sifre.DagilimHesapla();
            Sifre.DagilimFile();

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {
                //chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
                chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;            
        }

        private void exorShiftAnd192ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            double E, varyas, ort;

            Crypto Sifre = new Crypto(bmp1, N, k);

           // Sifre.KeyGeneraterExorShift192();
            Sifre.PRNGUreteciExor192();
            Sifre.SayilarFile();

            Sifre.DagilimHesapla();
            Sifre.DagilimFile();

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {
                //chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
                chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;            
        }

        private void exorShiftAnd256ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            double E, varyas, ort;

            Crypto Sifre = new Crypto(bmp1, N, k);

          //  Sifre.KeyGeneraterExorShift256();
            Sifre.PRNGUreteciExor256();
            Sifre.SayilarFile();

            Sifre.DagilimHesapla();
            Sifre.DagilimFile();

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {
                //chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
                chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;       
        }

        private void exorShiftAnd64ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            Crypto Sifre = new Crypto(bmp1, N, k);

           // Sifre.KeyGeneraterExorShift64();
            Sifre.PRNGUreteciExor64();
            Sifre.SayilarFile();


            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i < N; i++)
            {
                Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < N; i++)
            {
                //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }

            FileStream fs = new FileStream("c:\\Medpic\\histodft.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            for (i = 0; i < donus.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i - 200, donus[i].magnitude); }
            dosya.Close();

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;       
        }

        private void exorShiftAnd192ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            Crypto Sifre = new Crypto(bmp1, N, k);

           // Sifre.KeyGeneraterExorShift192();
            Sifre.PRNGUreteciExor192();
            Sifre.SayilarFile();


            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i < N; i++)
            {
                Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < N; i++)
            {
                //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }

            FileStream fs = new FileStream("c:\\Medpic\\histodft.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            for (i = 0; i < donus.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i - 200, donus[i].magnitude); }
            dosya.Close();

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;       
        }

        private void exorShiftAnd256ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            Crypto Sifre = new Crypto(bmp1, N, k);

          //  Sifre.KeyGeneraterExorShift256();
            Sifre.PRNGUreteciExor256();
            Sifre.SayilarFile();


            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i < N; i++)
            {
                Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < N; i++)
            {
                //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }

            FileStream fs = new FileStream("c:\\Medpic\\histodft.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            for (i = 0; i < donus.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i - 200, donus[i].magnitude); }
            dosya.Close();

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;       

        }

        private void xorMerdiveniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 400, 8);

            //Sifre.KeyGeneraterExorMerdiveni();
            Sifre.AnahtarAlExorMerdiveniHex(); 
            veri = Sifre.MaskeUreteciExorMerdiveni(); pictureBox1.Image = veri;
            bmp2 = Sifre.EncryptionExorMaskeli(); pictureBox2.Image = bmp2;     
        }

        private void xorMerdiveniToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Bitmap veri = new Bitmap(bmp1); int a; a = 37;
            Crypto Sifre = new Crypto(bmp1, 200, 8);

            Sifre.AnahtarAlExorMerdiveniHex(); 
            veri = Sifre.MaskeUreteciExorMerdiveni(); pictureBox1.Image = veri;
            bmp2 = Sifre.DecryptionExorMaskeli(); pictureBox2.Image = bmp2;
        }

        private void exorMerdiveniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; double E, varyans, ort;
            N = 400; k = 8;

            Crypto Sifre = new Crypto(bmp1, N, k);

            //Sifre.KeyGeneraterExorMerdiveni();
            Sifre.AnahtarAlExorMerdiveniHex();
            Sifre.PRNGUreteciExorMerdiveni();
            Sifre.SayilarFile();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.Sayilar.Length; i++)
            { chart1.Series["Series1"].Points.AddY(Sifre.Sayilar[i]); }

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;     
        }

        private void exorMerdiveniToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            double E, varyas, ort;

            Crypto Sifre = new Crypto(bmp1, N, k);

          //  Sifre.KeyGeneraterExorMerdiveni();
            Sifre.AnahtarAlExorMerdiveniHex();
            Sifre.PRNGUreteciExorMerdiveni();
            Sifre.SayilarFile();

            Sifre.DagilimHesapla();
            Sifre.DagilimFile();

            E = Sifre.EntropiHesapla(); textBox1.Text = Convert.ToString(E);
            ort = Sifre.OrtalamaHesapla(); textBox2.Text = Convert.ToString(ort);
            varyas = Sifre.VaryansHesapla(); textBox3.Text = Convert.ToString(varyas);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < Sifre.fi.Length; i++)
            {
                //chart1.Series["Series1"].Points.AddY(Sifre.fi[i]);
                chart1.Series["Series2"].Points.AddY(Sifre.pi[i]);
            }

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;       
        }

        private void xorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            DFT frekans = new DFT(bmp1);
            int i, k, N; N = 400; k = 8;
            Crypto Sifre = new Crypto(bmp1, N, k);

            Sifre.AnahtarAlExorMerdiveniHex();
            Sifre.PRNGUreteciExorMerdiveni();
            Sifre.SayilarFile();


            Complex[] input; input = new Complex[N];
            Complex[] output; output = new Complex[N];
            Complex[] donus; donus = new Complex[N];

            for (i = 0; i < N; i++)
            {
                Complex temp = new Complex(Sifre.Sayilar[i], 0);
                input[i] = temp;
            }
            output = frekans.dft(input, -1);
            donus = frekans.ArrayShift(output);

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            for (i = 0; i < N; i++)
            {
                //  chart1.Series["Series1"].Points.AddY(output[i].magnitude);
                chart1.Series["Series3"].Points.AddY(donus[i].magnitude);
            }

            FileStream fs = new FileStream("c:\\Medpic\\histodft.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            for (i = 0; i < donus.Length; i++)
            { dosya.WriteLine("{0:N4}\t{1:N5}", i - 200, donus[i].magnitude); }
            dosya.Close();

            listBox1.DataSource = null;
            listBox1.DataSource = Sifre.Sayilar;  
        }

    }
}
