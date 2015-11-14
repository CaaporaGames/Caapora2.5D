using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


namespace Caapora
{


public class Configuration : MonoBehaviour {

    public bool mute = false;
    public int difficult;
  


    public GameObject buttonLeft;
    public GameObject buttonRight;
    public GameObject buttonUp;
    public GameObject buttonDown;
    public GameObject buttonA;
    public GameObject buttonB;
    public GameObject buttonPause;
    public GameObject buttonSkip;
    public GameObject buttonZ;



        // Use this for initialization
        void Start () {

            // buttonUp = GameObject.Find("GUI/Controle/Up");

            AssignArrowButtonEvent(buttonUp, "up");
            AssignArrowButtonEvent(buttonRight, "right");
            AssignArrowButtonEvent(buttonDown, "down");
            AssignArrowButtonEvent(buttonLeft, "left");
    
            AssignActionButtonEvent(buttonA, "Catch");
            AssignActionButtonEvent(buttonB, "Launch");

            AssignOtherButtonEvent(buttonPause, "Exit");
            AssignOtherButtonEvent(buttonSkip, "Skip");

            AssignActionButtonEvent(buttonZ, "Zoom");


        }
	
	// Update is called once per frame
	void Update () {
	
	}






        void AssignArrowButtonEvent(GameObject button, string direction)
        {


            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();


            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((eventData) => { KeyboardController.instance.moveDirection = direction; });
            trigger.triggers.Add(entry);


            ResetOnPointerUp(button);


        }



        void AssignOtherButtonEvent(GameObject button, string type)
        {


            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();


            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => {

                switch (type)
                {
                    case "Exit":
                        Application.Quit();
                        break;
                    case "Skip":
                        GameManager.instance.hideConversationPanel();
                        break;
                   

                }

               




            });



            trigger.triggers.Add(entry);


        }


        void AssignActionButtonEvent(GameObject button, string action)
        {


            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();


            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((eventData) => {

                

                switch (action)
                {
                    case "Catch":
                        KeyboardController.instance.AClick = true;
                        break;
                    case "Launch":
                        KeyboardController.instance.BClick = true;
                        break;
                    case "Zoom":
                        Debug.Log("zoooom");
                        KeyboardController.instance.ZClick = true;
                        break;

                }


                ResetOnPointerUp(button);


            });
            trigger.triggers.Add(entry);


        }



        void ResetOnPointerUp(GameObject button)
        {



            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((eventData) =>
            {

                KeyboardController.instance.moveDirection = "";
                KeyboardController.instance.BClick = false;
                KeyboardController.instance.AClick = false;
                KeyboardController.instance.ZClick = false;
            }

                );
            trigger.triggers.Add(entry);



        }




}


}
