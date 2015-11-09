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

	/// <summary>
    /// Controla a transição entre cenas com carregamento
    /// </summary>
	void Start () {

        instance = this;

        background = GameObject.Find("Tela/Background");
        text = GameObject.Find("Tela/Text");
        progressBar = GameObject.Find("Tela/Scrollbar");

        background.SetActive(false);
        text.SetActive(false);
        progressBar.SetActive(false);


        switch (GameManager.next_scene)
        {
            case "Map1":
                LoadLevel("Map1");
                break;

            case "Map2":
                LoadLevel("Map2");
                break;

        }

    }
	
	// Update is called once per frame
	void Update () {

        
        if (loadProgress > 80)
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


        AsyncOperation async = Application.LoadLevelAdditiveAsync(level);
        while (!async.isDone)
        {
            Debug.Log("LodingProgress = " + async.progress);
            loadProgress = (int)(async.progress * 100);
            text.GetComponent<Text>().text = "Loading Progress " + loadProgress + "%";
          
            progressBar.GetComponent<Scrollbar>().size = async.progress;


            yield return null; 
        }

    }
}
