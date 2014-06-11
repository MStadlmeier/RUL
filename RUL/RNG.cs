using System;
using System.Collections.Generic;
using System.Text;

namespace RUL
{
    /// <summary>
    /// Simple Linear congruential generator
    /// Utilizes Microsoft's LCG algorithm
    /// </summary>
    internal static class RNG
    {
        #region Constants

        public const int MAX_VALUE = 32767;

        #endregion

        #region Public Fields
        public static bool Initialized { get { return _initialized; } }
        public static int Seed { get { return _seed; } }

        #endregion

        #region Private Fields
        private static bool _initialized;
        private static int _seed;
        private static int _state;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the RNG with a random seed
        /// </summary>
        public static int Initialize()
        {
            Initialize(Environment.TickCount);
            return _seed;
        }

        public static void Initialize(int seed)
        {
            if (seed > 0)
            {
                _seed = seed;
                _state = seed;
                _initialized = true;
            }
            else
            {
                throw new ArgumentException("Seed must be greater than zero");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int NextNumber()
        {
            if (!_initialized)
                Initialize();
            _state = 214013 * _state + 2531011;
            return (_state & int.MaxValue) >> 16;
        }

        #endregion
    }
}
