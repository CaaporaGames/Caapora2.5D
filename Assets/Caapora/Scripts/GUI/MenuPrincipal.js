#pragma strict

function Start () {

}

function Update () {

}


function OnMouseDown () {
		Application.LoadLevel ("Caapora");
}

function OnGUI () {

	GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2, 150, 40), "Start Game");
	GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2 + 50, 150, 40), "Player");
	GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2 + 100, 150, 40), "Options");
	GUI.Button(Rect(Screen.width / 2 + 130, Screen.height /2 + 150, 150, 40), "Credits");
}