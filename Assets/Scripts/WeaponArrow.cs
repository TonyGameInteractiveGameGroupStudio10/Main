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
	public bool poison;
	public bool vine;
	public GameObject vinePrefab;
	public bool shock;
	public bool quaking;
	public GameObject quakePrefab;
	public bool ricochet;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Update
	////////////////
	void FixedUpdate(){
        this.GetComponent<Rigidbody2D>().velocity = firingDirection.normalized * speed;
        Destroy(gameObject, 7);
	}

	// Collision
	////////////////
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Wall"){
			this.QuakingEffect();
			Destroy(gameObject);
		}
		else if (coll.gameObject.tag == "Enemy"){
			coll.gameObject.SendMessage("TakeDamage", 10);
			this.PoisonEffect(coll.gameObject);
			this.VineEffect();
			this.ShockEffect(coll.gameObject);
			this.QuakingEffect();
			this.RicochetEffect();
			Destroy(gameObject);
		}
	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Effects
	/////////////////
	public void SetAttackEffects(int buffArrayIndex, int buffValue){
		if ((buffArrayIndex == 0) && (buffValue > 0)){
			poison = true;
		}
		if ((buffArrayIndex == 1) && (buffValue > 0)){
			vine = true;
		}
		if ((buffArrayIndex == 2) && (buffValue > 0)){
			shock = true;
		}
		if ((buffArrayIndex == 3) && (buffValue > 0)){
			quaking = true;
		}
		if ((buffArrayIndex == 4) && (buffValue > 0)){
			ricochet = true;
		}
	}

	private void PoisonEffect(GameObject target){
		if (this.poison == true){
			target.GetComponent<MonsterClass>().ReceivingPoison();
		}
	}

	private void VineEffect(){
		if (this.vine == true){
			// create vine prefab
		}
	}

	private void ShockEffect(GameObject target){
		if (this.shock == true){
			target.GetComponent<MonsterClass>().ReceivingStun();
		}
	}

	private void QuakingEffect(){
		if (this.quaking == true){
			Instantiate(quakePrefab,transform.position,Quaternion.identity);
		}
	}

	private void RicochetEffect(){
		if (this.ricochet == true){
			// create arrow prefab
		}
	}

	// Speed
	/////////////////
	public float GetSpeed(){
		return this.speed;
	}

	public void SetSpeed(float newSpeed){
		this.speed = newSpeed;
	}

	public void AddSpeed(float addedSpeed){
		this.speed += addedSpeed;
	}

	public void MinusSpeed(float minusSpeed){
		this.speed -= minusSpeed;
	}

	// Firing Direction
	/////////////////
	public Vector2 GetFiringDirection(){
		return this.firingDirection;
	}

	public void SetFiringDirection(Vector2 newDirection){
		this.firingDirection = newDirection;
	}

	// Resets
	/////////////////
	public void ResetWeaponArrow(){
		this.speed = 8f;
		this.poison = false;
		this.vine = false;
		this.shock = false;
		this.quaking = false;
		this.ricochet = false;
	}

}