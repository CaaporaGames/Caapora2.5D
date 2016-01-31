using UnityEngine;
using System.Collections;

public class ConversationBallon : MonoBehaviour {
	// necessario para acessar metodos desta classe fora dela
	public ConversationBallon instance;



	void Awake(){

		instance = this;

	}
	void Start () {
		

		ActiveBallon (false);

		//InvokeRepeating( this.AtiveBallon(), .01, 1.0);

	
	}


	public void ActiveBallon(bool value){

		gameObject.SetActive (value);
	}



}
