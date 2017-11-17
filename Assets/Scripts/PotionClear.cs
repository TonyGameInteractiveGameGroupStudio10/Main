using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionClear : MonoBehaviour {

    void Start(){
        Invoke("DestroySelf", 1f);
    }

    void OnTriggerEnter2D(Collider2D coll){
        // Destroy Wall
        if (coll.gameObject.tag == "DropWall"){
            coll.gameObject.GetComponent<DropWall>().DestroySelf();
        }
        // Destroy Fire
        if (coll.gameObject.tag == "DropFire"){
            coll.gameObject.GetComponent<DropFire>().DestroySelf();
        }

        // Destroy Poison
        if (coll.gameObject.tag == "DropPoison"){
            coll.gameObject.GetComponent<DropPoison>().DestroySelf();
        }

        // Destroy Oil
        if (coll.gameObject.tag == "DropOil"){
            coll.gameObject.GetComponent<DropOil>().DestroySelf();
        }

        // Destroy Ice
        if (coll.gameObject.tag == "DropIce"){
            coll.gameObject.GetComponent<DropIce>().DestroySelf();
        }
    }

    private void DestroySelf(){
        Destroy(gameObject);
    }
}