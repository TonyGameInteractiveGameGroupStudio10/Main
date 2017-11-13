using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode {

    public delegate bool Decision();
    public delegate void Action();

    private TreeNode right;
    private TreeNode left;
    private Decision theDecision;
    private Action theAction;

    // Builder
    public TreeNode(){
        right = null;
        left = null;
        theDecision = null;
        theAction = null;
    }

    public void SetLeft(TreeNode newLeft){
        left = newLeft;
    }

    public void SetRight(TreeNode newRight){
        right = newRight;
    }

    public void SetDecision(Decision newDecision){
        theDecision = newDecision;
    }

    public void SetAction(Action newAction){
        theAction = newAction;
    }

    // Decisions
    public bool Decide(){
		return theDecision();
    }

    public void GoLeft(){
        left.Search();
    }

    public void GoRight(){
        right.Search();
    }

    public void Search(){
        if(theAction != null) {
            theAction();
        }          
        else if(Decide()) {
            this.GoRight();
        }   
        else{
            this.GoLeft();
        }

    }
}