using UnityEngine;
using System.Collections;
using IsoTools;

public class Fire : MonoBehaviour {

    protected float demage;
    private IsoRigidbody rb;

    // Use this for initialization
    void Start () {

        demage = 10f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public float GetDamage()
    {

        return demage;

    }
}
