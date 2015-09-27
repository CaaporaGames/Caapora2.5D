using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class text : MonoBehaviour {

	Text txt;

	// Use this for initialization
	void Start () {

		StartCoroutine(TypeText());

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	
	IEnumerator TypeText() {
		
		
		yield return new WaitForSeconds(2);
		txt = transform.GetComponent<Text> ();
		txt.text = "Mudando de assunto";

		
	}


	
}
