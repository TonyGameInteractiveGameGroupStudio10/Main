using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIce : MonoBehaviour {
    // Time Player has before freeze
    private float playerTimer;
    // Reference to player object
    private GameObject playerRef;
    // Is the player inside the freeze
    private bool playerInside;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    void Start(){
        playerTimer = 1f;
        playerInside = false;
        Invoke("DestroySelf", 3.5f);
    }

    void Update(){
      if (playerInside == true){
        if (playerTimer > 0){
          playerTimer -= Time.deltaTime;
        } else {
          playerRef.SendMessage("StunPlayer");
          playerTimer = 1f;
        }
      }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            collision.gameObject.SendMessage("TakeDamage", 2);
            playerInside = true;
            playerRef = collision.gameObject;
        }
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<MonsterClass>().getType() != 4){
            collision.gameObject.SendMessage("TakeDamage", 2);
            collision.gameObject.SendMessage("ReceivingStun");       
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            playerInside = false;
            playerTimer = 1f;
        }
    }

    ///////////////////////////////////
    // Methods
    ///////////////////////////////////
    private void DestroySelf(){
      Destroy(gameObject);
    }
}