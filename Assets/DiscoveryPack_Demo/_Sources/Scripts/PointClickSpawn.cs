using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointClickSpawn : MonoBehaviour {

	public List<GameObject>	m_FX;
	public Texture 			tex;
	public Texture 			tex2;

	private int				m_CurrentFx = 0;

	void OnGUI()
	{
		if (tex != null)
			GUI.DrawTexture(new Rect(10, 20, tex.width/2.0f, tex.height/2.0f), tex);
		if (tex2 != null)
			GUI.DrawTexture(new Rect(10, 120, tex2.width/2.0f, tex2.height/2.0f), tex2);
		GUI.Label(new Rect(10, 10, tex.width/2.0f, tex.height/2.0f), m_FX[m_CurrentFx].name);
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.RightArrow))
			m_CurrentFx = (m_CurrentFx + 1) % m_FX.Count;
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			m_CurrentFx = (m_CurrentFx - 1) % m_FX.Count;
			if (m_CurrentFx < 0)
				m_CurrentFx = m_FX.Count + m_CurrentFx;
		}

		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray,out hit, 500))
			{
				GameObject prefab = m_FX[m_CurrentFx];
				GameObject go = Instantiate(prefab, hit.point + hit.normal.normalized / 10.0f, prefab.transform.rotation) as GameObject;
				go.transform.Rotate(Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles);
				go.transform.Translate(prefab.transform.position);
			}
		}
	}
}
