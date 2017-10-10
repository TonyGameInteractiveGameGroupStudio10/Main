using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClass : MonoBehaviour {

    Transform target;
    

    private int healthPool;
    private int maxHealth;
    private float currentSpeed; //4.2f


    private Rigidbody2D enemyBody;

    private Animator enemyAnimator;

    // Use this for initialization
    void Start() {
        enemyAnimator = GetComponent<Animator>();
        currentSpeed = 4.2f;
        enemyBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate() {

        target = GameObject.FindWithTag("Player").transform;
       
        //enemyBody.velocity = target.position * currentSpeed;
        while (Vector3.Distance(transform.position, target.position) >= 0.1f)
        {
           enemyBody.velocity = target.position * currentSpeed;
        }

    }
    public int GetHealth()
    {
        return this.healthPool;
    }

    public void SetHealth(int newHealth)
    {
        this.healthPool = newHealth;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        this.maxHealth = newMaxHealth;
    }

    public void TakeDamage(int incomingDamage)
    {
        this.healthPool -= incomingDamage;
    }

    public void ResetHealth()
    {
        this.SetHealth(40);
        this.SetMaxHealth(40);
    }
}
