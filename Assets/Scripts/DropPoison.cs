using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drop Poison
// - creates a circle of poison that DoTs enemies, and player
// - Fire Wins, destory (HERE)
// - Oil Loses, spread (HERE)
// - Ice Loses, spread (HERE)
public class DropPoison : MonoBehaviour {

    // Can it tick
    private bool poison;
    // Poison Tick Timer
    private float poisonTimer;

    // Unity Methods
    ////////////////
    void Start(){
        this.poison = true;
        this.poisonTimer = 0f;
    }

    void Update() {
        if(poisonTimer > 0){
            poisonTimer -= Time.deltaTime;
        } else {
            poison = true;
            poisonTimer = 3f;
        }
    }

  void OnTriggerEnter2D(Collider2D coll){
        // On collision with enemy, poison him
        if (coll.gameObject.tag == "Enemy"){
            coll.gameObject.GetComponent<MonsterClass>().ReceivingPoison();
        }
        // On collision with player, poison him
        else if (coll.gameObject.tag == "Player") {
            coll.gameObject.GetComponent<UserClass>().ReceivingPoison();
        }
        // The poison burns away if touches fire
        else if (coll.gameObject.tag == "DropFire"){
            Destroy(gameObject);
        }
        // The poison spreads into the oil if it touches it
        else if (coll.gameObject.tag == "DropOil"){
            Vector3 oilLocation = coll.gameObject.transform.position;
            Destroy(coll.gameObject);
            Instantiate(this, oilLocation, Quaternion.identity); 
        }
        // The poison corrupts the water/ice/juel
        else if (coll.gameObject.tag == "DropIce"){
            Vector3 iceLocation = coll.gameObject.transform.position;
            Destroy(coll.gameObject);
            Instantiate(this, iceLocation, Quaternion.identity);
        }
    }
    
    void OnTriggerStay2D(Collider2D coll){
        if (this.poison == true){
            if (coll.gameObject.tag == "Enemy"){
                coll.gameObject.GetComponent<MonsterClass>().ReceivingPoison();
            }
            else if (coll.gameObject.tag == "Player") {
                coll.gameObject.GetComponent<UserClass>().ReceivingPoison();
            }
            this.poison = false;
        }
    }
}