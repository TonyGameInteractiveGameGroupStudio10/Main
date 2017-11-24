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

    // Sprite
    public Sprite smallSprite;
    public Sprite largeSprite;

    // Other
    private Transform tempLocation;
    private Transform enemyPosition;
    private bool Bounding;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    // Start
    ////////////////
    protected override void Start() {
        // Run MonsterClass Start()
        base.Start();
        // Set Stats
        monsterType = 0;
        maxHealth = 25;
        healthPool = maxHealth;
        monsterSpeed = 2f;
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
        this.BoundAttack();
    }

    public void BoundAttack(){
        Debug.Log("entered bound attack");
        Vector2 newVector2 = playerLocation.position - this.transform.position;
        enemyBody.AddForce(newVector2 * currentSpeed * 75);
        Bounding = true;
        Invoke("Stop", 0.5f);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Bounding == true)
        {
            collision.gameObject.SendMessage("TakeDamage", 10);
        }
        else if (collision.gameObject.tag == "Enemy" && Bounding == true) 
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "DropWall" && Bounding == true)
        {
            Destroy(collision.gameObject);
        }
    }

    public void Stop(){
        enemyBody.velocity = new Vector3(0,0,0);
        Bounding = false;
        inSpecial = false;
    }
}
