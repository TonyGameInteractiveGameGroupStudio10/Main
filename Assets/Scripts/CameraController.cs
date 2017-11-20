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
		if (Input.GetKey(KeyCode.F)){
			this.transform.position = new Vector3(0,0, -10);
			cameraComp.orthographicSize = 11;
		} else{
			float playerx = player.transform.position.x;
			float playery = player.transform.position.y;
			this.transform.position = new Vector3 (playerx, playery, -10);
			cameraComp.orthographicSize = 5;
		}
	}
}