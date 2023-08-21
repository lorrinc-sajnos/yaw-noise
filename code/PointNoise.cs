namespace yawNoise
{
    /// <summary>
    /// A noise which has controlabbly spread out points in the plane
    /// </summary>
    public class PointNoise2D : INoise, IVecFunc2D, IAsByte
    {
        #region Fields
        public int Seed { get; private set; }

        public vec2 CellSize { get; private set; }

        public vec2 BufferSize { get; private set; }


        vec2 bufferMax;

        WhiteNoiseInt noiseX;
        WhiteNoiseInt noiseY;
        #endregion

        #region Constructors

        public PointNoise2D(vec2 cellSize, vec2 bufferSize, int seed)
        {
            Seed = seed;
            noiseX = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
            noiseY = new WhiteNoiseInt(new RawNoise(Seed)[Seed + 69]);

            //Cell settings
            CellSize = cellSize;

            //cell buffer setup
            BufferSize = bufferSize;

            bufferMax = CellSize - 2 * BufferSize;
        }

        public PointNoise2D(float cellWidth, float cellHeight, float bufferWidth, float bufferHeight, int seed)
        {
            Seed = seed;
            noiseX = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
            noiseY = new WhiteNoiseInt(new RawNoise(Seed)[Seed + 69]);

            //Cell settings
            CellSize = new vec2(cellWidth, cellHeight);

            //cell buffer setup
            BufferSize = new vec2(bufferWidth, bufferHeight);

            bufferMax = CellSize - 2 * BufferSize;

        }

        #endregion

        #region Functions

        vec2i GetCellIndex(float x, float y)
        {
            return new vec2i((int)(x / CellSize.x), (int)(y / CellSize.y));
        }
        public vec2 GetVec2(float x, float y)
        {
            //Cell index calc
            int cIndX = (int)(x / CellSize.x);
            int cIndY = (int)(y / CellSize.y);

            vec2 rtn = new vec2(noiseX[cIndX, cIndY], noiseY[cIndX, cIndY]);

            rtn.x = rtn.x * bufferMax.x + BufferSize.x + cIndX * CellSize.x;
            rtn.y = rtn.y * bufferMax.y + BufferSize.y + cIndY * CellSize.y;

            return rtn;
        }

        public vec2[] GetAllVec2(float x, float y)
        {
            vec2[] points = new vec2[9];

            int cIndX = (int)(x / CellSize.x);
            cIndX -= x < 0 ? 1 : 0;
            int cIndY = (int)(y / CellSize.y);
            cIndY -= y < 0 ? 1 : 0;

            int c = 0;
            for (int i = cIndX - 1; i <= cIndX + 1; i++)
                for (int j = cIndY - 1; j <= cIndY + 1; j++)
                {
                    points[c] = new vec2(noiseX[i, j], noiseY[i, j]);

                    points[c].x = points[c].x * bufferMax.x + BufferSize.x + i * CellSize.x;
                    points[c].y = points[c].y * bufferMax.y + BufferSize.y + j * CellSize.y;
                    c++;
                }

            return points;
        }

        public byte GetByte(int x, int y)
        {
            vec2i a = (vec2i)this[x, y];
            if (a == (x, y))
            {
                return 255;
            }
            else
            {
                int cIndX = (int)(x / CellSize.x);
                int cIndY = (int)(y / CellSize.y);

                if (cIndX % 2 == 0 ^ cIndY % 2 == 0)
                {
                    return 127;
                }
                return 0;

            }

        }

        #endregion

        #region Indexers
        public vec2 this[float x, float y] => GetVec2(x, y);

        #endregion

    }
}
