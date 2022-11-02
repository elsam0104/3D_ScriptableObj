using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public float height = 5.0f;
    public float distance = 10f;
    public float angle = 45f;
    public float lookAtHeight = 1f;
    public float smoothSpeed = .5f;

    private Vector3 refVelocity;
    
    void Start()
    {
           
    }

    void LateUpdate()
    {
        HandleCamera();
    }

    void HandleCamera()
    {
        if (!target)
            return;
        Vector3 worldPos = (Vector3.forward * -distance) + (Vector3.up * height);
        Vector3 roatateVec = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;

        Vector3 flatTargetPos = target.position;// target이 참조형이니 넣어주고 연산
        flatTargetPos.y += lookAtHeight;

        Vector3 finalPos = flatTargetPos + roatateVec;

        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref refVelocity, smoothSpeed);

        transform.LookAt(flatTargetPos);
    }
}
