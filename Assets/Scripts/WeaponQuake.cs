using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quake weapon effect
// - creates a circle of quaking at hit location
public class WeaponQuake : WeaponType {
	// Can it quake
	private bool quake;

	// Unity Methods
	////////////////
	void Start(){
		this.quake = true;
		InvokeRepeating("ActivateQuake", 0f, 1f);
		Invoke("DestroySelf", 3f);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (this.quake == true){
			if (coll.gameObject.tag == "Enemy"){
				coll.gameObject.SendMessage("TakeDamage", 3);
			}
			this.quake = false;
		}
	}

	void OnTriggerStay2D(Collider2D coll){
		if (this.quake == true){
			if (coll.gameObject.tag == "Enemy"){
				coll.gameObject.SendMessage("TakeDamage", 3);
			}
			this.quake = false;
		}
	}

	// Methods
	////////////////
	private void ActivateQuake(){
		this.quake = true;
	}

	private void DestroySelf(){
		Destroy(gameObject);
	}
}