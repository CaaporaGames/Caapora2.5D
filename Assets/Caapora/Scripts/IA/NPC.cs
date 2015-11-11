using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;


/// <summary>
/// Código encontrado em tutorial no Youtube postado por Sebastian Lague
/// </summary>
namespace Caapora.Pathfinding {

    public class NPC : Pathfinding {
    private bool move = true, canStart = true;
    public IEnumerator updatePosition;
    public IEnumerator animatePath;
    private IsoObject seekerIso;
    public GameObject _targetPos;


        public Vector3 targetPos
        {

      
            get { return _targetPos.GetComponent<IsoObject>().position; }
            set {
                        _targetPos.GetComponent<IsoObject>().position = value;
            }
        }





        void Start()
        {


            seekerIso = GetComponent<IsoObject>();
            // posicao no modo isometrico
            cachedSeekerPos = seekerIso.position;
            cachedTargetPos = _targetPos.GetComponent<IsoObject>().position;



        }

       void find()
       {

                if (cachedSeekerPos != seekerIso.position)
                {
                    cachedSeekerPos = seekerIso.position;
                    FindPath(seekerIso.position, _targetPos.GetComponent<IsoObject>().position);
                }
                if (cachedTargetPos != _targetPos.GetComponent<IsoObject>().position)
                {
                    cachedTargetPos = _targetPos.GetComponent<IsoObject>().position;
                    FindPath(seekerIso.position, _targetPos.GetComponent<IsoObject>().position);
                }
          


        }



       void Update()
       {

            if (GameManager.npc_start) { 

                        // Enquanto não move calcula o caminho
                        if (!move)
                        {
                            find();
           
                        }

                            AnimatePath();


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


                NPCController.stopWalking = false;

                // Apenas para o caipora, seta a posição anterior para movimentação automática
                NPCController.prevPosition = currentPos;



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

       
            }
               
            else
            {

                move = false;
                GameManager.npc_start = false;
                Debug.Log("UpdatePositio finalizado");
                StopCoroutine(updatePosition);
                               

            }
                
        }


        void AnimatePath()
        {
            // Enquanto estiver animando para de checar a posição
            move = false;

            Vector3 currentPos = seekerIso.position;


            if (canStart)
            {
                updatePosition = UpdatePosition(currentPos, grid.path[0], 0);
                StartCoroutine(updatePosition);
            }

        }

    } // end Pathfinding 
} // end namespace Pathfinding