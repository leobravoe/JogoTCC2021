using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Spawner : MonoBehaviour, AudioProcessor.AudioCallbacks
{

    public GameObject ponto;
    public GameObject obstaculo;

    // Start is called before the first frame update
    void Start()
    {
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.addAudioCallback(this);
    }

    public void onOnbeatDetected()
    {
        float num = UnityEngine.Random.Range(1, 10);
        if (num <= 2)
        {
            SpawnObstaculo();
        }
        else
        {
            SpawnPonto();
        }
        
    }

    public void onSpectrum(float[] spectrum)
    {

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void SpawnPonto()
    {
        Instantiate(ponto, transform.position, transform.rotation);
    }

    public void SpawnObstaculo()
    {
        Instantiate(obstaculo, transform.position, transform.rotation);
    }
}
