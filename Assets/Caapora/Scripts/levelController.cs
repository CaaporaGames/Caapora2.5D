using UnityEngine;
using System.Collections;

public class levelController : MonoBehaviour {

	public static levelController instance;
	

	// Use this for initialization
	void Start () {
		instance = this;
		DontDestroyOnLoad (gameObject);
		Application.LoadLevel("Caapora");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
