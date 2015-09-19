using UnityEngine;
using System.Collections;
using IsoTools;

public class generateWorld : MonoBehaviour {

	public GameObject floor = null;
	// Use this for initialization
	void Start () {


	
		for (int i =0; i < 3; i++) {
			floor = Instantiate(Resources.Load("Prefabs/grassPrefab")) as GameObject;
			floor.transform.position = new Vector3(i, i, 0);
		}

		//		floor = Instantiate(Resources.Load("Prefabs/grassPrefab")) as GameObject;
			//	floor.AddComponent<IsoRigidbody>();
			 //   floor.AddComponent<IsoBoxCollider>();
		//		floor.transform.position = new Vector3(x, y, 0);

		

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
