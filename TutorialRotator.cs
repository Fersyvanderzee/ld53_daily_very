using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialRotator : MonoBehaviour
{
    public Image img;
    public int tutNum;

    void Start(){
        tutNum = 1;
    }

    void Update(){
        img.sprite = Resources.Load<Sprite>("tut_" + tutNum);
    }

    public void ClickButtonToIncrease(){
        if(tutNum < 8){
            tutNum += 1;
        }
    }

    public void ClickButtonToDecrease(){
        if(tutNum > 1){
            tutNum -= 1;
        }
    }

    public void ClickBackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
