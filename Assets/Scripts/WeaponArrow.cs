using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The Arrow Prefab Script, inhertance from WeaponType
public class WeaponArrow : WeaponType {

	// Stats
	////////////////
	public float speed = 1f;
	public Vector2 firingDirection;

	// Effects
	////////////////


	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Update
	////////////////
	void FixedUpdate(){
        GetComponent<Rigidbody2D>().velocity = firingDirection.normalized * speed;
        Destroy(gameObject, 7);
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

	// Firing Direction
	/////////////////
	public Vector2 GetFiringDirection(){
		return firingDirection;
	}

	public void SetFiringDirection(Vector2 newDirection){
		firingDirection = newDirection;
	}

	// Effects
	/////////////////

	// Resets
	/////////////////
	public void ResetWeaponArrow(){
		speed = 8f;
	}

}