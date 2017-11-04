using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drop Oil
// - creates a circle of oil
// - slows the unit
// - Fire Wins, spread (FIRE)
// - Poison Wins, spread (POISON)
// - Ice Loses, spread (HERE)
public class DropOil : MonoBehaviour {

    // Unity Methods
    ////////////////
    void OnTriggerEnter2D(Collider2D coll){
        // Slow enemy 
        if (coll.gameObject.tag == "Enemy"){
            float objectSpeed = coll.gameObject.GetComponent<MonsterClass>().GetCurrentSpeed();
            coll.gameObject.GetComponent<MonsterClass>().SetCurrentSpeed(objectSpeed/2);
        }
        // Slow Player
        else if (coll.gameObject.tag == "Player") {
            float objectSpeed = coll.gameObject.GetComponent<UserClass>().GetCurrentSpeed();
            coll.gameObject.GetComponent<UserClass>().SetCurrentSpeed(objectSpeed/2);
        }
        // The oil corrupts the water/ice/juel
        else if (coll.gameObject.tag == "DropIce"){
            Vector3 iceLocation = coll.gameObject.transform.position;
            Destroy(coll.gameObject);
            Instantiate(this, iceLocation, Quaternion.identity);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if (coll.gameObject.tag == "Enemy"){
            float objectFullSpeedE = coll.gameObject.GetComponent<MonsterClass>().GetMonsterSpeed();
            coll.gameObject.GetComponent<MonsterClass>().SetCurrentSpeed(objectFullSpeedE);
        }
        else if (coll.gameObject.tag == "Player") {
            float objectFullSpeedP = coll.gameObject.GetComponent<UserClass>().GetSpeed();
            coll.gameObject.GetComponent<UserClass>().SetCurrentSpeed(objectFullSpeedP);
        }
    }
}