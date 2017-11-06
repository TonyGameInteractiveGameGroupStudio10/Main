using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceNode : MonoBehaviour {

	//0=wall, 1=fire, 2=poison, 3=oil 
	int[] threats;

	public InfluenceNode(int threat){
		for (int i = 0; i < threats.length; i++){
			threats[i] = threat;
		}
	}

	public void updateThreats(int type, int val){
		threats[type] += val;
	}

	// returns the threat value of the type given
	public int[] getThreat(){
		return threats;
	}
}