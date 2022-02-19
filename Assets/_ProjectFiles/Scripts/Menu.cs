using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void iniciarTelaMusica()
    {
        if (GameObject.Find("MusicaSelecionada"))
            Destroy(GameObject.Find("MusicaSelecionada"));
        SceneManager.LoadScene("TelaEscolhaMusica");
    }

    public void iniciar()
    {
        SceneManager.LoadScene("Interlagos");
    }

    public void sair()
    {
        //No Edtior
        //UnityEditor.EditorApplication.isPlaying = false;

        //No Jogo
        Application.Quit();
    }
}
