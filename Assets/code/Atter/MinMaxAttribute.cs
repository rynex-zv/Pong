using UnityEngine;

public class MinMaxAttribute : PropertyAttribute
{
    public int Min { get; private set; }
    public int Max { get; private set; }

    public MinMaxAttribute( int min , int max )
    {
        Min = min;
        Max = max;
    }
}