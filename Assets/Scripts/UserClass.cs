using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Requirments:
[RequireComponent (typeof (Rigidbody2D))]

// Main Class for the Player
public class UserClass : MonoBehaviour {

	// Stats
	////////////////
	private int healthPool;  
	private int maxHealthPool; // 60
	private float playerSpeed;
	private float maxSpeed; // 4f
	private Vector2 targetVelocity;
    private bool Stunned;

	// UI
	///////////////
	public Slider hpSlider;
	private bool damaged;
	public Image damageImage;
	private Color flashColour = new Color(1f,0f,0f,0.1f);
	private float flashSpeed = 5f;
	public Text healthPotionCount;
	public Text hastePotionCount;
	public Text clearPotionCount;
	public Image poisonImage;
	public Image shockImage;
	public Image vineImage;
	public Image quakeImage;
	public Image bounceImage;

    // Status Effects
    ///////////////
    private int poison;
    private float poisonTimer; // 6 seconds
    private bool[] poisonDamage = new bool[3];
    private bool isSlowed;

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
	private int[] attackMod = new int[5];
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
		// r - clear ; e - speed; q - health
		if (Input.GetKeyDown("r")) { 
			this.UsePotion(0); 
		}
		else if (Input.GetKeyDown ("e")) { 
			this.UsePotion(1); 
		}
		else if (Input.GetKeyDown("q")){
			this.UsePotion(2);
		}

		// Fire
		if ((Input.GetKey("space")) || (Input.GetKey(KeyCode.Mouse1))){
			if (this.recheckAttack == true){
				weaponPrefab = attackModule.ApplyMod(attackMod);
				this.recheckAttack = false;
			}
			if (this.recheckWeapon == true){
				weaponModule.ApplyMod(weaponMod);
				this.recheckWeapon = false;
			}
			if(weaponModule.Fire(weaponPrefab, this.transform.Find("FiringPosition"))){
				anim.SetTrigger("shoot");
			}
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

        // Health Stuff
        if (damaged == true){
        	damageImage.color = flashColour;
        } else {
        	damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

	// Fixed Update
	////////////////
	void FixedUpdate(){
		// Movement
		this.targetVelocity = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		GetComponent<Rigidbody2D>().velocity = targetVelocity * playerSpeed;
		anim.SetFloat("speed", GetComponent<Rigidbody2D>().velocity.magnitude);

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
		hpSlider.value = this.healthPool;
	}

	public int GetMaxHealth(){ 
		return this.maxHealthPool; 
	}

	public void SetMaxHealth(int newMaxHealth){ 
		this.maxHealthPool =  newMaxHealth; 
	}

	public void TakeDamage(int incomingDamage){ 
		this.healthPool -= incomingDamage;
		damaged = true;
		hpSlider.value = healthPool;
		// Death
        if (healthPool <= 0){
        	SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
	}

	public void ResetHealth(){
		this.SetMaxHealth(60);
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

	public float GetMaxSpeed(){
		return this.maxSpeed;
	}

	public void SetMaxSpeed(float newSpeed){
		this.maxSpeed = newSpeed;
	}

	public void ResetSpeed(){
		this.SetSpeed(4f);
		this.SetMaxSpeed(4f);
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
		// 0 - clear; 1 - haste; 2 - health
		if (incomingPotion == 0){
			clearPotionCount.text = this.GetPotion(incomingPotion).ToString();
		} else if (incomingPotion == 1){
			hastePotionCount.text = this.GetPotion(incomingPotion).ToString();
		} else {
			healthPotionCount.text = this.GetPotion(incomingPotion).ToString();
			hpSlider.value = healthPool;
		}
	}

	public void ResetPotions(){
		for (int c = 0; c < 3; c += 1){
			this.SetPotion(c,0);
		}
	}

	public void UsePotion(int outgoingPotion){ 
		// if they have a potion use it
		if (this.GetPotion(outgoingPotion) > 0) {
			// remove one from inventory
			this.potion[outgoingPotion] -= 1; 
			// send to potion module
			potionModule.Potion(outgoingPotion);
			// Update UI
			if (outgoingPotion == 0){
				clearPotionCount.text = this.GetPotion(outgoingPotion).ToString();
			} else if (outgoingPotion == 1){
				hastePotionCount.text = this.GetPotion(outgoingPotion).ToString();
			} else {
				healthPotionCount.text = this.GetPotion(outgoingPotion).ToString();
				anim.SetTrigger("heal");
			}
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
		this.TurnOnAttackSprite(indexAttackMod);
		this.recheckAttack = true;
	}

	private void TurnOnAttackSprite(int index){
		// 0 - Posion ; 1 - vine ; 2 - shock ;
		// 3 - quaking ; 4 - ricochet
		if (index == 0){
			poisonImage.enabled = true;
		} else if (index == 1){
			vineImage.enabled = true;
		} else if (index == 2){
			shockImage.enabled = true;
		} else if (index == 3){
			quakeImage.enabled = true;
		} else {
			bounceImage.enabled = true;
		}
	}

	public void ResetAttackMod(){
		for (int c = 0; c < 1; c += 1) {
			this.SetAttackMod(c, 0);
		}
		poisonImage.enabled = false;
		shockImage.enabled = false;	
		vineImage.enabled = false;
		quakeImage.enabled = false;
		bounceImage.enabled = false;
		this.recheckAttack = true;
	}
   
    public void SlowPlayer()
    {
        if (isSlowed == true)
        {
            SetSpeed(4.0f);
            isSlowed = false;
        }
        else { 
            SetSpeed(2.0f);
            isSlowed = true;
            StartCoroutine(ResetTimer());
        }
    }
    public void StunPlayer()
    {
        if(Stunned)
        {
            SetSpeed(4.0f);
            Stunned = false;
        }
        else
        {
            SetSpeed(0.0f);
            Stunned = true;
            StartCoroutine(StunReset());
        }
    }
    IEnumerator StunReset()
    {
        yield return new WaitForSeconds(1.0f);
        StunPlayer();
    }
    IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(2.0f);
        SlowPlayer();
    }
}
