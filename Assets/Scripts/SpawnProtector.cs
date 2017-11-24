using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProtector : MonoBehaviour {
    
    // Used to protect the spawn from wall/forced damage
    void OnTriggerEnter2D(Collider2D coll){
        // Remove Oil
        if (coll.gameObject.tag == "DropOil"){
            coll.gameObject.GetComponent<DropOil>().DestroySelf();
        }
        // Remove Ice
        else if (coll.gameObject.tag == "DropIce"){
            coll.gameObject.GetComponent<DropIce>().DestroySelf();
        }
        // Remove Fire
        else if (coll.gameObject.tag == "DropFire"){
            coll.gameObject.GetComponent<DropFire>().DestroySelf();
        }
        // Remove Wall
        else if (coll.gameObject.tag == "DropWall"){
            coll.gameObject.GetComponent<DropWall>().DestroySelf();
        }
        // Remove Poison
        else if (coll.gameObject.tag == "DropPoison"){
            coll.gameObject.GetComponent<DropPoison>().DestroySelf();
        }
    }
}