using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class text : MonoBehaviour {

	Text txt;
	public Text textBox;
	//Store all your text in this string array
	string[] goatText = new string[]{
		"1. Ola! Eu sou o Caapora", 
		"2. Nossa! O que fizeram com a floresta", 
		"3. De onde veio todo esse fogo!", 
		"4. Me ajude a apagar as chamas",
		"5. Basta pegar o balde logo ali e e jogar onde ha fogo" ,
		"6. Nao temos tempo a perder. Vamos la!"};


	int currentlyDisplayingText = 0;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	

		// InvokeRepeating("SkipToNextText", .01f, 1.0f);

		if (Input.GetKey (KeyCode.A)) {
			

			
		}

	}

	

	void Awake () {

		StartCoroutine(AnimateFrase ());
	}

	//This is a function for a button you press to skip to the next text
	public void SkipToNextText(){
		StopAllCoroutines();
		currentlyDisplayingText++;
		//If we've reached the end of the array, do anything you want. I just restart the example text
		if (currentlyDisplayingText>goatText.Length) {
			currentlyDisplayingText=0;
		}
		StartCoroutine(AnimateText("lalalal"));
	}


	IEnumerator AnimateFrase(){
	

		for (int i = 0; i < goatText.Length ; i++) {
			//StopAllCoroutines();
			Debug.Log("Passou aqui em AnimateFrase");
			StartCoroutine(AnimateText(goatText[i]));
			yield return new WaitForSeconds(3f);
		}

		Destroy(GameObject.Find ("txtBalloon"));



	}


	// Para cada letra leva um certo tempo para mostrar
	//Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
	IEnumerator AnimateText(string str){
		txt = transform.GetComponent<Text> ();

		for (int i = 0; i < (str.Length+1); i++)
		{
			txt.text = str.Substring(0, i);
			yield return new WaitForSeconds(.07f);
		}
	}


	
}
