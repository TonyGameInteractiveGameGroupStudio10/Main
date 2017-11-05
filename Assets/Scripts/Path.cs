using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data type for paths
public class Path {

    // The Vector position
    private Vector3 vectorPosition;
    // Parent of the vector
    private Path parent;
    // The score of the vector
    private int vectorScore;

    public Path(Vector3 position, Path par, int score){
        vectorPosition = position;
        parent = par;
        vectorScore = score;
    }

    // Position
    ////////////
    public Vector3 GetPosition(){
        return vectorPosition;
    }

    public void SetPosition(Vector3 newPosition){
        vectorPosition = newPosition
    }

    // Parent
    ////////////
    public Path GetParent(){
        return parent;
    }

    public void SetParent(Path parent){
        parent = newParent
    }

    // Score
    ////////////
    public int GetScore(){
        return vectorScore;
    }

    public void SetScore(int newScore){
        vectorScore = newScore;
    }


}