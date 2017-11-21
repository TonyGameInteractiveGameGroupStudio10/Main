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
    // Grab game master for access to influenceMap
    private GameObject gameMaster;

    // Unity Methods
    ////////////////
    void Start(){
        gameMaster = GameObject.FindWithTag("GameMaster");
    }

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
            // fire monster isn't damage via fire
            if (coll.gameObject.GetComponent<MonsterClass>().getType() != 1) {
                coll.gameObject.GetComponent<MonsterClass>().TakeDamage(4);
            }
        }
        else if (coll.gameObject.tag == "Player") {
            coll.gameObject.GetComponent<UserClass>().TakeDamage(4);
        }
        // The fires spreads into the oil
        else if (coll.gameObject.tag == "DropOil"){
            // Remove Oil
            Vector3 oilLocation = coll.gameObject.transform.position;
            coll.gameObject.GetComponent<DropOil>().DestroySelf();
            // Add Fire
            Instantiate(this, oilLocation, Quaternion.identity);
            gameMaster.GetComponent<InfluenceMap>().addNode(oilLocation,1); 

        }
        // The water/ice/juel puts out the fire
        else if (coll.gameObject.tag == "DropIce"){
            // Remove Fire
            this.DestroySelf();
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

    // Methods
    ////////////////
    public void DestroySelf(){
            gameMaster.GetComponent<InfluenceMap>().delNode(transform.position,1);
            Destroy(gameObject);
    }
}