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
    public AudioClip defaultSong;

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

        if (GameObject.Find("MusicaSelecionada").GetComponent<MusicaSelecionada>().EnderecoMusica == "https://coloque-o-link-aqui.com.br")
        {
            myClipFantasma = defaultSong;
            audioSourceFantasma.clip = myClipFantasma;
            player.GetComponent<AudioSource>().clip = myClipFantasma;
            audioSourceFantasma.Play();
            player.GetComponent<AudioSource>().Stop();
            Debug.Log("Tocando Musica no Fantasma...");

            player.SetActive(true);
            fantasma.SetActive(true);
        }
        else
        {
            UnityWebRequest www;
            if (GameObject.Find("MusicaSelecionada"))
            {
                www = UnityWebRequestMultimedia.GetAudioClip(GameObject.Find("MusicaSelecionada").GetComponent<MusicaSelecionada>().EnderecoMusica, AudioType.MPEG);
            }
            else
            {
                www = UnityWebRequestMultimedia.GetAudioClip("https://cdn02.mp3yt.link/download/zOILAZHf2pE/mp3/128/1645492590/c044b7d295c3bcd32387a0d59238896d218bb45f3178565ba8465907999ab8b5/1", AudioType.MPEG);
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
                    if (audioSourceFantasma.clip.isReadyToPlay)
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
                Invoke("WinCodition", 5);
            }
        }
    }

    protected void WinCodition()
    {
        CanvasErroMusica.SetActive(true);
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

