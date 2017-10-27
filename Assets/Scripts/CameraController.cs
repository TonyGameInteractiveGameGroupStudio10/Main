using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera Controller
public class CameraController : MonoBehaviour {

    public GameObject player;
    private Camera cameraComp;

    void Start() {
    	cameraComp = GetComponent<Camera>();
    }
    
    void Update () {
		if (Input.GetKey(KeyCode.E)){
			this.transform.position = new Vector3(0,0, -10);
			cameraComp.orthographicSize = 11;
		} else{
			this.transform.position = player.transform.position;
			cameraComp.orthographicSize = 5;
		}
	}
}