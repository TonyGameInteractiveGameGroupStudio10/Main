using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class columnWall : MonoBehaviour {
    // Game Master for access to the influence map
    GameObject gameMaster;

    // Find the Game Master
    void Start(){
        gameMaster = GameObject.FindWithTag("GameMaster");
        Renderer rend = GetComponent<Renderer>();
        Vector3 topLeft = new Vector3(rend.bounds.min.x, rend.bounds.max.y, 0);
        Vector3 topRight = new Vector3(rend.bounds.max.x, rend.bounds.max.y, 0);
        Vector3 bottemLeft = new Vector3(rend.bounds.min.x, rend.bounds.min.y, 0);
        Vector3 bottemRight = new Vector3(rend.bounds.max.x, rend.bounds.min.y, 0);

        gameMaster.GetComponent<InfluenceMap>().addNode(topLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(topRight, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottemLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottemRight, 0);
    }
}