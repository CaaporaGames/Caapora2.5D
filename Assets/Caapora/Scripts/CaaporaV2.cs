using UnityEngine;
using System.Collections;
using IsoTools;

[RequireComponent(typeof(IsoRigidbody))]
public class CaaporaV2 : MonoBehaviour {

    // public GameObject gameObject = null;
    public Animator animator;

	public float speed = 4.0f;
	
	IsoRigidbody _isoRigidbody = null;



	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
        // gameObject = Instantiate(Resources.Load("Prefabs/FloorPrefab")) as GameObject;

		_isoRigidbody = GetComponent<IsoRigidbody>();
		if ( !_isoRigidbody ) {
			throw new UnityException("PlayerController. IsoRigidbody component not found!");
		}

		// posiçao inicial do caipora
		//gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);

    }
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {
		Debug.Log("vel = " + _isoRigidbody.velocity);

			if (Input.GetKey(KeyCode.LeftArrow)) {
		
				_isoRigidbody.velocity = IsoUtils.Vec3ChangeX(_isoRigidbody.velocity, -speed);
				Debug.Log("Entrou em leftArrow vel = -" + speed  + _isoRigidbody.velocity);
				animator.SetTrigger ("CaaporaWest");

			} 
			else if (Input.GetKey (KeyCode.RightArrow)) {
			
				_isoRigidbody.velocity = IsoUtils.Vec3ChangeX(_isoRigidbody.velocity,  speed);
				animator.SetTrigger ("CaaporaEast");

			} 
			else if (Input.GetKey (KeyCode.DownArrow)) {
			
				_isoRigidbody.velocity = IsoUtils.Vec3ChangeY(_isoRigidbody.velocity, -speed);
				animator.SetTrigger ("CaaporaSouth");


			}
			else if (Input.GetKey (KeyCode.UpArrow)) {
			
				_isoRigidbody.velocity = IsoUtils.Vec3ChangeY(_isoRigidbody.velocity,  speed);
				animator.SetTrigger ("CaaporaNorth");

			}


		if (gameObject.GetComponent<IsoObject> ().positionZ < -5) {
		
			Application.LoadLevel("GameOver");

		}

	//	}else {
        
    //        animator.SetTrigger("CaaporaIdle");
    //    }

	
	}
}
