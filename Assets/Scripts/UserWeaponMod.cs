using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserWeaponMod : MonoBehaviour {

	public void ApplyMod(int[] buffs){
		// Apply modifiers from the buffs to the weapon module
		for (int i = 0; i < 5; i += 1){

		}
	}

	public void Fire(GameObject firingWeapon, Transform firingPosition){
		Instantiate(firingWeapon,firingPosition.position,Quaternion.identity);
	}
}