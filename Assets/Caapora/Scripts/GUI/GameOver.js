#pragma strict

var customSkin : GUISkin;

function Start () {

}

function Update () {

}

function OnGUI () {

	GUI.skin = customSkin;

	if(GUI.Button(Rect(Screen.width / 2 - 80, Screen.height /2 + 40, 150, 40), "Play Again")){
		Application.LoadLevel("Map6");
	}
	if(GUI.Button(Rect(Screen.width / 2 - 80, Screen.height /2 + 90, 150, 40), "Back")){
		Application.LoadLevel("MenuPrincipal");
	}
}