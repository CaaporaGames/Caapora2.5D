using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;// Required when using Event data.

public class controleUp : MonoBehaviour , IPointerDownHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData) 
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }
}
