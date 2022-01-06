using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavgator : MonoBehaviour
{
    NavMeshAgent nav; // Objeto necessário para navegação de IA do Unity

    public GameObject caminhoEsquerda; // Referência para o caminho da esquerda
    public GameObject caminhoCentroEsquerda; // Referência para o caminho da esqueda centro
    public GameObject caminhoCentroDireita; // Referência para o caminho da direita centro
    public GameObject caminhoDireita; // Referência para o caminho da direita
    private int _caminhoAtual = 0; // Índice que controla em qual pista o carro está
    public int indiceCaminho; // Índice que controla em qual ponto da pista o carro está (usado como referência para quando é necessário trocar de pista)
    
    public GameObject fantasma; // Referência para o objeto que irá fazer o spawn das notas musicais
    private NavMeshPath pathParaOFantasma;

    public float smoothTime = 0.1F;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        nav.updateRotation = false;          //< let us control the rotation explicitly
        pathParaOFantasma = new NavMeshPath(); // Inicializa o Path entre o carro e o fantasma
    }

    protected Vector3 GetTerrainNormal() {
        RaycastHit hit;
        Vector3 terrainLocalPos = transform.position;
        Physics.Raycast(transform.position, Vector3.down, out hit, 2f, 1 << LayerMask.NameToLayer("Ground"));
        return hit.normal;
    }

    // Update is called once per frame
    void Update() 
    {
        ProcessaApertoDeTela();

        // Controle da distância entre o Fantasma e o carro
        NavMesh.CalculatePath(transform.position, fantasma.transform.position, NavMesh.AllAreas, pathParaOFantasma);

        float distancia = 0f;
        for (int i = 0; i < pathParaOFantasma.corners.Length - 1; i++)
        {
            distancia += Vector3.Distance(pathParaOFantasma.corners[i], pathParaOFantasma.corners[i + 1]);
            Debug.DrawLine(pathParaOFantasma.corners[i], pathParaOFantasma.corners[i + 1], Color.red);
        }
        Debug.Log(distancia);
        nav.speed = distancia / 40f * 15f;


        Vector3 normal_v = GetTerrainNormal();
        
        // Mostra a normal do terreno
        ///print(normal_v);
        Debug.DrawRay(transform.position, normal_v, Color.yellow);

        //transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, (Quaternion.FromToRotation(transform.up, normal_v) * transform.rotation).eulerAngles, ref velocity, smoothTime));
        transform.rotation = Quaternion.FromToRotation(transform.up, normal_v) * transform.rotation;

        transform.LookAt(nav.destination);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nav.destination - transform.position), 1.5f);
    }

    private void ProcessaApertoDeTela()
    {
        if (Input.GetKeyDown("left") && _caminhoAtual > 0)
        {
            //Debug.Log(_caminhoAtual);
            switch (_caminhoAtual)
            {
                case 1:
                    caminhoEsquerda.SetActive(true);
                    caminhoCentroEsquerda.SetActive(false);
                    caminhoCentroDireita.SetActive(false);
                    caminhoDireita.SetActive(false);
                    nav.destination = caminhoEsquerda.transform.GetChild(indiceCaminho).position;
                    break;

                case 2:
                    caminhoEsquerda.SetActive(false);
                    caminhoCentroEsquerda.SetActive(true);
                    caminhoCentroDireita.SetActive(false);
                    caminhoDireita.SetActive(false);
                    nav.destination = caminhoCentroEsquerda.transform.GetChild(indiceCaminho).position;
                    break;

                case 3:
                    caminhoEsquerda.SetActive(false);
                    caminhoCentroEsquerda.SetActive(false);
                    caminhoCentroDireita.SetActive(true);
                    caminhoDireita.SetActive(false);
                    nav.destination = caminhoCentroDireita.transform.GetChild(indiceCaminho).position;
                    break;

                default:
                    break;

            }
            _caminhoAtual--;
        }

        if (Input.GetKeyDown("right") && _caminhoAtual < 3)
        {
            //Debug.Log(_caminhoAtual);
            switch (_caminhoAtual)
            {
                case 0:
                    caminhoEsquerda.SetActive(false);
                    caminhoCentroEsquerda.SetActive(true);
                    caminhoCentroDireita.SetActive(false);
                    caminhoDireita.SetActive(false);
                    nav.destination = caminhoCentroEsquerda.transform.GetChild(indiceCaminho).position;
                    break;

                case 1:
                    caminhoEsquerda.SetActive(false);
                    caminhoCentroEsquerda.SetActive(false);
                    caminhoCentroDireita.SetActive(true);
                    caminhoDireita.SetActive(false);
                    nav.destination = caminhoCentroDireita.transform.GetChild(indiceCaminho).position;
                    break;

                case 2:
                    nav.destination = caminhoDireita.transform.GetChild(indiceCaminho).position;
                    caminhoEsquerda.SetActive(false);
                    caminhoCentroEsquerda.SetActive(false);
                    caminhoCentroDireita.SetActive(false);
                    caminhoDireita.SetActive(true);
                    break;

                default:
                    break;

            }
            _caminhoAtual++;
        }
    }

}
