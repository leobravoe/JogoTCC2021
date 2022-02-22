using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PegaPontos : MonoBehaviour
{

    public GameObject scoreText;
    public int score;
    AudioSource audio;
    public AudioClip som;
    public float volume;

    public GameObject successParticle;
    public GameObject errorParticle;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ponto"))
        {
            score += 1;
            scoreText.GetComponent<Text>().text= "x" + score;
            Destroy(other.gameObject);
            Destroy(Instantiate(successParticle, transform.position, transform.rotation),1);
        }

        if (other.gameObject.CompareTag("Obstaculo"))
        {
            if (score <= 10)
            {
                score = 0;
            }
            else
            {
                score -= 10;
            }

            scoreText.GetComponent<Text>().text = "x" + score;
            Destroy(other.gameObject);
            Destroy(Instantiate(errorParticle, transform.position, transform.rotation), 1);

            GameObject.Find("Main Camera").SendMessage("DoShake"); //Manda uma mensagem para a MainCamera para iniciar o método DoShake
            audio.PlayOneShot(som, volume);
            Debug.Log("Colisão");

        }
        if (other.gameObject.CompareTag("TriggerInicio"))
        {
            Destroy(other.gameObject);
        }
    }
}
