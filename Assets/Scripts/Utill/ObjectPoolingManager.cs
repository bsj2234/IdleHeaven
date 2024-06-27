using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface IPooledObject
{
    GameObject Prefab { get; }
    Transform transform { get; }

    void Init(GameObject prefab);
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

    private GameObject poolParent;

    private void Awake()
    {
        poolDictionary = new Dictionary<GameObject, Queue<IPooledObject>>();
        poolParent = new GameObject("=======ObjectPool=======");

        foreach (Pool pool in pools)
        {
            Queue<IPooledObject> objectPool = new Queue<IPooledObject>();

            GameObject currentPoolParent = new GameObject($"======={pool.prefab.name}=======");
            currentPoolParent.transform.SetParent(poolParent.transform);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                obj.transform.SetParent(currentPoolParent.transform);


                if(obj.TryGetComponent(out IPooledObject pooledObject))
                {
                    objectPool.Enqueue(pooledObject);
                }
                else
                {
                    Assert.IsTrue(false,$"Prefab {pool.prefab.name} doesn't have a component that implements IPooledObject interface.");
                }
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

        if(objectToSpawn.transform.gameObject.activeSelf == true)
        {
            Debug.Assert(false,$"Object {objectToSpawn.transform.gameObject.name} is already active. Returning to pool.");
            ReturnToPool(prefab, objectToSpawn);
            return null;
        }

        objectToSpawn.transform.gameObject.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.OnObjectReuse();

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject prefab, IPooledObject objectToReturn, float lifeTime)
    {
        StartCoroutine(ReleaseTimer(prefab, objectToReturn, lifeTime));
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


    private IEnumerator ReleaseTimer(GameObject prefab, IPooledObject objectToReturn, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool(prefab, objectToReturn);
    }
}
