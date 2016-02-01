using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoTools;
using System;

namespace Caapora.Pathfinding {

    public class NPC : Pathfinding {


        public IEnumerator updatePosition;
        public IEnumerator animatePath;

        private IsoObject seekerIso;
        private IsoRigidbody seekerRigidybody;
        private IsoObject _targetIsoObject;
        private IsoWorld world;
        private NPCBase NPCCharacter;
        public GameObject _targetPos;

        public float stepTicks = 0.5f;
        public float stepRndTicks = 0.5f;


        public int time;  
        private int MinDistance;

        private bool move, canStart;
        public bool npc_start;
        public bool IsNear;
        private bool MoveTiming;


      

        void Start()
        {


            IsNear = false;
            move = false;
            canStart = true;
            npc_start = true;
            MoveTiming = true;

            NPCCharacter = GetComponent<NPCBase>();
            seekerIso = GetComponent<IsoObject>();
            seekerRigidybody = GetComponent<IsoRigidbody>();

            cachedSeekerPos = seekerIso.position;
            cachedTargetPos = _targetPos.GetComponent<IsoObject>().position;

            _targetIsoObject = _targetPos.GetComponent<IsoObject>();

            world = GameObject.Find("Camera").GetComponent<IsoWorld>();


        }


        void Update()
        {
            MinDistance = 18;

            if (npc_start)
            {
                StartCoroutine(enableNPCTimer());


                if (!move)
                    find();

                AnimatePath();

               

            }

     

           // if (grid.path.Count < MinDistance && grid.path.Count > 1)
           //     canStart = false;

          //  if (IsTargetNear())
          //      IsNear = true; 

        }


        public bool IsTargetNear()
        {

            if (grid.path == null)
                return false;

            return grid.path.Count < 3 && grid.path.Count > 1;
        }

        void find()
       {


               if (cachedSeekerPos != seekerIso.position)
                {
                    cachedSeekerPos = seekerIso.position;
                    FindPath(seekerIso.position, _targetIsoObject.position);
                }
                if (cachedTargetPos != _targetIsoObject.position)
                {
                    cachedTargetPos = _targetIsoObject.position;
                    FindPath(seekerIso.position, _targetIsoObject.position);
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
            return new WaitForSeconds(stepTicks + UnityEngine.Random.Range(0.0f, stepRndTicks));
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

            move = false;
                
            Vector3 SeekerCurrentPos = seekerIso.position;


            if (canStart)
            {
                if(grid.path != null) { 
                 updatePosition = UpdatePosition(SeekerCurrentPos, grid.path[0], 0);
                 StartCoroutine(updatePosition);
                }
            }

        }




        IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)
        {

           
            float t = 0.0f;

            Vector3 tmpWorldPosition  = world.ScreenToIso(n.worldPosition);

            Vector3 correctedPathPos = tmpWorldPosition;

            //seekerIso.position =   Vector3.Lerp(currentPos, correctedPathPos, t);


            while (t < 1f)
            {
                t += Time.deltaTime;

                var Velocity = currentPos - correctedPathPos;

                float dotUp = Vector3.Dot(Velocity, Vector3.down);
                float dotRight = Vector3.Dot(Velocity, Vector3.left);


                if (MoveTiming != false)
                    StartCoroutine(NPCMovement(dotRight, dotUp));

                NPCCharacter.stopWalking = false;

                NPCCharacter.prevPosition = currentPos;


                yield return null; 

            }
                //  seekerIso.position = correctedPathPos;

     
            currentPos = correctedPathPos;
        

            index++;

           
            if (index < grid.path.Count)
            {


                StartCoroutine(UpdatePosition(currentPos, grid.path[index], index));
        
            }
               
            else
            {

                move = false;
                npc_start = false;
                StopCoroutine(updatePosition);
                               

            }
                
        }

        private IEnumerator NPCMovement(float dotRight, float dotUp)
        {
  

            MoveTiming = false;

            if (dotUp > 0)
            {
               GetComponent<NPCBase>().moveUp();
           
            }
            else if (dotUp < 0)
            {
                GetComponent<NPCBase>().moveDown();
             
            }
            else if (dotRight > 0)
            {
                GetComponent<NPCBase>().moveRight();
           
            }
            else if (dotRight < 0)
            {
              GetComponent<NPCBase>().moveLeft();
               
            }


            yield return new WaitForSeconds(0.1f);

            MoveTiming = true;

        }

        public Vector3 targetPos
        {


            get { return _targetIsoObject.position; }
            set
            {
                _targetIsoObject.position = value;
            }
        }




    } // end Pathfinding 
} // end namespace Pathfinding