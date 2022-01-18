using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Spawner : MonoBehaviour
{

    public GameObject ponto;
    public GameObject obstaculo;
    public float timeToSpawnPonto;
    private float currentTimeToSpawnPonto;
    public float timeToSpawnObst;
    private float currentTimeToSpawnObst;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        /*AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onOnbeatDetected.AddListener(onOnbeatDetected);
        processor.onSpectrum.AddListener(onSpectrum);*/
    }

    void onOnbeatDetected()
    {
        Debug.Log("Beat!!!");
    }

    /*void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
       if (currentTimeToSpawnPonto > 0) //Spawna Pontos
        {
            currentTimeToSpawnPonto -= Time.deltaTime;

        }
        else
        {
            SpawnPonto();
            currentTimeToSpawnPonto = timeToSpawnPonto;
        }

        if (currentTimeToSpawnObst > 0) //Spawna Obstaculos
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
