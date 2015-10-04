using UnityEngine;
using System.Collections;

public class generator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		float cellY;
		float cellX;
		int mapize = 50;
		int tile_width = 34;
		int tile_height = 16;


		/*
		for (cellX = 0; cellX < 30; cellX++){
			for (cellY = 0; cellY < 30; cellY++) {
			
	
				GameObject myFloorInstance = Instantiate(Resources.Load("Prefabs/grassPrefab"),
				    new Vector3(( cellX * tileSize / 2) + (cellY * tileSize / 2), 
				            	( cellY * tileSize / 2) - (cellX * tileSize / 2), 2),
				                         Quaternion.identity) as GameObject; 


				}	
		} */


		for (int i = 0; i < mapize; i++) {
		
			for (int j = mapize; j >= 0; j--){
				// Changed loop condition here.

				GameObject myFloorInstance = Instantiate(Resources.Load("Prefabs/grassPrefab"),
				           new Vector3(( j * tile_width / 2) + (i * tile_width / 2), 
				            ( i * tile_height / 2) - (j * tile_height / 2), 2),
				
				                                         Quaternion.identity) as GameObject; 
				/*draw(
					tile_map[i][j],
					x = (j * tile_width / 2) + (i * tile_width / 2)
					y = (i * tile_height / 2) - (j * tile_height / 2)
					)*/

			}  
		}
			
				
					
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
