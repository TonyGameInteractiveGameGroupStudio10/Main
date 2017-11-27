using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIce : MonoBehaviour {
    public float speed = 5.0f;
    public Vector3 FiringDirections;


	// Use this for initialization
		
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1.0f);
	}
    public void SetFiringDirection(Vector3 Direction)
    {
        FiringDirections = Direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", 2);
            collision.gameObject.SendMessage("SetSpeed", 0.0f);
         //   yield new WaitForSeconds(0.5f);
            collision.gameObject.SendMessage("SetSpeed", 4.0f);
            Destroy(gameObject);
        }
        /*  if (collision.gameObject.tag == "Enemy")
          {
              collision.gameObject.SendMessage("TakeDamage", 5);
              Destroy(gameObject);
          }*/
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "DropWall")
        {
            Destroy(gameObject);
        }
    }
}
