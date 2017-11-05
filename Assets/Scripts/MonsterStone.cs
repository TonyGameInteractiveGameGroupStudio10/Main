using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Stone
// - Slow
// - Larger Health Pool
// - Drops Walls
public class MonsterStone : MonsterClass {

    // Stats
    public int Thrust = 50;
    private int maxHealth = 30;

    // Sprite
    private Transform tempLocation;
    private SpriteRenderer spriteSwitcher;
    public Sprite smallSprite;
    private float DistanceToPlayer;
    public Sprite largeSprite;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    // Start
    ////////////////
    void Start() {
        // Set Stats
        healthPool = maxHealth;
        monsterSpeed = 2f;
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
        if (stunned == false){
             transform.right = playerLocation.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, currentSpeed * Time.deltaTime);
        }
        if (DistanceToPlayer <= 2.0f)
        {
            BoundAttack();
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
    ///////////////////////////////////
    //BoundAttack
    ///////////////////////////////////
    public void BoundAttack()
    {
           transform.right = playerLocation.position;
           playerLocation = tempLocation;
           //gameObject.AddForce(transform.right * Thrust);
    }
    //It's collision.. Sadly I'm using Linux and Unity isn't being very friendly so I have no way of testing this yet. 
    //Once I get home I'll be able to actually fix and run this and hopefully have something running.
    //void OnCollisionEnter2D(Collision coll)
    //{
        //if(coll.tag == "Player")
        // thePlayer.GetComponent<userClass>().TakeDamage(5.0f);
        //else if (coll.tag == "wall")
        //  this.takeDamage(10.0f);
      
    //}
}
