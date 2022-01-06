using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraFinal : MonoBehaviour
{
    public Transform target;
    public float x_scale = 5f;
    public float y_scale = 5f;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, (target.position - target.forward * x_scale) + Vector3.up * y_scale, ref velocity, smoothTime);
        //transform.position = (target.position - target.forward*x_scale) + Vector3.up*y_scale;
        transform.LookAt(target);
    }
}
