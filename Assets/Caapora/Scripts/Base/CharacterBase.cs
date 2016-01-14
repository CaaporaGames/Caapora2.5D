using IsoTools;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Caapora
{
   public abstract class CharacterBase : CreatureBase, IAutoMovable
    {


        protected static bool _canLauchWater = true;
        public Vector3 prevPosition;
        public bool stopWalking = false;
        protected Image ManaBar;

        protected override IsoObject iso_object { get; set; }
        protected override IsoRigidbody iso_rigidyBody { get; set; }

        public virtual void Start()
        {
            base.Start();

            _animator = GetComponent<Animator>();
            prevPosition = Vector3.zero;
        
        }

        
    
        public override void Update()
        {

            base.Update();

          
        }

        

        public void ThrowWater()
        {
           

            if (canLauchWater())
            {
                _canLauchWater = false;

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

            IsoRigidbody objRb = objeto.GetComponent<IsoRigidbody>();
            IsoObject obj = objeto.GetComponent<IsoObject>();
            objRb.mass = 0.1f;
   
            if (direction == "east")
            {
                obj.position = playerCurPosition + (Vector3.right + Vector3.down) / 3;
                objRb.velocity = (Vector3.right + Vector3.down) * 3;
                balde.UseWalter();
            }

            if (direction == "west")
            {
                obj.position = playerCurPosition + (Vector3.left + Vector3.up) / 3;
                objRb.velocity = (Vector3.left + Vector3.up) * 3;
                balde.UseWalter();
            }

            if (direction == "north")
            {
                obj.position = playerCurPosition + (Vector3.up + Vector3.right) / 3;
                objRb.velocity = (Vector3.up + Vector3.right) * 3;
                balde.UseWalter();
            }

            if (direction == "south")
            {
                obj.position = playerCurPosition + (Vector3.down + Vector3.left) / 3;
                objRb.velocity = (Vector3.down + Vector3.left) * 3;
                balde.UseWalter();
            }

           

            yield return new WaitForSeconds(0.5f);

            _canLauchWater = true;
      
        }


        public void AutomaticMovement(IAutoMovable character)
        {

            var _currentPosition = iso_object;

            var TmpPrevPosition = new Vector2(Mathf.Floor(prevPosition.x), Mathf.Floor(prevPosition.y));
            var TmpCurrentPosition = new Vector2(Mathf.Floor(_currentPosition.positionX), Mathf.Floor(_currentPosition.positionY));


            if (prevPosition == Vector3.zero || stopWalking)
            {
               // _animator.SetTrigger("Down");
            }
            else
            {

                if (TmpPrevPosition.x == TmpCurrentPosition.x + 1)
                {

                    character.moveLeft();

                }


                else if (TmpPrevPosition.x == TmpCurrentPosition.x - 1)
                {

                    character.moveRight();


                }



                else if (TmpPrevPosition.y == TmpCurrentPosition.y + 1)
                {

                    character.moveUp();


                }


                else if (TmpPrevPosition.y == TmpCurrentPosition.y - 1)
                {

                    character.moveDown();


                }

            }

        }

        
        private bool canLauchWater()
        {

            return !Inventory.isEmpty() && Inventory.getItem().GetComponent<Balde>().waterPercent > 0  && _canLauchWater;
        }

        public abstract void moveRight();
        public abstract void moveLeft();
        public abstract void moveUp();
        public abstract void moveDown();

        public float life
        {
            get
            {
                return _life;
            }
            set
            {
                _life = value;
            }
        }



    } // end CharacterController
} // end namespace
