using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene("Level1");
    }

    public void HowToPlay(){
        SceneManager.LoadScene("HowToPlay");
    }
    public void Credits(){
        SceneManager.LoadScene("Credits");
    }

    public void Quit(){
        Application.Quit();
    }
}
