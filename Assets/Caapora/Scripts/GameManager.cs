using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;



static class Coodenadas
{

	public enum North : long { x = 12, y = 0 };
	public enum South : long { x = 14, y = 24 };
	public enum East : long { x = 12, y = 12 }; // Provisorio
	public enum West : long { x = 0, y = 12 };

	
}


// ######## Esta Classe implementa o padrão de projeto Singleton e tem como função gerenciar todos os estados do game
public class GameManager: MonoBehaviour {
	private static GameManager _instance;
	//used to store latest used door
	public Vector3 LastUsedDoorPosition;

    public Completed.PlayerBehavior player;

	// ID da ultimo caminho passado
	public int PathID;
	public Sprite baldeCheio;
    public Sprite enemy;
    public bool canFillBucket;

    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Contrutor que implementa o padrão de projeto Singleton
    /// </summary>
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Start()
    {
      
        // Habilita ou não a animação de introdução
        //StartCoroutine (Introduction());

        // Recebe a instancia do player
        player = Completed.PlayerBehavior.instance;

    }



    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }


    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    ///   
    /// <param name="go">objeto alvo</param> 
    /// <returns></returns>
    /// 
    /// </summary>
    void CatchItem(GameObject item)
    {



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
            for (int y = -1; y <= 1; y++) {

                  
               if(go != null) 
                if (Mathf.RoundToInt(go.GetComponent<IsoObject>().positionX) == Mathf.RoundToInt(player.GetComponent<IsoObject>().positionX + x)  &&
                        Mathf.RoundToInt(go.GetComponent<IsoObject>().positionY) == Mathf.RoundToInt(player.GetComponent<IsoObject>().positionY + y))
                {

                    return true;
                }
                   

             }
        }


        return false;


    }


   
    void Update()
        {

        GameObject water = GameObject.Find("waterPrefab");
        GameObject baldeTeste = GameObject.Find("baldeVazioPrefab");

        if (canCatch(water))
        {
            Debug.Log("Pode pegar água!");

            // Se tiver algo no inventory 
            // o canFill serve como flag para garantir que encha de um em um
            if (!Inventory.isEmpty() && !canFillBucket)
            {
                canFillBucket = true;
                FillBucket();
                
            }
               
                    
        }

        if (canCatch(baldeTeste))
        {
   
            // Checa por entrada de dados
            if (Input.GetKeyDown(KeyCode.A) || player.AClick)
            {
                // Exibe a dica de tecla
                Advice.ShowAdvice(false);

                AddItemToInventory(baldeTeste);

                // Migra a animação para a do balde
                player.animator.SetTrigger("CaaporaParaBalde-idle");

                baldeTeste.SetActive(false);

                

            }

        }
        
            
       



        // Atualiza o valor do status na interface
        GameObject.Find("hp").GetComponent<Text>().text = player.life.ToString(); ;


            // Condições para o game over
            GameOVer();

        }


    public void FillBucket()
    {
        
        StartCoroutine(FillBucketSlowly());
        //Garante a chamada apenas uma vez
        canFillBucket = false;

    }

    public IEnumerator FillBucketSlowly()
    {
        var balde = Inventory.getItem().GetComponent<Balde>();

            // Incrementa a porcentagem de agua em um em um
            balde.waterPercent++;

            GameObject.Find("Inventory/item1/Text").GetComponent<Text>().text = balde.waterPercent + "/100";


            yield return new WaitForSeconds(1);
     

    }

    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Transfere o Player de uma posição a outra baseada no mapeamento na classe Coordenadas
    /// </summary>
    /// <see cref="Coodenadas"/>
    /// <param name="source">Nome do GameObject raiz de exibição do mapa</param>
    /// <param name="destination">Nome do GameObject raiz do mapa de destino</param>
    /// <param name="portal">Nome do portal que foi feita a colisão</param>
    void movePlayer(string source, string destination, string portal ){

		float x = 0f, y = 0f;

		switch (portal) {
			case "GateEast":
				x = (float)Coodenadas.West.x;
				y = (float)Coodenadas.West.y;
				break;
			case "GateWest":
				x = (float)Coodenadas.East.x;
				y = (float)Coodenadas.East.y;
				break;
			case "GateNorth":
				x = (float)Coodenadas.South.x;
				y = (float)Coodenadas.South.y;
				break;
			case "GateSouth":
				x = (float)Coodenadas.North.x;
				y = (float)Coodenadas.North.y;
				break;
		}

		Application.LoadLevelAdditive(destination);
		gameObject.GetComponent<IsoObject>().position = new Vector3(x , y , 1);
		Destroy(GameObject.Find(source));
		PlayerPrefs.SetString("CurrentMap",destination);
		
	}



    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Possui Todas as condição para dar GameOver 
    /// </summary>
    void GameOVer()
    {

        if (gameObject.GetComponent<IsoObject>().positionZ < -15 || player.life <= 0)
        {

            Destroy(gameObject);
            Application.LoadLevel("GameOver");

        }

    }


    /// *************************************************************************
    /// Author: 
    /// <summary> 
    /// Sobrecarregou o método padrão do Unity OnCollisionEnter
    /// </summary>
    /// <param name="iso_collision">A referencia do objeto colidido</param>
    void OnIsoCollisionEnter(IsoCollision iso_collision) {

        var GateName = iso_collision.gameObject.name;

    
        if(GateName == "GateEast")
             movePlayer("Map1", "Map2", GateName);
        


       // movePlayer(source, destination, GateName);

        // Colisao com o balde vazio
        if (iso_collision.gameObject.name == "baldeVazioPrefab") {

            // Exibe a dica de tecla
            Advice.ShowAdvice(true);
 

		}


     

        if ( iso_collision.gameObject.name == "chamas"  || iso_collision.gameObject.name == "chamas(Clone)") {

            // Reduz o life do caipora de acordo com o demage do objeto
            player.life = player.life - iso_collision.gameObject.GetComponent<spreadFrame>().demage;

            StartCoroutine(CaaporaHit());



			var objeto = iso_collision.gameObject.GetComponent<IsoRigidbody>();
			if ( objeto ) {
				
				Destroy(objeto.gameObject);
				
				//	objeto.transform.parent = transform;
			}
		}


       

		if ( iso_collision.gameObject.name == "waterPrefab" ) {

			// load all frames in fruitsSprites array
			baldeCheio = Resources.Load("Sprites/balde", typeof(Sprite)) as Sprite;



			//enemy = Resources.Load("Sprites/Enemy", typeof(Sprite)) as Sprite;

			// substitui sprite
			//transform.FindChild("baldevazio").GetComponent<SpriteRenderer>().sprite = baldeCheio;
			Debug.Log("Colidiu com a agua");

		//	gameObject.GetComponent<SpriteRenderer>().sprite = enemy;


		
		}
	}




    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Fecha o jogo
    /// </summary>
    public void Exit()
    {
        Debug.Log("Apertou sair");
        Application.Quit();
    }

    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Animação que deixa o sprite vermelho por um periodo de tempo
    /// </summary>o
    public IEnumerator CaaporaHit()
    {

        float t = 0.0f;

        // Forma gradativa de fazer transição
        while (t < 1f)
            {
                t += Time.deltaTime;

                GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, t);
                yield return null;
          

        }

             



    }

    /// <summary>
    /// Animação com a movimentação da camera em alguma direção
    /// </summary>
    /// <param name="direcao"></param>
    /// <returns></returns>
    public IEnumerator moverCamera(string direcao)
    {

        float t = 0.0f;

        for(int i =0; i< 70; i++)
        {

            t += Time.deltaTime;
            if (direcao == "down")
                Camera.main.transform.position += new Vector3(0, -3f, 0);

              yield return new WaitForSeconds(0.1f);

        }
         
    }


    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Executa as rotinas da animação uma a uma
    /// </summary>
    public IEnumerator Introduction(){

        // Desabilita a camera principal para fazer a animação
        var mainCamera = GameObject.Find("Player/Camera").GetComponent<Camera>();
        mainCamera.enabled = false;

        StartCoroutine(moverCamera("down"));
        yield return new WaitForSeconds(10f);

        mainCamera.enabled = true;

        StartCoroutine (Completed.PlayerBehavior.AnimateCaapora ("right", 30));
		yield return new WaitForSeconds(3f);

		StartCoroutine (Completed.PlayerBehavior.ShakePlayer());
		yield return new WaitForSeconds(1f);


		ConversationPanel.ActivePanel (true);
		StartCoroutine (CaaporaConversation.AnimateFrase());
	



	}

    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Desabilita painel de conversa
    /// </summary>
    public void hideConversationPanel()
    {

        GameObject.Find("Tela de Conversa").SetActive(false);
    }

    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Exibe área de debug na tela
    /// </summary>
	void OnGUI(){
		
		
		GUI.Label(new Rect(0,0,300,50), "Player Position X :" + gameObject.GetComponent<IsoObject>().positionX);
		GUI.Label(new Rect(0,50,300,50), "Player Position Y :" + gameObject.GetComponent<IsoObject>().positionY);
		GUI.Label(new Rect(0,100,300,50), "Player Position Z :" + gameObject.GetComponent<IsoObject>().positionZ);

		GUI.Label(new Rect(0,150,300,50), "Mapa Atual :" + PlayerPrefs.GetString("CurrentMap"));

		
		
	}


}