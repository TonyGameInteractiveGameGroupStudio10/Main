using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

    public float speed = 5.0f;
    public Vector3 FiringDirection;

	// Use this for initialization
	
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Rigidbody2D>().velocity = FiringDirection.normalized * speed;
        Destroy(gameObject, 7);
	}
    public void setFiringDirection(Vector3 Direction)
    {
        FiringDirection = Direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            collision.gameObject.SendMessage("TakeDamage", 5);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<MonsterClass>().getType() != 1)
        {
            collision.gameObject.SendMessage("TakeDamage", 5);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "DropWall")
        {
            Destroy(gameObject);
        }
    }
}
