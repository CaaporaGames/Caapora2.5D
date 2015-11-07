using UnityEngine;
using System.Collections;

public class Advice : MonoBehaviour {

    // necessario para acessar metodos desta classe fora dela
    public static Advice instance;

    // Use this for initialization
    void Start () {

        // Acessar atributos da classe pelos metodos estaticos
        instance = this;

        // Ao iniciar desabilita o painel
        ShowAdvice(false);

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    // Romulo Lima
    // Ativa a Dica
    public static void ShowAdvice(bool value)
    {

        instance.gameObject.SetActive(value);
    }
}
