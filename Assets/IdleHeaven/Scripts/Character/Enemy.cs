using IdleHeaven;
using UnityEngine;


[RequireComponent(typeof(Attack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _autoSet = false;
    [SerializeField] private Health _health;

    [SerializeField] private EnemyRandomItemDropper _itemDroper;

    [SerializeField] private Stats _baseStats;
    [SerializeField] private Stats _resultStats;
    [SerializeField] private LevelSystem _levelSystem;

    [SerializeField] private CharacterStats _characterStats;

    private void Awake()
    {
        _health.OnDead.AddListener(HandleDead);
        _levelSystem = GetComponent<CharacterStats>().LevelSystem;
    }
    private void OnDestroy()
    {
        _health.OnDead.RemoveListener(HandleDead);
    }
    private void OnValidate()
    {
        if (_autoSet)
        {
            _health = GetComponent<Health>();
            _autoSet = false;
        }
    }

    private void HandleDead(Attack attacker, Health self)
    {
        Destroy(gameObject);
    }

    public Enemy Init(EnemyData randomEnemyData)
    {
        SetBaseStats(randomEnemyData);
        _characterStats.Stats = _baseStats;

        return this;
    }

    public void SetLevel(int level)
    {
        _levelSystem.Level = level;
        CalcStat();
    }

    public void SetBaseStats(EnemyData randomEnemyData)
    {
        _baseStats[StatType.Attack] = randomEnemyData.BaseAttack;
        _baseStats[StatType.Defense] = randomEnemyData.BaseDefense;
        _baseStats[StatType.Hp] = randomEnemyData.BaseHealth;
        _baseStats[StatType.Speed] = randomEnemyData.BaseSpeed;
    }

    public void CalcStat()
    {
        _resultStats.Clear();
        _resultStats.AddStats(_baseStats);
        _resultStats.MultiplyStats(_levelSystem.Level);
        _characterStats.Stats = _resultStats;
    }


}
