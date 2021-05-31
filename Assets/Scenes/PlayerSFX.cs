using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource sneezeAudioSource;
    public AudioSource jemiAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSneezeAudioSource()
    {
        //sneezeAudioSource.Play();
        sneezeAudioSource.PlayOneShot(sneezeAudioSource.clip, 1f);
    }

    public void playJemiAudioSource()
    {
        //jemiAudioSource.Play();
        jemiAudioSource.PlayOneShot(jemiAudioSource.clip, 1f);
    }
}
