using System;

/// <summary>
/// A short Vector3 representation. This class will be slower than the Vector3Int one, but it will take lass RAM (I guess...)
/// </summary>
[Serializable]
public struct Vector3Short : IEquatable<Vector3Short>, IEquatable<long>
{
    public static Vector3Short zero { get; } = new Vector3Short(0, 0, 0);
    public static Vector3Short one { get; } = new Vector3Short(1, 1, 1);
    public static Vector3Short up { get; } = new Vector3Short(0, 1, 0);
    public static Vector3Short down { get; } = new Vector3Short(0, -1, 0);
    public static Vector3Short left { get; } = new Vector3Short(-1, 0, 0);
    public static Vector3Short right { get; } = new Vector3Short(1, 0, 0);
    public static Vector3Short forward { get; } = new Vector3Short(0, 0, 1);
    public static Vector3Short back { get; } = new Vector3Short(0, 0, -1);

    public short x;
    public short y;
    public short z;

    public Vector3Short cubicNormalized
    {
        get
        {
            return NormalizeCubic(this);
        }
    }

    // 31 = sizeof(int) - 1
    // ABS : (t ^ (t >> 31)) - (t >> 31)

    public Vector3Short(short x, short y, short z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Short(short x, short y)
    {
        this.x = x;
        this.y = y;
        z = 0;
    }

    public Vector3Short(long hash64)
    {
        hash64 &= 0x0000FFFFFFFFFFFF;
        x = (short)(hash64 >> 32);
        y = (short)(hash64 >> 16 & 0xFFFFFFFF);
        z = (short)(hash64 & 0xFFFF);
    }

    /// <summary>
    /// Stores the Vector inside a long (48bits stored inside 64bits => Loseless)
    /// </summary>
    /// <returns></returns>
    public long ComputeHash64()
    {
        return ((long)x << 32) | ((long)y << 16) | (long)z;
    }

    /// <summary>
    /// Converts a 64 bits hash into a Vector3Int
    /// </summary>
    /// <param name="hash64"></param>
    /// <returns></returns>
    public static Vector3Short Hash64ToVector3(long hash64)
    {
        hash64 &= 0x0000FFFFFFFFFFFF;
        short p1 = (short)(hash64 >> 32);
        short p2 = (short)(hash64 >> 16 & 0xFFFFFFFF);
        short p3 = (short)(hash64 & 0xFFFF);
        return new Vector3Short(p1, p2, p3);
    }

    /// <summary>
    /// Returns the distance between 2 hexes in cubic coordinates
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Distance(Vector3Short a, Vector3Short b)
    {
        int m1 = Math.Max(Math.Abs(a.x - b.x), Math.Abs(a.y - b.y));
        return Math.Max(m1, Math.Abs(a.z - b.z));
    }

    /// <summary>
    /// Returns the distance between 2 hexes in cubic coordinates
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public int Distance(Vector3Short b)
    {
        int m1 = Math.Max(Math.Abs(x - b.x), Math.Abs(y - b.y));
        return Math.Max(m1, Math.Abs(z - b.z));
    }

    public override string ToString()
    {
        return string.Format("({0},{1},{2})", x, y, z);
    }

    /// <summary>
    /// As the ComputeHash64 is a long, the GetHashCode method will have to keep the default behaviour.
    /// Another solution was to compute an int hash, but we will loose some data, as each coordinate is on 10 bits instead od 16bits
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        Vector3Short other = (Vector3Short)obj;
        return other.x == x && other.y == y && other.z == z;
    }

    public bool Equals(Vector3Short other)
    {
        return other.x == x && other.y == y && other.z == z;
    }

    public bool Equals64(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        return ComputeHash64() == ((Vector3Short)obj).ComputeHash64();
    }

    public bool Equals64(Vector3Short other)
    {
        return ComputeHash64() == other.ComputeHash64();
    }

    public bool Equals(long other)
    {
        return ComputeHash64() == other;
    }

    public static Vector3Short NormalizeCubic(Vector3Short from, Vector3Short to)
    {
        Vector3Short d = to - from;
        int max = d.Max();
        int min = d.Min();
        Vector3Short normalized = new Vector3Short(0, 0, 0);
        if (max == d.x)
        {
            normalized.x = (short)(d.x / max);
        }
        else if (max == d.y)
        {
            normalized.y = (short)(d.y / max);
        }
        else
        {
            normalized.z = (short)(d.z / max);
        }

        if (min == d.x)
        {
            normalized.x = (short)(-d.x / min);
        }
        else if (min == d.y)
        {
            normalized.y = (short)(-d.y / min);
        }
        else
        {
            normalized.z = (short)(-d.z / min);
        }
        return normalized;
    }

    public static Vector3Short NormalizeCubic(Vector3Short direction)
    {
        short max = direction.Max();
        short min = direction.Min();
        Vector3Short normalized = new Vector3Short(0, 0, 0);
        if (max == direction.x)
        {
            normalized.x = (short)(direction.x / max);
        }
        else if (max == direction.y)
        {
            normalized.y = (short)(direction.y / max);
        }
        else
        {
            normalized.z = (short)(direction.z / max);
        }

        if (min == direction.x)
        {
            normalized.x = (short)(-direction.x / min);
        }
        else if (min == direction.y)
        {
            normalized.y = (short)(-direction.y / min);
        }
        else
        {
            normalized.z = (short)(-direction.z / min);
        }
        return normalized;
    }

    public short Min()
    {
        short m1 = Math.Min(x, y);
        return Math.Min(m1, z);
    }

    public static short Min(Vector3Short v)
    {
        short m1 = Math.Min(v.x, v.y);
        return Math.Min(m1, v.z);
    }

    public short Max()
    {
        short m1 = Math.Max(x, y);
        return Math.Max(m1, z);
    }

    public static short Max(Vector3Short v)
    {
        short m1 = Math.Max(v.x, v.y);
        return Math.Max(m1, v.z);
    }

    #region Operators
    public static bool operator ==(Vector3Short v1, Vector3Short v2)
    {
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector3Short v1, Vector3Short v2)
    {
        return !v1.Equals(v2);
    }

    public static bool operator ==(Vector3Short v1, long hash)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(Vector3Short v1, long hash)
    {
        return !v1.Equals(hash);
    }

    public static bool operator ==(long hash, Vector3Short v1)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(long hash, Vector3Short v1)
    {
        return !v1.Equals(hash);
    }

    public static Vector3Short operator +(Vector3Short v1, Vector3Short v2)
    {
        return new Vector3Short((short)(v1.x + v2.x), (short)(v1.y + v2.y), (short)(v1.z + v2.z));
    }

    public static Vector3Short operator -(Vector3Short v1, Vector3Short v2)
    {
        return new Vector3Short((short)(v1.x - v2.x), (short)(v1.y - v2.y), (short)(v1.z - v2.z));
    }

    public static Vector3Short operator -(Vector3Short v1)
    {
        return new Vector3Short((short)(-v1.x), (short)(-v1.y), (short)(-v1.z));
    }

    public static Vector3Short operator *(short i, Vector3Short v1)
    {
        return new Vector3Short((short)(i * v1.x), (short)(i * v1.y), (short)(i * v1.z));
    }

    public static Vector3Short operator *(Vector3Short v1, short i)
    {
        return new Vector3Short((short)(i * v1.x), (short)(i * v1.y), (short)(i * v1.z));
    }

    public static Vector3Short operator *(int i, Vector3Short v1)
    {
        return new Vector3Short((short)(i * v1.x), (short)(i * v1.y), (short)(i * v1.z));
    }

    public static Vector3Short operator *(Vector3Short v1, int i)
    {
        return new Vector3Short((short)(i * v1.x), (short)(i * v1.y), (short)(i * v1.z));
    }

    /// <summary>
    /// REMINDER : THIS IS AN INTEGER DIVISION (no rounding, just a basic division)
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static Vector3Short operator /(Vector3Short v1, short i)
    {
        return new Vector3Short((short)(v1.x / i), (short)(v1.y / i), (short)(v1.z / i));
    }

    /// <summary>
    /// REMINDER : THIS IS AN INTEGER DIVISION (no rounding, just a basic division)
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="i"></param>
    /// <returns></returns>MO
    public static Vector3Short operator /(Vector3Short v1, int i)
    {
        return new Vector3Short((short)(v1.x / i), (short)(v1.y / i), (short)(v1.z / i));
    }

    public static implicit operator Vector2Short(Vector3Short v)
    {
        return new Vector2Short(v.x, v.y);
    }
    #endregion
}