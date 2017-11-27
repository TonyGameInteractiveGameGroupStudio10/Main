using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The Arrow Prefab Script, inhertance from WeaponType
public class WeaponArrow : WeaponType {

	// Stats
	////////////////
	public float speed = 1f;
	public Vector2 firingDirection;
	public bool hasBounced;

	// Effects
	////////////////
	public bool poison;
	public bool vine;
	public bool shock;
	public bool quaking;
	public bool ricochet;
	public GameObject quakePrefab;
	public GameObject vinePrefab;


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
	void OnTriggerEnter2D(Collider2D coll){
		if ((coll.gameObject.tag == "Wall") || (coll.gameObject.tag == "DropWall")) {
			// If this isn't a ricochet
			if (hasBounced == false){
				this.QuakingEffect();
			}
			Destroy(gameObject);
		}
		else if (coll.gameObject.tag == "Enemy"){
			// Send Damage
			coll.gameObject.SendMessage("TakeDamage", 10);
			// Apply Poison Effect
			this.PoisonEffect(coll.gameObject);
			// Apply Stun Effect
			this.ShockEffect(coll.gameObject);
			// If this isnt a ricochet
			if (hasBounced == false){
				// Apply Quaking
				this.QuakingEffect();
				// Apply Vine
				this.VineEffect();
			}
			// Apply Ricochet
			this.RicochetEffect();
			// Destroy Self
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
			Instantiate(vinePrefab,transform.position,Quaternion.identity);
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
			// grab new directiosn
			float randomX = Random.Range(-1.0f, 1.0f);
			float randomY = Random.Range(-1.0f, 1.0f);
			// set firing direction and position
			Vector3 firingDirection = new Vector3(randomX, randomY, 1);
			Vector3 firingPosition = new Vector3(transform.position.x+randomX, transform.position.y+randomY, 1);
			// shoot object and rotate
			GameObject ricochetClone = Instantiate(gameObject,firingPosition,Quaternion.identity);
			ricochetClone.GetComponent<WeaponArrow>().SetFiringDirection(firingDirection);
			ricochetClone.GetComponent<WeaponArrow>().SetBounce(true);
			ricochetClone.transform.rotation = Quaternion.LookRotation(new Vector3(0,0,1), firingDirection);

		}
	}

	public void SetBounce(bool newBool){
		this.hasBounced = newBool;
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
		this.hasBounced = false;
	}
}