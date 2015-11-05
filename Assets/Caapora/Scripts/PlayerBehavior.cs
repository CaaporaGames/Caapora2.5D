using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;
using Pathfinding;

namespace Completed { 
public class PlayerBehavior : CharacterBase {

    protected bool paused = true;
	
	// public GameObject gameObject = null;
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

	public static bool isPlayingAnimation = false;
	public static PlayerBehavior instance;


	Text txt;


	void Awake(){
		
			instance = this;
		
	}

 	

	// Use this for initialization
	protected void Start () {
		

		base.Start();
		
       

		currentLevel = levelController.GetCurrentLevel();

		animator = GetComponent<Animator>();
	
		// posiçao inicial do 
		gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);
		

        rb2D = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<IsoBoxCollider>();

        inverseMoveTime = 1f / moveTime;


    }

	
	// Update is called once per frame
	void Update () {

            moveUpdate();
			
			/*
			if (gameObject.GetComponent<IsoObject> ().positionZ < -15) {

				Destroy(gameObject);
				Application.LoadLevel("GameOver");
				
			}*/
			

	}

   void moveUpdate()
        {

          
            // quando clicar com o botão esquerdo do Mouse
            if (Input.GetButtonDown("Fire1"))
            {
         
                // seta a posição alvo do player para a posição do clique no mapa
                GetComponent<PathFinding.Pathfinding>().targetPos = new Vector3(1, 20, 0);



                Debug.Log("Posicao clique =" + GetComponent<PathFinding.Pathfinding>().targetPos);

                // necessário executar apos um periodo de tempo para dar tempo de pegar a posição
                StartCoroutine(clicked());

            }


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

                if (Input.GetKey(KeyCode.LeftArrow))
                {

                    moveLeft();

                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {

                    moveRight();

                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {

                    moveDown();

                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {

                    moveUp();

                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {

                    paused = paused ? false : true;

                    Jump();
                  

                }
                else if (!isPlayingAnimation)
                { // Caso nao esteja precionando nenhuma tecla

                    animator.SetTrigger("CaaporaIdle");

                }

            }

        }



        void moveLeft()
        {

            //gameObject.GetComponent<IsoObject> ().position += new Vector3 (-this.speed, 0, 0);

            iso_rigidyBody.velocity = new Vector3(-this.speed, 0, 0);

            animator.SetTrigger("Caapora-left");

        }

        void moveRight()
        {

            //gameObject.GetComponent<IsoObject> ().position += new Vector3 (this.speed, 0, 0);
            iso_rigidyBody.velocity = new Vector3(this.speed, 0, 0);
            animator.SetTrigger("Caapora-right");

        }


        void moveDown()
        {
            //gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, -this.speed, 0);
            iso_rigidyBody.velocity = new Vector3(0, -this.speed, 0);
            animator.SetTrigger("Caapora-Norte");


        }


        void moveUp()
        {
            //gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, this.speed, 0);
            iso_rigidyBody.velocity = new Vector3(0, this.speed, 0);
            animator.SetTrigger("Caapora-Sul");

        }



        void Jump()
        {
            //gameObject.GetComponent<IsoRigidbody> ().velocity += new Vector3 (0, 0, 10f);
            iso_rigidyBody.velocity = new Vector3(0, 0, this.speed);
            animator.SetTrigger("Caapora-Norte");

        }

        // Seta a Flag para iniciar o Pathfinding apos um segundo para pegar a posição antes
        public IEnumerator clicked()
        {
            // aguarda um segundo
            yield return new WaitForSeconds(1);
            // Inicia percurso 
            GetComponent<PathFinding.Pathfinding>().click = true;

        }

	void OnPauseGame ()
	{
		paused = true;
	}
	

	
	void OnResumeGame ()
	{
		paused = false;
	}

    
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

    protected void OnCantMove<T>(T component)
        where T : Component
    {
    }



	IEnumerator RemoveBalloon() {
		
		yield return new WaitForSeconds(2);
		textBallon.AtiveBallon (false);
		

	}


		public static IEnumerator AnimateCaapora(string direction, int steps){
			
			instance.caapora = instance.gameObject.GetComponent<IsoObject> ();
			
			isPlayingAnimation = true;
			
			
					
			for (int i = 0; i < steps; i++)
			{

				Debug.Log("Passou em animate caapora for");
				
				instance.GetComponent<Animator>().SetTrigger(direction);
				
				 instance.caapora.position += new Vector3 (0, instance.speed, 0);
				//instance.iso_rigidyBody.velocity = new Vector3 (0, -instance.speed, 0);


				// caso seja a ultima animaçao
				isPlayingAnimation = (i == steps - 1) ? false : true;
				
				yield return new WaitForSeconds(.08f);
			}
		}

	public static IEnumerator ShakePlayer(){

			instance.caapora = instance.gameObject.GetComponent<IsoObject> ();
	
			for (int i = 0; i < 10; i++) {

				//instance.caapora.position += new Vector3 (0, 0, instance.speed  );
				instance.iso_rigidyBody.velocity = new Vector3 ( 0, 0, 0.5f);

				yield return new WaitForSeconds(.08f);	

			
			}
		
	}
		




	}

}