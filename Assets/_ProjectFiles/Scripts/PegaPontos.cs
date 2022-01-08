using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PegaPontos : MonoBehaviour
{

    public GameObject scoreText;
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
