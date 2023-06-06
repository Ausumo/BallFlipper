using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector2 _range;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _bonus;

    [SerializeField] private float _enemySpawnTime = 1.5f;
    [SerializeField] private float _bonusSpawnTime = 2.0f;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnBonus());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_enemySpawnTime);
        Vector2 spawnPos = transform.position + new Vector3(0, Random.Range(-_range.y, _range.y));
        Instantiate(_enemy, spawnPos, Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnBonus()
    {
        yield return new WaitForSeconds(_bonusSpawnTime);
        Vector2 spawnPos = transform.position + new Vector3(0, Random.Range(-_range.y, _range.y));
        Instantiate(_bonus, spawnPos, Quaternion.identity);
        StartCoroutine(SpawnBonus());
    }
}
