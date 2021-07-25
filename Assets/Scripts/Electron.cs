using System;
using UnityEngine;

public enum ElectronMoveState
{
    Moving,
    NotMoving,
}

[RequireComponent(typeof(Rigidbody))]
public class Electron : MonoBehaviour
{
    [SerializeField] Vector3 velocityVector = Vector3.zero;

    [SerializeField, Range(0, 90)] public float angle;
    [SerializeField] public float velocity;
    [SerializeField] public float charge;
    [SerializeField] public float magneticFieldStrength = 10;

    [SerializeField] ElectronMoveState currentElectronMoveState = ElectronMoveState.NotMoving;

    private Rigidbody electronBody;

    private Vector3 prevPosition;
    private Vector3 directionOfMovement;
    private Vector3 magneticFieldVector = Vector3.right;

    [SerializeField] PlayStateChannelSO playStateChannelSO;
    [SerializeField] ElectronDataChannelSO electronDataChannelSO;

    private void Awake()
    {
        electronBody = GetComponent<Rigidbody>();
        prevPosition = transform.position;
        currentElectronMoveState = ElectronMoveState.NotMoving;
    }

    private void OnEnable()
    {
        playStateChannelSO.ESetState += SetState;
        electronDataChannelSO.EValuesUpdated += UpdateValues;
    }

    private void OnDisable()
    {
        playStateChannelSO.ESetState -= SetState;
        electronDataChannelSO.EValuesUpdated -= UpdateValues;
    }

    private void UpdateValues()
    {
        this.angle = electronDataChannelSO.angle;
        this.magneticFieldStrength = electronDataChannelSO.magneticFieldStrength;
        this.velocity = electronDataChannelSO.speed;
        this.charge = electronDataChannelSO.charge;
    }

    private void SetState(PlayState stateToSet)
    {
        if (stateToSet == PlayState.Play) currentElectronMoveState = ElectronMoveState.Moving;
        if (stateToSet == PlayState.Pause) currentElectronMoveState = ElectronMoveState.NotMoving;
    }

    private void FixedUpdate()
    {
        if (currentElectronMoveState != ElectronMoveState.Moving)
        {
            electronBody.velocity = Vector3.zero;
            return;
        }

        velocityVector = new Vector3(velocity * Mathf.Cos(angle * Mathf.Deg2Rad), velocity * Mathf.Sin(angle * Mathf.Deg2Rad));

        directionOfMovement = (transform.position - prevPosition).normalized == Vector3.zero ? velocityVector.normalized : (transform.position - prevPosition).normalized;

        directionOfMovement = new Vector3(velocityVector.x, directionOfMovement.y, directionOfMovement.z);

        electronBody.velocity = velocityVector.magnitude * directionOfMovement;
        electronBody.AddForce(GetForce(charge, directionOfMovement * velocityVector.magnitude, magneticFieldVector * magneticFieldStrength), ForceMode.Force);

        prevPosition = transform.position;
    }

    private Vector3 GetForce(float charge, Vector3 velocity, Vector3 magneticFieldVector)
    {
        Vector3 forceDirection = Vector3.Cross(velocity, magneticFieldVector).normalized;

        float forceMagnitude = charge * velocityVector.magnitude * magneticFieldVector.magnitude;

        return forceDirection * forceMagnitude;
    }
}
