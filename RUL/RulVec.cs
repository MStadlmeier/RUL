using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL.Vector
{
    public static class RulVec
    {
        #region Public Methods

        /// <summary>
        /// Returns a random 2-dimensional vector within the specified range
        /// </summary>
        /// <param name="lowerBoundX">Lower bound for the x-component</param>
        /// <param name="upperBoundX">Upper bound for the x-component</param>
        /// <param name="lowerBoundY">Lower bound for the y-component</param>
        /// <param name="upperBoundY">Upper bound for the y-component</param>
        public static Vec2 RandVec2(float lowerBoundX, float upperBoundX, float lowerBoundY, float upperBoundY)
        {
            return new Vec2(Rul.RandFloat(lowerBoundX, upperBoundX), Rul.RandFloat(lowerBoundY, upperBoundY));
        }

        /// <summary>
        /// Returns a random 2-dimensional vector representing a between the given end points
        /// </summary>
        public static Vec2 RandVec2(Vec2 pointA, Vec2 pointB)
        {
            if (pointA == pointB)
                return pointA;
            Vec2 difference = pointB - pointA;
            return pointA + difference * Rul.RandFloat();

        }

        /// <summary>
        /// Returns a 2-dimensional vector whose components can vary from the base vector by a limited amount.
        /// </summary>
        /// <param name="baseVector">The vector that is used as a base for the new one</param>
        /// <param name="maxXVariance">The highest possible difference between the vectors' x-coordinates</param>
        /// <param name="maxYVariance">The highest possible difference between the vectors' y-coordinates</param>
        /// <returns></returns>
        public static Vec2 RandVec2(Vec2 baseVector, float maxXVariance, float maxYVariance)
        {
            return baseVector + new Vec2(Rul.RandFloat(-maxXVariance, maxXVariance), Rul.RandFloat(-maxYVariance, maxYVariance));
        }


        /// <summary>
        /// Returns a randomly rotated version of the given base vector
        /// </summary>
        /// <param name="baseVector">The vector that is used as a base for the new one</param>
        /// <param name="maxAngle">The greatest possible angle(in radians) between the base vector and the rotated random vector</param>
        public static Vec2 RandVec2(Vec2 baseVector, float maxAngle)
        {
            float angle = Rul.RandFloat(0, maxAngle % (float)(2F * Math.PI)) * Rul.RandSign();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            float newX = (float)(baseVector.X * cos - baseVector.Y * sin);
            float newY = (float)(baseVector.X * sin + baseVector.Y * cos);
            return new Vec2(newX, newY);
        }

        /// <summary>
        /// Returns a random 2-dimensional vector with the length 1
        /// </summary>
        public static Vec2 RandUnitVec2()
        {
            float rad = Rul.RandFloat(0, (float)Math.PI * 2);
            return new Vec2((float)Math.Cos(rad), (float)Math.Sin(rad));
        }

        #endregion
    }
}
