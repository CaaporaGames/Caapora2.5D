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
        private CharacterBase Character;
        private IsoWorld world;


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


            Character = GetComponent(typeof(CharacterBase)) as CharacterBase;

           
            seekerIso = GetComponent<IsoObject>();
            world = GameObject.Find("Camera").GetComponent<IsoWorld>();

            cachedSeekerPos = seekerIso.position;
            cachedTargetPos = _targetPos;

        }



        void Update()
        {
            

            if (_start)
            {
               
                move = true;
                canStart = true;
            }
          
            if (!move && canStart)
            {


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

         
            if (canStart)
            {

                updatePosition = UpdatePosition(currentPos, grid.path[0], 0);
                StartCoroutine(updatePosition);
            }

        }




        IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)
        {

            float t = 0.0f;
           
            Vector3 tmpWorldPosition = world.ScreenToIso(n.worldPosition);

            Vector3 correctedPathPos = tmpWorldPosition;

            while (t < 1f)
            {
                t += Time.deltaTime;

                seekerIso.position = Vector3.Lerp(currentPos, correctedPathPos, t);

                Character.stopWalking = false;
 
                Character.prevPosition = currentPos;


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
             
                Character.stopWalking = true;
                canStart = false;
                StopCoroutine(updatePosition);


            }

        }


    } // end Pathfinding 
} // end namespace Pathfinding