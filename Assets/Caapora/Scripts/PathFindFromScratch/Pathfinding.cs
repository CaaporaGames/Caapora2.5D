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
    private bool move = false, canStart = false;
    public IEnumerator updatePosition;
        public IEnumerator animatePath;
      
       
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


        void Start()
        {

         

            seekerIso = GetComponent<IsoObject>();
            // posicao no modo isometrico
            cachedSeekerPos = seekerIso.position;
            cachedTargetPos = _targetPos;

        }



       void Update()
       {
           // Debug.Log("Valor de Canstart = " + canStart);
           // Debug.Log("Valor de move = " +  move);
           // Debug.Log("Valor de _start = " + _start );

            // Se clicar inicia
            if (_start) {
                Debug.Log("Habilitou o move pelo _start");
                move = true;
                canStart = true;
            }
            // enquanto nao inicia deixa as posições originais
            if (!move && canStart)
           {

                Debug.Log("Movimentacao desabilitada ");

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
                if(canStart)
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
            click = false;
        
            Vector3 currentPos = seekerIso.position;

            Debug.Log("ANIMATING PATH");

            if (canStart) { 
              updatePosition = UpdatePosition(currentPos, grid.path[0], 0);
              StartCoroutine(updatePosition);
            }

        }


     
       
        IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)
        {
   
            float t = 0.0f;
            // Vector3 correctedPathPos = new Vector3(n.GetWorldPos().x, 1, n.GetWorldPos().z);

            // não sei porque, mas foi necessário fazer a coversão
            // foi necessário criar uma variável apenas para essa finalizade para não sobrescrever a posição original
            Vector3 tmpWorldPosition  = seekerIso.isoWorld.ScreenToIso(n.worldPosition);



            Vector3 correctedPathPos = tmpWorldPosition;

            while (t < 0.5f)
            {
                t += Time.deltaTime;

            
                seekerIso.position =   Vector3.Lerp(currentPos, correctedPathPos, t);


                Completed.PlayerBehavior.stopWalking = false;
                // Apenas para o caipora, seta a posição anterior para movimentação automática
                Completed.PlayerBehavior.prevPosition = currentPos;
                // Vector3.MoveTowards(currentPos, correctedPathPos, t);

                yield return null;
            }


            seekerIso.position = correctedPathPos;

     
            currentPos = correctedPathPos;

      
            // Para cada ponto do caminho executa novamente este método
            index++;
            if (index < grid.path.Count)
            {

                  updatePosition = UpdatePosition(currentPos, grid.path[index], index);

                  StartCoroutine(updatePosition);
                  // grid.path.Remove(grid.path[index]);
                  Debug.Log("Caminho " + index + " Alcançado");
       
            }
               
            else
            {

                Completed.PlayerBehavior.stopWalking = true;
                Debug.Log("UpdatePositio finalizado");
                canStart = false;
                StopCoroutine(updatePosition);
                               

            }
                
        } 


    } // end Pathfinding 
} // end namespace Pathfinding