
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





        // Rômulo Lima  
        // Use this for initialization
        protected void Start()
        {

            // Herda da classe base
            base.Start();


            instance = this;

            currentLevel = StatsController.GetCurrentLevel();

          

        }


        // Rômulo Lima
        void Awake(){
		    
            // Acessar recursos de metodos estaticos
			instance = this;
		
	}


        void Update() {

          //  base.Update();

            if (!GameManager.isAnimating)
                GameObject.Find("Status/hp").GetComponent<Text>().text = _life.ToString();



                 GameObject balde = GameObject.Find("baldeVazioPrefab");



                if (!Inventory.isEmpty())
                {

                    if (Inventory.getItem().GetComponent<Balde>().waterPercent <= 0.0f)
                        Debug.Log("Ande proximo ao lago");
                    //     AdviceSimple.showAdvice("Ande próximo ao lago para encher o balde com água!");
                }


            if (canCatch(balde))
            {

                // Checa por entrada de dados
                if (Input.GetKeyDown(KeyCode.A) || KeyboardController.instance.AClick)
                {
                    // Exibe a dica de tela
                    Advice.ShowAdvice(true);

                    AddItemToInventory(balde);

                    // Migra a animação para a do balde
                    animator.SetTrigger("Catch");

                    balde.SetActive(false);



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

            Debug.Log("enchendo de agua");
            animator.SetTrigger("Catch");

            // Incrementa a porcentagem de agua em um em um
            balde.FillBucket();


            yield return new WaitForSeconds(0.1f);

            canFillBucket = true;



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
           // DebugGame.instance.debug_message = "Colidiu com a " + iso_collision.gameObject.name;

            // Colisao com o balde vazio
            if (iso_collision.gameObject.name == "waterPrefab")
            {
                Debug.Log("Colidiu com a agua");
                

                if (isPlayerWithBucket())
                {
                    Debug.Log("Player com balde proximo de agua");

                 
                    if (canFillBucket)
                        StartCoroutine(FillBucketSlowly());

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