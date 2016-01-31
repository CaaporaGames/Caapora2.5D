using UnityEngine;
using System.Collections.Generic;
using IsoTools;
using UnityEngine.UI;

namespace Caapora {



public class Inventory : MonoBehaviour {


    private static List<GameObject> itemList = new List<GameObject>();
    public static Inventory instance;
    
    

    void Start () {

        instance = this;

	}
	


    
    public static bool isEmpty()
    {
        return itemList.Count == 0;
    }


   
    public static GameObject getItem()
    {
        return itemList[0];
    }


    public static void AddItem(GameObject item)
    {

        itemList.Add(item);

    }

    public static void RemoveItem(GameObject item)
    {

        if (!isEmpty())
        {
            itemList.Remove(item);

        }
           
    }


}
}