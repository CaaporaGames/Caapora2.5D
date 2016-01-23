using UnityEngine;
using System.Collections;
using IsoTools;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Caapora;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
static class Coodenadas
{

	public enum North : long { x = 12, y = 0 };
	public enum South : long { x = 14, y = 24 };
	public enum East : long { x = 12, y = 12 }; // Provisorio
	public enum West : long { x = 0, y = 12 };

	
}

namespace Caapora { 
[System.Serializable]
public class GameManager: MonoBehaviour {

	private static GameManager _instance;
    public static float CurrentTimeLeft { get; private set; }
    public float TimeLeft = 120;
    private GameObject CameraAux;
	public Vector3 LastUsedDoorPosition;
    private GameObject SceneInformation;
    private Caapora player;


	public int PathID;
    public Sprite enemy;
    public bool showIntroduction = false;
    public static string current_scene;
    public static string next_scene;
    private bool _paused;
    public static bool isAnimating = false;
    private bool gameover;
    private int _zoomState = 1;
    private int _totalOfFlames = 0;



    public static List<GameManager> savedGames = new List<GameManager>();


    void Awake()
    {
            
            PopulatePool();

            instance.PrepareGame();

            CameraAux = GameObject.Find("CameraAux");
            SceneInformation = GameObject.Find("Informacoes");

            if (!showIntroduction)
            {
                CameraAux.SetActive(false);
                SceneInformation.SetActive(false);
            }

            

            if (showIntroduction)
            {
                StartCoroutine(Introduction());
            }


    
            player = Caapora.instance;

            instance.UnPause();

            SoundManager.instance.musicSource.Play();

        }

        public void PrepareGame()
        {
            CurrentTimeLeft = TimeLeft;

            _instance.gameover = false;

            UIInterface.instance.Show();
            

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
     

            if (_instance == null)
            {

                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {

                if (this != _instance)
                    Destroy(this.gameObject);
            }

        }



  

    void Update()
    {
        
        
        CurrentTimeLeft -= Time.deltaTime;


            Debug.Log("gameonver : pause = " + gameover + " : " + instance._paused);
             
 

            if (!gameover)
            {
                if (WinCondition())
                    YouWin();

                if (LoseCondition())
                    GameOVer();
            }


        if (!InputController.isPlayingAnimation)
        { 

            if (!Inventory.isEmpty())
                Caapora.instance.animator.SetTrigger("bucket");


        }

        if (!showIntroduction)
        {
            if (_zoomState == 1)
                Camera.main.orthographicSize = 80;
            else
                Camera.main.orthographicSize = 250;
        }

    }


    public bool WinCondition()
    {

        return GameObject.FindWithTag("Flame") == null && CurrentTimeLeft > 0;

    }

    public void YouWin()
    {

            Debug.Log("Voce venceu");
            gameover = true;
            StopGame();
            UIInterface.instance.winnerModal.SetActive(true);

    }


        private bool LoseCondition()
    {

        return gameObject.GetComponent<IsoObject>().positionZ < -15 || player.life <= 0 || CurrentTimeLeft <= 0;
    }

    public void GameOVer()
    {
            Debug.Log("Voce perdeu");
            gameover = true;
            StopGame();
            UIInterface.instance.loserModal.SetActive(true);


    }

    public void Pause()
    {

        if (_paused)
        {
            Time.timeScale = 1;
            _paused = false;
             UIInterface.instance.pauseModal.SetActive(false);
                SoundManager.instance.musicSource.UnPause();

        }
        else
        {
            Time.timeScale = 0;
            _paused = true;
            UIInterface.instance.pauseModal.SetActive(true);
                SoundManager.instance.musicSource.Pause(); ;

            }

        }

         private void StopGame()
        {
            Time.timeScale = 0;
            _paused = true;
            SoundManager.instance.musicSource.Stop();

        }

    public void UnPause()
        {
            Time.timeScale = 1;
            _paused = false;

        }

    public void Exit()
    {


        Application.Quit();

    }


    public void LoadNextLevel(string scene)
    {
     
       
       UIInterface.instance.Hide();

       CurrentTimeLeft = TimeLeft;

        LevelController.AddLevel();

        next_scene = scene;
        SceneManager.LoadScene("Loader");

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


        SceneManager.LoadScene(destination, LoadSceneMode.Additive);

        gameObject.GetComponent<IsoObject>().position = new Vector3(x , y , 1);
		Destroy(GameObject.Find(source));
		PlayerPrefs.SetString("CurrentMap",destination);
		
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

   


    public IEnumerator CaaporaHit()
    {

        float t = 0.0f;

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
                CameraAux.transform.position += new Vector3(0, -3f, 0);

              yield return new WaitForSeconds(0.1f);

        }
         
    }


    public static int totalOfFlames
    {

        set
        {
            instance._totalOfFlames = value;
        }

        get
        {
            return instance._totalOfFlames;
        }

    }


    public IEnumerator Introduction(){

        var MainCamera = Camera.main;

        if(showIntroduction)
            Camera.main.enabled = false;


        StartCoroutine(hideAndShowObject(GameObject.Find("Informacoes"), 3));

        yield return new WaitForSeconds(3f);

        StartCoroutine(showAndHideObject(GameObject.Find("Informacoes"), 3));


        StartCoroutine(hideAndShowObject(GameObject.Find("CanvasGUIContainer"), 7));



        StartCoroutine(moverCamera("down"));
        yield return new WaitForSeconds(10f);

        MainCamera.enabled = true;

       
        player.GetComponent<Animator>().SetTrigger("CaaporaIdle");


		ConversationPanel.ActivePanel (true);
		StartCoroutine (CaaporaConversation.AnimateFrase());






    }


    public void MapZoomOut()
    {


       _zoomState = 2;


    }


    public void MapZoomIn()
    {

        _zoomState = 1;
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




    public void hideConversationPanel()
    {

        GameObject.Find("Tela de Conversa").SetActive(false);
    }



    }
}