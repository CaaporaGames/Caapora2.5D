using UnityEngine;
using System.Collections.Generic;
using IsoTools;

public class Inventory : MonoBehaviour {


    public List<GameObject> itemList = new List<GameObject>();
    public static Inventory instance;
    
    
    // Use this for initialization
    void Start () {

        instance = this;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// Author:     Rômulo Lima
    /// <summary>
    ///  Informa se há itens no inventory
    /// </summary>
    /// <returns>Boolean</returns>
    public static bool isEmpty()
    {
        return instance.itemList.Count == 0;
    }

    
}
