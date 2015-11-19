using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour
{

    /// <summary>
    /// The sound source that will be played when the effect is started.
    /// </summary>
    public AudioSource soundSource;

    /// <summary>
    /// The sound clips that will randomly be played if there is more than 1.
    /// </summary>
    public AudioClip[] soundClips;

    /// <summary>
    /// The length of the effectin seconds.
    /// </summary>
    public float effectLength = 1f;

    /// <summary>
    /// Should the effect be pooled after its completed.
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
        soundSource.PlayOneShot(soundClips[Random.Range(0, soundClips.Length)]);

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