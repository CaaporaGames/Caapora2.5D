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
        private bool  _AKey = false, _BKey = false;
        public string _moveDirection = "";
        public string _lookingAt = "down";
        private float _life = 1000;
        private static bool _canLauchWater;

        // Use this for initialization
        void Start () {

            // Acessar recursos de metodos estaticos
            instance = this;
            animator = GetComponent<Animator>();

        }
	
	    // Update is called once per frame
	    void Update () {

            Debug.Log(_moveDirection);
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
            Debug.Log("Clicou e habilitou o _start");
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



            // habilita a animação
            if (isPlayingAnimation)
                animator.speed = 1;


            iso_rigidyBody = gameObject.GetComponent<IsoRigidbody>();


            if (iso_rigidyBody)
            {


                if (Input.GetKey(KeyCode.LeftArrow) || _moveDirection == "left")
                {

                    lookingAt = "left";
                    moveLeft();

                }
                else if (Input.GetKey(KeyCode.RightArrow) || _moveDirection == "right")
                {
                    lookingAt = "right";
                    moveRight();


                }
                else if (Input.GetKey(KeyCode.DownArrow) || _moveDirection == "down")
                {

                    lookingAt = "down";
                    moveDown();


                }
                else if (Input.GetKey(KeyCode.UpArrow) || _moveDirection == "up")
                {
                    lookingAt = "up";
                    moveUp();


                }
                else if (Input.GetKeyDown(KeyCode.B) || _BKey)
                {
                    Debug.Log("Apertou B");
                    PlayerBehavior.instance.ThrowWater();

                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Jump();
                }

                /*
                else if (!isPlayingAnimation)
                { // Caso nao esteja precionando nenhuma tecla

                    // Inicialmente apenas verifica se há itens
                    if (!Inventory.isEmpty())
                        animator.SetTrigger("CaaporaParaBalde-idle");
                    else
                        animator.SetTrigger("CaaporaIdle");

                }*/

            }

        }



        // Métodos com movimentação
        public void moveLeft()
        {


            iso_rigidyBody.velocity = new Vector3(-PlayerBehavior.instance.speed, 0, 0);

            animator.SetTrigger("Caapora-left");

        }

        public void moveRight()
        {


            iso_rigidyBody.velocity = new Vector3(PlayerBehavior.instance.speed, 0, 0);
            animator.SetTrigger("Caapora-right");

        }


        public void moveDown()
        {

            iso_rigidyBody.velocity = new Vector3(0, -PlayerBehavior.instance.speed, 0);
            animator.SetTrigger("Caapora-Sul");


        }


        public void moveUp()
        {

            iso_rigidyBody.velocity = new Vector3(0, PlayerBehavior.instance.speed, 0);
            animator.SetTrigger("Caapora-Norte");

        }



        public void Jump()
        {
            iso_rigidyBody.velocity = new Vector3(0, 0, PlayerBehavior.instance.speed);


        }


        // End métodos com movimentação



        // Métodos de Flags para ativar a movimentação
       

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
                return this._AKey;
            }
            set
            {
                this._AKey = value;
            }
        }


        public bool BClick
        {
            get
            {
                return this._BKey;
            }
            set
            {
                this._BKey = value;
            }
        }

    }

}
