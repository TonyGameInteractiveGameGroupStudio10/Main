using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIce : MonoBehaviour {
    public Vector3 FiringDirections;


	// Use this for initialization
		
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4.0f);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", 2);
            collision.gameObject.SendMessage("SetSpeed", 0.0f);
            StartCoroutine(ResetSpeed());
            collision.gameObject.SendMessage("SetSpeed", 4.0f);
        }
        /*  if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<MonsterClass>().getType() != 4)
          {
              collision.gameObject.SendMessage("TakeDamage", 2);
              collision.gameObject.SendMessage("SetCurrentSpeed", 0.0f);
              StartCoroutine(ResetSpeed());
              collision.gameObject.SendMessage("SetCurrentSpeed", 2.0f);
          }*/
    }
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
