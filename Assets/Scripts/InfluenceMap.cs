using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceMap : MonoBehaviour {

	// the world unit length and width of our game
	// each block is 0.5 world units long
	private static int gridWidth = 40;
	private static int gridLength = 40;

	//y,x for efficiency I thenk 
	public static InfluenceNode[,] influenceMap = new InfluenceNode[gridLength, gridWidth];


	public void Start(){
		for(int i = 0; i < gridLength; i++){
			for(int j = 0; j < gridWidth; j++){
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
		Vector2 conVec = new Vector2((float) Mathf.Round(worldPos.x), (float) Mathf.Round(worldPos.y));

		return conVec;
	}



	public IntVector2 worldPosToGrid(Vector3 worldPos){
		Vector2 conWorldPos = scaleWorldPos(worldPos);
		return new IntVector2(((int)(conWorldPos.x+20)),((int)(conWorldPos.y+20)));
	}


	
	public InfluenceNode getInfluenceNode(Vector3 position){
		IntVector2 conVec = worldPosToGrid(position);

		return influenceMap[conVec.y , conVec.x];
	}

	private void spreadInfluence(bool delInfluence, int type, IntVector2 center){
		int radius = 1;
		int decayFactor = 75;
		int threatVal = 100;
		int spread;

		if(delInfluence) {
			threatVal = -threatVal;
		}

		// walls don't spread influence for now
		if (type == 0){
			influenceMap[center.y, center.x].getThreat()[type] += threatVal;
			return;
		}

		influenceMap[center.y, center.x].getThreat()[type] += threatVal;
		for (int i = -radius; i <= radius; i += 1){
			if(((center.y + i) < gridLength) && ((center.y + i) > 0)) {
				for(spread = (radius - Mathf.Abs(i)); spread > 0; spread--){
					if((center.x + spread) < gridWidth) {
						influenceMap[center.y+i , center.x+spread].getThreat()[type] += threatVal/4;
					}
					if((center.x - spread) > 0){
						influenceMap[center.y+i , center.x-spread].getThreat()[type] += threatVal/4;
					}
				}		
			}
		}
		/**
		for(int i = -radius; i <= radius; i++) {
			if(((center.y + i) < gridLength) && ((center.y + i) > 0)) {
				influenceMap[center.y+i , center.x].getThreat()[type] += threatVal * (decayFactor * i);
				for(spread = (radius - Mathf.Abs(i)); spread > 0; spread--){
					if((center.x + spread) < gridWidth) {
						influenceMap[center.y+i , center.x+spread].getThreat()[type] += threatVal * (decayFactor * spread);
					}
					if((center.x - spread) > 0){
						influenceMap[center.y+i , center.x-spread].getThreat()[type] += threatVal * (decayFactor * spread);
					}
				}	
			}
		}
		*/
	}
}
