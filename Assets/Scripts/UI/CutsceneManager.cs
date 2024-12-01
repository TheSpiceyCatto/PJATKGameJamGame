using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private float transitionTime = 20f;
    [SerializeField] private Sprite[] cutsceneImages;
    private Image image;
    private int cutsceneIndex;

    private void Start() {
        image = GetComponent<Image>();
        cutsceneIndex = 0;
        NextSlide();
    }

    private void NextSlide() {
        if (cutsceneIndex < cutsceneImages.Length) {
            StartCoroutine(CutsceneTransition());
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    private IEnumerator CutsceneTransition() {
        image.sprite = cutsceneImages[cutsceneIndex];
        cutsceneIndex++;
        yield return new WaitForSeconds(transitionTime);
        NextSlide();
    }
}
