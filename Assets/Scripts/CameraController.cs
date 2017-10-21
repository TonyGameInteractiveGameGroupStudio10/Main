using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera Controller
public class CameraController : MonoBehaviour {

    public GameObject player;
    
    void Update () {
		this.transform.position = player.transform.position;
	}
}