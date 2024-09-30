using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sounds;
    public static AudioManager inst = null;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }else if(inst != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        sounds.clip = clip;
        sounds.Play();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
