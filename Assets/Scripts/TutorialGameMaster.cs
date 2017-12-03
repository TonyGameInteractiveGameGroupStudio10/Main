using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGameMaster : GameMaster {
	// Access to user
	public GameObject thePlayer;

	// Event Timer and Position
	private float eventTimer;
	private int eventPlace;

	// Timers for Monster Phase
	private float monsterTimer;
	private int currentType;
	private int howMany;

	// Event Texts
	public GameObject welcome;
	public GameObject movement;
	public GameObject shooting;
	public GameObject powerUps;
	public GameObject potions;
	public GameObject monstersText;
	public GameObject monstersTextPT2;
	public GameObject completedButtons;

	// Props
	public GameObject attackMod;
	public GameObject potionHealth;
	public GameObject potionHaste;
	public GameObject potionClear;

	// Complete Buttons
	public Button retryButton;
	public Button menuButton;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		spawnLocations = GameObject.FindGameObjectsWithTag("Spawner");
		welcome.SetActive(false);
		movement.SetActive(false);
		shooting.SetActive(false);
		powerUps.SetActive(false);
		potions.SetActive(false);
		monstersText.SetActive(false);
		monstersTextPT2.SetActive(false);
		completedButtons.SetActive(false);
		attackMod.SetActive(false);
		potionHealth.SetActive(false);
		potionHaste.SetActive(false);
		potionClear.SetActive(false);
		eventPlace = 0;
		this.WelcomePhase();

        retryButton.GetComponent<Button>().onClick.AddListener(Retry);
        menuButton.GetComponent<Button>().onClick.AddListener(MainMenu);
	}

	// Update
	////////////////
	void Update(){
		// Keep the player at max life
		if (thePlayer.GetComponent<UserClass>().GetHealth() < 60){
			thePlayer.GetComponent<UserClass>().SetHealth(60);
		}

		if (eventTimer > 0){
			eventTimer -= Time.deltaTime;
		}
		else if(eventPlace == 1){
			MovementPhase();
			if ((Input.GetKeyDown("w")) || (Input.GetKeyDown("a")) || (Input.GetKeyDown("s")) || (Input.GetKeyDown("d"))){
				eventPlace += 1;
				eventTimer = 3f;
			}
		}
		else if(eventPlace == 2){
			ShootingPhase();
			if ((Input.GetKey("space")) || (Input.GetKey(KeyCode.Mouse1))){
				eventPlace += 1;
				eventTimer = 3f;
			}
		}
		else if(eventPlace == 3){
			PowerUpPhase();
			if (thePlayer.GetComponent<UserClass>().GetAttackMod(0) >= 1){
				eventPlace += 1;
				eventTimer = 3f;
			}
		}
		else if(eventPlace == 4){
			PotionPhase();
			if ((potionHealth == null) && (potionHaste == null) && (potionClear == null)){
				eventPlace += 1;
				eventTimer = 5f;
			}
		}
		else if (eventPlace == 5){
			MonsterPhase();
			eventPlace += 1;
			eventTimer = 8f;
		} 
		else if (eventPlace == 6){
			MonsterPhasePT2();
		}
		else {
			Completed();
		}
	}

	///////////////////////////////////
	// Methods
	///////////////////////////////////
	// Tutorial Phase
	//////////////////
	private void WelcomePhase(){
		welcome.SetActive(true);
		eventTimer = 4f;
		eventPlace += 1;
	}

	private void MovementPhase(){
		welcome.SetActive(false);
		movement.SetActive(true);
	}

	private void ShootingPhase(){
		movement.SetActive(false);
		shooting.SetActive(true);
	}

	private void PowerUpPhase(){
		shooting.SetActive(false);
		powerUps.SetActive(true);
		if (attackMod != null){
			attackMod.SetActive(true);
		}
	}

	private void PotionPhase(){
		powerUps.SetActive(false);
		potions.SetActive(true);
		if (potionHealth != null){
			potionHealth.SetActive(true);
		}
		if (potionHaste != null){
			potionHaste.SetActive(true);
		}
		if (potionClear != null){
			potionClear.SetActive(true);
		}
	}

	private void MonsterPhase(){
		potions.SetActive(false);
		monstersText.SetActive(true);
		howMany = 0;
		currentType = 0;
	}

	private void MonsterPhasePT2(){
		monstersText.SetActive(false);
		monstersTextPT2.SetActive(true);
		if (monsterTimer > 0){
			monsterTimer -= Time.deltaTime;
		} else{
			if ((currentType >= 4) && (howMany >= 2)){
				eventPlace += 1;
			} else if (howMany >= 2){
				currentType += 1;
				howMany = 0;
			} else {
				MonsterSpawningTut(currentType);
				monsterTimer = 10f;
				howMany += 1;
			}
		}
	}

	private void Completed(){
		monstersTextPT2.SetActive(false);
		completedButtons.SetActive(true);
	}

	// Monster Spawner
	//////////////////
	private void MonsterSpawningTut(int monsterType){
		Vector3 diceRollSpawner = this.SpawnSelector();
		Instantiate(monsters[monsterType],diceRollSpawner,Quaternion.identity);
	}

	// Buttons
	//////////////////
	void Retry(){
		completedButtons.SetActive(false);
		eventPlace = 5;
		currentType = 0;
		howMany = 0;
	}

	void MainMenu(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}