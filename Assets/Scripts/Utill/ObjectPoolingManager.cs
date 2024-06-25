using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface IPooledObject
{
    Transform transform { get; }
    void OnObjectReuse();
    void OnObjectRelease();
}

public class ObjectPoolingManager : MonoSingleton<ObjectPoolingManager>
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<GameObject, Queue<IPooledObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<GameObject, Queue<IPooledObject>>();

        foreach (Pool pool in pools)
        {
            Queue<IPooledObject> objectPool = new Queue<IPooledObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                if(TryGetComponent(out IPooledObject pooledObject))
                {
                    objectPool.Enqueue(pooledObject);
                }
                else
                {
                    Assert.IsTrue(false,$"Prefab {pool.prefab.name} doesn't have a component that implements IPooledObject interface.");
                }
                objectPool.Enqueue(pooledObject);
            }

            poolDictionary.Add(pool.prefab, objectPool);
        }
    }

    public IPooledObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning($"Pool for prefab {prefab.name} doesn't exist.");
            return null;
        }
        if(poolDictionary[prefab].Count == 0)
        {
            Debug.LogWarning($"Pool for prefab {prefab.name} is empty. Instantating");
            GameObject obj = Instantiate(prefab);
            return null;
        }
        IPooledObject objectToSpawn = poolDictionary[prefab].Dequeue();

        objectToSpawn.transform.gameObject.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.OnObjectReuse();

        poolDictionary[prefab].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject prefab, IPooledObject objectToReturn)
    {
        objectToReturn.transform.gameObject.SetActive(false);

        objectToReturn.OnObjectRelease();

        if (poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab].Enqueue(objectToReturn);
        }
        else
        {
            Assert.IsTrue(false, $"Pool for prefab {prefab.name} doesn't exist.");
        }
    }
}
