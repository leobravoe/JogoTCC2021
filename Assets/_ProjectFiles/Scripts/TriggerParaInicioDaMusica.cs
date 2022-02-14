using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParaInicioDaMusica : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Tocando Música no Player...");
            other.GetComponent<AudioSource>().Play();
        }
        
    }
}
