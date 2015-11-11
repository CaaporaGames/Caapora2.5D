
using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;

namespace Caapora {
    [System.Serializable]
    public class PlayerBehavior : CharacterController {

	
    // Armazena o componente da animação
	public Animator animator;
	public GameObject go;
	public IsoObject caapora;
	public IsoRigidbody iso_rigidyBody;
    public static Vector3 prevPosition;
    // Sinalizador para a movimentação automática com Pathfinding
    public static bool stopWalking = false;
	public static bool isPlayingAnimation = false;
	public static PlayerBehavior instance;
    private bool _moveUp = false, _moveDown = false, _moveLeft = false, _moveRight = false, _AKey = false, _BKey = false;
    private string _moveDirection = "";



    // Rômulo Lima
	void Awake(){
		    
            // Acessar recursos de metodos estaticos
			instance = this;
		
	}


        void Update() {
            base.Update();

            if (!GameManager.isAnimating)
                GameObject.Find("Status/hp").GetComponent<Text>().text = _life.ToString();



        }


        // Rômulo Lima  
        // Use this for initialization
        protected void Start () {
		
         // Herda da classe base
		    base.Start();


        instance = this;

		currentLevel = StatsController.GetCurrentLevel();

		animator = GetComponent<Animator>();
	


    }


        /// *************************************************************************
        /// Author: 
        /// <summary> 
        /// Sobrecarregou o método padrão do Unity OnCollisionEnter
        /// </summary>
        /// <param name="iso_collision">A referencia do objeto colidido</param>
        void OnIsoCollisionEnter(IsoCollision iso_collision)
        {
            // código comentado abaixo é ótimo para debug
            // Debug.Log("Colidingo com " + iso_collision.gameObject.name);
            DebugGame.instance.debug_message = "Colidiu com a " + iso_collision.gameObject.name;

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

                if (direction == "left")
                    instance._moveDirection = "left";

                if (direction == "right")
                    instance._moveDirection = "right";

                if (direction == "up")
                    instance._moveDirection = "up";

                if (direction == "down")
                    instance._moveDirection = "down";

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


    
        

    }

}