using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNext : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // Se existir um NavMeshAgent dentro do objeto que colidiu
        if (other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>())
        {
            // Pego a referência do NavMeshAgent dentro do objeto que colidiu e armazeno em "nav"
            UnityEngine.AI.NavMeshAgent nav = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if ( gameObject.transform.parent.name.Contains("Fantasma") && other.name.Contains("Fantasma") )
            {
                // Seto o destino como a posição do próximo objeto
                nav.destination = NextChild().position;
                other.gameObject.GetComponent<FantasmaNavgator>().indiceCaminho = this.transform.GetSiblingIndex() + 1;
            }
            if(!gameObject.transform.parent.name.Contains("Fantasma") && other.name.Contains("Player"))
            {
                // Seto o destino como a posição do próximo objeto
                nav.destination = NextChild().position;
                other.gameObject.GetComponent<PlayerNavgator>().indiceCaminho = this.transform.GetSiblingIndex() + 1;
            }
            
        }
    }

    // Update is called once per frame
    private Transform NextChild()
    {
        //Checa onde o veículo está
        int thisIndex = this.transform.GetSiblingIndex();

        
        if (this.transform.parent == null)
            return null;
        if (this.transform.parent.childCount <= thisIndex + 1)
            return this.transform.parent.GetChild(0);

        //Retorna o próiximo nodo de Pathfinding
        return this.transform.parent.GetChild(thisIndex + 1);
    }
}
