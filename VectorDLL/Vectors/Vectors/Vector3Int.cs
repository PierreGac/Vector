/*
using System;

[Serializable]
public struct Vector3Int : IEquatable<Vector3Int>, IEquatable<long>
{
    public const long __NEGATIVE_FLAG = 0x100000;
    public const long __ALLOWED_BITS = 0x1FFFFF;
    public const long __NEGATIVE_OFFSET = __ALLOWED_BITS - __NEGATIVE_FLAG;

    public int x;
    public int y;
    public int z;

    public Vector3Int cubicNormalized
    {
        get
        {
            return NormalizeCubic(this);
        }
    }

    // ABS : (t ^ (t >> 31)) - (t >> 31)

    #region Ctors
    public Vector3Int(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Vector3Int(long x, long y, long z)
    {
        this.x = (int)x;
        this.y = (int)y;
        this.z = (int)z;
    }

    public Vector3Int(int x, int y)
    {
        this.x = x;
        this.y = y;
        z = 0;
    }

    public Vector3Int(long x, long y)
    {
        this.x = (int)x;
        this.y = (int)y;
        this.z = 0;
    }

    public Vector3Int(Vector2Int vector2)
    {
        x = vector2.x;
        y = vector2.y;
        z = 0;
    }

    public Vector3Int(Vector3Short vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }
    #endregion

    public long ComputeHash()
    {
        long p1 = (long)x << 42;
        if (x < 0)
        {
            p1 = ((__NEGATIVE_OFFSET - ((x ^ (x >> 31)) - (x >> 31))) | __NEGATIVE_FLAG) << 42;
        }
        long p2 = (long)y << 21;
        if (y < 0)
        {
            p2 = ((__NEGATIVE_OFFSET - ((y ^ (y >> 31)) - (y >> 31))) | __NEGATIVE_FLAG) << 21;
        }
        long p3 = (long)z;
        if (p3 < 0)
        {
            p3 = ((__NEGATIVE_OFFSET - ((z ^ (z >> 31)) - (z >> 31))) | __NEGATIVE_FLAG);
        }
        return p1 | p2 | p3;
    }

    /// <summary>
    /// Converts a hash into a Vector3Int
    /// </summary>
    /// <param name="hash"></param>
    /// <returns></returns>
    public static Vector3Int HashToVector3(int hash)
    {
        long p1 = hash >> 42;
        if ((p1 & __NEGATIVE_FLAG) == __NEGATIVE_FLAG)
        {
            p1 = -(__ALLOWED_BITS - p1);
        }
        long p2 = hash >> 21 & ~(__ALLOWED_BITS << 21);
        if ((p2 & __NEGATIVE_FLAG) == __NEGATIVE_FLAG)
        {
            p2 = -(__ALLOWED_BITS - p2);
        }
        long p3 = hash & ~(__ALLOWED_BITS << 42 | __ALLOWED_BITS << 21);
        if ((p3 & __NEGATIVE_FLAG) == __NEGATIVE_FLAG)
        {
            p3 = -(__ALLOWED_BITS - p3);
        }
        return new Vector3Int(p1, p2, p3);
    }

    /// <summary>
    /// Returns the distance between 2 hexes in cubic coordinates
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Distance(Vector3Int a, Vector3Int b)
    {
        int m1 = Math.Max(Math.Abs(a.x - b.x), Math.Abs(a.y - b.y));
        return Math.Max(m1, Math.Abs(a.z - b.z));
    }

    /// <summary>
    /// Returns the distance between 2 hexes in cubic coordinates
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public int Distance(Vector3Int b)
    {
        int m1 = Math.Max(Math.Abs(x - b.x), Math.Abs(y - b.y));
        return Math.Max(m1, Math.Abs(z - b.z));
    }


    public static Vector3Int NormalizeCubic(Vector3Int from, Vector3Int to)
    {
        Vector3Int d = to - from;
        int max = d.Max();
        int min = d.Min();
        Vector3Int normalized = new Vector3Int(0, 0, 0);
        if (max == d.x)
        {
            normalized.x = d.x / max;
        }
        else if(max == d.y)
        {
            normalized.y = d.y / max;
        }
        else
        {
            normalized.z = d.z / max;
        }

        if (min == d.x)
        {
            normalized.x = -d.x / min;
        }
        else if (min == d.y)
        {
            normalized.y = -d.y / min;
        }
        else
        {
            normalized.z = -d.z / min;
        }
        return normalized;
    }

    public static Vector3Int NormalizeCubic(Vector3Int direction)
    {
        int max = direction.Max();
        int min = direction.Min();
        Vector3Int normalized = new Vector3Int(0, 0, 0);
        if (max == direction.x)
        {
            normalized.x = direction.x / max;
        }
        else if (max == direction.y)
        {
            normalized.y = direction.y / max;
        }
        else
        {
            normalized.z = direction.z / max;
        }

        if (min == direction.x)
        {
            normalized.x = -direction.x / min;
        }
        else if (min == direction.y)
        {
            normalized.y = -direction.y / min;
        }
        else
        {
            normalized.z = -direction.z / min;
        }
        return normalized;
    }

    public int Min()
    {
        int m1 = Math.Min(x, y);
        return Math.Min(m1, z);
    }

    public static int Min(Vector3Int v)
    {
        int m1 = Math.Min(v.x, v.y);
        return Math.Min(m1, v.z);
    }

    public int Max()
    {
        int m1 = Math.Max(x, y);
        return Math.Max(m1, z);
    }

    public static int Max(Vector3Int v)
    {
        int m1 = Math.Max(v.x, v.y);
        return Math.Max(m1, v.z);
    }


    public override string ToString()
    {
        return string.Format("({0},{1},{2})", x, y, z);
    }

    [Obsolete("Please use the ComputeHash method. GetHashCode is int not long !")]
    public override int GetHashCode()
    {
        return (int)ComputeHash(); // OUCH !
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        return ComputeHash() == ((Vector3Int)obj).ComputeHash();
    }

    public bool Equals(Vector3Int other)
    {
        return other.x == x && other.y == y && other.z == z;
    }

    public bool Equals(long other)
    {
        return ComputeHash() == other;
    }

    public static bool operator ==(Vector3Int v1, Vector3Int v2)
    {
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector3Int v1, Vector3Int v2)
    {
        return !v1.Equals(v2);
    }

    public static bool operator ==(Vector3Int v1, long hash)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(Vector3Int v1, long hash)
    {
        return !v1.Equals(hash);
    }

    public static bool operator ==(long hash, Vector3Int v1)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(long hash, Vector3Int v1)
    {
        return !v1.Equals(hash);
    }

    public static Vector3Int operator +(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector3Int operator -(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static Vector3Int operator -(Vector3Int v1)
    {
        return new Vector3Int(-v1.x, -v1.y, -v1.z);
    }

    public static Vector3Int operator *(int i, Vector3Int v1)
    {
        return new Vector3Int(i * v1.x, i * v1.y, i * v1.z);
    }

    public static Vector3Int operator *(Vector3Int v1, int i)
    {
        return new Vector3Int(i * v1.x, i * v1.y, i * v1.z);
    }

    /// <summary>
    /// REMINDER : THIS IS AN INTEGER DIVISION (no rounding, just a basic division)
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static Vector3Int operator /(Vector3Int v1, int i)
    {
        return new Vector3Int(v1.x / i, v1.y / i, v1.z / i);
    }

    public static explicit operator Vector3Int(Vector3Short v)
    {
        return new Vector3Int(v.x, v.y, v.z);
    }
}
*/