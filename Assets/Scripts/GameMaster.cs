using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	// Spawn Location
	public GameObject[] spawner = new GameObject[12];

	// Sprite Containers
	public Sprite[] potionSprites = new Sprite[3];
	public Sprite[] weaponSprites;
	public Sprite[] attackSprites;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		// Store all the Spawn Points
		spawner = GameObject.FindGameObjectsWithTag("Spawner");
	}

	// Start
	////////////////
	void Update(){

	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Sprites
	////////////////
	// Only One thing can be message passed, so passing an array of objects
	// [0] - sprite index; [1] - sender
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