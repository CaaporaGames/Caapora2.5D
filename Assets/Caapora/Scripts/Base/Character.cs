using IsoTools;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Caapora
{
   public class Character : CharacterBase
    {

        protected float _life = 1000;
        public float speed = 5f;
        public static Character instance;
        protected static bool _canLauchWater;
        protected Animator animator;


        void UpdateBar()
        {

            GetComponentInChildren<Scrollbar>().size = _life / 1000;

        }

        protected void Start()
        {

            animator = GetComponent<Animator>();

        }


        protected void Update()
        {
            if (_life > 1000)
                    _life = 1000;


            UpdateBar();
            Destroy();

        }

     
        /// *************************************************************************
        /// Author: 
        /// <summary> 
        /// Sobrecarregou o método padrão do Unity OnCollisionEnter
        /// </summary>
        /// <param name="iso_collision">A referencia do objeto colidido</param>
        protected void OnIsoCollisionStay(IsoCollision iso_collision)
        {

            if (iso_collision.gameObject.name == "Altar")
            {
                Debug.Log("Recuperando life");

                _life = _life + 5;


            }


            if (iso_collision.gameObject.name == "chamas" || iso_collision.gameObject.name == "chamas(Clone)")
            {

                // Reduz o life do caipora de acordo com o demage do objeto
                _life = _life - iso_collision.gameObject.GetComponent<SpreadFrame>().demage;

                StartCoroutine(CharacterHit());



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
        void Destroy()
        {

            if (gameObject.GetComponent<IsoObject>().positionZ < -15 || _life <= 0)
            {

                Destroy(gameObject);
               

            }

        }




        public void ThrowWater()
        {

            Debug.Log("Jogou agua e valor de _canLauchWater = " + _canLauchWater);
            if (canLauchWater())
            {
                animator.SetTrigger("Atack2");

                Debug.Log("movedirection throw water =" + KeyboardController.instance.moveDirection);

                switch (KeyboardController.instance.lookingAt)
                {

                        
                    case "up":
                        StartCoroutine(launchOject("north", 5));
                        break;
                    case "down":
                        StartCoroutine(launchOject("south", 5));
                        break;
                    case "right":
                        StartCoroutine(launchOject("east", 5));
                        break;
                    case "left":
                        StartCoroutine(launchOject("west", 5));
                        break;
                    default:
                        StartCoroutine(launchOject("south", 5));
                        break;

                }

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

        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Animação que deixa o sprite vermelho por um periodo de tempo
        /// </summary>o
        public IEnumerator CharacterHit()
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
        /// Condição para poder jogar água
        /// </summary>
        /// <returns></returns>
        private bool canLauchWater()
        {
            // Se houver algum item no invetory e esse item tiver agua
            return !Inventory.isEmpty() && Inventory.getItem().GetComponent<Balde>().waterPercent > 0;
        }

        public float life
        {
            get
            {
                return this._life;
            }
            set
            {
                this._life = value;
            }
        }



    } // end CharacterController
} // end namespace
