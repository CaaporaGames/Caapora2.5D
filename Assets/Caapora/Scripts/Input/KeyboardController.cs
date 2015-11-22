using UnityEngine;
using System.Collections;
using IsoTools;


namespace Caapora
{

    public class KeyboardController : MonoBehaviour {


        public static KeyboardController instance;
        public Animator animator;
        public GameObject go;
        public IsoObject caapora;
        public float savedTimeState;
        public IsoRigidbody iso_rigidyBody;
        public static Vector3 prevPosition;
        // Sinalizador para a movimentação automática com Pathfinding
        public static bool stopWalking = false;
        public static bool isPlayingAnimation = false;
        private bool  _AKey = false, _BKey = false, _ZKey = false, _JKey = false;

        public string _lookingAt = "down";
        private float _life = 1000;
        private static bool _canLauchWater;
        private Camera mainCamera;
       


        // Use this for initialization
        void Start () {

            // Acessar recursos de metodos estaticos
            instance = this;
            mainCamera = GameObject.Find("Player/Camera").GetComponent<Camera>(); 

        }
	
	    // Update is called once per frame
	    void Update () {

            
       
            // Movimentação pelo teclado do player através de flags
            MainController();

           
            // Habilitar a movimentação por clique no local
            // moveToPlace();

        }



        

        // End métodos com movimentação


        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Método que ativa a flag para iniciar o pathfinding com a movimentação por clique no destino
        /// Está com um segundo de atraso para pegar a posição com antecedencia
        /// </summary>
        public IEnumerator clicked()
        {
          
            // aguarda um segundo
            yield return new WaitForSeconds(1);
            // Inicia percurso 
            // GetComponent<PathFinding.Pathfinding>().click = true;

        }




        /// *************************************************************************
        /// Author: Rômulo Lima e Mateus Souza
        /// <summary> 
        /// Controle de movimentação pelas setas do teclado e pelo Touch
        /// </summary> 
        void MainController()
        {

            // Seleciona a camera de acorod com a tecla
            if (GameManager.instance.zoomState == 1)
                mainCamera.orthographicSize = 80;
            else
                mainCamera.orthographicSize = 250;


            // habilita a animação
            if (isPlayingAnimation)
                animator.speed = 1;


            iso_rigidyBody = gameObject.GetComponent<IsoRigidbody>();


            if (iso_rigidyBody)
            {


                if (Input.GetKey(KeyCode.LeftArrow) || PlayerBehavior.moveDirection == "left")
                {

                    lookingAt = "left";
                    PlayerBehavior.instance.moveLeft();
                    
          

                }
                else if (Input.GetKey(KeyCode.RightArrow) || PlayerBehavior.moveDirection == "right")
                {
                    lookingAt = "right";
                    PlayerBehavior.instance.moveRight();


                }
                else if (Input.GetKey(KeyCode.DownArrow) || PlayerBehavior.moveDirection == "down")
                {

                    lookingAt = "down";
                    PlayerBehavior.instance.moveDown();


                }
                else if (Input.GetKey(KeyCode.UpArrow) || PlayerBehavior.moveDirection == "up")
                {
                    lookingAt = "up";
                    PlayerBehavior.instance.moveUp();


                }
                else if (Input.GetKeyDown(KeyCode.B) || _BKey)
                {
                    PlayerBehavior.instance.ThrowWater();

                }

                if (Input.GetKeyDown(KeyCode.Z) || _ZKey)
                {

                    GameManager.instance.showAllMap();

                }

                if (Input.GetKeyDown(KeyCode.J) || _JKey)
                {

                    JClick = false;
                    StartRun();

                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    PlayerBehavior.instance.Jump();
                    GameManager.instance.Pause();
                }

             

            }

        }


        public void StartRun()
        {
          
                PlayerBehavior.running = true;
        }


  
        // End métodos com movimentação





   



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
