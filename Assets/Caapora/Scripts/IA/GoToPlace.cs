using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;

/// <summary>
/// Código encontrado em tutorial no Youtube postado por Sebastian Lague
/// </summary>
/// 
namespace Caapora.Pathfinding {

    public class GoToPlace : Pathfinding {

        private IsoObject seekerIso;
        private Vector3 _targetPos;
        private bool _start = false;
        private bool move = false, canStart = false;
        public IEnumerator updatePosition;
        public IEnumerator animatePath;


        public Vector3 targetPos
        {


            get { return _targetPos; }
            set
            {
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
            if (_start)
            {
                Debug.Log("Habilitou o move pelo _start");
                move = true;
                canStart = true;
            }
            // enquanto nao inicia deixa as posiçőes originais
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
                if (canStart)
                    AnimatePath();
            }
        }


        void AnimatePath()
        {
            move = false;
            click = false;

            Vector3 currentPos = seekerIso.position;

            Debug.Log("ANIMATING PATH");

            if (canStart)
            {
                updatePosition = UpdatePosition(currentPos, grid.path[0], 0);
                StartCoroutine(updatePosition);
            }

        }




        IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)
        {

            float t = 0.0f;
            // Vector3 correctedPathPos = new Vector3(n.GetWorldPos().x, 1, n.GetWorldPos().z);

            // năo sei porque, mas foi necessário fazer a coversăo
            // foi necessário criar uma variável apenas para essa finalizade para năo sobrescrever a posiçăo original
            Vector3 tmpWorldPosition = seekerIso.isoWorld.ScreenToIso(n.worldPosition);



            Vector3 correctedPathPos = tmpWorldPosition;

            while (t < 0.5f)
            {
                t += Time.deltaTime;


                seekerIso.position = Vector3.Lerp(currentPos, correctedPathPos, t);


                PlayerBehavior.stopWalking = false;
                // Apenas para o caipora, seta a posiçăo anterior para movimentaçăo automática
                PlayerBehavior.prevPosition = currentPos;
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

                PlayerBehavior.stopWalking = true;
                Debug.Log("UpdatePositio finalizado");
                canStart = false;
                StopCoroutine(updatePosition);


            }

        }


    } // end Pathfinding 
} // end namespace Pathfinding