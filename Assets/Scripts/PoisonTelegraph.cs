using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTelegraph : MonoBehaviour {

    public GameObject poisonDrop;
    
    // Unity Method
    ////////////////////
    void Start(){
        Invoke("PoisonSpawn", 2f);
        Invoke("DestroySelf", 2.5f);
    }

    // Method
    ////////////////////
    private void PoisonSpawn(){
        Instantiate(poisonDrop, transform.position, Quaternion.identity);
    }

    private void DestroySelf(){
        Destroy(gameObject);
    }
}