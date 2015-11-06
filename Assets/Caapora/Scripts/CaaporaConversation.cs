using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaaporaConversation : MonoBehaviour {


	public static CaaporaConversation instance;

	Text txt;
	public Text textBox;
	//Store all your text in this string array
	string[] goatText = new string[]{
		"Ola! Eu sou o Caapora", 
		"Nossa! O que fizeram com a floresta", 
		"De onde veio todo esse fogo!", 
		"Me ajude a apagar as chamas",
		"Basta pegar o balde logo ali" ,
		"e jogar onde ha fogo",
		"Nao temos tempo a perder.",
		"Vamos la!"};


	int currentlyDisplayingText = 0;

	void Awake(){
	
		
		instance = this;
	
	}

	// Use this for initialization
	void Start () {
	




	}
	
	// Update is called once per frame
	void Update () {
	

		// InvokeRepeating("SkipToNextText", .01f, 1.0f);

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


	public static IEnumerator AnimateFrase(){
	
	
		for (int i = 0; i < instance.goatText.Length ; i++) {
			//StopAllCoroutines();
			Debug.Log("Passou pelo for de animatefrase ");
			instance.StartCoroutine(AnimateText(instance.goatText[i]));
			yield return new WaitForSeconds(3f);
		}
		Destroy(GameObject.Find ("Tela de Conversa"));
		

	}


	// Para cada letra leva um certo tempo para mostrar
	//Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
	public static IEnumerator  AnimateText(string str){
		instance.txt = instance.transform.GetComponent <Text> ();


		for (int i = 0; i < (str.Length+1); i++)
		{
			instance.txt.text = str.Substring(0, i);
			yield return new WaitForSeconds(.07f);
		}
	}


	
}
