using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserWeaponMod : MonoBehaviour {

	// Attack Properties
	private float attackSpeed;
	private float attackCounter;
	private bool inAttackAnimation;

	// Vector Direction containers
	Vector2 firingDirection;
	GameObject attackClone;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		attackSpeed = 0.7f;
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
		// Apply modifiers from the buffs to the weapon module
		for (int i = 0; i < 5; i += 1){
		}
	}

	// Firing Weapon
	////////////////
	public void Fire(GameObject firingWeapon, Transform firingPosition){
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
		}
	}


}