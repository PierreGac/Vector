/*
using System;

[Serializable]
public struct Vector2Int : IEquatable<Vector2Int>, IEquatable<int>
{
    public const int __NEGATIVE_FLAG = 0x200;
    public const int __ALLOWED_BITS = 0x3FF; // Size of an axis inside the hash value
    public const int __NEGATIVE_OFFSET = __ALLOWED_BITS - __NEGATIVE_FLAG;
    public int x;
    public int y;

    public Vector2Int normalized
    {
        get
        {
            return Normalize(this);
        }
    }

    // ABS : (t ^ (t >> 31)) - (t >> 31)

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2Int(Vector3Int vector3)
    {
        x = vector3.x;
        y = vector3.y;
    }

    public int ComputeHash()
    {
        int p1 = x << 10;
        if (x < 0)
        {
            p1 = ((__NEGATIVE_OFFSET - ((x ^ (x >> 31)) - (x >> 31))) | __NEGATIVE_FLAG) << 10;
        }
        int p2 = y;
        if (p2 < 0)
        {
            p2 = ((__NEGATIVE_OFFSET - ((y ^ (y >> 31)) - (y >> 31))) | __NEGATIVE_FLAG);
        }
        return p1 | p2;
    }

    /// <summary>
    /// Converts a hash into a Vector2Int
    /// </summary>
    /// <param name="hash"></param>
    /// <returns></returns>
    public static Vector2Int HashToVector2(int hash)
    {
        int p1 = hash >> 10;
        if ((p1 & __NEGATIVE_FLAG) == __NEGATIVE_FLAG)
        {
            p1 = -(__ALLOWED_BITS - p1);
        }
        int p2 = hash & ~(__ALLOWED_BITS << 10);
        if ((p2 & __NEGATIVE_FLAG) == __NEGATIVE_FLAG)
        {
            p2 = -(__ALLOWED_BITS - p2);
        }

        return new Vector2Int(p1, p2);
    }


    /// <summary>
    /// Returns the distance between 2 Vector2Int
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Distance(Vector2Int a, Vector2Int b)
    {
        return Math.Max(Math.Abs(a.x - b.x), Math.Abs(a.y - b.y));
    }

    /// <summary>
    /// Returns the distance between 2 Vector2Int
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public int Distance(Vector2Int b)
    {
        return Math.Max(Math.Abs(x - b.x), Math.Abs(y - b.y));
    }

    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }


    public override int GetHashCode()
    {
        return ComputeHash();
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        return ComputeHash() == ((Vector2Int)obj).ComputeHash();
    }

    public bool Equals(Vector2Int other)
    {
        return ComputeHash() == other.ComputeHash();
    }

    public bool Equals(int other)
    {
        return ComputeHash() == other;
    }

    public static Vector2Int Normalize(Vector2Int from, Vector2Int to)
    {
        return (to - from) / from.Distance(to);
        
    }

    public static Vector2Int Normalize(Vector2Int direction)
    {
        return direction / direction.Distance(new Vector2Int(0,0));
    }

    public int Min()
    {
        return Math.Min(x, y);
    }

    public static int Min(Vector2Int v)
    {
        return Math.Min(v.x, v.y);
    }

    public int Max()
    {
        return Math.Max(x, y);
    }

    public static int Max(Vector2Int v)
    {
        return Math.Max(v.x, v.y);
    }

    public static bool operator ==(Vector2Int v1, Vector2Int v2)
    {
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector2Int v1, Vector2Int v2)
    {
        return !v1.Equals(v2);
    }

    public static bool operator ==(Vector2Int v1, int hash)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(Vector2Int v1, int hash)
    {
        return !v1.Equals(hash);
    }

    public static bool operator ==(int hash, Vector2Int v1)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(int hash, Vector2Int v1)
    {
        return !v1.Equals(hash);
    }

    public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.x + v2.x, v1.y + v2.y);
    }

    public static Vector2Int operator -(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.x - v2.x, v1.y - v2.y);
    }

    public static Vector2Int operator -(Vector2Int v1)
    {
        return new Vector2Int(-v1.x, -v1.y);
    }

    public static Vector2Int operator *(int i, Vector2Int v1)
    {
        return new Vector2Int(i * v1.x, i * v1.y);
    }

    public static Vector2Int operator *(Vector2Int v1, int i)
    {
        return new Vector2Int(i * v1.x, i * v1.y);
    }

    /// <summary>
    /// REMINDER : THIS IS AN INTEGER DIVISION (no rounding, just a basic division)
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static Vector2Int operator /(Vector2Int v1, int i)
    {
        return new Vector2Int(v1.x / i, v1.y / i);
    }
}
*/