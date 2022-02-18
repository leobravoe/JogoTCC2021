using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomDano : MonoBehaviour
{
    public AudioClip som;
    AudioSource audio;
    public float volume;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void onTriggerEnter()
    {
        audio.PlayOneShot(som, volume);
    }
}
