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

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {

			if (Input.GetKey (KeyCode.LeftArrow)) {
		
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (-0.1f, 0, 0);
				animator.SetTrigger ("CaaporaWest");

			} 
			if (Input.GetKey (KeyCode.RightArrow)) {
			
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (0.1f, 0, 0);
				animator.SetTrigger ("CaaporaEast");

			} 
			if (Input.GetKey (KeyCode.DownArrow)) {
			
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, -0.1f, 0);
				animator.SetTrigger ("CaaporaSouth");

			}
			if (Input.GetKey (KeyCode.UpArrow)) {
			
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0.1f, 0);
				animator.SetTrigger ("CaaporaNorth");

			}

		}else {
        
            animator.SetTrigger("CaaporaIdle");
        }

	
	}
}
