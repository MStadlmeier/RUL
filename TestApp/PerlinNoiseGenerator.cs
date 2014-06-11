using System;
using System.Collections.Generic;
using System.Linq;
using RUL;
using System.Drawing;
namespace TestApp
{
    public static class PerlinNoiseGenerator
    {
        private const int GRID_POINT_FREQUENCY = 10;

        public static float[,] PerlinNoise(int width, int height)
        {
            float[,] pixels = new float[width,height];
            //Populate grid points
            Vec2[,] gridPoints = new Vec2[width / GRID_POINT_FREQUENCY, height / GRID_POINT_FREQUENCY];
            for (int x = 0; x < gridPoints.GetLength(0); x++)
                for (int y = 0; y < gridPoints.GetLength(1); y++)
                    gridPoints[x, y] = Rul.RandUnitVec2();

            //Iterate through all pixels
            for (int x = 0; x < width - 1; x++)
            {
                for(int y = 0; y < height -1;y++)
                {
                    Point[] corners = GetClosestCorners(x, y);//Index of corners
                    Vec2[] cornerVecs = new Vec2[4];//Actual position of corners
                    for (int i = 0; i < 4; i++)
                        cornerVecs[i] = new Vec2(corners[i].X * GRID_POINT_FREQUENCY, corners[i].Y * GRID_POINT_FREQUENCY);
                    Vec2[] distances = new Vec2[4];
                    for (int i = 0; i < 4; i++)
                        distances[i] = new Vec2(x, y) - cornerVecs[i];
                    float[] dots = new float[4];
                    for (int i = 0; i < 4; i++)
                        dots[i] = cornerVecs[i].Dot(distances[i]);// !!!Looking for gridPoints(gradients) not position of points
                }
            }

            return new float[5, 5];
        }

        private static Point[] GetClosestCorners(int x, int y)
        {
            //Order : ul,ur,ll,lr
            Point[] corners = new Point[4];
            corners[0] = new Point(x / GRID_POINT_FREQUENCY, y / GRID_POINT_FREQUENCY);
            corners[1] = new Point(x / GRID_POINT_FREQUENCY + 1, y / GRID_POINT_FREQUENCY);
            corners[2] = new Point(x / GRID_POINT_FREQUENCY, y / GRID_POINT_FREQUENCY + 1);
            corners[3] = new Point(x / GRID_POINT_FREQUENCY + 1, y / GRID_POINT_FREQUENCY + 1);
            return corners;
        }

    }

}
