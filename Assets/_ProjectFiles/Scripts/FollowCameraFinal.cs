using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraFinal : MonoBehaviour
{
    public Transform target; //Target referencia o carro
    public float x_scale = 5f;
    public float y_scale = 5f;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;
    private Vector3 OriginalPos;
    private Quaternion OriginalRot;

    // Start is called before the first frame update
    void Start()
    {
        Shaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Método para aleatorização do movimento de câmera
        if (ShakeIntensity > 0)
        {
            transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
            transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                      OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                      OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                      OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);

            ShakeIntensity -= ShakeDecay;
        }
        else if (Shaking)
        {
            Shaking = false;
        }

        transform.position = Vector3.SmoothDamp(transform.position, (target.position - target.forward * x_scale) + Vector3.up * y_scale, ref velocity, smoothTime);
        transform.LookAt(target);
    }

    public void DoShake()
    {
        OriginalPos = transform.position;
        OriginalRot = transform.rotation;

        ShakeIntensity = 1.6f;
        ShakeDecay = 0.02f;
        Shaking = true;
    } 
}
