using UnityEngine;

public static class Vector2Extension
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}

public static class IntegerExtensions
{
    public static bool ReadBit(this int i, int bitNumber)
    {
        return (i & (1 << bitNumber - 1)) != 0;
    }

    public static void SetBit(this int i, int bitNumber, bool bitValue)
    {
        if(bitValue)
            i |= 1 << bitNumber;
        else
            i &= ~(1 << bitNumber);
    }
}