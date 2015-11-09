using UnityEngine;
using UnityEngine.UI;

public class Balde : MonoBehaviour {

    public float waterPercent = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Não funcionando pois o objeto é desativado quando é pego
	void Update () {


    }


    public void UseWalter()
    {

        if (waterPercent < 0)
            waterPercent = 0;


            waterPercent--;
            updateInventoryStatus();

    }


    public void FillBucket()
    {

        if (waterPercent > 100)
            waterPercent = 100;

             waterPercent++;
             updateInventoryStatus();

    }

    public void updateInventoryStatus()
    {

        GameObject.Find("Inventory/item1/Text").GetComponent<Text>().text = waterPercent + "/100";

    }
}
