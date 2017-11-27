using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoisonTelegraph : MonoBehaviour {

    // Poison Drop
    public GameObject poisonDrop;
    // Store the created poison
    private GameObject poisonSave;
    
    // Unity Method
    ////////////////////
    void Start(){
        Invoke("PoisonSpawn", 2f);
		Invoke("DestroyPoison", 5f);
        Invoke("DestroySelf", 5.1f);
    }

    // Method
    ////////////////////
    private void PoisonSpawn(){
        poisonSave = Instantiate(poisonDrop, transform.position, Quaternion.identity);
    }

    private void DestroyPoison(){
        poisonSave.GetComponent<DropPoison>().DestroySelf();
    }

    private void DestroySelf(){
        Destroy(gameObject);
    }
}