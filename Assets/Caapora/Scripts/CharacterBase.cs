using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasicStates{
	
	public float startLife;
	public float startMana;
	public int strenght;
	public int magic;
	public int agillity;
	public int baseDefence;
	public int baseAttack;
}

public abstract class CharacterBase : MonoBehaviour {

	public int currentLevel;
	public BasicStates basicStats;
	
	// Use this for initialization
	protected void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
	
	}
}
