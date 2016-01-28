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
        private Image LifeBar;
        public GameObject CBTPrefab;
        private bool inFire = false;
        private float TempDemage = 0;
        private float AnimationDelay = 0.5f;
        private bool CanAnimate = true;

        protected abstract IsoRigidbody iso_rigidyBody { get; set; }
        protected abstract IsoObject iso_object { get; set; }

        private float collisionTime = 0;


    
  
        protected void Start()
        {
            iso_object = gameObject.GetComponent<IsoObject>();

            if (transform.Find("healthBar/LifeBar") != null)
                 LifeBar = transform.Find("healthBar/LifeBar").GetComponent<Image>();
        
        }

        public virtual void Update()
        {
            collisionTime = Time.deltaTime;

            if (inFire && CanAnimate)
            {
                
                StartCoroutine(ApplyDamage());
            }


            if (LifeBar != null)
                UpdateBar();

            if (_life > 1000)
                _life = 1000;

            Die();
        }

        void UpdateBar()
        {

           LifeBar.fillAmount = _life / 1000;

        }

        public void Die()
        {

            if (iso_object.positionZ < -15 || _life <= 0)
            {

                Destroy(gameObject);

            }

        }


        public virtual void OnIsoCollisionEnter(IsoCollision iso_collision)
        {


            collisionTime = 0;

            if (iso_collision.gameObject.name == "Altar")
            {
                
                _life = _life + 10;
                
            }


            if (iso_collision.gameObject.tag == "Flame")
            {
       
                TempDemage = iso_collision.gameObject.GetComponent<Fire>().GetDamage();

                inFire = true;
            }

            

        }


        public virtual void OnIsoCollisionExit(IsoCollision iso_collision)
        {

            inFire = false;

        }


        public void Hit(float damage)
        {
          
            TempDemage = damage;

            if(CanAnimate)
                StartCoroutine(ApplyDamage());

        }


        private IEnumerator ApplyDamage()
        {

            CanAnimate = false;
  
            _life = _life - TempDemage;

   
            InitCBT(TempDemage.ToString());

            StartCoroutine(CharacterHit());

            yield return new WaitForSeconds(AnimationDelay);

            CanAnimate = true;
        }



            /*
            public virtual void OnIsoTriggerStay(IsoCollider iso_collider)
            {


                    if (iso_collider.gameObject.name == "Altar")
                    {

                        _life = _life + 10;

                    }


                    if (iso_collider.gameObject.tag == "Flame")
                    {
                        float demageTemp = iso_collider.gameObject.GetComponent<Fire>().GetDamage();

                        _life = _life - demageTemp;

                        InitCBT(demageTemp.ToString());

                        StartCoroutine(CharacterHit());

                    }



            }*/

            void InitCBT(string text)
        {
            GameObject temp = Instantiate(CBTPrefab) as GameObject;
            RectTransform tempRect = temp.GetComponent<RectTransform>();
            temp.transform.SetParent(transform.FindChild("healthBar"));

            
            tempRect.transform.localPosition = CBTPrefab.transform.localPosition;
            tempRect.transform.localRotation = CBTPrefab.transform.localRotation;
            tempRect.transform.localScale = CBTPrefab.transform.localScale;

            temp.GetComponent<Text>().text = text;
            temp.GetComponent<Animator>().SetTrigger("Hit");


            Destroy(temp.gameObject,2);


        }

        public IEnumerator CharacterHit()
        {

            float t = 0.0f;

       
            while (t < 1f)
            {
                t += Time.deltaTime;

                GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, t);
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