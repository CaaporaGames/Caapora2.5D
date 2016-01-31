using UnityEngine;
using System.Collections;


public class Advice : MonoBehaviour {

    public static Advice instance;
    

    void Awake () {


        instance = this;

        ShowAdvice(false);

    }


    public void ShowAdvice(bool value)
    {

        if(value)
        {

            Caapora.GameManager.ShowObjectAPeriodOfTime(gameObject, 5);
          
        }
             
        else
             gameObject.SetActive(false);

    }
}
