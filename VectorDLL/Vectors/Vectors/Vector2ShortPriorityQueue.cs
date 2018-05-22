using System.Collections.Generic;

public class Vector2ShortPriorityQueue
{
    private Dictionary<Vector2Short, int> elements;

    public int count { get; private set; }

    public Vector2ShortPriorityQueue()
    {
        elements = new Dictionary<Vector2Short, int>(new Vector2ShortComparer());
        count = 0;
    }
    public Vector2ShortPriorityQueue(int allocSize)
    {
        elements = new Dictionary<Vector2Short, int>(allocSize, new Vector2ShortComparer());
        count = 0;
    }

    public bool Contains(Vector2Short v)
    {
        return elements.ContainsKey(v);
    }


    public void Enqueue(Vector2Short v, int priority)
    {
        if (!elements.ContainsKey(v))
        {
            elements.Add(v, priority);
            count++;
        }
    }

    public Vector2Short Dequeue()
    {
        int bestValue = 999;
        Vector2Short bestKey = Vector2Short.zero;

        Dictionary<Vector2Short, int>.Enumerator enumerator = elements.GetEnumerator();
        KeyValuePair<Vector2Short, int> keyValue;
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