using UnityEngine;
using IdleHeaven;
public class HitVfx : MonoBehaviour, Vfx
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private float _randomOffset;

    Vector3 attackerPos;

    public void Play()
    {
        Vector3 randomV3 = new Vector3(Random.Range(-_randomOffset, _randomOffset)
            , Random.Range(-_randomOffset, _randomOffset),
            Random.Range(-_randomOffset, _randomOffset));


        Vector3 resultPos = transform.position + randomV3 + Vector3.up ;

        Vector3 toAttacker = attackerPos - resultPos;
        toAttacker = toAttacker.normalized;

        resultPos += toAttacker * 0.5f;

        Vector3 toCamera = Camera.main.transform.position - resultPos;
        toCamera = toCamera.normalized;

        resultPos += toCamera * 7f;

        IPooledObject pooledObject = ObjectPoolingManager.Instance.SpawnFromPool(_hitEffect, resultPos, Quaternion.identity);
        ObjectPoolingManager.Instance.ReturnToPool(pooledObject, 1f);

    }

    public void HandleOnDamaged(Attack attack, Health health)
    {
        attackerPos = attack.transform.position;
        Play();
    }
}
