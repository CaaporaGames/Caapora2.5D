using IsoTools;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Caapora
{
   public abstract class CharacterBase : CreatureBase, IAutoMovable
    {


        protected static bool _canLauchWater = true;
        public static bool isPlayingAnimation;
        public Vector3 prevPosition;
        public bool stopWalking = false;
        protected Image ManaBar;
        public string _moveDirection;
        private GameObject Water;
        private GameObject Bucket;

        protected override IsoObject iso_object { get; set; }
        protected override IsoRigidbody iso_rigidyBody { get; set; }

        public virtual void Awake()
        {

            _canLauchWater = true;
            _animator = GetComponent<Animator>();
            prevPosition = Vector3.zero;

        }

        
        public virtual void Start()
        {
            base.Start();
        }
        
        public virtual void Update()
        {
            base.Update();
            MainController();

        }


        public void ThrowWater()
        {

            if (canLauchWater())
            {

                _canLauchWater = false;

                _animator.SetTrigger("Atack2");

                StartCoroutine(launchOject("Water", 5));

            }

        }


        public IEnumerator launchOject(string type, float distance)
        {


            GameObject objeto;

            switch (type)
            {
                case "Water":
                    objeto = Instantiate(Resources.Load("Prefabs/splashWaterPrefab")) as GameObject;
                    break;
                case "Bucket":
                    GameObject baldeTmp = Inventory.getItem();
                    baldeTmp.GetComponent<Balde>().Active();
                    objeto = baldeTmp;
                    break;
                default:
                    objeto = Instantiate(Resources.Load("Prefabs/splashWaterPrefab")) as GameObject;
                    break;
            }


            var playerCurPosition = GetComponent<IsoObject>().position;

            var balde = Inventory.getItem().GetComponent<Balde>();

            IsoRigidbody objRb = objeto.GetComponent<IsoRigidbody>();
            IsoObject obj = objeto.GetComponent<IsoObject>();
            objRb.mass = 0.1f;
   
            if (InputController.instance.lookingAt == "right")
            {
                obj.position = playerCurPosition + (Vector3.right + Vector3.down) / 3;
                objRb.velocity = (Vector3.right + Vector3.down) * 3;
                balde.UseWalter();
            }

            if (InputController.instance.lookingAt == "left")
            {
                obj.position = playerCurPosition + (Vector3.left + Vector3.up) / 3;
                objRb.velocity = (Vector3.left + Vector3.up) * 3;
                balde.UseWalter();
            }

            if (InputController.instance.lookingAt == "up")
            {
                obj.position = playerCurPosition + (Vector3.up + Vector3.right) / 3;
                objRb.velocity = (Vector3.up + Vector3.right) * 3;
                balde.UseWalter();
            }

            if (InputController.instance.lookingAt == "down")
            {
                obj.position = playerCurPosition + (Vector3.down + Vector3.left) / 3;
                objRb.velocity = (Vector3.down + Vector3.left) * 3;
                balde.UseWalter();
            }

           

            yield return new WaitForSeconds(0.5f);

            _canLauchWater = true;
      
        }


        


        public IEnumerator CharacterMovement(string direction, int steps)
        {


            isPlayingAnimation = true;


            for (int i = 0; i < steps; i++)
            {

                moveDirection = direction;

                if (direction == "jump")
                    InputController.instance.AClick = true;

                isPlayingAnimation = (i == steps - 1) ? false : true;

                yield return new WaitForSeconds(.02f);

                moveDirection = "";
            }

        }



        public string moveDirection
        {
            get
            {
                return _moveDirection;
            }

            set
            {
                _moveDirection = value;
            }
        }




        public IEnumerator ShakePlayer()
        {

           // instance.caapora = instance.gameObject.GetComponent<IsoObject>();

            for (int i = 0; i < 10; i++)
            {


                iso_rigidyBody.velocity = new Vector3(0, 0, 0.5f);

                yield return new WaitForSeconds(.08f);


            }

        }



        private void MainController()
        {



            if (isPlayingAnimation)
                animator.speed = 1;


            if (iso_rigidyBody)
            {


                if (moveDirection == "left")
                {

                    moveLeft();

                }
                else if (moveDirection == "right")
                {

                    moveRight();

                }
                else if (moveDirection == "down")
                {

                    moveDown();

                }
                else if (moveDirection == "up")
                {

                    moveUp();

                }


                else
                {

                    animator.SetTrigger("Idle");
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
