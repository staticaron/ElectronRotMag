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
    [SerializeField] float charge;
    [SerializeField] Vector3 magneticFieldVector;

    [SerializeField] ElectronMoveState currentElectronMoveState;

    private Rigidbody electronBody;

    private Vector3 prevPosition;
    private Vector3 direction;

    private void Awake()
    {
        //INIT
        electronBody = GetComponent<Rigidbody>();
        prevPosition = transform.position;
    }

    private void FixedUpdate()
    {
        electronBody.velocity = velocityVector;

        Vector3 directionOfMovement = (transform.position - prevPosition).normalized;

        if (currentElectronMoveState == ElectronMoveState.MOVING)
        {
            Vector3 force = GetForce(charge, directionOfMovement * velocityVector.magnitude, magneticFieldVector);
            electronBody.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private Vector3 GetForce(float charge, Vector3 velocityVector, Vector3 magneticFieldVector)
    {
        Vector3 forceDirection = Vector3.Cross(velocityVector.normalized, magneticFieldVector.normalized);

        float forceMagnitude = charge * Mathf.Sqrt(Vector3.SqrMagnitude(velocityVector)) * Mathf.Sqrt(Vector3.SqrMagnitude(magneticFieldVector));

        Debug.Log(forceMagnitude);

        Debug.DrawLine(transform.position, transform.position - forceDirection, Color.red);

        return forceDirection * forceMagnitude;
    }
}
