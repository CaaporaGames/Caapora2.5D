using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;
using UnityEngine.EventSystems;



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
    //public Animator enemy;

    // Construtor implementando Singleton
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
        StartCoroutine (Introduction());

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



    void CatchItem(GameObject item)
    {



    }



    void AddItemToInventory(GameObject item)
    {

        item.GetComponent<Image>().sprite = Resources.Load("Sprites/balde", typeof(Sprite)) as Sprite;

    }


    bool canCatch(GameObject go)
    {
        // Percorre todos as posicoes vizinhas
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++) {

                  
                
                if (Mathf.RoundToInt(go.GetComponent<IsoObject>().positionX) == Mathf.RoundToInt(player.GetComponent<IsoObject>().positionX + x)  &&
                        Mathf.RoundToInt(go.GetComponent<IsoObject>().positionY) == Mathf.RoundToInt(player.GetComponent<IsoObject>().positionY + y))
                {

                    Debug.Log("É vizinho!");
                    return true;
                }
                   

             }
        }


        return false;


    }

    void Update()
        {

        GameObject baldeTeste = GameObject.Find("baldeVazioPrefab"); ;


        if (canCatch(baldeTeste))
        {



            // Checa por entrada de dados
            if (Input.GetKeyDown(KeyCode.A) || player.AClick)
            {
                // Exibe a dica de tecla
                Advice.ShowAdvice(false);

                AddItemToInventory(GameObject.Find("item1"));

                Destroy(baldeTeste);

            }

        }
        
            
       



        // Atualiza o valor do status na interface
        GameObject.Find("GUI/Inventory/CharStats/hp").GetComponent<Text>().text = player.life.ToString(); ;


            // Condições para o game over
            GameOVer();

        }
         

    // Facilita a transferencia de um mapa para outro
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



    // Rômulo Lima
    void GameOVer()
    {

        if (gameObject.GetComponent<IsoObject>().positionZ < -15 || player.life <= 0)
        {

            Destroy(gameObject);
            Application.LoadLevel("GameOver");

        }

    }



    void OnIsoCollisionEnter(IsoCollision iso_collision) {

        var GateName = iso_collision.gameObject.name;

    
        if(GateName == "GateEast")
             movePlayer("Map1", "Map2", GateName);
        


       // movePlayer(source, destination, GateName);

        // Colisao com o balde vazio
        if (iso_collision.gameObject.name == "baldeVazioPrefab") {

            Debug.Log("Colidindo com o balde");
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

   


    // Rômulo Lima
    // Sair do Jogo
    public void Exit()
    {
        Debug.Log("Apertou sair");
        Application.Quit();
    }

    // Romulo Lima
    // Anima o Sprite na colisao, fica piscando em vermelho
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


    // Executa a Introduçao passo a passo
    public IEnumerator Introduction(){


	
		StartCoroutine (Completed.PlayerBehavior.AnimateCaapora ("right", 30));
		yield return new WaitForSeconds(3f);

		StartCoroutine (Completed.PlayerBehavior.ShakePlayer());
		yield return new WaitForSeconds(1f);


		ConversationPanel.ActivePanel (true);
		StartCoroutine (CaaporaConversation.AnimateFrase());
	



	}

    // Rômulo Lima
    // desabilita a tela de conversassão
    public void hideConversationPanel()
    {

        GameObject.Find("Tela de Conversa").SetActive(false);
    }


	void OnGUI(){
		
		
		GUI.Label(new Rect(0,0,300,50), "Player Position X :" + gameObject.GetComponent<IsoObject>().positionX);
		GUI.Label(new Rect(0,50,300,50), "Player Position Y :" + gameObject.GetComponent<IsoObject>().positionY);
		GUI.Label(new Rect(0,100,300,50), "Player Position Z :" + gameObject.GetComponent<IsoObject>().positionZ);

		GUI.Label(new Rect(0,150,300,50), "Mapa Atual :" + PlayerPrefs.GetString("CurrentMap"));

		
		
	}


}