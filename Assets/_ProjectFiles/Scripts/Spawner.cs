using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Spawner : MonoBehaviour
{
    
    //public List<Transform> objectsReactingToBasses, objectsReactingToNB, objectsReactingToMiddles, objectsReactingToHighs;
    //[SerializeField] float t = 0.1f;

    public GameObject obj;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    public AudioSource audioSource;
    //private float[] tuneList = new float[4];

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CoroutineParaSpawnar());
    }

    /*IEnumerator CoroutineParaSpawnar()
    {
        while (true)
        {
            // Espera para spawnar
            yield return new WaitForSeconds(0.2);

            // Trocar de pista (usando a tonalidade do momento)
            tuneList[0] = MusicManager.instance.getFrequenciesDiapason(0, 5, 10);
            tuneList[1] = MusicManager.instance.getFrequenciesDiapason(5, 10, 100);
            tuneList[2] = MusicManager.instance.getFrequenciesDiapason(10, 20, 200);
            tuneList[3] = MusicManager.instance.getFrequenciesDiapason(20, 32, 1000);

            int position = Array.IndexOf(tuneList, Mathf.Max(tuneList));

            if (position == 0)
            {
                if (tuneList[0] >= 4) {
                    SpawnObject();
                }
            }
            else if (position == 1)
            {
                if (tuneList[1] >= 8)
                {
                    SpawnObject();
                }
            }
            else if (position == 2)
            {
                if (tuneList[2] >= 15)
                {
                    SpawnObject();
                }
            }
            else
            {
                if (tuneList[3] >= 25)
                {
                    SpawnObject();
                }
            }
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
