﻿using System;
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
                float minX = 0;
                float minY = 0;
                float maxX = 0;
                float maxY = 0;
                for (int i = 0; i < 100000; i++)
                {
                    var vec = RUL.Vector.RulVec.RandVec2(new Vec2(0, 1), (float)(2.5F * Math.PI));
                    if (i == 0)
                    {
                        minX = vec.X;
                        minY = vec.Y;
                        maxX = vec.X;
                        maxY = vec.Y;
                    }
                    if (vec.X < minX)
                        minX = vec.X;
                    if (vec.Y < minY)
                        minY = vec.Y;
                    if (vec.X > maxX)
                        maxX = vec.X;
                    if (vec.Y > maxY)
                        maxY = vec.Y;
                }
                Console.WriteLine("X : {0} / {1} // Y : {2} / {3}", minX, maxX, minY, maxY);
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