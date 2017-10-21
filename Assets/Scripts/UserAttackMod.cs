using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attack Module extension for the User Class
// - Holds the default Weapon Prefab
// - Applies all modifiers to the weapon Prefab
// - Returns the prefab to be stored in player
public class UserAttackMod : MonoBehaviour {

	// Prefab
	////////////////
	public GameObject weaponPrefab;

	// Apply Modifiers to the WeaponType script
	public GameObject ApplyMod(int[] buffs){
		// Grab weapon script attached to prefab object
		WeaponArrow newWeapon = weaponPrefab.GetComponent<WeaponArrow>();
		newWeapon.ResetWeaponArrow();

		// Apply modifiers from buffs to the script
		for (int i = 0; i < 5; i += 1){
			
		}
	
		return weaponPrefab;
	}
}