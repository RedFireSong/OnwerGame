using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance { get { return instance; } }
    void Awake() { instance = (T)(MonoBehaviour)this; }
}
