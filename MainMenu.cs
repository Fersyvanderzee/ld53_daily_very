using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton(){
        SceneManager.LoadScene("MainGame");
    }

    public void TutorialButton(){
        SceneManager.LoadScene("Tutorial");
    }
}
