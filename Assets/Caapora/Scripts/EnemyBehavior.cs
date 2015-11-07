using UnityEngine;
using System.Collections;
using IsoTools;


public class EnemyBehavior : CharacterBase {

	public float speed = 0.2f;
	public Animator animator;
	// Use this for initialization
	protected void Start () {
		animator = GetComponent<Animator>();

		base.Start();
		currentLevel = StatsController.GetCurrentLevel();

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKey (KeyCode.A)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (-this.speed, 0, 0);
			animator.SetTrigger ("Left");
			
		} 
		if (Input.GetKey (KeyCode.D)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (this.speed, 0, 0);
			animator.SetTrigger ("Right");
			
		} 
		if (Input.GetKey (KeyCode.W)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, this.speed, 0);
			animator.SetTrigger ("Up");
			
			
		}
		if (Input.GetKey (KeyCode.S)) {
			
			gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, -this.speed, 0);
			animator.SetTrigger ("Down");
			
		}

		if (gameObject.GetComponent<IsoObject> ().positionZ < -5) {
			
			Application.LoadLevel("GameOver");
			
		}


	
	}
}
