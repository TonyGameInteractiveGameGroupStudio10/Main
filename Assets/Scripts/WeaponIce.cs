﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIce : MonoBehaviour {

    void Start(){
        Invoke("DestroySelf", 3.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            collision.gameObject.SendMessage("TakeDamage", 2);
            StartCoroutine(DelayedFreeze());
            collision.gameObject.SendMessage("StunPlayer");   
        }
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<MonsterClass>().getType() != 4){
            collision.gameObject.SendMessage("TakeDamage", 2);
            collision.gameObject.SendMessage("ReceivingStun");       
        }
    }

    IEnumerator DelayedFreeze(){
        yield return new WaitForSeconds(1.5f);
    }
   

    private void DestroySelf(){
      Destroy(gameObject);
    }
}
