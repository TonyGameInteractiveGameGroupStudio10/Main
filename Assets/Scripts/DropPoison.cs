using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drop Poison
// - creates a circle of poison
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

    void OnTriggerEnter2D(Collider2D coll){
            if (coll.gameObject.tag == "Enemy"){
                coll.gameObject.GetComponent<MonsterClass>().ReceivingPoison();
            }
            else if (coll.gameObject.tag == "Player") {
                coll.gameObject.GetComponent<UserClass>().ReceivingPoison();
            }
    }
}