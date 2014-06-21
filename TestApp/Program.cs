using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RUL;
using RUL.Color;
using RUL.Noise;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            char input = ' ';
            while (input != 'c')
            {
                int w = 400;
                int h = 400;
                Bitmap bmp = new Bitmap(w,h);
                float[,] values = RulNoise.RandPerlinNoise2(w, h);
                float min = values.Cast<float>().Min();
                float max = values.Cast<float>().Max();
                float avg = values.Cast<float>().Average();
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        float val = values[x, y];
                        Color col;
                        if (val <= 0.58F)
                            col = Color.FromArgb(0, 0, 100);
                        else if (val <= 0.6F)
                            col = Color.FromArgb(0, 50, 255);
                        else if (val <= 0.63F)
                            col = Color.FromArgb(200, 200, 25);
                        else if (val <= 0.84F)
                            col = Color.FromArgb(75, 255, 25);
                        else if (val <= 0.9F)
                            col = Color.FromArgb(160, 160, 160);
                        else
                            col = Color.FromArgb(240, 240, 240);
                        bmp.SetPixel(x, y, col);
                    }
                }
                bmp.Save("M:\\Desktop\\perlintest.bmp");
                Console.WriteLine("Done.");
                Console.WriteLine("Min : {0}\nMax : {1}\nAvg : {2}\n", min, max, avg);
                input = Console.ReadKey().KeyChar;
            }
        }

        static void TestColors()
        {
            char input = ' ';

            while (input != 'c')
            {
                int totalR = 0;
                int totalG = 0;
                int totalB = 0;
                int n = 9;
                Bitmap bmp = new Bitmap(100 * n, 100 * n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Hues hue = (Hues)j;
                        Col randColor = new Col();
                        if (i < 3)
                            randColor = RulCol.RandColor(hue, LuminosityTypes.Light);
                        else if (i < 6)
                            randColor = RulCol.RandColor(hue, LuminosityTypes.Medium);
                        else
                            randColor = RulCol.RandColor(hue, LuminosityTypes.Dark);
                        totalR += randColor.R;
                        totalG += randColor.G;
                        totalB += randColor.B;
                        Color sysColor = Color.FromArgb(randColor.A, randColor.R, randColor.G, randColor.B);
                        for (int x = 0; x < 100; x++)
                        {
                            for (int y = 0; y < 100; y++)
                            {
                                bmp.SetPixel(x + i * 100, y + j * 100, sysColor);
                            }
                        }
                    }
                }
                bmp.Save("M:\\Desktop\\randcolor.bmp");
                Console.WriteLine("{0} / {1} / {2} ==> {3} ", totalR, totalG, totalB, (totalR + totalG + totalB) / (3 * n * n));
                input = Console.ReadKey().KeyChar;
            }
        }
    }
}