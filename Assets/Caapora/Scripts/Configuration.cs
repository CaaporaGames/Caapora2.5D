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



        // Use this for initialization
        void Start () {

            // buttonUp = GameObject.Find("GUI/Controle/Up");

            AssignArrowButtonEvent(buttonUp, "up");
            AssignArrowButtonEvent(buttonRight, "right");
            AssignArrowButtonEvent(buttonDown, "down");
            AssignArrowButtonEvent(buttonLeft, "left");
    
            AssignActionButtonEvent(buttonA, "Atack");
            AssignActionButtonEvent(buttonB, "Jump");

            AssignOtherButtonEvent(buttonPause, "Exit");
            AssignOtherButtonEvent(buttonSkip, "Skip");


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


            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => {

                

                switch (action)
                {
                    case "Atack":
                        KeyboardController.instance.AClick = true;
                        Debug.Log("Ativou a A action = " + action);
                        break;
                    case "Jump":
                        KeyboardController.instance.BClick = true;
                        break;

                }
              




            });
            trigger.triggers.Add(entry);


        }



        void ResetOnPointerUp(GameObject button)
        {



            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((eventData) => { KeyboardController.instance.moveDirection = ""; });
            trigger.triggers.Add(entry);



        }




}


}
