using UnityEngine;

public enum ElectronMoveState
{
    MOVING,
    NOT_MOVING,
}

[RequireComponent(typeof(Rigidbody))]
public class Electron : MonoBehaviour
{
    [SerializeField] Vector3 velocityVector = Vector3.zero;

    [SerializeField, Range(-90, 90)] public float angle;
    [SerializeField] public float velocity;
    [SerializeField] public float charge;
    [SerializeField] public float magneticFieldStrength = 10;

    [SerializeField] ElectronMoveState currentElectronMoveState;

    private Rigidbody electronBody;

    private Vector3 prevPosition;
    private Vector3 directionOfMovement;
    private Vector3 magneticFieldVector = Vector3.right;

    private void Awake()
    {
        //INIT
        electronBody = GetComponent<Rigidbody>();
        prevPosition = transform.position;
    }

    private void ToggleSim()
    {
        if (currentElectronMoveState == ElectronMoveState.MOVING) currentElectronMoveState = ElectronMoveState.MOVING;
        else currentElectronMoveState = ElectronMoveState.NOT_MOVING;
    }

    private void FixedUpdate()
    {
        velocityVector = new Vector3(velocity * Mathf.Cos(angle * Mathf.Deg2Rad), velocity * Mathf.Sin(angle * Mathf.Deg2Rad));

        directionOfMovement = (transform.position - prevPosition).normalized == Vector3.zero ? velocityVector.normalized : (transform.position - prevPosition).normalized;

        directionOfMovement = new Vector3(velocityVector.x, directionOfMovement.y, directionOfMovement.z);

        electronBody.velocity = velocityVector.magnitude * directionOfMovement;

        if (currentElectronMoveState == ElectronMoveState.MOVING)
        {
            electronBody.AddForce(GetForce(charge, directionOfMovement * velocityVector.magnitude, magneticFieldVector * magneticFieldStrength), ForceMode.Force);
        }

        prevPosition = transform.position;
    }

    private Vector3 GetForce(float charge, Vector3 velocity, Vector3 magneticFieldVector)
    {
        Vector3 forceDirection = Vector3.Cross(velocity, magneticFieldVector).normalized;

        float forceMagnitude = charge * velocityVector.magnitude * magneticFieldVector.magnitude;

        return forceDirection * forceMagnitude;
    }
}
