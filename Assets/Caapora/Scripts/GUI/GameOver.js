﻿#pragma strict

function Start () {

}

function Update () {

}

function OnGUI () {

	if(GUI.Button(Rect(Screen.width / 2 - 80, Screen.height /2 + 40, 150, 40), "Play Again")){
		Application.LoadLevel("Caapora");
	}
	if(GUI.Button(Rect(Screen.width / 2 - 80, Screen.height /2 + 90, 150, 40), "Back")){
		Application.LoadLevel("MenuPrincipal");
	}
}