using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    internal static class MathHelper
    {
        /// <summary>
        /// Clamps the value between lower and upper bound
        /// </summary>
        internal static int Clamp(int value, int lowerBound, int upperBound)
        {
            if (lowerBound < upperBound)
            {
                if (value < lowerBound)
                    return lowerBound;
                else if (value > upperBound)
                    return upperBound;
                else
                    return value;
            }
            else if (lowerBound == upperBound)
                return lowerBound;
            else
                throw new ArgumentException("Lower bound must be less than upper bound");
        }
    }
}
