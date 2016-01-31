using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IsoTools;


namespace Caapora
{


    public class Monkey : NPCBase {

        Text MonkeyHp;


	    public override void Start () {
            base.Start();

            MonkeyHp = GameObject.Find("Monkey/hp").GetComponent<Text>();

            iso_rigidyBody = GetComponent<IsoRigidbody>();

            iso_object = GetComponent<IsoObject>();

            _animator = GetComponent<Animator>();

        }


    


        public  void Update () {
            base.Update();

            if (!GameManager.isAnimating)
              MonkeyHp.text = _life.ToString();


        }
    }

}
