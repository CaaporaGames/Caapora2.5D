using UnityEngine;
using System.Collections;

public class textBallon : MonoBehaviour {
	// necessario para acessar metodos desta classe fora dela
	public static textBallon instance;

	// Use this for initialization
	void Start () {
		instance = this;
		AtiveBallon (false);

	
		//InvokeRepeating( this.AtiveBallon(), .01, 1.0);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void AtiveBallon(bool value){

		instance.gameObject.SetActive (value);
	}



}
