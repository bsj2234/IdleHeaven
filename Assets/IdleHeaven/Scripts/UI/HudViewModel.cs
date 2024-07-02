using IdleHeaven;
using System.ComponentModel;
using UnityEngine;

public class HudViewModel : MonoBehaviour, INotifyPropertyChanged
{
    [SerializeField] CharacterStats _characterStats;
    [SerializeField] Health _health;

    private float _hp;
    private float _maxHp;
    private float _hpPercentage;

    public float Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            OnPropertyChanged(nameof(Hp));
        }
    }
    public float MaxHp
    {
        get => _maxHp;
        set
        {
            _maxHp = value;
            OnPropertyChanged(nameof(MaxHp));
        }
    }
    public float HpPercentage
    {
        get => _hpPercentage;
        set
        {
            _hpPercentage = value;
            OnPropertyChanged(nameof(HpPercentage));
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;


    private void Start()
    {
        _characterStats.Stats.stats[(int)StatType.Hp].RegisterStatChanged(HandleMaxHpChanged);
        _health.OnDamaged.AddListener(HandleOnDamaged);
        _health.OnHeal.AddListener(HandleOnHeal);
    }

    public void UpdateAllHp()
    {
        Hp = _health.GetHp();
        MaxHp = _characterStats.Stats.stats[(int)StatType.Hp].Value;
        HpPercentage = Hp / MaxHp;
    }

    // 이벤트 핸들러
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void HandleMaxHpChanged(Stat stat)
    {
        UpdateAllHp();
    }
    private void HandleOnHeal()
    {
        UpdateAllHp();
    }
    private void HandleOnDamaged(Attack attack, AttackType attackType)
    {
        UpdateAllHp();
    }
}
