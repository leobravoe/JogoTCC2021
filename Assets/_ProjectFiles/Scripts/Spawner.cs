using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Spawner : MonoBehaviour, AudioProcessor.AudioCallbacks
{

    public GameObject ponto;
    public GameObject obstaculo;
    public float timeToSpawnObst;
    private float currentTimeToSpawnObst;

    // Start is called before the first frame update
    void Start()
    {
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.addAudioCallback(this);
    }

    public void onOnbeatDetected()
    {
        Debug.Log("Beat!!!");
        SpawnPonto();
    }

    public void onSpectrum(float[] spectrum)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeToSpawnObst > 0)
        {
            currentTimeToSpawnObst -= Time.deltaTime;

        }
        else
        {
            SpawnObstaculo();
            currentTimeToSpawnObst = timeToSpawnObst;
        }
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
