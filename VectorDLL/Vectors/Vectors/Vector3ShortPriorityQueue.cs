using System.Collections.Generic;

public class Vector3ShortPriorityQueue
{
    private Dictionary<Vector3Short, int> elements;

    public int count { get; private set; }

    public Vector3ShortPriorityQueue()
    {
        elements = new Dictionary<Vector3Short, int>(new Vector3ShortComparer());
        count = 0;
    }
    public Vector3ShortPriorityQueue(int allocSize)
    {
        elements = new Dictionary<Vector3Short, int>(allocSize, new Vector3ShortComparer());
        count = 0;
    }

    public bool Contains(Vector3Short v)
    {
        return elements.ContainsKey(v);
    }


    public void Enqueue(Vector3Short v, int priority)
    {
        if (!elements.ContainsKey(v))
        {
            elements.Add(v, priority);
            count++;
        }
    }

    public Vector3Short Dequeue()
    {
        int bestValue = 999;
        Vector3Short bestKey = Vector3Short.zero;

        Dictionary<Vector3Short, int>.Enumerator enumerator = elements.GetEnumerator();
        KeyValuePair<Vector3Short, int> keyValue;
        while (enumerator.MoveNext())
        {
            keyValue = enumerator.Current;
            if (keyValue.Value < bestValue)
            {
                bestValue = keyValue.Value;
                bestKey = keyValue.Key;
            }
        }

        elements.Remove(bestKey);
        count--;
        return bestKey;
    }
}
