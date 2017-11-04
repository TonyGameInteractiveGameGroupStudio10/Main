using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Fire
// - Fast
// - Low HP
// - Drops Fire
// - Cant be stunned
public class MonsterFire : MonsterClass {

    // Stats
    private float monsterSpeed = 4f;
    private int maxHealth = 15;

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
        effectRoller();

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
        transform.right = playerLocation.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, currentSpeed * Time.deltaTime);


        // Poison Timer
        if (this.poisonTimer > 0){
            this.poisonTimer -= Time.deltaTime;
            if ((poisonTimer >= 4) && (poisonDamage[0]) == false){
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