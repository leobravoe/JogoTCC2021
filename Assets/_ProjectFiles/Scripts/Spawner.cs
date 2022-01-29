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
        // Quando for realizar um spawn de um ponto, verifica se na região existe um Obstaculo. Caso exista um obstaculo, o ponto não será criado.
        RaycastHit[] objetosEmVolta;
        int i;
        objetosEmVolta = Physics.SphereCastAll(transform.position, 1.0f, transform.forward, Mathf.Infinity);
        //Debug.Log(objetosEmVolta);
        for (i = 0; i < objetosEmVolta.Length && objetosEmVolta[i].collider.tag != "Obstaculo"; i++) { }

        // Se não existir objetos com a tag "Obstaculo" em volta
        if (i == objetosEmVolta.Length)
            Instantiate(ponto, transform.position, transform.rotation);
    }

    public void SpawnObstaculo()
    {
        // Quando for realizar um spawn de um obstáculo, verifica se na região existe um ponto. Caso exista um ponto, o obstáculo não será criado.
        RaycastHit [] objetosEmVolta;
        int i;
        objetosEmVolta = Physics.SphereCastAll(transform.position, 1.0f, transform.forward, Mathf.Infinity);
        //Debug.Log(objetosEmVolta);
        for (i = 0; i < objetosEmVolta.Length && objetosEmVolta[i].collider.tag != "Ponto"; i++){}
        
        // Se não existir objetos com a tag "Ponto" em volta
        if(i == objetosEmVolta.Length)
            Instantiate(obstaculo, transform.position, transform.rotation);
    }
}
