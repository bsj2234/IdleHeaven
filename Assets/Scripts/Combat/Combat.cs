using UnityEngine;

public interface ICombat
{
    Combat GetCombat();
    void TakeDamage(ICombat attackerCombat, float damage);
    void Attack(ICombat targetCombat, float damage);
    bool IsDead();
    float GetDistance(Vector3 origin);
    Transform GetTransform();
    GameObject GetGameObject();
}

[System.Serializable]
public class Combat:MonoBehaviour
{
    public Transform _owner;
    public float initalMaxHp;
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _hp = 100f;
    [SerializeField] private bool _dead = false;
    [SerializeField] private float _invincibleTimeOnHit = .1f;
    [SerializeField] private float _prevHitTime = 0f;

    public System.Action OnDamaged { get; set; }
    public System.Action<Combat> OnDamagedWAttacker { get; set; }
    public System.Action OnDead { get; set; }
    public System.Action<Combat> OnDeadWAttacker { get; set; }
    //falseknight가 mainbody일때 데미지를 안먹기 위해서
    public System.Func<bool> AdditionalDamageableCondition { get; set; }
    public System.Action OnHeal { get; set; }
    public void Init(Transform owner, bool defaultEffectOnDamaged = true)
    {
        _owner = owner;
        _hp = _maxHp;
        initalMaxHp = _maxHp;
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
    public bool DealDamage(Combat target, float damage)
    {
        bool isAttackSucceeded = target.TakeDamage(this, damage);
        if (isAttackSucceeded)
        {
            OnDamagedWAttacker?.Invoke(target);
            return false;
        }
        return true;
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
        if (AdditionalDamageableCondition != null)
        {
            result = result && AdditionalDamageableCondition.Invoke();
        }
        if (!result)
        {
            return false;
        }
        return true;
    }
    private void CalcTakeDamage(float damage)
    {
        _prevHitTime = Time.time;
        _hp -= damage;
        OnDamaged?.Invoke();
    }
    public bool TakeDamage(Combat attacker, float damage)
    {
        if (!IsDamageable())
            return false;
        CalcTakeDamage(damage);

        if (_hp <= 0f)
        {
            _dead = true;
            OnDead?.Invoke();
            OnDeadWAttacker(attacker);
        }
        return true;
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
        TakeDamage(this, _hp);
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