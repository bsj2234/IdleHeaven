using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour, IPooledObject
{
    public UnityEvent OnObjectReuseEvent;
    public GameObject _prefab;

    public GameObject Prefab => _prefab;

    public void Init(GameObject prefab)
    {
       _prefab = prefab;
    }

    public void OnObjectReuse()
    {
        OnObjectReuseEvent?.Invoke();
    }

    public void OnObjectRelease()
    {
    }

    private IEnumerator ReleaseTimer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPoolingManager.Instance.ReturnToPool(Prefab ,this as IPooledObject);
    }
}