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
    public Sprite smallSprite;
    public Sprite largeSprite;

    // Other
    private Transform tempLocation;
    private Transform enemyPosition;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    // Start
    ////////////////
    protected override void Start() {
        // Run MonsterClass Start()
        base.Start();
        Physics2D.IgnoreCollision(enemyBody.GetComponent<Collider2D>(), thePlayer.GetComponent<CircleCollider2D>());
        // Set Stats
        monsterType = 0;
        healthPool = maxHealth;
        monsterSpeed = 2f;
        currentSpeed = monsterSpeed;

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
    protected override void Update(){
        // Rune MonsterClass Update()
        base.Update();
        //DistanceToPlayer = playerLocation - enemyPosition;
       // if (gameObject.transform == playerLocation)
        //{
          //  BoundAttack();
        //}

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
        Vector2 newVector2 = playerLocation.position - this.transform.position;
        enemyBody.AddForce(newVector2 * currentSpeed * 25);
        Debug.Log("YAYAYAYAYA");
    }
    //It's collision.. Sadly I'm using Linux and Unity isn't being very friendly so I have no way of testing this yet. 
    //Once I get home I'll be able to actually fix and run this and hopefully have something running.
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "AttackDistance")
        {
            Debug.Log("Hello");
            BoundAttack();
        }
        
      
    }
}
