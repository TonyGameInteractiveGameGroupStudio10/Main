using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Poison
// - Medium Speed
// - Medium HP
// - Drop Poison
// - Can't be poisoned
public class MonsterPoison : MonsterClass {

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
        monsterType = 2;
		maxHealth = 20;
        healthPool = maxHealth;
        monsterSpeed = 2.8f;
        currentSpeed = monsterSpeed;

        // Set the box collider
        if (hasDrop == true){
            // Large sprite
            GetComponent<BoxCollider2D>().size = new Vector2(0.12f,0.13f);
        } else {
            // Small sprite
            GetComponent<BoxCollider2D>().size = new Vector2(0.1f,0.1f);
        }
    }

    // Update
    ////////////////
    protected override void Update(){
        // Run MonsterClass Update()
        base.Update();

        // Stun Timer
        if (this.stunTimer > 0){
            this.stunTimer -= Time.deltaTime;
            if (this.stunTimer <= 0){
                this.stunned = false;
            }
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
}