using UnityEngine;
using UnityEngine.UI;

public class Balde : MonoBehaviour {

    public float waterPercent = 0.0f;
    public static Balde instance;


	// Use this for initialization
	void Start () {

        instance = this;
	}
	
	// Não funcionando pois o objeto é desativado quando é pego
	void Update () {


    }


    public void UseWalter()
    {

        waterPercent--;

        if (waterPercent < 1)
            waterPercent = 0;

            updateInventoryStatus();

    }


    public void FillBucket()
    {

        waterPercent += 10;

        if (waterPercent > 99)
            waterPercent = 100;

            
            updateInventoryStatus();

    }

    public void updateInventoryStatus()
    {
        if (waterPercent < 1)
            GameObject.Find("Inventory/item1").GetComponent<Image>().color = Color.red;
        else
            GameObject.Find("Inventory/item1").GetComponent<Image>().color = Color.cyan;

        GameObject.Find("Inventory/item1/Text").GetComponent<Text>().text = waterPercent + "/100";

        GameObject.Find("WaterNivel").GetComponent<Image>().fillAmount = waterPercent / 100;



    }
}
