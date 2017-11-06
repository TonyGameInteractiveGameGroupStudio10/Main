using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceNode : Monobehavior {

	//0=wall, 1=fire, 2=poison, 3=oil 
	int[] threats;


	public void updateThreats(int type, int val){
		threats[type] += val;
	}

	// returns the threat value of the type given
	public int[] getThreat(){
		return threats;
	}
}