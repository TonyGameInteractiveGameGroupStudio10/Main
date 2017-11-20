using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The enemies ask for a path.
public class PathFinder : MonoBehaviour {

    // Player Info
    private GameObject player;
    private Vector3 playerLocation;

    // Threat Map access
    private GameObject gameMaster;
    private InfluenceMap threatMap;

    // Path Lists
    private List<Path> openList = new List<Path>();
    private List<Path> closedList = new List<Path>();

    // Methods
    ////////////////
    // Path Finding
    ////////////////
    public List<Vector3> FindPath(Vector3 startingVector, int monsterType){
        this.RunSetup();
        // Data Set up
        bool pathFound = false;
        Vector3 currentVector = startingVector;
        Vector3[] adjacentVectors = new Vector3[4];
        Path adjacentPath;
        Path smallestPath;
        Path currentPath = new Path(currentVector, null, TileWeight(currentVector, monsterType));

        // Add starting path to open list
        openList.Add(currentPath);
        do {
            // Find the smallest vector/tile in open list
            smallestPath = FindSmallest();
            // Make the smallest the current location
            currentPath = smallestPath;
            // Add current location to the closed list
            closedList.Add(currentPath);
            // Remove current location from the open list
            openList.Remove(currentPath);

            // Check to see if we arrived at destination
            currentVector = smallestPath.GetPosition();
            if ((currentVector.x > playerLocation.x-0.5f) && (currentVector.x < playerLocation.x+0.5f)){
                if  ((currentVector.y > playerLocation.y-0.5f) && (currentVector.y < playerLocation.y+0.5f)) {
                    pathFound = true;
                    break;
                }
            }

            // retreive all adjacent squares
			adjacentVectors[0] = new Vector3(currentVector.x,currentVector.y+0.5f,0);
			adjacentVectors[1] = new Vector3(currentVector.x,currentVector.y-0.5f,0);
			adjacentVectors[2] = new Vector3(currentVector.x+0.5f,currentVector.y,0);
			adjacentVectors[3] = new Vector3(currentVector.x-0.5f,currentVector.y,0);

            // for loop through the adjacent square
            for (int i = 0; i < 4; i += 1){
                // check if its out of bounds
				if ((adjacentVectors[i].x > -20) && (adjacentVectors[i].x < 20) && (adjacentVectors[i].y > -20) && (adjacentVectors[i].y < 20)){
					int adjacentScore = TileWeight(adjacentVectors[i], monsterType);
                    // Verify we want to go there (wall)
                    if (adjacentScore < 1000){
						adjacentPath = new Path(adjacentVectors[i],currentPath,adjacentScore);
                        // Check the closed list for it, if found ignore it
                        if (closedList.Find(x => x.GetPosition() == adjacentPath.GetPosition()) == null){
                            // Check the open list for it, if found ignore it
                            if (openList.Find(x => x.GetPosition() == adjacentPath.GetPosition()) == null){
                                openList.Add(adjacentPath);
                            }
                        }
                    }
                }
            }
         // Continue until there is no more available square in the open list (which means there is no path)
        } while (openList.Count > 0);
        // If a path was found
        if (pathFound == true){
            List<Vector3> thePath = new List<Vector3>();
            // Follow it backwards
            while(currentPath.GetParent() != null){
                thePath.Add(currentPath.GetPosition());
                currentPath = currentPath.GetParent();
            }
            // Reverse it
            thePath.Reverse();
            // Return it
            return thePath;
        }
        // If no path was found
        else{
            // rekt something
            return null;
        }
    }

    private Path FindSmallest(){
        // Save the list length
        int listLength = openList.Count;
        // Holder variables
        Path currentSmallest;
        int smallestWeight;
        int contenderWeight;
        // Assign the first value to smallest
        currentSmallest = openList[0];
        smallestWeight = currentSmallest.GetScore();
        // Loop Through openList finding the smallest tile weight
        for (int i = 1; i < openList.Count; i += 1){
            contenderWeight = openList[i].GetScore();
            if (smallestWeight > contenderWeight){
                smallestWeight = contenderWeight;
                currentSmallest = openList[i];
            }
        }
        return currentSmallest;
    }

    // Calculating Weights
    ////////////////
    private int TileWeight(Vector3 currentVector, int monsterType){
        InfluenceNode tempNode = threatMap.getInfluenceNode(currentVector); 
        int[] tile = tempNode.getThreat();
        int h = this.DistanceToPlayer(currentVector);
        int f = this.ThreatWeight(tile, monsterType);
        return (f+h);
    }

    private int DistanceToPlayer(Vector3 currentVector){
        int xDifference = (int)Mathf.Abs(playerLocation.x-currentVector.x);
        int yDifference = (int)Mathf.Abs(playerLocation.y-currentVector.y);
        return (xDifference+yDifference);
    }

    private int ThreatWeight(int[] tile, int monsterType){
        // Threat Types: 0 - wall; 1- fire ; 2 - poison; 3 - oil
        int threatCounter = 0;

        // Wall
        if (tile[0] > 99){ threatCounter += 1000; }

        // Fire
        if (monsterType != 1){
            if (tile[1] > 99){ threatCounter += 6; }
            else if (tile[1] > 49){ threatCounter += 2; }
            else if (tile[1] > 24){ threatCounter += 1; }
        }

        // Poison
        if (monsterType != 2){
            if (tile[2] > 99){ threatCounter += 6; }
            else if (tile[2] > 49){ threatCounter += 3; }
            else if (tile[2] > 24){ threatCounter += 1; }
        }
        

        // Oil
        if (monsterType != 3){
            if (tile[3] > 99){ threatCounter += 3; }
            else if (tile[3] > 49){ threatCounter += 1; }
            else if (tile[3] > 24){ threatCounter += 1; }
        }
        
        return threatCounter;
    }

    // Setup
    ///////////////////
    private void RunSetup(){
        // Verify the setup
        if (this.player == null){
            player = GameObject.FindWithTag("Player");
        }
        if (this.gameMaster == null){
            gameMaster = GameObject.FindWithTag("GameMaster");
        }
        if (this.threatMap == null){
            threatMap = gameMaster.GetComponent<InfluenceMap>();
        }
        this.playerLocation = player.transform.position;
        openList = new List<Path>();
        closedList = new List<Path>();
        return;
    }
}