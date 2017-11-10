using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeaponMod : MonoBehaviour {

	// Index of Weapon Modifier
	public int itemIndex = 123;

	// Link to GameMaster
	private GameObject theGameMaster;

	// Sprite
	private SpriteRenderer spriteSwitcher;
	private bool askedForSprite;

	// Unity Methods
	////////////////
	void Start () {	
		askedForSprite = false;
		this.spriteSwitcher = GetComponent<SpriteRenderer>();
		theGameMaster = GameObject.FindGameObjectWithTag("GameMaster");
	}

	void Update() {
		// If that index has been changed/set
		if ((itemIndex != 123) && (askedForSprite == false)) {
			//spriteSwitcher.sprite = theGameMaster.GetComponent<GameMaster>().GetWeaponSprite(itemIndex);
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
	public void SetItemIndex(int newIndex){
		this.itemIndex = newIndex;
	}
}