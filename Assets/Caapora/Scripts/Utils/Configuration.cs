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
    public GameObject buttonJ;



        // Use this for initialization
        void Start () {

            // buttonUp = GameObject.Find("GUI/Controle/Up");

            AssignArrowButtonEvent(buttonUp, "up");
            AssignArrowButtonEvent(buttonRight, "right");
            AssignArrowButtonEvent(buttonDown, "down");
            AssignArrowButtonEvent(buttonLeft, "left");
    
            AssignActionButtonEvent(buttonA, "Catch");
            AssignActionButtonEvent(buttonB, "Launch");
            AssignActionButtonEvent(buttonJ, "Run");

            AssignOtherButtonEvent(buttonPause, "Exit");
            AssignOtherButtonEvent(buttonSkip, "Skip");

            AssignActionButtonEvent(buttonZ, "Zoom");


        }
	
	



        void AssignArrowButtonEvent(GameObject button, string direction)
        {


            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();


            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((eventData) => { Caapora.instance.moveDirection = direction; });
            trigger.triggers.Add(entry);


            ResetArrowOnPointerUp(button);
            
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
                        GameManager.instance.LoadNextLevel("MenuPrincipal");
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
                        InputController.instance.AClick = true;
                        break;
                    case "Launch":
                        InputController.instance.BClick = true;
                        break;
                    case "Zoom":
                        InputController.instance.ZClick = true;
                        break;
                    case "Run":
                        InputController.instance.JClick = true;
                        break;

                }

 

            });

            trigger.triggers.Add(entry);


            ResetOnPointerUp(button);

        }



        void ResetOnPointerUp(GameObject button)
        {



            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((eventData) =>
            {

                Caapora.instance.moveDirection = "";
                InputController.instance.BClick = false;
                InputController.instance.AClick = false;
                InputController.instance.ZClick = false;
                InputController.instance.JClick = false;
                Caapora.running = false;
                GameManager.instance.MapZoomIn();

            }

                );
            trigger.triggers.Add(entry);

        }



        void ResetArrowOnPointerUp(GameObject button)
        {



            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((eventData) =>
            {

               Caapora.instance.moveDirection = "";


            }

                );
            trigger.triggers.Add(entry);
            
        }

        
    }


}
