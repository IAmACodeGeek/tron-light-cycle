using System;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 5f;
    public float lookOffset = 1.75f;

    private float currentOmega = 0f;
    public float angularSmoothSpeed = 30f;

    // For some high level math that i did but can never explain to anyone
    private float FOLLOW_CIRCLE_RADIUS = 0f;
    private float DELTA = 0f;

    void Start(){
        FOLLOW_CIRCLE_RADIUS = (float) Math.Sqrt(offset.x * offset.x + offset.z * offset.z);
        DELTA = (float) Math.Tanh(offset.x / offset.z);
        DELTA = Mathf.Deg2Rad * DELTA;
    }

    void LateUpdate(){
        float theta = target.eulerAngles.y;
        float omega = theta + DELTA;

        omega = Mathf.Deg2Rad * omega;
        if(omega - currentOmega > 5) currentOmega += 2 * Mathf.PI;
        else if(omega - currentOmega < -5) currentOmega -= 2 * Mathf.PI;
        currentOmega = Mathf.Lerp(currentOmega, omega, angularSmoothSpeed * Time.smoothDeltaTime);

        float desiredX = (float)(target.position.x + Math.Sin(-currentOmega) * FOLLOW_CIRCLE_RADIUS);
        float desiredZ = (float)(target.position.z - Math.Cos(currentOmega) * FOLLOW_CIRCLE_RADIUS);
        float desiredY = target.position.y + offset.y;

        Vector3 desiredPosition = new Vector3(desiredX, desiredY, desiredZ);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.smoothDeltaTime);
        
        Vector3 lookPosition = target.position + target.forward.normalized * lookOffset;
        transform.LookAt(lookPosition);  
    }
}
