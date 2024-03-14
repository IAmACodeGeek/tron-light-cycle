using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour{
    public Transform target;
    public Vector3 offset, lookOffset;

    public float smoothSpeed = 5f;

    private void LateUpdate(){
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.smoothDeltaTime);
        transform.LookAt(target.position + lookOffset);
    }
}
