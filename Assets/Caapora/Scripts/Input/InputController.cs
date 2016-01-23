using UnityEngine;
using System.Collections;
using IsoTools;
using Caapora.Pathfinding;

namespace Caapora
{

    public class InputController : MonoBehaviour {

        public bool MoveToPlace = false;
        private static InputController _instance;
        public Animator animator;
        public GameObject go;
        public IsoObject caapora;
        public float savedTimeState;
        public IsoRigidbody iso_rigidyBody;
        public static Vector3 prevPosition;
        public static bool stopWalking = false;
        public static bool isPlayingAnimation = false;
        private bool  _AKey = false, _BKey = false, _ZKey = false, _JKey = false;

        public string _lookingAt = "down";
        private float _life = 1000;
        private static bool _canLauchWater;
        private Camera mainCamera;


        void Awake()
        {
            if (_instance == null)
            {

                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {

                if (this != _instance)
                    Destroy(this.gameObject);
            }
        }



        public static InputController instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InputController>();

                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        void Start () {

            mainCamera = GameObject.Find("Player/Camera").GetComponent<Camera>(); 

        }
	

	    void Update () {

            MainController();


            if (MoveToPlace)
            {
                if (Touched() || Clicked())
                {



                    var clickIsoPosition = Touched() ?
                     mainCamera.GetComponent<IsoWorld>().TouchIsoTilePosition(0) :
                     mainCamera.GetComponent<IsoWorld>().MouseIsoTilePosition();


                    GetComponent<GoToPlace>().targetPos = clickIsoPosition;

                    StartCoroutine(clicked());


                }

            }

        }


        private bool Touched()
        {
            return Input.touchCount > 0;
        }



        private bool Clicked()
        {

            return Input.GetButtonDown("Fire1");
        }

        

        public IEnumerator clicked()
        {

            yield return new WaitForSeconds(1);

            GetComponent<GoToPlace>().click = true;

        }



        void MainController()
        {


            if (isPlayingAnimation)
                animator.speed = 1;


            iso_rigidyBody = gameObject.GetComponent<IsoRigidbody>();


            if (iso_rigidyBody)
            {


                if (Input.GetKey(KeyCode.LeftArrow) || Caapora.moveDirection == "left")
                {

                    lookingAt = "left";
                    Caapora.instance.moveLeft();
                    
          

                }
                else if (Input.GetKey(KeyCode.RightArrow) || Caapora.moveDirection == "right")
                {
                    lookingAt = "right";
                    Caapora.instance.moveRight();


                }
                else if (Input.GetKey(KeyCode.DownArrow) || Caapora.moveDirection == "down")
                {

                    lookingAt = "down";
                    Caapora.instance.moveDown();


                }
                else if (Input.GetKey(KeyCode.UpArrow) || Caapora.moveDirection == "up")
                {
                    lookingAt = "up";
                    Caapora.instance.moveUp();


                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.LeftArrow) || Caapora.moveDirection == "up")
                {
                    lookingAt = "up";
                    Caapora.instance.moveUpLeft();


                }
                else if (Input.GetKeyDown(KeyCode.B) || _BKey)
                {
                    Caapora.instance.ThrowWater();

                }
                else
                {

                    Caapora.instance.animator.SetTrigger("CaaporaIdle");
                }

                if (Input.GetKeyDown(KeyCode.Z) || _ZKey)
                {

                    GameManager.instance.MapZoomOut();

                }

                if (Input.GetKeyUp(KeyCode.Z))
                {

                    GameManager.instance.MapZoomIn();

                }

                if (Input.GetKeyDown(KeyCode.X) || _JKey)
                {

                    JClick = false;
                    StartRun();

                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    Caapora.instance.Jump();
                    GameManager.instance.Pause();
                }

             

            }

        }


        public void StartRun()
        {
          
                Caapora.running = true;
        }


 


        public string lookingAt
        {
            get
            {
                return _lookingAt;
            }

            set
            {
                _lookingAt = value;
            }
        }



        public bool AClick
        {
            get
            {
              
                return _AKey;
            }
            set
            {
               
                _AKey = value;
            }
        }

        public bool ZClick
        {
            get
            {
                return _ZKey;
            }
            set
            {
               _ZKey = value;
            }
        }


        public bool BClick
        {
            get
            {
                return _BKey;
            }
            set
            {
                _BKey = value;
            }
        }



        public bool JClick
        {
            get
            {
                return _JKey;
            }
            set
            {
                _JKey = value;
            }
        }

    }

}
