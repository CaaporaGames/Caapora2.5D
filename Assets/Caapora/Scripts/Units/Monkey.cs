using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Caapora
{


    public class Monkey : Character {

	    // Use this for initialization
	    void Start () {
	
	    }

    

        // Update is called once per frame
        void Update () {
            base.Update();

           

            if(!GameManager.isAnimating)
              GameObject.Find("Monkey/hp").GetComponent<Text>().text = _life.ToString();


        }
    }

}
