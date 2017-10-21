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


    ///////////////////////////////////
    // Methods
    ///////////////////////////////////
    // Health
    ////////////////
    public int GetHealth()
    {
        return this.healthPool;
    }

    public void SetHealth(int newHealth)
    {
        this.healthPool = newHealth;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        this.maxHealth = newMaxHealth;
    }

    public void TakeDamage(int incomingDamage)
    {
        this.healthPool -= incomingDamage;
    }

    public void Die(){
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
        int diceRoll = Random.Range(1,100);

        
    }
}
