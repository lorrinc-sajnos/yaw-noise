using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yawNoise
{

    public interface IFloatFunc1D
    {
        float GetFloat(float x);
        float this[float x] { get; }
    }
    public interface IFloatFunc2D
    {
        float GetFloat(float x, float y);
        float this[float x, float y] { get; }
    }
    public interface IFloatFunc3D
    {
        float GetFloat(float x, float y, float z);
        float this[float x, float y, float z] { get; }
    }
    public interface IFloatFunc4D
    {
        float GetFloat(float x, float y, float z, float w);
        float this[float x, float y, float z, float w] { get; }
    }
}
