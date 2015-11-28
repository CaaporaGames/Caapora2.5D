using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;

/// <summary>
/// Código encontrado em tutorial no Youtube postado por Sebastian Lague
/// </summary>
namespace Caapora.Pathfinding {

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

            var testPos = GameObject.Find("centerRef");

            Debug.Log("Posicao Iso de centerRef = " + testPos.GetComponent<IsoObject>().position);


            Debug.Log("Posicao world de centerRef = " + testPos.transform.position);


            Debug.Log("Posicao convertida com isoToScreen = " + iso_object.isoWorld.IsoToScreen(testPos.GetComponent<IsoObject>().position));



		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	
	void CreateGrid() {
        // Cria um grade vazia com o tamanho passador por parametros 
         grid = new Node[gridSizeX,gridSizeY];

            // Pega a posição Base
            Vector3 worldBottomLeft = iso_object.GetComponent<IsoObject>().position;

     //   worldBottomLeft = Quaternion.Euler(-90, 0, 0) * worldBottomLeft;

        // Popula a grade com as posições de acordo com o código
		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {

                    // Gera a grade baseado nas posições isometricas de 0,0 à n,n
                    Vector3 worldPoint = worldBottomLeft + new Vector3(x, y, 0);


                    // Converte posição para isometrico
                    var newWorldPoint = iso_object.isoWorld.IsoToScreen(worldPoint);


                    // Caso haja um colisão com algum elemento do tipo unwalkableMask passado como parametro seta a variavel walkable para true
                    bool walkable = (Physics2D.OverlapCircle(newWorldPoint, 20, unwalkableMask, 100, 100) == null); // if no collider2D is returned by overlap circle, then this node is walkable

               
                            var debugObject = new GameObject("Grid " + newWorldPoint.x + "x" + newWorldPoint.y);
                              debugObject.transform.position = new Vector3(newWorldPoint.x, newWorldPoint.y, 0f);
                    


                    if (!walkable)
                        Debug.Log("encontrou um obstaculo");

                            // if (walkable)
                            // {
                            //     var floor = Instantiate(Resources.Load("Prefabs/chamas")) as GameObject;
                            //     floor.transform.position = worldPoint;
                            // }


                            // popula a grade com o nó
                            grid[x,y] = new Node(walkable, newWorldPoint, x,y);
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

    // Debug
    // Apenas exibe as grades
	void OnDrawGizmos() {


            Gizmos.color = Color.cyan;

            if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ?  Color.white : Color.red;
				if (path != null)
					if (path.Contains(n))
                        {
                            Gizmos.color = Color.black;
                           // Debug.Log("Dot Position = " + n.worldPosition);

                        }

                    // Exemplo de codigo para instanciar isoObject
                    // var floor = new GameObject();
                    // floor.AddComponent<IsoObject>();
                    // floor.transform.position = n.worldPosition;


                   // Handles.color = Color.red;
                   // Handles.DrawWireDisc(n.worldPosition // position
                   //                               , Vector3.forward  // normal
                   //                               , nodeDiameter * 2);

                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                       // IsoUtils.DrawCube(iso_object.isoWorld, n.worldPosition, Vector3.one * (nodeDiameter-.1f), Gizmos.color);
                }
		}
	}


        

    } // end Grid 
}  // namesapace

