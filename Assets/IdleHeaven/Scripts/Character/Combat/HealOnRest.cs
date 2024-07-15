using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnRest : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Attack _attack;

    private float _foughtTimeStamp = 0f;

    private void OnEnable()
    {
        StartCoroutine(HealOverTime());
        _attack.OnAttack.AddListener(OnFought);
        _health.OnDamaged.AddListener(OnFought);
    }
    private void OnDisable()
    {
        _health.OnDamaged.RemoveListener(OnFought);
        _attack.OnAttack.RemoveListener(OnFought);
        StopAllCoroutines();
    }

    private void OnFought(Health health, float damage, AttackType attackType)
    {
        SetFoughtTimeStamp();
    }
    private void OnFought(Attack attack, AttackType attackType)
    {
        SetFoughtTimeStamp();
    }

    private void SetFoughtTimeStamp()
    {
        _foughtTimeStamp = Time.time;
    }

    private IEnumerator HealOverTime()
    {
        while (true)
        {

            if (_foughtTimeStamp + 1.5f < Time.time)
            {
                _health.Heal(_health.GetMaxHp() * 0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
    }


}
