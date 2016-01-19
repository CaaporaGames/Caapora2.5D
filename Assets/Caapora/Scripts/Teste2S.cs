using UnityEngine;
using System.Collections;
using IsoTools;

public class Teste2S : MonoBehaviour {

    public GameObject enemy;
    public GameObject hero;
    public LayerMask enemyLayer;
    public IsoWorld world;


	// Use this for initialization
	void Start () {

        enemy = GameObject.Find("Enemy");
        hero = GameObject.Find("Hero");


        for (int x = 0; x < 30; x++)
        {
            for (int y = 0; y < 30; y++)
            {
                world = GameObject.Find("Camera").GetComponent<IsoWorld>();


                // Gera a grade baseado nas posições isometricas de 0,0 à n,n
                Vector3 worldPoint = new Vector3(x, y, 0);

                // Converte posição para isometrico
                var newWorldPoint = world.IsoToScreen(worldPoint);


                if (Physics2D.OverlapCircle(newWorldPoint , 0.2f, enemyLayer))
                    Debug.Log("Colisao detectada com OverlapCircle na grade : newWorldPoint = " + newWorldPoint);


            }
        }
    }
	
	// Update is called once per frame
	void Update () {


        Collider2D[] colliders = Physics2D.OverlapCircleAll(hero.transform.position, 0.1f, enemyLayer);
        if (colliders.Length > 0)
        {
            Debug.Log("Colisao detectada com OverlapCircleAll");

        }


        if (Physics2D.OverlapCircle(hero.transform.position, 0.1f, enemyLayer))
            Debug.Log("Colisao detectada com OverlapCircle");
                
     }   
}
