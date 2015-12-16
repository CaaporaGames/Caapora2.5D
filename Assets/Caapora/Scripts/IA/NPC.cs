using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;


/// <summary>
/// C�digo encontrado em tutorial no Youtube postado por Sebastian Lague
/// </summary>
namespace Caapora.Pathfinding {

    public class NPC : Pathfinding {
        private bool move = true, canStart = true;
        public IEnumerator updatePosition;
        public IEnumerator animatePath;
        private IsoObject seekerIso;
        public GameObject _targetPos;
        private NPCBase NPCCharacter;
        public float stepTicks = 0.5f;
        public float stepRndTicks = 0.5f;
        public  bool npc_start = true;
        private IsoWorld world;
        public int time = 7;


      

        void Start()
        {

            NPCCharacter = GetComponent<NPCBase>();
            seekerIso = GetComponent<IsoObject>();
            cachedSeekerPos = seekerIso.position;
            cachedTargetPos = _targetPos.GetComponent<IsoObject>().position;
            world = GameObject.Find("Camera").GetComponent<IsoWorld>();


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
            if (npc_start) {

                if (!move)
                     find();
               
                AnimatePath();

           }

            if (npc_start == true)
            {
                StartCoroutine(enableNPCTimer());
            }

        }


        IEnumerator enableNPCTimer()
        {
            npc_start = false;
            yield return new WaitForSeconds(time);
            npc_start = true;

        }


        WaitForSeconds RndWait()
        {
            return new WaitForSeconds(stepTicks + Random.Range(0.0f, stepRndTicks));
        }

        IEnumerator Move()
        {
            var iso_object = GetComponent<IsoObject>();
            if (iso_object)
            {
                for (;;)
                {
                    yield return RndWait();
                    iso_object.position += new Vector3(1, 0, 0);
                    yield return RndWait();
                    iso_object.position += new Vector3(0, 1, 0);
                    yield return RndWait();
                    iso_object.position += new Vector3(-1, 0, 0);
                    yield return RndWait();
                    iso_object.position += new Vector3(0, -1, 0);
                }
            }
        }

        void AnimatePath()
        {
            // Enquanto estiver animando para de checar a posi��o
            move = false;
                
            // Posicao em formato isometrico
            Vector3 SeekerCurrentPos = seekerIso.position;


            if (canStart)
            {
                updatePosition = UpdatePosition(SeekerCurrentPos, grid.path[0], 0);
                StartCoroutine(updatePosition);
            }

        }




        IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)
        {
   
            float t = 0.0f;
            // Vector3 correctedPathPos = new Vector3(n.GetWorldPos().x, 1, n.GetWorldPos().z);
            // n�o sei porque, mas foi necess�rio fazer a covers�o
            // foi necess�rio criar uma vari�vel apenas para essa finalizade para n�o sobrescrever a posi��o original

            Vector3 tmpWorldPosition  = world.ScreenToIso(n.worldPosition);

            Vector3 correctedPathPos = tmpWorldPosition;

            while (t < 1f)
            {
                t += Time.deltaTime;
          
                seekerIso.position =   Vector3.Lerp(currentPos, correctedPathPos, t);

                NPCCharacter.stopWalking = false;

                NPCCharacter.prevPosition = currentPos;

                yield return null;
            }


            seekerIso.position = correctedPathPos;

     
            currentPos = correctedPathPos;
        

            index++;
            if (index < grid.path.Count)
            {
               
                  updatePosition = UpdatePosition(currentPos, grid.path[index], index);

                  StartCoroutine(updatePosition);

                  grid.path.Remove(grid.path[index]);

       
            }
               
            else
            {

                move = false;
                npc_start = false;
                StopCoroutine(updatePosition);
                               

            }
                
        }


        public Vector3 targetPos
        {


            get { return _targetPos.GetComponent<IsoObject>().position; }
            set
            {
                _targetPos.GetComponent<IsoObject>().position = value;
            }
        }




    } // end Pathfinding 
} // end namespace Pathfinding