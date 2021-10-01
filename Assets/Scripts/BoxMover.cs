using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    public Rigidbody rb;
    Transform trf;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trf = GetComponent<Transform>();
        //direction = new Vector3(0f, 0f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        direction = trf.forward * -20;
        // Debugando o código
        //print(direction);
        //trf.Translate(0f, 0f, -0.1f);
        rb.AddForce(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("colidiu com " + collision.gameObject.name);
        if (collision.gameObject.name == "PlayerArmature")
        {
            //Mata o jogador
            Destroy(gameObject);
        }
    }

}
