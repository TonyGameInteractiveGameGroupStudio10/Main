﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackMod : MonoBehaviour {

	// Index of Attack Modifier
	public int itemIndex;

	// Link to GameMaster
	private GameObject theGameMaster;

	// Sprite
	private SpriteRenderer spriteRenderer;
	private bool askedForSprite;

	// Unity Methods
	////////////////
	void Start () {	
		//itemIndex = 123;   <------- TURN THIS ON
		askedForSprite = false;
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		theGameMaster = GameObject.FindGameObjectWithTag("GameMaster");
	}

	void Update(){
		// If that index has been changed/set
		if ((itemIndex != 123) && (askedForSprite == false)) {
			//theGameMaster.GetComponent<GameMaster>().GetAttackSprite(itemIndex); <------ TURN THIS ON
			askedForSprite = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if ((coll.gameObject.tag == "Player") && (itemIndex != 123)){
			coll.gameObject.SendMessage("GiveAttackMod", itemIndex);
			Destroy(gameObject);
		}
	}

	// Methods
	////////////////
	public void setItemIndex(int newIndex){
		this.itemIndex = newIndex;
	}

	public void setSprite(Sprite newSprite){
		this.spriteRenderer.sprite = newSprite;
	}
}