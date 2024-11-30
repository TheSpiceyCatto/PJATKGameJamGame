using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPoint : MonoBehaviour {
    private Transform player;
    private Vector3 mouse;

    private void Start() {
        player = GetComponentInParent<Transform>();
    }

    private void Update() { 
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = (Vector2)((mouse - player.position) * 0.5f);
    }
}
