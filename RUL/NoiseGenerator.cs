using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    internal class NoiseGenerator
    {
        #region Private

        private int _width, _height;
        private float[,] _baseNoise;

        #endregion


        #region Constructors

        public NoiseGenerator(int width, int height)
        {
            this._width = width;
            this._height = height;
            _baseNoise = Rul.RandNoise2(width, height);
        }

        #endregion
    }
}
