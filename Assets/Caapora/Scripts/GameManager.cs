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


[System.Serializable]
public class GameManager: MonoBehaviour {

	private static GameManager _instance;
    private float Timeleft = 120;

	public Vector3 LastUsedDoorPosition;

    private Caapora.Caapora player;


	public int PathID;
    public Sprite enemy;
    public bool showIntroduction = false;
    public static string current_scene;
    public static string next_scene;
    private bool _paused = false;
    public static bool isAnimating = false;
    private GameObject winnerModal;
    private GameObject loserModal;
    private bool gameover = false;
    private int _zoomState = 1;
    private int _totalOfFrame = -1;
    private Text TotalChamas;
    private Text Timer;



    public static List<GameManager> savedGames = new List<GameManager>();


    void Awake()
    {
        if (_instance == null)
        {
           
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {

            if (this != _instance)
                Destroy(this.gameObject);
        }
    }



    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
             
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }


    void Start()
    {
        PopulatePool();

        winnerModal = GameObject.Find("Winner");
        winnerModal.SetActive(false);

        loserModal = GameObject.Find("GameOver");
        loserModal.SetActive(false);

        // Habilita ou não a animação de introdução
        if (showIntroduction)
        {
            StartCoroutine(Introduction());
        }

        TotalChamas = GameObject.Find("TotalChamas").GetComponent<Text>();

        Timer = GameObject.Find("Tempo").GetComponent<Text>();

        // Recebe a instancia do player
        player = Caapora.Caapora.instance;

    }



  

    void Update()
    {

        Timeleft -= Time.deltaTime;

        TotalChamas.text = "Chamas: " + totalOfFrame;

        Timer.text = "Tempo : " + Mathf.Round(Timeleft);

        // Condições para o game over
        if (!gameover)
        {
            if (WinCondition())
                YouWin();

             GameOVer();
        }




        if (!InputController.isPlayingAnimation)
        { // Caso nao esteja precionando nenhuma tecla


            /* Inicialmente apenas verifica se há itens*/
            if (!Inventory.isEmpty())
                Caapora.Caapora.instance.animator.SetTrigger("bucket");


        }

    }



    void PopulatePool()
    {

      
        int total;

        for(total = 0; total < 30; total++)
        {
             
            var frame = Instantiate(Resources.Load("Prefabs/chamasSemSpread")) as GameObject;
            frame.name = "chamasSemSpread";
            GetComponent<ObjectPool>().PoolObject(frame);

        }
           



    }




   
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



    void GameOVer()
    {

        if (gameObject.GetComponent<IsoObject>().positionZ < -15 || player.life <= 0  || Timeleft <= 0)
        {


            gameover = true;
            Pause();
            loserModal.SetActive(true);

        }

    }


    void OnIsoCollisionEnter(IsoCollision iso_collision) {

        var GateName = iso_collision.gameObject.name;

    
        if(GateName == "GateEast")
             movePlayer("Map1", "Map2", GateName);
        



       
	}


    public static void ShowObjectAPeriodOfTime(GameObject go, int seconds)
    {
        
        instance.StartCoroutine(showAndHideObject(go, seconds));

    }



    public static IEnumerator showAndHideObject(GameObject go, int seconds)
    {

        
        go.SetActive(true);
        yield return new WaitForSeconds(seconds);
        go.SetActive(false);
        yield return new WaitForSeconds(seconds);
    }


    public static IEnumerator hideAndShowObject(GameObject go, int seconds)
    {


        go.SetActive(false);
        yield return new WaitForSeconds(seconds);
        go.SetActive(true);
    }




    public void Exit()
    {
       

        // Save();
        Application.Quit();
        
    }


    public void LoadNextLevel(string level)
    {

        GameManager.next_scene = level;
        Application.LoadLevel("Loader");

    }


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


    public static int totalOfFrame
    {

        set
        {
            instance._totalOfFrame = value;
        }

        get
        {
            return instance._totalOfFrame;
        }

    }


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

       // StartCoroutine (PlayerBehavior.AnimateCaapora ("right", 5));
	   //	yield return new WaitForSeconds(3f);

       
        player.GetComponent<Animator>().SetTrigger("CaaporaIdle");


		ConversationPanel.ActivePanel (true);
		StartCoroutine (CaaporaConversation.AnimateFrase());






    }


    public void showAllMap()
    {


        _zoomState = 2;


    }




    // Métodos de Flags para ativar a movimentação
    public int zoomState
    {
        get
        {
            return _zoomState;
        }

        set
        {
            _zoomState = value;
        }
    }

    public void Pause()
    {

        if (_paused)
        {
            Time.timeScale = 1;
            _paused = false;
        }
        else
        {
            Time.timeScale = 0;
            _paused = true;
        }

    }



    public static void Save()
    {
        savedGames.Add(instance);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, savedGames);
        file.Close();
    }


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

        gameover = true;
        Pause();
        winnerModal.SetActive(true);

        
    }


    public void hideConversationPanel()
    {

        GameObject.Find("Tela de Conversa").SetActive(false);
    }




}