using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Monkey : CharacterBase {


    public float _life = 1000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(!GameManager.isAnimating)
          GameObject.Find("Monkey/hp").GetComponent<Text>().text = _life.ToString();


    }
}
