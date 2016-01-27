using UnityEngine;
using System.Collections;

public class BounceDie : MonoBehaviour {

	public GameObject		m_InstanciateOnDeath;
	public GameObject		m_InstanciateOnBounce;
	public int				m_NbBounces;

	private int				m_CurrentNbBounces = 0;
	private ContactPoint	m_LastHit;

	void OnCollisionEnter(Collision col)
	{
		m_CurrentNbBounces++;
		m_LastHit = col.contacts[0];
		if (m_InstanciateOnBounce != null)
		{
			GameObject.Instantiate(m_InstanciateOnBounce,
			                       m_LastHit.point + m_LastHit.normal.normalized/10.0f,
			                       //m_InstanciateOnBounce.transform.rotation);
			                       Quaternion.FromToRotation(Vector3.up, m_LastHit.normal));
		}
	}

	// Update is called once per frame
	void Update () {
		if (m_CurrentNbBounces > m_NbBounces)
		{
			PKFxFX fx = this.GetComponent<PKFxFX>();
			if (fx != null)
				fx.StopEffect();
			if (m_InstanciateOnDeath != null)
			{
				GameObject.Instantiate(m_InstanciateOnDeath,
				                       m_LastHit.point,
				                       Quaternion.FromToRotation(Vector3.up, m_LastHit.normal));
			}
			GameObject.Destroy(this.gameObject);
		}
	}
}
