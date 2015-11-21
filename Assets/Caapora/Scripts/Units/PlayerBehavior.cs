
using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;

namespace Caapora {
    [System.Serializable]
    public class PlayerBehavior : Character {

	
    // Armazena o componente da animação
	public GameObject go;
	public IsoObject caapora;
	public IsoRigidbody iso_rigidyBody;
    public static Vector3 prevPosition;
    // Sinalizador para a movimentação automática com Pathfinding
    public static bool stopWalking = false;
	public static bool isPlayingAnimation = false;
	public static PlayerBehavior instance;
    private bool  _AKey = false, _BKey = false;
    private string _moveDirection = "";
    public Sprite baldeCheio;
    public bool canFillBucket = true;
    private Vector3 direction;
    private float currentXp;
    private    Text StatusHP;
    private    GameObject balde;



        // Rômulo Lima  
        // Use this for initialization
        protected void Start()
        {

            // Herda da classe base
            base.Start();

            StatusHP = GameObject.Find("Status/hp").GetComponent<Text>();
            balde = GameObject.Find("baldeVazioPrefab"); 

            instance = this;


        }


        // Rômulo Lima
        void Awake(){
		    
            // Acessar recursos de metodos estaticos
			instance = this;
		
	}



        void Update() {


            base.Update();

            currentLevel = StatsController.GetCurrentLevel();
            currentXp = StatsController.GetCurrentXp();



            /* create a ray going into the scene from the screen location the user clicked at
           
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            // TESTES COM RAYCAST

            Vector3 direction = (GameObject.Find("centerRef").transform.position - transform.position).normalized;

            Ray ray = new Ray(transform.position, direction);

            // the raycast hit info will be filled by the Physics.Raycast() call further
            RaycastHit hit;

            // perform a raycast using our new ray. 
            // If the ray collides with something solid in the scene, the "hit" structure will
            // be filled with collision information
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(hit.point , ray.origin);
            }

             */



            if (!GameManager.isAnimating)
               StatusHP.text = _life.ToString();



                if (!Inventory.isEmpty())
                {

                    if (Balde.instance.waterPercent <= 0.0f)
                         AdviceSimple.showAdvice("Ande próximo ao lago para encher o balde com água!");
                }


            if (Inventory.isEmpty()) { 
                

                    if (canCatch(balde))
                    {

                    // Exibe a dica de tela
                    Advice.ShowAdvice(true);

                    // Checa por entrada de dados
                    if (Input.GetKeyDown(KeyCode.A) || KeyboardController.instance.AClick)
                        {
                            

                            AddItemToInventory(balde);

                            // Migra a animação para a do balde
                            animator.SetTrigger("Catch");

                            balde.SetActive(false);



                        }

                    }

            }




        }



        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// 
        /// Adiciona um item no painel na tela 
        /// <param name="item">item no painel</param> 
        /// <returns></returns>
        /// 
        /// </summary>
        /// *************************************************************************
        void AddItemToInventory(GameObject item)
        {
            Inventory.instance.itemList.Add(item);

            // Temporário
            GameObject.Find("item1").GetComponent<Image>().sprite = Resources.Load("Sprites/balde", typeof(Sprite)) as Sprite;

        }

        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// 
        /// Retorna true se a posição do objeto é vizinha em uma posição do objeto alvo
        /// <param name="go">objeto alvo</param> 
        /// <returns></returns>
        /// 
        /// </summary>
        /// *************************************************************************
        bool canCatch(GameObject go)
        {


            // Percorre todos as posicoes vizinhas
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {



                    if (go != null)
                        if (Mathf.RoundToInt(go.GetComponent<IsoObject>().positionX) == Mathf.RoundToInt(GetComponent<IsoObject>().positionX + x) &&
                                Mathf.RoundToInt(go.GetComponent<IsoObject>().positionY) == Mathf.RoundToInt(GetComponent<IsoObject>().positionY + y))
                        {

                            return true;
                        }


                }
            }


            return false;


        }




        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Enche o balde forma lenta
        /// </summary>
        public IEnumerator FillBucketSlowly()
        {
            // Desabilita o preenchimento do balde temporariamente   
            canFillBucket = false;

            var balde = Inventory.getItem().GetComponent<Balde>();

         
            animator.SetTrigger("Catch");

            // Incrementa a porcentagem de agua em um em um
            balde.FillBucket();


            yield return new WaitForSeconds(0.1f);

            canFillBucket = true;



        }


        void OnIsoCollisionEnter(IsoCollision iso_collision)
        {
           // base.OnIsoCollisionEnter(iso_collision);

            // código comentado abaixo é ótimo para debug
            // Debug.Log("Colidingo com " + iso_collision.gameObject.name);
            // DebugGame.instance.debug_message = "Colidiu com a " + iso_collision.gameObject.name;

        }


        void OnIsoCollisionStay(IsoCollision iso_collision)
        {

            base.OnIsoCollisionStay(iso_collision);

            // Colisao com o balde vazio
            if (iso_collision.gameObject.name == "waterPrefab")
            {



                if (isPlayerWithBucket())
                {
                    GetComponent<IsoRigidbody>().velocity = Vector3.one;


                    if (canFillBucket)
                        StartCoroutine(FillBucketSlowly());

                }


            }


        }




            bool isPlayerWithBucket()
        {

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