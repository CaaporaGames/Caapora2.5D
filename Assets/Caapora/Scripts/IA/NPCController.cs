using UnityEngine;
using IsoTools;


namespace Caapora{


public class NPCController : Character {

    // Armazena o componente da animação
    public IsoRigidbody iso_rigidyBody;
    public static Vector3 prevPosition;
    public static NPCController instance;

    // Sinalizador para a movimentação automática com Pathfinding
    public static bool stopWalking = false;
    private string _movingTo = "down";


       

        // Use this for initialization
       public override void Start () {
        // Herda da classe base
        base.Start();


        instance = this;

        iso_rigidyBody = GetComponent<IsoRigidbody>();

        currentLevel = StatsController.GetCurrentLevel();

        _animator = GetComponent<Animator>();

        // posiçao inicial do 
        gameObject.GetComponent<IsoObject>().position += new Vector3(0, 0, 0);


    }

    // Update is called once per frame
    public override void Update () {
            base.Update();
            
        // Movimentação pelo teclado do player através de flags


        // Habilitar a movimentação por clique no local
        // moveToPlace();



        PathFindingController();

  


    }

    void PathFindingController()
    {


        var velocidade = gameObject.GetComponent<IsoRigidbody>().velocity;
        var _currentPosition = GetComponent<IsoObject>();
        var PositionDiff = prevPosition - _currentPosition.position;


            prevPosition.x = Mathf.Floor(prevPosition.x);
            prevPosition.y = Mathf.Floor(prevPosition.y);
            _currentPosition.positionX = Mathf.Floor(_currentPosition.positionX);
            _currentPosition.positionY = Mathf.Floor(_currentPosition.positionY);


            if (prevPosition == Vector3.zero || stopWalking)
            {
                _animator.SetTrigger("Down");
            }
            else { 
         
                    if (prevPosition.x == _currentPosition.positionX + 1)
                    {


                    moveLeft();

                    }


                    else if (prevPosition.x == _currentPosition.positionX - 1)
                    {

                    moveRight();
        

                    }



                    else if (prevPosition.y == _currentPosition.positionY + 1)
                    {

                    moveUp();
           


                }


                    else if (prevPosition.y ==_currentPosition.positionY - 1 )
                    {

                    moveDown();
             

                }

            }



     


        // seta a posição alvo do player para a posição do clique no mapa
        //GetComponent<PathFinding.Pathfinding>().targetPos = new Vector3(13, 13, 0);

        // quando clicar com o botão esquerdo do Mouse
        // if (Input.GetButtonDown("Fire1"))


        if (Input.touchCount > 0)
        {

            //var clickIsoPosition = gameObject.GetComponent<IsoObject>().isoWorld.MouseIsoTilePosition();

            var clickIsoPosition = gameObject.GetComponent<IsoObject>().isoWorld.TouchIsoTilePosition(0);
            // seta a posição alvo do player para a posição do clique no mapa
            // GetComponent<PathFinding.Pathfinding>().targetPos = clickIsoPosition;

            // Debug.Log("Posicao do clique do mouse foi = " + clickIsoPosition);

            // necessário executar apos um periodo de tempo para dar tempo de pegar a posição
            // StartCoroutine(clicked());

        }

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


public void moveLeft()
{


    iso_rigidyBody.velocity = new Vector3(-this.speed, 0, 0);

    _animator.SetTrigger("Left");

}

public void moveRight()
{


    iso_rigidyBody.velocity = new Vector3(this.speed, 0, 0);
    _animator.SetTrigger("Right");

}


public void moveDown()
{

    iso_rigidyBody.velocity = new Vector3(0, -this.speed, 0);
    _animator.SetTrigger("Up");


}


public void moveUp()
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