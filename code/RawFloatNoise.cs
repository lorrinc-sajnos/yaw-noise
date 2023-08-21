using System;

namespace yawNoise
{
    /// <summary>
    /// A noisefunction which returns a random float between 0 and 1.
    /// </summary>
    public class WhiteNoiseInt : IAsByte
    {
        #region Fields
        public int Seed { get; private set; }
        RawNoise innerNoise;

        const float floatPerInt = 1f/uint.MaxValue;

        #endregion

        #region Constructors

        public WhiteNoiseInt()
        {
            Seed = new Random().Next();
            innerNoise = new RawNoise(new RawNoise(Seed)[Seed]);
        }
        public WhiteNoiseInt(int seed)
        {
            Seed = seed;
            innerNoise = new RawNoise(new RawNoise(seed)[seed]);
        }
        #endregion

        #region Functions
        private float Floatify(int i)
        {
            return i * floatPerInt + 0.5f;
        }


        public float GetFloat(int x)
        {
            return Floatify(innerNoise.GetInt(x));
        }
        public float GetFloat(int x, int y)
        {
            return Floatify(innerNoise.GetInt(x, y));
        }
        public float GetFloat(int x, int y, int z)
        {
            return Floatify(innerNoise.GetInt(x, y, z));
        }
        public float GetFloat(int x, int y, int z, int w)
        {
            return Floatify(innerNoise.GetInt(x, y, z, w));
        }
        public float GetFloat(int[] numbers)
        {
            return Floatify(innerNoise.GetInt(numbers));
        }

        #endregion

        #region Indexers
        public float this[int x] { get => GetFloat(x); }
        public float this[int x, int y] { get => GetFloat(x, y); }
        public float this[int x, int y, int z] { get => GetFloat(x, y, z); }
        public float this[int x, int y, int z, int w] { get => GetFloat(x, y, z, w); }
        public float this[int[] numbers] { get => GetFloat(numbers); }
        #endregion

        public byte GetByte(int x, int y)
        {
            return (byte)(GetFloat(x, y)*255);
        }
    }
}
