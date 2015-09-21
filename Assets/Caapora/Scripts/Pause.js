#pragma strict

var controlePause : boolean;

function Start () {

}

function Update () {

	if(Input.GetKeyDown(KeyCode.P)){
		
		if(controlePause){
			Time.timeScale = 0;
			controlePause = false;
		}
		else{
			Time.timeScale = 1;
			controlePause = true;
		}
	}
}