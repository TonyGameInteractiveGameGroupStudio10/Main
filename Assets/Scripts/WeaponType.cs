using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour {

	protected int damage;

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