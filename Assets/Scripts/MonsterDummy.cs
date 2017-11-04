using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDummy : MonsterClass {

	// Stats
    private float monsterSpeed = 4.2f;
    private int maxHealth = 100;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    // Start
    ////////////////
    void Start() {
    	// Set Stats
    	this.healthPool = this.maxHealth;
    	this.currentSpeed = this.monsterSpeed;

    	// Grab Components
        this.enemyAnimator = GetComponent<Animator>();
        this.enemyBody = GetComponent<Rigidbody2D>();
        this.audioPlayer = GetComponent<AudioSource>();

        // Locate Player
        this.thePlayer = GameObject.FindWithTag("Player");
        this.playerLocation = thePlayer.transform;

        // Check to see if we drop something
        effectRoller();

    }

    // Update
    ////////////////
    void Update(){
        // Poison Timer
        if (this.poisonTimer > 0){
            this.poisonTimer -= Time.deltaTime;
            if ((poisonTimer >= 4) && (poisonDamage[0] == false)){
                this.Poisoned();
                this.poisonDamage[0] = true;
            }
            else if ((poisonTimer < 4) && (poisonTimer >= 2) && (poisonDamage[1] == false)){
                this.Poisoned();
                this.poisonDamage[1] = true;
            }
            else if ((poisonTimer < 2)  && (poisonDamage[2] == false)) {
                this.Poisoned();
                this.poisonDamage[2] = true;
            }
        }

        // Stun Timer
        if (stunTimer > 0){
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0){
                stunned = false;
            }
        }

        // Movement
        if (stunned == false){

        }

        // if HP is less then 0
        if (healthPool <= 0){
            Die();
        }
    }

    ///////////////////////////////////
    // Methods
    ///////////////////////////////////
    // Health
    ////////////////
    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        this.maxHealth = newMaxHealth;
    }

    // Speed
    ////////////////
    public float GetMonsterSpeed(){
    	return monsterSpeed;
    }

    public void SetMonsterSpeed(float newSpeed){
    	monsterSpeed = newSpeed;
    }
}