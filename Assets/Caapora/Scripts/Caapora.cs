using UnityEngine;
using System.Collections;
using IsoTools;

public class Caapora : MonoBehaviour {

	public int life;
	
	// public GameObject gameObject = null;
	public Animator animator;
	public float speed = 0.2f;
	public GameObject go;


	void OnIsoCollisionEnter(IsoCollision iso_collision) {


		if ( iso_collision.gameObject.name == "chamas" ) {


			var objeto = iso_collision.gameObject.GetComponent<IsoRigidbody>();
			if ( objeto ) {
			
				Destroy(objeto.gameObject);

			//	objeto.transform.parent = transform;
			}
		}
	}
	
	// Use this for initialization
	void Start () {

		// go = Instantiate(Resources.Load("Prefabs/moitaPrefab")) as GameObject; 
		
		//	go.transform.parent = this.transform; 		
		
		

		animator = GetComponent<Animator>();
		// gameObject = Instantiate(Resources.Load("Prefabs/FloorPrefab")) as GameObject;
		
		// posiçao inicial do caipora
		gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);
		
	}
	
	// Update is called once per frame
	void Update () {

	//	go.transform.position = this.transform.position;

		//if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (-this.speed, 0, 0);
			animator.SetTrigger ("Caapora-left");
			
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (this.speed, 0, 0);
			animator.SetTrigger ("Caapora-right");
			
		} 
		if (Input.GetKey (KeyCode.DownArrow)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, -this.speed, 0);
			animator.SetTrigger ("Caapora-Sul");
			
			
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, this.speed, 0);
			animator.SetTrigger ("Caapora-Norte");
			
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			
			gameObject.GetComponent<IsoRigidbody> ().velocity += new Vector3 (0, 0, 10f);
			animator.SetTrigger ("Caapora-Norte");
			
		}

		if (gameObject.GetComponent<IsoObject> ().positionZ < -5) {
			
			Application.LoadLevel("GameOver");
			
		}
		
		//	}else {
		
		//        animator.SetTrigger("CaaporaIdle");
		//    }
		
		
	}
}