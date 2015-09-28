using UnityEngine;
using System.Collections;

public abstract class Hero : MonoBehaviour {

    private int baseLife;
    private int baseEnergy;
    private int baseDefense;
    private int baseAttack;

    public Hero()
    {

    }

    public Hero(int baseLife, int baseEnergy, int baseDefense, int baseAttack)
    {

        this.BaseLife = baseLife;
        this.BaseEnergy = baseEnergy;
        this.BaseDefense = baseDefense;
        this.BaseAttack = baseAttack;

    }

    public int BaseLife
    {
        get
        {
            return baseLife;
        }

        set
        {
            baseLife = value;
        }
    }

    public int BaseEnergy
    {
        get
        {
            return baseEnergy;
        }

        set
        {
            baseEnergy = value;
        }
    }

    public int BaseDefense
    {
        get
        {
            return baseDefense;
        }

        set
        {
            baseDefense = value;
        }
    }

    public int BaseAttack
    {
        get
        {
            return baseAttack;
        }

        set
        {
            baseAttack = value;
        }
    }

}
