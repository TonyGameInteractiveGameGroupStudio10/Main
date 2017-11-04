using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPotion : MonoBehaviour {

	// Index of Potion
	public int itemIndex = 123;

	// Link to gameMaster
	public GameObject theGameMaster;

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
		if ((askedForSprite == false) && (itemIndex != 123)) {
			spriteSwitcher.sprite = theGameMaster.GetComponent<GameMaster>().GetPotionSprite(itemIndex);
			askedForSprite = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		// check if its the player, and the index has been changed/set
		if ((coll.gameObject.tag == "Player") && (itemIndex != 123)) {
			coll.gameObject.SendMessage("GivePotion", itemIndex);
			Destroy(gameObject);
		}
	}

	// Methods
	////////////////
	public void SetItemIndex(int newIndex){
		itemIndex = newIndex;
	}
}
