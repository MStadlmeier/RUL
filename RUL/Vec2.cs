using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    public struct Vec2
    {
        public static Vec2 Zero { get { return new Vec2(); } }

        #region Public Fields

        public float X, Y;

        #endregion

        #region Contructors

        public Vec2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vec2(float c)
        {
            this.X = c;
            this.Y = c;
        }

        #endregion

        #region Operators

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2 operator -(Vec2 value)
        {
            return new Vec2(-value.X, -value.Y);
        }

        public static bool operator ==(Vec2 a, Vec2 b)
        {
            return (a.X == b.X && a.Y == b.Y);
        }

        public static bool operator !=(Vec2 a, Vec2 b)
        {
            return !(a == b);
        }

        public static Vec2 operator *(Vec2 a, float scalar)
        {
            return new Vec2(a.X * scalar, a.Y * scalar);
        }

        public static Vec2 operator /(Vec2 a, float divider)
        {
            return new Vec2(a.X / divider, a.Y / divider);
        }


        #endregion

        #region Public Methods

        public float Dot(Vec2 other)
        {
            return (this.X * other.X + this.Y * other.Y);
        }

        public override string ToString()
        {
            return String.Format("X: {0} Y: {1}", X, Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec2)
            {
                Vec2 vec = (Vec2)obj;
                return (this.X == vec.X && this.Y == vec.Y);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        #endregion
    }
}
