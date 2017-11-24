using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOil : MonoBehaviour {

    public float speed = 5.0f;
    public Vector3 FiringDirection;
	
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Rigidbody2D>().velocity = FiringDirection.normalized * speed;
        Destroy(gameObject, 7);
	}
    public void SetFiringDirection(Vector3 Direction)
    {
        FiringDirection = Direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("Slowed");
            Debug.Log("Speed should be reduced by 2");
            Destroy(gameObject);
            StartCoroutine(ResetTimer());
            GameObject.FindGameObjectWithTag("Player").SendMessage("slowed");
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "DropWall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("SetCurrentSpeed", 2);
            Destroy(gameObject);
            
        }

    }
    IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
