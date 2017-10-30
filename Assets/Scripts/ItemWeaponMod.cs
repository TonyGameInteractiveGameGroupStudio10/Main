using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeaponMod : MonoBehaviour {

	// Index of Weapon Modifier
	private int itemIndex;

	// Link to GameMaster
	private GameObject theGameMaster;

	// Sprite
	private SpriteRenderer spriteRenderer;
	private bool askedForSprite;

	// Unity Methods
	////////////////
	void Start () {	
		itemIndex = 123;
		askedForSprite = false;
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		theGameMaster = GameObject.FindGameObjectWithTag("GameMaster");
	}

	void Update() {
		// If that index has been changed/set
		if ((itemIndex != 123) && (askedForSprite == false)) {
			theGameMaster.GetComponent<GameMaster>().GetWeaponSprite(itemIndex);
			askedForSprite = true;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if ((coll.gameObject.tag == "Player") && (itemIndex != 123)) {
			coll.gameObject.SendMessage("GiveWeaponMod", itemIndex);
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