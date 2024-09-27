using IdleHeaven;
using UnityEngine;


[RequireComponent(typeof(Attack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _autoSet = false;
    [SerializeField] private Health _health;

    [SerializeField] private EnemyRandomItemDropper _itemDroper;

    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private StateMachine _stateMachine;

    [SerializeField] private EnemyAiController _aiController;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _aiController = GetComponent<EnemyAiController>();
    }
    private void OnValidate()
    {
        if (_autoSet)
        {
            _health = GetComponent<Health>();
            _autoSet = false;
        }
    }
    private void OnDestroy()
    {
    }

    public Enemy Init(EnemyData randomEnemyData, int level)
    {
        Stats bastStats =randomEnemyData.GetStats();
        _health.ResetDead();
        _characterStats.Init(bastStats, level);
        _health.SetMaxHp(_characterStats.GetResultStats()[StatType.Hp]);
        _health.ResetHpWithRatio(1);
        _aiController.Init();
        _stateMachine.ChangeState<IdleState>();
        return this;
    }
}
