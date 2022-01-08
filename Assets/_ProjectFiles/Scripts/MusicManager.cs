using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class MusicManager : MonoBehaviour
{
    
    //public static MusicManager instance;
    //public float[] spectrumWidth;

    AudioSource audioSource;
    AudioClip myClip;
    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
        //spectrumWidth = new float[64];

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(GetAudioClip());
        Debug.Log("Downloading Audio...");
    }

    IEnumerator GetAudioClip() //Pega a URL da música, faz download e toca dentro do jogo, tocando como AudioSource
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://ytop1.net/pt/Thankyou?token=U2FsdGVkX19Gkjg3K96lKhLc9Y%2fy5PCUiyFgquUZt%2fMnGm3waZlR7lNPHlvblk94buNT7VI415nfR9XYR%2byzLhjw8WBIHPmVTQLOlbn3SV31K7TB3A9C39BB1EQxrB%2bxRgKDlHbJl2BSj5E7vji%2bETyu58sjazxvBy0oB4UQFQnDqXwjr8q4ye69oBMZOdAPBycCVgxRa%2fFtlMfcFuhpfTR7EYPRjNnK9x1SbTpPQMY%3d&s=youtube&id=&h=7986292408680756847", AudioType.MPEG))
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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman); //GetSpectrumData retorna os dados de frequências da música
    }

    /*public float getFrequenciesDiapason(int start, int end, int mult)
    {
        return spectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }*/
}
