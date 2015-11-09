using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameManager.Load();
        Application.LoadLevel("Map1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
