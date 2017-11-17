using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	// Spawn Location
	public GameObject[] spawner = new GameObject[12];

	// Enemy Types
	public GameObject monsterFire;
	public GameObject monsterIce;
	public GameObject monsterOil;
	public GameObject monsterStone;
	public GameObject monsterPoison;

	// Sprite Containers
	public Sprite[] potionSprites = new Sprite[3];
	public Sprite[] weaponSprites = new Sprite[1];
	public Sprite[] attackSprites = new Sprite[5];

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		// Store all the Spawn Points
		spawner = GameObject.FindGameObjectsWithTag("Spawner");
	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Sprites
	////////////////
	// Only One thing can be message passed, so passing an array of objects
	public Sprite GetPotionSprite(int index){
		return potionSprites[index];
	}

	public Sprite GetWeaponSprite(int index){
		return weaponSprites[index];
	}

	public Sprite GetAttackSprite(int index){
		return attackSprites[index];
	}

	// Spawns
	////////////////
	private GameObject SpawnSelector(){
		return spawner[Random.Range(0, 10)];
	}

}