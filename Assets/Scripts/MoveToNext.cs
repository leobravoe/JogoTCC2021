using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNext : MonoBehaviour
{
    public GameObject next;
    // Start is called before the first frame update
    void Start()
    {
        //print("teste");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se existir um NavMeshAgent dentro do objeto que colidiu
        if(other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>())
        {
            // Pego a referência do NavMeshAgent dentro do objeto que colidiu e armazeno em "nav"
            UnityEngine.AI.NavMeshAgent nav = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            // Seto o destino como a posição do próximo objeto
            nav.destination = next.transform.position;
            print("Destino:" + nav.destination);
        }
    }
}
