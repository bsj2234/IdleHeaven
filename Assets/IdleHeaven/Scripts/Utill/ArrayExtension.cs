using System;
using UnityEngine;
public static class ArrayExtensions
{
    public static T GetRandomValue<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
        {
            throw new InvalidOperationException("Array is null or empty.");
        }

        int randomIndex = UnityEngine.Random.Range(0, array.Length);
        return array[randomIndex];
    }
}