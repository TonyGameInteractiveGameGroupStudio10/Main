using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
    // Tutorial Button
    public Button tutorialButton;
	// The restart/play button
    public Button restartButton;
    // The quit button
    public Button quitButton;

    void Start(){
        if (tutorialButton != null){
            tutorialButton.GetComponent<Button>().onClick.AddListener(TutorialClick);
        }
        if (restartButton != null){
            restartButton.GetComponent<Button>().onClick.AddListener(RestartClick);
        }
        if (quitButton != null){
            quitButton.GetComponent<Button>().onClick.AddListener(QuitClick);
        }
    }

    void TutorialClick(){
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }

    void RestartClick(){
    	SceneManager.LoadScene("GamePlayScene", LoadSceneMode.Single);
    }

    void QuitClick(){
    	Application.Quit();
    }
}