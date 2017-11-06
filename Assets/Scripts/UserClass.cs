using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requirments:
[RequireComponent (typeof (Rigidbody2D))]

// Main Class for the Player
public class UserClass : MonoBehaviour {

	// Stats
	////////////////
	private int healthPool;  
	private int maxHealthPool; // 40
	private float currentSpeed;
	private float playerSpeed; // 4f
	private Vector2 targetVelocity;

    // Status Effects
    ///////////////
    protected int poison;
    protected float poisonTimer; // 6 seconds
    protected bool[] poisonDamage = new bool[3];

	// Items
	////////////////
	// 0 - clear; 1 - haste; 2 - health
	private int[] potion = new int[3];
	private UserPotion potionModule;
	// 0 - Attack Speed ;
	private int[] weaponMod = new int[1]; 
	private UserWeaponMod weaponModule; //
	// 0 - Posion ; 1 - vine ; 2 - shock ;
	// 3 - quaking ; 4 - ricochet
	public int[] attackMod = new int[5];
	private UserAttackMod attackModule;

	// Arrow
	////////////////
	private bool recheckAttack;
	private bool recheckWeapon;
	private GameObject weaponPrefab;

	// Animator
	////////////////
	private Animator anim;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
		// Grab all the components needed
		anim = GetComponent<Animator>();
		weaponModule = GetComponent<UserWeaponMod>();
		attackModule = GetComponent<UserAttackMod>();
		potionModule = GetComponent<UserPotion>();
		// Reseting
		this.ResetHealth();
		this.ResetSpeed();
		this.ResetPotions();
		this.ResetWeaponMod();
		this.ResetAttackMod();
		this.poison = 0;
		// Set player to center stage
		transform.position = new Vector3(0,0,0);
	}

	// Update
	////////////////
	void Update(){
		// Potions
		// c - clear ; x - speed
		if (Input.GetKeyDown("c")) { 
			this.UsePotion(0); 
		}
		else if (Input.GetKeyDown ("x")) { 
			this.UsePotion(1); 
		}

		// Fire
		if (Input.GetKeyDown("space")){
			if (this.recheckAttack == true){
				weaponPrefab = attackModule.ApplyMod(attackMod);
				this.recheckAttack = false;
			}
			if (this.recheckWeapon == true){
				weaponModule.ApplyMod(weaponMod);
				this.recheckWeapon = false;
			}
			weaponModule.Fire(weaponPrefab, this.transform.Find("FiringPosition"));
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

        // Death
        if (healthPool <= 0){
        	
        }
	}

	// Fixed Update
	////////////////
	void FixedUpdate(){
		// Movement
		this.targetVelocity = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		GetComponent<Rigidbody2D>().velocity = targetVelocity * currentSpeed;

		// Rotate
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(new Vector3(0,0,1), transform.position - mousePos);
	}

	////////////////////////////////
	// Methods 
	////////////////////////////////
	// Status Effects
    ////////////////
    protected void Poisoned(){
        int tick = 2*this.poison;
        this.TakeDamage(tick);
    }

    public void ReceivingPoison(){
        if (this.poison < 3){
            this.poison += 1;
        }
        this.poisonTimer = 6f;
        for (int i = 0; i < poisonDamage.Length; i += 1){
            poisonDamage[i] = false;
        }
    }

	// Health
	////////////////
	public int GetHealth(){ 
		return this.healthPool; 
	}

	public void SetHealth(int newHealth){ 
		this.healthPool = newHealth; 
	}

	public int GetMaxHealth(){ 
		return this.maxHealthPool; 
	}

	public void SetMaxHealth(int newMaxHealth){ 
		this.maxHealthPool =  newMaxHealth; 
	}

	public void TakeDamage(int incomingDamage){ 
		this.healthPool -= incomingDamage;
	}

	public void ResetHealth(){
		this.SetMaxHealth(40);
		this.SetHealth(maxHealthPool);
	}

	// Speed
	////////////////
	public float GetSpeed(){
		return this.playerSpeed;
	}

	public void SetSpeed(float newSpeed){
		this.playerSpeed = newSpeed;
	}

	public float GetCurrentSpeed(){
		return this.currentSpeed;
	}

	public void SetCurrentSpeed(float newSpeed){
		this.currentSpeed = newSpeed;
	}

	public void ResetSpeed(){
		this.SetSpeed(4f);
		this.SetCurrentSpeed(4f);
	}

	// Potion
	////////////////
	public int GetPotion(int indexPotion) { 
		return this.potion[indexPotion];
	}

	public void SetPotion(int indexPotion, int newPotionCount) {
		this.potion[indexPotion] = newPotionCount;
	}

	public void GivePotion(int incomingPotion){ 
		this.potion[incomingPotion] += 1;
	}

	public void ResetPotions(){
		for (int c = 0; c < 3; c += 1){
			this.SetPotion(c,0);
		}
	}

	public void UsePotion(int outgoingPotion){ 
		if (this.GetPotion(outgoingPotion) <= 0){
			// Notify GM to display "out of potion message"
		}
		else {
			this.potion[outgoingPotion] -= 1; 
			// send to potion module ---->
		}
	}

	// WeaponMod
	////////////////
	public int GetWeaponMod(int indexWeaponMod){
		return this.weaponMod[indexWeaponMod];
	}

	public int[] GetWeaponModFull(){
		return this.weaponMod;
	}

	public void SetWeaponMod(int indexWeaponMod, int newWeaponMod){
		this.weaponMod[indexWeaponMod] = newWeaponMod;
		this.recheckWeapon = true;
	}

	public void GiveWeaponMod(int indexWeaponMod){
		this.weaponMod[indexWeaponMod] += 1;
		this.recheckWeapon = true;
	}

	public void ResetWeaponMod(){
		for (int c = 0; c < 1; c += 1){
			this.SetWeaponMod(c, 0);
		}
		this.recheckWeapon = true;
	}

	// AttackMod
	////////////////
	public int GetAttackMod(int indexAttackMod) {
		return this.attackMod[indexAttackMod];
	}

	public int[] GetAttackModFull(){
		return this.attackMod;
	}

	public void SetAttackMod(int indexAttackMod, int newAttackMod) {
		this.attackMod[indexAttackMod] = newAttackMod;
		this.recheckAttack = true;
	}

	public void GiveAttackMod(int indexAttackMod) {
		this.attackMod[indexAttackMod] += 1;
		this.recheckAttack = true;
	}

	public void ResetAttackMod(){
		for (int c = 0; c < 1; c += 1) {
			this.SetAttackMod(c, 0);
		}
		this.recheckAttack = true;
	}
}
