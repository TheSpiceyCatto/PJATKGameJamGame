using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private static bool _isPaused = false; //PAMIETAC O SPRAWDZANIU TEGO PRZY INNYCH ELEMENTACH!
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (_isPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }
    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void MainMenu(){
        _isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit(){
        Application.Quit();
    }
}
