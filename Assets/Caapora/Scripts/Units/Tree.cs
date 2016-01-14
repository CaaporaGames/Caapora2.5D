using UnityEngine;
using System.Collections;
using IsoTools;


namespace Caapora
{


public class Tree : CreatureBase {

        protected override IsoRigidbody iso_rigidyBody { get; set; }
        protected override IsoObject iso_object { get; set; }

        public override void Update () {
            base.Update();
	
	    }

        public void Start(){
            base.Start();
        }

        void RecoverLife()
        {

            _life = _life + 10;

        }


        void OnIsoCollisionEnter(IsoCollision iso_collision)
        {
            base.OnIsoCollisionEnter(iso_collision);

            if (iso_collision.gameObject.name == "splashWaterPrefab(Clone)")
            {

                RecoverLife();
            }

        }
    }

}