using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vine weapon effect
// - creates a circle of vines at hit location
public class WeaponVine : WeaponType {

	// Timer for the length of the vine
	private float vineTimer;

	void Start(){
		this.vineTimer = 0.5f;
	}

	void Update() {
		this.vineTimer -= Time.deltaTime;
		if (vineTimer <= 0){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Enemy"){
			// grab the transform of the collider
			Transform collTransform = coll.gameObject.transform;
			// save the position of the collider
			Vector2 oldPos = new Vector2(collTransform.position.x, collTransform.position.y);
			// save the position of the vine
			Vector2 vinePos = new Vector2(transform.position.x, transform.position.y);
			// move the collider from the old position to the vine position
			coll.gameObject.transform.position = Vector2.MoveTowards(oldPos, vinePos, 25 * Time.deltaTime);
		}
	}
}