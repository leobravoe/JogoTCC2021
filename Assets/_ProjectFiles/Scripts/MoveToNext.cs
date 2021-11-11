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
        if (other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>())
        {
            // Pego a referência do NavMeshAgent dentro do objeto que colidiu e armazeno em "nav"
            UnityEngine.AI.NavMeshAgent nav = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            // Seto o destino como a posição do próximo objeto
            nav.destination = NextChild().position;
        }
    }

    // Update is called once per frame
    private Transform NextChild()
    {
        // Check where we are
        int thisIndex = this.transform.GetSiblingIndex();

        // We have a few cases to rule out
        if (this.transform.parent == null)
            return null;
        if (this.transform.parent.childCount <= thisIndex + 1)
            return this.transform.parent.GetChild(0);

        // Then return whatever was next, now that we're sure it's there
        return this.transform.parent.GetChild(thisIndex + 1);
    }
}
