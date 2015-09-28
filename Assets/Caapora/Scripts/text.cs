using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class text : MonoBehaviour {

	Text txt;
	public Text textBox;
	//Store all your text in this string array
	string[] goatText = new string[]{
		"1. Laik's super awesome custom typewriter script", 
		"2. You can click to skip to the next text", 
		"3. All text is stored in a single string array", 
		"4. Ok, now we can continue","" +
		"5. End Kappa"};


	int currentlyDisplayingText = 0;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.A)) {
			
			SkipToNextText();
			
		}

	}

	

	void Awake () {
		StartCoroutine(AnimateText());
	}

	//This is a function for a button you press to skip to the next text
	public void SkipToNextText(){
		StopAllCoroutines();
		currentlyDisplayingText++;
		//If we've reached the end of the array, do anything you want. I just restart the example text
		if (currentlyDisplayingText>goatText.Length) {
			currentlyDisplayingText=0;
		}
		StartCoroutine(AnimateText());
	}
	//Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
	IEnumerator AnimateText(){
		txt = transform.GetComponent<Text> ();

		for (int i = 0; i < (goatText[currentlyDisplayingText].Length+1); i++)
		{
			txt.text = goatText[currentlyDisplayingText].Substring(0, i);
			yield return new WaitForSeconds(.07f);
		}
	}


	
}
