using UnityEngine;
using System.Collections;

public class TimedSuicide : MonoBehaviour {

	public GameObject	m_InstanciateOnDeath;
	public float		m_Time;

	void Die()
	{
		PKFxFX fx = this.GetComponent<PKFxFX>();
		if (fx != null)
			fx.StopEffect();
		if (m_InstanciateOnDeath != null)
		{
			GameObject.Instantiate(m_InstanciateOnDeath,
			                       this.gameObject.transform.position,
			                       this.gameObject.transform.rotation);
		}

		GameObject.Destroy(this.gameObject);
	}
	
	void Update ()
	{
		Invoke("Die", m_Time);
	}
}
