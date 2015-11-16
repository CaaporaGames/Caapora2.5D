using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdviceSimple : MonoBehaviour {

    // necessario para acessar metodos desta classe fora dela
    public static AdviceSimple instance;

    // Use this for initialization
    void Start () {

        // Acessar atributos da classe pelos metodos estaticos
        instance = this;

        // Ao iniciar desabilita o painel
        gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Caso chegue alguma mensagem de texto ativa o balao de mensagem e exibea
    /// </summary>
    /// <param name="text"></param>
    public static void showAdvice(string message = "")
    {
     
            instance.gameObject.SetActive(true);

            // perecisa do caminho completo nao sei porque
            GameObject.Find("CanvasGUIContainer/PanelConversa/col12/AdviceSimple/Text").GetComponent<Text>().text = message;
            GameManager.ShowObjectAPeriodOfTime(instance.gameObject, 5);


    }
}
