using System;

namespace yawNoise
{
    public class Funcs
    {
        public const float PI = (float)Math.PI;
        public const float TAU = PI*2;

        public static vec2i IndexFromFloat(vec2 pos, float freq)
        {
            vec2i rtn = (vec2i)(pos / freq);
            return new vec2i(rtn.x > 0 ? rtn.x : rtn.x + 1, rtn.y > 0 ? rtn.y : rtn.y + 1);
        }
    }

    public interface INoise
    {
        int Seed { get; }
    }
    public interface IHaveFreq
    {
        float Frequency { get; }
    }
    public interface IAsByte
    {
        byte GetByte(int x, int y);
    }
}
