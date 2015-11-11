using UnityEngine;
using System.Collections;

public class ConversationPanel : MonoBehaviour {
	// necessario para acessar metodos desta classe fora dela
	public static ConversationPanel instance;

	// Use this for initialization


	void Awake(){

        // Acessar atributos da classe pelos metodos estaticos
		instance = this;

	}
	void Start () {
		
        // Ao iniciar desabilita o painel
		ActivePanel (false);

	
		//InvokeRepeating( this.AtiveBallon(), .01, 1.0);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void ActivePanel(bool value){

		instance.gameObject.SetActive (value);
	}



}
