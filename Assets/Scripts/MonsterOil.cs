using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Oil
// - Slow
// - Medium HP
// - Drop oil
// - Can't Be Slowed
public class MonsterOil : MonsterClass { 

    // Sprite
    public Sprite smallSprite;
    public Sprite largeSprite;

    public GameObject OilSpit;
    public Transform OilMouth;
    private Vector2 FiringDirection;
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

        // Set the box collider
        if (hasDrop == true){
            // Large sprite
            GetComponent<BoxCollider2D>().size = new Vector2(0.14f,0.12f);
        } else {
            // Small sprite
            GetComponent<BoxCollider2D>().size = new Vector2(0.14f,0.09f);
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
        OilBomb();
    }

    private void OilBomb()
    {
        //find location to shoot
        FiringDirection = (playerLocation.position - OilMouth.position).normalized;
        //set the direction in which to fire
        OilSpit.GetComponent<WeaponOil>().SetFiringDirection(FiringDirection);
        //Instantiate and save the object
        GameObject tempShot = Instantiate(OilSpit, OilMouth.position, OilMouth.rotation);
        //leave special attack state
        inSpecial = false;
    }
}
