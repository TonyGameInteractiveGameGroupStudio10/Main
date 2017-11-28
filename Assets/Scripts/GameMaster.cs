using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	// Spawn Location
	public GameObject[] spawnLocations = new GameObject[4];

	// Enemy Types
	public GameObject[] monsters = new GameObject[5];

	// Sprite Containers
	public Sprite[] potionSprites = new Sprite[3];
	public Sprite[] weaponSprites = new Sprite[1];
	public Sprite[] attackSprites = new Sprite[5];

	// Wave & Round Controllers
	// 3 waves per round
	private int currentRound;
	private int currentWave;
	// Number of Waves per round, and rounds per game
	private int maxWaves;
	private int maxRounds;
	// Timers for next spawn
	private float spawnTimer;
	private float nextRoundTimer;
	private bool stopSpawner;
	// Store all the keeper information
	private float[,] timeKeeper;
	private float[,] spawnRateKeeper;

	// UI
	public Text waveUI;
	public Text roundUI;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		// Store all the Spawn Points
		spawnLocations = GameObject.FindGameObjectsWithTag("Spawner");
		// Set up counters
		currentRound = 0;
		currentWave = 0;
		maxWaves = 3;
		maxRounds = 5;
		nextRoundTimer = 5f;
		stopSpawner = false;
		// Set up the time Keeper
		this.SetUpKeepers();
		// Set up the UI
		waveUI.text = 1.ToString();
		roundUI.text = 1.ToString();
	}

	// Update
	////////////////
	void Update(){
		// Timer For Waves & Rounds
		if (timeKeeper[currentRound,currentWave] > 0){
			timeKeeper[currentRound,currentWave] -= Time.deltaTime;
		} 
		// If the player is on the last wave, and the time is out
		else if (currentWave == maxWaves-1){
			// Stop the spawning
			stopSpawner = true;
			// Check every five seconds
			if (nextRoundTimer > 0){
				nextRoundTimer -= Time.deltaTime;
			} else {
				nextRoundTimer = 5f;
				// too see if all enemy are destroy.
				if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
					// If true, and it was the last round, win. 
					if (currentRound == maxRounds-1){
						// WIN
					// Else increase the round, and start spawning again
					} else{
						currentRound += 1;
						currentWave = 0;
						roundUI.text = (currentRound+1).ToString();
						waveUI.text = (currentWave+1).ToString();
						stopSpawner = false;
					}
				} 
			}
		} else {
			currentWave += 1;
			waveUI.text = (currentWave+1).ToString();
		}

		// Timer for spawns
		if (spawnTimer > 0){
			spawnTimer -= Time.deltaTime;
		} else if (stopSpawner == false){
			this.Spawner();
		}
	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Time Keeping
	////////////////
	private void SetUpKeepers(){
		// Spawn count per wave - total - total round time - avg spawn rate
		// 10 10 15 - 35 / 90 / 2.57
		// 15 15 20 - 50 / 90 / 1.8
		// 20 20 25 - 65 / 90 / 1.38
		// 50 50 50 - 150 / 180 / 1.2
		// 50 55 55 - 160 / 180 / 1.12

		// Set Up Round Timers, and Wave Timers 
		timeKeeper = new float[maxRounds,maxWaves];
		for (int i = 0; i < maxWaves; i += 1){
			timeKeeper[0,i] = 30f;
			timeKeeper[1,i] = 30f;
			timeKeeper[2,i] = 30f;
			timeKeeper[3,i] = 60f;
			timeKeeper[4,i] = 60f;
		}

		// Set Up Spawn Rates 
		spawnRateKeeper = new float[maxRounds,maxWaves];
		// Round One
		spawnRateKeeper[0,0] = 3f;
		spawnRateKeeper[0,1] = 3f;
		spawnRateKeeper[0,2] = 2f;
		// Round Two
		spawnRateKeeper[1,0] = 2f;
		spawnRateKeeper[1,1] = 2f;
		spawnRateKeeper[1,2] = 1.5f;
		// Round Three
		spawnRateKeeper[2,0] = 1.5f;
		spawnRateKeeper[2,1] = 1.5f;
		spawnRateKeeper[2,2] = 1.2f;
		// Round Four
		spawnRateKeeper[3,0] = 1.2f;
		spawnRateKeeper[3,1] = 1.2f;
		spawnRateKeeper[3,2] = 1.2f;
		// Round Five
		spawnRateKeeper[4,0] = 1.2f;
		spawnRateKeeper[4,1] = 1.09f;
		spawnRateKeeper[4,2] = 1.09f;
	}

	// Spawners
	////////////////
	private void Spawner(){
		spawnTimer = spawnRateKeeper[currentRound,currentWave];
		this.MonsterSpawning();
	}

	private Vector3 SpawnSelector(){
		return spawnLocations[Random.Range(0, 4)].transform.position;
	}

	private void MonsterSpawning(){
		int diceRollMob = Random.Range(0,5);
		Vector3 diceRollSpawner = this.SpawnSelector();
		Instantiate(monsters[diceRollMob],diceRollSpawner,Quaternion.identity);
	}

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


}