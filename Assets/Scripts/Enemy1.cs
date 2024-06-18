using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combat))]
public class Enemy1 : MonoBehaviour, ICombat
{
    [SerializeField] bool autoSet = false;
    [SerializeField] Combat combat;

    [SerializeField] ItemSpawner spawner;
    
    [SerializeField] GameObject item;

    private void Start()
    {
        combat.Init(transform);
        combat.OnDead += DestroySelf;
    }
    private void OnValidate()
    {
        if(autoSet)
        {
            combat = GetComponent<Combat>();
            autoSet = false;
        }
    }


    public void Attack(ICombat targetCombat, float damage)
    {
        targetCombat.TakeDamage(this, damage);
    }

    public Combat GetCombat()
    {
        return combat;
    }

    public void TakeDamage(ICombat attackerCombat, float damage)
    {
        combat.TakeDamage(attackerCombat.GetCombat(), damage);
    }

    public bool IsDead()
    {
        return combat.IsDead();
    }

    public float GetDistance(Vector3 origin)
    {
        return Vector3.Distance(origin, transform.position);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public GameObject GetGameObject()
    {
        if(this == null)
        {
            return null;
        }
        return gameObject;
    }
}
