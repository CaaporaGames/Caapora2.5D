using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {
	
	private float			m_Zoom = 0.0f;
	private Vector3			m_Origin;
	
	void Start()
	{
		m_Origin = this.transform.position;
		if (QualitySettings.desiredColorSpace != ColorSpace.Linear)
			Debug.LogWarning("The rendering color space is not set to linear, " +
			                 "colors won't be accurate\n" +
			                 "To change this setting go to \"Player Settings\">\"Other Settings\">\"Color Space\"");
	}

	void Update () {
		if (Input.GetKey(KeyCode.UpArrow))
			m_Zoom += 0.05f;
		if (Input.GetKey(KeyCode.DownArrow))
			m_Zoom -= 0.05f;
		
		this.transform.position = m_Origin + this.transform.forward * m_Zoom;
	}
}
