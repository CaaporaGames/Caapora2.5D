using UnityEngine;
using System.Collections;
using IsoTools;
using Caapora;
using Caapora.Pathfinding;

namespace Caapora { 
public class Foe : NPCBase {
	

	private Animator animator;
	public IsoObject foe;
    private NPC FoeNPC;


	public static bool isPlayingAnimation = false;
	public static Foe instance;

	private void  Start () {
        base.Start();

        speed = 0.2f;
        instance = this;

        FoeNPC = GetComponent<NPC>();

        iso_rigidyBody = GetComponent<IsoRigidbody>();

        iso_object = GetComponent<IsoObject>();

        animator = GetComponent<Animator>();



	}
	

    private void Update()
    {
            base.Update();

            if (FoeNPC.IsTargetNear()) {

                Atack(FoeNPC._targetPos.GetComponent<CreatureBase>(), basicStats.baseAttack);
                FoeNPC.IsNear = false;
            }
           
    }
	
	public static IEnumerator moveInSquarePath(){


		instance.StartCoroutine (AnimateFoe("Down", 10));
		yield return new WaitForSeconds(3f); 
		instance.StartCoroutine (AnimateFoe ("Left", 10));
		yield return new WaitForSeconds(3f); 
		
		
	}


    private void Atack(CreatureBase go, float damage)
    {
        //Debug.Log("NPC Atacando");
        go.Hit(damage);

    }

    public static IEnumerator AnimateFoe(string direction, int steps){
		

		isPlayingAnimation = true;

		instance.foe = instance.gameObject.GetComponent<IsoObject> ();
		
		instance.GetComponent<Animator>().SetTrigger(direction);

		for (int i = 0; i < steps; i++)
		{


			if(direction == "Left"){
			
				instance.foe.position += new Vector3 (-instance.speed, 0, 0);
			}else {
				instance.foe.position += new Vector3 (0, -instance.speed, 0);
			}

			isPlayingAnimation = (i == steps - 1) ? false : true;
			
			yield return new WaitForSeconds(.1f);
		}
	}
}

}

