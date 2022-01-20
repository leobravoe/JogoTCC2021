using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class MusicManager : MonoBehaviour
{
    public GameObject player;
    public GameObject fantasma;
    public static MusicManager instance;
    public float[] spectrumWidth;
    public GameObject playerMusicManager;

    private bool mudando;
    private float pitchOriginal;
    private float tempoPraMudar;
    private float pitchMin;

    AudioSource audioSource;
    AudioClip myClip;
    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spectrumWidth = new float[64];

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(GetAudioClip());
        Debug.Log("Downloading Audio...");

        //playerMusicManager.GetComponent<AudioSource>().pitch = pitchOriginal;
        //mudando = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstaculo"))
        {
            //MudaPitch();
            Debug.Log("Mudar Pitch");
        }
    }

    IEnumerator GetAudioClip() //Pega a URL da música, faz download e toca dentro do jogo, tocando como AudioSource
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://ytop1.net/pt/Thankyou?token=U2FsdGVkX1906yFJDQeARBrmPo6E2ioqygQENO4QpQSbiaLt%2f6B%2fw4s5yP5rIu8trmE5dtsxouUYsWz94F5ne2Q2dRUpJ2EZDtsUqOAUPQp4II%2bSqJVaHlY5xfEj4p3wDRw2rzKoJtOEZlsS0%2fNl2wvhF%2btsM%2fQtYpw4jtZrcj5wemguwtM5Egsfi3b2Av5usY8%2fkcgzvQA0rC4XK56KB9IBOgI4Cgqc3xfNBvIrYd7KgR%2fsIj%2bsMEasFITTCJoo&s=youtube&id=&h=6785987323857762984", AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
                Debug.Log("Tocando Musica...");

                // Ativa o playerMusicManager
                StartCoroutine(CoroutineParaAtivarOPlayerMusicManager());

                player.SetActive(true);
                fantasma.SetActive(true);
            }
        }
    }
    IEnumerator CoroutineParaAtivarOPlayerMusicManager()
    {
        // Espera dois segundos
        yield return new WaitForSeconds(2.3f);

        playerMusicManager.GetComponent<AudioSource>().clip = myClip;
        playerMusicManager.GetComponent<AudioSource>().Play();
    }


        // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman); //GetSpectrumData retorna os dados de frequências da música

        /*while (playerMusicManager.GetComponent<AudioSource>().pitch > pitchMin)
        {
            playerMusicManager.GetComponent<AudioSource>().pitch -= Time.deltaTime * pitchOriginal / tempoPraMudar;
        }

        while (playerMusicManager.GetComponent<AudioSource>().pitch == pitchOriginal)
        {
            playerMusicManager.GetComponent<AudioSource>().pitch += Time.deltaTime * pitchOriginal / tempoPraMudar;
        }

        mudando = false;*/
    }

    public float getFrequenciesDiapason(int start, int end, int mult)
    {
        return spectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }

    public void MudaPitch()
    {
        pitchOriginal = 1;
        tempoPraMudar = 2;
        pitchMin = 0.3f;
        mudando = true;
    }
}

