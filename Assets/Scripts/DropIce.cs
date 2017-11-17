using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drop Ice
// - creates a circle of Fuel/Water/Ice
// - Fire Loses, destory (Fire)
// - Oil Wins, spread (Oil)
// - Poison Wins, spread (poison)
public class DropIce : MonoBehaviour {

    public void DestroySelf(){
    	Destroy(gameObject);
    }
}