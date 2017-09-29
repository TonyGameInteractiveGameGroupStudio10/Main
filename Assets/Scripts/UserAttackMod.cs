using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAttackMod : MonoBehaviour {

	private weaponArrow newArrow;

	public weaponArrow ApplyMod(int[] buffs){
		newArrow = new weaponArrow();

		for (int i = 0; i < 5; i += 1){
			// Apply the effects to the arrow
		}

		return newArrow;
	}
}