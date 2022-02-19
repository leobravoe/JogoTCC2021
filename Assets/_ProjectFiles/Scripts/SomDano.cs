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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.PlayOneShot(som, volume);
            Debug.Log("Colisão");
        }
    }
}
