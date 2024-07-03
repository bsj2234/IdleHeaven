using IdleHeaven;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] CharacterStats characterStats;

    [SerializeField] float initalMaxHp = 100f;
    [SerializeField] float _maxHp = 100f;

    [SerializeField] float _hp = 100f;

    [SerializeField] bool _dead = false;

    [SerializeField] float _invincibleTimeOnHit = .1f;
    [SerializeField] float _prevHitTime = 0f;

    [SerializeField] Attack _attackComponentOfSelf;

    public Func<bool> AdditionalDamageableCheck { get; set; }
    public UnityEvent<Attack, AttackType> OnDamaged;
    public UnityEvent OnHeal;
    public UnityEvent<Attack, Health> OnDead;



    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
    }
    public void Init()
    {
        Heal(_maxHp);
        initalMaxHp = _maxHp;
        _attackComponentOfSelf = GetComponent<Attack>();
    }
    public float GetHp() { return _hp; }
    public void SetMaxHp(float maxHp)
    {
        _maxHp = maxHp;
    }
    public float GetMaxHp()
    {
        return _maxHp;
    }
    public void AddMaxHp(float add)
    {
        _maxHp = initalMaxHp + add;
    }
    public void ResetHpWithRatio(float ratio)
    {
        _hp = _maxHp * ratio;
    }


    private bool IsDamageable()
    {
        if (Time.time < _prevHitTime + _invincibleTimeOnHit)
        {
            return false;
        }
        if (_dead)
        {
            return false;
        }
        bool result = true;
        if (AdditionalDamageableCheck != null)
        {
            result = result && AdditionalDamageableCheck.Invoke();
        }
        if (!result)
        {
            return false;
        }
        return true;
    }
    public bool TakeDamage(Attack attacker, float damage, AttackType attackType = AttackType.None)
    {
        if (!IsDamageable())
            return false;
        CalcTakeDamage(damage);
        OnDamaged?.Invoke(attacker, attackType);

        if (_hp <= 0f)
        {
            _dead = true;
            OnDead?.Invoke(attacker,this);
        }
        return true;
    }
    private void CalcTakeDamage(float damage)
    {
        _prevHitTime = Time.time;
        _hp -= damage;
    }


    public void Heal(float amount)
    {
        if (_hp < _maxHp)
        {
            _hp += amount;
            if (_hp > _maxHp)
            {
                _hp = _maxHp;
            }
        }
        if (OnHeal != null)
        {
            OnHeal.Invoke();
        }
    }
    public void Die()
    {
        if (_attackComponentOfSelf != null)
        {
            TakeDamage(_attackComponentOfSelf, _hp);
        }
        TakeDamage(null,_hp);
    }


    public bool IsDead()
    {
        return _dead;
    }
    public void ResetDead()
    {
        Heal(_maxHp);
        _dead = false;
    }

    public void OnLevelUp()
    {
        _maxHp = GetComponent<CharacterStats>().Stats[StatType.Hp];
        Heal(_maxHp);
    }
}
