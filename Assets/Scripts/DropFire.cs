using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drop fire
// - creates a box of Fire
// - Damage while it the fire
// - Differs from poison because doesn't dot afterwards
// - Oil Loses, spread (HERE)
// - Poison Loses, destory (POISON)
// - Ice Wins, destroy (HERE)
public class DropFire : MonoBehaviour {

    // Can fire tick
    private bool fire;

    // Timer before fire can tick
    private float fireTimer;

    // Unity Methods
    ////////////////
    void Update() {
        if(fireTimer > 0){
            fireTimer -= Time.deltaTime;
        } else {
            fire = true;
            fireTimer = 2.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Enemy"){
            coll.gameObject.GetComponent<MonsterClass>().TakeDamage(4);
        }
        else if (coll.gameObject.tag == "Player") {
            coll.gameObject.GetComponent<UserClass>().TakeDamage(4);
        }
        // The fires spreads into the oil
        else if (coll.gameObject.tag == "DropOil"){
            Vector3 oilLocation = coll.gameObject.transform.position;
            Destroy(coll.gameObject);
            Instantiate(this, oilLocation, Quaternion.identity); 
        }
        // The water/ice/juel puts out the fire
        else if (coll.gameObject.tag == "DropIce"){
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D coll){
        if (this.fire == true){
            if (coll.gameObject.tag == "Enemy"){
                coll.gameObject.GetComponent<MonsterClass>().TakeDamage(4);
            }
            else if (coll.gameObject.tag == "Player") {
                coll.gameObject.GetComponent<UserClass>().TakeDamage(4);
            }
            this.fire = false;
        }
    }
}