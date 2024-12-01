using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueTrigger))]
public class EndLevelDialogue : MonoBehaviour {
    private DialogueTrigger dialogueTrigger;
    private bool enemiesDead;

    private void Awake() {
        GameEventManager.OnEnemiesDefeated += TriggerDialogue;
        GameEventManager.OnCutsceneEnd += CutsceneEnded;
    }

    private void OnDestroy() {
        GameEventManager.OnEnemiesDefeated -= TriggerDialogue;
        GameEventManager.OnCutsceneEnd -= CutsceneEnded;
    }

    private void Start() {
        dialogueTrigger = gameObject.GetComponent<DialogueTrigger>();
    }

    private void TriggerDialogue() {
        enemiesDead = true;
        dialogueTrigger.TriggerDialogue();
    }

    private void CutsceneEnded() {
        if (enemiesDead) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
