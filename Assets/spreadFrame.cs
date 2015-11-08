using UnityEngine;
using System.Collections;
using IsoTools;

public class spreadFrame : MonoBehaviour {


    public float demage = 50;
    public float spreadTime = 3f;
	// Use this for initialization
	void Start () {

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

     
                  var frame = Instantiate(Resources.Load("Prefabs/chamas")) as GameObject;
                   frame.GetComponent<IsoObject>().position = new Vector3(current_frame.positionX + x, current_frame.positionY + y, 0);

               
                 yield return new WaitForSeconds(this.spreadTime);


            }
        }

               
    }

}
