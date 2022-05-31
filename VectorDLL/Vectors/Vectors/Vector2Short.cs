using System;

[Serializable]
public struct Vector2Short : IEquatable<Vector2Short>, IEquatable<int>
{
    public static Vector2Short zero { get; } = new Vector2Short(0, 0);
    public static Vector2Short one { get; } = new Vector2Short(1, 1);
    public static Vector2Short up { get; } = new Vector2Short(0, 1);
    public static Vector2Short down { get; } = new Vector2Short(0, -1);
    public static Vector2Short left { get; } = new Vector2Short(-1, 0);
    public static Vector2Short right { get; } = new Vector2Short(1, 0);

    public short x;
    public short y;

    public short this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return x;
                case 1:
                    return y;
                default:
                    throw new IndexOutOfRangeException(string.Format("Invalid Vector2Short index {0}", index));
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                default:
                    throw new IndexOutOfRangeException(string.Format("Invalid Vector2Short index {0}", index));
            }
        }
    }

    public Vector2Short normalized
    {
        get
        {
            return Normalize(this);
        }
    }

    // ABS : (t ^ (t >> 31)) - (t >> 31)

    public Vector2Short(short x, short y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2Short(int x, int y)
    {
        this.x = (short)x;
        this.y = (short)y;
    }

    public Vector2Short(int x, short y)
    {
        this.x = (short)x;
        this.y = y;
    }

    public Vector2Short(short x, int y)
    {
        this.x = x;
        this.y = (short)y;
    }

    public Vector2Short(Vector3Short vector3)
    {
        x = vector3.x;
        y = vector3.y;
    }

    public int ComputeHash()
    {
        return x << 16 | (int)y;
    }

    /// <summary>
    /// Converts a hash into a Vector2Int
    /// </summary>
    /// <param name="hash"></param>
    /// <returns></returns>
    public static Vector2Short HashToVector2(int hash)
    {
        return new Vector2Short((short)(hash >> 16), (short)(hash & 0xFFFF));
    }


    /// <summary>
    /// Returns the distance between 2 Vector2Int
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Distance(Vector2Short a, Vector2Short b)
    {
        return Math.Max(Math.Abs(a.x - b.x), Math.Abs(a.y - b.y));
    }

    /// <summary>
    /// Returns the distance between 2 Vector2Int
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public int Distance(Vector2Short b)
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
        return ComputeHash() == ((Vector2Short)obj).ComputeHash();
    }

    public bool Equals(Vector2Short other)
    {
        return ComputeHash() == other.ComputeHash();
    }

    public bool Equals(int other)
    {
        return ComputeHash() == other;
    }

    public static Vector2Short Normalize(Vector2Short from, Vector2Short to)
    {
        return (to - from) / from.Distance(to);

    }

    public static Vector2Short Normalize(Vector2Short direction)
    {
        return direction / direction.Distance(new Vector2Short(0, 0));
    }

    public int Min()
    {
        return Math.Min(x, y);
    }

    public static int Min(Vector2Short v)
    {
        return Math.Min(v.x, v.y);
    }

    public int Max()
    {
        return Math.Max(x, y);
    }

    public static int Max(Vector2Short v)
    {
        return Math.Max(v.x, v.y);
    }

    public static bool operator ==(Vector2Short v1, Vector2Short v2)
    {
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector2Short v1, Vector2Short v2)
    {
        return !v1.Equals(v2);
    }

    public static bool operator ==(Vector2Short v1, int hash)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(Vector2Short v1, int hash)
    {
        return !v1.Equals(hash);
    }

    public static bool operator ==(int hash, Vector2Short v1)
    {
        return v1.Equals(hash);
    }

    public static bool operator !=(int hash, Vector2Short v1)
    {
        return !v1.Equals(hash);
    }

    public static Vector2Short operator +(Vector2Short v1, Vector2Short v2)
    {
        return new Vector2Short((short)(v1.x + v2.x), (short)(v1.y + v2.y));
    }

    public static Vector2Short operator +(int i, Vector2Short v1)
    {
        return new Vector2Short((short)(v1.x + i), (short)(v1.y + i));
    }

    public static Vector2Short operator +(Vector2Short v1, int i)
    {
        return i + v1;
    }

    public static Vector2Short operator -(Vector2Short v1, Vector2Short v2)
    {
        return new Vector2Short((short)(v1.x - v2.x), (short)(v1.y - v2.y));
    }

    public static Vector2Short operator -(Vector2Short v1)
    {
        return new Vector2Short((short)(-v1.x), (short)(-v1.y));
    }

    public static Vector2Short operator -(int i, Vector2Short v1)
    {
        return new Vector2Short((short)(i - v1.x), (short)(i - v1.y));
    }

    public static Vector2Short operator -(Vector2Short v1, int i)
    {
        return new Vector2Short((short)(v1.x - i), (short)(v1.y - i));
    }

    public static Vector2Short operator *(int i, Vector2Short v1)
    {
        return new Vector2Short((short)(i * v1.x), (short)(i * v1.y));
    }

    public static Vector2Short operator *(Vector2Short v1, int i)
    {
        return i * v1;
    }

    /// <summary>
    /// REMINDER : THIS IS AN INTEGER DIVISION (no rounding, just a basic division)
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static Vector2Short operator /(Vector2Short v1, int i)
    {
        return new Vector2Short((short)(v1.x / i), (short)(v1.y / i));
    }


    public static implicit operator Vector3Short(Vector2Short v)
    {
        return new Vector3Short(v.x, v.y, 0);
    }
}