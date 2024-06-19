using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float initalMaxHp = 100f;
    [SerializeField] float _maxHp = 100f;
    [SerializeField] float _hp = 100f;
    [SerializeField] bool _dead = false;
    [SerializeField] float _invincibleTimeOnHit = .1f;
    [SerializeField] float _prevHitTime = 0f;
    [SerializeField] Attack _attackComponentOfSelf;

    public Action OnDamaged { get; set; }
    public Action<Attack> OnDamagedWAttacker { get; set; }
    public Action OnDead { get; set; }
    public Action<Attack> OnDeadWAttacker { get; set; }
    public Func<bool> OnDamageableCheck { get; set; }
    public Action OnHeal { get; set; }
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        _hp = _maxHp;
        initalMaxHp = _maxHp;
        _attackComponentOfSelf = GetComponent<Attack>();
    }
    public float GetHp() { return _hp; }
    public void SetMaxHp(float maxHp)
    {
        _maxHp = maxHp;
        ResetDead();
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
        _dead = false;
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
        if (OnDamageableCheck != null)
        {
            result = result && OnDamageableCheck.Invoke();
        }
        if (!result)
        {
            return false;
        }
        return true;
    }
    public bool TakeDamage(Attack attacker, float damage)
    {
        if (!IsDamageable())
            return false;
        CalcTakeDamage(damage);
        OnDamagedWAttacker?.Invoke(attacker);

        if (_hp <= 0f)
        {
            _dead = true;
            OnDead?.Invoke();
            OnDeadWAttacker(attacker);
        }
        return true;
    }
    private void CalcTakeDamage(float damage)
    {
        _prevHitTime = Time.time;
        _hp -= damage;
        OnDamaged?.Invoke();
    }
    public void Heal(int v)
    {
        if (_hp < _maxHp)
        {
            _hp += v;
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
        _hp = _maxHp;
        _dead = false;
    }
}
