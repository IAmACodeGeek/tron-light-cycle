using UnityEngine;

public class Movement : MonoBehaviour{
    private Rigidbody bikeBody;
    
    public float topSpeed = 40f;
    private float currentSpeed = 0f;
    public float accelerationFactor = 1f;
    public float decelerationFactor = 1f;
    public float brakingFactor = 1f;

    // Degrees
    private float maxLean = 30f;
    private float currentLean = 0f;
    public float leanFactor = 3f;
    public float deleanFactor = 3f;
    public float turnThreshold = 5f;
    public float turnSpeedDependancyFactor = 0.15f;

    public float maxTurnSpeed = 25f;

    private void Awake(){
        bikeBody = GetComponent<Rigidbody>();
    }

    void Update(){
        // Forward Movement
        if(Input.GetKey(KeyCode.Space)){
            Decelerate(brakingFactor);
        }
        else if(Input.GetKey(KeyCode.UpArrow)){
            Accelerate(accelerationFactor);
        }
        else{
            Decelerate(decelerationFactor);
        }

        // Leaning left or right
        if((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) || !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))){
            DeLean();
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            Lean(1);
        }
        else{
            Lean(-1);
        }

        // Movement
        Vector3 direction = transform.forward;
        direction.Normalize();
        Vector3 bikeVelocity = direction * currentSpeed;
        transform.position = transform.position + bikeVelocity * Time.smoothDeltaTime;

        // Lean and Change direction
        float currentDirectionAngle = transform.eulerAngles.y;
        currentDirectionAngle -= Time.smoothDeltaTime * maxTurnSpeed * (currentLean/maxLean);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, currentDirectionAngle, currentLean);
    }

    void Accelerate(float factor){
        currentSpeed = Mathf.Lerp(currentSpeed, topSpeed, accelerationFactor * Time.smoothDeltaTime);
    }

    void Decelerate(float factor){
        currentSpeed = Mathf.Lerp(currentSpeed, 0f, factor * Time.smoothDeltaTime);
    }

    void Lean(int direction){
        if(currentSpeed > turnThreshold)
            currentLean = Mathf.Lerp(currentLean, direction * maxLean * (1f - (currentSpeed/topSpeed)*turnSpeedDependancyFactor), leanFactor * Time.smoothDeltaTime);
        else
            DeLean();
    }

    void DeLean(){
        currentLean = Mathf.Lerp(currentLean, 0f, deleanFactor * Time.smoothDeltaTime);
    }
}
