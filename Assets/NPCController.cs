using UnityEngine;
using IsoTools;
using System.Collections;
using UnityEngine.UI;

public class NPCController : CharacterBase {

    // Armazena o componente da animação
    public Animator animator;
    public float speed = 5f;
    public GameObject go;
    public IsoObject caapora;
    public float savedTimeState;
    public IsoRigidbody iso_rigidyBody;
    public static Vector3 prevPosition;
    // Sinalizador para a movimentação automática com Pathfinding
    public static bool stopWalking = false;
    public static bool isPlayingAnimation = false;

    // prevPosition é enviado da classe de IA nesse caso da NPC
    public static NPCController instance;
    private bool _moveUp = false, _moveDown = false, _moveLeft = false, _moveRight = false, _AKey = false, _BKey = false;
    private string LookingAtDirection = "north";
    private float _life = 1000;
    private static bool _canLauchWater;

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

        // Atualiza o valor do status na interface
        GameObject.Find("Monkey/hp").GetComponent<Text>().text = _life.ToString();


        PathFindingController();

        GameOVer();


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
        {
            animator.SetTrigger("Down");
            _moveUp = false; _moveDown = false; _moveLeft = false; _moveRight = false;
        }

        else
        {

            if (prevPosition.x > _currentPosition.positionX)
            {
                _moveLeft = true;

            }


            else if (prevPosition.x < _currentPosition.positionX)
            {
                animator.SetTrigger("Right");


            }



            else if (prevPosition.y < _currentPosition.positionY)
            {
                animator.SetTrigger("Down");


            }


            else if (prevPosition.y > _currentPosition.positionY)
            {
                animator.SetTrigger("Up");

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



    public void ThrowWater()
{

    if (_canLauchWater)
    {


        switch (LookingAtDirection)
        {

            case "north":
                StartCoroutine(launchOject("north", 5));
                break;
            case "south":
                StartCoroutine(launchOject("south", 5));
                break;
            case "east":
                StartCoroutine(launchOject("east", 5));
                break;
            case "west":
                StartCoroutine(launchOject("west", 5));
                break;
            default:
                StartCoroutine(launchOject("south", 5));
                break;

        }

    }

}



    /// *************************************************************************
    /// Author: 
    /// <summary> 
    /// Sobrecarregou o método padrão do Unity OnCollisionEnter
    /// </summary>
    /// <param name="iso_collision">A referencia do objeto colidido</param>
    void OnIsoCollisionEnter(IsoCollision iso_collision)
    {




        if (iso_collision.gameObject.name == "chamas" || iso_collision.gameObject.name == "chamas(Clone)")
        {

            // Reduz o life do caipora de acordo com o demage do objeto
            _life = _life - iso_collision.gameObject.GetComponent<spreadFrame>().demage;

            StartCoroutine(NPCHit());



            var objeto = iso_collision.gameObject.GetComponent<IsoRigidbody>();
            if (objeto)
            {

                // Destroy(objeto.gameObject);

                //	objeto.transform.parent = transform;
            }
        }



    }



    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Possui Todas as condição para dar GameOver 
    /// </summary>
    void GameOVer()
    {

        if (gameObject.GetComponent<IsoObject>().positionZ < -15 || _life <= 0)
        {

            Destroy(gameObject);
            Destroy(GameObject.Find("Player"));
            Application.LoadLevel("GameOver");

        }

    }



    /// *************************************************************************
    /// Author: Rômulo Lima
    /// <summary> 
    /// Animação que deixa o sprite vermelho por um periodo de tempo
    /// </summary>o
    public IEnumerator NPCHit()
    {

        float t = 0.0f;

        // Forma gradativa de fazer transição
        while (t < 1f)
        {
            t += Time.deltaTime;

            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, t);
            yield return null;


        }


    }


    /// <summary>
    /// Anima o lançamento de um objeto
    /// </summary>
    /// <param name="distance">a distancia a ser percorrida pelo objeto</param>
    /// <param name="direction"> a direcao a ser enviado o objeto</param>
    /// <returns></returns>
    public IEnumerator launchOject(string direction, float distance)
        {


            // Carrega o prefab da água
            var objeto = Instantiate(Resources.Load("Prefabs/splashWaterPrefab")) as GameObject;

            // a posicao inicial do objeto
            var playerCurPosition = GetComponent<IsoObject>().position;

            var balde = Inventory.getItem().GetComponent<Balde>();



          //  for (int i =0; i < distance; )
          //  {

                if (direction == "east")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.right;
                    balde.UseWalter();
                }
                  
                if (direction == "west")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.left;
                    balde.UseWalter();
                }

                if (direction == "north")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.up;
                     balde.UseWalter();
                }

                if (direction == "south")
                {
                    objeto.GetComponent<IsoObject>().position = playerCurPosition + Vector3.down;
                    balde.UseWalter();
                }


                yield return new WaitForSeconds(0.1f);
        //    }


        }



    public static float life()
    {
        return instance._life;
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
}
