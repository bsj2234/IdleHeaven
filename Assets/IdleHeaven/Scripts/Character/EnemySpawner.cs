using IdleHeaven;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EnemySpawnLocationType
{
    Vector3,
    Transform
}

[Serializable]
public class EnemySpawnPos
{
    public EnemySpawnLocationType LocationType;
    public Vector3 PosVec3;
    public Transform PosTrf;

    public Vector3 GetPos()
    {
        if (LocationType == EnemySpawnLocationType.Vector3)
        {
            return PosVec3;
        }
        else
        {
            return PosTrf.localPosition;
        }
    }
}


public class EnemySpawner : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    [SerializeField] private float _spawnInterval;
    [SerializeField] private EnemySpawnPos[] _localSpawnPoint;

    [SerializeField] private string[] _enemyToSpawn;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private int _stageLevel = 1;

    [SerializeField] private Attack _playerAttack;

    private Coroutine spawnCoroutine;



    private void OnDisable()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }

    public void Init(EnemySpawnData enemySpawnData)
    {
        StopAllCoroutines();
        _enemyToSpawn = enemySpawnData.Enemies;
        _maxEnemies = enemySpawnData.MaxEnemies;
        _stageLevel = enemySpawnData.StageLevel;
        _spawnInterval = enemySpawnData.SpawnInterval;
        StartCoroutine(SpawnEnemy());
    }


    private IEnumerator SpawnEnemy()
    {
        List<Vector3> AvailableSpawnPoints = new List<Vector3>();
        while (true)
        {
            if (_enemies.Count >= _maxEnemies)
            {
                yield return new WaitForSeconds(_spawnInterval);
                continue;
            }
            AvailableSpawnPoints.Clear();

            foreach (var spawnPoint in _localSpawnPoint)
            {
                if (Physics.Raycast(spawnPoint.GetPos() + _playerAttack.transform.position, Vector3.down, out RaycastHit hit, 3f, LayerMask.GetMask("Ground")))
                {
                    AvailableSpawnPoints.Add(hit.point);
                }
            }

            Vector3 randomPos = AvailableSpawnPoints.GetRandomValue();

            string enemyKey = _enemyToSpawn.GetRandomValue();
            EnemyData randomEnemyData = CSVParser.Instance.EnemyDatas[enemyKey];
            GameObject randomEnemyPrf = randomEnemyData.Prefab;

            if (_playerAttack == null)
                Debug.LogWarning($"missing PlayerAttack{gameObject.name}");

            Vector3 relativeRandomPos = randomPos;

            GameObject enemy = Instantiate(randomEnemyPrf, relativeRandomPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(randomEnemyData).SetLevel(_stageLevel);
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
