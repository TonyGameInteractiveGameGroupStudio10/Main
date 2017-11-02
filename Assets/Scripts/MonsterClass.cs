using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Class that all monster classes are to be built from
public class MonsterClass : MonoBehaviour {

    // Stats
    ////////////////
    protected int healthPool;
    protected float currentSpeed;

    // Status Effects
    ///////////////
    protected int poison;
    protected float poisonTimer; // 3 seconds
    protected bool stunned; 
    protected float stunnedTimer; // 1 seconds

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
        this.effectDropper();
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

    // Status Effects
    ////////////////
    protected void Poisoned(){
        TakeDamage(2*this.poison);
    }

    public void ReceivingPoison(){
        if (this.poison <= 5){
            this.poison += 1;
        }
        this.poisonTimer = 3f;
    }

    public void ReceivingStun(){
        this.stunned = true;
        this.stunnedTimer = 1f;
    }

    // Effects/Items
    ////////////////
    protected void effectRoller(){
        int diceRoll = Random.Range(1,101);
        // Roll values are currently temp, this is more of a skeleton
        if (diceRoll <= 2){
            this.potionDrop = true; 
            // 0 - clear; 1 - haste; 2 - health;
            this.dropIndex = Random.Range(0,4);
        }
        else if (diceRoll >= 3 && diceRoll <= 4){
            this.weaponModDrop = true;
            // 0 - Arrow Speed ; 1 - Attack Speed ; 2 - Crit; 3 - cone;
            this.dropIndex = Random.Range(0,4);
        }
        else if (diceRoll >= 5 && diceRoll <= 6){
            this.attackModDrop = true;
            // 0 - Posion ; 1 - vine ; 2 - shock ; 3 - quaking ; 4 - ricochet;
            this.dropIndex = Random.Range(0,5);
        }
        else if (diceRoll >= 7 && diceRoll <= 12){
            // doesn't have a drop table because each environment drop is unique to
            // the monster that it is being dropped by
            this.environmentDrop = true;
        }
    }

    protected void effectDropper(){
        // If any dropper has been marked true, the instantiate a drop of that type
        if (this.potionDrop == true){
            GameObject potionTemp = Instantiate(potionPrefab,transform.position,Quaternion.identity);
            potionTemp.SendMessage("setItemIndex", dropIndex);
        }
        else if(this.weaponModDrop == true){
            GameObject weaponTemp = Instantiate(weaponModPrefab,transform.position,Quaternion.identity);
            weaponTemp.SendMessage("setItemIndex", dropIndex);
        }
        else if(this.attackModDrop == true){
            GameObject attackTemp = Instantiate(attackModPrefab,transform.position,Quaternion.identity);
			attackTemp.SendMessage ("setItemIndex", dropIndex);
        }
        else if (this.environmentDrop == true){
            // set drop index
            // environment prefab
        }
    }
}
