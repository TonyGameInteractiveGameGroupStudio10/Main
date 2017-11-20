using System.Collections;
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
    protected TreeNode treeRoot;

    // Pathing/Decisions
    //////////////////////
    protected int numberOfMoves = 0;
    protected Vector3 goalPosition;
    protected List<Vector3> listOfMovement;
    protected bool inAction;
    protected bool inSpecial;

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

        this.BuildTree();       

        // Locate Player
        thePlayer = GameObject.FindWithTag("Player");
        playerLocation = thePlayer.transform;

        // Find the first path, then search every second
        inAction = false;
    }

    // Update
    ////////////////
    protected virtual void Update(){

        // if not stunned
        if (stunned == false){
            // Make a decision
            if (inSpecial == false) {
                this.MakeDecision();
            }
            // Verify the list isn't empty, more then current square, and isn't in action
            if((listOfMovement != null) && (listOfMovement.Count > 1) && (inAction == false)){
                if (numberOfMoves < listOfMovement.Count) {
                    goalPosition = listOfMovement[numberOfMoves];
                    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), goalPosition, (currentSpeed * Time.deltaTime));
                    if ((transform.position.x == goalPosition.x) && (transform.position.y == goalPosition.y)){
                        numberOfMoves += 1;
                    }
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
        if (diceRoll <= 2){ // 2 2%
            this.potionDrop = true; 
            // 0 - clear; 1 - haste; 2 - health;
            this.dropIndex = Random.Range(0,3);
        }
        else if (diceRoll >= 3 && diceRoll <= 4){ // 3 4 2%
            this.weaponModDrop = true;
            // 0 -  Attack Speed ;
            this.dropIndex = 0;
        }
        else if (diceRoll >= 5 && diceRoll <= 6){ // 5 6 2%
            this.attackModDrop = true;
            // 0 - Posion ; 1 - vine ; 2 - shock ; 3 - quaking ; 4 - ricochet;
            this.dropIndex = Random.Range(0,5);
        }
        else if (diceRoll >= 7 && diceRoll <= 43){ // 7 57 50%
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
            Vector3 scaledVector = gameMaster.GetComponent<InfluenceMap>().scaleWorldPos(transform.position);
            Instantiate(environDrop,scaledVector,Quaternion.identity);
            // if the monster type isn't ice, add it to the map
            if (monsterType != 4){
                gameMaster.GetComponent<InfluenceMap>().addNode(transform.position,monsterType);
            }
        }
    }

    public virtual void SpecialMove(){
        // method is overridden in the children classes
    }

    // Decisions
    ///////////////
    // Decision Start/Selector
    protected void MakeDecision(){
        treeRoot.Search();
    }

    // Is the target in ray cast
    protected bool InRayCast(){
        RaycastHit2D hit = Physics2D.Linecast(transform.position, playerLocation.position);
        if (hit.collider == null){
            return false;
        } else if (hit.collider.gameObject.tag == "Player") {
			return true;
		} else {
			return false;
		}
    }

    // Is the target in range
    protected bool InRange(){
        float distance =  Vector3.Distance(this.transform.position, playerLocation.position);
        if (distance < 10) {
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
        int diceRoll = Random.Range(0,100);
        if (diceRoll < 1){
            inSpecial = true;
            return true;
        } else {
            return false;
        }

    }

    // Check if there are nearby friendlies
    protected bool FriendInRange(){
        GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < friendlies.Length; i += 1){
            // Verify the object isn't itself
            if (!GameObject.ReferenceEquals(friendlies[i], this.gameObject)){
                float distance = Vector3.Distance(this.transform.position, friendlies[i].transform.position);
                if (distance < 2){
                    return true;
                }
            }
        }
       return false;
    }

    // Check to see if the
    protected bool HPLow(){
        float PercentagePool = (maxHealth*(0.15f));
        if (PercentagePool >= healthPool) {
            return true;
        } else {
            return false;
        }
    }

    // Actions
    ////////////////
    // Find the path
    protected void FindPath(){
        inAction = false;
        listOfMovement = movementPlan.FindPath(transform.position, monsterType);
        numberOfMoves = 1;
    }

    // Attack the player
    protected void Attack(){
        inAction = true;
        transform.right = playerLocation.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, playerLocation.position, currentSpeed * Time.deltaTime);
    }

    // Run from the player
    protected void Retreat(){
        // RUN AWAY
    }

    // Move towards the player
    protected void Advance(){
        inAction = true;
        transform.right = playerLocation.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, playerLocation.position, currentSpeed * Time.deltaTime);
    }

    // Cast the special
    protected void Special(){
        inAction = true;
        this.SpecialMove();
    }

    // Building The Tree
    ////////////////////////
    protected void BuildTree(){
        // Decisions
        TreeNode rayCastNode = new TreeNode();
        rayCastNode.SetDecision(InRayCast);

        TreeNode rangeNode = new TreeNode();
        rangeNode.SetDecision(InRange);

        TreeNode specialCheckNode = new TreeNode();
        specialCheckNode.SetDecision(SpecialCheck);

        TreeNode specialRangeNode = new TreeNode();
        specialRangeNode.SetDecision(SpecialAttackRange);

        TreeNode attackRangeNode = new TreeNode();
        attackRangeNode.SetDecision(AttackRange);

        TreeNode hpNode = new TreeNode();
        hpNode.SetDecision(HPLow);

        TreeNode friendNode = new TreeNode();
        friendNode.SetDecision(FriendInRange);

        // Actions
        TreeNode pathFindNode = new TreeNode();
        pathFindNode.SetAction(FindPath);

		TreeNode attackNode = new TreeNode();
		attackNode.SetAction (Attack);

        TreeNode retreatNode = new TreeNode();
        retreatNode.SetAction(Retreat);

        TreeNode advanceNode = new TreeNode();
        advanceNode.SetAction(Advance);

        TreeNode specialNode = new TreeNode();
        specialNode.SetAction(Special);

        // Assemble
        rayCastNode.SetRight(rangeNode);
        rayCastNode.SetLeft(pathFindNode);

        rangeNode.SetRight(attackRangeNode);
        rangeNode.SetLeft(hpNode);

        attackRangeNode.SetRight(attackNode);
        attackRangeNode.SetLeft(specialRangeNode);

        specialRangeNode.SetRight(specialCheckNode);
        specialRangeNode.SetLeft(advanceNode);

        specialCheckNode.SetRight(specialNode);
        specialCheckNode.SetLeft(advanceNode);

        hpNode.SetRight(friendNode);
        hpNode.SetLeft(pathFindNode);

        friendNode.SetRight(pathFindNode);
        friendNode.SetLeft(retreatNode);

        // Set the tree root
        treeRoot = rayCastNode;
    }


}
