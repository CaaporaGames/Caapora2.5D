using UnityEngine;
using IsoTools;


namespace Caapora{


public class NPCController : CharacterController {

    // Armazena o componente da animação
    public Animator animator;
    public IsoRigidbody iso_rigidyBody;
    public static Vector3 prevPosition;
    // Sinalizador para a movimentação automática com Pathfinding
    public static bool stopWalking = false;
        private string _movingTo = "down";


    


    // Use this for initialization
    void Start () {

        // Herda da classe base
        base.Start();


        instance = this;

        currentLevel = StatsController.GetCurrentLevel();

        animator = GetComponent<Animator>();

        // posiçao inicial do 
        gameObject.GetComponent<IsoObject>().position += new Vector3(0, 0, 0);


    }

    // Update is called once per frame
    void Update () {

        // Movimentação pelo teclado do player através de flags


        // Habilitar a movimentação por clique no local
        // moveToPlace();



        PathFindingController();

  


    }



    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Controle de movimentação baseada no clique do destino no mapa, detecta a direção de destino e ativa a animação correspondente
    /// </summary>
    void PathFindingController()
    {

        // Movimentação por clique na posição desejada
        var velocidade = gameObject.GetComponent<IsoRigidbody>().velocity;
        var _currentPosition = GetComponent<IsoObject>();



            // Animação por identificação de movimentação
            // prevPosition é enviado da classe de IA nesse caso da NPC
            if (prevPosition == Vector3.zero || stopWalking)


                animator.SetTrigger("Down");

          
            else { 
         
                    if (prevPosition.x > _currentPosition.positionX)
                    {


                            animator.SetTrigger("Right");

                    }


                    else if (prevPosition.x < _currentPosition.positionX)
                    {

                            animator.SetTrigger("Left");


                        }



                    else if (prevPosition.y < _currentPosition.positionY)
                    {

                            animator.SetTrigger("Up");


                        }


                    else if (prevPosition.y > _currentPosition.positionY)
                    {

                            animator.SetTrigger("Down");

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

   

// Métodos com movimentação
public void moveLeft()
{


    iso_rigidyBody.velocity = new Vector3(-this.speed, 0, 0);

    animator.SetTrigger("Left");

}

public void moveRight()
{


    iso_rigidyBody.velocity = new Vector3(this.speed, 0, 0);
    animator.SetTrigger("Right");

}


public void moveDown()
{

    iso_rigidyBody.velocity = new Vector3(0, -this.speed, 0);
    animator.SetTrigger("Up");


}


public void moveUp()
{

    iso_rigidyBody.velocity = new Vector3(0, this.speed, 0);
    animator.SetTrigger("Down");

}



public void Jump()
{
    iso_rigidyBody.velocity = new Vector3(0, 0, this.speed);


}



}  // end NPCController




} // end namespace