namespace yawNoise
{
    public class RawNoise : IAsByte
    {
        #region Fields
        public int Seed { get; private set; }

        private const int xPrime = 353594023;
        private const int yPrime = 1465077127;
        private const int zPrime = 2082801727;
        private const int wPrime = 580550587;
        private static readonly int[] Primes = { xPrime, yPrime, zPrime, wPrime };

        /// <summary>
        /// A WhiteNoise with the seed 0
        /// </summary>
        public static readonly RawNoise Static = new RawNoise(0);
        #endregion

        #region Constructors
        //public RawNoise()
        //{
        //   Seed = new Random().Next();
        //}
        public RawNoise(int seed)
        {
            Seed = seed;
        }
        public RawNoise(RawNoise noise)
        {
            Seed = noise.Seed;
        }


        #endregion

        #region Functions
        public int GetInt(int x)
        {
            int num = Seed;
            num ^= xPrime * x;

            return num * num * num;
        }
        public int GetInt(int x, int y)
        {
            int num = Seed;
            num ^= xPrime * x;
            num ^= yPrime * y;

            return num * num * num;
        }
        public int GetInt(int x, int y, int z)
        {
            int num = Seed;
            num ^= xPrime * x;
            num ^= yPrime * y;
            num ^= zPrime * z;

            return num * num * num;
        }
        public int GetInt(int x, int y, int z, int w)
        {
            int num = Seed;
            num ^= xPrime * x;
            num ^= yPrime * y;
            num ^= zPrime * z;
            num ^= wPrime * w;

            return num * num * num;
        }
        public int GetInt(int[] numbers)
        {
            int num = Seed;

            for (int i = 1; i < numbers.Length; i++)
                num ^= numbers[i] * Primes[i % 4];

            return num * num * num;
        }
        #endregion

        #region Indexers
        public int this[int x] { get => GetInt(x); }
        public int this[int x, int y] { get => GetInt(x, y); }
        public int this[int x, int y, int z] { get => GetInt(x, y, z); }
        public int this[int x, int y, int z, int w] { get => GetInt(x, y, z, w); }
        public int this[int[] numbers] { get => GetInt(numbers); }
        #endregion

        const float byteVar = 255f / uint.MaxValue;
        public byte GetByte(int x, int y)
        {
            return (byte)(GetInt(x, y) * byteVar + byteVar / 2);
        }
    }
}
