using UnityEngine;

public class CounterWithMaxValue
{
    public int Max { get; private set; }
    public int Current { get; private set; }

    public CounterWithMaxValue(int max, int current)
    {
        Max = max;
        Current = current;
    }

    public void Add(int value)
    {
        Current = Mathf.Min(Current + value, Max);
    }

    public void Remove(int value)
    {
        Current -= value;
    }
}
