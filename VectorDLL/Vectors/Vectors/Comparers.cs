using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Vector3ShortComparer : IEqualityComparer<Vector3Short>
{
    public bool Equals(Vector3Short v1, Vector3Short v2)
    {
        return v1.Equals(v2);
    }

    public int GetHashCode(Vector3Short obj)
    {
        return obj.GetHashCode();
    }
}

public class Vector3Short64Comparer : IEqualityComparer<Vector3Short>
{
    public bool Equals(Vector3Short v1, Vector3Short v2)
    {
        return v1.Equals64(v2);
    }

    public int GetHashCode(Vector3Short obj)
    {
        return obj.GetHashCode();
    }
}

