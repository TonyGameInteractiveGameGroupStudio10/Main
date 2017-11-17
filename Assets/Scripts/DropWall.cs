using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWall : MonoBehaviour {
    // Game Master for access to the influence map
    GameObject gameMaster;

    // Find the Game Master
    void Start(){
        gameMaster = GameObject.FindWithTag("GameMaster");
    }

    // Destroy Ones Self
    public void DestroySelf(){
        gameMaster.GetComponent<InfluenceMap>().delNode(transform.position,0);
        Destroy(gameObject);
    }
}