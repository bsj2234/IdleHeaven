using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] bool autoSet = false;
    [SerializeField] Health health;

    [SerializeField] ItemSpawner spawner;
    
    private void Awake()
    {
        health.OnDead += HandleDead;
    }
    private void OnDestroy()
    {
        health.OnDead -= HandleDead;
    }
    private void OnValidate()
    {
        if(autoSet)
        {
            health = GetComponent<Health>();
            autoSet = false;
        }
    }

    private void HandleDead(Health self)
    {
        Destroy(gameObject);
    }
}
