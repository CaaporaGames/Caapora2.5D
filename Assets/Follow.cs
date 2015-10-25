using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public NavMeshAgent navComponent;
	// Use this for initialization
	void Start () {
	
		navComponent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (target) {
		
			navComponent.SetDestination(target.position);
		}
	
	}
}
