using UnityEngine;
using System.Collections;
using IsoTools;

public class SpreadFrame : MonoBehaviour {


    protected float demage;
    private float spreadTime;
    private IsoRigidbody rb;
    private GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");


        spreadTime = 3f / (LevelController.GetCurrentLevel() + 1);
        demage = 100f;

       
        StartCoroutine(multiplyFrame());
    }
	
	// Update is called once per frame
	void Update () {
	

	}


    

    // Romulo Lima
    // Multiplica um elemento para seus vizinhos
    public IEnumerator multiplyFrame()
    {
        
        //pega a posicao do fogo
        IsoObject current_frame = GetComponent<IsoObject>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // exclui a própria posição    
                if (x == 0 && y == 0)
                    continue;


                StartCoroutine(createNewFlame(current_frame, 0, y));

               
                 yield return new WaitForSeconds(this.spreadTime);


            }
        }

               
    }


    // Romulo Lima
    // Multiplica um elemento para seus vizinhos
    public IEnumerator multiplyFrameInLine()
    {

        //pega a posicao do fogo
        IsoObject current_frame = GetComponent<IsoObject>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // exclui a própria posição    
                if (x == 0 && y == 0)
                    continue;


                StartCoroutine(createNewFlame(current_frame, x, 1));


                yield return new WaitForSeconds(this.spreadTime);


            }
        }


    }



    IEnumerator createNewFlame(IsoObject current_frame,int x, int y)
    {


       // var frame = Instantiate(Resources.Load("Prefabs/chamas")) as GameObject;
       
        var frame = ObjectPool.instance.GetObjectForType("chamas", true);

        // Adiciona em tempo de execucao para ganhar performance

        frame.GetComponent<IsoRigidbody>().mass = 0.01f;

        frame.GetComponent<IsoObject>().position =
            new Vector3(current_frame.positionX + x, current_frame.positionY + y, 0);

        yield return null;


    }

    public float GetDamage()
    {

        return demage;

    }

}
