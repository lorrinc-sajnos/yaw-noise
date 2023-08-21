
using System;

namespace yawNoise
{
    public class SimplexNoise : INoise, IHaveFreq, IFloatFunc2D
    {
        #region Fields
        public int Seed { get; private set; }
        public float Frequency { get; private set; }

        public float this[float x, float y, float z] => throw new System.NotImplementedException();

        public float this[float x, float y] => GetFloat(x, y);

        private const int xPrime = 894303281;
        private const int yPrime = 1306672973;
        private const int zPrime = 2022289333;
        private const int wPrime = 615701591;

        #endregion

        #region Constructors
        public SimplexNoise(int seed, float freq)
        {
            Seed = seed;
            Frequency = freq;
            //innerNoise = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
        }
        #endregion

        const float sin15 = 0.258819f;
        const float cos15 = 0.9659258f;
        const float tan15 = 0.2679492f;

        #region Fucntions
        public float GetFloat(float x, float y)
        {
            float x_tri = x / Frequency;
            float y_tri = y / Frequency;

            //Koordináták visszalakítása
            float x_sqr = x_tri + tan15 * y_tri;
            float y_sqr = y_tri + tan15 * x_tri;

            //Gyors egészrész
            int x_int = x_sqr > 0 ? (int)x_sqr : (int)x_sqr - 1;
            int y_int = y_sqr > 0 ? (int)y_sqr : (int)y_sqr - 1;

            //Kellék változók kiszámolása

            int x_hash = Seed ^ (x_int * xPrime);
            int y_hash = y_int * xPrime;
            int x1_hash = Seed ^ ((x_int + 1) * xPrime);
            int y1_hash = (y_int + 1) * xPrime;

            float xi_tan15 = x_int * tan15;
            float yi_tan15 = y_int * tan15;
            //Háromsz rácspontjainak visszalakítása
            //A kettő szemközti az biztos
            float val = 0;
            float currWigl;

            //A pont (0,0)
            float ax = x_tri - (x_int + yi_tan15);
            float ay = y_tri - (y_int + xi_tan15);

            currWigl = Wiggle(ax, ay);
            if (currWigl >= 0)
            {
                currWigl *= currWigl;
                val += currWigl * currWigl * DotProd(ax, ay, x_hash, y_hash);
            }
            //Eldöntjük, hogy melyik a B pont
            float bx, by;

            if (x_sqr - x_int > y_sqr - y_int)
            {
                //B pont (0,1)
                //x_int, y_int+1
                bx = x_tri - (x_int + yi_tan15 + tan15);
                by = y_tri - (y_int + 1 + xi_tan15);

                currWigl = Wiggle(bx, by);
                if (currWigl >= 0)
                {
                    currWigl *= currWigl;
                    val += currWigl * currWigl * DotProd(bx, by, x_hash, y1_hash);
                }
            }
            else
            {
                //B pont (1,0)
                //x_int +1, y_int
                bx = x_tri - (x_int + 1 + yi_tan15);
                by = y_tri - (y_int + xi_tan15 + tan15);

                currWigl = Wiggle(bx, by);
                if (currWigl >= 0)
                {
                    currWigl *= currWigl;
                    val += currWigl * currWigl * DotProd(bx, by, x1_hash, y_hash);
                }
            }

            //C pont (1,1)
            float cx = x_tri - (x_int + 1 + yi_tan15 + tan15);//yi_tan15 + tan15 == (y_int + 1) * tan15
            float cy = y_tri - (y_int + 1 + xi_tan15 + tan15);//xi_tan15 + tan15 == (x_int + 1) * tan15

            currWigl = Wiggle(bx, by);
            if (currWigl <= 0)
            {
                currWigl *= currWigl;
                val += currWigl * DotProd(cx, cy, x1_hash, y1_hash);
            }

            return 70 * val;//
        }

        float Wiggle(float x, float y)
        {
            return 0.5f - x * x - y * x;
        }

        float DotProd(float x, float y, int hashX, int hashY)
        {
            //Eredeti kód:
            //
            //int hash = Seed;
            //hash ^= xPrime * indX;
            //hash ^= yPrime * indY;
            //
            //hash = (hash * hash * hash) >> 16;

            //Gyorsított kód:
            //mivel a hashX = Seed^(indX*xPrime),
            //és a hashY = indY*yPrime;

            int hash = hashX ^ hashY;

            hash *= hash * hash;
            hash >>= 20;

            switch (hash & 7)
            {
                default:
                case 0:
                    return y;
                case 1:
                    return x + y;
                case 2:
                    return x;
                case 3:
                    return x - y;
                case 4:
                    return -y;
                case 5:
                    return -x - y;
                case 6:
                    return -x;
                case 7:
                    return y - x;
            }
        }

        #endregion

    }
}
