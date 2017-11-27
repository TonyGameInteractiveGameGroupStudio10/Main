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
	// Timers for the current round, wave and game
	private float currentRoundTimer;
	private float currentWaveTimer;
	// Store all the keeper information
	private float[][] timeKeeper;

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
		if (timeKeeper[currentRound][currentWave] > 0){
			timeKeeper[currentRound][currentWave] -= Time.deltaTime;
		}
		else if (currentWave == maxWaves-1){
			// if != monster alive
				// check if last level
					// win 
					// increase round
		}
		else {
			currentWave += 1;
			waveUi.text = (currentWave+1).ToString();
		}

		this.spawner();
	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Time Keeping
	////////////////
	private void SetUpKeepers(){
		timeKeeper = new float[maxRounds][maxWaves];
		// Set Up Round Timers, and Wave Timers 
		for (int i = 0; i < maxWaves; i += 1){
			timeKeeper[0][i] = 30f;
			timeKeeper[1][i] = 30f;
			timeKeeper[2][i] = 30f;
			timeKeeper[3][i] = 60f;
			timeKeeper[4][i] = 60f;
		}
	}

	// Spawners
	////////////////
	private void spanwer(){


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