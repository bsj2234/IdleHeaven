using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, ICombat
{
    [SerializeField] Combat combat = new Combat();

    [SerializeField] ItemSpawner spawner;
    
    [SerializeField] GameObject item;

    private void Start()
    {
        combat.Init(transform);
        combat.OnDeadWAttacker += SpawnItem;
        combat.OnDead += DestroySelf;
    }

    public void SpawnItem(Combat attacker)
    {
        spawner.SpawnItem(true, transform, item, attacker._owner.GetComponent<ICombat>());
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
