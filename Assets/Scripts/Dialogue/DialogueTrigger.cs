using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private BoxCollider2D _box;

    private void Start() {
        _box = GetComponent<BoxCollider2D>();
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        TriggerDialogue();
        _box.enabled = false;
    }
}
