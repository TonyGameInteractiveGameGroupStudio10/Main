using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Poison
// - Medium Speed
// - Medium HP
// - Drop Poison
// - Can't be poisoned
public class MonsterPoison : MonsterClass {

    // Stats
    private float monsterSpeed = 3.5f;
    private int maxHealth = 20;

    // Sprite
    private SpriteRenderer spriteSwitcher;
    public Sprite smallSprite;
    public Sprite largeSprite;

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
        spriteSwitcher = GetComponent<SpriteRenderer>();

        // Locate Player
        thePlayer = GameObject.FindWithTag("Player");
        playerLocation = thePlayer.transform;

        // Check to see if we drop something
        this.effectRoller();

        // Check which sprite to load
        // large sprite = has a drop ; small sprite = has no drop
        if ((potionDrop == true) || (weaponModDrop == true) || (attackModDrop == true) || (environmentDrop == true)){
            spriteSwitcher.sprite = largeSprite;
        } else {
            spriteSwitcher.sprite = smallSprite;
        }
    }

    // Update
    ////////////////
    void Update(){
        // Movement
        if (this.stunned == false){
            transform.right = playerLocation.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, currentSpeed * Time.deltaTime);
        }

        // Stun Timer
        if (this.stunTimer > 0){
            this.stunTimer -= Time.deltaTime;
            if (this.stunTimer <= 0){
                this.stunned = false;
            }
        }

        // if HP is less then 0
        if (healthPool <= 0){
            this.Die();
        }
    }

    // Fixed Update
    ////////////////
    void FixedUpdate() {
        // probably place this in a different place, and have it check less often
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
        return this.monsterSpeed;
    }

    public void SetMonsterSpeed(float newSpeed){
        this.monsterSpeed = newSpeed;
    }
}