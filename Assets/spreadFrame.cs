using UnityEngine;
using System.Collections;
using IsoTools;

public class spreadFrame : MonoBehaviour {

    public float spreadTime = 3f;
	// Use this for initialization
	void Start () {

        StartCoroutine(multiplyFrame());
    }
	
	// Update is called once per frame
	void Update () {
	
	}



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

                Debug.Log("inseringo fogo na posicao: ("+ (current_frame.tilePositionX + x )+ " e " + (current_frame.tilePositionY + y) + ")");

                 yield return new WaitForSeconds(this.spreadTime);


            }
        }

               
    }



    /* retorna um vizinho de cada vez respeitando as restrições de limites de mapa
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        // Verifica as posições vizinhas que são de -1 à 1 
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // exclui a própria posição    
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                // respeita os limites do mapa e exclui posição do proprio elemento   
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    } */
}
