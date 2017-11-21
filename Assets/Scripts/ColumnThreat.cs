using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Updated Threat Map for Colums
public class ColumnThreat : MonoBehaviour {

    // Game Master for access to the influence map
    GameObject gameMaster;

    // Find the Game Master
    void Start(){
        gameMaster = GameObject.FindWithTag("GameMaster");

        transform.position = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(transform.position);

        Renderer rend = GetComponent<Renderer>();
        Vector3 topLeft = new Vector3(rend.bounds.min.x+1f, rend.bounds.max.y-0.5f, 0);
        Vector3 topRight = new Vector3(rend.bounds.max.x-1f, rend.bounds.max.y-0.5f, 0);
		float middle = (((rend.bounds.max.y-0.5f) + (rend.bounds.min.y+0.5f))/2);
        Vector3 middleRight = new Vector3(rend.bounds.max.x-1f,middle,0);
        Vector3 middleLeft = new Vector3(rend.bounds.min.x+1f,middle,0);
        Vector3 bottomLeft = new Vector3(rend.bounds.min.x+1f, rend.bounds.min.y+0.5f, 0);
        Vector3 bottomRight = new Vector3(rend.bounds.max.x-1f, rend.bounds.min.y+0.5f, 0);

        gameMaster.GetComponent<InfluenceMap>().addNode(topLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(topRight, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleRight, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottomLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottomRight, 0);
    }
}