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
		// Fire in direction of the mouse
		firingWeapon.GetComponent<WeaponArrow>().SetFiringDirection((Camera.main.ScreenToWorldPoint(Input.mousePosition) - firingPosition.position).normalized);
		// Create weapon swing/strike
		Instantiate(firingWeapon,firingPosition.position,Quaternion.identity);
	}
}