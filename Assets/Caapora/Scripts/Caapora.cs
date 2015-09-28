using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;

public class Caapora : MonoBehaviour {

	public int life;
    protected bool paused = true;
	
	// public GameObject gameObject = null;
	public Animator animator;
	public float speed = 0.2f;
	public GameObject go;
	public IsoObject caapora;
	public float savedTimeState;
	Text txt;


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

		//StartCoroutine(RemoveBalloon());




//		 go = Instantiate(Resources.Load("Prefabs/SpeechBubblePrefab")) as GameObject; 
		
//		 go.transform.parent = this.transform; 		


		animator = GetComponent<Animator>();
		// gameObject = Instantiate(Resources.Load("Prefabs/FloorPrefab")) as GameObject;
		
		// posiçao inicial do 
		gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);



		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


//	 	if (paused) {
//			savedTimeState = Time.timeScale;
//			Time.timeScale = 0; 
//		} else {
//			Time.timeScale = savedTimeState;
//		}



			//		go.transform.position = this.transform.position;
			
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

				paused = paused ? false : true;
				
				
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

	void OnPauseGame ()
	{
		paused = true;
	}
	
	
	void OnResumeGame ()
	{
		paused = false;
	}


	IEnumerator RemoveBalloon() {
		
		Debug.Log("Before Waiting 5 seconds");
		yield return new WaitForSeconds(2);
		textBallon.AtiveBallon (false);
		Debug.Log("After Waiting 5 Seconds");
	}


	void Awake () {
		StartCoroutine(AnimateCaapora());
	}


	IEnumerator AnimateCaapora(){

		caapora = gameObject.GetComponent<IsoObject> ();
		
		for (int i = 0; i < 20; i++)
		{
			caapora.position += new Vector3 (0, this.speed, 0);

			yield return new WaitForSeconds(.08f);
		}
	}




	void OnGUI(){
		
	
		GUI.Label(new Rect(0,0,300,50), "Player Position X " + gameObject.GetComponent<IsoObject>().positionX);
		GUI.Label(new Rect(0,50,300,50), "Player Position Y " + gameObject.GetComponent<IsoObject>().positionY);
		GUI.Label(new Rect(0,100,300,50), "Player Position Z " + gameObject.GetComponent<IsoObject>().positionZ);

	
	}

}