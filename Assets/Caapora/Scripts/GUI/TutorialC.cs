using UnityEngine;
using System.Collections;

public class TutorialC : MonoBehaviour
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

        GameManager.instance.LoadNextLevel("Map1");

    }

}
