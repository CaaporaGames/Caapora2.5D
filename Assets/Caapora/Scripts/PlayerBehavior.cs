
using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;

namespace Completed { 
public class PlayerBehavior : CharacterBase {

    // Flag para o pause nao funcionando ainda
    protected bool paused = true;
	
    // Armazena o componente da animação
	public Animator animator;
	public float range;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private IsoBoxCollider boxCollider;
    private float moveTime;
    private LayerMask blockingLayer;

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
    public bool _moveUp = false, _moveDown = false, _moveLeft = false, _moveRight = false, _AKey = false, _BKey = false;

    private float _life = 1000;





	Text txt;

    // Rômulo Lima
	void Awake(){
		    
            // Acessar recursos de metodos estaticos
			instance = this;
		
	}

 	
    // Rômulo Lima 
    // Mateus Souza    
	// Use this for initialization
	protected void Start () {
		
        // Herda da classe base
		base.Start();


        instance = this;

		currentLevel = StatsController.GetCurrentLevel();

		animator = GetComponent<Animator>();
	
		// posiçao inicial do 
		gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);
		

        rb2D = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<IsoBoxCollider>();

        inverseMoveTime = 1f / moveTime;


    }

	// Rômulo Lima
	// Update is called once per frame
	void Update () {


            // Movimentação pelo teclado do player através de flags
            moveUpdate();

            // Habilitar a movimentação por clique no local
            // moveToPlace();

         
			

	}





        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Controle de movimentação baseada no clique do destino no mapa 
        /// </summary>
        void moveToPlace()
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
        /// Controle de movimentação pelas setas do teclado
        /// </summary> 
        void moveUpdate()
        {
            


           
            // Debug.Log("Touches = " + Input.touchCount);


            iso_rigidyBody = gameObject.GetComponent<IsoRigidbody>();

            // Checar movimentaçao do controle
            int h = (int)(Input.GetAxisRaw("Horizontal"));
            int v = (int)(Input.GetAxisRaw("Vertical"));

            if (h != 0)
            {
                v = 0;
            }

            // checa se houve algum controle do teclado ou controle
            if (h != 0 || v != 0)
            {
                //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
                //Pass in horizontal and vertical as parameters to specify the direction to move Player in.


                AttemptMove<Wall>(h, v);  // desabilitado 


            }


            if (iso_rigidyBody)
            {

                if (Input.GetKey(KeyCode.LeftArrow) || _moveLeft)
                {

                    moveLeft();

                }
                else if (Input.GetKey(KeyCode.RightArrow) || _moveRight)
                {

                    moveRight();

                }
                else if (Input.GetKey(KeyCode.DownArrow) || _moveDown)
                {

                    moveDown();

                }
                else if (Input.GetKey(KeyCode.UpArrow) || _moveUp)
                {

                    moveUp();

                }
                else if (Input.GetKeyDown(KeyCode.Space) || _BKey)
                {

                    paused = paused ? false : true;

                    Jump();
                  
                  
                }
                else if (!isPlayingAnimation)
                { // Caso nao esteja precionando nenhuma tecla

                    // Inicialmente apenas verifica se há itens
                    if (!Inventory.isEmpty())
                        animator.SetTrigger("CaaporaParaBalde-idle");
                    else
                        animator.SetTrigger("CaaporaIdle");

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
            GetComponent<PathFinding.Pathfinding>().click = true;

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

    
        // Mateus Souza
        // ???????
        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            //Store start position to move from, based on objects current transform position.
            Vector2 start = transform.position;

            // Calculate end position based on the direction parameters passed in when calling Move.
            Vector2 end = start + new Vector2(xDir, yDir);

            //Disable the boxCollider so that linecast doesn't hit this object's own collider.
            boxCollider.enabled = false;

            //Cast a line from start point to end point checking collision on blockingLayer.
            hit = Physics2D.Linecast(start, end, blockingLayer);

            //Re-enable boxCollider after linecast
            boxCollider.enabled = true;

            //Check if anything was hit
            if (hit.transform == null)
            {
                //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
                StartCoroutine(SmoothMovement(end));

                //Return true to say that Move was successful
                return true;
            }

            //If something was hit, return false, Move was unsuccesful.
            return false;
        }

        // Mateus Souza 
        // ??????
        protected IEnumerator SmoothMovement(Vector3 end)
        {
            //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
            //Square magnitude is used instead of magnitude because it's computationally cheaper.
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //While that distance is greater than a very small amount (Epsilon, almost zero):
            while (sqrRemainingDistance > float.Epsilon)
            {
                //Find a new position proportionally closer to the end, based on the moveTime
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

                //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                rb2D.MovePosition(newPostion);

                //Recalculate the remaining distance after moving.
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;

                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                yield return null;
            }
        }

        // Mateus Souza 
        // ??????
        protected virtual void AttemptMove<T>(int xDir, int yDir)
                where T : Component
        {
            //Hit will store whatever our linecast hits when Move is called.
            RaycastHit2D hit;

            //Set canMove to true if Move was successful, false if failed.
            bool canMove = Move(xDir, yDir, out hit);


            //Check if nothing was hit by linecast
            if (hit.transform == null)
                //If nothing was hit, return and don't execute further code.
                return;

            //Get a component reference to the component of type T attached to the object that was hit
            T hitComponent = hit.transform.GetComponent<T>();

            //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
            if (!canMove && hitComponent != null)

                //Call the OnCantMove function and pass it hitComponent as a parameter.
                OnCantMove(hitComponent);
        }

        // Mateus Souza
        protected void OnCantMove<T>(T component)
            where T : Component
        {
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