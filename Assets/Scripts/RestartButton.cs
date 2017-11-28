using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
	// The restart Button
    public Button button;

    void Start(){
    	Button btn = button.GetComponent<Button>();
    	btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
    	SceneManager.LoadScene("GamePlayScene", LoadSceneMode.Single);
    }
}