using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavgator : MonoBehaviour
{
    NavMeshAgent nav;
    public Transform backLeft;
    public Transform backRight;
    public Transform frontLeft;
    public Transform frontRight;
    private RaycastHit lr;
    private RaycastHit rr;
    private RaycastHit lf;
    private RaycastHit rf;
    private Vector3 upDir;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        // Tenta ajustar a inclinação do modelo
        Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
        Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);

        upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                 Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                 Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                 Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                ).normalized;
        Debug.DrawRay(rr.point, Vector3.up);
        Debug.DrawRay(lr.point, Vector3.up);
        Debug.DrawRay(lf.point, Vector3.up);
        Debug.DrawRay(rf.point, Vector3.up);
        Debug.DrawRay(transform.position, upDir);

        transform.up = upDir;

        print(upDir);


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                nav.destination = hit.point;
            }
        }
    }
}
