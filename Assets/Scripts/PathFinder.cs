using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The enemies ask for a path.
// Threat Types: 0 - wall; 1- fire ; 2 - poison; 3 - oil
// Direction Types: 0 - north ; 2 - east; 3 - south; 4 - west

// GetAdjecent (current, direction(0-4))
// array of threats getThreat() size 4

// NEEDS WORK: FIND SMALLEST
public class PathFinder : MonoBehaviour {

    // Player Info
    private GameObject player;
    private Vector3 playerLocation;

    // Path Lists
    private List<Vector3> openList = new List<Vector3>();
    private List<Vector3> closeList = new List<Vector3>();

    // Unity Methods
    ////////////////
    void start(){
        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Look for the players location every second
        InvokeRepeating("findPlayer", 0f, 1f);
    }

    // Methods
    ////////////////
    // Path Finding
    ////////////////
    public Vector3[] FindPath(Vector3 currentVector){
        bool pathFound = false;
        Vector3 currentLocation = currentVector;
        Vector3 smallestLocation;

        Vector3 north;
        Vector3 east;
        Vector3 south;
        Vector3 west;

        // Add starting position to open list
        openList.Add(currentLocation);
        do {
            // Find the smallest vector/tile in open list
            smallestLocation = FindSmallest();
            // Make the smallest the current location
            currentLocation = smallestLocation;
            // Add current location to the closed list
            closedList.Add(currentLocation);
            // Remove current location from the open list
            openList.Remove(currentLocation);
            // Check to see if we arrived at destination
            if ((currentLocation.x > playerLocation.x-0.5) && (currentLocation.x < playerLocation.x+0.5)){
                if  ((currentLocation.y > playerLocation.y-0.5) && (currentLocation.y > playerLocation.y+0.5)) {
                    pathFound = true;
                    break;
                }
            }
            // retreive all adjacent squares
            north = new Vector3(currentLocation.x,currentLocation.y+0.5,0);
            south = new Vector3(currentLocation.x,currentLocation.y-0.5,0);
            east = new Vector3(currentLocation.x+0.5,currentLocation.y,0);
            west = new Vector3(currentLocation.x-0.5,currentLocation.y,0);


            // for loop through it adjacent square
                // if it is already in the closed list, or cant go, ignore it
                // if it not in the open list, compute the score, add it to open list
                // else if its already in the open list, check to see if its a better path
         // Continue until there is no more available square in the open list (which means there is no path)
        } while (openList.Count > 0);
        if (pathFound == true){
            // if we found the path follow it backwards
        }
        else{
            // wrekt something
        }
    }

    private Vector3 FindSmallest(){
        int listLength = openList.Count;
        int smallestWeight;
        Vector3 currentSmallest;

        currentSmallest = openList.Item[0];

        for (int i = 1; i < openList.Count; i += 1){
            contenderWeight = TileWeight(openList.Item[i], TILE CALL);
            if (smallestWeight > contenderWeight){
                smallestWeight = contenderWeight;
                currentSmallest = openList.Item[i];
            }
        }
        return currentSmallest;
    }

    // Calculating Weights
    ////////////////
    private int TileWeight(Vector3 currentVector, var Tile){
        int h = this.DistanceToPlayer(currentVector);
        int f = this.ThreatWeight(Tile);
        return f+h;
    }

    private int DistanceToPlayer(Vector3 currentVector){
        int xDifference = (int)Mathf.Round(playerLocation.x-currentVector.x);
        int yDifference = (int)Mathf.Round(playerLocation.y-currentVector.y);
        return (xDifference+yDifference);
    }

    private int ThreatWeight(){

    }

    // Player
    ////////////////
    private void findPlayer(){
        playerLocation = player.transform.position;
    }

    // Nested class for the paths
    ////////////////s
    private class Paths{
        public Vector3 location;
        public Vector3 parent;
    }
}