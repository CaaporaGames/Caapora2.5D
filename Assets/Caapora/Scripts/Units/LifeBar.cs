using UnityEngine;
using System.Collections;
using Caapora;

public class LifeBar : MonoBehaviour {


    private CreatureBase creature;

	// Use this for initialization
	void Start () {
	    
        creature = GetComponentInParent(typeof(CreatureBase)) as CreatureBase;
    }
	
	// Update is called once per frame
	void Update () {

       
	}
}
