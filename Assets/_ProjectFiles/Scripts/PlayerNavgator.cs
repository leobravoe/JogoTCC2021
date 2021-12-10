using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavgator : MonoBehaviour
{
    NavMeshAgent nav;
    private NavMeshAgent agent;
    private Quaternion lookRotation;
    private float journeyLength;

    public GameObject caminhoEsquerda;
    public GameObject caminhoCentroEsquerda;
    public GameObject caminhoCentroDireita;
    public GameObject caminhoDireita;
    private int _caminhoAtual = 0;
    public int indiceCaminho;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        nav.updateRotation = false;          //< let us control the rotation explicitly
        lookRotation = transform.rotation;     //< set original rotation
    }

    protected Vector3 GetTerrainNormal() {
        RaycastHit hit;
        Vector3 terrainLocalPos = transform.position;
        Physics.Raycast(transform.position, Vector3.down, out hit, 2f, 1 << LayerMask.NameToLayer("Ground"));
        return hit.normal;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown("left") && _caminhoAtual > 0)
        {
            Debug.Log(_caminhoAtual);
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
            Debug.Log(_caminhoAtual);
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


        Vector3 normal_v = GetTerrainNormal();
        
        // Mostra a normal do terreno
        ///print(normal_v);
        Debug.DrawRay(transform.position, normal_v, Color.yellow);

        transform.rotation = Quaternion.FromToRotation(transform.up, normal_v) * transform.rotation;

        transform.LookAt(nav.destination);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nav.destination - transform.position), 1.5f);
    }
}
