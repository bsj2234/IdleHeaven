using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnRest : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Attack _attack;

    private float _foughtTimeStamp = 0f;

    public void Start()
    {
        StartCoroutine(HealOverTime());
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
