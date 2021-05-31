using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public AudioSource hitAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playHitAudioSource()
    {
        //hitAudioSource.Play();
        hitAudioSource.PlayOneShot(hitAudioSource.clip, 1);
    }

    public void play()
    {
        //hitAudioSource.Play();
        hitAudioSource.Play();
    }
}
