using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdviceSimple : MonoBehaviour {


    public static AdviceSimple instance;
    public static bool enable = false;
    private Text adviceText;

   
    void Start () {

        
        instance = this;

        adviceText = GameObject.Find("PanelConversa/AdviceSimple/Text").GetComponent<Text>();

        gameObject.SetActive(false);

    }
	

	void Update () {
	
	}

 
    public static void showAdvice(string message = "")
    { 

 
            instance.adviceText.text = message;
            enable = true;
            GameManager.ShowObjectAPeriodOfTime(instance.gameObject, 5);
            

        

    }
}
