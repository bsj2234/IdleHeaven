using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    [SerializeField] float _spawnInterval;
    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] GameObject[] _enemyPrefab;
    [SerializeField] int _maxEnemies;


    [SerializeField] Attack _playerAttack;


    private void Start()
    {
        StartCoroutine(SpawnEnemy());
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
            GameObject randomEnemy = _enemyPrefab[Random.Range(0, _enemyPrefab.Length)];
            GameObject enemy = Instantiate(randomEnemy, randomTrf.position, Quaternion.identity);
            AddEnemy(enemy);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy.GetComponent<Enemy>());
        enemy.GetComponent<Health>().OnDead += HandleOnEnemyDead;
    }

    private void HandleOnEnemyDead(Health health)
    {
        _enemies.Remove(health.GetComponent<Enemy>());
        health.OnDead -= HandleOnEnemyDead;
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
