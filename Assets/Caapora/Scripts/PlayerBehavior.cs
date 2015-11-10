
using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;

namespace Completed {
    [System.Serializable]
    public class PlayerBehavior : CharacterBase {

    // Flag para o pause nao funcionando ainda
    protected bool paused = true;
	
    // Armazena o componente da animação
	public Animator animator;
	public float speed = 5f;
	public GameObject go;
	public IsoObject caapora;
	public float savedTimeState;
	public IsoRigidbody iso_rigidyBody;
    public static Vector3 prevPosition;
    // Sinalizador para a movimentação automática com Pathfinding
    public static bool stopWalking = false;
	public static bool isPlayingAnimation = false;
	public static PlayerBehavior instance;
    private bool _moveUp = false, _moveDown = false, _moveLeft = false, _moveRight = false, _AKey = false, _BKey = false;
    private string LookingAtDirection = "north";
    private float _life = 1000;
    private static bool _canLauchWater;


 


	Text txt;

    // Rômulo Lima
	void Awake(){
		    
            // Acessar recursos de metodos estaticos
			instance = this;
		
	}

 	
    // Rômulo Lima  
	// Use this for initialization
	protected void Start () {
		
        // Herda da classe base
		base.Start();


        instance = this;

		currentLevel = StatsController.GetCurrentLevel();

		animator = GetComponent<Animator>();
	
		// posiçao inicial do 
		gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);
	


    }


        /// *************************************************************************
        /// Author: 
        /// <summary> 
        /// Sobrecarregou o método padrão do Unity OnCollisionEnter
        /// </summary>
        /// <param name="iso_collision">A referencia do objeto colidido</param>
        void OnIsoCollisionEnter(IsoCollision iso_collision)
        {

            Debug.Log("Colidingo com " + iso_collision.gameObject.name);
            GameManager.instance.debug_message = "Colidiu com a " + iso_collision.gameObject.name;

            // Colisao com o balde vazio
            if (iso_collision.gameObject.name == "waterPrefab")
            {
                Debug.Log("Colidiu com a agua");
                

                if (isPlayerWithBucket())
                {
                    Debug.Log("Player com balde proximo de agua");

                 
                    if (GameManager.instance.canFillBucket)
                        StartCoroutine(GameManager.instance.FillBucketSlowly());

                }


            }

        }



        bool isPlayerWithBucket()
        {

            var player = GameObject.Find("Player").GetComponent<IsoObject>();

            return !Inventory.isEmpty();

        }


        // Rômulo Lima
        // Update is called once per frame
        void Update () {


            // Movimentação pelo teclado do player através de flags
            MainController();

            // Habilitar a movimentação por clique no local
            // moveToPlace();

         
			

	}





        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Controle de movimentação baseada no clique do destino no mapa 
        /// </summary>
        void PathFindingController()
        {

            // Movimentação por clique na posição desejada
            var velocidade = gameObject.GetComponent<IsoRigidbody>().velocity;
            var _currentPosition = GetComponent<IsoObject>();


           // Debug.Log("prevPositon =" + prevPosition);
           // Debug.Log("Posicao atual =" + _currentPosition.position);


            // Animação por identificação de movimentação
            if (prevPosition == Vector3.zero || stopWalking)
            {
                animator.SetTrigger("Caapora-Iddle");
                _moveUp = false; _moveDown = false; _moveLeft = false; _moveRight = false;
            }
             
            else
            {

                if (prevPosition.x > _currentPosition.positionX)
                {
                    _moveLeft = true;
         
                }


                else if (prevPosition.x < _currentPosition.positionX)
                {
                    animator.SetTrigger("Caapora-right");
                

                }



                else if (prevPosition.y < _currentPosition.positionY)
                {
                    animator.SetTrigger("Caapora-Sul");
                

                }


                else if (prevPosition.y > _currentPosition.positionY)
                {
                    animator.SetTrigger("Caapora-Norte");
        
                }



            }


            // seta a posição alvo do player para a posição do clique no mapa
            //GetComponent<PathFinding.Pathfinding>().targetPos = new Vector3(13, 13, 0);

            // quando clicar com o botão esquerdo do Mouse
            // if (Input.GetButtonDown("Fire1"))


            if (Input.touchCount > 0)
            {

                //var clickIsoPosition = gameObject.GetComponent<IsoObject>().isoWorld.MouseIsoTilePosition();

                var clickIsoPosition = gameObject.GetComponent<IsoObject>().isoWorld.TouchIsoTilePosition(0);
                // seta a posição alvo do player para a posição do clique no mapa
                // GetComponent<PathFinding.Pathfinding>().targetPos = clickIsoPosition;

               // Debug.Log("Posicao do clique do mouse foi = " + clickIsoPosition);

                // necessário executar apos um periodo de tempo para dar tempo de pegar a posição
                StartCoroutine(clicked());

            }

        }




        /// *************************************************************************
        /// Author: Rômulo Lima e Mateus Souza
        /// <summary> 
        /// Controle de movimentação pelas setas do teclado e pelo Touch
        /// </summary> 
        void MainController()
        {

            // Debug.Log("Touches = " + Input.touchCount);

        
            iso_rigidyBody = gameObject.GetComponent<IsoRigidbody>();

   
            if (iso_rigidyBody)
            {

                if (Input.GetKey(KeyCode.LeftArrow) || _moveLeft)
                {

                    LookingAtDirection = "west";
                
                    moveLeft();

                }
                else if (Input.GetKey(KeyCode.RightArrow) || _moveRight)
                {

                    moveRight();

                    LookingAtDirection = "east";


                }
                else if (Input.GetKey(KeyCode.DownArrow) || _moveDown)
                {

                    moveDown();
                    LookingAtDirection = "south";

                }
                else if (Input.GetKey(KeyCode.UpArrow) || _moveUp)
                {

                    moveUp();

                    LookingAtDirection = "north";

                }
                else if (Input.GetKeyDown(KeyCode.B) || _BKey)
                {

                    paused = paused ? false : true;

                    ThrowWater();           
                  
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


        public void ThrowWater()
        {

            if (canLauchWater())
            {


                switch (LookingAtDirection)
                {

                    case "north":
                        StartCoroutine(launchOject("north",5));
                        break;
                    case "south":
                        StartCoroutine(launchOject("south", 5));
                        break;
                    case "east":
                        StartCoroutine(launchOject("east", 5));
                        break;
                    case "west":
                        StartCoroutine(launchOject("west", 5));
                        break;
                    default:
                        StartCoroutine(launchOject("south", 5));
                        break;

                }
 
            }

        }


        // Métodos com movimentação
       public void moveLeft()
        {

            
            iso_rigidyBody.velocity = new Vector3(-this.speed, 0, 0);

            animator.SetTrigger("Caapora-left");

        }

        public void moveRight()
        {

            
            iso_rigidyBody.velocity = new Vector3(this.speed, 0, 0);
            animator.SetTrigger("Caapora-right");

        }


        public void moveDown()
        {
         
            iso_rigidyBody.velocity = new Vector3(0, -this.speed, 0);
            animator.SetTrigger("Caapora-Sul");


        }


        public void moveUp()
        {
            
            iso_rigidyBody.velocity = new Vector3(0, this.speed, 0);
            animator.SetTrigger("Caapora-Norte");

        }



        public void Jump()
        {
            iso_rigidyBody.velocity = new Vector3(0, 0, this.speed);


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


        // Romulo Lima
        // Não funciona ainda
	    void OnPauseGame ()
	    {
		    paused = true;
	    }
	

	    // Não funciona ainda
	    void OnResumeGame ()
	    {
		    paused = false;
	    }

    
     


        // Remove Balão de coversa da tela
	    IEnumerator RemoveBalloon() {
		
		    yield return new WaitForSeconds(2);
		    ConversationPanel.ActivePanel (false);
		

	    }


        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Usado para criar animações onde o personagem se move sozinho
        /// </summary>
        /// <param name="direction">Direção que o personagem irá se mover</param>
        /// <param name="steps">A quantidade de passos que ele dará na direção</param>
        public static IEnumerator AnimateCaapora(string direction, int steps){
		
			instance.caapora = instance.gameObject.GetComponent<IsoObject> ();

           
            // Flag para animação automática
			isPlayingAnimation = true;
			

			for (int i = 0; i < steps; i++)
            {

                if(direction == "left")
                    instance._moveLeft = true;

                if (direction == "right")
                    instance._moveRight = true;

                if (direction == "up")
                    instance._moveUp = true;

                if (direction == "down")
                    instance._moveDown = true;

                if (direction == "jump")
                    instance._AKey = true;

                // caso seja a ultima animaçao
                isPlayingAnimation = (i == steps - 1) ? false : true;
				
				yield return new WaitForSeconds(.08f);
			}

            // Ao encerrar a animação zera as Flags
            instance._moveLeft = false;
            instance._moveRight = false;
            instance._moveUp = false;
            instance._moveDown = false;
            instance._AKey = false;
        }



        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Balança o player para sinalizar algo
        /// </summary>
        public static IEnumerator ShakePlayer(){

			instance.caapora = instance.gameObject.GetComponent<IsoObject> ();
	
			for (int i = 0; i < 10; i++) {

				//instance.caapora.position += new Vector3 (0, 0, instance.speed  );
				instance.iso_rigidyBody.velocity = new Vector3 ( 0, 0, 0.5f);

				yield return new WaitForSeconds(.08f);	

			
			}
		
	    }


    

        /// <summary>
        /// Anima o lançamento de um objeto
        /// </summary>
        /// <param name="distance">a distancia a ser percorrida pelo objeto</param>
        /// <param name="direction"> a direcao a ser enviado o objeto</param>
        /// <returns></returns>
        public IEnumerator launchOject(string direction, float distance)
        {


            // Carrega o prefab da água
            var objeto = Instantiate(Resources.Load("Prefabs/splashWaterPrefab")) as GameObject;

            // a posicao inicial do objeto
            var playerCurPosition = GetComponent<IsoObject>().position;

            var balde = Inventory.getItem().GetComponent<Balde>();



          //  for (int i =0; i < distance; )
          //  {

                if (direction == "east")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.right;
                    balde.UseWalter();
                }
                  
                if (direction == "west")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.left;
                    balde.UseWalter();
                }

                if (direction == "north")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.up;
                     balde.UseWalter();
                }

                if (direction == "south")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.down;
                    balde.UseWalter();
                }


                yield return new WaitForSeconds(0.1f);
        //    }


        }




     

        // Métodos de Flags para ativar a movimentação
        public bool moveUpClick
        {
            get
            {
                return this._moveUp;
            }
            set
            {
                this._moveUp = value;
            }
        }



        public bool moveDownClick
        {
            get
            {
                return this._moveDown;
            }
            set
            {
                this._moveDown = value;
            }
        }

        public string _LookingAtDirection
        {
            get
            {
                return this.LookingAtDirection;
            }
            set
            {
                this.LookingAtDirection = value;
            }
        }

        public bool moveLeftClick
        {
            get
            {
                return this._moveLeft;
            }
            set
            {
                this._moveLeft = value;
            }
        }


        public bool moveRightClick
        {
            get
            {
                return this._moveRight;
            }
            set
            {
                this._moveRight = value;
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
                this._AKey= value;
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

        /// <summary>
        /// Condição para poder jogar água
        /// </summary>
        /// <returns></returns>
        private bool canLauchWater()
        {
            // Se houver algum item no invetory e esse item tiver agua
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
        
        // End Flags para ajudar na movimentação




    }

}