using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;

namespace Caapora { 
public class EnemyBehavior : NPCController {


	// Use this for initialization
	protected void Start () {
		base.Start();

            currentLevel = StatsController.GetCurrentLevel();

	}


        void OnIsoCollisionStay(IsoCollision iso_collision)
        {

            base.OnIsoCollisionStay(iso_collision);

            // Colisao com o balde vazio
            if (iso_collision.gameObject.name == "splashWaterPrefab(Clone)")
            {
               
                _life = _life - 50;
              

            }
        }

    
}

}