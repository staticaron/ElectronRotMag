using UnityEngine;

public enum ElectronMoveState
{
    MOVING,
    NOT_MOVING,
}

[RequireComponent(typeof(Rigidbody))]
public class Electron : MonoBehaviour
{
    [SerializeField] float velocity;
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
        electronBody.velocity = transform.forward * velocity;

        Vector3 velocityVector = (transform.position - prevPosition).normalized * velocity;

        if (currentElectronMoveState == ElectronMoveState.MOVING)
        {
            Vector3 force = GetForce(charge, velocityVector, magneticFieldVector);
            electronBody.AddForce(force, ForceMode.Force);
        }
    }

    private Vector3 GetForce(float charge, Vector3 velocityVector, Vector3 magneticFieldVector)
    {
        Vector3 forceDirection = Vector3.Cross(velocityVector.normalized, magneticFieldVector.normalized);

        float forceMagnitude = charge * Mathf.Sqrt(Vector3.SqrMagnitude(velocityVector)) * Mathf.Sqrt(Vector3.SqrMagnitude(magneticFieldVector));

        return forceDirection * forceMagnitude;
    }
}
