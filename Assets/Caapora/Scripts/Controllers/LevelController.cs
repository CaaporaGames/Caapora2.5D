using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public int LevelMultiply = 1;
    public int FirstLevel = 1;
    public float DifficultFactor = 1.5f;

    // Use this for initialization
    void Start () {

        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        GameObject.Find("Level").GetComponent<Text>().text = "Level: " + GetCurrentLevel().ToString();

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
