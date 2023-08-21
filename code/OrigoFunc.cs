using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yawNoise
{
    /// <summary>
    /// A function which retruns the given center
    /// </summary>
    class PointFunc : IVecFunc2D
    {
        public vec2 Center { get; }

        public PointFunc(vec2 center)
        {
            Center = center;
        }

        public vec2 GetVec2(float x, float y)
        {
            return Center;
        }
        public vec2 this[float x, float y] => GetVec2(x, y);
    }


    /// <summary>
    /// A function which retruns the given center
    /// </summary>
    class PointFunc3D : IVecFunc3D
    {
        public vec3 Center { get; }

        public PointFunc3D(vec3 center)
        {
            Center = center;
        }

        public vec3 GetVec3(float x, float y, float z)
        {
            return Center;
        }
        public vec3 this[float x, float y, float z] => GetVec3(x, y, z);
    }

    /// <summary>
    /// A function which retruns the given center
    /// </summary>
    class PointFunc4D : IVecFunc4D
    {
        public vec4 Center { get; }

        public PointFunc4D(vec4 center)
        {
            Center = center;
        }

        public vec4 GetVec4(float x, float y, float z, float w)
        {
            return Center;
        }
        public vec4 this[float x, float y, float z, float w] => GetVec4(x, y, z, w);
    }
}
