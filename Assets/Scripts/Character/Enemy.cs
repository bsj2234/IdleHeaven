using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] bool autoSet = false;
    [SerializeField] Health combat;

    [SerializeField] ItemSpawner spawner;
    
    [SerializeField] GameObject item;

    private void Awake()
    {
        combat.OnDead += HandleDead;
    }
    private void OnDestroy()
    {
        combat.OnDead -= HandleDead;
    }
    private void OnValidate()
    {
        if(autoSet)
        {
            combat = GetComponent<Health>();
            autoSet = false;
        }
    }

    private void HandleDead()
    {
        Destroy(gameObject);
    }
}
