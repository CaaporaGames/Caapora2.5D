#pragma strict

var customSkin : GUISkin;

function Start () {
	
}

function Update () {

}

function OnGUI () {

	GUI.skin = customSkin;
	
	if(GUI.Button(Rect(Screen.width / 2 + 220 , Screen.height /2 + 200, 150, 40), "pular")){
		Application.LoadLevel("Caapora");
	}
	GUI.Box(Rect(Screen.width / 2 - 220, Screen.height /2 - 200, 450, 300), "\n\n\nApos ter um pesadelo onde a Floresta Amazonica e destruida,\n\n o guardiao da floresta, conhecido como Caipora\n\n decide parar de se esconder, e proteger o que ele considera tao valioso\n\n\n\n\n\nAjude o Caipora a salvar a floresta da destruiçao");
	
																				
	
}