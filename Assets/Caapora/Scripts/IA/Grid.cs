using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;


namespace Caapora.Pathfinding {
    [ExecuteInEditMode]
    public class Grid : MonoBehaviour {


    public IsoWorld world; 
	public LayerMask unwalkableMask;
	public Vector3 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;
    public List<Node> path;

    float nodeDiameter;
	int gridSizeX, gridSizeY;
	
	void Start() {


        world = GameObject.Find("Player/Camera").GetComponent<IsoWorld>();

            
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	
	void CreateGrid() {
         // Cria um grade vazia com o tamanho passador por parametros 
         grid = new Node[gridSizeX,gridSizeY];

         // Pega a posi��o Base
         Vector3 worldBottomLeft = GetComponent<IsoObject>().position;


        // Popula a grade com as posi��es de acordo com o c�digo
		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {

                    // Gera a grade baseado nas posi��es isometricas de 0,0 � n,n
                    Vector3 worldPoint = worldBottomLeft + new Vector3(x, y, 0);

                    // Converte posi��o para isometrico
                    var newWorldPoint = world.IsoToScreen(worldPoint);


                    // Caso haja um colis�o com algum elemento do tipo unwalkableMask passado como parametro seta a variavel walkable para true
                    bool walkable = (Physics2D.OverlapCircle(newWorldPoint, 0.2f, unwalkableMask) == false);

     
                    // popula a grade com o n�
                    grid[x,y] = new Node(walkable, newWorldPoint, x,y);
			}
		}
	}


        protected bool IsDiagonal(int x, int y)
        {

            if (x != 0 && y != 0)
                return true;

            return false;
        }

        // retorna um vizinho de cada vez respeitando as restri��es de limites de mapa
        public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();
		// Verifica as posi��es vizinhas que s�o de -1 � 1 
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {

                // exclui a pr�pria posi��o    ou diagonal
				if ((x == 0 && y == 0) || IsDiagonal(x,y))
					continue;
				
                
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				
                 // respeita os limites do mapa e exclui posi��o do proprio elemento   
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}
		
		return neighbours;
	}

	
	// worldPosition: posi��o do objeto alvo no formato isometrico
    // gridWorldSize: valor dos parametros 
    // Retorna: posi��o do objeto na grade
	public Node NodeFromWorldPoint(Vector3 worldPosition) {

           
        // Distancia entr
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
            //Debug.Log("Valor de percenty: " + percentY);

            percentX = Mathf.Clamp01(percentX);
	    	percentY = Mathf.Clamp01(percentY);


            int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		    int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

          
            // est� utilizando coordenadas isometricas
            return grid[x,y]; 
	}
	
	


	void OnDrawGizmos() {


            Gizmos.color = Color.cyan;

            if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ?  Color.white : Color.red;
				if (path != null)
					if (path.Contains(n))
                        {
                           
                            Gizmos.color = Color.black;
                          
                        }

             
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                       
                }
		}
	}

   

    } // end Grid 
}  // namesapace

