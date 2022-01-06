using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FantasmaNavgator : MonoBehaviour
{
    NavMeshAgent nav; // Objeto necessário para navegação de IA do Unity

    public GameObject caminhoEsquerda; // Referência para o caminho da esquerda
    public GameObject caminhoCentroEsquerda; // Referência para o caminho da esqueda centro
    public GameObject caminhoCentroDireita; // Referência para o caminho da direita centro
    public GameObject caminhoDireita; // Referência para o caminho da direita
    private int _caminhoAtual = 0; // Índice que controla em qual pista o carro está
    public int indiceCaminho; // Índice que controla em qual ponto da pista o carro está (usado como referência para quando é necessário trocar de pista)

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(CoroutineParaTrocaDePista());
    }

    IEnumerator CoroutineParaTrocaDePista() {
        while (true)
        {
            //yield on a new YieldInstruction that waits for 2 seconds.
            yield return new WaitForSeconds(2);

            // Calcula um numero aleatório
            float numeroAleatorio = Random.Range(0f, 100.0f);

            if (numeroAleatorio < 25)
            {
                caminhoEsquerda.SetActive(true);
                caminhoCentroEsquerda.SetActive(false);
                caminhoCentroDireita.SetActive(false);
                caminhoDireita.SetActive(false);
                nav.destination = caminhoEsquerda.transform.GetChild(indiceCaminho).position;
            }
            else if (numeroAleatorio >= 25 && numeroAleatorio < 50)
            {
                caminhoEsquerda.SetActive(false);
                caminhoCentroEsquerda.SetActive(true);
                caminhoCentroDireita.SetActive(false);
                caminhoDireita.SetActive(false);
                nav.destination = caminhoCentroEsquerda.transform.GetChild(indiceCaminho).position;
            }
            else if (numeroAleatorio >= 50 && numeroAleatorio < 75)
            {
                caminhoEsquerda.SetActive(false);
                caminhoCentroEsquerda.SetActive(false);
                caminhoCentroDireita.SetActive(true);
                caminhoDireita.SetActive(false);
                nav.destination = caminhoCentroDireita.transform.GetChild(indiceCaminho).position;
            }
            else
            {
                caminhoEsquerda.SetActive(false);
                caminhoCentroEsquerda.SetActive(false);
                caminhoCentroDireita.SetActive(false);
                caminhoDireita.SetActive(true);
                nav.destination = caminhoDireita.transform.GetChild(indiceCaminho).position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        


    }

}
