using UnityEngine;


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
	




}
