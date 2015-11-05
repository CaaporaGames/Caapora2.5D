using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;



namespace PathFinding {
    public class Pathfinding : MonoBehaviour {

    private IsoObject seekerIso;
    private Vector3 _targetPos;
    public Vector3 cachedSeekerPos, cachedTargetPos;
    public Grid grid;
    private bool _start = false;
    private bool move = false, canStart = true;



        public Vector3 targetPos
        {
            get { return _targetPos; }
            set {
                        _targetPos = value;
            }
        }





        public bool click
        {
            get { return _start; }
            set
            {
                _start = value;
            }
        }


        void Awake() {
		grid = grid.GetComponent<Grid>();
	}


   void Start()
        {

            seekerIso = GetComponent<IsoObject>();
            // posicao no modo isometrico
            cachedSeekerPos = seekerIso.position;
            cachedTargetPos = _targetPos;

        }



       void Update()
       {

           if (_start) {
          //      move = true;
          //      click = false;
            }
            // enquanto nao inicia deixa as posições originais
            if (!move && canStart)
           {
               if (cachedSeekerPos != seekerIso.position)
               {
                   cachedSeekerPos = seekerIso.position;
                   FindPath(seekerIso.position, _targetPos);
               }
               if (cachedTargetPos != _targetPos)
               {
                   cachedTargetPos = _targetPos;
                   FindPath(seekerIso.position, _targetPos);
               }
           }

           // inicia IA
           else
           {
               AnimatePath();
           }
       }

        void FindPath(Vector3 startPos, Vector3 targetPos) {

           
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

            List<Node> openSet = new List<Node>();

        HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);
		
		while (openSet.Count > 0) {
			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet[i];
				}
			}
			
			openSet.Remove(currentNode);
			closedSet.Add(currentNode);
			
			if (currentNode == targetNode) {
				RetracePath(startNode,targetNode);
				return;
			}
			
			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}
				
				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = currentNode;
					
					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}
	
	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		
		grid.path = path;
		
	}
	
	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}

      

        void AnimatePath()
        {
            move = false;

            Vector3 currentPos = seekerIso.position;
            if (grid.path != null)
            {
                Debug.Log("ANIMATING PATH");
                StartCoroutine(UpdatePosition(currentPos, grid.path[0], 0));
            }
        }





        IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)
        {
            // Debug.Log ("Started Coroutine...");
            float t = 0.0f;
            // Vector3 correctedPathPos = new Vector3(n.GetWorldPos().x, 1, n.GetWorldPos().z);

            // não sei porque, mas foi necessário fazer a coversão
            // foi necessário criar uma variável apenas para essa finalizade para não sobrescrever a posição original
            Vector3 tmpWorldPosition  = seekerIso.isoWorld.ScreenToIso(n.worldPosition);

         //   Debug.Log("node position =" + n.worldPosition);

         //   Debug.Log("node position converted =" + tmpWorldPosition);

         //   Debug.Log("player position  =" + currentPos);

            Vector3 correctedPathPos = tmpWorldPosition;

            while (t < 1f)
            {
                t += Time.deltaTime;


                seekerIso.position =   Vector3.Lerp(currentPos, correctedPathPos, t);

                // Vector3.MoveTowards(currentPos, correctedPathPos, t);

                yield return null;
            }


           // Debug.Log ("Finished updating...");
            seekerIso.position = correctedPathPos;

           // Debug.Log("seeker pos after = " + seekerIso.position);
            currentPos = correctedPathPos;

            Debug.Log("Caminhos = " + index);

            // Para cada ponto do caminho executa novamente este método
            index++;
            if (index < grid.path.Count)
                StartCoroutine(UpdatePosition(currentPos, grid.path[index], index));
            else
            {
                canStart = false;   
            }
                
        } 


    } // end Pathfinding 
} // end namespace Pathfinding