using UnityEngine;

public static class LowestPointCalculator
{
    public static float GetLowestPoint<T>(this Transform origin) where T : Collider =>
    origin.GetComponent<T>().bounds.min.y;
}
