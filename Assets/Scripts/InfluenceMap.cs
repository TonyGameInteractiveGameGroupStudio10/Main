using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceMap : MonoBehaviour {

	// the world unit length and width of our game
	// each block is 0.5 world units long
	private static int gridWidth = 40;
	private static int gridLength = 40;
	private static int scaledLength = gridLength * 2;
	private static int scaledWidth = gridWidth * 2;

	//y,x for efficiency I thenk 
	public static InfluenceNode[,] influenceMap = new InfluenceNode[scaledLength, scaledWidth];


	public void Start(){
		for(int i = 0; i < scaledLength; i++){
			for(int j = 0; j < scaledWidth; j++){
				influenceMap[i,j] = new InfluenceNode(0);
			}
		}
	}


	// changes the node at nodePos from 
	public void changeTile(Vector3 nodePos, int rType, int newType) {

	}


	public void addNode(Vector3 nodePos, int type){
		IntVector2 gridLoc = worldPosToGrid(nodePos);

		spreadInfluence(false, type, gridLoc);
	}

	public void delNode(Vector3 nodePos, int type){
		IntVector2 gridLoc = worldPosToGrid(nodePos);

		spreadInfluence(true, type, gridLoc);
	}


	// takes the world position and scales it to the nearest .5
	public Vector2 scaleWorldPos(Vector3 worldPos){
		
		Vector2 conVec = new Vector2(worldPos.x, worldPos.y);

		int x = (int) worldPos.x;
		int y = (int) worldPos.y;

		if(conVec.x < x+0.3f){
			conVec.x = (float) x;
		}
		else if (conVec.x > x+0.7f){
			conVec.x = x + 1.0f;
		}
		else{
			conVec.x = x + 0.5f;
		}

		if(conVec.y < y+0.3f){
			conVec.y = (float) y;
		}
		else if(conVec.y > y+0.7f){
			conVec.y = y + 1.0f;
		}
		else{
			conVec.y = y + 0.5f;
		}
		return conVec;
	}



	public IntVector2 worldPosToGrid(Vector3 worldPos){
		Vector2 conWorldPos = scaleWorldPos(worldPos);
		return new IntVector2(((int)(conWorldPos.x+20)*2),((int)(conWorldPos.y+20)*2));
	}


	
	public InfluenceNode getInfluenceNode(Vector3 position){
		IntVector2 conVec = worldPosToGrid(position);

		return influenceMap[conVec.y , conVec.x];
	}

	// these numbers need to be play tested; for now I'm just picking some
	// arbitrary shtuff cuz we r in finish 'em mode - Chris
	private void spreadInfluence(bool delInfluence, int type, IntVector2 center){
		int radius = 3;
		int decayFactor = 75;
		int threatVal = 100;
		int spread;

		if(delInfluence) {
			threatVal = -threatVal;
		}

		/*
		// walls don't spread influence for now
		if (type == 0){
			influenceMap[center.y, center.x].getThreat()[type] += threatVal;
			return;
		}
		*/

		for(int i = -3; i <= radius; i++) {
			if(((center.y + i) < scaledLength) && ((center.y + i) > 0)) {
				influenceMap[center.y+i , center.x].getThreat()[type] += threatVal * (decayFactor * i);
				for(spread = (radius - Mathf.Abs(i)); spread > 0; spread--){
					if((center.x + spread) < scaledWidth) {
						influenceMap[center.y+i , center.x+spread].getThreat()[type] += threatVal * (decayFactor * spread);
					}
					if((center.x - spread) > 0){
						influenceMap[center.y+i , center.x-spread].getThreat()[type] += threatVal * (decayFactor * spread);
					}
				}	
			}
		}
	}
}
