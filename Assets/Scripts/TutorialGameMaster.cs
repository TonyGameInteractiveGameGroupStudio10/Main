using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameMaster : GameMaster {

	// Event Timer and Position
	private float eventTimer;
	private int eventPlace;

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
		this.WelcomePhase();
		eventTimer = 8f;
		eventPlace = 1;
	}

	void Update(){
		if (eventTimer > 0){
			eventTimer -= Time.deltaTime;
		}
		else if(eventPlace == 1){

		}
		else if(eventPlace == 2){

		}
		else if(eventPlace == 3){

		}
		else if(eventPlace == 4){

		}
		else if(eventPlace == 5){

		}
		else{

		}
	}

	public void WelcomePhase(){

	}

	public void MovementPhase(){

	}

	public void ShootingPhase(){

	}

	public void PowerUpPhase(){

	}

	public void PotionPhase(){

	}

	public void MonsterPhase(){

	}
}