using UnityEngine;
using System.Collections;

public class RomanCandle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PKFxFX fx = this.GetComponent<PKFxFX>();
		if (fx == null)
		{
			Debug.LogError("Can't init roman candle");
			return;
		}
		fx.SetAttribute(new PKFxManager.Attribute("RGB", new Vector3(Random.value+0.1f, Random.value+0.1f, Random.value+0.1f)));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
