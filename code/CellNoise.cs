using yawNoise;

namespace yawNoise
{
    public class CellNoise : IFloatFunc2D
    {
        #region Fields
        public int Seed { get; private set; }

        public float CellSize { get; private set; }
        public float CenterBuffer { get; private set; }

        public float this[float x, float y] => GetFloat(x, y);

        private const int x1Prime = 2093215799;
        private const int y1Prime = 182775937;

        private const int x2Prime = 564872351;
        private const int y2Prime = 996545789;

        const float float2int = 1f / uint.MaxValue;
        readonly float conv;
        readonly float offset;

        #endregion

        #region Constructors

        public CellNoise(int seed, float cellSize, float centerBuffer)
        {
            Seed = seed;

            //Cell settings
            CellSize = 1 / cellSize;
            //cell buffer setup
            CenterBuffer = centerBuffer;
            conv = float2int * (1 - 2 * CenterBuffer);
            offset = 0.5f + CenterBuffer;
        }


        #endregion

        #region Functions

        float SquareDist(float x, float y, int xIn, int yIn)
        {
            //A cellákhoz tartotzó relatív pontok kiszámítása (0-1 közötti szám lesz)

            //X
            int a = Seed;
            a ^= x1Prime * xIn;
            a ^= y1Prime * yIn;
            a *= a * a;

            //Y
            int b = Seed;
            b ^= x2Prime * xIn;
            b ^= y2Prime * yIn;
            b *= b * b;

            //Kivonjuk egymásból őket
            float p = xIn + a * conv + offset - x;//Kivonás első felében kiszámítjuk a koordinátát
            float q = yIn + b * conv + offset - y;

            //A távolság nyégyzete, mivel csak összehasonlítani kell őket
            return p * p + q * q;
        }
        float FastAbs(float x)
        {
            return x > 0 ? x : -x;
        }
        float Min(float x, float y)
        {
            return x < y ? x : y;
        }
        float Max(float x, float y)
        {
            return x > y ? x : y;
        }

        public vec2i GetVec2(float x, float y)
        {
            x *= CellSize;
            y *= CellSize;

            //Cell index calc
            int cIndX = x > 0 ? (int)x : (int)x - 1;
            int cIndY = y > 0 ? (int)y : (int)y - 1;

            int targetX = cIndX + 2;
            int targetY = cIndY + 2;

            float min = float.MaxValue;
            int rX = 0, rY = 0;

            for (int i = cIndX - 1; i < targetX; i++)
                for (int j = cIndY - 1; j < targetY; j++)
                {
                    float f = SquareDist(x, y, i, j);
                    if (min > f)
                    {
                        rX = i;
                        rY = j;
                        min = f;
                    }
                }


            return new vec2i { x = rX, y = rY };
        }

        public float GetFloat(float x, float y)
        {
            x *= CellSize;
            y *= CellSize;

            //Cell index calc
            int cIndX = x > 0 ? (int)x : (int)x - 1;
            int cIndY = y > 0 ? (int)y : (int)y - 1;

            int targetX = cIndX + 2;
            int targetY = cIndY + 2;

            float min1 = float.MaxValue;
            float min2 = float.MaxValue;

            for (int i = cIndX - 1; i < targetX; i++)
            {
                for (int j = cIndY - 1; j < targetY; j++)
                {
                    float f = SquareDist(x, y, i, j);
                    if (f < min2)
                    {
                        //*
                        bool cond = f < min1;

                        min2 = cond ? min1 : f;
                        min1 = cond ? f : min1;//*/
                        /*
                        if (f < min1)
                        {
                            min2 = min1;
                            min1 = f;
                        }
                        else
                        {
                            min2 = f;
                        }//*/
                    }
                }
            }

            //return 1 - min1 / min2 + min2 - min1;
            return min2 - min1;
        }
        #endregion

    }
}
