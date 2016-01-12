using UnityEngine;
using System.Collections;
using IsoTools;

namespace Caapora{ 
public class DebugGame : MonoBehaviour {

    public int totalNumberOfObject;
    public static DebugGame instance;
    public string debug_message;


    void Start () {

        instance = this;

	}
	
	// Update is called once per frame
	void Update () {
        // Problema de desempenho
        //totalNumberOfObject = GameObject.FindObjectsOfType(typeof(MonoBehaviour)).Length; //returns Object[];
    }


    void OnGUI()
    {



        GUI.Label(new Rect(0, 0, 300, 50), "Player Position X :" + gameObject.GetComponent<IsoObject>().positionX);
        GUI.Label(new Rect(0, 50, 300, 50), "Player Position Y :" + gameObject.GetComponent<IsoObject>().positionY);
        GUI.Label(new Rect(0, 100, 300, 50), "Player Position Z :" + gameObject.GetComponent<IsoObject>().positionZ);
        GUI.Label(new Rect(0, 150, 300, 50), "Debug collision: " + debug_message);

        GUI.Label(new Rect(300, 0, 300, 50), "Total GameObjects In Scene: " + totalNumberOfObject);






    }
}
}