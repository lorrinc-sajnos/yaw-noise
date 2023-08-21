using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yawNoise
{
    /// <summary>
    /// For every point in the plane it returns a 2 dimensional vector
    /// </summary>
    public interface IVecFunc2D
    {
        vec2 GetVec2(float x, float y);
        vec2 this[float x, float y] { get; }
    }
    /// <summary>
    /// For every point in space it returns a 3 dimensional vector
    /// </summary>
    public interface IVecFunc3D
    {
        vec3 GetVec3(float x, float y, float z);
        vec3 this[float x, float y, float z] { get; }
    }

    /// <summary>
    /// For every point in 4 dimensional space it returns a 4 dimensional vector
    /// </summary>
    public interface IVecFunc4D
    {
        vec4 GetVec4(float x, float y, float z, float w);
        vec4 this[float x, float y, float z, float w] { get; }
    }
}
