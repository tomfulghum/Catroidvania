using System;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] bool dontDestroyOnLoad;

    static readonly Lazy<T> lazyInstance = new Lazy<T>(Instantiate);

    public static T Instance => lazyInstance.Value;

    static T Instantiate()
    {
        var instance = FindObjectOfType<T>();
        if (!instance) {
            Debug.LogWarningFormat("Instance of {0} was accessed but not yet initialized.", typeof(T));
            instance = new GameObject($"{typeof(T)} (Singleton)").AddComponent<T>();
        }

        return instance;
    }

    protected virtual void Awake()
    {
        if (Instance != this) {
            Destroy(gameObject);
            return;
        }

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
