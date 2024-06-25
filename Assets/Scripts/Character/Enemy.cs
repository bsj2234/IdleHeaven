using IdleHeaven;
using System;
using UnityEngine;


[RequireComponent(typeof(Attack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] bool autoSet = false;
    [SerializeField] Health health;

    [SerializeField] ItemSpawner spawner;

    private void Awake()
    {
        health.OnDead.AddListener(HandleDead);
    }
    private void OnDestroy()
    {
        health.OnDead.RemoveListener(HandleDead);
    }
    private void OnValidate()
    {
        if (autoSet)
        {
            health = GetComponent<Health>();
            autoSet = false;
        }
    }

    private void HandleDead(Attack attacker ,Health self)
    {
        Destroy(gameObject);
    }
}
