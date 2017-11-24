using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Ice
// - Medium Speed
// - High HP
// - Drops water/fuel/maybe ice?
public class MonsterIce : MonsterClass {

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
        monsterType = 4;
        maxHealth = 20;
        healthPool = maxHealth;
        monsterSpeed = 2.8f;
        currentSpeed = monsterSpeed;

        // Check which sprite to load
        // large sprite = has a drop ; small sprite = has no drop
        if (hasDrop == true){
            spriteSwitcher.sprite = largeSprite;
            GetComponent<BoxCollider2D>().size = new Vector2(0.12f,0.13f);
        } else {
            spriteSwitcher.sprite = smallSprite;
            GetComponent<BoxCollider2D>().size = new Vector2(0.11f,0.1f);
        }
    }

    // Update
    ////////////////
    protected override void Update(){
        // Run MonsterClass Update()
        base.Update();

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