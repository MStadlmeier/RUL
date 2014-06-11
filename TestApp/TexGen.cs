using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RUL;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace TestApp
{
    public static class TexGen
    {

        private static float[,] _baseNoise;

        public static float[,] PerlinNoise2(int width, int height, float zoom)
        {
            RandNoiseTex2(width, height);
            float[,] perlinNoise = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    perlinNoise[x, y] = TurbulenceAt(x, y, zoom);
                }
            }
            return perlinNoise;
        }

        public static float[,] RandNoiseTex2(int width, int height)
        {
            _baseNoise = new float[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    _baseNoise[x, y] = Rul.RandFloat();
            return _baseNoise;
        }

        public static Bitmap GetTerrain(int width, int height, float zoom, bool saveNoise)
        {
            float[,] noise = PerlinNoise2(width, height, zoom);
            int imgSize = 1;

            Color[,] terrainTexture = new Color[width * imgSize, height * imgSize];
            //Noise x
            for (int x = 0; x < width; x++)
            {
                //Noise y
                for (int y = 0; y < width; y++)
                {
                    int startX = x * imgSize;
                    int startY = y * imgSize;
                    //Tile texture x
                    for (int posX = 0; posX < imgSize; posX++)
                    {
                        //Tile texture y
                        for (int posY = 0; posY < imgSize; posY++)
                        {
                            float noiseVal = noise[x, y];
                            if (noiseVal > 0.80F)//Stone
                                terrainTexture[startX + posX, startY + posY] = Color.FromArgb(0xFFFFFF);
                            else if (noiseVal > 0.77F)//Rocks
                                terrainTexture[startX + posX, startY + posY] = Color.FromArgb(0x999999);
                            else if (noiseVal > 0.6F)//Grass
                                terrainTexture[startX + posX, startY + posY] = Color.FromArgb(0x19A319);
                            else if (noiseVal > 0.55F)//Dirt
                                terrainTexture[startX + posX, startY + posY] = Color.FromArgb(0xE8BF6C);
                            else//Water
                                terrainTexture[startX + posX, startY + posY] = Color.FromArgb(7, 30, 240);

                        }
                    }
                }
            }
            if (saveNoise)
            {
                Bitmap noiseBmp = ToBitmap(noise);
                noiseBmp.Save("M:\\Desktop\\noise_raw.bmp");
            }
            return ToBitmap(terrainTexture);
        }

        private static unsafe Bitmap ToBitmap(Color[,] rawData)
        {
            int width = rawData.GetLength(0);
            int height = rawData.GetLength(1);

            Bitmap image = new Bitmap(width, height);
            BitmapData bmpData = image.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            ColorARGB* startingPosition = (ColorARGB*)bmpData.Scan0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    ColorARGB col = new ColorARGB(rawData[x, y]);
                    ColorARGB* position = startingPosition + x + y * width;
                    position->A = 255;
                    position->R = col.R;
                    position->G = col.G;
                    position->B = col.B;
                }
            }
            image.UnlockBits(bmpData);
            return image;
        }

        private static unsafe Bitmap ToBitmap(float[,] rawData)
        {
            int width = rawData.GetLength(0);
            int height = rawData.GetLength(1);

            Color[,] colData = new Color[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int colValue = (int)(rawData[x, y] * 255);
                    colData[x, y] = Color.FromArgb(colValue,colValue,colValue);
                }
            }
            return ToBitmap(colData);
        }

        private static float SmoothNoiseAt(float x, float y)
        {
            int width = _baseNoise.GetLength(0);
            int height = _baseNoise.GetLength(1);
            float fracX = x - (int)x;
            float fracY = y - (int)y;

            //Adjacent points
            int x1 = ((int)x + width) % width;
            int x2 = (x1 + width - 1) % width;
            int y1 = ((int)y + height) % height;
            int y2 = (y1 + height - 1) % height;

            //Interpolate
            float value = 0;
            value += fracX * fracY * _baseNoise[x1, y1];
            value += fracX * (1 - fracY) * _baseNoise[x1, y2];
            value += (1 - fracX) * fracY * _baseNoise[x2, y1];
            value += (1 - fracX) * (1 - fracY) * _baseNoise[x2, y2];
            return value;
        }

        private static float TurbulenceAt(float x, float y, float initialZoom)
        {
            float value = 0;
            float zoom = initialZoom;

            while (zoom >= 1)
            {
                value += SmoothNoiseAt(x / zoom, y / zoom) * zoom;
                zoom /= 2F;
            }

            return (0.5F * value / initialZoom);
        }
    }

    public struct ColorARGB
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;

        public ColorARGB(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public ColorARGB(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }
    }
}