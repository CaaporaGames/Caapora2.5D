using UnityEngine;
using System.Collections;
using IsoTools;



static class Coodenadas
{

	public enum Map1 : long { x = -58, y = 27 };
	public enum Map2 : long { x = -32, y = 27 };
	public enum Map3 : long { x = -4, y = 27 }; // Provisorio
	public enum Map4 : long { x = 21, y = 27 };


	public enum Map8 : long { x = -58, y = -2 };
	public enum Map7 : long { x = -33, y = -2 };
	public enum Map6 : long { x = -4, y = -2 };
	public enum Map5 : long { x = 21, y = -2 };

	public enum Map9 : long { x = -58, y = -27 };
	public enum Map10 : long { x = -33, y = -27 };
	public enum Map11 : long { x = -4, y = -27 };
	public enum Map12 : long { x = 21, y = -27 };

	
}



public class GameManager: MonoBehaviour {
	private static GameManager _instance;
	//used to store latest used door
	public Vector3 LastUsedDoorPosition;

	// ID da ultimo caminho passado
	public int PathID;
	public Sprite baldeCheio;
	//public Animator enemy;



	// Facilita a transferencia de um mapa para outro
	void movePlayer(string source, string destination ){

		float x = 0f, y = 0f;

		switch (destination) {
			case "Map1":
				x = (float)Coodenadas.Map1.x;
				y = (float)Coodenadas.Map1.y;
				break;
			case "Map2":
				x = (float)Coodenadas.Map2.x;
				y = (float)Coodenadas.Map2.y;
				break;
			case "Map3":
				x = (float)Coodenadas.Map3.x;
				y = (float)Coodenadas.Map3.y;
				break;
			case "Map4":
				x = (float)Coodenadas.Map4.x;
				y = (float)Coodenadas.Map4.y;
				break;
			case "Map5":
				x = (float)Coodenadas.Map5.x;
				y = (float)Coodenadas.Map5.y;
				break;
			case "Map6":
				x = (float)Coodenadas.Map6.x;
				y = (float)Coodenadas.Map6.y;
				break;
			case "Map7":
				x = (float)Coodenadas.Map7.x;
				y = (float)Coodenadas.Map7.y;
				break;
			case "Map8":
				x = (float)Coodenadas.Map8.x;
				y = (float)Coodenadas.Map8.y;
				break;
			case "Map9":
				x = (float)Coodenadas.Map8.x;
				y = (float)Coodenadas.Map8.y;
				break;
			case "Map10":
				x = (float)Coodenadas.Map8.x;
				y = (float)Coodenadas.Map8.y;
				break;
			case "Map11":
				x = (float)Coodenadas.Map8.x;
				y = (float)Coodenadas.Map8.y;
				break;
			case "Map12":
				x = (float)Coodenadas.Map8.x;
				y = (float)Coodenadas.Map8.y;
				break;
		}

		Application.LoadLevelAdditive(destination);
		gameObject.GetComponent<IsoObject>().position = new Vector3(x , y , 1);
		Destroy(GameObject.Find(source));
		PlayerPrefs.SetString("CurrentMap",destination);
		
	}



	void OnIsoCollisionEnter(IsoCollision iso_collision) {


		// Baseado na colisao com os portoes faz a movimentaçao do personagem
		switch(iso_collision.gameObject.name){


	        // Map1 to Map2
			case "map1GateEastPrefab":
				movePlayer("Map1","Map2");
				break;
		
			// Map1 to Map8	
			case "map1GateSouth":
				movePlayer("Map1", "Map8");
				break;

			// Map2 to Map1	
			case "map2GateWestPrefab":
				movePlayer("Map2", "Map1");
				break;

			// Map2 to Map3	
			case "map2GateEastPrefab":
				movePlayer("Map2", "Map3");
				break;

			// Map3 to Map2	
			case "map3GateWestPrefab":
				movePlayer("Map3", "Map2");
				break;

	        // Map8 to Map1
			case "map8GateNorthPrefab":
				movePlayer("Map8","Map1");
				break;

			// Map8 to Map7
			case "map8GateEastPrefab":
				movePlayer("Map8","Map7");
				break;


			// Map8 to Map9
			case "map8GateSouthPrefab":
				movePlayer("Map8","Map7");
				break;
			
			// Map7 to Map8
			case "map7GateWestPrefab":
				movePlayer("Map8","Map7");
				break;
			
			// Map7 to Map2
			case "map7GateNorthPrefab":
				movePlayer("Map8","Map7");
				break;
			
			// Map7 to Map6
			case "map7GateEastPrefab":
				movePlayer("Map8","Map7");
				break;
			
			// Map7 to Map10
			case "map7GateSouthPrefab":
				movePlayer("Map8","Map7");
				break;
			
			// Map6 to Map7
			case "map6GateWestPrefab":
				movePlayer("Map6","Map7");
				break;
			
			// Map6 to Map3
			case "map6GateNorthPrefab":
				movePlayer("Map6","Map3");
				break;	
			// Map6 to Map5
			case "map6GateEastPrefab":
				movePlayer("Map6","Map5");
				break;
			
			// Map6 to Map11
			case "map6GateSouthPrefab":
				movePlayer("Map6","Map11");
				break;

				
			// Map3 to Map4
			case "map3GateEastPrefab":
				movePlayer("Map3","Map4");
				break;	

			// Map3 to Map6
			case "map3GateSouthPrefab":
				movePlayer("Map3","Map6");
				break;
				


			
		}
		
		// Colisao com o balde vazio
		if (iso_collision.gameObject.name == "baldevazio") {
		
			var objeto = iso_collision.gameObject.GetComponent<IsoRigidbody>();
			if ( objeto ) {
				
				// pega o balde
			//	objeto.transform.parent = transform;
			}
		}
		
		if ( iso_collision.gameObject.name == "chamas"  || iso_collision.gameObject.name == "chamas(Clone)") {
			

			var objeto = iso_collision.gameObject.GetComponent<IsoRigidbody>();
			if ( objeto ) {
				
				Destroy(objeto.gameObject);
				
				//	objeto.transform.parent = transform;
			}
		}


		if ( iso_collision.gameObject.name == "waterPrefab" ) {

			// load all frames in fruitsSprites array
			baldeCheio = Resources.Load("Sprites/balde", typeof(Sprite)) as Sprite;



			//enemy = Resources.Load("Sprites/Enemy", typeof(Sprite)) as Sprite;

			// substitui sprite
			//transform.FindChild("baldevazio").GetComponent<SpriteRenderer>().sprite = baldeCheio;
			Debug.Log("Colidiu com a agua");

		//	gameObject.GetComponent<SpriteRenderer>().sprite = enemy;


		
		}
	}

	void Start(){

	
	 	// StartCoroutine (Introduction());
	
	}


	
	public static GameManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	// Executa a Introduçao passo a passo
	public IEnumerator Introduction(){


	
		StartCoroutine (Completed.PlayerBehavior.AnimateCaapora ("Caapora-left", 30));
		yield return new WaitForSeconds(3f);

		StartCoroutine (Completed.PlayerBehavior.ShakePlayer());
		yield return new WaitForSeconds(1f);


		textBallon.AtiveBallon (true);
		StartCoroutine (CaaporaConversation.AnimateFrase());
	



	}


	void OnGUI(){
		
		
		GUI.Label(new Rect(0,0,300,50), "Player Position X :" + gameObject.GetComponent<IsoObject>().positionX);
		GUI.Label(new Rect(0,50,300,50), "Player Position Y :" + gameObject.GetComponent<IsoObject>().positionY);
		GUI.Label(new Rect(0,100,300,50), "Player Position Z :" + gameObject.GetComponent<IsoObject>().positionZ);

		GUI.Label(new Rect(0,150,300,50), "Mapa Atual :" + PlayerPrefs.GetString("CurrentMap"));

		
		
	}


	
	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
}