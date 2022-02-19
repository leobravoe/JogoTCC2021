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

    AudioSource audioSourceFantasma;
    AudioClip myClipFantasma;
    public GameObject CanvasErroMusica;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spectrumWidth = new float[64];

        audioSourceFantasma = GetComponent<AudioSource>();
        StartCoroutine(GetAudioClip());
        Debug.Log("Downloading Audio...");

        //playerMusicManager.GetComponent<AudioSource>().pitch = pitchOriginal;
        //mudando = false;
    }

    IEnumerator GetAudioClip() //Pega a URL da música, faz download e toca dentro do jogo, tocando como AudioSource
    {
        UnityWebRequest www;
        if (GameObject.Find("MusicaSelecionada"))
        {
            www = UnityWebRequestMultimedia.GetAudioClip(GameObject.Find("MusicaSelecionada").GetComponent<MusicaSelecionada>().EnderecoMusica, AudioType.MPEG);
        }
        else
        {
            www = UnityWebRequestMultimedia.GetAudioClip("https://ytop1.net/pt/Thankyou?token=U2FsdGVkX18yYyY%2fhIaGARbrDF7GG0w6M9pgOmViyDzMIzYvuwDKOPlcgjGhMufpHfl9rsPCJlQahnLA%2fUjYce%2f09tts8TINNeFRgqliHSzK8KMEukY1APTyD79ciXyjq35L7ALxYeX1AKzb5TYIRfUdsJfrmjusIG5PNR%2f97kOLMXWNQ63E%2bVxK%2fH4%2fGd4L8uJykVNdY4gcPGpdm7IuaA%3d%3d&s=youtube&id=&h=6785987323857765821", AudioType.MPEG);
        }
        
        using (www)
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                CanvasErroMusica.SetActive(true);
            }
            else
            {
                myClipFantasma = DownloadHandlerAudioClip.GetContent(www);
                audioSourceFantasma.clip = myClipFantasma;
                if(audioSourceFantasma.clip.isReadyToPlay)
                {
                    player.GetComponent<AudioSource>().clip = myClipFantasma;
                    audioSourceFantasma.Play();
                    player.GetComponent<AudioSource>().Stop();
                    Debug.Log("Tocando Musica no Fantasma...");

                    player.SetActive(true);
                    fantasma.SetActive(true);
                }
                else
                {
                    CanvasErroMusica.SetActive(true);
                }
            }
        }
    }


        // Update is called once per frame
    void Update()
    {
        if(audioSourceFantasma && myClipFantasma)
        {
            audioSourceFantasma.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman); //GetSpectrumData retorna os dados de frequências da música

            //Debug.Log("Tempo musica" + audioSourceFantasma.time);
            //Debug.Log("Final" + myClipFantasma.length);
            if (audioSourceFantasma.time >= myClipFantasma.length)
            {
                audioSourceFantasma.mute = true;
            }
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

