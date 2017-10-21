using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// General Weapon Script that all prefab scripts should inherit from
public class WeaponType : MonoBehaviour {

	// Stats
	////////////////
	protected int damage;

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Damage
	/////////////////
	public int GetDamage(){
		return damage;
	}

	public void SetDamage(int newDamage){
		damage = newDamage;
	}

	public void PlusDamage(int newDamage){
		damage += newDamage;
	}

	public void MinusDamage(int newDamage){
		damage -= newDamage;
	}
}