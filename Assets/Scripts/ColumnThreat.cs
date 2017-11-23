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
		float middley = (((rend.bounds.max.y-0.5f) + (rend.bounds.min.y+0.5f))/2);
        float middlex = (((rend.bounds.max.x-1f) + (rend.bounds.min.x+1f))/2);
        Vector3 middleRight = new Vector3(rend.bounds.max.x-1f,middley,0);
        Vector3 middleLeft = new Vector3(rend.bounds.min.x+1f,middley,0);
        Vector3 middleTop = new Vector3(middlex,rend.bounds.max.y-0.5f,0);
        Vector3 middleBottom = new Vector3(middlex,rend.bounds.min.y+0.5f,0);
        Vector3 bottomLeft = new Vector3(rend.bounds.min.x+1f, rend.bounds.min.y+0.5f, 0);
        Vector3 bottomRight = new Vector3(rend.bounds.max.x-1f, rend.bounds.min.y+0.5f, 0);

        gameMaster.GetComponent<InfluenceMap>().addNode(topLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(topRight, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleRight, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleTop, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleBottom, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottomLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottomRight, 0);
    }
}