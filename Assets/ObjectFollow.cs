using UnityEngine;
using System.Collections;

public class ObjectFollow : MonoBehaviour {

	public RectTransform canvasRectT;
	public RectTransform healthBar;
	public Transform objectToFollow;


	void Update()
	{
//		healthBar = gameObject.GetComponents<RectTransform> ();

	//	Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position);
		//healthBar.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
	}
}
