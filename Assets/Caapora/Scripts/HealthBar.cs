using UnityEngine;
using System.Collections;
using IsoTools;

public class HealthBar : MonoBehaviour {
	
	public float barDisplay; //current progress
	public Vector3 pos = new Vector3(20,40);
	public Vector3 size = new Vector3(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	
	void OnGUI() {

		pos = GetScreenPosition (gameObject.GetComponent<IsoObject>().transform,
		                         gameObject.GetComponent<Canvas>());
		//size = transform.position;

		Debug.Log ("GUI POSX : " + pos.x);
		Debug.Log ("GUI POSY : " + pos.y);


		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
		GUI.EndGroup();
		GUI.EndGroup();
	}
	
	void Update() {
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		barDisplay = Time.time*0.05f;
		//        barDisplay = MyControlScript.staticHealth;
	}


	public static Vector3 GetScreenPosition(Transform transform,Canvas canvas)
	{
		Vector3 pos;
		float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
		float height = canvas.GetComponent<RectTransform>().sizeDelta.y;
		float x = Camera.main.WorldToScreenPoint(transform.position).x / Screen.width;
		float y = Camera.main.WorldToScreenPoint(transform.position).y / Screen.height;
		pos = new Vector3(width * x - width / 2, y * height - height / 2);    
		return pos;    
	}


}