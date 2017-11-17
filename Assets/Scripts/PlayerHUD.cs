using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : UserClass {
    public int maxHealth = 100;
    public int currHealth;

    public Texture2D backGround;
    public Texture2D foreGround;

   
    public GameObject HealthBar;


    // Use this for initialization
    void Start () {    
        HealthBar = GameObject.FindWithTag("HealthLength");
        currHealth = maxHealth;
    }
    private void Update()
    {
        var HealthBarLength = HealthBar.transform as RectTransform;
        //HealthBarLength.sizeDelta =
        
       
    }
    // Update is called once per frame
    public void LowerCurrentHealth(int adj)
    {
        if (currHealth < 0)
        {
            currHealth = 0;
        }
        var healthRectTransform = HealthBar.transform as RectTransform;
        healthRectTransform.sizeDelta = new Vector2(currHealth - adj, healthRectTransform.sizeDelta.y);
        
    }
    public void IncreaseCurrentHealth(int adj)
    {
        currHealth += adj;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
      //  healthBarLength = (Screen.width / 2) * (currHealth / (float)maxHealth);
    }
}
