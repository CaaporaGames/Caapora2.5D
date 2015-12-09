using IsoTools;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Caapora
{
   public class Character : CreatureBase
    {

        
        protected static bool _canLauchWater;
        private Scrollbar lifeBar;


        public virtual void Start()
        {

            _animator = GetComponent<Animator>();
        
        }



    
        public override void Update()
        {

            base.Update();

         
        }

        

        public void ThrowWater()
        {

           
            if (canLauchWater())
            {
                _animator.SetTrigger("Atack2");

                

                switch (InputController.instance.lookingAt)
                {

                        
                    case "up":
                        StartCoroutine(launchOject("north", 5));
                        break;
                    case "down":
                        StartCoroutine(launchOject("south", 5));
                        break;
                    case "right":
                        StartCoroutine(launchOject("east", 5));
                        break;
                    case "left":
                        StartCoroutine(launchOject("west", 5));
                        break;
                    default:
                        StartCoroutine(launchOject("south", 5));
                        break;

                }

            }

        }


        public IEnumerator launchOject(string direction, float distance)
        {

            var objeto = Instantiate(Resources.Load("Prefabs/splashWaterPrefab")) as GameObject;


            var playerCurPosition = GetComponent<IsoObject>().position;

            var balde = Inventory.getItem().GetComponent<Balde>();


            if (direction == "east")
            {
                objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.right + Vector3.down;
                balde.UseWalter();
            }

            if (direction == "west")
            {
                objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.left + Vector3.up;
                balde.UseWalter();
            }

            if (direction == "north")
            {
                objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.up + Vector3.right;
                balde.UseWalter();
            }

            if (direction == "south")
            {
                objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.down + Vector3.left;
                balde.UseWalter();
            }


            yield return new WaitForSeconds(0.1f);


        }

        
        private bool canLauchWater()
        {

            return !Inventory.isEmpty() && Inventory.getItem().GetComponent<Balde>().waterPercent > 0;
        }

        public float life
        {
            get
            {
                return this._life;
            }
            set
            {
                this._life = value;
            }
        }



    } // end CharacterController
} // end namespace
