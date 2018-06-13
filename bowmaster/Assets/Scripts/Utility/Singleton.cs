using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{

    public static T instance;

    private void OnEnable()
    {
        if (instance == null)
            instance = this as T;
    }

    private void OnDisable()
    {
        instance = null;
    }
}
