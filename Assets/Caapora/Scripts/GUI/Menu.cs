using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {


	void Start () {
	
	}

	void Update () {
	
	}


    public void StartGame()
    {
        Application.LoadLevel("Tutorial");
    }

    public void LoadGame()
    {

        Caapora.GameManager.next_scene = "TestMap";
        Application.LoadLevel("Loader");
    }

    public void ExitGame()
    {

        Application.Quit();
    }
}


