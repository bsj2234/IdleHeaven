using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class Enemy1 : MonoBehaviour
{
    [SerializeField] bool autoSet = false;
    [SerializeField] Health combat;

    [SerializeField] ItemSpawner spawner;
    
    [SerializeField] GameObject item;

    private void Start()
    {
        combat.OnDead += DestroySelf;
    }
    private void OnValidate()
    {
        if(autoSet)
        {
            combat = GetComponent<Health>();
            autoSet = false;
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
