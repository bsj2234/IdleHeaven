using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnKill : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Attack _attack;

    private void OnEnable()
    {
        _attack.OnKillEnemy += Heal;
    }
    private void OnDisable()
    {
        _attack.OnKillEnemy -= Heal;
    }

    private void Heal()
    {
        _health.Heal(_health.GetMaxHp() * 0.1f);
    }



}
