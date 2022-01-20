using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParaInicioDaMusica : MonoBehaviour
{
    //public AudioSource playerMusicManagerAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Music Start");
            other.GetComponent<AudioSource>().Play();
            //playerMusicManagerAudioSource.Play();
        }
    }
}
