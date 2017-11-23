using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Fire
// - Fast
// - Low HP
// - Drops Fire
// - Cant be stunned
public class MonsterFire : MonsterClass {

    // Sprite
    public Sprite smallSprite;
    public Sprite largeSprite;
    public GameObject FlameSpit;
    public Transform FlameMouth;
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
        monsterType = 1;
        maxHealth = 15;
        healthPool = maxHealth;
        monsterSpeed = 3f;
        currentSpeed = monsterSpeed;

        // Check to see if it has a drop
        if (hasDrop == true){
            spriteSwitcher.sprite = largeSprite;
            enemyAnimator.SetBool("isLarge", true);
        } else {
            spriteSwitcher.sprite = smallSprite;
        }  
        // Check which sprite to load
        // large sprite = has a drop ; small sprite = has no drop
        Debug.Log(hasDrop + " in Monster Type");
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
        BlastingCannon();
    }

    private void BlastingCannon()
      {
        // Find player location
        FiringDirection = (playerLocation.position - FlameMouth.position).normalized;
        // Set the firing direction
        FlameSpit.GetComponent<WeaponFire>().setFiringDirection(FiringDirection);
        // Instantiate and save the object
        GameObject tempShot = Instantiate(FlameSpit, FlameMouth.position, FlameMouth.rotation);
        // Rotate the object
        tempShot.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), FiringDirection);
        
        inSpecial = false;
    }
}
