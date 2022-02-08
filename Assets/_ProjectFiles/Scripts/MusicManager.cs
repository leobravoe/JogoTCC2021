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

    IEnumerator GetAudioClip() //Pega a URL da m�sica, faz download e toca dentro do jogo, tocando como AudioSource
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://ytop1.net/pt/Thankyou?token=U2FsdGVkX18yYyY%2fhIaGARbrDF7GG0w6M9pgOmViyDzMIzYvuwDKOPlcgjGhMufpHfl9rsPCJlQahnLA%2fUjYce%2f09tts8TINNeFRgqliHSzK8KMEukY1APTyD79ciXyjq35L7ALxYeX1AKzb5TYIRfUdsJfrmjusIG5PNR%2f97kOLMXWNQ63E%2bVxK%2fH4%2fGd4L8uJykVNdY4gcPGpdm7IuaA%3d%3d&s=youtube&id=&h=6785987323857765821", AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClipFantasma = DownloadHandlerAudioClip.GetContent(www);
                audioSourceFantasma.clip = myClipFantasma;
                player.GetComponent<AudioSource>().clip = myClipFantasma;
                audioSourceFantasma.Play();
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
        audioSourceFantasma.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman); //GetSpectrumData retorna os dados de frequ�ncias da m�sica

        Debug.Log("Tempo musica" + audioSourceFantasma.time);
        Debug.Log("Final" + myClipFantasma.length);
        if ( audioSourceFantasma.time >= myClipFantasma.length){
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

