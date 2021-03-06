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
    public GameObject ColdAsIce;
    public Transform ColdMouth;
    private GameObject tempIce;
    public Vector2 Direction;

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

        // Check which hitbox to load & raycasters
        float vx = transform.position.x;
        float vy = transform.position.y;
        if (hasDrop == true){
            // Large hit box
            GetComponent<BoxCollider2D>().size = new Vector2(0.12f,0.14f);
            topLine.transform.position = new Vector3(vx,vy+0.45f,0f);
            bottomLine.transform.position = new Vector3(vx,vy-0.45f,0f);
        } else {
            // Small hit box
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

        if (tempIce != null){
             tempIce.transform.position = ColdMouth.position;
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
    public int GetMaxHealth(){
        return this.maxHealth;
    }

    public void SetMaxHealth(int newMaxHealth){
        this.maxHealth = newMaxHealth;
    }

    // Attack
    ////////////////
    public override void SpecialMove(){
        AbsoluteZero();
    }

    public void AbsoluteZero(){
        tempIce = Instantiate(ColdAsIce, ColdMouth.position, ColdMouth.rotation);
        inSpecial = false;
    }
}