﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster Class that all monster classes are to be built from
public class MonsterClass : MonoBehaviour {

    // Stats
    ////////////////
    protected int monsterType;
    protected int healthPool;
    protected int maxHealth;
    protected float currentSpeed;
    protected float monsterSpeed;

    // Status Effects
    ///////////////
    protected int poison;
    protected float poisonTimer; // 6 seconds
    protected bool[] poisonDamage = new bool[3];
    protected bool stunned; 
    protected float stunTimer; // 1 seconds

    // Components
    ////////////////
    protected Rigidbody2D enemyBody;
    protected Animator enemyAnimator;
    protected SpriteRenderer spriteSwitcher;
    protected PathFinder movementPlan;
    protected GameObject gameMaster;

    // Pathing
    ////////////////
    protected int numberOfMoves = 0;
    protected Vector3 goalPosition;
    protected List<Vector3> listOfMovement;

    // Player
    ////////////////
    protected GameObject thePlayer;
    protected Transform playerLocation;

    // Audio
    ///////////////
    public AudioClip deathCry;
    protected AudioSource audioPlayer;

    // Effects
    ////////////////
    protected int dropIndex;
    protected bool potionDrop;
    protected bool attackModDrop;
    protected bool weaponModDrop;
    protected bool environmentDrop;
    public GameObject environDrop;

    // Prefab
    ////////////////
    public GameObject potionPrefab;
    public GameObject attackModPrefab;
    public GameObject weaponModPrefab;

    ///////////////////////////////////
    // Unity Methods
    ///////////////////////////////////
    // Start
    ////////////////
    protected virtual void Start(){
        // Grab Components
        enemyAnimator = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody2D>();
        audioPlayer = GetComponent<AudioSource>();
        spriteSwitcher = GetComponent<SpriteRenderer>();
        movementPlan = GetComponent<PathFinder>();
		gameMaster = GameObject.FindWithTag("GameMaster");

        // Locate Player
        thePlayer = GameObject.FindWithTag("Player");
        playerLocation = thePlayer.transform;

        // Find the first path, then search every second
        InvokeRepeating("FindPath", 1f, 0.2f);
    }

    // Update
    ////////////////
    protected virtual void Update(){
        // Movement
        // Verify the list isn't empty
        if((listOfMovement != null) && (listOfMovement.Count > 1)){
            if (numberOfMoves < listOfMovement.Count) {
                goalPosition = listOfMovement[numberOfMoves];
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), goalPosition, (currentSpeed * Time.deltaTime));

                if ((transform.position.x == goalPosition.x) && (transform.position.y == goalPosition.y)){
                        numberOfMoves += 1;
                }
            }
        }

        // if HP is less then 0
        if (healthPool <= 0){
            this.Die();
        }
    }

    ///////////////////////////////////
    // Methods
    ///////////////////////////////////
    // Health
    ////////////////
    public int GetHealth(){
        return this.healthPool;
    }

    public void SetHealth(int newHealth){
        this.healthPool = newHealth;
    }

    public void TakeDamage(int incomingDamage){
        this.healthPool -= incomingDamage;
		Debug.Log(incomingDamage);
    }

    public void Die(){
        this.effectDropper();
        Destroy(gameObject);
    }

    // Speed
    ////////////////
    public float GetCurrentSpeed(){
        return this.currentSpeed;
    }

    public void SetCurrentSpeed(float newSpeed){
        this.currentSpeed = newSpeed;
    }

    public float GetMonsterSpeed(){
        return this.monsterSpeed;
    }

    public void SetMonsterSpeed(float newSpeed){
        this.monsterSpeed = newSpeed;
    }

    // Status Effects
    ////////////////
    protected void Poisoned(){
        int tick = 2*this.poison;
        this.TakeDamage(tick);
    }

    public void ReceivingPoison(){
        if (this.poison < 5){
            this.poison += 1;
        }
        this.poisonTimer = 6f;
        for (int i = 0; i < poisonDamage.Length; i += 1){
            poisonDamage[i] = false;
        }
    }

    public void ReceivingStun(){
        this.stunned = true;
        this.stunTimer = 1f;
    }

    // Effects/Items
    ////////////////
    protected void effectRoller(){
        int diceRoll = Random.Range(1,101);
        // Roll values are currently temp, this is more of a skeleton
        if (diceRoll <= 2){ // 2
            this.potionDrop = true; 
            // 0 - clear; 1 - haste; 2 - health;
            this.dropIndex = Random.Range(0,3);
        }
        else if (diceRoll >= 3 && diceRoll <= 4){ // 3 4
            this.weaponModDrop = true;
            // 0 -  Attack Speed ;
            this.dropIndex = 0;
        }
        else if (diceRoll >= 5 && diceRoll <= 6){ // 5 6
            this.attackModDrop = true;
            // 0 - Posion ; 1 - vine ; 2 - shock ; 3 - quaking ; 4 - ricochet;
            this.dropIndex = Random.Range(0,5);
        }
        else if (diceRoll >= 7 && diceRoll <= 100){ // 7 12
            // doesn't have a drop table because each environment drop is unique to
            // the monster that it is being dropped by
            this.environmentDrop = true;
        }
    }

    protected void effectDropper(){
        // If any dropper has been marked true, the instantiate a drop of that type
        if (this.potionDrop == true){
            GameObject potionTemp = Instantiate(potionPrefab,transform.position,Quaternion.identity);
            potionTemp.GetComponent<ItemPotion>().SetItemIndex(dropIndex);
        }
        else if(this.weaponModDrop == true){
            GameObject weaponTemp = Instantiate(weaponModPrefab,transform.position,Quaternion.identity);
            weaponTemp.GetComponent<ItemWeaponMod>().SetItemIndex(dropIndex);
        }
        else if(this.attackModDrop == true){
            GameObject attackTemp = Instantiate(attackModPrefab,transform.position,Quaternion.identity);
			attackTemp.GetComponent<ItemAttackMod>().SetItemIndex(dropIndex);
        }
        else if (this.environmentDrop == true){
            //IntVector2 tempVec = gameMaster.GetComponent<InfluenceMap>().worldPosToGrid(transform.position);
            //Vector3 tempVec3 = new Vector3(tempVec.x/2, tempVec.y/2,0);
            Instantiate(environDrop,transform.position,Quaternion.identity);
            gameMaster.GetComponent<InfluenceMap>().addNode(transform.position,monsterType);
        }
    }

    // Decisions
    ///////////////
    // Is the target in ray cast
    protected bool InRayCast(){
        if (){
            return true;
        } else {
            return false;
        }
    }

    // Is the target in range
    protected bool InRange(){
        float distance =  Vector3.Distance(this.transform.position, playerLocation.position);
        if (distance < 8) {
            return true;
        } else {
            return false;
        }
    }

    // Is the player in attack range
    protected bool AttackRange(){
        float distance =  Vector3.Distance(this.transform.position, playerLocation.position);
        if (distance < 3) {
            return true;
        } else {
            return false;
        }

    }

    // If the target isn't close enough
    protected bool SpecialAttackRange(){
        float distance =  Vector3.Distance(this.transform.position, playerLocation.position);
        if (distance < 5) {
            return true;
        } else {
            return false;
        }
    }

    // Roll to see if you can cast special
    protected bool SpecialCheck(){

    }

    // Check if there are nearby friendlies
    protected bool FriendInRange(){

    }

    // Check to see if the
    protected bool HPLow(){
        float PercentagePool = (maxHealth*(0.15));
        if (PercentagePool >= healthPool) {
            return true;
        } else {
            return false;
        }
    }

    // Check to see if there a swarm
    protected bool SwarmHost(){
        if () {
            return true;
        } else {
            return false;
        }
    }

    // Actions
    ////////////////
    // Find the path
    protected void FindPath(){
       listOfMovement = movementPlan.FindPath(transform.position);
       numberOfMoves = 1;
    }

    // Attack the player
    protected void Attack(){

    }

    // Run from the player
    protected void Retreat(){

    }

    // Move towards the player
    protected void Advance(){

    }

    // Cast the special
    protected void Special(){

    }

    // Go to swarm
    protected void GoToSwarm(){

    }

    // Building
    ////////////////
    protected void BuildTree(){

    }


}
