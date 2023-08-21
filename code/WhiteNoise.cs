using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yawNoise
{
    public class WhiteNoise : INoise, IHaveFreq, IFloatFunc1D, IFloatFunc2D, IFloatFunc3D, IFloatFunc4D, IAsByte
    {
        #region Fields
        public int Seed { get; private set; }
        public float Frequency { get; private set; }


        WhiteNoiseInt innerNoise;
        #endregion

        #region Constructors

        public WhiteNoise()
        {
            Seed = new Random().Next();
            Frequency = 1;
            innerNoise = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
        }
        public WhiteNoise(int seed)
        {
            Seed = seed;
            Frequency = 1;
            innerNoise = new WhiteNoiseInt(new RawNoise(seed)[seed]);
        }
        public WhiteNoise(int seed, float frequency)
        {
            Seed = seed;
            Frequency = frequency;
            innerNoise = new WhiteNoiseInt(new RawNoise(seed)[seed]);
        }

        #endregion

        #region Functions
        static int Floor(float a, float b)
        {
            int x = (int)(a / b);
            return a >= 0 == b > 0 ? x : x - 1;
        }

        static float Interpolate2Dnew(float bL, float bR, float tL, float tR, float x, float y)
        {
            return Interpolate1D(Interpolate1D(bL, bR, x), Interpolate1D(tL, tR, x), y);
        }

        static float EaseF(float t)
        {
            //return t <= 0.5f ? 2 * t * t : 4 * t - 2 * t * t - 1;
            return t * t * (3 - 2 * t);
        }
        static float Interpolate1D(float x, float y, float t)
        {
            //if (t < 0 || t > 1) throw new Exception("t (" + t + ") is not a valid value");
            
            return x + (y - x) * EaseF(t);
        }
        static float Interpolate2D(float bL, float bR, float tL, float tR, float x, float y)
        {
            float baseX = EaseF(x);
            float baseY = EaseF(y);
            float interpB = bL + (bR - bL) * baseX;
            float interpT = tL + (tR - tL) * baseX;

            return interpB + EaseF(y) * (interpT - interpB);
        }

        public float GetFloat(float x)
        {
            int floor = Floor(x, Frequency);
            //Console.WriteLine(floor + "  " + x);
            return Interpolate1D(innerNoise[floor], innerNoise[floor + 1], (x - floor * Frequency) / Frequency);
        }

        public float GetFloat(float x, float y)
        {
            int xF = Floor(x, Frequency);
            int yF = Floor(y, Frequency);
            return Interpolate2D(
                innerNoise[xF, yF], innerNoise[xF + 1, yF],
                innerNoise[xF, yF + 1], innerNoise[xF + 1, yF + 1],
                (x - xF * Frequency) / Frequency,
                (y - yF * Frequency) / Frequency
                );
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public float GetFloat(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public float GetFloat(float x, float y, float z, float w)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Indexers
        public float this[float x] { get => GetFloat(x); }
        public float this[float x, float y] { get => GetFloat(x, y); }
        public float this[float x, float y, float z] { get => GetFloat(x, y, z); }
        public float this[float x, float y, float z, float w] { get => GetFloat(x, y, z, w); }
        #endregion

        public byte GetByte(int x, int y)
        {
            return (byte)(GetFloat(x, y) * 255);
        }
    }
}
