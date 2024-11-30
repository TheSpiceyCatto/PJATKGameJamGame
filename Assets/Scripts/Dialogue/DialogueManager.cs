using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;
    
    public Text titleText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool(IsOpen, true);
        titleText.text = dialogue.title;
        _sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void Update(){
        if (InputManager.Talk){
            Debug.Log("talk:" + InputManager.Talk);
            DisplayNextSentence();
        }
    }
    

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (var letter in sentence)
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool(IsOpen, false);
    }
}
