using IdleHeaven;
using UnityEngine;


[RequireComponent(typeof(Attack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] bool autoSet = false;
    [SerializeField] Health health;

    [SerializeField] ItemSpawner spawner;

    [SerializeField] Stats _baseStats;
    [SerializeField] Stats _resultStats;
    [SerializeField] LevelSystem _levelSystem;

    [SerializeField] CharacterStats _characterStats;

    private void Awake()
    {
        health.OnDead.AddListener(HandleDead);
        _levelSystem = GetComponent<CharacterStats>().LevelSystem;
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
        //_resultStats = _levelSystem.Level / 60f * _baseStats;
        _baseStats.Clear();
        _baseStats.AddStats(_resultStats);
    }


}
