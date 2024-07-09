using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour, IPooledObject
{
    public UnityEvent OnObjectReuseEvent;
    public GameObject _prefab;
    public string _tag_Pool;
    public bool pooled { get; set; }

    public string tag_Pool => _tag_Pool;
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

    private void Update()
    {
        if(pooled != gameObject.activeSelf)
        {
            Debug.LogError($"{gameObject.name}PooledObject is not enabled");
        }
    }

    public void Release()
    {
        ObjectPoolingManager.Instance.ReturnToPool(this);
    }

    private IEnumerator ReleaseTimer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPoolingManager.Instance.ReturnToPool(this);
    }
}
