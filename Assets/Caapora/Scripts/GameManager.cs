using UnityEngine;
using System.Collections;
using IsoTools;

public class GameManager: MonoBehaviour {
	private static GameManager _instance;
	//used to store latest used door
	public Vector3 LastUsedDoorPosition;

	// ID da ultimo caminho passado
	public int PathID;



	void OnIsoCollisionEnter(IsoCollision iso_collision) {


		switch(iso_collision.gameObject.name){

			case "map1PortalOeste":
				Application.LoadLevel("Map2");
				break;
			case "map2PortalLeste":
				Application.LoadLevel("Caapora");
				break;

		}
		

		
		if ( iso_collision.gameObject.name == "chamas" ) {
			
			
			
			var objeto = iso_collision.gameObject.GetComponent<IsoRigidbody>();
			if ( objeto ) {
				
				Destroy(objeto.gameObject);
				
				//	objeto.transform.parent = transform;
			}
		}
	}

	void Start(){
	
		StartCoroutine (Introduction());
	
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