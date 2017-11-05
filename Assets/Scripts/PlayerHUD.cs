using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour {
    public int maxHealth = 100;
    public int currHealth;

    public Texture2D backGround;
    public Texture2D foreGround;

    public float healthBarLength;



	// Use this for initialization
	void Start () {
        healthBarLength = Screen.width / 4;	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void OnGUI()
    {
        GUI.BeginGroup(new Rect(0, 0, healthBarLength, 32));

        GUI.Box(new Rect(0, 0, healthBarLength, 32), backGround);

        GUI.BeginGroup(new Rect(0, 0, healthBarLength, 32), foreGround);

        GUI.EndGroup();
        GUI.EndGroup();
    }
    public void LowerCurrentHealth(int adj)
    {
        currHealth -= adj;

        if (currHealth < 0)
        {
            currHealth = 0;
        }
        healthBarLength = (Screen.width / 2) * (currHealth / (float)maxHealth);
    }
    public void IncreaseCurrentHealth(int adj)
    {
        currHealth += adj;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        healthBarLength = (Screen.width / 2) * (currHealth / (float)maxHealth);
    }
}
