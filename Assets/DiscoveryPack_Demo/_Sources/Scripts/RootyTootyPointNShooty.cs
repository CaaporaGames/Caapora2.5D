using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RootyTootyPointNShooty : MonoBehaviour {

	public Transform		m_StartPoint;
	public Camera			m_Camera;
	public Texture			m_Crossheir;
	public List<GameObject>	m_AmmoType;
	public List<float>		m_CoolDowns;
	public PKFxFX			m_FlameThrower;

	public float		m_StartVel = 50.0f;

	private int			m_CurrentAmmoType = 0;
	private bool		m_CanFire = true;

	public Texture			tex;
	public Texture			tex2;

	void OnGUI()
	{
		if (tex2 != null)
			GUI.DrawTexture(new Rect(10, 200, tex2.width/2.0f, tex2.height/2.0f), tex2);
		if (tex != null)
			GUI.DrawTexture(new Rect(10, 100, tex.width/2.0f, tex.height/2.0f), tex);

		GUI.DrawTexture(new Rect(Screen.width / 2 - m_Crossheir.width / 2,
		                         Screen.height / 2 - m_Crossheir.height / 2,
		                         m_Crossheir.width, m_Crossheir.height),
		                m_Crossheir);
		GUI.Label(new Rect(10, 10, tex.width/2.0f, tex.height/2.0f), m_AmmoType[m_CurrentAmmoType].name);
	}

	void CoolDown()
	{
		m_CanFire = true;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			m_CurrentAmmoType = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2))
			m_CurrentAmmoType = 1;

		if (Input.GetMouseButton(0) && m_CanFire)
		{
			Ray r = m_Camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
			RaycastHit rh = new RaycastHit();
			Vector3 aimPoint = r.GetPoint(500.0f);
			if (Physics.Raycast(r, out rh))
				aimPoint = rh.point;
			Vector3 bulletVel = (aimPoint - m_StartPoint.position).normalized * m_StartVel;

			GameObject go = GameObject.Instantiate(m_AmmoType[m_CurrentAmmoType],
			                                       m_StartPoint.position,
			                                       m_StartPoint.rotation) as GameObject;
			go.transform.GetComponent<Rigidbody>().AddForce(bulletVel + new Vector3((Random.value-0.5f)*20.0f,
			                                                        (Random.value-0.5f)*20.0f,0.0f));
			m_CanFire = false;
			Invoke("CoolDown", m_CoolDowns[m_CurrentAmmoType]);
		}
		else if (Input.GetMouseButton(1))
			m_FlameThrower.StartEffect();
		else
			m_FlameThrower.StopEffect();
	}
}
