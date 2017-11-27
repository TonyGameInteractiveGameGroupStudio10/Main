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

    public GameObject specialTelegraph;

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

        // Check which Hit Box to load
        if (hasDrop == true){
            // Large Hit Box
            GetComponent<BoxCollider2D>().size = new Vector2(0.12f,0.12f);
        } else {
            // Small Hit Box
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
    public int GetMaxHealth(){
        return this.maxHealth;
    }

    public void SetMaxHealth(int newMaxHealth){
        this.maxHealth = newMaxHealth;
    }

    // Attack
    ////////////////
    public override void SpecialMove(){
        envenom();
    }

    public void envenom(){
        // find a random square in range of +/- 2 squares from the player
        float randomx = Random.Range(playerLocation.position.x-2f, playerLocation.position.x+2f);
        float randomy = Random.Range(playerLocation.position.y-2f, playerLocation.position.y+2f);
        Vector3 randomLocation = new Vector3(randomx,randomy,0);
        // Create telegraph at the location the posion will be coming from
        Vector3 scaledVector = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(randomLocation);
        Instantiate(specialTelegraph, scaledVector, Quaternion.identity);
        inSpecial = false;
    }
}