using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yawNoise
{
    public enum SearchType
    {
        Self,
        Neighbours
    }

    public class LengthFunc : IFloatFunc2D, IAsByte
    {
        public float Radius { get; private set; }
        public PointNoise2D InnerPointNoise { get; }
        public SearchType searchType { get; }

        public LengthFunc(float radius, PointNoise2D noise, SearchType searchTypeIn)
        {
            Radius = radius;
            InnerPointNoise = noise;
            searchType = searchTypeIn;
        }


        #region Indexers

        public float this[float x, float y] => GetFloat(x, y);
        #endregion

        #region Functions


        public float GetFloat(float x, float y)
        {
            switch (searchType)
            {
                case SearchType.Self:
                    {
                        double temp = (1 - new vec2(x, y).GetDistance(InnerPointNoise[x, y]) / Radius);
                        return temp > 0 ? (float)temp : 0;
                    }
                case SearchType.Neighbours:
                    {
                        double temp, rtn = 0;
                        vec2[] points = InnerPointNoise.GetAllVec2(x, y);
                        for (int i = 0; i < 9; i++)
                        {
                            temp = 1 - new vec2(x, y).GetDistance(points[i]) / Radius;
                            rtn = temp > rtn ? temp : rtn;
                            //rtn *= temp;
                        }
                        return (float)rtn;
                    }
                default:
                    throw new Exception("This shouldn't be hit lol");
            }
        }

        public byte GetByte(int x, int y)
        {
            return (byte)(255 * GetFloat(x, y));
        }
        #endregion
    }
}
