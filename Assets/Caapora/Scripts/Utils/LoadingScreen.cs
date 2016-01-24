using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Código encontrado em um vídeo no Youtube postado po unitycookie
/// </summary>
public class LoadingScreen : MonoBehaviour {

    public static GameObject background;
    public static GameObject text;
    public static GameObject progressBar;
    private static int loadProgress = 0;
    public static string levelToLoad = "Map1";
    public static LoadingScreen instance;

    AsyncOperation async;


    void Start () {

        instance = this;

        background = GameObject.Find("Tela/Background");
        text = GameObject.Find("Tela/Text");
        progressBar = GameObject.Find("Tela/Scrollbar");

        background.SetActive(false);
        text.SetActive(false);
        progressBar.SetActive(false);


        switch (Caapora.GameManager.next_scene)
        {
            case "Map1":
                LoadLevel("Map1");
                break;

            case "Map2":
                LoadLevel("Map2");
                break;
            case "TestMap":
                LoadLevel("AmbienteTestes2.5D");
                break;
            case "MenuPrincipal":
                LoadLevel("MenuPrincipal");
                break;

        }

    }

	void Update () {

        
        if (loadProgress >= 100)
        {
            
            background.SetActive(false);
            text.SetActive(false);
            progressBar.SetActive(false);      

        }
	}


    public static void LoadLevel(string levelToLoad)
    {
            instance.StartCoroutine(instance.DisplayLoadingScreen(levelToLoad));

     
       
    }

    IEnumerator DisplayLoadingScreen(string level)
    {
        background.SetActive(true);
        text.SetActive(true);
        progressBar.SetActive(true);
        
        text.GetComponent<Text>().text = "Loading Progress " + loadProgress + "%";


        async = Application.LoadLevelAsync(level);

        async.allowSceneActivation = false;

        
        while (!async.isDone)
        {
            if(async.progress > 0.89f)
            {
                async.allowSceneActivation = true;
                             
            }
                

           //  Debug.Log("async progress " + async.progress.ToString());

            loadProgress = (int)(async.progress * 100);

            text.GetComponent<Text>().text = "Loading Progress " + loadProgress + "%";
          
            progressBar.GetComponent<Scrollbar>().size = async.progress;


            yield return null; 
        }



    }
}
