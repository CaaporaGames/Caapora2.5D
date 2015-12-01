using UnityEngine;
using IsoTools;


/// <summary>
/// Código encontrado em tutorial no Youtube postado por Sebastian Lague
/// </summary>
namespace Caapora.Pathfinding { 
public class Node {
	
	public bool walkable;
	public Vector2 worldPosition;
	public int gridX;
	public int gridY;
	
	public int gCost;
	public int hCost;
	public Node parent;
	
	public Node(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;

    }
	
	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}

}