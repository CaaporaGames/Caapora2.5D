using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Caapora
{


    public class Monkey : Character {

        Text MonkeyHp;

	    // Use this for initialization
	    void Start () {

            MonkeyHp = GameObject.Find("Monkey/hp").GetComponent<Text>();
        }


    

        // Update is called once per frame
        void Update () {
            base.Update();

           

            if(!GameManager.isAnimating)
              MonkeyHp.text = _life.ToString();


        }
    }

}
