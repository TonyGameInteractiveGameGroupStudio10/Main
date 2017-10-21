using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkeleton : MonsterClass {

	// Stats
    private float monsterSpeed = 4.2f;
    private int maxHealth = 10;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    // Start
    ////////////////
    void Start() {
    	// Set Stats
    	healthPool = maxHealth;
    	currentSpeed = monsterSpeed;

    	// Grab Components
        enemyAnimator = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody2D>();
        audioPlayer = GetComponent<AudioSource>();

        // Locate Player
        thePlayer = GameObject.FindWithTag("Player");
        playerLocation = thePlayer.transform;

        // Check to see if we drop something
        effectRoller();

    }

    // Update
    ////////////////
    void Update(){
        transform.right = playerLocation.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, speed * Time.deltaTime);

        if (healthPool <= 0){
            Die();
        }
    }

    // Fixed Update
    ////////////////
    void FixedUpdate() {
        playerLocation = thePlayer.transform;
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
    	monsterSpeed = newSpeed();
    }
}