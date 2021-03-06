﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Caapora
{


public class DayNight : MonoBehaviour
{

    private int dayLength;   //in minutes
    private int dayStart;
    private int nightStart;   //also in minutes
    private int currentTime;
    private int nightStage1;
    public float cycleSpeed;
    private bool isDay;
    private Vector3 sunPosition;
    public Light sun;
    public GameObject earth;
   
 

    // Day and Night Script for 2d,
    // Unity needs one empty GameObject (earth) and one Light (sun)
    // make the sun a child of the earth
    // reset the earth position to 0,0,0 and move the sun to -200,0,0
    // attach script to sun
    // add sun and earth to script publics
    // set sun to directional light and y angle to 90



    void Start()
    {
        dayLength = 1440;
        dayStart = 300;
        nightStage1 = 1000 ;
        nightStart = 1200;
        currentTime = 1100;


        StartCoroutine(TimeOfDay());
        earth = gameObject.transform.parent.gameObject;
    }

    void Update()
    {

        if (currentTime > 0 && currentTime < dayStart)
        {
            isDay = false;
            sun.intensity = 0.3f;

           
            }
        else if (currentTime >= nightStage1 && currentTime < nightStart)
        {
            isDay = true;
            sun.intensity = 0.5f;

                if(BeforeNight())
                  SoundManager.instance.PlaySingle(SoundManager.instance.wolf);

         }

        else if (currentTime >= dayStart && currentTime < nightStart)
        {
            isDay = true;
            sun.intensity = 1f;
       

            }
            else if (currentTime >= nightStart && currentTime < dayLength)
        {
            isDay = false;
            sun.intensity = 0;
        }
        else if (currentTime >= dayLength)
        {
            currentTime = 0;
        }
        float currentTimeF = currentTime;
        float dayLengthF = dayLength;
        earth.transform.eulerAngles = new Vector3(0, 0, (-(currentTimeF / dayLengthF) * 360) + 90);
    }

    private bool BeforeNight()
        {

            return currentTime == nightStart - 60;
        }

    IEnumerator TimeOfDay()
    {
        while (true)
        {
            currentTime += 1;
            int hours = Mathf.RoundToInt(currentTime / 60);

            int minutes = currentTime % 60;

            UIInterface.instance.timeGUI.text = String.Format("Hora: {0:00}:{1:00}", hours, minutes);
            yield return new WaitForSeconds(1F / cycleSpeed);
        }
    }
}

}