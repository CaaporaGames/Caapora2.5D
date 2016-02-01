using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public int LevelMultiply = 1;
    public int FirstLevel = 1;
    public float DifficultFactor = 1.5f;
    private Text levelText;


    void Start () {


        instance = this;
        levelText = GameObject.Find("Level").GetComponent<Text>();

    }

    void OnLevelWasLoaded()
    {
        if(GameObject.Find("Level") != null)
          levelText = GameObject.Find("Level").GetComponent<Text>();

    }

    void Update () {
        if(levelText != null)
         levelText.text = "Level: " + GetCurrentLevel().ToString();

    }


    public static void AddLevel()
    {
        int newLevel = GetCurrentLevel() + 1;
        PlayerPrefs.SetInt("currentGameLevel", newLevel);
    }




    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("currentGameLevel");
    }
}
