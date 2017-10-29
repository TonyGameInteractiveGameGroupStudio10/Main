using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Class that all monster classes are to be built from
public class MonsterClass : MonoBehaviour {

    // Stats
    ////////////////
    protected int healthPool;
    protected float currentSpeed;

    // Components
    ////////////////
    protected Rigidbody2D enemyBody;
    protected Animator enemyAnimator;

    // Player
    ////////////////
    protected GameObject thePlayer;
    protected Transform playerLocation;

    // Audio
    ///////////////
    public AudioClip deathCry;
    protected AudioSource audioPlayer;

    // Effects
    ////////////////
    private int dropIndex;
    private bool potionDrop;
    private bool attackModDrop;
    private bool weaponModDrop;
    private bool environmentDrop;

    // Prefab
    ////////////////
    public GameObject potionPrefab;
    public GameObject attackModPrefab;
    public GameObject weaponModPrefab;


    ///////////////////////////////////
    // Methods
    ///////////////////////////////////
    // Health
    ////////////////
    public int GetHealth(){
        return this.healthPool;
    }

    public void SetHealth(int newHealth){
        this.healthPool = newHealth;
    }

    public void TakeDamage(int incomingDamage){
        this.healthPool -= incomingDamage;
    }

    public void Die(){
        effectDropper();
        Destroy(gameObject);
    }

    // Speed
    ////////////////
    public float GetCurrentSpeed(){
        return this.currentSpeed;
    }

    public void SetCurrentSpeed(float newSpeed){
        this.currentSpeed = newSpeed;
    }

    // Effects
    ////////////////
    protected void effectRoller(){
        int diceRoll = Random.Range(1,101);

        // Roll values are currently temp, this is more of a skeleton
        if (diceRoll <= 2){
            potionDrop = true;
            dropIndex = Random.Range(0,2);
        }
        else if (diceRoll >= 3 && diceRoll <= 4){
            weaponModDrop = true;
            dropIndex = Random.Range(0,5);
        }
        else if (diceRoll >= 5 && diceRoll <= 6){
            attackModDrop = true;
            dropIndex = Random.Range(0,5);
        }
        else if (diceRoll >= 7 && diceRoll <= 8){
            environmentDrop = true;
        }
    }

    protected void effectDropper(){
        if (potionDrop == true){
            GameObject potionTemp = Instantiate(potionPrefab,transform.position,Quaternion.identity);
            potionTemp.SendMessage("setItemIndex", dropIndex);
        }
        else if(weaponModDrop == true){
            GameObject weaponTemp = Instantiate(weaponModPrefab,transform.position,Quaternion.identity);
            weaponTemp.SendMessage("setItemIndex", dropIndex);
        }
        else if(attackModDrop == true){
            GameObject attackTemp = Instantiate(attackModPrefab,transform.position,Quaternion.identity);
            attackTemp.SendMessage("setItemIndex", dropIndex)
        }
        else if (environmentDrop == true){
            // set drop index
            // environment prefab
        }
    }
}
