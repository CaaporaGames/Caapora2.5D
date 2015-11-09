#pragma strict

var customSkin : GUISkin;

function Start () {

}

function Update () {

}

function OnGUI () {

	GUI.skin = customSkin;

	if(GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2, 150, 40), "Start Game")){
		Application.LoadLevel("Tutorial");
	}
	if(GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2 + 50, 150, 40), "Load")) 
	    Application.LoadLevel("LoadGame");

	GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2 + 100, 150, 40), "Options");
	
	if(GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2 + 150, 150, 40), "Sair"))
	    Application.Quit();
}