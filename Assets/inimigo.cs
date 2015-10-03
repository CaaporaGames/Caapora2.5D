using UnityEngine;
using System.Collections;
using IsoTools;


public class inimigo : MonoBehaviour {

	public float speed = 0.2f;
	public Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKey (KeyCode.A)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (-this.speed, 0, 0);
			animator.SetTrigger ("Enemy-left");
			
		} 
		if (Input.GetKey (KeyCode.D)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (this.speed, 0, 0);
			animator.SetTrigger ("Enemy-right");
			
		} 
		if (Input.GetKey (KeyCode.W)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, this.speed, 0);
			animator.SetTrigger ("Enemy-up");
			
			
		}
		if (Input.GetKey (KeyCode.S)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, -this.speed, 0);
			animator.SetTrigger ("Enemy-down");
			
		}

		if (gameObject.GetComponent<IsoObject> ().positionZ < -5) {
			
			Application.LoadLevel("GameOver");
			
		}


	
	}
}
