using UnityEngine;
using System.Collections;

public class generator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		float cellY;
		float cellX;
		int tileSize = 16;

		for (cellX = 0; cellX < 30; cellX++){
			for (cellY = 0; cellY < 30; cellY++) {
			
	
				GameObject myFloorInstance = Instantiate(Resources.Load("Prefabs/grassPrefab"),
				    new Vector3(( cellX * tileSize / 2) + (cellY * tileSize / 2), 
				            	( cellY * tileSize / 2) - (cellX * tileSize / 2), 2),
				                                         Quaternion.identity) as GameObject; 


				}	
		}
				
					
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
