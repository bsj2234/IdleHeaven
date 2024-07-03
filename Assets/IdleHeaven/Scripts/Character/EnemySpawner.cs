using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    [SerializeField] private float _spawnInterval;

    [SerializeField] private Transform[] _spawnPoint;


    [SerializeField] private string[] _enemyToSpawn;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private int _stageLevel = 1;


    [SerializeField] private Attack _playerAttack;
    [SerializeField] private ItemSpawner _itemSpawner;


    private Coroutine spawnCoroutine;


    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        if(spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }

    public void Init(EnemySpawner enemySpawner)
    {
        _enemyToSpawn = enemySpawner._enemyToSpawn; 
        _maxEnemies = enemySpawner._maxEnemies;
        _stageLevel = enemySpawner._stageLevel;
        spawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (_enemies.Count >= _maxEnemies)
            {
                yield return new WaitForSeconds(_spawnInterval);
                continue;
            }
            Transform randomTrf = _spawnPoint[Random.Range(0, _spawnPoint.Length)];

            EnemyData randomEnemyData = CSVParser.Instance.enemies[_enemyToSpawn[Random.Range(0, _enemyToSpawn.Length)]];

            GameObject randomEnemyPrf = randomEnemyData.Prefab;
            Vector3 relativeToPlayerLocal = _playerAttack.transform.position + randomTrf.localPosition;

            GameObject enemy = Instantiate(randomEnemyPrf, relativeToPlayerLocal, Quaternion.identity);

            enemy.GetComponent<Enemy>().Init(randomEnemyData).SetLevel(_stageLevel); ;
            enemy.GetComponent<ItemDroper>().Init(_itemSpawner);
            AddEnemy(enemy);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy.GetComponent<Enemy>());
        enemy.GetComponent<Health>().OnDead.AddListener(HandleOnEnemyDead);
    }

    private void HandleOnEnemyDead(Attack attacker, Health health)
    {
        _enemies.Remove(health.GetComponent<Enemy>());
        health.OnDead.RemoveListener(HandleOnEnemyDead);
    }


    public void ClearEnemies()
    {
        foreach (var enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }
        _enemies.Clear();
    }


}
