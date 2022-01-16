using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Spawner : MonoBehaviour
{

    public GameObject obj;
    public float timeToSpawn;
    private float currentTimeToSpawn;
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
        //SpawnObject();
        //timeToSpawn = audioSource.GetSpectrumData()

       if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;

        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    public void SpawnObject()
    {
        Instantiate(obj, transform.position, transform.rotation);

        //Código abaixo para alterar o objeto de acordo com o tom (esse altera escala de objeto, temos que alterar para fazer spawnar os pontos)
        /*foreach (Transform obj in objectsReactingToBasses)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(0, 7, 10), 1), t);
        }
        foreach (Transform obj in objectsReactingToNB)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(7, 15, 100), 1), t);
        }
        foreach (Transform obj in objectsReactingToMiddles)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(15, 30, 200), 1), t);
        }
        foreach (Transform obj in objectsReactingToHighs)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(30, 32, 1000), 1), t);
        }*/
    }
}
