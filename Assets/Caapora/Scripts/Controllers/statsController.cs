using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Caapora{

public class StatsController : MonoBehaviour {

	public static StatsController instance;

	public int xpMultiply = 1;
	public float xpFirstLevel = 100;
	public float difficultFactor = 1.5f;
        private Scrollbar manaBar;
        private Text levelText;
        private Text xpText;


	void Start () {
		instance = this;
		

          
            levelText = GameObject.Find("CaaporaStatus/level").GetComponent<Text>();
            xpText = GameObject.Find("CaaporaStatus/xp").GetComponent<Text>();

        }



        void OnLevelWasLoaded()
        {

            if(GameObject.Find("CaaporaStatus/level") != null)
            {
                levelText = GameObject.Find("CaaporaStatus/level").GetComponent<Text>();
                xpText = GameObject.Find("CaaporaStatus/xp").GetComponent<Text>();

            }
             


        }

        void Update () {


            levelText.text = "Lv. " + GetCurrentLevel().ToString();

            xpText.text = "xp. " + GetCurrentXp().ToString();


        }

	public static void AddXp (float xpAdd) {
		float newXp = (GetCurrentXp() + xpAdd)* StatsController.instance.xpMultiply;
		while(newXp >= GetNextXp ()) {
			newXp -= GetNextXp();
			AddLevel();

		}

		PlayerPrefs.SetFloat("currentXp", newXp);
	}

	public static float GetCurrentXp() {
		return PlayerPrefs.GetFloat("currentXp");
	}

	public static int GetCurrentLevel() {
		return PlayerPrefs.GetInt("currentLevel");
	}

	public static void AddLevel() {
		int newLevel = GetCurrentLevel() + 1;
		PlayerPrefs.SetInt("currentLevel", newLevel);
	}

	public static float GetNextXp() {
		return StatsController.instance.xpFirstLevel * (GetCurrentLevel() + 1) * StatsController.instance.difficultFactor;
	}
	
}
}