using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Class that all monster classes are to be built from
public class MonsterClass : MonoBehaviour {

    // Stats
    ////////////////
    private int healthPool;
    private float currentSpeed;

    // Components
    ////////////////
    private Rigidbody2D enemyBody;
    private Animator enemyAnimator;

    // Player
    ////////////////
    private GameObject thePlayer;
    private Transform playerLocation;

    // Audio
    ///////////////
    public AudioClip deathCry;
    private AudioSource audioPlayer;

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

    public int GetMaxHealth(){
        return this.maxHealth;
    }

    public void SetMaxHealth(int newMaxHealth){
        this.maxHealth = newMaxHealth;
    }

    public void TakeDamage(int incomingDamage){
        this.healthPool -= incomingDamage;
    }

    public void Die(){
        effectDropper();
        Destory(gameObject);
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
    private void effectRoller(){
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

    private void effectDropper(){
        if (potionDrop == true){
            // potion prefab
        }
        else if(weaponModDrop == true){
            // weapon prefab
        }
        else if(attackModDrop == true){
            // attack prefab
        }
        else if (environmentDrop == true){
            // environment prefab
        }
    }
}
