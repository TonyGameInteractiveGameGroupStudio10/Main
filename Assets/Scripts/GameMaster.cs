using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	// Spawn Location
	public GameObject[] spawner = new GameObject[4];

	// Enemy Types
	public GameObject[] monsters = new GameObject[5];

	// Sprite Containers
	public Sprite[] potionSprites = new Sprite[3];
	public Sprite[] weaponSprites = new Sprite[1];
	public Sprite[] attackSprites = new Sprite[5];

	// Wave Timer
	private float gameTimer;
	private int waves;
	private int currentWave;
	private float[] waveTimer;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		// Store all the Spawn Points
		spawner = GameObject.FindGameObjectsWithTag("Spawner");
		// Set up the wave timers
		currentWave = 0;
		waves = 5;
		waveTimer = new float[waves];
		gameTimer  = waves*120;
		for (int i = 0; i < waves; i += 1){
			waveTimer[i] = 120;
		}
		InvokeRepeating("MonsterSpawning", 2f, 2f);
	}

	void Update(){

		if(gameTimer > 0){
			gameTimer -= Time.deltaTime;
		} else {
			// YOU WIN
		}

		if(waveTimer[currentWave] > 0){
			waveTimer[currentWave] -= Time.deltaTime;
		} else {
			currentWave += 1;
		}

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
	private Vector3 SpawnSelector(){
		return spawner[Random.Range(0, 4)].transform.position;
	}

	private void MonsterSpawning(){
		int diceRollMob = Random.Range(0,5);
		Vector3 diceRollSpawner = this.SpawnSelector();
		Instantiate(monsters[diceRollMob],diceRollSpawner,Quaternion.identity);

	}


}