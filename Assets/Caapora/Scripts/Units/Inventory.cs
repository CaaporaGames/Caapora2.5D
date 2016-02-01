using UnityEngine;
using System.Collections.Generic;
using IsoTools;
using UnityEngine.UI;

namespace Caapora {



public class Inventory : MonoBehaviour {


    private static List<GameObject> itemList = new List<GameObject>();
    private static Inventory _instance;
    
    

    void Start () {

        _instance = this;

	}

        public void Awake()
        {


            if (_instance == null)
            {

                DontDestroyOnLoad(this);


                _instance = this;

            }

            else
            {

                if (this != _instance)
                    Destroy(gameObject);
            }



        }


        public static Inventory instance
        {
            get
            {
                if (_instance == null)
                {
                    DontDestroyOnLoad(_instance);

                    _instance = FindObjectOfType<Inventory>() as Inventory;
                }

                return _instance;
            }
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