using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The enemies ask for a path.
// Threat Types: 0 - wall; 1- fire ; 2 - poison; 3 - oil
public class PathFinder : MonoBehaviour {

    // Player Info
    private GameObject player;
    private Vector3 playerLocation;

    // Threat Map access
    private GameObject gameMaster;
    private ThreatSCRIPT threatMap;

    // Path Lists
    private List<Path> openList = new List<Path>();
    private List<Path> closedList = new List<Path>();

    // Unity Methods
    ////////////////
    void start(){
        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Find the threat map
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        threatMap = gameMaster.GetComponent<>();
        // Look for the players location every second
        InvokeRepeating("findPlayer", 0f, 1f);
    }

    // Methods
    ////////////////
    // Path Finding
    ////////////////
    public Vector3[] FindPath(Vector3 currentVector){
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
        closedList.Clear()

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
            adjacentSquares[0] = new Vector3(currentLocation.x,currentLocation.y+0.5,0);
            adjacentSquares[1] = new Vector3(currentLocation.x,currentLocation.y-0.5,0);
            adjacentSquares[2] = new Vector3(currentLocation.x+0.5,currentLocation.y,0);
            adjacentSquares[3] = new Vector3(currentLocation.x-0.5,currentLocation.y,0);

             // for loop through the adjacent square
            for (int i = 0; i < 4; i + 1){
                adjacentScore = TileWeight(adjacentSquare[i]);
                adjacentPath = new Path(adjacentSquare[i],currentPath,adjacentScore);
                // if it is already in closed list, ignore it
                if (!(closedList.Contains(adjacentPath))){
                    // if you cannot pass the square, ignore it
                    if (adjacentPath.GetScore > 1000){
                         // if its not in the open list, add it
                        if (!(openList.Contains(adjacentPath))){
                            openList.Add(adjacentPath);
                        }
                    }
                }
            }
         // Continue until there is no more available square in the open list (which means there is no path)
        } while (openList.Count > 0);
        if (pathFound == true){
            // if we found the path follow it backwards
        }
        else{
            // wrekt something
        }
    }

    private Path FindSmallest(){
        // Save the list length
        int listLength = openList.Count;
        // Holder variables
        Path currentSmallest;
        // Assign the first value to smallest
        currentSmallest = openList.Item[0];
        smallestWeight = currentSmallest.GetScore();
        // Loop Through openList finding the smallest tile weight
        for (int i = 1; i < openList.Count; i += 1){
            contenderWeight = openList.Item[i].GetScore;
            if (smallestWeight > contenderWeight){
                smallestWeight = contenderWeight;
                currentSmallest = openList.Item[i];
            }
        }
        return currentSmallest;
    }

    // Calculating Weights
    ////////////////
    private int TileWeight(Vector3 currentVector){
        int[] tile = threatMap.getInfluenceNode(currentVector).getThreat()
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

    }

    // Player
    ////////////////
    private void findPlayer(){
        playerLocation = player.transform.position;
    }
}