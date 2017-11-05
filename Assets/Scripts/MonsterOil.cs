using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Oil
// - Slow
// - Medium HP
// - Drop oil
// - Can't Be Slowed
public class MonsterOil : MonsterClass {

    // Stats
    private int maxHealth = 20;
    //Distance to player and temp location
    private int DistanceToPlayer;
    public Transform TempLocation;
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
        monsterSpeed = 2.5f;
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
        //Attack
        if (DistanceToPlayer <= 8)
        {
            OilSling();   
        }
        // Poison Timer
        if (this.poisonTimer > 0){
            this.poisonTimer -= Time.deltaTime;
            if ((poisonDamage[0] == false) && (poisonTimer >= 4)){
                this.Poisoned();
                this.poisonDamage[0] = true;
            }
            else if ((poisonDamage[1] == false) && (poisonTimer < 4) && (poisonTimer >= 2)){
                this.Poisoned();
                this.poisonDamage[1] = true;
            }
            else if ((poisonDamage[2] == false) && (poisonTimer < 2)) {
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

        // Can't be slowed
        if (this.monsterSpeed != this.currentSpeed){
        	this.currentSpeed = this.monsterSpeed;
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
    //private void OilBomb();
    //{
    //     transform.right = playerLocation.position - transform.position;
    //     Instantiate.OilBomb;
    //     if (oil hits player)
    //         slow Player for 2 tiles
    //     else
    //         do nothing
    //}
}
