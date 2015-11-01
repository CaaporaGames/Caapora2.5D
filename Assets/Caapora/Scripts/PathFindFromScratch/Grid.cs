using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;

namespace PathFinding {

    [ExecuteInEditMode]
    public class Grid : MonoBehaviour {


    public IsoObject iso_object; 
	public LayerMask unwalkableMask;
	public Vector3 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;
	
	float nodeDiameter;
	int gridSizeX, gridSizeY;
	
	void Start() {

     
            //gridWorldSize = gameObject.GetComponent<IsoObject>().size;
            iso_object = gameObject.GetComponent<IsoObject>();    

		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	
	void CreateGrid() {
        // Cria um grade vazia com o tamanho passador por parametros 
         grid = new Node[gridSizeX,gridSizeY];

            // Pega a posição Base
            Vector3 worldBottomLeft = iso_object.GetComponent<IsoObject>().position; //- Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

     //   worldBottomLeft = Quaternion.Euler(-90, 0, 0) * worldBottomLeft;

        // Popula a grade com as posições de acordo com o código
		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {


                    // Calcula as posições na grade
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);

                    // Converte posição para isometrico
                    worldPoint = iso_object.isoWorld.IsoToScreen(worldPoint);

                    
                    // Caso haja um colisão com algum elemento do tipo unwalkableMask passado como parametro seta a variavel walkable para true
                    bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius ,unwalkableMask));

                   // if (walkable)
                   // {
                   //     var floor = Instantiate(Resources.Load("Prefabs/chamas")) as GameObject;
                   //     floor.transform.position = worldPoint;
                   // }

                    
                    // popula a grade com o nó
                    grid[x,y] = new Node(walkable, worldPoint , x,y);
			}
		}
	}
	
    // retorna um vizinho de cada vez respeitando as restrições de limites de mapa
	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();
		// Verifica as posições vizinhas que são de -1 à 1 
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
                // exclui a própria posição    
				if (x == 0 && y == 0)
					continue;
				
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				
                 // respeita os limites do mapa e exclui posição do proprio elemento   
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}
		
		return neighbours;
	}

	
	// worldPosition: posição do objeto alvo no formato isometrico
    // gridWorldSize: valor dos parametros 
    // Retorna: posição do objeto na grade
	public Node NodeFromWorldPoint(Vector3 worldPosition) {

        // solução temporária pois o valor do tamanho da grade está como um número pequeno pois está no formato isometrico
       // gridWorldSize = new Vector3(300, 300, 1);
    
        // Distancia entr
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
            //Debug.Log("Valor de percenty: " + percentY);

            percentX = Mathf.Clamp01(percentX);
	    	percentY = Mathf.Clamp01(percentY);


            int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		    int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

          
            // está utilizando coordenadas isometricas
            return grid[x,y]; 
	}
	
	public List<Node> path;

    // Apenas exibe as grades
	void OnDrawGizmos() {


            //Vector3 teste = new Vector3(gridWorldSize.x * 10, gridWorldSize.y * 10, 5);

            Vector3 teste = new Vector3(gridWorldSize.x, gridWorldSize.y, 5);

            //Debug.Log("iso_object " + iso_object);

            //Vector3 teste2 = new Vector3(iso_object.positionX, iso_object.positionY, 5);

            // right = x axis ( left -x)
            // up = y axis ( down -y )
            // foward = z exis ( backward -z )

            // teste = Quaternion.Euler(-90,  0, 0) *  teste;

            // Exibe o Contorno na tela
            //Gizmos.DrawWireCube(gameObject.GetComponent<IsoObject>().transform.position, teste);

     
            // exibe o contorno
            // IsoUtils.DrawCube( iso_object.isoWorld, iso_object.position + iso_object.size * 0.5f, iso_object.size, Color.green);


            Gizmos.color = Color.cyan;

            if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ?  Color.white : Color.red;
				if (path != null)
					if (path.Contains(n))
						Gizmos.color = Color.black;


                          Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                       // IsoUtils.DrawCube(iso_object.isoWorld, n.worldPosition, Vector3.one * (nodeDiameter-.1f), Gizmos.color);
                }
		}
	}


        

    } // end Grid 
}  // namesapace

