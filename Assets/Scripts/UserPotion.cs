using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPotion : MonoBehaviour {
	// reference to user class
	private UserClass player;
	// variables for potions
	private bool hasteActive = false;
	private float timer;
	public GameObject clear;

	void Update(){
		if (timer > 0){
			timer -= Time.deltaTime;
		}
		if ((hasteActive == true) && (timer <= 0)){
			hasteActive = false;
			player.SetSpeed(player.GetMaxSpeed());
		}
	}

	public void Potion(int outgoingPotion){
		// Verify player isn't null 
		if (player == null){
			player = GetComponent<UserClass>();
		}
		// 0 - clear; 1 - haste; 2 - health
		if (outgoingPotion == 0){
			Instantiate(clear, transform.position, Quaternion.identity);
		} else if (outgoingPotion == 1){
			player.SetSpeed(player.GetSpeed()+1f);
			hasteActive = true;
			timer = 2f;
		} else {
			int newHealth = player.GetHealth() + 15;
			// If it is larger then max, set to max.
			if (newHealth <= player.GetMaxHealth()){
				player.SetHealth(newHealth);
			} else {
				player.SetHealth(player.GetMaxHealth());
			}
		}
	}
}