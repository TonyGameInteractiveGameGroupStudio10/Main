using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Weapon Module extension for the User Class
// - Applies all weapon modifiers
// - Handles the attack (Fire, etc)
public class UserWeaponMod : MonoBehaviour {

	// Stats
	////////////////
	private float startingAttackSpeed;
	private float attackSpeed;
	private float attackCounter;
	private bool inAttackAnimation;

	// Vector Containers
	////////////////
	Vector2 firingDirection;
	GameObject attackClone;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		startingAttackSpeed = 0.60f;
		attackSpeed = startingAttackSpeed;
		inAttackAnimation = false;
	}

	// Update
	////////////////
	void Update(){
		// Checks if the character is still locked in animation
		if (inAttackAnimation == true){
			attackCounter -= Time.deltaTime;
			if (attackCounter <= 0){
				inAttackAnimation = false;
			}
		}
	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Applying Modifiers
	///////////////
	public void ApplyMod(int[] buffs){
		float attackSpeedBuff = buffs[0] * 0.05f;
		float newAttackSpeed = startingAttackSpeed - attackSpeedBuff;
		if(newAttackSpeed < 0.45f){
			attackSpeed = 0.45f;
		} else{
			attackSpeed = newAttackSpeed;
		}
	}

	// Firing Weapon
	////////////////
	public bool Fire(GameObject firingWeapon, Transform firingPosition){
		// If the user isn't in the attack animation, then fire
		if (inAttackAnimation == false){
			// Find direction in which the mouse is pointed
			firingDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firingPosition.position).normalized;
			// Fire that direction
			firingWeapon.GetComponent<WeaponArrow>().SetFiringDirection(firingDirection);
			// Create weapon swing/strike
			attackClone = Instantiate(firingWeapon,firingPosition.position,Quaternion.identity);
			// Rotate object to correct direction
			attackClone.transform.rotation = Quaternion.LookRotation(new Vector3(0,0,1), firingDirection);

			// reset swing counter
			attackCounter = attackSpeed;
			inAttackAnimation = true;
			return true;
		}
		return false;
	}
}