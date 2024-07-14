using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtender 
{
    public static T FindComponentInChildOrSelf<T>(this Transform transform) where T : Component
    {
        T result = transform.GetComponent<T>();
        if (result != null)
        {
            return result;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            result = transform.GetChild(i).FindComponentInChildOrSelf<T>();
            if (result != null)
            {
                return result;
            }
        }
        return default(T);
    }
}
