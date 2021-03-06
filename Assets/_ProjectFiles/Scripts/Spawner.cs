using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Spawner : MonoBehaviour, AudioProcessor.AudioCallbacks
{

    public GameObject ponto;
    public GameObject obstaculo;

    public GameObject pontoSpawnParticle;
    public GameObject obstaculoSpawnParticle;

    //public float delay = 1f;

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
        // Quando for realizar um spawn de um ponto, verifica se na regi?o existe um Obstaculo. Caso exista um obstaculo, o ponto n?o ser? criado.
        RaycastHit[] objetosEmVolta;
        int i;
        objetosEmVolta = Physics.SphereCastAll(transform.position, 1.0f, transform.forward, Mathf.Infinity);
        //Debug.Log(objetosEmVolta);
        for (i = 0; i < objetosEmVolta.Length && objetosEmVolta[i].collider.tag != "Obstaculo"; i++) { }

        // Se n?o existir objetos com a tag "Obstaculo" em volta
        if (i == objetosEmVolta.Length)
        {
            Destroy(Instantiate(ponto, transform.position + new Vector3(0f, 1f, 0f), ponto.transform.rotation), 20);
            Destroy(Instantiate(pontoSpawnParticle, transform.position, transform.rotation), 1);
        }
            
    }

    public void SpawnObstaculo()
    {
        // Quando for realizar um spawn de um obst?culo, verifica se na regi?o existe um ponto. Caso exista um ponto, o obst?culo n?o ser? criado.
        RaycastHit [] objetosEmVolta;
        int i;
        objetosEmVolta = Physics.SphereCastAll(transform.position, 1.0f, transform.forward, Mathf.Infinity);
        //Debug.Log(objetosEmVolta);
        for (i = 0; i < objetosEmVolta.Length && objetosEmVolta[i].collider.tag != "Ponto"; i++){}
        
        // Se n?o existir objetos com a tag "Ponto" em volta
        if(i == objetosEmVolta.Length)
        {
            Destroy(Instantiate(obstaculo, transform.position + new Vector3(0f, 1f, 0f), transform.rotation), 20);
            Destroy(Instantiate(obstaculoSpawnParticle, transform.position, transform.rotation), 1);
        }
            
    }
}
