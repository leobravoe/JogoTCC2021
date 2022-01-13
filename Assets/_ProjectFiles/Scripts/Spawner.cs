using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    //public List<Transform> objectsReactingToBasses, objectsReactingToNB, objectsReactingToMiddles, objectsReactingToHighs;
    //[SerializeField] float t = 0.1f;

    public GameObject obj;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
