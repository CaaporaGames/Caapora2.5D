using UnityEngine;
using System.Collections;
using IsoTools;

public class Caapora : MonoBehaviour {

	// public GameObject gameObject = null;

	// Use this for initialization
	void Start () {

		// gameObject = Instantiate(Resources.Load("Prefabs/FloorPrefab")) as GameObject;
	
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
		
			gameObject.GetComponent<IsoObject>().position += new Vector3(-0.1f , 0, 0);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			
			gameObject.GetComponent<IsoObject>().position += new Vector3(0.1f , 0, 0);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			
			gameObject.GetComponent<IsoObject>().position += new Vector3(0 , -0.1f, 0);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			
			gameObject.GetComponent<IsoObject>().position += new Vector3(0 , 0.1f, 0);
		}

	
	}
}
