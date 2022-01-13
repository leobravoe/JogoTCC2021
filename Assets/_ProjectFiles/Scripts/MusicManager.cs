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
    }

    IEnumerator GetAudioClip() //Pega a URL da música, faz download e toca dentro do jogo, tocando como AudioSource
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://ytop1.net/pt/Thankyou?token=U2FsdGVkX18RLQq2c7EniOVrkPEx9KAcmID%2bVVjXcSG8jQuY6eHKDADFt7O1FDscgET5qogA%2bUto3t6ORXHHciPhJ9wiaRVj8RSTIFArIaUqt2ccvcN1%2fSdcoPsBde904%2fBhgRSJofNeEPXeSWXdVl0yFvgUD2OZYk3SNM6jThVE1eXUFZfUoIo3ytM%2bLr%2bT&s=youtube&id=&h=7986292408680757838", AudioType.MPEG))
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
        yield return new WaitForSeconds(2);

        playerMusicManager.GetComponent<AudioSource>().clip = myClip;
        playerMusicManager.GetComponent<AudioSource>().Play();
    }


        // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman); //GetSpectrumData retorna os dados de frequências da música
    }

    public float getFrequenciesDiapason(int start, int end, int mult)
    {
        return spectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }
}
