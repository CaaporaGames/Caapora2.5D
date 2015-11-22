using UnityEngine;
using System.Collections;
using IsoTools;

public class Fire : MonoBehaviour {

    protected float demage;
    private IsoRigidbody rb;
    private IsoRigidbody fire;

    // Use this for initialization
    void Start () {

        demage = 1f;
        fire = gameObject.GetComponent<IsoRigidbody>();

        GameManager.totalOfFrame++;
    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(Atack());
	}


    public float GetDamage()
    {

        return demage;

    }


    void OnDestroy()
    {

        GameManager.totalOfFrame--;
    }


    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Balança o player para sinalizar algo
    /// </summary>
    private IEnumerator Atack()
    {
        
        //fire.velocity = new Vector3(0, 0.1f, 0f);

        yield return new WaitForSeconds(.08f);

    }
}
