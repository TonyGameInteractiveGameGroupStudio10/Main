using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameMaster : GameMaster {

	// Event Timer and Position
	private float eventTimer;
	private int eventPlace;
	public KeyCode lastHitKey;

	// Event Texts
	public GameObject welcome;
	public GameObject movement;
	public GameObject shooting;
	public GameObject powerUps;
	public GameObject potions;
	public GameObject monstersText;

	// Props
	public GameObject attackMod;
	public GameObject potionHealth;
	public GameObject potionHaste;
	public GameObject potionClear;


	void Start(){
		welcome.SetActive(false);
		movement.SetActive(false);
		shooting.SetActive(false);
		powerUps.SetActive(false);
		potions.SetActive(false);
		monstersText.SetActive(false);
		attackMod.SetActive(false);
		potionHealth.SetActive(false);
		potionHaste.SetActive(false);
		potionClear.SetActive(false);
		eventPlace = 0;
		this.WelcomePhase();
	}

	void Update(){
		if (eventTimer > 0){
			eventTimer -= Time.deltaTime;
		}
		else if(eventPlace == 1){
			MovementPhase();
			if ((Input.GetKeyDown("w")) || (Input.GetKeyDown("a")) || (Input.GetKeyDown("s")) || (Input.GetKeyDown("d"))){
				eventPlace += 1;
				eventTimer = 2f;
			}
		}
		else if(eventPlace == 2){
			ShootingPhase();
			if ((Input.GetKey("space")) || (Input.GetKey(KeyCode.Mouse1))){
				eventPlace += 1;
				eventTimer = 2f;
			}
		}
		else if(eventPlace == 3){
			PowerUpPhase();
		}
		else if(eventPlace == 4){
			PotionPhase();
		}
		else if(eventPlace == 5){
			MonsterPhase();
		}
		else{
			Completed();
		}
	}

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

	private  void PowerUpPhase(){

	}

	private void PotionPhase(){

	}

	private void MonsterPhase(){

	}

	private void Completed(){

	}
}