using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable IDE1006

namespace yawNoise
{
    public struct vec2
    {
        #region Fields
        public float x { get; set; }
        public float y { get; set; }

        public static readonly vec2 Zero = new vec2(0f, 0f);

        public static readonly vec2 Up = new vec2(0f, 1f);
        public static readonly vec2 Down = new vec2(0f, -1f);
        public static readonly vec2 Left = new vec2(-1f, 0f);
        public static readonly vec2 Right = new vec2(1f, 0f);


        public static readonly vec2 I = new vec2(1f, 0f);
        public static readonly vec2 J = new vec2(0f, 1f);
        #endregion

        #region Constructors
        public vec2(float X, float Y)
        {
            x = X;
            y = Y;
        }
        #endregion

        #region Functions
        //Calculates a unit vector from an angle
        public static vec2 CalculateUnitlVec(float angle)
        {
            return new vec2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public static double GetLength(vec2 vector)
        {
            return Math.Sqrt(vector.x * vector.x + vector.y * vector.y);
        }
        public double GetLength()
        {
            return GetLength(this);
        }

        public static double GetDistance(vec2 A, vec2 B)
        {
            return (B - A).GetLength();
        }
        public double GetDistance(vec2 point)
        {
            return (point - this).GetLength();
        }

        public static double GetAngle(vec2 A, vec2 B)
        {
            return Math.Acos((A.x * B.x + A.y * B.y) / (A.GetLength() * B.GetLength()));
        }
        public double GetAngle(vec2 A)
        {
            return GetAngle(this, A);
        }

        public static double GetNormalVecAngle(vec2 A, vec2 B)
        {
            return Math.Acos(A.x * B.x + A.y * B.y);
        }
        public double GetNormalVecAngle(vec2 A)
        {
            return GetNormalVecAngle(this,A);
        }

        public static vec2 Normalize(vec2 vector)
        {
            return vector / (float)vector.GetLength();
        }
        public vec2 Normalize()
        {
            return Normalize(this);
        }
        #endregion

        #region Indexers
        const string indexErrorMsg = "The index \"{0}\" must be 0 or 1";
        public float this[int i]
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
        public static implicit operator vec2((float, float) var)
        {
            return new vec2(var.Item1, var.Item2);
        }
        #endregion

        #region -   FromFloat

        //From vec3; explicit, because there is lost data
        public static explicit operator vec2(vec3 vec)
        {
            return new vec2(vec.x, vec.y);
        }

        //From vec4; explicit, because there is lost data
        public static explicit operator vec2(vec4 vec)
        {
            return new vec2(vec.x, vec.y);
        }
        #endregion

        #region -   FromInt

        //From vec2i; implicit, because there is no data loss
        public static implicit operator vec2(vec2i vec)
        {
            return new vec2(vec.x, vec.y);
        }

        //From vec3i; explicit, because there is lost data
        public static explicit operator vec2(vec3i vec)
        {
            return new vec2(vec.x, vec.y);
        }

        //From vec4i; explicit, because there is lost data
        public static explicit operator vec2(vec4i vec)
        {
            return new vec2(vec.x, vec.y);
        }
        #endregion

        #endregion

        #region Operators
        //Equals
        public static bool operator ==(vec2 left, vec2 right)
        {
            return left.x == right.x && left.y == right.y;
        }
        public static bool operator !=(vec2 left, vec2 right)
        {
            return !(left == right);
        }

        //Addition
        public static vec2 operator +(vec2 left, vec2 right)
        {
            return new vec2(left.x + right.x, left.y + right.y);
        }

        //Negation
        public static vec2 operator -(vec2 left, vec2 right)
        {
            return new vec2(left.x - right.x, left.y - right.y);
        }

        //Scalar Multiplication
        public static vec2 operator *(vec2 left, float right)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }
        public static vec2 operator *(float left, vec2 right)
        {
            return right * left;
        }

        //Scalar Division
        public static vec2 operator /(vec2 left, float right)
        {
            left.x /= right;
            left.y /= right;
            return left;
        }
        public static vec2 operator /(float left, vec2 right)
        {
            return right / left;
        }

        //Vector Multiplication
        public static vec2 operator *(vec2 left, vec2 right)
        {
            left.x *= right.x;
            left.y *= right.y;
            return left;
        }
        //Vector Division
        public static vec2 operator /(vec2 left, vec2 right)
        {
            left.x /= right.x;
            left.y /= right.y;
            return left;
        }

        #endregion
    }
    public struct vec3
    {
        #region Fields
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public static readonly vec3 Zero = new vec3(0f, 0f, 0f);
        public static readonly vec3 Up = new vec3(0f, 1f, 0f);
        #endregion

        #region Constructors
        public vec3(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
        #endregion

        #region Functions
        public static float GetLength(vec3 vector)
        {
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        public float GetLength()
        {
            return GetLength(this);
        }

        public static vec3 Normalize(vec3 vector)
        {
            return vector / vector.GetLength();
        }
        public vec3 Normalize()
        {
            return Normalize(this);
        }
        #endregion

        #region Indexers
        const string indexErrorMsg = "The index \"{0}\" must be between 0 and 2";
        public float this[int i]
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
        public static implicit operator vec3((float, float, float) var)
        {
            return new vec3(var.Item1, var.Item2, var.Item3);
        }
        #endregion

        #region -   FromFloat

        //From vec3; implicit, because there is no data loss
        public static implicit operator vec3(vec2 vec)
        {
            return new vec3(vec.x, vec.y, 0f);
        }

        //From vec4; explicit, because there is lost data
        public static explicit operator vec3(vec4 vec)
        {
            return new vec3(vec.x, vec.y, vec.z);
        }
        #endregion

        #region -   FromInt

        //From vec2i; implicit, because there is no data loss
        public static implicit operator vec3(vec2i vec)
        {
            return new vec3(vec.x, vec.y, 0f);
        }

        //From vec3i; implicit, because there is no data loss
        public static implicit operator vec3(vec3i vec)
        {
            return new vec3(vec.x, vec.y, vec.z);
        }

        //From vec4i; explicit, because there is lost data
        public static explicit operator vec3(vec4i vec)
        {
            return new vec3(vec.x, vec.y, vec.z);
        }
        #endregion
        #endregion

        #region Operators
        //Equals
        public static bool operator ==(vec3 left, vec3 right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z;
        }
        public static bool operator !=(vec3 left, vec3 right)
        {
            return !(left == right);
        }

        //Addition
        public static vec3 operator +(vec3 left, vec3 right)
        {
            return new vec3(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        //Negation
        public static vec3 operator -(vec3 left, vec3 right)
        {
            return new vec3(left.x - right.x, left.y - right.y, left.z - right.z);
        }

        //Scalar Multiplication
        public static vec3 operator *(vec3 left, float right)
        {
            left.x *= right;
            left.y *= right;
            left.z *= right;
            return left;
        }
        public static vec3 operator *(float left, vec3 right)
        {
            return right * left;
        }

        //Scalar Division
        public static vec3 operator /(vec3 left, float right)
        {
            left.x /= right;
            left.y /= right;
            left.z /= right;
            return left;
        }
        public static vec3 operator /(float left, vec3 right)
        {
            return right / left;
        }

        //Vector Multiplication
        public static vec3 operator *(vec3 left, vec3 right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;
            return left;
        }
        //Vector Division
        public static vec3 operator /(vec3 left, vec3 right)
        {
            left.x /= right.x;
            left.y /= right.y;
            left.z /= right.z;
            return left;
        }
        #endregion
    }
    public struct vec4
    {
        #region Fields
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }

        public static readonly vec4 Zero = new vec4(0f, 0f, 0f, 0f);
        public static readonly vec4 Up = new vec4(0f, 1f, 0f, 0f);
        #endregion

        #region Constructors
        public vec4(float X, float Y, float Z, float W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }
        #endregion

        #region Functions
        public static float GetLength(vec4 vector)
        {
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z + vector.w * vector.w);
        }
        public float GetLength()
        {
            return GetLength(this);
        }

        public static vec4 Normalize(vec4 vector)
        {
            return vector / vector.GetLength();
        }
        public vec4 Normalize()
        {
            return Normalize(this);
        }
        #endregion

        #region Indexers
        const string indexErrorMsg = "The index \"{0}\" must be between 0 and 3";
        public float this[int i]
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
        public static implicit operator vec4((float, float, float, float) var)
        {
            return new vec4(var.Item1, var.Item2, var.Item3, var.Item4);
        }
        #endregion

        #region -   FromFloat

        //From vec2; implicit, because there is no data loss
        public static implicit operator vec4(vec2 vec)
        {
            return new vec4(vec.x, vec.y, 0f, 0f);
        }

        //From vec3; implicit, because there is no data loss
        public static implicit operator vec4(vec3 vec)
        {
            return new vec4(vec.x, vec.y, vec.z, 0f);
        }
        #endregion

        #region -   FromInt

        //From vec2i; implicit, because there is no data loss
        public static implicit operator vec4(vec2i vec)
        {
            return new vec4(vec.x, vec.y, 0f, 0f);
        }

        //From vec3i; implicit, because there is no data loss
        public static implicit operator vec4(vec3i vec)
        {
            return new vec4(vec.x, vec.y, vec.z, 0f);
        }

        //From vec4i; implicit, because there is no data loss
        public static implicit operator vec4(vec4i vec)
        {
            return new vec4(vec.x, vec.y, vec.z, vec.w);
        }
        #endregion
        #endregion

        #region Operators
        //Equals
        public static bool operator ==(vec4 left, vec4 right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z && left.w == right.w;
        }
        public static bool operator !=(vec4 left, vec4 right)
        {
            return !(left == right);
        }


        //Addition
        public static vec4 operator +(vec4 left, vec4 right)
        {
            return new vec4(left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.w);
        }

        //Negation
        public static vec4 operator -(vec4 left, vec4 right)
        {
            return new vec4(left.x - right.x, left.y - right.y, left.z - right.z, left.w - right.w);
        }

        //Scalar Multiplication
        public static vec4 operator *(vec4 left, float right)
        {
            left.x *= right;
            left.y *= right;
            left.z *= right;
            left.w *= right;
            return left;
        }
        public static vec4 operator *(float left, vec4 right)
        {
            return right * left;
        }

        //Scalar Division
        public static vec4 operator /(vec4 left, float right)
        {
            left.x /= right;
            left.y /= right;
            left.z /= right;
            left.w /= right;
            return left;
        }
        public static vec4 operator /(float left, vec4 right)
        {
            return right / left;
        }

        //Vector Multiplication
        public static vec4 operator *(vec4 left, vec4 right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;
            left.w *= right.w;
            return left;
        }
        //Vector Division
        public static vec4 operator /(vec4 left, vec4 right)
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