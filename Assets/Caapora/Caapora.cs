using UnityEngine;
using System.Collections;
using IsoTools;

public class Caapora : MonoBehaviour {

    // public GameObject gameObject = null;
    public Animator animator;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
        // gameObject = Instantiate(Resources.Load("Prefabs/FloorPrefab")) as GameObject;

    }
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
		
			gameObject.GetComponent<IsoObject>().position += new Vector3(-0.1f , 0, 0);
            animator.SetTrigger("CaaporaWest");

		} 
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
			
			gameObject.GetComponent<IsoObject>().position += new Vector3(0.1f , 0, 0);
            animator.SetTrigger("CaaporaEast");

        } 
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
			
			gameObject.GetComponent<IsoObject>().position += new Vector3(0 , -0.1f, 0);
            animator.SetTrigger("CaaporaSouth");

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
			
			gameObject.GetComponent<IsoObject>().position += new Vector3(0 , 0.1f, 0);
            animator.SetTrigger("CaaporaNorth");

        }
        if (!Input.GetKeyDown(KeyCode.UpArrow) || !Input.GetKeyDown(KeyCode.DownArrow) || !Input.GetKeyDown(KeyCode.RightArrow) || !Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetTrigger("CaaporaIdle");
        }

	
	}
}
