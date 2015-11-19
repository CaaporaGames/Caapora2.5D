using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour
{
    /// <summary>
    /// The array of emitters to fire when the effect starts.
    /// </summary>
    public ParticleEmitter[] emitters;

    /// <summary>
    /// The length of the effect in seconds.  After which the effect will be reset and pooled if needed.
    /// </summary>
    public float effectLength = 1f;


    /// <summary>
    /// Should the effect be added to the effects pool after completion.
    /// </summary>
    public bool poolAfterComplete = true;



    /// <summary>
    /// Resets the effect.
    /// </summary>
    public virtual void ResetEffect()
    {
        if (poolAfterComplete)
        {
            ObjectPool.instance.PoolObject(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Starts the effect.
    /// </summary>
    public virtual void StartEffect()
    {
        foreach (ParticleEmitter emitter in emitters)
        {
            emitter.Emit();
        }

        StartCoroutine(WaitForCompletion());
    }

    public IEnumerator WaitForCompletion()
    {
        //Wait for the effect to complete itself
        yield return new WaitForSeconds(effectLength);

        //Reset the now completed effect
        ResetEffect();

    }



}
