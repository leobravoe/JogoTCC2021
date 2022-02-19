using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicaSelecionada : MonoBehaviour
{
    private string enderecoMusica;
    public InputField musicaInputField;

    public string EnderecoMusica { get => enderecoMusica; set => enderecoMusica = value; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        enderecoMusica = musicaInputField.text;
    }
}
