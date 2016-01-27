using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddPKFxFXComponent : PKFxPackDependent {

	public string			m_FxName;
	public List<string>		m_FloatAttributesNames;
	public List<float>		m_FloatAttributes;
	public List<string>		m_Float3AttributesNames;
	public List<Vector3>	m_Float3Attributes;
	
	void Awake()
	{
		PKFxFX fx = this.gameObject.AddComponent<PKFxFX>();
		if (fx == null)
		{
			Debug.LogError("Failed to add PKFxFX component.\n" +
				"Did you import the PopcornFX plugin package?");
			return;
		}
		fx.m_FxName = this.m_FxName;
		fx.m_PlayOnStart = false;
	}

	IEnumerator Start()
	{
		base.BaseInitialize();
		yield return WaitForPack(true);
		PKFxFX fx = this.gameObject.GetComponent<PKFxFX>();
		if (fx == null)
			yield break;
		fx.LoadAttributes(PKFxManager.ListEffectAttributesFromFx(m_FxName), false);
		for (int i = 0; i < m_FloatAttributesNames.Count; i++)
		{
			if (i < m_FloatAttributes.Count)
				fx.SetAttribute(new PKFxManager.Attribute(m_FloatAttributesNames[i], m_FloatAttributes[i]));
		}
		for (int i = 0; i < m_Float3AttributesNames.Count; i++)
		{
			if (i < m_Float3Attributes.Count)
				fx.SetAttribute(new PKFxManager.Attribute(m_Float3AttributesNames[i], m_Float3Attributes[i]));
		}
		fx.StartEffect();
	}

}
