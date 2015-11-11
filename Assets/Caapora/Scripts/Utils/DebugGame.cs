using UnityEngine;
using System.Collections;
using IsoTools;

namespace Caapora{ 
public class DebugGame : MonoBehaviour {

    public int totalNumberOfObject;
    public static DebugGame instance;
    public string debug_message;

    // Use this for initialization
    void Start () {

        instance = this;

	}
	
	// Update is called once per frame
	void Update () {

        totalNumberOfObject = GameObject.FindObjectsOfType(typeof(MonoBehaviour)).Length; //returns Object[];
    }


    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Exibe área de debug na tela
    /// </summary>
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