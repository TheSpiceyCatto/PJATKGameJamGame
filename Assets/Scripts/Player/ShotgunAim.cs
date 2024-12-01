using System;
using Managers;
using UnityEngine;

public class ShotgunAim : MonoBehaviour
{
    private bool playerDead = false;

    private void Awake() {
        PlayerEventManager.OnDeath += PlayerDeath;
    }

    private void OnDestroy() {
        PlayerEventManager.OnDeath -= PlayerDeath;
    }

    private void Update()
    {
        if (playerDead)
            return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void PlayerDeath() {
        playerDead = true;
    }
}