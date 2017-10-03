using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArrow : WeaponType {

	// Stats
	////////////////
	public float speed = 1f;

	// Effects
	////////////////


	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Update
	////////////////
	void FixedUpdate(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(1,0) * speed;
	}

	// Collision
	////////////////
	void OnCollisionEnter2D(Collision2D coll){

	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Speed
	/////////////////
	public float GetSpeed(){
		return speed;
	}

	public void SetSpeed(float newSpeed){
		speed = newSpeed;
	}

	public void AddSpeed(float addedSpeed){
		speed += addedSpeed;
	}

	public void MinusSpeed(float minusSpeed){
		speed -= minusSpeed;
	}

	// Effects
	/////////////////

	// Resets
	/////////////////
	public void ResetWeaponArrow(){
		speed = 1f;
	}

}