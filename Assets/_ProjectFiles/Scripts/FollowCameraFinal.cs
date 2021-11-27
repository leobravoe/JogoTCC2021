using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraFinal : MonoBehaviour
{
    public Transform target;
    public float x_scale = 5f;
    public float y_scale = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (target.position - target.forward*x_scale) + Vector3.up*y_scale;
        transform.LookAt(target);
    }
}
