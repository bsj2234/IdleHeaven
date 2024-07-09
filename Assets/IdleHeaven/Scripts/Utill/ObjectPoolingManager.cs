using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface IPooledObject
{
    GameObject Prefab { get; }
    Transform transform { get; }

    string tag_Pool { get; }

    bool pooled { get; set; }
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
    private Dictionary<string, GameObject> tagpoolDictionary;
    private Dictionary<string, Transform> parentDictionary;

    private GameObject poolParent;

    private void Awake()
    {
        poolDictionary = new Dictionary<GameObject, Queue<IPooledObject>>();
        tagpoolDictionary = new Dictionary<string, GameObject>();
        parentDictionary = new Dictionary<string, Transform>();
        poolParent = new GameObject("=======ObjectPool=======");

        foreach (Pool pool in pools)
        {
            Queue<IPooledObject> objectPool = new Queue<IPooledObject>();

            GameObject currentPoolParent = new GameObject($"======={pool.prefab.name}=======");
            currentPoolParent.transform.SetParent(poolParent.transform);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.name = $"{obj.name} {i}";
                obj.SetActive(false);

                obj.transform.SetParent(currentPoolParent.transform);


                if(obj.TryGetComponent(out IPooledObject pooledObject))
                {
                    objectPool.Enqueue(pooledObject);

                    if(!tagpoolDictionary.ContainsKey(pooledObject.tag_Pool))
                    {
                        tagpoolDictionary.Add(pooledObject.tag_Pool, pool.prefab);
                    }
                    if(!parentDictionary.ContainsKey(pooledObject.tag_Pool))
                    {
                        parentDictionary.Add(pooledObject.tag_Pool, currentPoolParent.transform);
                    }
                }
                else
                {
                    Assert.IsTrue(false,$"Prefab {pool.prefab.name} doesn't have a component that implements IPooledObject interface.");
                }
            }

            poolDictionary.Add(pool.prefab, objectPool);
        }
    }

    private IPooledObject IntantiateAdditional(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        IPooledObject pooledObject1 = obj.GetComponent<IPooledObject>();
        pooledObject1.pooled = true;
        Transform currentPoolParent = parentDictionary[pooledObject1.tag_Pool];


        obj.transform.SetParent(currentPoolParent);


        if (obj.TryGetComponent(out IPooledObject pooledObject))
        {
            return pooledObject;
        }
        else
        {
            Assert.IsTrue(false, $"Prefab {prefab.name} doesn't have a component that implements IPooledObject interface.");
            return null;
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
            return IntantiateAdditional(prefab);
        }
        IPooledObject objectToSpawn = poolDictionary[prefab].Dequeue();

        if (objectToSpawn.transform.gameObject.activeSelf == true)
        {
            Debug.LogWarning($"Object {objectToSpawn.transform.gameObject.name} is already active. Instantiate");
            StartCoroutine(DelayedCheck(objectToSpawn.transform));
            return IntantiateAdditional(prefab);
        }

        objectToSpawn.transform.gameObject.SetActive(true);
        objectToSpawn.pooled = true;
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.OnObjectReuse();

        return objectToSpawn;
    }
    private IEnumerator DelayedCheck(Transform transform)
    {
        yield return null;
        yield return null;
        if(transform.gameObject.activeSelf == true)
        {
            Debug.LogWarning($"Object is still active. Instantiate");
        }
    }
    public void ReturnToPool(IPooledObject objectToReturn, float lifeTime)
    {
        StartCoroutine(ReleaseTimer(objectToReturn, lifeTime));
    }

    public void ReturnToPool(IPooledObject objectToReturn)
    {

        objectToReturn.transform.gameObject.SetActive(false);

        objectToReturn.OnObjectRelease();

        if(tagpoolDictionary.ContainsKey(objectToReturn.tag_Pool))
        {
            objectToReturn.transform.gameObject.SetActive(false);
            objectToReturn.pooled = false;

            Queue<IPooledObject> queue = poolDictionary[tagpoolDictionary[objectToReturn.tag_Pool]];

            if(queue.Contains(objectToReturn))
            {
                Debug.LogWarning($"Object {objectToReturn.transform.gameObject.name} is already in the pool.");
                return;
            }

            queue.Enqueue(objectToReturn);
            if(objectToReturn.transform.gameObject.activeSelf == true)
            {
                Debug.Assert(false,$"Object {objectToReturn.transform.gameObject.name} is active even set it to false ,Never gonna happen");
            }
        }
        else
        {
            Assert.IsTrue(false, $"Pool for prefab {objectToReturn.tag_Pool} doesn't exist.");
        }
    }


    private IEnumerator ReleaseTimer(IPooledObject objectToReturn, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool(objectToReturn);
    }
}
