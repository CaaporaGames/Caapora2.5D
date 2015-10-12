using UnityEngine;
using System.Collections;
using IsoTools;

public class GameManager: MonoBehaviour {
	private static GameManager _instance;
	//used to store latest used door
	public Vector3 LastUsedDoorPosition;

	// ID da ultimo caminho passado
	public int PathID;
	public Sprite baldeCheio;
	//public Animator enemy;



	void OnIsoCollisionEnter(IsoCollision iso_collision) {



		switch(iso_collision.gameObject.name){

			case "map1PortalOeste":
				Application.LoadLevelAdditive("Map2");
				gameObject.GetComponent<IsoObject>().position = new Vector3(11, 0, 1);
				Destroy(GameObject.Find("root"));
				break;
			case "map2PortalLeste":
				Application.LoadLevelAdditive("Caapora");
				gameObject.GetComponent<IsoObject>().position = new Vector3(9, 0, 1);
				Destroy(GameObject.Find("map2"));
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
		
		if ( iso_collision.gameObject.name == "chamas" ) {
			

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
	
	//	StartCoroutine (Introduction());
	
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