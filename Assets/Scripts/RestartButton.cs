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
    // The main menu button
    public Button mainButton;

    void Start(){
        if (tutorialButton != null){
            tutorialButton.GetComponent<Button>().onClick.AddListener(TutorialClick);
        }
        if (restartButton != null){
            restartButton.GetComponent<Button>().onClick.AddListener(RestartClick);
        }
        if (mainButton != null){
            mainButton.GetComponent<Button>().onClick.AddListener(MainMenuClick);
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

    void MainMenuClick(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void QuitClick(){
    	Application.Quit();
    }
}