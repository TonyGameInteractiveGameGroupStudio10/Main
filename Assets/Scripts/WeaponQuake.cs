using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quake weapon effect
// - creates a circle of quaking at hit location
public class WeaponQuake : WeaponType {

	// Can it quake
	private bool quake;
	// Quake length timer
	private float quakeTimer;

	// Unity Methods
	////////////////
	void Start(){
		this.quake = true;
		this.quakeTimer = 3f;
		InvokeRepeating("ActivateQuake", 1f, 1f);
	}

	void Update() {
		this.quakeTimer -= Time.deltaTime;
		if (quakeTimer <= 0){
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D coll){
		if (this.quake == true){
			if (coll.gameObject.tag == "Enemy"){
				coll.gameObject.SendMessage("TakeDamage", 4);
			}
			this.quake = false;
		}
	}

	// Methods
	////////////////
	private void ActivateQuake(){
		this.quake = true;
	}
}