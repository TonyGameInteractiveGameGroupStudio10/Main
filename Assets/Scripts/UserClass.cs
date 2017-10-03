using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requirments:
[RequireComponent (typeof (Rigidbody2D))]

public class UserClass : MonoBehaviour {

	// Stats
	////////////////
	private int healthPool;  
	private int maxHealthPool; // 40
	private float currentSpeed;
	private float playerSpeed; // 4f
	private Vector2 targetVelocity;

	// Items
	////////////////
	// 0 - clear; 1 - speed
	private int[] potion = new int[2];
	private UserPotion potionModule;
	// 0 - effect1 ; 1 - effect2 ; 2 - effect3;
	private int[] weaponMod = new int[5]; 
	private UserWeaponMod weaponModule; //
	// 0 - effect1 ; 1 - effect2 ; 2 - effect3 ;
	private int[] attackMod = new int[5];
	private UserAttackMod attackModule;

	// Arrow
	////////////////
	private bool recheckAttack;
	private bool recheckWeapon;
	private GameObject weaponPrefab;
	// Effects
	////////////////
	// 0 - ; 1 - ; 2 -
	// 3 - ; 4 - ; 5 -
	//private gameObject[] currentAilments = new gameObject[20]; // change and activate once created

	// Animator
	////////////////
	private Animator anim;

	///////////////////////////////////
	// Unity Methods
	///////////////////////////////////
	// Start
	////////////////
	void Start(){
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

		// Attack
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
	}

	// Fixed Update
	////////////////
	void FixedUpdate(){
		this.targetVelocity = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		GetComponent<Rigidbody2D> ().velocity = targetVelocity * currentSpeed;
	}

	////////////////////////////////
	// Methods 
	////////////////////////////////
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
		this.SetHealth(40);
		this.SetMaxHealth(40);
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

	public void UsePotion(int outgoingPotion){ 
		if (this.GetPotion(outgoingPotion) <= 0){
			// Notify GM to display "out of potion message"
		}
		else {
			this.potion[outgoingPotion] -= 1; 
			// send to potion module ---->
		}
	}

	public void ResetPotions(){
		for (int c = 0; c <=1; c += 1){
			this.SetPotion(c,0);
		}
	}

	// WeaponMod
	////////////////
	public int GetWeaponMod(int indexWeaponMod){
		return this.weaponMod[indexWeaponMod];
	}

	public void SetWeaponMod(int indexWeaponMod, int newWeaponMod){
		this.weaponMod[indexWeaponMod] = newWeaponMod;
		this.recheckWeapon = true;
	}

	public void GiveWeaponMod(int indexWeaponMod){
		this.weaponMod[indexWeaponMod] += 1;
		this.recheckWeapon = true;
	}

	public void TakeWeaponMod(int indexWeaponMod){
		if (this.GetWeaponMod(indexWeaponMod) > 0){
			this.weaponMod[indexWeaponMod] -= 1;
		}
		this.recheckWeapon = true;
	}

	public int[] GetWeaponModFull(){
		return this.weaponMod;
	}

	public void ResetWeaponMod(){
		for (int c = 0; c <= 4; c += 1){
			this.SetWeaponMod(c, 0);
		}
		this.recheckWeapon = true;
	}

	// AttackMod
	////////////////
	public int GetAttackMod(int indexAttackMod) {
		return this.attackMod[indexAttackMod];
	}

	public void SetAttackMod(int indexAttackMod, int newAttackMod) {
		this.attackMod[indexAttackMod] = newAttackMod;
		this.recheckAttack = true;
	}

	public void GiveAttackMod(int indexAttackMod) {
		this.attackMod[indexAttackMod] += 1;
		this.recheckAttack = true;
	}

	public void TakeAttackMod(int indexAttackMod) {
		if (this.GetAttackMod(indexAttackMod) > 0){
			this.attackMod[indexAttackMod] -= 1;
		}
		this.recheckAttack = true;
	}

	public int[] GetAttackModFull(){
		return this.attackMod;
	}

	public void ResetAttackMod(){
		for (int c = 0; c <= 4; c += 1) {
			this.SetAttackMod(c, 0);
		}
		this.recheckAttack = true;
	}
}
