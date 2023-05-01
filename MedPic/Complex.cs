using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedPic
{

    public class Complex
    {
        public double real = 0.0;
        public double imag = 0.0;

        public Complex() { }

        public Complex(double real, double imag)
        {
            this.real = real;
            this.imag = imag;
        }

        public Complex(Complex copy)
        {
            this.real = copy.real;
            this.imag = copy.imag;
        }
        public string toString()
        {
            string data = real.ToString() + " " + imag.ToString() + "i";
            return data;
        }

        public static Complex from_polar(double r, double radians)
        {
            Complex data = new Complex(r * Math.Cos(radians), r * Math.Sin(radians));
            return data;
        }

        public static Complex operator +(Complex a, Complex b)
        {
            Complex data = new Complex(a.real + b.real, a.imag + b.imag);
            return data;
        }

        public static Complex operator -(Complex a, Complex b)
        {
            Complex data = new Complex(a.real - b.real, a.imag - b.imag);
            return data;
        }

        public static Complex operator *(Complex a, Complex b)
        {
            // Complex data = new Complex((a.real * b.real) - (a.imag * b.imag), (a.real * b.imag + (a.imag * b.real)));
            // return data;

            return new Complex(a.real * b.real - a.imag * b.imag, a.real * b.imag + a.imag * b.real);

        }

        public double magnitude
        {
            get
            { return Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imag, 2)); }
        }

        public double Phase
        {
            get
            {
                if (real != 0)
                {
                    return Math.Atan(imag / real);
                }
                else if (imag > 0)
                {
                    return Math.PI / 2;
                }
                else
                {
                    return -Math.PI / 2; //-90;
                }
            }
        }

        public double Aci
        {
            get
            {
                if (real == 0)
                {
                    if (imag == 0) return 0.0;
                    else if (imag > 0) return 90;
                    else return 270;
                }
                else if (imag == 0)
                {
                    if (real > 0) return 0.0;
                    else return 180;
                }
                else if (real < 0 && imag > 0)
                    return 180 - Math.Atan(imag / (-real)) * (180 / Math.PI);
                else if (real < 0 && imag < 0)
                    return 180 + Math.Atan(imag / real) * (180 / Math.PI);
                else if (real > 0 && imag < 0)
                    return 360 - Math.Atan((-imag) / real) * (180 / Math.PI);
                else
                    return Math.Atan(imag / real) * (180 / Math.PI);
            }

        }
    }

}
