using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
	// The restart button
    public Button restartButton;
    // The quit button
    public Button quitButton;

    void Start(){

  		quitButton.GetComponent<Button>().onClick.AddListener(QuitClick);
    }

    void RestartClick(){
    	SceneManager.LoadScene("GamePlayScene", LoadSceneMode.Single);
    }

    void QuitClick(){
    	Application.Quit();
    }
}