using UnityEngine;
using System.Collections;
using IsoTools;
public class SplashWater : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<IsoRigidbody>().mass = 0.01f;
    }
	
	// Update is called once per frame
	void Update () {

         
         StartCoroutine(AutoDestroy());
	}



    /// *************************************************************************
    /// Author: 
    /// <summary> 
    /// Sobrecarregou o método padrão do Unity OnCollisionEnter
    /// </summary>
    /// <param name="iso_collision">A referencia do objeto colidido</param>
    void OnIsoCollisionEnter(IsoCollision iso_collision)
    {

        // Caso o fogo colida com o splash de agua deleta os dois
        if (iso_collision.gameObject.name == "chamas" || iso_collision.gameObject.name == "chamas(Clone)")
        {

            Debug.Log("Colidiu com a agua");

            Destroy(iso_collision.gameObject);
            Destroy(gameObject);

        }


    }



    public IEnumerator AutoDestroy()
    {
     
        yield return new WaitForSeconds(1);
        Destroy(gameObject);


    }
}
