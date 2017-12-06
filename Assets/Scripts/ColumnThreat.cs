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
        Invoke("MapInfluence",2f);
    }

    private void MapInfluence(){
                Renderer rend = GetComponent<Renderer>();
        Vector3 topLeft = new Vector3(rend.bounds.min.x, rend.bounds.max.y-0.01f, 0);
        Vector3 topRight = new Vector3(rend.bounds.max.x, rend.bounds.max.y-0.01f, 0);
        float middley = (((rend.bounds.max.y) + (rend.bounds.min.y))/2);
        float middlex = (((rend.bounds.max.x) + (rend.bounds.min.x))/2);
        Vector3 middleRight = new Vector3(rend.bounds.max.x,middley,0);
        Vector3 middleLeft = new Vector3(rend.bounds.min.x,middley,0);
        Vector3 middleTop = new Vector3(middlex,rend.bounds.max.y-0.01f,0);
        Vector3 middleBottom = new Vector3(middlex,rend.bounds.min.y+0.01f,0);
        Vector3 middleMiddle = new Vector3(middlex,middley,0);
        Vector3 bottomLeft = new Vector3(rend.bounds.min.x, rend.bounds.min.y+0.01f, 0);
        Vector3 bottomRight = new Vector3(rend.bounds.max.x, rend.bounds.min.y+0.01f, 0); 

        /////////////////////////////////

        Debug.DrawLine(topRight,topLeft,Color.red,100f);
        Debug.DrawLine(middleRight,middleLeft,Color.red,100f);
        Debug.DrawLine(bottomRight,bottomLeft,Color.red,100f);
        Debug.DrawLine(middleTop,middleBottom,Color.red,100f);

        topLeft = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(topLeft);
        topRight = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(topRight);
        middleRight = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(middleRight);
        middleLeft = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(middleLeft);
        middleTop = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(middleTop);
        middleBottom = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(middleBottom);
        middleMiddle = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(middleMiddle);
        bottomRight = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(bottomRight);
        bottomLeft = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(bottomLeft);

        Vector3 upLine = new Vector3(topLeft.x,topLeft.y+0.5f,0);
        Vector3 downLine = new Vector3(topLeft.x,topLeft.y-0.5f,0);
        Vector3 rightLine = new Vector3(topLeft.x+0.5f,topLeft.y);
        Vector3 leftLine = new Vector3(topLeft.x-0.5f,topLeft.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(topRight.x,topRight.y+0.5f,0);
        downLine = new Vector3(topRight.x,topRight.y-0.5f,0);
        rightLine = new Vector3(topRight.x+0.5f,topRight.y);
        leftLine = new Vector3(topRight.x-0.5f,topRight.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(middleTop.x,middleTop.y+0.5f,0);
        downLine = new Vector3(middleTop.x,middleTop.y-0.5f,0);
        rightLine = new Vector3(middleTop.x+0.5f,middleTop.y);
        leftLine = new Vector3(middleTop.x-0.5f,middleTop.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(middleLeft.x,middleLeft.y+0.5f,0);
        downLine = new Vector3(middleLeft.x,middleLeft.y-0.5f,0);
        rightLine = new Vector3(middleLeft.x+0.5f,middleLeft.y);
        leftLine = new Vector3(middleLeft.x-0.5f,middleLeft.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(bottomRight.x,bottomRight.y+0.5f,0);
        downLine = new Vector3(bottomRight.x,bottomRight.y-0.5f,0);
        rightLine = new Vector3(bottomRight.x+0.5f,bottomRight.y);
        leftLine = new Vector3(bottomRight.x-0.5f,bottomRight.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(middleRight.x,middleRight.y+0.5f,0);
        downLine = new Vector3(middleRight.x,middleRight.y-0.5f,0);
        rightLine = new Vector3(middleRight.x+0.5f,middleRight.y);
        leftLine = new Vector3(middleRight.x-0.5f,middleRight.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(middleBottom.x,middleBottom.y+0.5f,0);
        downLine = new Vector3(middleBottom.x,middleBottom.y-0.5f,0);
        rightLine = new Vector3(middleBottom.x+0.5f,middleBottom.y);
        leftLine = new Vector3(middleBottom.x-0.5f,middleBottom.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(middleMiddle.x,middleMiddle.y+0.5f,0);
        downLine = new Vector3(middleMiddle.x,middleMiddle.y-0.5f,0);
        rightLine = new Vector3(middleMiddle.x+0.5f,middleMiddle.y);
        leftLine = new Vector3(middleMiddle.x-0.5f,middleMiddle.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        upLine = new Vector3(bottomLeft.x,bottomLeft.y+0.5f,0);
        downLine = new Vector3(bottomLeft.x,bottomLeft.y-0.5f,0);
        rightLine = new Vector3(bottomLeft.x+0.5f,bottomLeft.y);
        leftLine = new Vector3(bottomLeft.x-0.5f,bottomLeft.y);

        Debug.DrawLine(upLine,downLine,Color.blue,100f);
        Debug.DrawLine(leftLine,rightLine,Color.blue,100f);

        /////////////////////////////////////////

        gameMaster.GetComponent<InfluenceMap>().addNode(topLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(topRight, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleRight, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleTop, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleBottom, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(middleMiddle, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottomLeft, 0);
        gameMaster.GetComponent<InfluenceMap>().addNode(bottomRight, 0);
    }
}