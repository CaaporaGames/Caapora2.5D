﻿using UnityEngine;
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

        GameManager.next_scene = "Map1";
        Application.LoadLevel("Loader");

    }

}
