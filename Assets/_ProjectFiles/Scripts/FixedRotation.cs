using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    public int x = 1;
    public int y = 1;
    public int z = 1;
    public float velocity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x*velocity, y*velocity, z*velocity);
    }
}
