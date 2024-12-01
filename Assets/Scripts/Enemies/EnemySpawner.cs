using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private enemy[] enemies;
    public static int EnemyCount;

    [Serializable]
    private struct enemy {
        public GameObject enemies;
        public int amount;
    }

    private void Awake() {
        GameEventManager.OnCutsceneEnd += InstantiateEnemies;
    }

    private void OnDestroy()
    {
        GameEventManager.OnCutsceneEnd -= InstantiateEnemies;
    }
    
    private void Start()
    {
        foreach (var enemyType in enemies) {
            EnemyCount += enemyType.amount;
        }
    }

    public static void DecreaseCount() {
        EnemyCount -= 1;
        if (EnemyCount <= 0) {
            GameEventManager.EnemiesDefeated();
        }
    }


    private void InstantiateEnemies() {
        foreach (var enemyType in enemies) {
            for (int i = 0; i < enemyType.amount; i++) {
                Instantiate(enemyType.enemies, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
