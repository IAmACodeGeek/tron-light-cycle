using UnityEngine;

public class Movement : MonoBehaviour{
    private Rigidbody bikeBody;
    public float forwardSpeed = 5f;

    private void Start(){
        bikeBody = GetComponent<Rigidbody>();
    }

    void Update(){
        if(Input.GetKey(KeyCode.UpArrow)){
            Vector3 direction = transform.forward;
            direction.Normalize();
            bikeBody.velocity = direction * forwardSpeed;
        }
        else if(Input.GetKey(KeyCode.DownArrow)){
            Vector3 direction = transform.forward * -1;
            direction.Normalize();
            bikeBody.velocity = direction * forwardSpeed;
        }
        else{
            bikeBody.velocity = Vector3.zero;
        }
    }
}
