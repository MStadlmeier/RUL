using System;
using System.Collections.Generic;
using System.Text;

namespace RUL
{
    public static class Rul
    {
        #region Public

        #region  RNG settings / initialization

        public static void Initialize(int seed)
        {
            RNG.Initialize(seed);
        }

        public static int Seed { get { return RNG.Seed; } }

        #endregion

        #region Random Numbers
        /// <summary>
        /// Returns a random float between 0 and 1
        /// </summary>
        public static float RandFloat()
        {
            return (float)RNG.NextNumber() / (float)RNG.MAX_VALUE;
        }

        /// <summary>
        /// Returns a random float between min and max, both included
        /// </summary>
        /// <param name="min">The lower bound for the random value</param>
        /// <param name="max">The upper bound for the random value</param>
        public static float RandFloat(float min, float max)
        {
            if (min < max)
                return (min + (max - min) * RandFloat());
            else if (min == max)
                return min;
            else
                return RandFloat(max, min);
        }

        /// <summary>
        /// Returns a random double between 0 and 1
        /// </summary>
        public static double RandDouble()
        {
            return (double)RNG.NextNumber() / (double)RNG.MAX_VALUE;
        }

        /// <summary>
        /// Returns a random double between min and max, both included
        /// </summary>
        /// <param name="min">The lower bound for the random value</param>
        /// <param name="max">The upper bound for the random value</param>
        public static double RandDouble(double min, double max)
        {
            if (min < max)
                return (min + (max - min) * RandDouble());
            else if (min == max)
                return min;
            else
                return RandDouble(max, min);
        }


        /// <summary>
        /// Returns a random integer between 0 and max. Max is excluded by default
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        /// <param name="option">Determines which bounds are included</param>
        /// <returns></returns>
        public static int RandInt(int max, InclusionOptions option = InclusionOptions.Lower)
        {
            return RandInt(0, max, InclusionOptions.Lower);
        }

        public static int RandInt(int min, int max, InclusionOptions option = InclusionOptions.Both)
        {
            if (min < max)
            {
                switch (option)
                {
                    case InclusionOptions.Both:
                        return RandInRange(min, max);
                    case InclusionOptions.Lower:
                        return RandInRange(min, max - 1);
                    case InclusionOptions.Upper:
                        return RandInRange(min + 1, max);
                    case InclusionOptions.None:
                        return RandInRange(min + 1, max - 1);
                    default:
                        throw new ArgumentException("Invalid InclusionOption");
                }
            }
            else if (min == max)
                return min;
            else
                return RandInt(max, min, option);
        }

        /// <summary>
        /// Returns true or false
        /// </summary>
        public static bool RandBool()
        {
            return RandFloat() < 0.5F;
        }

        /// <summary>
        /// Returns 1 or -1
        /// </summary>
        public static int RandSign()
        {
            if (RandBool())
                return 1;
            return -1;
        }

        #endregion

        #region Random Vectors

        /// <summary>
        /// Returns a random 2-dimensional vector within the specified range
        /// </summary>
        /// <param name="lowerBoundX">Lower bound for the x-component</param>
        /// <param name="upperBoundX">Upper bound for the x-component</param>
        /// <param name="lowerBoundY">Lower bound for the y-component</param>
        /// <param name="upperBoundY">Upper bound for the y-component</param>
        public static Vec2 RandVec2(float lowerBoundX, float upperBoundX, float lowerBoundY, float upperBoundY)
        {
            return new Vec2(RandFloat(lowerBoundX, upperBoundX), RandFloat(lowerBoundY, upperBoundY));
        }

        /// <summary>
        /// Returns a random 2-dimensional vector within the specified range
        /// </summary>
        /// <param name="lowerBound">The lower bound as a vector</param>
        /// <param name="upperBound">The upper bound as a vector</param>
        public static Vec2 RandVec2(Vec2 lowerBound, Vec2 upperBound)
        {
            return RandVec2(lowerBound.X, upperBound.X, lowerBound.Y, upperBound.Y);
        }

        /// <summary>
        /// Returns a 2-dimensional vector whose components can vary from the base vector up to a specified amount.
        /// </summary>
        /// <param name="baseVector">The vector that is used as a base for the new one</param>
        /// <param name="maxXVariance">The highest possible difference between the vectors' x-coordinates</param>
        /// <param name="maxYVariance">The highest possible difference between the vectors' y-coordinates</param>
        /// <returns></returns>
        public static Vec2 RandVec2(Vec2 baseVector, float maxXVariance, float maxYVariance)
        {
            return baseVector + new Vec2(RandFloat(-maxXVariance, maxXVariance), RandFloat(-maxYVariance, maxYVariance));
        }

        /// <summary>
        /// Returns a random 2-dimensional vector with the length 1
        /// </summary>
        public static Vec2 RandUnitVec2()
        {
            float rad = RandFloat(0, (float)Math.PI * 2);
            return new Vec2((float)Math.Cos(rad), (float)Math.Sin(rad));
        }

        #endregion

        #region Noise Generation

        /// <summary>
        /// Returns a random, one-dimensional array of floats between 0 and 1
        /// </summary>
        public static float[] RandNoise1(int length)
        {
            ValidateSizeParameters(new object[] { length });

            float[] noise = new float[length];
            for (int i = 0; i < length; i++)
                noise[i] = RandFloat();
            return noise;
        }

        /// <summary>
        /// Returns a random, two-dimensional array of floats between 0 and 1
        /// </summary>
        public static float[,] RandNoise2(int width, int height)
        {
            ValidateSizeParameters(new object[] { width, height });

            float[,] noise = new float[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    noise[x, y] = RandFloat();
            return noise;
        }

        /// <summary>
        /// Returns a random, three-dimensional array of floats between 0 and 1
        /// </summary>
        public static float[, ,] RandNoise3(int width, int height, int depth)
        {
            ValidateSizeParameters(new object[] { width, height, depth });

            float[, ,] noise = new float[width, height, depth];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    for (int z = 0; z < depth; z++)
                        noise[x, y, z] = RandFloat();
            return noise;
        }

        //#TODO_Noise
        public static float[,] RandPerlinNoise2(int width, int height, float zoom)
        {
            ValidateSizeParameters(new object[] { width, height });
            var myval = new int[] { 1, 2, 3 };

            NoiseGenerator texGen = new NoiseGenerator(width, height);
            return null;
        }

        #endregion

        #endregion

        #region Private

        private static int RandInRange(int lower, int upper)
        {
            if (lower < upper)
            {
                int diff = upper - lower;
                int r = lower + (int)((diff + 1) * RandFloat());
                return MathHelper.Clamp(r, lower, upper);
            }
            else if (lower == upper)
                return lower;
            else
                return RandInRange(upper, lower);

        }

        private static void ValidateSizeParameters(object[] parameters)
        {
            foreach (object p in parameters)
            {
                if ((int)p < 0)
                    throw new ArgumentException("Size cannot be less than zero");
            }
        }

        #endregion
    }
}
