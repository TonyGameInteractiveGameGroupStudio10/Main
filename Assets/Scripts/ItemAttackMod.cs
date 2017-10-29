using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackMod : MonoBehaviour {

	public int itemIndex;

	public Sprite displaySprite;
	private SpriteRenderer spriteRenderer;

	void Start () {	
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		//displaySprite = gameMaster.getAttackModSprite(itemIndex);
		//this.spriteRenderer.sprite = displaySprite;	
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player"){
			coll.gameObject.SendMessage("GiveAttackMod", itemIndex);
			Destroy(gameObject);
		}
	}

	public void setItemIndex(int newIndex){
		this.itemIndex = newIndex;
	}
}