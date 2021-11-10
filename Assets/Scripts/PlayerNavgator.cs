using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavgator : MonoBehaviour
{
    NavMeshAgent nav;
    private NavMeshAgent agent;
    private Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        nav.updateRotation = false;          //< let us control the rotation explicitly
        lookRotation = transform.rotation;     //< set original rotation
    }

    protected Vector3 GetTerrainNormal() {
        RaycastHit hit;
        Vector3 terrainLocalPos = transform.position;
        Physics.Raycast(transform.position, Vector3.down, out hit, 2f, 1 << LayerMask.NameToLayer("Ground"));
        return hit.normal;
    }

    // Update is called once per frame
    void Update() {
        Vector3 normal_v = GetTerrainNormal();
        print(normal_v);
        Debug.DrawRay(transform.position, normal_v, Color.yellow);
        Vector3 direction = nav.steeringTarget - transform.position;
        direction.y = 0.0f;
        if (direction.magnitude > 0.1f || normal_v.magnitude > 0.1f) {
            Quaternion qLook = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion qNorm = Quaternion.FromToRotation(Vector3.up, normal_v);
            lookRotation = qNorm * qLook;
        }
        // soften the orientation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime / 0.2f);
    }
}
