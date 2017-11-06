using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Updated Threat Map for Colums
public class ColumnThreat : MonoBehaviour {

    // Add this wall to the threat map
    void Start(){
        GameObject.FindWithTag("GameMaster").GetComponent<InfluenceMap>().addNode(transform.position, 0);
    }

}