using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Caapora;


[System.Serializable]
static class Coodenadas
{

	public enum North : long { x = 12, y = 0 };
	public enum South : long { x = 14, y = 24 };
	public enum East : long { x = 12, y = 12 }; // Provisorio
	public enum West : long { x = 0, y = 12 };

	
}

/// <summary>
///  Esta Classe implementa o padrão de projeto Singleton e tem como função gerenciar todos os estados do game
/// </summary>
[System.Serializable]
public class GameManager: MonoBehaviour {
	private static GameManager _instance;
	//used to store latest used door
	public Vector3 LastUsedDoorPosition;

    public PlayerBehavior player;

	// ID da ultimo caminho passado
	public int PathID;
    public Sprite enemy;
    public bool showIntroduction = false;
    public static string current_scene;
    public static string next_scene;
    private bool _paused = false;
    public static bool isAnimating = false;
    public static int zoom = 1;


    public static List<GameManager> savedGames = new List<GameManager>();
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

    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Apenas pega o Singleton do Player
    /// </summary>
    void Start()
    {


        // Habilita ou não a animação de introdução
        if (showIntroduction)
        {
            StartCoroutine(Introduction());
        }
    
            

        // Recebe a instancia do player
        player = PlayerBehavior.instance;

    }


    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Criar o Singleton do GameManager, garantindo apenas uma instancia
    /// </summary>
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
    /// Regras de negócio do Game 
    /// </summary>
    void Update()
    {


        Pause();

    

        if (WinCondition())
            YouWin();

        
        // Condições para o game over
        GameOVer();

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
        // || GameObject.Find("Monkey").GetComponent<Monkey>().life <= 0
        if (gameObject.GetComponent<IsoObject>().positionZ < -15 || player.life <= 0  )
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
        



       
	}


    /// <summary>
    /// Mostra um GameObject na tela por um periodo de tempo
    /// </summary>
    /// <param name="go"></param>
    /// <param name="seconds"></param>
    public static void ShowObjectAPeriodOfTime(GameObject go, int seconds)
    {

        instance.StartCoroutine(showAndHideObject(go, seconds));

    }



    /// <summary>
    /// Mostra um objeto após um periodo de tempo
    /// </summary>
    /// <param name="go"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static IEnumerator showAndHideObject(GameObject go, int seconds)
    {

        
        go.SetActive(true);
        yield return new WaitForSeconds(seconds);
        go.SetActive(false);
    }


    /// <summary>
    /// Mostra um objeto após um periodo de tempo
    /// </summary>
    /// <param name="go"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static IEnumerator hideAndShowObject(GameObject go, int seconds)
    {


        go.SetActive(false);
        yield return new WaitForSeconds(seconds);
        go.SetActive(true);
    }




    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Fecha o jogo
    /// </summary>
    public void Exit()
    {
       

        // Save();
        //Application.Quit();
        next_scene = "Map2";
        Application.LoadLevel("Loader");
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
                GameObject.Find("CameraAux").GetComponent<Camera>().transform.position += new Vector3(0, -3f, 0);

              yield return new WaitForSeconds(0.1f);

        }
         
    }


    public bool WinCondition()
    {

   
       
        return GameObject.Find("chamas") == null && GameObject.Find("chamasSemSpread") == null;

    }


    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Executa as rotinas da animação uma a umam todos os eventos tem que ser bem programados para os recursos poderem ser acessíveis no tempo certo
    /// </summary>
    public IEnumerator Introduction(){

        // Desabilita a camera principal para fazer a animação
        var mainCamera = GameObject.Find("Player/Camera").GetComponent<Camera>();

        mainCamera.enabled = false;

        if(!showIntroduction)
            Camera.main.enabled = false;


        // Exibe após quatro segundos
        StartCoroutine(hideAndShowObject(GameObject.Find("Informacoes"), 3));

        yield return new WaitForSeconds(3f);
        // Exibe informações da fase
        StartCoroutine(showAndHideObject(GameObject.Find("Informacoes"), 3));

        // Esconde o Inventory por 10 segundos
        StartCoroutine(hideAndShowObject(GameObject.Find("CanvasGUIContainer"), 7));



        StartCoroutine(moverCamera("down"));
        yield return new WaitForSeconds(10f);

        mainCamera.enabled = true;

        StartCoroutine (PlayerBehavior.AnimateCaapora ("right", 30));
		yield return new WaitForSeconds(3f);

        // vira o caipora para o sul
        player.GetComponent<Animator>().SetTrigger("CaaporaIdle");


		ConversationPanel.ActivePanel (true);
		StartCoroutine (CaaporaConversation.AnimateFrase());






    }


    public static void Zoom(int value)
    {

        zoom = value == 1 ? 2 : 1;
             

    }

    public void Pause()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {

            if (_paused)
            {
                Time.timeScale = 0;
                _paused = false;
            }
            else
            {
                Time.timeScale = 1;
                _paused = true;
            }
        }

    }


    /// Author :    Eric Daily
    /// <summary>
    /// Salva o estado do game
    /// </summary>
    //it's static so we can call it from anywhere
    public static void Save()
    {
        savedGames.Add(instance);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, savedGames);
        file.Close();
    }

    /// Author :    Eric Daily
    /// <summary>
    /// Carrega o estado do game
    /// </summary>
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            savedGames = (List<GameManager>)bf.Deserialize(file);
            file.Close();
        }
    }

    public void YouWin()
    {
        Destroy(gameObject);
        Application.LoadLevel("Winner");
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




}