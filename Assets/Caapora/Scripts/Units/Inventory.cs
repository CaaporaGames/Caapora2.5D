using UnityEngine;
using System.Collections.Generic;
using IsoTools;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {


    public List<GameObject> itemList = new List<GameObject>();
    public static Inventory instance;
    
    

    void Start () {

        instance = this;

	}
	

	void Update () {

        
	}

    
    public static bool isEmpty()
    {
        return instance.itemList.Count == 0;
    }


   
    public static GameObject getItem()
    {
        return instance.itemList[0];
    }

    
}
