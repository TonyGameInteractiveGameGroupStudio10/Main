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

    // Unity Methods
    ////////////////
    void start(){
       InvokeRepeating("findPlayer", 0f, 1f);
    }

    // Methods
    ////////////////
    // Path Finding
    ////////////////
    public List<Vector3> FindPath(Vector3 currentVector){
        // set up components, if they aren't set up
        if (player == null){
            player = GameObject.FindWithTag("Player");
        }
        if (gameMaster == null){
            gameMaster = GameObject.FindWithTag("GameMaster");
        }
        if (threatMap == null){
            threatMap = gameMaster.GetComponent<InfluenceMap>();
        }

        // Path hasn't been found
        bool pathFound = false;

        // Set up the first path
        Vector3 currentPosition = currentVector;
        Path currentParent = null;
        int currentScore = TileWeight(currentPosition);
        Path currentPath = new Path(currentPosition, currentParent, currentScore);

        // Stores the Smallest Location
        Path smallestLocation;

        // Stores for the adjacent squares
        // 0 - north; 1 - east; 2 - south; 3 - west
        Vector3[] adjacentSquares = new Vector3[4];
        Path adjacentPath;
        int adjacentScore;

        // Clear open and closed list
        openList.Clear();
		closedList.Clear();

        // Add starting path to open list
        openList.Add(currentPath);
        do {
            // Find the smallest vector/tile in open list
            smallestLocation = FindSmallest();
            // Make the smallest the current location
            currentPath = smallestLocation;
            // Add current location to the closed list
            closedList.Add(currentPath);
            // Remove current location from the open list
            openList.Remove(currentPath);

            // Check to see if we arrived at destination
            currentPosition = smallestLocation.GetPosition();
            if ((currentPosition.x > playerLocation.x-0.5) && (currentPosition.x < playerLocation.x+0.5)){
                if  ((currentPosition.y > playerLocation.y-0.5) && (currentPosition.y > playerLocation.y+0.5)) {
                    pathFound = true;
                    break;
                }
            }

            // retreive all adjacent squares
            adjacentSquares[0] = new Vector3(currentPosition.x,playerLocation.y+0.5f,0);
            adjacentSquares[1] = new Vector3(currentPosition.x,playerLocation.y-0.5f,0);
			adjacentSquares[2] = new Vector3(currentPosition.x+0.5f,playerLocation.y,0);
            adjacentSquares[3] = new Vector3(currentPosition.x-0.5f,playerLocation.y,0);

            // for loop through the adjacent square
            for (int i = 0; i < 4; i += 1){
                adjacentScore = TileWeight(adjacentSquares[i]);
                adjacentPath = new Path(adjacentSquares[i],currentPath,adjacentScore);
                // if it is already in closed list, ignore it
                if (!(closedList.Contains(adjacentPath))){
                    // if you cannot pass the square, ignore it
					if (adjacentPath.GetScore() > 1000){
                         // if its not in the open list, add it
                        if (!(openList.Contains(adjacentPath))){
                            openList.Add(adjacentPath);
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
            while(currentPath.GetParent().GetParent() != null){
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
    private int TileWeight(Vector3 currentVector){
        if (player == null) { Debug.Log("Player is null"); }
        if (threatMap == null) { Debug.Log("Threat Map is null"); }
        if (gameMaster == null) { Debug.Log("GameMaster is null"); }
        InfluenceNode tempNode = threatMap.getInfluenceNode(currentVector);
        if (tempNode == null) { Debug.Log("TempNode is null");} 
		int[] tile = tempNode.getThreat();
        int h = this.DistanceToPlayer(currentVector);
        int f = this.ThreatWeight(tile);
        return f+h;
    }

    private int DistanceToPlayer(Vector3 currentVector){
        int xDifference = (int)Mathf.Round(playerLocation.x-currentVector.x);
        int yDifference = (int)Mathf.Round(playerLocation.y-currentVector.y);
        return (xDifference+yDifference);
    }

    private int ThreatWeight(int[] tile){
        // Threat Types: 0 - wall; 1- fire ; 2 - poison; 3 - oil
        int threatCounter = 0;
        // Wall
        if(tile[0] > 0){
			threatCounter += 1000;
        }
        // Fire
        if(tile[1] > 0){
			threatCounter += 2;
        }	
        // Poison
        if(tile[2] > 0){
			threatCounter += 2;
        }
        // Oil
        if(tile[3] > 0){
			threatCounter += 1;
        }
        return threatCounter;
    }

    // Player
    ////////////////
    private void findPlayer(){
        playerLocation = player.transform.position;
    }
}