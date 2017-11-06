using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// tiles changing
// add tile
// destroy tile
// convert worldpasta to block

public class InfluenceMap : MonoBehaviour {

	// the world unit length and width of our game
	private int gridWidth = 40;
	private int gridLength = 40;

	//y,x for efficiency I thenk 
	InfluenceNode[][] influenceMap;


	void Start(){
		influenceMap = new InfluenceNode[gridWidth << 1][gridLength << 1];
	}


	public void addNode(Vector3 nodePos, int type){
		Vector2 gridLoc = worldPosToGrid(nodePos);

		spreadInfluence(false, type, gridLoc);
	}

	public void delNode(Vector3 nodePos, int type){
		Vector2 gridLoc = worldPosToGrid(nodePos);

		spreadInfluence(true, type, gridLoc);
	}


	public Vector2 worldPosToGrid(Vector3 worldPos){
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

		return new Vector2(x,y);
	}



	public InfluenceNode getInfluenceNode(Vector3 position){
		Vector2 conVec = worldPosToGrid(position);

		return influenceMap[conVec.y][conVec.x];
	}

	// these numbers need to be play tested; for now I'm just picking some
	// arbitrary shtuff cuz we r in finish 'em mode - Chris
	private void spreadInfluence(bool delInfluence, int type, Vector2 center){
		int radius = 3;
		int decayFactor = 75;
		int threatVal = 100;
		int spread;

		if(delInfluence) {
			threatVal = -threatVal;
		}

		for(int i = -3; i < radius; i++){
			if(((center.y + i) < influenceMap.Length) && ((center.y + i) > 0)) {
				influenceMap[center.y+i][center.x].threats[type] += threatVal * (decayFactor * i);
				for(spread = (Mathf.Abs(radius - i)); spread > 0; spread--){
					if((center.x + spread) < influenceMap.Length) {
						influenceMap[center.y+i][center.x+spread].threats[type] += threatVal * (decayFactor * spread);
					}
					if((center.x - spread) > 0){
						influenceMap[center.y+i][center.x-spread].threats[type] += threatVal * (decayFactor * spread);
					}
				}
				
			}
		}

	}

}