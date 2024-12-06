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
    public static EndLevelDialogue Instance { get; private set; }
    public bool isScene1 = true;

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

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

    public void TriggerDialogue() {
        enemiesDead = true;
        dialogueTrigger.TriggerDialogue();
    }
    
    public void DialogueCompleted() {
        if (isScene1)
        {
            GameEventManager.EndCutscene();
        }else
            GameEventManager.AscDialogueEnded();
    }

    private void CutsceneEnded() {
        if (enemiesDead) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
