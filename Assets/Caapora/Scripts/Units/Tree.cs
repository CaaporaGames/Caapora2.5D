using UnityEngine;
using System.Collections;
using IsoTools;


namespace Caapora
{


public class Tree : Character {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
            base.Update();
	
	}


    

    
        void RecoverLife()
        {

            _life = _life + 10;

        }


        /// *************************************************************************
        /// Author: 
        /// <summary> 
        /// Sobrecarregou o método padrão do Unity OnCollisionEnter
        /// </summary>
        /// <param name="iso_collision">A referencia do objeto colidido</param>
        void OnIsoCollisionEnter(IsoCollision iso_collision)
        {
            base.OnIsoCollisionEnter(iso_collision);

            // Caso o fogo colida com o splash de agua deleta os dois
            if (iso_collision.gameObject.name == "splashWaterPrefab")
            {


                RecoverLife();

            }


        }
    }

}