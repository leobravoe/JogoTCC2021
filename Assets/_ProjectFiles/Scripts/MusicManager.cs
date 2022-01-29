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
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://ytop1.net/pt/Thankyou?token=U2FsdGVkX18idHpBdP12Rh1Y%2bIW5gDpyr36Vh5FGEwLowvIyuG6l3wpATGdeF0LlnnOI0fasiLwLdleLHtOUcSYKbxWvG%2bFEer4V0%2b0ZNPzL%2fpGyB%2fWgSyrCW9sa%2bGfftD8t7hQeKT9W91XW1XKEXwflp4yJSfLBeWqvvL4P8GJQeN%2b1myGkjb9JPc8am2Ze3VfPkRFIfxX6VOYlW0rWvA%3d%3d&s=youtube&id=&h=6785987323857763105", AudioType.MPEG))
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
                player.GetComponent<AudioSource>().clip = myClip;
                audioSource.Play();
                player.GetComponent<AudioSource>().Stop();
                Debug.Log("Tocando Musica no Fantasma...");

                player.SetActive(true);
                fantasma.SetActive(true);
            }
        }
    }


        // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman); //GetSpectrumData retorna os dados de frequências da música

        Debug.Log("Tempo musica" + audioSource.time);
        Debug.Log("Final" + myClip.length);
        if ( audioSource.time >= myClip.length){
            Destroy(fantasma);
        }
        

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

