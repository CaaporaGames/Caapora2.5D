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
		grid = new Node[gridSizeX,gridSizeY];

         Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

        worldBottomLeft = Quaternion.Euler(-90, 0, 0) * worldBottomLeft;


		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {

                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);

                     worldPoint = Quaternion.Euler(90, 0, 0) * worldPoint; // não sei porque mas é invertido o sinal

                    bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
				grid[x,y] = new Node(walkable,worldPoint, x,y);
			}
		}
	}
	
	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();
		
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;
				
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}
		
		return neighbours;
	}

	
	// worldPosition: posição do objeto alvo
    // gridWorldSize: valor dos parametros 
	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		
		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y]; 
	}
	
	public List<Node> path;


	void OnDrawGizmos() {


            //Vector3 teste = new Vector3(gridWorldSize.x * 10, gridWorldSize.y * 10, 5);

            Vector3 teste = new Vector3(gridWorldSize.x, gridWorldSize.y, 5);

            //Debug.Log("iso_object " + iso_object);

            //Vector3 teste2 = new Vector3(iso_object.positionX, iso_object.positionY, 5);

            // right = x axis ( left -x)
            // up = y axis ( down -y )
            // foward = z exis ( backward -z )

            teste = Quaternion.Euler(-90,  0, 0) *  teste;

            // Exibe o Contorno na tela
            //Gizmos.DrawWireCube(gameObject.GetComponent<IsoObject>().transform.position, teste);

            Gizmos.DrawWireCube(transform.position, teste);

            // IsoUtils.DrawCube( iso_object.isoWorld, iso_object.position + iso_object.size * 0.5f, iso_object.size, Color.green);


            Gizmos.color = Color.cyan;

            if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				if (path != null)
					if (path.Contains(n))
						Gizmos.color = Color.black;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}
}
}

