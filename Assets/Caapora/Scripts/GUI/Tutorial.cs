using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadNextLevel()
    {

        Caapora.GameManager.next_scene = "Map1";
        Application.LoadLevel("Loader");

    }

}
