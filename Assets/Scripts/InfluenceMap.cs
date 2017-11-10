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


	public IntVector2 worldPosToGrid(Vector3 worldPos){
		int x, y;

		if(worldPos.x < 0) {
			x = (int) (Mathf.Abs(worldPos.x * 2));
		} else {
			x = (int) (worldPos.x * 4);
		}

		if(worldPos.y < 0) {
			y = (int) (Mathf.Abs(worldPos.x * 2));
		} else {
			y = (int) (worldPos.y * 4);
		}

		return new IntVector2(x,y);
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
