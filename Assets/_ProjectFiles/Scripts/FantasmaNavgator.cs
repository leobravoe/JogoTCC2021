using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;

public class FantasmaNavgator : MonoBehaviour
{
    NavMeshAgent nav; // Objeto necessário para navegação de IA do Unity

    public GameObject caminhoEsquerda; // Referência para o caminho da esquerda
    public GameObject caminhoCentroEsquerda; // Referência para o caminho da esqueda centro
    public GameObject caminhoCentroDireita; // Referência para o caminho da direita centro
    public GameObject caminhoDireita; // Referência para o caminho da direita
    private int _caminhoAtual = 0; // Índice que controla em qual pista o carro está
    public int indiceCaminho; // Índice que controla em qual ponto da pista o carro está (usado como referência para quando é necessário trocar de pista)

    private float[] tuneList = new float[4];

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(CoroutineParaTrocaDePista());
    }

    IEnumerator CoroutineParaTrocaDePista() {
        while (true)
        {
            // Espera para trocar de pista
            yield return new WaitForSeconds(0.5f);

            // Trocar de pista (usando a tonalidade do momento)
            tuneList[0] = MusicManager.instance.getFrequenciesDiapason(0, 5, 10);
            tuneList[1] = MusicManager.instance.getFrequenciesDiapason(5, 10, 100);
            tuneList[2] = MusicManager.instance.getFrequenciesDiapason(10, 20, 200);
            tuneList[3] = MusicManager.instance.getFrequenciesDiapason(20, 32, 1000);

            int position = Array.IndexOf(tuneList, Mathf.Max(tuneList));

            //Debug.Log("MAX: " + Mathf.Max(tuneList) + "position" + Array.IndexOf(tuneList, Mathf.Max(tuneList)));

            if (position == 0)
            {
                caminhoEsquerda.SetActive(true);
                caminhoCentroEsquerda.SetActive(false);
                caminhoCentroDireita.SetActive(false);
                caminhoDireita.SetActive(false);
                nav.destination = caminhoEsquerda.transform.GetChild(indiceCaminho).position;
            }
            else if (position == 1)
            {
                caminhoEsquerda.SetActive(false);
                caminhoCentroEsquerda.SetActive(true);
                caminhoCentroDireita.SetActive(false);
                caminhoDireita.SetActive(false);
                nav.destination = caminhoCentroEsquerda.transform.GetChild(indiceCaminho).position;
            }
            else if (position == 2)
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
