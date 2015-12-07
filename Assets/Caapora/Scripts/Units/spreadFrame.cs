using UnityEngine;
using System.Collections;
using IsoTools;

public class SpreadFrame : MonoBehaviour {

    private float spreadTime;
    private GameObject player;


	void Start () {

        player = GameObject.Find("Player");

        spreadTime = 3f;

        StartCoroutine(MultiplyFrameInLine());

    }

    public IEnumerator MultiplyFrameInLine()
    {
        IsoObject current_frame = GetComponent<IsoObject>();

        for (int i = 0; i < 7; i++)
        {
            StartCoroutine(createNewFlame(current_frame, i, 0));

            yield return new WaitForSeconds(spreadTime);
        }

    }

    public IEnumerator MultiplyFrame()
    {

        IsoObject current_frame = GetComponent<IsoObject>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                
                 StartCoroutine(createNewFlame(current_frame, y, x));
        
                 yield return new WaitForSeconds(spreadTime);

            }
        }
               
    }


  




    IEnumerator createNewFlame(IsoObject current_frame,int x, int y)
    {


       // var frame = Instantiate(Resources.Load("Prefabs/chamas")) as GameObject;
        var frame = ObjectPool.instance.GetObjectForType("chamasSemSpread", true);
        frame.GetComponent<IsoRigidbody>().mass = 0.01f;
        frame.GetComponent<IsoObject>().position =
            new Vector3((current_frame.positionX + x), (current_frame.positionY + y), 0);

        yield return null;


    }

 

}
