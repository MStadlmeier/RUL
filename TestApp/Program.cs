using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RUL;
using RUL.Color;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            char input = ' ';
            while (input != 'c')
            {
                int acount = 0;
                int bcount = 0;
                int ccount = 0;
                int testCount = 10000;
                RandObject<string>[] randObjs = new RandObject<string>[] { new RandObject<string>("a", 0.8F), new RandObject<string>("b", 0.15F), new RandObject<string>("c", 0.05F) };
                for (int i = 0; i < testCount; i++)
                {
                    string result = Rul.RandElement(randObjs);
                    switch (result)
                    {
                        case "a":
                            acount++;
                            break;
                        case "b":
                            bcount++;
                            break;
                        case "c":
                            ccount++;
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("{0}, {1}, {2}", acount, bcount, ccount);
                Console.WriteLine("{0}, {1}, {2}\n\n", (float)acount / (float)testCount, (float)bcount / (float)testCount, (float)ccount / (float)testCount);
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