using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, int initialSize = 10, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateNewObject();
            ReturnToPool(obj);
        }
    }

    public T Get()
    {
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = CreateNewObject();
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    private T CreateNewObject()
    {
        T obj = Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        return obj;
    }

    public void Clear()
    {
        while (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            if (obj != null)
                Object.Destroy(obj.gameObject);
        }
    }
}

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPoolManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("ObjectPoolManager");
                    _instance = go.AddComponent<ObjectPoolManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private Dictionary<string, object> pools = new Dictionary<string, object>();

    public ObjectPool<T> CreatePool<T>(string poolName, T prefab, int initialSize = 10) where T : MonoBehaviour
    {
        if (pools.ContainsKey(poolName))
        {
            return pools[poolName] as ObjectPool<T>;
        }

        Transform poolParent = new GameObject($"Pool_{poolName}").transform;
        poolParent.SetParent(transform);

        ObjectPool<T> pool = new ObjectPool<T>(prefab, initialSize, poolParent);
        pools[poolName] = pool;
        return pool;
    }

    public ObjectPool<T> GetPool<T>(string poolName) where T : MonoBehaviour
    {
        return pools.ContainsKey(poolName) ? pools[poolName] as ObjectPool<T> : null;
    }

    public void ClearPool(string poolName)
    {
        if (pools.ContainsKey(poolName))
        {
            var pool = pools[poolName];
            if (pool is ObjectPool<MonoBehaviour> monoPool)
            {
                monoPool.Clear();
            }
            pools.Remove(poolName);
        }
    }

    public void ClearAllPools()
    {
        foreach (var pool in pools.Values)
        {
            if (pool is ObjectPool<MonoBehaviour> monoPool)
            {
                monoPool.Clear();
            }
        }
        pools.Clear();
    }
}