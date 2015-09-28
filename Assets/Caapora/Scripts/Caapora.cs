using UnityEngine;
using System.Collections;
using IsoTools;

namespace Completed { 
public class Caapora : Hero {

    // public GameObject gameObject = null;
    public Animator animator;
    public float range;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private IsoBoxCollider boxCollider;
    private float moveTime;
    private LayerMask blockingLayer;

    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();
        // gameObject = Instantiate(Resources.Load("Prefabs/FloorPrefab")) as GameObject;

		// posiçao inicial do caipora
		gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0, 0);

        rb2D = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<IsoBoxCollider>();

        inverseMoveTime = 1f / moveTime;

    }
	
	// Update is called once per frame
	void Update () {

        int h = (int) (Input.GetAxisRaw("Horizontal"));
        int v = (int) (Input.GetAxisRaw("Vertical"));

            if (h != 0)
            {
                v = 0;
            }

            if (h != 0 || v != 0)
        {
            //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
            //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
            AttemptMove<Wall>(h, v);
        }

        // transform.position += new Vector3(xPos, yPos, 0);


        //if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {

        if (Input.GetKey (KeyCode.LeftArrow)) {
		
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (-0.1f, 0, 0);
				animator.SetTrigger ("CaaporaWest");

			} 
			if (Input.GetKey (KeyCode.RightArrow)) {
			
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (0.1f, 0, 0);
				animator.SetTrigger ("CaaporaEast");

			} 
			if (Input.GetKey (KeyCode.DownArrow)) {
			
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, -0.1f, 0);
				animator.SetTrigger ("CaaporaSouth");


			}
			if (Input.GetKey (KeyCode.UpArrow)) {
			
				gameObject.GetComponent<IsoObject> ().position += new Vector3 (0, 0.1f, 0);
				animator.SetTrigger ("CaaporaNorth");

			}

	//	}else{
        
    //        animator.SetTrigger("CaaporaIdle");
    //    }

        if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            animator.SetTrigger("CaaporaIdle");
        }

	
	}

    
        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            //Store start position to move from, based on objects current transform position.
            Vector2 start = transform.position;

            // Calculate end position based on the direction parameters passed in when calling Move.
            Vector2 end = start + new Vector2(xDir, yDir);

            //Disable the boxCollider so that linecast doesn't hit this object's own collider.
            boxCollider.enabled = false;

            //Cast a line from start point to end point checking collision on blockingLayer.
            hit = Physics2D.Linecast(start, end, blockingLayer);

            //Re-enable boxCollider after linecast
            boxCollider.enabled = true;

            //Check if anything was hit
            if (hit.transform == null)
            {
                //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
                StartCoroutine(SmoothMovement(end));

                //Return true to say that Move was successful
                return true;
            }

            //If something was hit, return false, Move was unsuccesful.
            return false;
        }

        protected IEnumerator SmoothMovement(Vector3 end)
        {
            //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
            //Square magnitude is used instead of magnitude because it's computationally cheaper.
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //While that distance is greater than a very small amount (Epsilon, almost zero):
            while (sqrRemainingDistance > float.Epsilon)
            {
                //Find a new position proportionally closer to the end, based on the moveTime
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

                //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                rb2D.MovePosition(newPostion);

                //Recalculate the remaining distance after moving.
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;

                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                yield return null;
            }
        }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
            where T : Component
    {
        //Hit will store whatever our linecast hits when Move is called.
        RaycastHit2D hit;

        //Set canMove to true if Move was successful, false if failed.
        bool canMove = Move(xDir, yDir, out hit);

        //Check if nothing was hit by linecast
        if (hit.transform == null)
            //If nothing was hit, return and don't execute further code.
            return;

        //Get a component reference to the component of type T attached to the object that was hit
        T hitComponent = hit.transform.GetComponent<T>();

        //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
        if (!canMove && hitComponent != null)

            //Call the OnCantMove function and pass it hitComponent as a parameter.
            OnCantMove(hitComponent);
    }

    protected void OnCantMove<T>(T component)
        where T : Component
    {
    }

}

}