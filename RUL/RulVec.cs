using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RUL
{
    public static class RulVec
    {
        #region Public Methods

        #region 2D

        /// <summary>
        /// Returns a random 2-dimensional vector within the specified range
        /// </summary>
        /// <param name="lowerBoundX">Lower bound for the x-component</param>
        /// <param name="upperBoundX">Upper bound for the x-component</param>
        /// <param name="lowerBoundY">Lower bound for the y-component</param>
        /// <param name="upperBoundY">Upper bound for the y-component</param>
        public static Vec2 RandVec2(float lowerBoundX, float lowerBoundY, float upperBoundX, float upperBoundY)
        {
            return new Vec2(Rul.RandFloat(lowerBoundX, upperBoundX), Rul.RandFloat(lowerBoundY, upperBoundY));
        }

        /// <summary>
        /// Returns a random 2-dimensional vector that lies between the given end points
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
            float angle = Rul.RandFloat(maxAngle % (float)(2F * Math.PI)) * Rul.RandSign();
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
            float rad = Rul.RandFloat((float)Math.PI * 2);
            return new Vec2((float)Math.Cos(rad), (float)Math.Sin(rad));
        }

        /// <summary>
        /// Returns a random 2-dimensional vector that lies in the circle with the specified radius
        /// </summary>
        /// <param name="circleRadius">The radius of the circle that contains the point represented by the random vector</param>
        public static Vec2 RandVecInCircle(float circleRadius)
        {
            return RandUnitVec2() * Rul.RandFloat(circleRadius);
        }

        /// <summary>
        /// Returns a random 2-dimensional vector that points up, down, left or right
        /// </summary>
        public static Vec2 RandDirection2()
        {
            return Rul.RandElement(new Vec2(1, 0), new Vec2(0, 1), new Vec2(-1, 0), new Vec2(0, -1));
        }

        /// <summary>
        /// Returns an array of vectors that are randomly distributed in the given rectangle and have atleast the specified distance from each other
        /// </summary>
        /// <param name="count">The amount of vectors you want to generate</param>
        /// <param name="minDistance">The minimum distance between each vector</param>
        /// <param name="x">The x coordinate of the top left corner of the rectangle</param>
        /// <param name="y">The y coordinate of the top left corner of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        /// #TODO : not guaranteed to work, use safer implementation
        public static Vec2[] RandDistributedInRect(int count,float minDistance, float x, float y, float width, float height)
        {
            if (count >= 1)
            {
                Vec2[] vectors = new Vec2[count];
                for (int i = 0; i < count; i++)
                {
                    bool validPosition = true;
                    do
                    {
                        Vec2 vector = RandVec2(x, y, x + width, y + width);
                        for (int j = 0; j < i; j++)
                            if (vector.Distance(vectors[j]) < minDistance)
                                validPosition = false;
                    } while (!validPosition);
                }
                return vectors;
            }
            else
                throw new ArgumentException("Count cannot be negative");
        }

        /// <summary>
        /// Returns an array of vectors that are randomly distributed in the given circle with a minimum distance between each other
        /// </summary>
        /// <param name="count">THe amount of vectors you want to generate</param>
        /// <param name="minDistance">The minimum distance between each vector</param>
        /// <param name="x">The x coordinate of the center of the circle</param>
        /// <param name="y">The y coordinate of the center of the circle</param>
        /// <param name="circleRadius">The radius of the circle</param>
        /// #TODO : not guaranteed to work, use safer implementation
        public static Vec2[] RandDistributedInCircle(int count, float minDistance, float x, float y, float circleRadius)
        {
            if (circleRadius >= minDistance && count >= 0)
            {
                Vec2 center = new Vec2(x, y);
                Vec2[] vectors = new Vec2[count];
                for (int i = 0; i < count; i++)
                {
                    Vec2 vec;
                    if (i == 0)
                        vec = RandVecInCircle(circleRadius) + center;
                    else
                    {
                        Vec2 origin = vectors[Rul.RandInt(i - 1)];
                        //Distance from origin vector to center of circle
                        float originDistance = origin.Distance(center);
                        //Don't use points close to the edge as origin
                        if (originDistance / circleRadius > 0.75F)
                        {
                            origin = center;
                            originDistance = 0;
                        }
                        bool validPosition = true;
                        do
                        {
                            vec = RandVecInCircle(circleRadius - originDistance) + origin;
                            for (int j = 0; j < i; j++)
                                if(vec.Distance(vectors[j]) >= minDistance)
                                    validPosition = false;
                        } while (!validPosition);
                    }
                    vectors[i] = vec;
                }
                return vectors;
            }
            else if (count < 0)
                throw new ArgumentException("Count cannot be negative");
            else
                throw new ArgumentException("Circle radius must be greater than minimum distance");
        }

        #endregion

        #region 3D

        /// <summary>
        /// Returns a random 3-dimensional vector within the specified range
        /// </summary>
        /// <param name="lowerBoundX">Lower bound for the x-component</param>
        /// <param name="upperBoundX">Upper bound for the x-component</param>
        /// <param name="lowerBoundY">Lower bound for the y-component</param>
        /// <param name="upperBoundY">Upper bound for the y-component</param>
        /// <param name="lowerBoundZ">Lower bound for the z-component</param>
        /// <param name="upperBoundZ">Upper bound for the z-component</param>
        public static Vec3 RandVec3(float lowerBoundX, float lowerBoundY, float lowerBoundZ, float upperBoundX, float upperBoundY, float upperBoundZ)
        {
            return new Vec3(Rul.RandFloat(lowerBoundX, upperBoundX), Rul.RandFloat(lowerBoundY, upperBoundY), Rul.RandFloat(lowerBoundZ, upperBoundZ));
        }

        /// <summary>
        /// Returns a random 3-dimensional vector that lies between the given end points
        /// </summary>
        public static Vec3 RandVec3(Vec3 pointA, Vec3 pointB)
        {
            if (pointA == pointB)
                return pointA;
            Vec3 difference = pointB - pointA;
            return pointA + difference * Rul.RandFloat();

        }

        /// <summary>
        /// Returns a 3-dimensional vector whose components can vary from the base vector by a limited amount.
        /// </summary>
        /// <param name="baseVector">The vector that is used as a base for the new one</param>
        /// <param name="maxXVariance">The highest possible difference between the vectors' x-coordinates</param>
        /// <param name="maxYVariance">The highest possible difference between the vectors' y-coordinates</param>
        /// <param name="maxZVariance">The highest possible difference between the vectors' z-coordinates</param>
        /// <returns></returns>
        public static Vec3 RandVec3(Vec3 baseVector, float maxXVariance, float maxYVariance, float maxZVariance)
        {
            return baseVector + new Vec3(Rul.RandFloat(-maxXVariance, maxXVariance), Rul.RandFloat(-maxYVariance, maxYVariance), Rul.RandFloat(-maxZVariance, maxZVariance));
        }

        /// <summary>
        /// Returns a random 3-dimensional vector with the length 1
        /// </summary>
        public static Vec3 RandUnitVec3()
        {
            float theta = Rul.RandFloat((float)Math.PI * 2);
            float z = Rul.RandFloat(-1, 1);
            float c = (float)(Math.Sqrt(1-z * z));
            float x = (float)(c * Math.Cos(theta));
            float y = (float)(c * Math.Sin(theta));
            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Returns a random 3-dimensional vector that lies in the sphere with the specified radius
        /// </summary>
        /// <param name="sphereRadius">The radius of the sphere that contains the point represented by the random vector</param>
        public static Vec3 RandVecInSphere(float sphereRadius)
        {
            return RandUnitVec3() * sphereRadius;
        }

        /// <summary>
        /// Returns a random 3-dimensional vector that points left, right, up, down, forwards or backwards
        /// </summary>
        public static Vec3 RandDirection3()
        {
            return Rul.RandElement(new Vec3(-1, 0, 0), new Vec3(1, 0, 0), new Vec3(0, -1, 0), new Vec3(0, 1, 0), new Vec3(0, 0, -1), new Vec3(0, 0, 1));
        }

        #endregion

        #endregion
    }
}