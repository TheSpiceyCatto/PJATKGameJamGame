using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ProximitySpawner : MonoBehaviour
{
    [SerializeField] private enemy[] enemies;
    [SerializeField] private float timeBetweenSpawns = 2f;

    [Serializable]
    private struct enemy {
        public GameObject enemies;
        public int amount;
    }
    
    private void Start()
    {
        foreach (var enemyType in enemies) {
            EnemySpawner.EnemyCount += enemyType.amount;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(InstantiateEnemies());
        }
    }

    private IEnumerator InstantiateEnemies() {
        foreach (var enemyType in enemies) {
            for (int i = 0; i < enemyType.amount; i++) {
                Instantiate(enemyType.enemies, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
        Destroy(gameObject);
    }
}
