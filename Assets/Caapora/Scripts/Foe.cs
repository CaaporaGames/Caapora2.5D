using UnityEngine;
using System.Collections;
using IsoTools;


public class Foe : MonoBehaviour {
	
	public float speed = 0.2f;
	public Animator animator;
	public IsoObject foe;
	// Use this for initialization
	
	public static bool isPlayingAnimation = false;
	public static Foe instance;

	void Start () {

		instance = this;
		animator = GetComponent<Animator>();

		StartCoroutine (moveInSquarePath());

	

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.A)) {
		
			instance.foe.position += new Vector3 (-instance.speed, 0, 0);
		}

		
	}

	
	public static IEnumerator moveInSquarePath(){

		Debug.Log ("Entrou em moveInSquarePath");
		instance.StartCoroutine (AnimateFoe("Lion_Down", 10));
		yield return new WaitForSeconds(3f); 
		instance.StartCoroutine (AnimateFoe ("Lion_Left", 10));
		yield return new WaitForSeconds(3f); 
		
		
	}


	public static IEnumerator AnimateFoe(string direction, int steps){
		
		Debug.Log ("Entrou em animateFoe");
		
		isPlayingAnimation = true;

		instance.foe = instance.gameObject.GetComponent<IsoObject> ();
		
		instance.GetComponent<Animator>().SetTrigger(direction);

		for (int i = 0; i < steps; i++)
		{


			if(direction == "Lion_Left"){
			
				instance.foe.position += new Vector3 (-instance.speed, 0, 0);
			}else {
				instance.foe.position += new Vector3 (0, -instance.speed, 0);
			}

			// caso seja a ultima animaçao
			isPlayingAnimation = (i == steps - 1) ? false : true;
			
			yield return new WaitForSeconds(.1f);
		}
	}
}

