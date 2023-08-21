using System;

namespace yawNoise
{
    public struct vec2i
    {
        #region Fields
        public int x { get; set; }
        public int y { get; set; }
        #endregion

        #region Constructors
        public vec2i(int X, int Y)
        {
            x = X;
            y = Y;
        }
        #endregion

        #region Indexers
        const string indexErrorMsg = "The index \"{0}\" must be 0 or 1";
        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException(string.Format(indexErrorMsg, i));
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        return;
                    case 1:
                        y = value;
                        return;
                    default: throw new IndexOutOfRangeException(string.Format(indexErrorMsg, i));
                }
            }
        }
        #endregion

        #region Convertions
        #region -   FromTuplets
        public static implicit operator vec2i((int, int) var)
        {
            return new vec2i(var.Item1, var.Item2);
        }
        #endregion

        #region -   FromFloat

        //From vec2; explicit, because there is lost data
        public static explicit operator vec2i(vec2 vec)
        {
            return new vec2i((int)vec.x, (int)vec.y);
        }
        //From vec3; explicit, because there is lost data
        public static explicit operator vec2i(vec3 vec)
        {
            return new vec2i((int)vec.x, (int)vec.y);
        }

        //From vec4; explicit, because there is lost data
        public static explicit operator vec2i(vec4 vec)
        {
            return new vec2i((int)vec.x, (int)vec.y);
        }
        #endregion

        #region -   FromInt

        //From vec3i; explicit, because there is lost data
        public static explicit operator vec2i(vec3i vec)
        {
            return new vec2i(vec.x, vec.y);
        }

        //From vec4i; explicit, because there is lost data
        public static explicit operator vec2i(vec4i vec)
        {
            return new vec2i(vec.x, vec.y);
        }
        #endregion

        #endregion

        #region Operators
        //Equals
        public static bool operator ==(vec2i left, vec2i right)
        {
            return left.x == right.x && left.y == right.y;
        }
        public static bool operator !=(vec2i left, vec2i right)
        {
            return !(left == right);
        }

        //Addition
        public static vec2i operator +(vec2i left, vec2i right)
        {
            return new vec2i(left.x + right.x, left.y + right.y);
        }

        //Negation
        public static vec2i operator -(vec2i left, vec2i right)
        {
            return new vec2i(left.x - right.x, left.y - right.y);
        }

        //Scalar Multiplication
        public static vec2i operator *(vec2i left, int right)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }
        public static vec2i operator *(int left, vec2i right)
        {
            return right * left;
        }

        //Scalar Division
        public static vec2i operator /(vec2i left, int right)
        {
            left.x /= right;
            left.y /= right;
            return left;
        }
        public static vec2i operator /(int left, vec2i right)
        {
            return right / left;
        }

        //Vector Multiplication
        public static vec2i operator *(vec2i left, vec2i right)
        {
            left.x *= right.x;
            left.y *= right.y;
            return left;
        }
        //Vector Division
        public static vec2i operator /(vec2i left, vec2i right)
        {
            left.x /= right.x;
            left.y /= right.y;
            return left;
        }
        #endregion
    }
    public struct vec3i
    {
        #region Fields
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        #endregion

        #region Constructors
        public vec3i(int X, int Y, int Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
        #endregion

        #region Indexers
        const string indexErrorMsg = "The index \"{0}\" must be between 0 and 2";
        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new IndexOutOfRangeException(string.Format(indexErrorMsg, i));
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        return;
                    case 1:
                        y = value;
                        return;
                    case 2:
                        z = value;
                        return;
                    default: throw new IndexOutOfRangeException(string.Format(indexErrorMsg, i));
                }
            }
        }
        #endregion

        #region Convertions
        #region -   FromTuplets
        public static implicit operator vec3i((int, int, int) var)
        {
            return new vec3i(var.Item1, var.Item2, var.Item3);
        }
        #endregion

        #region -   FromFloat

        //From vec2; explicit, because there is lost data
        public static explicit operator vec3i(vec2 vec)
        {
            return new vec3i((int)vec.x, (int)vec.y, 0);
        }
        //From vec3; explicit, because there is lost data
        public static explicit operator vec3i(vec3 vec)
        {
            return new vec3i((int)vec.x, (int)vec.y, (int)vec.z);
        }

        //From vec4; explicit, because there is lost data
        public static explicit operator vec3i(vec4 vec)
        {
            return new vec3i((int)vec.x, (int)vec.y, (int)vec.z);
        }
        #endregion

        #region -   FromInt

        //From vec2i; implicit, because there is no data loss
        public static implicit operator vec3i(vec2i vec)
        {
            return new vec3i(vec.x, vec.y, 0);
        }

        //From vec4i; explicit, because there is lost data
        public static explicit operator vec3i(vec4i vec)
        {
            return new vec3i(vec.x, vec.y, vec.z);
        }
        #endregion
        #endregion

        #region Operators
        //Equals
        public static bool operator ==(vec3i left, vec3i right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z;
        }
        public static bool operator !=(vec3i left, vec3i right)
        {
            return !(left == right);
        }

        //Addition
        public static vec3i operator +(vec3i left, vec3i right)
        {
            return new vec3i(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        //Negation
        public static vec3i operator -(vec3i left, vec3i right)
        {
            return new vec3i(left.x - right.x, left.y - right.y, left.z - right.z);
        }

        //Scalar Multiplication
        public static vec3i operator *(vec3i left, int right)
        {
            left.x *= right;
            left.y *= right;
            left.z *= right;
            return left;
        }
        public static vec3i operator *(int left, vec3i right)
        {
            return right * left;
        }

        //Scalar Division
        public static vec3i operator /(vec3i left, int right)
        {
            left.x /= right;
            left.y /= right;
            left.z /= right;
            return left;
        }
        public static vec3i operator /(int left, vec3i right)
        {
            return right / left;
        }

        //Vector Multiplication
        public static vec3i operator *(vec3i left, vec3i right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;
            return left;
        }
        //Vector Division
        public static vec3i operator /(vec3i left, vec3i right)
        {
            left.x /= right.x;
            left.y /= right.y;
            left.z /= right.z;
            return left;
        }
        #endregion
    }
    public struct vec4i
    {
        #region Fields
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int w { get; set; }
        #endregion

        #region Constructors
        public vec4i(int X, int Y, int Z, int W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }
        #endregion

        #region Indexers
        const string indexErrorMsg = "The index \"{0}\" must be between 0 and 3";
        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new IndexOutOfRangeException(string.Format(indexErrorMsg, i));
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        return;
                    case 1:
                        y = value;
                        return;
                    case 2:
                        z = value;
                        return;
                    case 3:
                        w = value;
                        return;
                    default: throw new IndexOutOfRangeException(string.Format(indexErrorMsg, i));
                }
            }
        }
        #endregion

        #region Convertions
        #region -   FromTuplets
        public static implicit operator vec4i((int, int, int, int) var)
        {
            return new vec4i(var.Item1, var.Item2, var.Item3, var.Item4);
        }
        #endregion

        #region -   FromFloat

        //From vec2; explicit, because there is lost data
        public static explicit operator vec4i(vec2 vec)
        {
            return new vec4i((int)vec.x, (int)vec.y, 0, 0);
        }
        //From vec3; explicit, because there is lost data
        public static explicit operator vec4i(vec3 vec)
        {
            return new vec4i((int)vec.x, (int)vec.y, (int)vec.z, 0);
        }

        //From vec4; explicit, because there is lost data
        public static explicit operator vec4i(vec4 vec)
        {
            return new vec4i((int)vec.x, (int)vec.y, (int)vec.z, (int)vec.w);
        }
        #endregion

        #region -   FromInt

        //From vec2i; implicit, because there is no data loss
        public static implicit operator vec4i(vec2i vec)
        {
            return new vec4i(vec.x, vec.y, 0, 0);
        }

        //From vec4i; explicit, because there is lost data
        public static implicit operator vec4i(vec3i vec)
        {
            return new vec4i(vec.x, vec.y, vec.z, 0);
        }
        #endregion

        #endregion

        #region Operators
        //Equals
        public static bool operator ==(vec4i left, vec4i right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z && left.w == right.w;
        }
        public static bool operator !=(vec4i left, vec4i right)
        {
            return !(left == right);
        }

        //Addition
        public static vec4i operator +(vec4i left, vec4i right)
        {
            return new vec4i(left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.w);
        }

        //Negation
        public static vec4i operator -(vec4i left, vec4i right)
        {
            return new vec4i(left.x - right.x, left.y - right.y, left.z - right.z, left.w - right.w);
        }

        //Scalar Multiplication
        public static vec4i operator *(vec4i left, int right)
        {
            left.x *= right;
            left.y *= right;
            left.z *= right;
            left.w *= right;
            return left;
        }
        public static vec4i operator *(int left, vec4i right)
        {
            return right * left;
        }

        //Scalar Division
        public static vec4i operator /(vec4i left, int right)
        {
            left.x /= right;
            left.y /= right;
            left.z /= right;
            left.w /= right;
            return left;
        }
        public static vec4i operator /(int left, vec4i right)
        {
            return right / left;
        }
        //Vector Multiplication
        public static vec4i operator *(vec4i left, vec4i right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;
            left.w *= right.w;
            return left;
        }
        //Vector Division
        public static vec4i operator /(vec4i left, vec4i right)
        {
            left.x /= right.x;
            left.y /= right.y;
            left.z /= right.z;
            left.w /= right.w;
            return left;
        }
        #endregion
    }
}
