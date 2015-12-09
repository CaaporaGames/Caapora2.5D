using UnityEngine;
using IsoTools;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class BasicStates{
	
	public float startLife;
	public float startMana;
	public int strenght;
	public int magic;
	public int agillity;
	public int baseDefence;
	public int baseAttack;

}

namespace Caapora
{
    public abstract class CreatureBase : MonoBehaviour, ICreature
    {

        protected float _life = 1000;
        public int currentLevel;
        public BasicStates basicStats;
        public float speed = 5f;
        protected Animator _animator;
        private float collisionTime = 0;


        /// *************************************************************************
        /// Author: Rômulo Lima
        /// <summary> 
        /// Possui Todas as condição para dar GameOver 
        /// </summary>
        public void Die()
        {
  
            if (gameObject.GetComponent<IsoObject>().positionZ < -15 || _life <= 0)
            {

                Destroy(gameObject);

            }

        }

        public virtual void Update()
        {
            collisionTime += Time.deltaTime;

            UpdateBar();

            if (_life > 1000)
                _life = 1000;

            Die();
        }

        void UpdateBar()
        {

            GetComponentInChildren<Scrollbar>().size = _life / 1000;

        }

        public virtual void OnIsoCollisionStay(IsoCollision iso_collision)
        {

            
            collisionTime = 0;

            if (iso_collision.gameObject.name == "Altar")
            {
                
                _life = _life + 10;
                
            }


            if (iso_collision.gameObject.name == "chamas" || iso_collision.gameObject.name == "chamasSemSpread")
            {

                _life = _life - iso_collision.gameObject.GetComponent<Fire>().GetDamage();

                StartCoroutine(CharacterHit());

            }

            

        }


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


        public Animator animator
        {

            get
            {
                return _animator;
            }
        }

    }
}