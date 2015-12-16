using UnityEngine;
using IsoTools;


namespace Caapora{


public abstract class NPCBase : CharacterBase {


    public static NPCBase instance;
    private string _movingTo = "down";


  
       public override void Start () {

        base.Start();


        instance = this;

        currentLevel = StatsController.GetCurrentLevel();

        GetComponent<IsoObject>().position += new Vector3(0, 0, 0);


    }

        public override void OnIsoCollisionStay(IsoCollision iso_collision)
        {

            base.OnIsoCollisionStay(iso_collision);

            if (iso_collision.gameObject.name == "splashWaterPrefab(Clone)")
            {

                _life = _life - 50;


            }
        }

        public override void Update () {
            base.Update();
            

            AutomaticMovement(this);


        }



       public string movingTo
        {
            set
            {
                _movingTo = value;
            }
            get
            {
                return _movingTo;
            }
        }


public override void moveLeft()
{


    iso_rigidyBody.velocity = new Vector3(-this.speed, 0, 0);

    _animator.SetTrigger("Left");

}

public override void moveRight()
{


    iso_rigidyBody.velocity = new Vector3(this.speed, 0, 0);
    _animator.SetTrigger("Right");

}


public override void moveDown()
{

    iso_rigidyBody.velocity = new Vector3(0, -this.speed, 0);
    _animator.SetTrigger("Up");


}


public override void moveUp()
{

    iso_rigidyBody.velocity = new Vector3(0, this.speed, 0);
    _animator.SetTrigger("Down");

}



public void Jump()
{
    iso_rigidyBody.velocity = new Vector3(0, 0, this.speed);


}



}  // end NPCController




} // end namespace