using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Oil
// - Slow
// - Medium HP
// - Drop oil
// - Can't Be Slowed
public class MonsterOil : MonsterClass {

    //Distance to player and temp location
    private int DistanceToPlayer;
    public Transform TempLocation;

    // Sprite
    public Sprite smallSprite;
    public Sprite largeSprite;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    // Start
    ////////////////
    protected override void Start() {
        // Run MonsterClass Start()
        base.Start();

        // Set Stats
        monsterType = 3;
        maxHealth = 20;
        healthPool = maxHealth;
        monsterSpeed = 2.5f;
        currentSpeed = monsterSpeed;

        // Check which sprite to load
        // large sprite = has a drop ; small sprite = has no drop
        if (hasDrop == true){
            spriteSwitcher.sprite = largeSprite;
        } else {
            spriteSwitcher.sprite = smallSprite;
        }
    }

    // Update
    ////////////////
    protected override void Update(){
        // Run MonsterClass Update()
        base.Update();

        //Attack
        if (DistanceToPlayer <= 8)
        {
            //OilSling();   
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

    // Attack
    ////////////////
    public override void SpecialMove(){
        inSpecial = false;
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
